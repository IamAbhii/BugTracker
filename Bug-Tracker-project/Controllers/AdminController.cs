using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Tracker_project.Models;
using Bug_Tracker_project.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Bug_Tracker_project.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        UserManager<ApplicationUser> userManager;
        RoleManager<IdentityRole> roleManager;
        UserHelper userHelper;
        //ApplicationUser User;

        public AdminController()
        {
            roleManager= new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            userManager= new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            userHelper = new UserHelper(db);
        }
        // GET: Admin
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult AssignRole()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");
            ViewBag.RoleName = new SelectList(db.Roles, "Name", "Name");

            return View();
        }
        [HttpPost]
        public ActionResult AssignRole(string UserId,string RoleName)
        {
            userHelper.AssignRole(UserId, RoleName);

           return View("Index");
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}