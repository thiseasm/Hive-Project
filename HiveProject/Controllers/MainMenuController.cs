using HiveProject.Models;
using HiveProject.Viewmodels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HiveProject.Controllers
{
    [Authorize]
    public class MainMenuController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        // Testing match


        public ActionResult Matching()
        {
            var myLikes = new List<Likes>();
            var likedFrom = new List<Likes>();
            var matchingList = new List<ApplicationUser>();
            var finalMatchingList = new List<ApplicationUser>();
            using (var db = new ApplicationDbContext())
            {
                myLikes = db.Likes.Where(x => x.SenderId == User.Identity.GetUserId() && x.Like==true).ToList();
                likedFrom = db.Likes.Where(x => x.ReceiverId == User.Identity.GetUserId() && x.Like==true).ToList();
            }

            

            foreach(var x in myLikes)
            {
                foreach(var y in likedFrom)
                {
                    if (x.ReceiverId == y.SenderId)
                        matchingList.Add(new ApplicationUser { Id = x.ReceiverId });
                }
            }

            using (var db = new ApplicationDbContext())
            {
                finalMatchingList = db.Users.Where(x => matchingList.Any(y => y.Id == x.Id)).ToList();
            }

            var matchingModel = new MatchingViewModel();
            foreach(var user in finalMatchingList)
            {
                matchingModel.Id = user.Id;
                matchingModel.Username = user.UserName;
                matchingModel.Thumbnail = user.Thumbnail;
                matchingModel.Age = user.Age;
                matchingModel.Gender = user.UserGender;
            }


                return View(matchingModel);
        }

    }
}