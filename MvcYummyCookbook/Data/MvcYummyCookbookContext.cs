using Microsoft.EntityFrameworkCore;
using MvcYummyCookbook.Models;

namespace MvcYummyCookbook.Data
{
    public class MvcYummyCookbookContext : DbContext
    {
        public MvcYummyCookbookContext(DbContextOptions<MvcYummyCookbookContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipe { get; set; }
    }
}
