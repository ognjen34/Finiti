using AutoMapper;
using Finiti.DATA.Model;
using Finiti.DATA.Repositories;
using Finiti.DOMAIN.Model;
using Finiti.WEB.DTO.Requests;
using Finiti.WEB.DTO.Responses;

namespace Finiti.WEB
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AuthorEntity, Author>().ReverseMap();
            CreateMap<Role, RoleEntity>().ReverseMap();
            CreateMap<Author, AuthorEntity>().ReverseMap();
            CreateMap<RoleEntity, Role>().ReverseMap();
            CreateMap<ForbiddenWord, ForbiddenWordEntity>().ReverseMap();
            CreateMap<GlossaryTerm,GlossaryTermEntity>().ReverseMap();
            CreateMap<Author, AuthorResponse>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<CreateAuthorRequest, Author>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => new Role { Id = 0 }));
            CreateMap<CreateTermRequest, GlossaryTerm>();
            CreateMap<GlossaryTerm, TermResponse>().ReverseMap();
            CreateMap<ForbiddenWordRequest, ForbiddenWord>();

        }
    }
    
}
