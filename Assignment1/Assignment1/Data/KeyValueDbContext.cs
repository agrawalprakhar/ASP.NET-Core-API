using Assignment1.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Data
{
    public class KeyValueDbContext : DbContext
    {
        public KeyValueDbContext(DbContextOptions<KeyValueDbContext> options) : base(options)
        {
        }

      public DbSet<CustomKeyValuePair> KeyValuePairs { get; set; }


    }
}
