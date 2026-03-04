using LinkUpApp.Core.Domain.Common.Enums;

namespace LinkUpApp.Core.Application.Dtos.Friendship
{
    public class SaveFriendshipDto
    {
        public required int Id { get; set; }
        public required string SenderUserId { get; set; }
        public required string ReceiverUserId { get; set; }
        public FriendshipStatus Status { get; set; }
    }
}
