using HiveProject.Models;
using HiveProject.Viewmodels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HiveProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public async Task<ActionResult> Index()
        {
            var users = new List<UsersViewModel>();
            using (var db = new ApplicationDbContext())
            {
               users = await db.Users.Where(x => x.Email != "Admin@admin.com")
                                .Select(y => new UsersViewModel
                                {
                                    Id = y.Id,
                                    Username = y.UserName,
                                    Thumbnail = y.Thumbnail,
                                    Age = y.Age,
                                    Gender = y.UserGender,
                                    Bio = y.Bio
                                })
                                .ToListAsync();
            }
            return View(users);
        }
    }
}