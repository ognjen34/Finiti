using AutoMapper;
using Finiti.DATA.Model;
using Finiti.DOMAIN.Model;

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
            
        }
    }
    
}
