using HiveProject.Models;
using HiveProject.Viewmodels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace HiveProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        public async Task<ActionResult> Index(int? page)
        {
            using (var db = new ApplicationDbContext())
            {
                var role = await db.Roles.SingleAsync(y => y.Name == "Admin");
                var users = db.Users.Where(x => x.Email != "Admin@admin.com" && !x.Roles.Any(z => z.RoleId == role.Id))
                                 .Select(y => new UsersViewModel
                                 {
                                     Id = y.Id,
                                     Username = y.UserName,
                                     Thumbnail = y.Thumbnail,
                                     Age = y.Age,
                                     Gender = y.UserGender,
                                     Bio = y.Bio
                                 })
                                 .ToList().ToPagedList(page ?? 1, 5);

                return View(users);
            }
        }

        public async Task<ActionResult> Upgrade(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                await manager.AddToRoleAsync(id, "Admin");
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");

        }

        public ActionResult ShowUserMessages(string id,int? page)
        {
            using (var db = new ApplicationDbContext())
            {
                var messages = db.Messages.Include("ApplicationUser").Where(x => x.SenderId == id || x.ReceiverId == id)
                                        .Select(y => new MessageViewModel
                                        {
                                            MessageId = y.MessageId,
                                            SenderName = y.Sender.UserName,
                                            ReceiverName = y.Receiver.UserName,
                                            Date = y.DateSent,
                                            Body=y.Body
                                        }).OrderBy(c => c.Date)
                                        .ToList()
                                        .ToPagedList(page ?? 1, 10);
                return View(messages);
            }
        }
    }
}