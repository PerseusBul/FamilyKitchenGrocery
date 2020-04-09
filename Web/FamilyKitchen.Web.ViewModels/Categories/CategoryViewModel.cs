namespace FamilyKitchen.Web.ViewModels.Categories
{
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
