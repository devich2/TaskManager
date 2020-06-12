using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using TaskManager.Entities.Tables;
using TaskManager.Models.Unit;

namespace TaskManager.Bll.Impl.Mappers
{
    public class UnitProfile: Profile
    {
        public UnitProfile()
        {
            CreateMap<Unit, UnitSelectionModel>()
                .ForMember(x=>x.UnitId, opt=>
                    opt.MapFrom(src => src.UnitId))
                .ForMember(x=>x.ExtendedType, opt=>
                    opt.MapFrom(src=>src.Name))
                .ForMember(x=>x.Description, opt=>
                    opt.MapFrom(src=>src.Description))
                .ForMember(x=>x.ExtendedType, opt=>
                    opt.MapFrom(src=>src.UnitType))
                .ForMember(x=>x.TermInfo, opt=>
                    opt.MapFrom(src=>src.TermInfo))
                .ForMember(x=>x.Creator, opt =>
                    opt.MapFrom(src=>src.Creator))
                .ForAllOtherMembers(opt=>opt.AllowNull());
            
            CreateMap<UnitBlModel, Unit>()
                .ForMember(x=>x.UnitId, opt=> 
                    opt.MapFrom(src=>src.UnitId))
                .ForMember(x=>x.UnitParentId, opt=>
                    opt.MapFrom(src=>src.ParentId))
                .ForMember(x=>x.UnitType, opt =>
                    opt.MapFrom(src=>src.ExtendedType))
                .ForMember(x=> x.Name, opt=>
                    opt.MapFrom(src=>src.Name))
                .ForMember(x=>x.Description, opt=>
                    opt.MapFrom(src=>src.Description))
                .ForAllOtherMembers(opt=>opt.Ignore());
            
            CreateMap<UnitModel, UnitBlModel>();
            
            CreateMap<Unit, SubUnitModel>()
                .ForMember(x=>x.UnitId, opt => 
                    opt.MapFrom(src=>src.UnitId))
                .ForMember(x=>x.Name, opt =>
                    opt.MapFrom(src=>src.Name))
                .ForMember(x=>x.UnitType, opt=>
                    opt.MapFrom(src=>src.UnitType))
                .ForMember(x=>x.ParentId, opt=>
                    opt.MapFrom(src=>src.UnitParentId))
                .ForMember(x=>x.CreatedTs, opt=>
                    opt.MapFrom(src=>src.TermInfo.StartTs))
                .ForMember(x=>x.Status, opt => 
                    opt.MapFrom(src=>src.TermInfo.Status))
                .ForAllOtherMembers(opt=>opt.AllowNull());
                
        }
    }
}
