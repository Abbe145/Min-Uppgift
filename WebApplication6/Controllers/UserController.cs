using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;
using WebApplication6.ViewModels.User;

namespace WebApplication6.Controllers
{
    public class UserController : Controller

    {

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }




        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var model = new UserIndexViewModel();

            using (var db = new ApplicationDbContext())
            {
                model.Users.AddRange(db.Users.Select(u => new UserIndexViewModel.UserListViewModel
                {
                    UserId = u.Id,
                    Email = u.Email,
                    UserName = u.UserName
                }));
                foreach (var item in model.Users)
                {
                    item.UserRoles = UserManager.GetRoles(item.UserId).SingleOrDefault();
                }
                return View(model);
            }
        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            var model = new UserViewModel();
            var user = UserManager.FindById(id);
            using (var db = new ApplicationDbContext())
            {
                model.UserId = user.Id;
                model.Email = user.Email;
                model.UserName = user.UserName;
                model.UserRoles = UserManager.GetRoles(user.Id).SingleOrDefault();
                model.UserDropDownList = new List<SelectListItem>();

                foreach (var item in db.Roles)
                {
                    model.UserDropDownList.Add(new SelectListItem { Value = item.Name, Text = item.Name });
                }

                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new ApplicationDbContext())
            {
                var user = UserManager.FindById(model.UserId);
                user.Id = model.UserId;
                user.UserName = model.UserName;
                user.Email = model.Email;
                UserManager.RemoveFromRole(user.Id, model.UserRoles);
                UserManager.AddToRole(user.Id, model.UserDropDownHolder);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            var model = new UserViewModel();
            using (var db = new ApplicationDbContext())
            {
                var user = UserManager.FindById(id);
                model.Email = user.Email;
                model.UserName = user.UserName;
                model.UserRoles = UserManager.GetRoles(user.Id).SingleOrDefault();

                return View(model);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var user = await UserManager.FindByIdAsync(id);
            var rolesForUser = await UserManager.GetRolesAsync(id);

            if (rolesForUser.Count() > 0)
            {

                foreach (var item in rolesForUser.ToList())
                {
                    var result = await UserManager.RemoveFromRoleAsync(user.Id, item);
                }
            }

            await UserManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

    }
}

