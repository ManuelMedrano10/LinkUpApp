using LinkUpApp.Core.Application.Dtos.Comment;

namespace LinkUpApp.Core.Application.Interfaces
{
    public interface ICommentService : IGenericService<CommentDto, SaveCommentDto>
    {
    }
}
