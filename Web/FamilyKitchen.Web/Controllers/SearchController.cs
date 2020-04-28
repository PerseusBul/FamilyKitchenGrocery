namespace FamilyKitchen.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.ShopProducts;
    using Microsoft.AspNetCore.Mvc;
    using Nest;

    //[Route("api/[controller]")]
    //[ApiController]
    public class SearchController : BaseController
    {
        private readonly IShopProductsService productService;
        private readonly IElasticClient client;

        public SearchController(IShopProductsService productService, IElasticClient elasticClient)
        {
            this.productService = productService;
            this.client = elasticClient;
        }

        [HttpGet("Find")]
        public async Task<IActionResult> Find(string query, int page = 1, int pageSize = 5)
        {
            var response = await this.client.SearchAsync<ShopProduct>(
                 s => s.Query(q => q.QueryString(d => d.Query('*' + query + '*')))
                     .From((page - 1) * pageSize)
                     .Size(pageSize));

            var products = new List<ShopProductViewModel>();
            this.ViewBag.Query = query;
            var viewModel = new ListAllProductsViewModel
            {
                Products = new List<ShopProductViewModel>(),
            };

            if (!response.IsValid)
            {
                return this.View(viewModel);
            }

            foreach (var item in response.Hits)
            {
                var name = item.Source.Name;

                var model = this.productService.GetProductByName<ShopProductViewModel>(name);
                products.Add(model);
            }

            viewModel.Products = products;

            return this.View(viewModel);
        }
    }
}
