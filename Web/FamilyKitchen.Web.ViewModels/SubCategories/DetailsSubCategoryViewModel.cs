namespace FamilyKitchen.Web.ViewModels.SubCategories
{
    using System.Collections.Generic;

    public class DetailsSubCategoryViewModel
    {
        public SubCategoryViewModel SubCategory { get; set; }

        public IEnumerable<SubCategoryViewModel> SubCategories { get; set; }

        public bool Toggler { get; set; }
    }
}
