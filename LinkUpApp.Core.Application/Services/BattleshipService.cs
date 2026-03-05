using LinkUpApp.Core.Application.Dtos.Battleship;
using LinkUpApp.Core.Application.Interfaces;
using LinkUpApp.Core.Application.ViewModels.Battleship;
using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Core.Domain.Interfaces;

namespace LinkUpApp.Core.Application.Services
{
    public class BattleshipService : IBattleshipService
    {
        private readonly IGameRepository _repository;
        private readonly IAccountServiceWebApp _accountService;

        public BattleshipService(IGameRepository repository, IAccountServiceWebApp accountService)
        {
            _repository = repository;
            _accountService = accountService;
        }

        public async Task<int> CreateGameAsync(string senderId, string receiverId)
        {
            var allGames = await _repository.GetAllList();

            var activeGame = allGames.FirstOrDefault(g =>
                (g.Status == 1 || g.Status == 2) &&
                ((g.Player1Id == senderId && g.Player2Id == receiverId) ||
                (g.Player1Id == receiverId && g.Player2Id == senderId)));

            if (activeGame != null)
            {
                return activeGame.Id;
            }

            var game = new Game
            {
                Player1Id = senderId,
                Player2Id = receiverId,
                Status = 1
            };
            await _repository.AddAsync(game);
            return game.Id;
        }

        public async Task<GameViewModel?> GetGameDisplayAsync(int gameId, string currentUserId)
        {
            var game = await _repository.GetGameWithDetailsAsync(gameId);
            if (game == null)
            {
                return null;
            }

            string opponentId = currentUserId == game.Player1Id ? game.Player2Id : game.Player1Id;
            var opponent = await _accountService.GetUserById(opponentId);

            var vm = new GameViewModel
            {
                GameId = game.Id,
                CurrentUserId = currentUserId,
                OpponentId = opponentId,
                OpponentName = opponent?.UserName ?? "Opponent",
                Status = game.Status,
                IsMyTurn = game.CurrentTurnPlayerId == currentUserId,
                WinnerId = game.WinnerId,
                AmIReady = currentUserId == game.Player1Id ? game.Player1Ready : game.Player2Ready
            };

            foreach (var ship in game.Ships.Where(s => s.PlayerId == currentUserId))
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    int r = ship.IsHorizontal ? ship.StartRow : ship.StartRow + i;
                    int c = ship.IsHorizontal ? ship.StartCol + i : ship.StartCol;
                    if (r < 12 && c < 12) vm.MyBoard.Grid[r, c].HasShip = true;
                }
            }

            foreach (var shot in game.Shots.Where(s => s.PlayerId == opponentId))
            {
                vm.MyBoard.Grid[shot.Row, shot.Col].IsHit = true;
            }

            foreach (var shot in game.Shots.Where(s => s.PlayerId == currentUserId))
            {
                vm.OpponentBoard.Grid[shot.Row, shot.Col].IsHit = true;
                if (shot.IsHit) vm.OpponentBoard.Grid[shot.Row, shot.Col].HasShip = true;
            }

            return vm;
        }

        public async Task<string?> SetupBoardAsync(SetupBoard.SetupBoardDto setupDto)
        {
            var game = await _repository.GetGameWithDetailsAsync(setupDto.GameId);
            if (game == null)
            {
                return "Game not founded.";
            }
                
            if (game.Status != 1)
            {
                return "The game is not in the Preparation Stage anymore.";
            } 

            var sizes = setupDto.Ships.Select(s => s.Size).OrderByDescending(x => x).ToList();
            var requiredSizes = new[] { 5, 4, 3, 3, 2 };
            if (!sizes.SequenceEqual(requiredSizes))
            {
                return "You must enter the 5 ships required.";
            }

            foreach (var shipDto in setupDto.Ships)
            {
                game.Ships.Add(new ShipPosition
                {
                    Id = 0,
                    PlayerId = setupDto.PlayerId,
                    Size = shipDto.Size,
                    IsHorizontal = shipDto.IsHorizontal,
                    StartRow = shipDto.StartRow,
                    StartCol = shipDto.StartCol
                });
            }

            if (setupDto.PlayerId == game.Player1Id)
            {
                game.Player1Ready = true;
            } else if (setupDto.PlayerId == game.Player2Id)
            {
                game.Player2Ready = true;
            }

            if (game.Player1Ready && game.Player2Ready)
            {
                game.Status = 2;
                game.CurrentTurnPlayerId = game.Player1Id;
            }

            await _repository.UpdateAsync(game.Id, game);
            return null;
        }

        public async Task<string?> ShootAsync(int gameId, string shooterId, int row, int col)
        {
            var game = await _repository.GetGameWithDetailsAsync(gameId);
            if (game == null)
            {
                return "Game not founded.";
            }

            if (game.Status != 2)
            {
                return "The game is not active.";
            }

            if (game.CurrentTurnPlayerId != shooterId)
            { 
                return "It is not your turn."; 
            }

            if (game.Shots.Any(s => s.PlayerId == shooterId && s.Row == row && s.Col == col))
            {
                return "You akready shoot to this place.";
            }

            string opponentId = shooterId == game.Player1Id ? game.Player2Id : game.Player1Id;
            bool isHit = false;

            var opponentShips = game.Ships.Where(s => s.PlayerId == opponentId).ToList();
            foreach (var ship in opponentShips)
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    int r = ship.IsHorizontal ? ship.StartRow : ship.StartRow + i;
                    int c = ship.IsHorizontal ? ship.StartCol + i : ship.StartCol;

                    if (r == row && c == col)
                    {
                        isHit = true;
                        ship.Hits++;
                        break;
                    }
                }
                if (isHit) break;
            }

            game.Shots.Add(new Shot { 
                Id = 0,
                PlayerId = shooterId, 
                Row = row, 
                Col = col, 
                IsHit = isHit });
            game.CurrentTurnPlayerId = opponentId;

            if (opponentShips.All(s => s.IsSunk))
            {
                game.Status = 3;
                game.WinnerId = shooterId;
            }

            await _repository.UpdateAsync(game.Id, game);
            return null;
        }

        public async Task SurrenderAsync(int gameId, string userId)
        {
            var game = await _repository.GetById(gameId);

            if (game != null && game.Status != 3)
            {
                game.Status = 3; 
                game.WinnerId = game.Player1Id == userId ? game.Player2Id : game.Player1Id;

                await _repository.UpdateAsync(game.Id, game);
            }
        }

        public async Task<List<GameHistoryViewModel>> GetGameHistoryAsync(string userId)
        {
            var allGames = await _repository.GetAllList();

            var userGames = allGames
                .Where(g => g.Player1Id == userId || g.Player2Id == userId)
                .OrderByDescending(g => g.CreatedAt)
                .ToList();

            var history = new System.Collections.Generic.List<GameHistoryViewModel>();

            foreach (var g in userGames)
            {
                string opponentId = g.Player1Id == userId ? g.Player2Id : g.Player1Id;
                var opponent = await _accountService.GetUserById(opponentId);

                history.Add(new GameHistoryViewModel
                {
                    GameId = g.Id,
                    OpponentName = opponent?.UserName ?? "Usuario Desconocido",
                    Status = g.Status,
                    IsWinner = g.Status == 3 && g.WinnerId == userId,
                    CreatedAt = g.CreatedAt
                });
            }

            return history;
        }
    }
}
