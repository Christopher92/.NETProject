using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }


        [Display(Name="Imię")]
        public string Name { get; set; }
        [Display(Name="Nazwisko")]
        public string Surname { get; set; }
        [Display(Name="Telefon")]
        public long Telephone { get; set; }
        [Display(Name="Adres")]
        public string Address { get; set; }


    }

    public class ApplicationInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var identityRoles = new List<IdentityRole>
            {
                new IdentityRole { Id="1", Name="Administrator"},
                new IdentityRole { Id="2", Name="Klient"},
                new IdentityRole { Id="3", Name="Sprzedawca"}
            };

            identityRoles.ForEach(u => context.Roles.Add(u));
            context.SaveChanges();

        }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ComputerShopContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}