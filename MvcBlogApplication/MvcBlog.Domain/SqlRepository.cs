
namespace MvcBlog.Domain
{
    public partial class SqlRepository : IRepository
    {
        private readonly EFDbContext _context;

        public SqlRepository(EFDbContext context)
        {
            _context = context;
        }
    }
}
