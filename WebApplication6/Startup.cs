using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication6.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication6.Startup))]
namespace WebApplication6
{
   
    
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }
        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole { Name = "Admin" };
                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "Andre_145@hotmail.se",
                    Email = "Andre_145@hotmail.se"
                };
                var userPassword = "100%Abbe";
                var chkUser = userManager.Create(user, userPassword);

                if (chkUser.Succeeded)
                    userManager.AddToRole(user.Id, "Admin");

            }

            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole { Name = "ProductManager" };
                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "Abbe@gmail.com",
                    Email = "Abbe@gmail.com"
                };
                var userPassword = "100%Abbe";
                var chkUser = userManager.Create(user, userPassword);

                if (chkUser.Succeeded)
                    userManager.AddToRole(user.Id, "ProductManager");

            }
        }
    }
}