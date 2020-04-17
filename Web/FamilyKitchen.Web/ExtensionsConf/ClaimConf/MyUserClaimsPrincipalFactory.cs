using FamilyKitchen.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FamilyKitchen.Web.ExtensionsConf.ClaimConf
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<FamilyKitchenUser>
    {
        public MyUserClaimsPrincipalFactory(
            UserManager<FamilyKitchenUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
                : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(FamilyKitchenUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            //identity.AddClaim(new Claim("Nickname", user.Nickname ?? "[Click to edit profile]"));
            return identity;
        }
    }
}
