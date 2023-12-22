using EBIKES_s.Models;

namespace EBIKES_s.Data.ViewModels
{
    public class NewProductDropdownsVM
    {
        public NewProductDropdownsVM()
        {
            Categories = new List<Category>();
            Suppliers = new List<Supplier>();
            
        }

        public List<Category> Categories { get; set; }
        public List<Supplier> Suppliers { get; set; }
    }
}
