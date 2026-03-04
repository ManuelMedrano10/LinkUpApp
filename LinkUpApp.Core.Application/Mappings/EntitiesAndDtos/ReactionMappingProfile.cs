using AutoMapper;
using LinkUpApp.Core.Application.Dtos.Reaction;
using LinkUpApp.Core.Domain.Entities;

namespace LinkUpApp.Core.Application.Mappings.EntitiesAndDtos
{
    public class ReactionDtoMappingProfile : Profile
    {
        public ReactionDtoMappingProfile()
        {
            CreateMap<Reaction, ReactionDto>().ReverseMap();

            CreateMap<Reaction, SaveReactionDto>().ReverseMap();
        }
    }
}
