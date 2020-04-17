namespace FamilyKitchen.Web.ViewModels.SubCategories
{
    using System.Collections.Generic;

    public class ListAllSubCategoriesViewModel
    {
        public IEnumerable<SubCategoryViewModel> SubCategories { get; set; }

        public bool Toggler { get; set; }
    }
}
