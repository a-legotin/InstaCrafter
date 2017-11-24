using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InstaBackup.Models;
using InstaSharper.Classes.Models;
using InstaUser = InstaSharper.Classes.Models.InstaUser;

namespace InstaBackup.Mapper
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
                cfg.CreateMap<InstaMedia, InstaMediaPost>(MemberList.None)
                    .ForMember(m => m.Caption, im => im.MapFrom(media => media.Caption.Text))
                    .ForMember(m => m.ImageUrl, im => im.MapFrom(media => media.Images.FirstOrDefault().URI));
                cfg.CreateMap<InstaSharper.Classes.Models.InstaStory, Models.InstaStory>(MemberList.None);
            });

            return config.CreateMapper();
        }

        public static IMapper Instance => Lazy.Value;
    }
}
