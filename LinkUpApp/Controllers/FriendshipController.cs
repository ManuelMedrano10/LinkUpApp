using System.Security.Claims;
using AutoMapper;
using LinkUpApp.Core.Application.Interfaces;
using LinkUpApp.Core.Application.ViewModels.Friendship;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkUpApp.Controllers
{
    [Authorize]
    public class FriendshipController : Controller
    {
        private readonly IFriendshipService _friendshipService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public FriendshipController(IFriendshipService friendshipService, IMapper mapper, IUserService userService)
        {
            _friendshipService = friendshipService;
            _mapper = mapper;
            _userService = userService;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _friendshipService.GetFriendsAsync(GetUserId());
            var vms = _mapper.Map<List<FriendshipViewModel>>(dtos);
            return View(vms);
        }

        public async Task<IActionResult> Requests()
        {
            var dtos = await _friendshipService.GetPendingRequestsAsync(GetUserId());
            var vms = _mapper.Map<List<FriendshipViewModel>>(dtos);
            return View(vms);
        }

        [HttpPost]
        public async Task<IActionResult> SendRequest(string receiverUsername)
        {
            /*string receiverId = await _userService.GetUserIdByUsernameAsync(receiverUsername);

            if (string.IsNullOrEmpty(receiverId))
            {
                TempData["ErrorFriendship"] = $"The user '{receiverUsername}' does not exist.";
                return RedirectToRoute(new { action = "Index", controller = "Friendship" });
            }*/

            var errorMessage = await _friendshipService.AddFriendRequestAsync(GetUserId(), receiverUsername);

            if (errorMessage != null)
            {
                TempData["ErrorFriendship"] = errorMessage;
            }
            else
            {
                TempData["SuccessFriendship"] = "Friendship request sended successfully.";
            }

            return RedirectToRoute(new { action = "Index", controller = "Friendship" });
        }

        [HttpPost]
        public async Task<IActionResult> Accept(int id)
        {
            await _friendshipService.AcceptRequestAsync(id);
            return RedirectToRoute(new { action = "Requests", controller ="Friendship" } );
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            await _friendshipService.RejectRequestAsync(id);
            return RedirectToRoute(new { action = "Requests", controller = "Friendship" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _friendshipService.DeleteFriendAsync(id);
            return RedirectToRoute(new { action = "Index", controller = "Friendship" });
        }
    }
}
