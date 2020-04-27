namespace FamilyKitchen.Web.Controllers
{
    using FamilyKitchen.Common;
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.Families;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstants.FamilyHeadRoleName)]
    public class FamiliesController : BaseController
    {
        private readonly IFamiliesService familiesService;

        public FamiliesController(IFamiliesService familiesService)
        {
            this.familiesService = familiesService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FamilyCreateInputModel input)
        {
            var members = new List<string>();

            if (input.FamilyMemberSecond != null)
            {
                members.Add(input.FamilyMemberSecond);
            }

            if (input.FamilyMemberThird != null)
            {
                members.Add(input.FamilyMemberThird);
            }

            if (input.FamilyMemberFourth != null)
            {
                members.Add(input.FamilyMemberFourth);
            }

            var action = await this.familiesService.CreateFamily(this.User.Identity.Name, input.FamilyNickname, members);

            if (!action)
            {
                return this.BadRequest();
            }

            return this.Redirect("/");
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(FamilyBaseInputModel input)
        {
            var action = await this.familiesService.AddMember(this.User.Identity.Name, input.FamilyNickname, input.FamilyMember);

            if (!action)
            {
                return this.BadRequest();
            }

            return this.Redirect("/");
        }

        public IActionResult Remove()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Remove(FamilyBaseInputModel input)
        {
            var action = await this.familiesService.RemoveMember(this.User.Identity.Name, input.FamilyNickname, input.FamilyMember);

            if (!action)
            {
                return this.BadRequest();
            }

            return this.Redirect("/");
        }

        public IActionResult Delete()
        {
            var username = this.User.Identity.Name;
            var familyName = this.familiesService.GetFamilyName(username);

            var model = new FamilyBaseInputModel
            {
                FamilyNickname = familyName,
                FamilyMember = string.Empty,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(FamilyBaseInputModel input)
        {
            var action = await this.familiesService.DeleteFamily(this.User.Identity.Name, input.FamilyNickname);

            if (!action)
            {
                return this.BadRequest();
            }

            return this.Redirect("/");
        }

        [AllowAnonymous]
        public IActionResult Get()
        {
            var userMemberName = this.User.Identity.Name;
            var action = this.familiesService.GetFamilyCart(userMemberName);

            if (!action.Result)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("GetCart", "ShoppingCarts");
        }

        [AllowAnonymous]
        public IActionResult ReturnCart()
        {
            var userMemberName = this.User.Identity.Name;
            var action = this.familiesService.ReturnFamilyCart(userMemberName);

            if (!action.Result)
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Leave()
        {
            var username = this.User.Identity.Name;
            var familyName = this.familiesService.GetFamilyName(username);

            var model = new FamilyBaseInputModel
            {
                FamilyNickname = familyName,
                FamilyMember = string.Empty,
            };

            return this.View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Leave(FamilyBaseInputModel input)
        {
            var action = await this.familiesService.LeaveFamily(this.User.Identity.Name, input.FamilyNickname);

            if (!action)
            {
                return this.BadRequest();
            }

            return this.Redirect("/");
        }
    }
}
