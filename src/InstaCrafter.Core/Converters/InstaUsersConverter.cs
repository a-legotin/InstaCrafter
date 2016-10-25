using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.Classes.Database;
using InstaCrafter.Classes.Wrapper;
using InstaCrafter.Core.Interfaces;

namespace InstaCrafter.Core.Converters
{
    public class InstaUsersConverter : IObjectConverter<InstaUser, InstaResponseUser>
    {
        public InstaResponseUser SourceObject
        {
            get; set;
        }

        public InstaUser Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException("Source object");
            var user = new InstaUser();
            user.FullName = SourceObject.FullName;
            user.Id = SourceObject.Id;
            user.ProfilePicture = SourceObject.ProfilePicture;
            user.UserName = SourceObject.UserName;
            return user;
        }
    }
}
