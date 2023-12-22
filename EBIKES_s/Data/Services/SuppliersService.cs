using EBIKES_s.Data.Base;
using EBIKES_s.Models;

namespace EBIKES_s.Data.Services
{
    public class SuppliersService : EntityBaseRepository<Supplier>, ISuppliersService
    {
        public SuppliersService(AppDbContext context) : base(context)
        {
        }
    }
}
