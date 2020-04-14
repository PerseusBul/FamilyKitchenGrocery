using FamilyKitchen.Services.Data;
using FamilyKitchen.Web.ViewModels.ShopProducts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FamilyKitchen.Web.Controllers
{
    [Authorize]
    public class FavoriteProductsController : BaseController
    {
        private readonly IFavoriteProductService favoriteProductService;

        public FavoriteProductsController(IFavoriteProductService favoriteProductService)
        {
            this.favoriteProductService = favoriteProductService;
        }

        public IActionResult List()
        {
            var viewModel = new ListAllProductsViewModel()
            {
                Products = this.favoriteProductService.ListAll<ShopProductViewModel>(this.User.Identity.Name).ToList(),
            };

            return this.View(viewModel);
        }

        public IActionResult Add(int id)
        {
            var action = this.favoriteProductService.Add(id, this.User.Identity.Name);

            if (!action.Result)
            {
                return this.RedirectToPage("/Shared/Error");
            }

            return this.RedirectToAction(nameof(List));
        }

        public IActionResult Delete(int id)
        {
            var action = this.favoriteProductService.Delete(id, this.User.Identity.Name);

            if (!action.Result)
            {
                return this.RedirectToPage("/Shared/Error");
            }

            return this.RedirectToAction(nameof(List));
        }
    }
}
