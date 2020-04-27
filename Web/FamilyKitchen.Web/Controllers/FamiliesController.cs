namespace FamilyKitchen.Web.Controllers
{
    using FamilyKitchen.Common;
    using FamilyKitchen.Services.Data;
    using FamilyKitchen.Web.ViewModels.Families;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

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
        public IActionResult Create(FamilyCreateInputModel input)
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

            var action = this.familiesService.CreateFamily(this.User.Identity.Name, input.FamilyNickname, members);

            if (!action.Result)
            {
                return this.BadRequest();
            }

            return this.Redirect("/");
        }

        public IActionResult Add()
        {
            var trai = this.familiesService.AddMember("Az", "Toi");
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(FamilyBaseInputModel input)
        {
            return this.View();
        }

        public IActionResult Remove()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Remove(FamilyBaseInputModel input)
        {
            return this.View();
        }

        public IActionResult Delete()
        {
            var viewModel = new FamilyBaseInputModel
            {
                FamilyHead = "silvia.peicheva@gmail.com",
                FamilyNickname = "Smirfs",
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(FamilyBaseInputModel input)
        {
            return this.Redirect("/");
        }

        [AllowAnonymous]
        public IActionResult GetCart()
        {
            return this.View();
        }

        [AllowAnonymous]
        public IActionResult ReturnCart()
        {
            return this.View();
        }
    }
}
