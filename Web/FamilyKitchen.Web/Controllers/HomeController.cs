namespace FamilyKitchen.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.CloudinaryConf;
    using FamilyKitchen.Web.ViewModels;
    using FamilyKitchen.Web.ViewModels.Categories;
    using FamilyKitchen.Web.ViewModels.ShopProducts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly Cloudinary cloudinary;
        private readonly ICategoriesService categoriesService;
        private readonly IShopProductsService shopProductsService;

        public HomeController(ICategoriesService categoriesService, IShopProductsService shopProductsService, Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
            this.categoriesService = categoriesService;
            this.shopProductsService = shopProductsService;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            var viewModel = new ListAllProductsViewModel()
            {
                Products = this.shopProductsService.GetAllProducts<ShopProductViewModel>()
                                                   .Where(x => x.Discount > 0)
                                                   .ToList(),
            };
            return this.View(viewModel);
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            await CloudinaryExtension.UploadAsync(this.cloudinary, files);

            return this.Redirect("/");

            // var uploadParams = new ImageUploadParams()
            // {
            //    File = new FileDescription(@"D:\DemoProject\DataImports\product_1.jpg"),
            // };

            // var uploadResult = await this.cloudinary.UploadAsync(uploadParams);

            // return this.Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
