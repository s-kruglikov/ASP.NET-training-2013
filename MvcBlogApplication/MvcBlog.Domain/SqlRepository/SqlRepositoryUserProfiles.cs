using System.Linq;
using MvcBlog.Domain.Entities;

namespace MvcBlog.Domain
{
    public partial class SqlRepository
    {
        public IQueryable<UserProfile> UserProfiles
        {
            get
            {
                return _context.UserProfiles;
            }
        }

        public void SaveUser(UserProfile userProfile)
        {
            //if we are adding new user
            if (userProfile.UserId == 0)
            {
                _context.UserProfiles.Add(userProfile);
            }

            else
            {
                //if we are updating existing user
                UserProfile dbEntry = _context.UserProfiles.Find(userProfile.UserId);
                if (dbEntry != null)
                {
                    dbEntry.FirstName = userProfile.FirstName;
                    dbEntry.LastName = userProfile.LastName;
                    dbEntry.Email = userProfile.Email;
                    dbEntry.BirthDate = userProfile.BirthDate;
                    dbEntry.Avatar = userProfile.Avatar;
                    dbEntry.AvatarMimeType = userProfile.AvatarMimeType;
                }
            }

            _context.SaveChanges();
        }
    }
}
