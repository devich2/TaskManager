using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TaskManager.Entities.Tables;
using TaskManager.Models.RelationShip;

namespace TaskManager.Bll.Impl.Mappers
{
    public class RelationShipProfile: Profile
    {
        public RelationShipProfile()
        {
            CreateMap<RelationShipModel, RelationShip>()
                .ForMember(x=>x.ParentUnitId, opt=>
                    opt.MapFrom(x=>x.ParentUnitId))
                .ForAllOtherMembers(opt=>opt.AllowNull());
        }
    }
}
