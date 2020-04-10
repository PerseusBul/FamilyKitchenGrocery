using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyKitchen.Services.Data
{
    public interface IFoodResourcesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);
    }
}
