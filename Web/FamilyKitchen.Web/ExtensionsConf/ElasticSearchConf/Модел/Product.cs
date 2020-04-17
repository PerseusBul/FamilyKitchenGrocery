using FamilyKitchen.Data.Models;
using FamilyKitchen.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyKitchen.Web.ElasticSearchConf.Модел
{
    public class Product : IMapFrom<ShopProduct>
    {
        public string Name { get; set; }

        public string TradeMark { get; set; }

        public string Producer { get; set; }
    }
}
