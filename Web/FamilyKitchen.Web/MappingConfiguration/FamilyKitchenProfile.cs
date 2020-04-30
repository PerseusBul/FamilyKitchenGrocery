namespace FamilyKitchen.Web.MappingConfiguration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Web.ExtensionsConf.MongoDbConf;
    using FamilyKitchen.Web.ViewModels.MailSender;
    using FamilyKitchen.Web.ViewModels.Orders;
    using FamilyKitchen.Web.ViewModels.ShopProducts;
    using FamilyKitchen.Web.ViewModels.SubCategories;

    public class FamilyKitchenProfile : Profile
    {
        public FamilyKitchenProfile()
        {
            this.CreateMap<ShopProduct, ShopProductViewModel>()
                .ForMember(x => x.RecipeIsPrivate, y => y.MapFrom(z => z.Recipe.IsPrivate));
            this.CreateMap<SubCategory, SubCategoryViewModel>()
                .ForMember(x => x.Products, y => y
                .MapFrom(src => src.ShopProductsSubCategories.Select(spsc => spsc.ShopProduct).AsQueryable()));
            this.CreateMap<OrderProfileInputModel, OrderProfile>();
            this.CreateMap<ContactInputModel, Contact>();
        }
    }
}
