using EBIKES_s.Data.Base;
using EBIKES_s.Models;

namespace EBIKES_s.Data.Services
{
    public class CategoriesService : EntityBaseRepository<Category>, ICategoriesService
    {
        public CategoriesService(AppDbContext context) : base(context)
        {
        }
    }
}
