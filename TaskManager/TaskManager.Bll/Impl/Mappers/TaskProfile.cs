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

            CreateMap<TermInfoCreateModel, TermInfo>()
                .ForMember(x => x.DueTs, opt =>
                    opt.MapFrom(x => x.DueTs))
                .ForMember(x => x.Status, opt =>
                    opt.MapFrom(x => x.Status))
                .ForAllOtherMembers(opt=>opt.AllowNull());
        }
    }
}
