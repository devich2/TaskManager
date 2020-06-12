using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TaskManager.Entities.Tables;
using TaskManager.Models.Project;

namespace TaskManager.Bll.Impl.Mappers
{
    public class ProjectProfile: Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectCreateOrUpdateModel, Project>()
                .ForMember(x => x.Members, opt =>
                    opt.MapFrom(x => x.Members))
                .ForAllOtherMembers(x=>x.AllowNull());
                
            CreateMap<ProjectPreviewModel, ProjectDetailsModel>();
        }
    }
}
