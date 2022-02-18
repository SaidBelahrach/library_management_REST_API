using library_management_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace library_management_REST_API.DataAccess
{
    public class myDbContext:DbContext
    { 
        public myDbContext(DbContextOptions<myDbContext> options) : base(options)   { }

        public DbSet<Book> Books { get; set; }
    }
}
