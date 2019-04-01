
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
using System.Web.Services;

namespace HiveProject.Controllers
{
    [Authorize]
    public class MainMenuController : Controller
    {
        private MatchingManager _Manager { get; set; }
        private InteractionManager _Locator { get; set; }

        public MainMenuController()
        {
            _Manager = new MatchingManager();
        }

        public ActionResult Index()
        {
            return View();
        }


        //public async Task<ActionResult> Index22()
        //{
        //    var getUsers = await new MatchingManager().GetUsersAsync("3256e5eb-4a92-4372-90f0-abfa7b173c6f", 37.94m, 23.71m,50);

        //    return Json(getUsers, JsonRequestBehavior.AllowGet);
        //    //return View(getUsers);
        //}


        public async Task<ActionResult> Profiles()
        {
            var profile = new ProfileViewModel();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user =await db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                if (user != null)
                {
                    profile.Id = user.Id;
                    profile.Thumbnail = user.Thumbnail;
                }
            }
            return View(profile);
        }

        [HttpGet]
        [WebMethod]
        public async Task<JsonResult> GetPic()
        {
            string currentuser = User.Identity.GetUserId();
            using (var db = new ApplicationDbContext())
            {
                var currentUser = await db.Users.SingleOrDefaultAsync(z => z.Id == currentuser);
                var thumbnail = currentUser.Thumbnail;
                thumbnail = "/Content/Images/" + thumbnail;
                return Json(thumbnail, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfilePic(ApplicationUser user, HttpPostedFileBase Avatar)
        {
            var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg" };

            if (Avatar != null)
            {
                var getExtension = Path.GetExtension(Avatar.FileName);
                if (!allowedExtensions.Contains(getExtension))
                {
                    TempData["Error"] = "Not a supported extension,try .Jpg /.png/.jpg/.jpeg";
                    return RedirectToAction("Profiles");
                }

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
            return RedirectToAction("Profiles");
        }


        public async Task<ActionResult> Matching()
        {
            // await manager.AsyncMatching();
            var matches = await _Manager.ReturnMatchesAsync();
            return View(matches);
        }

        [HttpPost]
        public async Task<ActionResult> AddLike(string id)
        {
            var like = true;
            await _Manager.AddLikeAndMatch(id, like);
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> AddDislike(string id)
        {
            var like = false;
            await _Manager.AddLikeAndMatch(id, like);
            return PartialView();
        }

    }
}