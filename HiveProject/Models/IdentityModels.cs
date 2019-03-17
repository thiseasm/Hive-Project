using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace HiveProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public Gender UserGender { get; set; }

        [Required]
        public int Age { get; set; }

        public string Thumbnail { get; set; }

        [NotMapped]
        public HttpPostedFileBase Avatar { get; set; }

        [InverseProperty("User1")]
        public virtual ICollection<Likes> Likes1 { get; set; }

        [InverseProperty("User2")]
        public virtual ICollection<Likes> Likes2 { get; set; }

        public virtual CurrentLocation Location { get; set; }

        public UserPreferences Preferences { get; set; }

        public enum Gender
        {
            Male=1,
            Female=2,
            Fluid=3
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Likes> Likes { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}