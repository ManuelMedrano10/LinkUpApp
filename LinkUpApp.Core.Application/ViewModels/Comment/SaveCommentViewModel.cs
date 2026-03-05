using System.ComponentModel.DataAnnotations;

namespace LinkUpApp.Core.Application.ViewModels.Comment
{
    public class SaveCommentViewModel
    {
        public required int Id { get; set; }
        [Required(ErrorMessage = "You must enter content in the comment.")]
        [DataType(DataType.Text)]
        public required string Content { get; set; }
        public required int PostId { get; set; }
        public string? UserId { get; set; }
    }
}
