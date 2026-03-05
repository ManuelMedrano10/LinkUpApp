using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LinkUpApp.Core.Application.ViewModels.Post
{
    public class SavePostViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must entener content in the post.")]
        [DataType(DataType.Text)]
        public required string Content { get; set; }
        public string? PostType { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
        public string? ImageUrl { get; set; }

        [Url(ErrorMessage = "The URL link must be valid.")]
        public string? YoutubeUrl { get; set; }
    }
}

