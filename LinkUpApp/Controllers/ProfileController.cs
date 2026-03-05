using System.Security.Claims;
using AutoMapper;
using LinkUpApp.Core.Application.Dtos.User;
using LinkUpApp.Core.Application.Interfaces;
using LinkUpApp.Core.Application.ViewModels.User;
using LinkUpApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkUpApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ProfileController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        }

        public async Task<IActionResult> Index()
        {

            var userId = GetUserId();
            var updateDto = await _userService.GetUserForEditAsync(userId);

            if (updateDto == null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            var vm = _mapper.Map<UpdateUserViewModel>(updateDto);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var userId = GetUserId();

            var saveDto = _mapper.Map<SaveUserDto>(vm);
            saveDto.Id = userId; 

            if (vm.ProfileImageFile != null)
            {
                saveDto.ProfileImage = FileManager.Upload(vm.ProfileImageFile, userId, "Profiles") ?? "";
            }

            var origin = Request.Headers["origin"];
            EditResponseDto response = await _userService.UpdateProfileAsync(saveDto, origin);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Errors = response.Errors;
                return View(vm); 
            }

            TempData["SuccessMessage"] = "Profile updated successfully.";
            return RedirectToRoute(new { controller = "Profile", action = "Index" });
        }
    }
}
