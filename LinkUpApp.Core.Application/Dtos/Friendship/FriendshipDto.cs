using LinkUpApp.Core.Domain.Common.Enums;

namespace LinkUpApp.Core.Application.Dtos.Friendship
{
    public class FriendshipDto
    {
        public required int Id { get; set; }
        public required string SenderUserId { get; set; }
        public required string ReceiverUserId { get; set; }
        public FriendshipStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        //Data of the other user
        public required string OtherUserId { get; set; }
        public required string OtherUserName { get; set; }
        public required string OtherUserPhotoUrl { get; set; }
        public required int MutualFriendsCount { get; set; }
    }
}
