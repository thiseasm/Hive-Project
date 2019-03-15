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


        public async Task<ActionResult> Matching()
        {
            var myLikes = new List<string>();
            var likedFrom = new List<string>();
            var matchingList = new List<ApplicationUser>();
            var finalMatchingList = new List<ApplicationUser>();
            string currentLoggedUser = User.Identity.GetUserId();

            using (var db = new ApplicationDbContext())
            {
                myLikes = await db.Likes.Where(x => x.SenderId == currentLoggedUser && x.Like == true)
                                        .Select(y => y.ReceiverId)
                                        .Distinct()
                                        .ToListAsync();
                likedFrom = await db.Likes.Where(x => x.ReceiverId == currentLoggedUser && x.Like == true)
                                        .Select(y => y.SenderId)
                                        .Distinct()
                                        .ToListAsync();
            }

            foreach (var likedUser in myLikes)
            {
                foreach (var likedByUser in likedFrom)
                {
                    if (likedUser == likedByUser)
                        matchingList.Add(new ApplicationUser { Id = likedUser });
                }
            }

            using (var db = new ApplicationDbContext())
            {
                foreach (var matchedUser in matchingList)
                {
                    var finalMatchedUser = await db.Users.FirstOrDefaultAsync(x => x.Id == matchedUser.Id);
                    finalMatchingList.Add(finalMatchedUser);
                }
            }

            var matchingModel = new List<MatchingViewModel>();
            foreach (var user in finalMatchingList)
            {
                matchingModel.Add(new MatchingViewModel
                {
                    Id = user.Id,
                    Thumbnail = user.Thumbnail,
                    Age = user.Age,
                    Gender = user.UserGender,
                    Username = user.UserName
                });
            }
            return View(matchingModel);
        }

    }
}