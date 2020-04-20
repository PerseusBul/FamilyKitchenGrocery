using FamilyKitchen.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyKitchen.Services.Data
{
    public interface IFamilyKitchenUsersService
    {
        FamilyKitchenUser GetUserByName(string username);

        Task GiveClientCartsAsync();
    }
}
