using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication_C14.Entities;

namespace WebApplication_C14.server
{
    public class UserDb : IdentityDbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=130.185.75.54;Database=Project#C14;Encrypt=false;user id = i3center-1561 ; password = 123");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
