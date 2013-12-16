using System.Linq;
using MvcBlog.Domain.Entities;

namespace MvcBlog.Domain
{
    public partial class SqlRepository
    {
        public IQueryable<webpages_Membership> WebpagesMemberships
        {
            get { return _context.webpages_Memberships; }
        }
    }
}
