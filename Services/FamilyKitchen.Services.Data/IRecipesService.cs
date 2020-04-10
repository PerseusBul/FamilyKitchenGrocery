using FamilyKitchen.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyKitchen.Services.Data
{
    public interface IRecipesService
    {
        NutritionDeclaration GetNutritionDeclaration(int recipeId);
    }
}
