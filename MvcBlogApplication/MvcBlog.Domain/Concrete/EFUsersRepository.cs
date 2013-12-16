using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcBlog.Domain.Abstract;
using MvcBlog.Domain.Entities;

namespace MvcBlog.Domain.Concrete
{
    public class EFUsersRepository : IUsersRepository
    {
        private readonly EFDbContext _context;

        public EFUsersRepository(EFDbContext context)
        {
            _context = context;
        }

        public IQueryable<UserProfile> UserProfiles
        {
            get
            {
                return _context.UserProfiles;
            }
        }
    }
}
