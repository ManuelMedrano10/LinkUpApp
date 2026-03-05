using System.Security.Claims;
using LinkUpApp.Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static LinkUpApp.Core.Application.Dtos.Battleship.SetupBoard;

namespace LinkUpApp.Controllers
{
    [Authorize]
    public class BattleshipController : Controller
    {
        private readonly IBattleshipService _battleshipService;

        public BattleshipController(IBattleshipService battleshipService)
        {
            _battleshipService = battleshipService;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var history = await _battleshipService.GetGameHistoryAsync(userId);
            return View(history);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame(string friendId)
        {
            var userId = GetUserId();
            if (userId == friendId)
            {
                return BadRequest("You cannot play with yourself.");
            }

            int gameId = await _battleshipService.CreateGameAsync(userId, friendId);
            return RedirectToRoute(new { action = "Play", controller = "Battleship", id = gameId });
        }

        public async Task<IActionResult> Play(int id)
        {
            var userId = GetUserId();
            var gameVm = await _battleshipService.GetGameDisplayAsync(id, userId);

            if (gameVm == null)
            {
                return NotFound("Game not founded.");
            }
                
            if (gameVm.Status == 1 && !gameVm.AmIReady)
            {
                return View("Setup", gameVm);
            }

            return View("Game", gameVm);
        }

        [HttpPost]
        public async Task<IActionResult> SetupBoard([FromBody] SetupBoardDto dto)
        {
            dto.PlayerId = GetUserId();
            var error = await _battleshipService.SetupBoardAsync(dto);

            if (error != null)
                return Json(new { success = false, message = error });

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Shoot(int gameId, int row, int col)
        {
            var userId = GetUserId();
            var error = await _battleshipService.ShootAsync(gameId, userId, row, col);

            if (error != null)
                return Json(new { success = false, message = error });

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> Surrender(int id)
        {
            await _battleshipService.SurrenderAsync(id, GetUserId());

            return RedirectToAction("Play", new { id = id });
        }
    }
}
