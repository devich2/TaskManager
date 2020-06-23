using AutoMapper;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.User;

namespace TaskManager.Bll.Impl.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserBaseModel>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(src => src.Name))
                .ForMember(x => x.Email, opt =>
                    opt.MapFrom(src => src.Email))
                .ForMember(x => x.UserName, opt =>
                    opt.MapFrom(src => src.UserName))
                .ForAllOtherMembers(opt => opt.AllowNull());

            CreateMap<User, UserModel>().IncludeBase<User, UserBaseModel>()
                .ForAllOtherMembers(opt => opt.AllowNull());
        }
    }
}