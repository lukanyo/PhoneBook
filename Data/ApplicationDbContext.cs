using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Data
{
    public class ApplicationDbContext: DbContext
    {
        public virtual DbSet<Entry> Entries { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
    }

}