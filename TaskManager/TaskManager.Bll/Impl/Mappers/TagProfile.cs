using AutoMapper;
using TaskManager.Entities.Tables;
using TaskManager.Models.Tag;

namespace TaskManager.Bll.Impl.Mappers
{
    public class TagProfile: Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagModel>()
                .ForMember(x=>x.Id, opt => 
                    opt.MapFrom(src=>src.Id))
                .ForMember(x=>x.Name, opt=>
                    opt.MapFrom(src=>src.TextValue));
        }
    }
}