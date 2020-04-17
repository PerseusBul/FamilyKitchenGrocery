namespace FamilyKitchen.Web.Controllers
{
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Services.Data;
    using Microsoft.AspNetCore.Mvc;
    using Nest;

    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
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

            if (!response.IsValid)
            {
                return this.Ok(new ShopProduct[] { });
            }

            return this.Ok(response.Documents);
        }

    }
}
