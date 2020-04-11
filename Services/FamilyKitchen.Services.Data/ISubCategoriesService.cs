namespace FamilyKitchen.Services.Data
{
    using FamilyKitchen.Web.ViewModels.SubCategories;

    public interface ISubCategoriesService
    {
        SubCategoryViewModel GetSubcategoryById(int id);

        int GetCategoryId(int id);
    }
}
