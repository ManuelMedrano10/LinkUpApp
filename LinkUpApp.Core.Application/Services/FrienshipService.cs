using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Friendship;
using LinkUpApp.Core.Application.Interfaces;
using LinkUpApp.Core.Domain.Common.Enums;
using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Core.Domain.Interfaces;

namespace LinkUpApp.Core.Application.Services
{
    public class FrienshipService : GenericService<Friendship, FriendshipDto>, IFriendshipService
    {
        private readonly IFriendshipRepository _repository;
        private readonly IAccountServiceWebApp _accountService;
        private readonly IMapper _mapper;

        public FrienshipService(IFriendshipRepository repository, IMapper mapper, IAccountServiceWebApp accountService) 
            : base(repository, mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AcceptRequestAsync(int friendshipId)
        {
            var friendship = await _repository.GetById(friendshipId);
            if (friendship != null && friendship.Status == FriendshipStatus.Pending)
            {
                friendship.Status = FriendshipStatus.Accepted;
                await _repository.UpdateAsync(friendshipId, friendship);
            }
        }

        public async Task DeleteFriendAsync(int friendshipId)
        {
            var friendship = await _repository.GetById(friendshipId);
            if (friendship != null)
            {
                await _repository.DeleteAsync(friendshipId);
            }
        }

        public async Task RejectRequestAsync(int friendshipId)
        {
            var friendship = await _repository.GetById(friendshipId);
            if (friendship != null && friendship.Status == FriendshipStatus.Pending)
            {
                friendship.Status = FriendshipStatus.Rejected;
                await _repository.UpdateAsync(friendshipId, friendship);
            }
        }

        public async Task<List<FriendshipDto>> GetFriendsAsync(string currentUserId)
        {
            var allFriendships = await _repository.GetAllFriendshipsByUserIdAsync(currentUserId);
            var friends = allFriendships.Where(f => f.Status == FriendshipStatus.Accepted).ToList();

            return await MapToDtoAndPopulateUser(friends, currentUserId);
        }

        public async Task<List<FriendshipDto>> GetPendingRequestsAsync(string currentUserId)
        {
            var allFriendships = await _repository.GetAllList();

            var pending = allFriendships.Where(f => f.ReceiverUserId == currentUserId && f.Status == FriendshipStatus.Pending).ToList();

            var dtos = await MapToDtoAndPopulateUser(pending, currentUserId);
            return dtos.OrderByDescending(x => x.CreatedAt).ToList();
        }

        public async Task<List<FriendshipDto>> GetSentRequestsAsync(string currentUserId)
        {
            var allFriendships = await _repository.GetAllList();

            var sent = allFriendships.Where(f => f.SenderUserId == currentUserId).ToList();

            var viewModels = await MapToDtoAndPopulateUser(sent, currentUserId);
            return viewModels.OrderByDescending(x => x.CreatedAt).ToList();
        }

        private async Task<List<FriendshipDto>> MapToDtoAndPopulateUser(List<Friendship> friendships, string currentUserId)
        {
            var viewModels = new List<FriendshipDto>();
            var allCurrentFriendships = await _repository.GetAllFriendshipsByUserIdAsync(currentUserId);
            var currentUserFriendsIds = GetFriendIds(allCurrentFriendships, currentUserId);

            foreach (var f in friendships)
            {
                FriendshipDto vm = new()
                {
                    Id = f.Id,
                    SenderUserId = f.SenderUserId,
                    ReceiverUserId = f.ReceiverUserId,
                    Status = f.Status,
                    CreatedAt = f.CreatedAt,
                    OtherUserId = "",
                    OtherUserName = "",
                    OtherUserPhotoUrl = "",
                    MutualFriendsCount = 0
                };

                string otherUserId = f.SenderUserId == currentUserId ? f.ReceiverUserId : f.SenderUserId;
                vm.OtherUserId = otherUserId;

                var otherUser = await _accountService.GetUserById(otherUserId);
                if (otherUser != null)
                {
                    vm.OtherUserName = otherUser.UserName;
                    vm.OtherUserPhotoUrl = otherUser.ProfileImage ?? "";
                }

                var otherUserFriendships = await _repository.GetAllFriendshipsByUserIdAsync(otherUserId);
                var otherUserFriendsIds = GetFriendIds(otherUserFriendships, otherUserId);

                vm.MutualFriendsCount = currentUserFriendsIds.Intersect(otherUserFriendsIds).Count();

                viewModels.Add(vm);
            }

            return viewModels;
        }

        private List<string> GetFriendIds(List<Friendship> friendships, string userId)
        {
            return friendships
                .Where(f => f.Status == FriendshipStatus.Accepted)
                .Select(f => f.SenderUserId == userId ? f.ReceiverUserId : f.SenderUserId)
                .ToList();
        }
    }
}
