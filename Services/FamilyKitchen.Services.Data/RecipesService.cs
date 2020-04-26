namespace FamilyKitchen.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FamilyKitchen.Data.Common.Repositories;
    using FamilyKitchen.Data.Models;
    using FamilyKitchen.Data.Models.Enums;
    using FamilyKitchen.Services.Mapping;
    using FamilyKitchen.Web.ViewModels.FoodResources;
    using FamilyKitchen.Web.ViewModels.Recipes;

    public class RecipesService : IRecipesService
    {
        private readonly IDeletableEntityRepository<ShopProduct> productRepository;
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IRepository<FoodResourceRecipe> foodRecipeRepository;

        public RecipesService(IDeletableEntityRepository<ShopProduct> productRepository,
                               IDeletableEntityRepository<Recipe> recipesRepository,
                               IRepository<FoodResourceRecipe> foodRecipeRepository)
        {
            this.productRepository = productRepository;
            this.recipesRepository = recipesRepository;
            this.foodRecipeRepository = foodRecipeRepository;
        }

        public NutritionDeclaration GetNutritionDeclaration(int recipeId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Recipe> GetRecipeByProductId(int productId)
        {
            var product = this.productRepository.All().Where(x => x.Id == productId).AsQueryable();
            var recipe = product.Select(x => x.Recipe).AsQueryable();
            return recipe;
        }

        public IQueryable<Recipe> GetRecipeByRecipeId(int recipeId)
        {
            var recipe = this.recipesRepository.All().Where(x => x.Id == recipeId).AsQueryable();
            return recipe;
        }

        public async Task<TotalRecipeViewModel> CollectTotalRecipeInfo(IQueryable<Recipe> recipe)
        {
            var totalRecipeInfo = recipe.Select(x => new TotalRecipeViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Summary = x.Summary,
                Size = x.Size.ToString("f"),
                PreparationTime = x.PreparationTime,
                Allergens = x.FoodResourcesRecipes.SelectMany(y => y.FoodResource.FoodResourcesAllergens
                                                        .Select(z => z.Allergen.Name))
                                                        .Distinct()
                                                        .ToList(),

                RecipeFoodResources = x.FoodResourcesRecipes.Select(fr => new FoodResourceViewModel
                {
                    Id = fr.FoodResourceId,
                    Name = fr.FoodResource.Name,
                    Price = fr.FoodResource.Price,
                    Quantity = fr.Quantity,
                    PercentagePart = decimal.Parse((fr.Quantity / x.FoodResourcesRecipes.Select(fo => fo.Quantity).Sum() * 100).ToString("F1")),
                })
                .OrderByDescending(v => v.Quantity)
                .ToList(),

                Nutrients = new NutritionDeclaration
                {
                    Energy = decimal.Parse(((decimal)x.FoodResourcesRecipes.Select(h => h.FoodResource.NutritionDeclaration.Energy * h.Quantity / 0.1m).Sum()).ToString("F2")),
                    Fats = decimal.Parse(((decimal)x.FoodResourcesRecipes.Select(m => m.FoodResource.NutritionDeclaration.Fats * m.Quantity / 0.1m).Sum()).ToString("F2")),
                    SaturatedFats = decimal.Parse(((decimal)x.FoodResourcesRecipes.Select(n => n.FoodResource.NutritionDeclaration.SaturatedFats * n.Quantity / 0.1m).Sum()).ToString("F2")),
                    Carbohydrate = decimal.Parse(((decimal)x.FoodResourcesRecipes.Select(l => l.FoodResource.NutritionDeclaration.Carbohydrate * l.Quantity / 0.1m).Sum()).ToString("F2")),
                    Protein = decimal.Parse(((decimal)x.FoodResourcesRecipes.Select(k => k.FoodResource.NutritionDeclaration.Protein * k.Quantity / 0.1m).Sum()).ToString("F2")),
                    Sodium = decimal.Parse(((decimal)x.FoodResourcesRecipes.Select(g => g.FoodResource.NutritionDeclaration.Sodium * g.Quantity / 100m).Sum()).ToString("F2")),
                },
            })
                .FirstOrDefault();

            totalRecipeInfo.Allergens = new HashSet<string>(totalRecipeInfo.Allergens);

            return totalRecipeInfo;
        }

        public async Task<IQueryable<Recipe>> CreatePrivateRecipe(IQueryable<Recipe> baseRecipe, IEnumerable<RecipeResourceInputModel> resourses, string username)
        {
            var recipe = baseRecipe.FirstOrDefault();

            var newRecipe = new Recipe
            {
                Name = recipe.Name,
                Summary = recipe.Summary,
                PreparationTime = recipe.PreparationTime,
                Size = recipe.Size,
                IsPrivate = true,
                Creator = username,
            };

            await this.recipesRepository.AddAsync(newRecipe);
            await this.recipesRepository.SaveChangesAsync();

            foreach (var resourse in resourses)
            {
                var food = baseRecipe
                    .SelectMany(x => x.FoodResourcesRecipes)
                    .Where(y => y.FoodResource.Name == resourse.Name)
                    .FirstOrDefault();

                var oper = new FoodResourceRecipe
                {
                    FoodResourceId = food.FoodResourceId,
                    RecipeId = newRecipe.Id,
                    Quantity = Math.Round(resourse.Value / 100, 2),
                };

                await this.foodRecipeRepository.AddAsync(oper);
            }

            await this.foodRecipeRepository.SaveChangesAsync();

            var queryRecipe = this.recipesRepository.All().Where(x => x.Id == newRecipe.Id).AsQueryable();

            return queryRecipe;
        }
    }
}
