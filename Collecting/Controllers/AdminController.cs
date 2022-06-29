using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Collecting.Models;
using Microsoft.AspNetCore.Authorization;
using Collecting.ViewModels;

namespace Collecting.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ActionResult Index() => View(_userManager.Users.ToList());

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Block(string id)
        {
            // получаем пользователя
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                string[] act = { "active" };
                if (userRoles.Contains("active"))
                {
                    
                    await _userManager.RemoveFromRolesAsync(user, act);
                }
                else
                {
                    await _userManager.AddToRolesAsync(user, act);
                }
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> MakeAdmin(string id)
        {
            // получаем пользователя
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                string[] act = { "admin", "active" };
                if (userRoles.Contains("admin"))
                {
                    await _userManager.RemoveFromRolesAsync(user, act);
                }
                else
                {
                    await _userManager.AddToRolesAsync(user, act);
                }
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
