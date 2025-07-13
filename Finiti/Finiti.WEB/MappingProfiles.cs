using AutoMapper;
using Finiti.DATA.Model;
using Finiti.DATA.Repositories;
using Finiti.DOMAIN.Model;
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
            CreateMap<Author, AuthorResponse>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));

        }
    }
    
}
