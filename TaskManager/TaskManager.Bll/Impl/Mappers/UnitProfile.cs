using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TaskManager.Entities.Tables;
using TaskManager.Models.Unit;

namespace TaskManager.Bll.Impl.Mappers
{
    public class UnitProfile: Profile
    {
        public UnitProfile()
        {
            CreateMap<UnitCreateOrUpdateModel, Unit>()
                .ForMember(x=>x.UnitType, 
                    opt=> 
                        opt.MapFrom(x=>x.UnitStateModel.ExtendedType))
                .ForMember(x=>x.CreatorId, 
                    opt=>
                        opt.MapFrom(x=>x.UserId))
                .ForMember(x=>x.Name,
                    opt=>
                        opt.MapFrom(x=>x.UnitStateModel.UnitModel.Name))
                .ForMember(x=>x.Description,
                        opt=>
                        opt.MapFrom(x=>x.UnitStateModel.UnitModel.Description))
                .ForMember(x=>x.UnitParentId, opt=>
                        opt.MapFrom(src=>src.ParentId));

            CreateMap<Unit, UnitSelectionModel>()
                .ForMember(x => x.UnitId,
                    opt => opt.MapFrom(x => x.UnitId))
                .ForMember(x => x.ExtendedType,
                    opt => opt.MapFrom(x => x.UnitType))
                .ForMember(x => x.UnitModel.Description,
                    opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.UnitModel.Name,
                    opt => opt.MapFrom(x => x.Name))
                .ForMember(x=>x.TermInfo, 
                    opts=>opts.MapFrom(x=>x.TermInfo));
        }
    }
}
