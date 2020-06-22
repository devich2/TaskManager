using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TaskManager.Email.Template.Engine.Views.Email.TaskExpiration;
using TaskManager.Entities.Tables;
using TaskManager.Models.Task;
using TaskManager.Models.TermInfo;

namespace TaskManager.Bll.Impl.Mappers
{
    public class TaskProfile: Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskCreateOrUpdateModel, Task>()
                .ForMember(x=>x.Assigned, opt=>
                    opt.MapFrom(src=>src.AssignedId))
                .ForMember(x=>x.MileStoneId, opt=>
                    opt.MapFrom(src=>src.MileStoneId));
          
            CreateMap<TaskModel, TaskExpirationSelectionModel>()
                .ForMember(x=>x.Creator, opt=>
                    opt.MapFrom(src=>src.Creator))
                .ForMember(x=>x.DueTs, opt=>
                    opt.MapFrom(src=>src.DueTs))
                .ForMember(x=>x.TaskId, opt=>
                    opt.MapFrom(src=>src.TaskId))
                .ForMember(x=>x.TaskName, opt=>
                    opt.MapFrom(src=>src.TaskName))
                .ForMember(x=>x.Expired, opt=>
                    opt.Ignore());
            
            CreateMap<TaskExpirationItemModel, TaskExpirationEmailItemModel>()
                .ForMember(x=>x.ProjectId, opt=>
                    opt.MapFrom(src=>src.ProjectId))
                .ForMember(x=>x.ProjectName, opt=>
                    opt.MapFrom(src=>src.ProjectName))
                .ForMember(x=>x.Tasks, opt=>
                    opt.MapFrom(src=>src.Tasks));
            CreateMap<TaskExpirationModel, TaskExpirationEmailModel>()
                .ForMember(x=>x.Assignee, opt=>
                    opt.MapFrom(src=>src.Assignee))
                .ForMember(x=>x.ProjectTasks, opt=>
                    opt.MapFrom(src=>src.ProjectTasks));

            CreateMap<TaskPreviewModel, TaskDetailsModel>();
        }
    }
}
