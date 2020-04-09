namespace FamilyKitchen.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.CloudinaryConf;
    using FamilyKitchen.Web.ViewModels;
    using FamilyKitchen.Web.ViewModels.Categories;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        //private readonly Cloudinary cloudinary;
        private readonly ICategoriesService categoriesService;

        public HomeController(ICategoriesService categoriesService)//Cloudinary cloudinary, 
        {
           // this.cloudinary = cloudinary;
            this.categoriesService = categoriesService;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        //public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        //{
        //    await CloudinaryExtension.UploadAsync(this.cloudinary, files);

        //    return this.Redirect("/");
        //    //var uploadParams = new ImageUploadParams()
        //    //{
        //    //    File = new FileDescription(@"D:\DemoProject\DataImports\module-6.jpg"),
        //    //};

        //    //var uploadResult = await this.cloudinary.UploadAsync(uploadParams);

        //    //return this.Redirect("/");
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
