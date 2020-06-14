using AutoMapper;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.Role;

namespace TaskManager.Bll.Impl.Mappers
{
    public class RoleProfile: Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleModel>()
                .ForMember(x=>x.RoleId, opt=>
                    opt.MapFrom(src=>src.Id))
                .ForMember(x=>x.RoleName, opt=>   
                    opt.MapFrom(src=>src.Name));
        }
    }
}