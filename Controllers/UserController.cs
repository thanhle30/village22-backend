using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Village22.Models;

namespace Village22.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        ///////////// Still Need: Add Account (with correct role)

        private UserManager<User> userManager;
        private RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<User> userMngr, RoleManager<IdentityRole> roleMngr)
        {
            userManager = userMngr;
            roleManager = roleMngr;
        }

        public async Task<IActionResult> Index()
        {
            List<User> users = new List<User>();

            var users_db = userManager.Users.ToList();

            foreach (User user in users_db)
            {
                user.RoleNames = await userManager.GetRolesAsync(user);
                users.Add(user);
            }

            UserViewModel model = new UserViewModel
            {
                Users = users,
                Roles = roleManager.Roles
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    string errorMessage = "";
                    foreach (IdentityError error in result.Errors)
                    {
                        errorMessage += error.Description + " | ";
                    }
                    TempData["message"] = errorMessage;
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            IdentityRole adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                TempData["message"] = "Admin role does not exist. " + "Click 'Create Admin Role' button to create it.";
            }
            else
            {
                User user = await userManager.FindByIdAsync(id);
                await userManager.AddToRoleAsync(user, adminRole.Name);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Admin");
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddToTeacher(string id)
        {
            IdentityRole teacherRole = await roleManager.FindByNameAsync("Teacher");
            if (teacherRole == null)
            {
                TempData["message"] = "Teacher role does not exist.";
            }
            else
            {
                User user = await userManager.FindByIdAsync(id);
                await userManager.AddToRoleAsync(user, teacherRole.Name);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RemoveFromTeacher(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Teacher");
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddToTa(string id)
        {
            IdentityRole taRole = await roleManager.FindByNameAsync("Ta");
            if (taRole == null)
            {
                TempData["message"] = "TA role does not exist.";
            }
            else
            {
                User user = await userManager.FindByIdAsync(id);
                await userManager.AddToRoleAsync(user, taRole.Name);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RemoveFromTa(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Ta");
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            await roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(model.Username);
                var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        
    }
}
