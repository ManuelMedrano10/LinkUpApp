using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Post;
using LinkUpApp.Core.Application.Interfaces;
using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Core.Domain.Interfaces;
using LinkUpApp.Infraesctructure.Persistence.Repositories;

namespace LinkUpApp.Core.Application.Services
{
    public class PostService : GenericService<Post, PostDto>, IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IAccountServiceWebApp _accountService;
        private readonly IMapper _mapper;

        public PostService(IPostRepository repository, IMapper mapper, IAccountServiceWebApp accountService)
            : base(repository, mapper)
        {
            _repository = repository;
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<List<PostDto>> GetAllByUserIdAsync(string userId)
        {
            var posts = await _repository.GetAllWithCommentsAsync();
            var userPosts = posts.Where(p => p.UserId == userId).ToList();
            var postDtos = _mapper.Map<List<PostDto>>(userPosts);
            await PopulateUserInformation(postDtos);

            return postDtos;
        }

        public async Task<List<PostDto>> GetAllFromFriendsAsync(string userId)
        {
            var posts = await _repository.GetAllWithCommentsAsync();
            var postDtos = _mapper.Map<List<PostDto>>(posts);
            await PopulateUserInformation(postDtos);

            return postDtos;
        }

        private async Task PopulateUserInformation(List<PostDto> postDtos)
        {
            foreach (var post in postDtos)
            {
                var postAuthor = await _accountService.GetUserById(post.UserId);
                if (postAuthor != null)
                {
                    post.UserName = postAuthor.UserName;
                    post.UserPhotoUrl = postAuthor.ProfileImage ?? "";
                }

                if (post.Comments != null && post.Comments.Any())
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
