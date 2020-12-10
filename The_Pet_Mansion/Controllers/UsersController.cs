using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using The_Pet_Mansion.Models;

namespace The_Pet_Mansion.Controllers
{

    [Authorize(Roles = "Admin")]

    public class UsersController : Controller
    {
       private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = from user in db.Users
                        orderby user.UserName
                        select user;
            ViewBag.UsersList = users;
            return View();
        }


        public ActionResult Show(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            ViewBag.utilizatorCurent = User.Identity.GetUserId();
            string currentRole = user.Roles.FirstOrDefault().RoleId;
            var userRoleName = (from role in db.Roles
                                where role.Id == currentRole
                                select role.Name).First();
            ViewBag.roleName = userRoleName;
            return View(user);
        }
    }
}