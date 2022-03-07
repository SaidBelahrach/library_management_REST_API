using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace library_management_REST_API.Models
{
    public class myDbContext : IdentityDbContext
    {
        public myDbContext(DbContextOptions<myDbContext> options) : base(options) { }


        public DbSet<Book> Books { get; set; }
    }
}
