using HiveProject.Models;
using HiveProject.Viewmodels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HiveProject.Managers
{
    public class MatchingManager
    {
        private string _currentLoggedUser { get; set; }

        public MatchingManager()
        {
            _currentLoggedUser = HttpContext.Current.User.Identity.GetUserId();
        }

        // Calculates the matches whenever is called.
        public async Task AsyncMatching()
        {
            var myLikes = new List<string>();
            var likedFrom = new List<string>();
            var matchingList = new List<string>();
            var finalMatchingList = new List<ApplicationUser>();

            using (var db = new ApplicationDbContext())
            {
                myLikes = await db.Likes.Where(x => x.SenderId == _currentLoggedUser && x.Like == true)
                                        .Select(y => y.ReceiverId)
                                        .Distinct()
                                        .ToListAsync();
                likedFrom = await db.Likes.Where(x => x.ReceiverId == _currentLoggedUser && x.Like == true)
                                        .Select(y => y.SenderId)
                                        .Distinct()
                                        .ToListAsync();
            }

            foreach (var likedUser in myLikes)
            {
                foreach (var likedByUser in likedFrom)
                {
                    if (likedUser == likedByUser)
                        matchingList.Add(likedUser);
                }
            }

            using (var db = new ApplicationDbContext())
            {
                foreach (var likedUser in matchingList)
                {
                    var match = await db.Matches.FirstOrDefaultAsync(x => x.MyUserId == _currentLoggedUser && x.MatchedUserId == likedUser);
                    if (match == null)
                    {
                        db.Matches.Add(new Matches
                        {
                            MyUserId = _currentLoggedUser,
                            MatchedUserId = likedUser,
                            SeenByMyUser = false

                        });

                        // Also adding the same match by reversing users.
                        db.Matches.Add(new Matches
                        {
                            MyUserId = likedUser,
                            MatchedUserId = _currentLoggedUser,
                            SeenByMyUser = false

                        });
                    }
                }
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<MatchingViewModel>> AsyncReturnMatches()
        {
            var matches = new List<Matches>();
            var matchingModel = new List<MatchingViewModel>();

            using (var db = new ApplicationDbContext())
            {
                matches = await db.Matches.Include(y => y.MatchedUser2).Where(x => x.MyUserId == _currentLoggedUser).ToListAsync();
            }

            foreach (var user in matches)
            {
                matchingModel.Add(new MatchingViewModel
                {
                    Id = user.MatchedUser2.Id,
                    Thumbnail = user.MatchedUser2.Thumbnail,
                    Age = user.MatchedUser2.Age,
                    Gender = user.MatchedUser2.UserGender,
                    Username = user.MatchedUser2.UserName
                });
            }

            return matchingModel;
        }
    }
}