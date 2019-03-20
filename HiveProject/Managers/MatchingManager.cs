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


        public async Task<List<GetUsersViewModel>> GetUsersAsync()
        {
            using (var db = new ApplicationDbContext())
            {
                var returnUsers = await db.Users
                                    .Where(t => t.Id != _currentLoggedUser && !db.Likes
                                    .Where(u => u.SenderId == _currentLoggedUser && u.ReceiverId == t.Id).Any())
                                    .Select(g => new GetUsersViewModel
                                    {
                                        Id = g.Id,
                                        Thumbnail = g.Thumbnail,
                                        Age = g.Age,
                                        Gender = g.UserGender,
                                        Username = g.UserName
                                    })
                                    .ToListAsync();

                return returnUsers;
            }
        }

        // Gets the id of the user that you liked
        public async Task AddLikeAndMatch(string id, bool like)
        {
            using (var db = new ApplicationDbContext())
            {
                var addLike = db.Likes.Add(new Likes
                {
                    SenderId = _currentLoggedUser,
                    ReceiverId = id,
                    Like = like
                });

                if (await db.Likes.Where(x => x.SenderId == id && x.ReceiverId == _currentLoggedUser && x.Like == true).AnyAsync())
                {
                    db.Matches.Add(new Matches
                    {
                        MyUserId = _currentLoggedUser,
                        MatchedUserId = id,
                        SeenByMyUser = false
                    });
                    db.Matches.Add(new Matches
                    {
                        MyUserId = id,
                        MatchedUserId = _currentLoggedUser,
                        SeenByMyUser = false
                    });
                }

                await db.SaveChangesAsync();
            }
        }

        
        // Calculates the matches whenever is called.


        // NOT USED AT THE MOMENT
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

        public async Task<List<GetUsersViewModel>> ReturnMatchesAsync()
        {
            using (var db = new ApplicationDbContext())
            {
                var matches = await db.Matches.Include(y => y.MatchedUser2).Where(x => x.MyUserId == _currentLoggedUser)
                                            .Select(g => new GetUsersViewModel
                                            {
                                                Id = g.MatchedUser2.Id,
                                                Thumbnail = g.MatchedUser2.Thumbnail,
                                                Age = g.MatchedUser2.Age,
                                                Gender = g.MatchedUser2.UserGender,
                                                Username = g.MatchedUser2.UserName
                                            })
                                            .Distinct()
                                            .ToListAsync();
                return matches;
            }
        }
    }
}