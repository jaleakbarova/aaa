using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Template.Models;
using Microsoft.AspNetCore.Identity;


namespace Template.DAL
{
        public class AppDbContext : IdentityDbContext<AppUser>
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {

            }

            public DbSet<Slider> Sliders { get; set; }
            public DbSet<About> Abouts { get; set; }
            public DbSet<Client> Clients { get; set; }
            public DbSet<Blog> Blog { get; set; }
            public DbSet<OurService> OurServices { get; set; }
            public DbSet<Profession> Professions { get; set; }
            


    }
}

