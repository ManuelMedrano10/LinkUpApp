using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Reply;
using LinkUpApp.Core.Domain.Entities;

namespace LinkUpApp.Core.Application.Mappings.EntitiesAndDtos
{
    public class ReplyMappingProfile : Profile
    {
        public ReplyMappingProfile()
        {
            CreateMap<Reply, ReplyDto>().ReverseMap();

            CreateMap<Reply, SaveReplyDto>().ReverseMap();
        }
    }
}
