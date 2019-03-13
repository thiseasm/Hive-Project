using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiveProject.Models;

namespace HiveProject.Controllers
{
    public class InteractionController : Controller
    {
        // GET: Interaction
        public ActionResult Index()
        {
            return View();
        }

        public List<ApplicationUser> GetUnmatchedUsers(ApplicationUser User1)
        {
            using(var db = new ApplicationDbContext())
            {
                //TODO add linQ for unmatched users
            }
            
                
        }
    }
}