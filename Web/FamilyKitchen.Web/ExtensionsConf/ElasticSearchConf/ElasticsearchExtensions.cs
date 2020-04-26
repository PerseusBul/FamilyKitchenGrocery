﻿namespace FamilyKitchen.Web.ElasticSearchConf
{
    using System;

    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Web.ElasticSearchConf.Model;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Nest;

    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["Еlasticsearch:url"];
            var defaultIndex = configuration["Еlasticsearch:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex);

            AddDefaultMappings(settings);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings
                .DefaultMappingFor<Product>(m => m);

            // settings
            //    .DefaultMappingFor<ShopProduct>(m => m
            //    .Ignore(p => p.Price)
            //    .Ignore(p => p.Availability)
            //    .Ignore(p => p.Available)
            //    .Ignore(p => p.CreatedOn)
            //    .Ignore(p => p.DeletedOn)
            //    .Ignore(p => p.Discount)
            //    .Ignore(p => p.ExpireDate)
            //    .Ignore(p => p.ImageUrl)
            //    .Ignore(p => p.IsDeleted)
            //    .Ignore(p => p.IsExpired)
            //    .Ignore(p => p.MetricSystemUnit)
            //    .Ignore(p => p.ModifiedOn)
            //    .Ignore(p => p.NutritionDeclaration)
            //    .Ignore(p => p.NutritionDeclarationId)
            //    .Ignore(p => p.Recipe)
            //    .Ignore(p => p.RecipeId)
            //    .Ignore(p => p.ShoppingCartsShopProducts)
            //    .Ignore(p => p.ShopProductsSubCategories)
            // );
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(
                indexName,
                index => index.Map<Product>(x => x.AutoMap()));
        }
    }
}
