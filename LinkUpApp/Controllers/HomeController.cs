using System.Security.Claims;
using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Comment;
using LinkUpApp.Core.Application.Dtos.Post;
using LinkUpApp.Core.Application.Dtos.Reply;
using LinkUpApp.Core.Application.Interfaces;
using LinkUpApp.Core.Application.ViewModels.Comment;
using LinkUpApp.Core.Application.ViewModels.Post;
using LinkUpApp.Core.Application.ViewModels.Reply;
using LinkUpApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkUpApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;
        private readonly IReplyService _replyService;

        public HomeController(IPostService postService, ICommentService commentService, 
            IMapper mapper, IReplyService replyService)
        {
            _mapper = mapper;
            _commentService = commentService;
            _postService = postService;
            _replyService = replyService;
        }
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        }

        public async Task<IActionResult> Index()
        {
            var postDtos = await _postService.GetAllFromFriendsAsync(GetUserId());

            var postViewModels = _mapper.Map<List<PostViewModel>>(postDtos);

            return View(postViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(SavePostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToRoute( new { controller = "Home", action = "Index" });
            }

            var savePostDto = _mapper.Map<SavePostDto>(vm);
            savePostDto.UserId = GetUserId();

            if (vm.File != null)
            {
                savePostDto.ImageUrl = FileManager.Upload(vm.File, savePostDto.UserId, "Posts");
            }

            await _postService.AddAsync(savePostDto);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(SaveCommentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<SaveCommentDto>(vm);
                dto.UserId = GetUserId();
                await _commentService.AddAsync(dto);
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(SaveReplyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<SaveReplyDto>(vm);
                dto.UserId = GetUserId();
                await _replyService.AddAsync(dto);
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [HttpPost]
        public async Task<IActionResult> ToggleReaction(int postId, bool isLike)
        {
            await _postService.ToggleReactionAsync(postId, GetUserId(), isLike);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
