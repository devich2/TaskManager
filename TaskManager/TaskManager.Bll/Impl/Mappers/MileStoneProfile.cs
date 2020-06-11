using AutoMapper;
using TaskManager.Entities.Tables;
using TaskManager.Models.MileStone;

namespace TaskManager.Bll.Impl.Mappers
{
    public class MileStoneProfile: Profile
    {
        public MileStoneProfile()
        {
            CreateMap<MileStoneCreateOrUpdateModel, MileStone>()
                .ForMember(x=>x.Id, opt=>
                    opt.MapFrom(src=>src.Id));
        }
    }
}