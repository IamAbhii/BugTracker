using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker_project.Models
{
    public class UserHelper
    {
        ApplicationDbContext db;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        public UserHelper(ApplicationDbContext db)
        {
            this.db = db;
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public void CreateNewRole(string roleName)
        {
            roleManager.Create(new IdentityRole(roleName));
        }

        public string GetRole(string roleId)
        {
            return roleManager.FindById(roleId).Name;
        }

        public bool CheckRole(string roleName)
        {
            return roleManager.RoleExists(roleName);
        }

    
        public void AssignRole(string userId, string roleName)
        {
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(roleName))
            {
                userManager.AddToRole(userId, roleName);
            }
            else
            {
                throw new NullReferenceException("User Id and RoleName can not be null");
            }
        }
    }
}