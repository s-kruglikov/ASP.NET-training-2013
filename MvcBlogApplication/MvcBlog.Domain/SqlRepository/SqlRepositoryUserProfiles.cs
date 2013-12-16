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
    }
}
