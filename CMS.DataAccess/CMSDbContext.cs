using CMS.Models.Authentication;
using CMS.Models.Content;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CMS.DataAccess
{
    public class CMSDbContext : IdentityDbContext<ApplicationUser>
    {
        public CMSDbContext(DbContextOptions<CMSDbContext> options) : base(options)
        {
        }

        DbSet<Category> Categories { get; set; }
    }
}