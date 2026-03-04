using LinkUpApp.Core.Domain.Common.Enums;

namespace LinkUpApp.Core.Domain.Entities
{
    public class Friendship
    {
        public required int Id { get; set; }
        public required string SenderUserId { get; set; }
        public required string ReceiverUserId { get; set; }
        public FriendshipStatus Status { get; set; } = FriendshipStatus.Pending; //The solicitud by Default is pending
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
