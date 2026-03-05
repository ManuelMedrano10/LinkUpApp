using LinkUpApp.Core.Application.ViewModels.Battleship;
using static LinkUpApp.Core.Application.Dtos.Battleship.SetupBoard;

namespace LinkUpApp.Core.Application.Interfaces
{
    public interface IBattleshipService
    {
        Task<int> CreateGameAsync(string senderId, string receiverId);
        Task<GameViewModel?> GetGameDisplayAsync(int gameId, string currentUserId);
        Task<string?> SetupBoardAsync(SetupBoardDto setupDto);
        Task<string?> ShootAsync(int gameId, string shooterId, int row, int col);
        Task SurrenderAsync(int gameId, string userId);
        Task<List<GameHistoryViewModel>> GetGameHistoryAsync(string userId);
    }
}
