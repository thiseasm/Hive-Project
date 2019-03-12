using HiveProject.Models;
using HiveProject.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiveProject.Controllers
{
    [Authorize]
    [System.Runtime.InteropServices.Guid("4FAC958D-84A4-4265-83A2-18493FB9A39C")]
    public class MainMenuController : Controller
    {
        // GET: MainMenu
        public ActionResult Index()
        {
            var accountviewmodel = new AccountViewModel();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                if(user!=null)
                {
                    accountviewmodel.Username = user.UserName;
                    accountviewmodel.Thumbnail = user.Thumbnail;
                }
            }
                return View(accountviewmodel);
        }
    }
}