using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Comment;
using LinkUpApp.Core.Application.Interfaces;
using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Core.Domain.Interfaces;

namespace LinkUpApp.Core.Application.Services
{
    public class CommentService : GenericService<Comment, CommentDto, SaveCommentDto>, ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
