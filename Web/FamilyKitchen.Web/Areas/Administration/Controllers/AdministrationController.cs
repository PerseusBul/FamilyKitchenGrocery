namespace FamilyKitchen.Web.Areas.Administration.Controllers
{
    using FamilyKitchen.Common;
    using FamilyKitchen.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
