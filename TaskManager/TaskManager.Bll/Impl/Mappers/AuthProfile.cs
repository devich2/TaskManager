using AutoMapper;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.Auth;

namespace TaskManager.Bll.Impl.Mappers
{
    public class AuthProfile: Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterModel, User>()
                .ForMember(x=>x.Name, opt =>
                    opt.MapFrom(src=>src.Name))
                .ForMember(x=>x.Email, opt=>
                    opt.MapFrom(src=>src.Email))
                .ForMember(x=>x.UserName, opt => 
                    opt.MapFrom(src=>src.UserName))
                .ForAllOtherMembers(opt=>opt.AllowNull());
        }
        
    }
}