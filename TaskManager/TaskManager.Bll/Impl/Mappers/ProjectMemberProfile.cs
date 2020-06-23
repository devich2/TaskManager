using AutoMapper;
using TaskManager.Entities.Tables.Identity;
using TaskManager.Models.ProjectMember;

namespace TaskManager.Bll.Impl.Mappers
{
    public class ProjectMemberProfile: Profile
    {
        public ProjectMemberProfile()
        {
            CreateMap<User, UserInfoModel>()
                .ForMember(x=>x.Id, opt=>
                    opt.MapFrom(src=>src.Id))
                .ForMember(x=>x.Name, opt=>
                    opt.MapFrom(src=>src.Name))
                .ForMember(x=>x.UserName, opt=>
                    opt.MapFrom(src=>src.UserName));
            CreateMap<ProjectMemberBaseModel, ProjectMemberDisplayModel>()
                .ForMember(x=>x.Id, opt=>    
                    opt.MapFrom(src=>src.Id))
                .ForMember(x=>x.Name, opt=>
                    opt.MapFrom(src=>src.Name))
                .ForMember(x=>x.UserName, opt=>
                    opt.MapFrom(src=>src.UserName))
                .ForMember(x=>x.Email, opt=>
                    opt.MapFrom(src=>src.Email))
                .ForMember(x=>x.Personal, opt=>
                    opt.MapFrom(src=>src.Personal))
                .ForAllOtherMembers(opt=>opt.AllowNull());
        }
    }
}