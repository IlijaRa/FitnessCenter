using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FitnessCenterMVC.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
        }

        //[Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        //[Authorize(Roles = "Administrator")]
        public IActionResult AddRole()
        {
            return View(new IdentityRole());
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> AddRole(IdentityRole role)
        {
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
    }
}
