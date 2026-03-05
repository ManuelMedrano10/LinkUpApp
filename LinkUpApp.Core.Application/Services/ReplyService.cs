using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Reply;
using LinkUpApp.Core.Application.Interfaces;
using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Core.Domain.Interfaces;
using LinkUpApp.Infraesctructure.Persistence.Repositories;

namespace LinkUpApp.Core.Application.Services
{
    public class ReplyService : GenericService<Reply, ReplyDto, SaveReplyDto>, IReplyService
    {
        private readonly IReplyRepository _repository;
        private readonly IMapper _mapper;
        public ReplyService(IReplyRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
