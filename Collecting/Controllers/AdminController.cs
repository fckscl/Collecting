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

        //public IActionResult Create() => View();

        //[HttpPost]
        //public async Task<IActionResult> Create(CreateUserViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IdentityUser user = new IdentityUser { Email = model.Email, UserName = model.Email, Year = model.Year };
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }
        //        }
        //    }
        //    return View(model);
        //}

        //public async Task<IActionResult> Edit(string id)
        //{
        //    IdentityUser user = await _userManager.FindByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, Year = user.Year };
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(EditUserViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IdentityUser user = await _userManager.FindByIdAsync(model.Id);
        //        if (user != null)
        //        {
        //            user.Email = model.Email;
        //            user.UserName = model.Email;
        //            user.Year = model.Year;

        //            var result = await _userManager.UpdateAsync(user);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                foreach (var error in result.Errors)
        //                {
        //                    ModelState.AddModelError(string.Empty, error.Description);
        //                }
        //            }
        //        }
        //    }
        //    return View(model);
        //}

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
        public async Task<ActionResult> Block(string userId)
        {
            // получаем пользователя
            IdentityUser user = await _userManager.FindByIdAsync(userId);
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
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> MakeAdmin(string userId)
        {
            // получаем пользователя
            IdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                string[] act = { "admin" };
                if (userRoles.Contains("admin"))
                {
                    await _userManager.RemoveFromRolesAsync(user, act);
                }
                else
                {
                    await _userManager.AddToRolesAsync(user, act);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
