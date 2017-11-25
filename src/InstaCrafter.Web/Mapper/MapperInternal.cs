using System;
using System.Linq;
using AutoMapper;
using InstaCrafter.Web.Models;
using InstaSharper.Classes.Models;

namespace InstaCrafter.Web.Mapper
{
    public static class MapperInternal
    {
        private static readonly Lazy<IMapper> Lazy =
            new Lazy<IMapper>(CreateMapper);

        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InstaCurrentUser, Models.InstaUser>(MemberList.None);
                cfg.CreateMap<InstaUserShort, Models.InstaUser>(MemberList.None);
                cfg.CreateMap<Models.InstaUser, InstaUserShort>(MemberList.None);
                cfg.CreateMap<InstaMedia, InstaMediaPost>(MemberList.None);
                cfg.CreateMap<InstaSharper.Classes.Models.InstaStory, Models.InstaStory>(MemberList.None);
                cfg.CreateMap<InstaSharper.Classes.Models.InstaCaption, Models.InstaCaption>(MemberList.None);
                cfg.CreateMap<InstaImage, InstaMediaInfo>(MemberList.None)
                    .ForMember(m => m.Url, im => im.MapFrom(media => media.URI));
                cfg.CreateMap<InstaSharper.Classes.Models.InstaCarouselItem, Models.InstaCarouselItem>(MemberList.None)
                    .ForMember(m => m.Medias, im => im.MapFrom(media => media.Images));
            });

            return config.CreateMapper();
        }

        public static IMapper Instance => Lazy.Value;
    }
}
