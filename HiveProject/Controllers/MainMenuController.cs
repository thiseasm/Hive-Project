using HiveProject.Models;
using HiveProject.Viewmodels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HiveProject.Managers;

namespace HiveProject.Controllers
{
    [Authorize]
    public class MainMenuController : Controller
    {


        public async Task<ActionResult> Index()
        {
            var getUsers =await new MatchingManager().GetUsersAsync();
            return View(getUsers);
        }


        // GET: MainMenu
        public ActionResult Profile()
        {
            var profile = new ProfileViewModel();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                if (user != null)
                {
                    profile.Id = user.Id;
                    profile.Thumbnail = user.Thumbnail;
                }
            }
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfilePic(ApplicationUser user, HttpPostedFileBase Avatar)
        {
            if (Avatar != null)
            {
                user.Thumbnail = Path.GetFileName(user.Avatar.FileName);
                string fileName = Path.Combine(Server.MapPath("~/Content/Images/"), user.Thumbnail);
                Avatar.SaveAs(fileName);
                using (var context = new ApplicationDbContext())
                {
                    var usertoupdate = context.Users.Find(user.Id);
                    usertoupdate.Thumbnail = user.Thumbnail;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Profile");
        }


        public async Task<ActionResult> Matching()
        {
            MatchingManager manager = new MatchingManager();
           // await manager.AsyncMatching();
            var matches = await manager.ReturnMatchesAsync();
            return View(matches);
        }

    }
}