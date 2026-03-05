using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Post;
using LinkUpApp.Core.Application.Interfaces;
using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Core.Domain.Interfaces;
using LinkUpApp.Infraesctructure.Persistence.Repositories;

namespace LinkUpApp.Core.Application.Services
{
    public class PostService : GenericService<Post, PostDto, SavePostDto>, IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IAccountServiceWebApp _accountService;
        private readonly IMapper _mapper;
        private readonly IReactionRepository _reactionRepository;
        private readonly IFriendshipService _frienshipService;

        public PostService(IPostRepository repository, IMapper mapper, IAccountServiceWebApp accountService,
                            IReactionRepository reactionRepository, IFriendshipService frienshipService)
            : base(repository, mapper)
        {
            _repository = repository;
            _accountService = accountService;
            _mapper = mapper;
            _frienshipService = frienshipService;
            _reactionRepository = reactionRepository;
        }

        public async Task<List<PostDto>> GetAllByUserIdAsync(string userId)
        {
            var posts = await _repository.GetAllWithCommentsAsync();
            var userPosts = posts.Where(p => p.UserId == userId).ToList();
            var postDtos = _mapper.Map<List<PostDto>>(userPosts);
            await PopulateUserInformation(postDtos, userId);

            return postDtos;
        }

        public async Task<List<PostDto>> GetAllFromFriendsAsync(string userId)
        {
            var friends = await _frienshipService.GetFriendsAsync(userId);
            var friendsIds = friends.Select(f => f.OtherUserId).ToList();
            friendsIds.Add(userId);

            var posts = await _repository.GetAllWithCommentsAsync();
            var filteredPosts = posts.Where(p => friendsIds.Contains(p.UserId)).ToList();

            var postDtos = _mapper.Map<List<PostDto>>(filteredPosts);
            await PopulateUserInformation(postDtos, userId);

            return postDtos;
        }

        public async Task ToggleReactionAsync(int postId, string userId, bool isLike)
        {
            var reactions = await _reactionRepository.GetAllList();
            var existingReaction = reactions.FirstOrDefault(r => r.PostId == postId && r.UserId == userId);

            if (existingReaction != null)
            {
                if (existingReaction.IsLiked == isLike)
                {
                    await _reactionRepository.DeleteAsync(existingReaction.Id);
                }
                else
                {
                    existingReaction.IsLiked = isLike;
                    await _reactionRepository.UpdateAsync(existingReaction.Id, existingReaction);
                }
            }
            else
            {
                var newReaction = new Reaction
                {
                    Id = 0,
                    PostId = postId,
                    UserId = userId,
                    IsLiked = isLike,
                };
                await _reactionRepository.AddAsync(newReaction);
            }
        }

        private async Task PopulateUserInformation(List<PostDto> postDtos, string currentUserId)
        {
            foreach (var post in postDtos)
            {
                var postAuthor = await _accountService.GetUserById(post.UserId);
                if (postAuthor != null)
                {
                    post.UserName = postAuthor.UserName;
                    post.UserPhotoUrl = postAuthor.ProfileImage ?? "";
                }

                var postEntity = await _repository.GetById(post.Id);
                if (postEntity != null)
                {
                    var reactions = await _reactionRepository.GetAllList();
                    var postReactions = reactions.Where(r => r.PostId == post.Id).ToList();

                    post.LikesCount = postReactions.Count(r => r.IsLiked);
                    post.DislikesCount = postReactions.Count(r => !r.IsLiked);

                    var currentUserReaction = postReactions.FirstOrDefault(r => r.UserId == currentUserId);
                    post.IsLikedByCurrentUser = currentUserReaction?.IsLiked;
                }

                if (post.Comments != null && post.Comments.Count != 0)
                {
                    foreach (var comment in post.Comments)
                    {
                        var commentAuthor = await _accountService.GetUserById(comment.UserId);
                        if (commentAuthor != null)
                        {
                            comment.UserName = commentAuthor.UserName;
                            comment.UserImageUrl = commentAuthor.ProfileImage ?? "";
                        }
                    }
                }
            }
        }
    }
}
