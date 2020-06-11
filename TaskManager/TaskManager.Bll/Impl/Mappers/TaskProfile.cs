using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
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
          
        }
    }
}
