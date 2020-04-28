namespace FamilyKitchen.Web.ExtensionsConf.SignalRConf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.ShopProducts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    [Authorize]
    public class CartHub : Hub
    {
        private readonly IShopProductsService shopProductsService;
        private readonly IFamiliesService familiesService;

        public CartHub(IShopProductsService shopProductsService, IFamiliesService familiesService)
        {
            this.shopProductsService = shopProductsService;
            this.familiesService = familiesService;
        }

        [HttpPost]
        public async Task Add(int id)
        {
            var product = this.shopProductsService.GetProductById<ShopProductViewModel>(id);

            var groupName = this.familiesService.GetFamilyGroupNameSignalR(this.Context.User.Identity.Name);

            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);

            await this.Clients.Groups(groupName).SendAsync(
                "RenderProduct",
                product);
        }
    }
}
