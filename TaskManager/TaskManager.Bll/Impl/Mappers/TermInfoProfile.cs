using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AutoMapper;
using TaskManager.Entities.Tables;
using TaskManager.Models.TermInfo;

namespace TaskManager.Bll.Impl.Mappers
{
    public class TermInfoProfile: Profile
    {
        public TermInfoProfile()
        {
            CreateMap<TermInfoCreateModel, TermInfo>()
                .ForMember(x=> x.DueTs, opt=>
                    opt.MapFrom(src=>src.DueTs))
                .ForMember(x=>x.Status, opt=>
                    opt.MapFrom(src=>src.Status));
                    
            CreateMap<TermInfo, TermInfoSelectionModel>()
                .ForMember(x => x.Status,
                    opt => opt.MapFrom(x => x.Status))
                .ForMember(x => x.CreatedTs,
                    opt => opt.MapFrom(x => x.StartTs))
                .ForMember(x => x.DueTs,
                    opt => opt.MapFrom(x => x.DueTs))
                .ForMember(x => x.Expired,
                    opt => opt.MapFrom(x => x.DueTs < DateTimeOffset.Now));
        }
    }
}
