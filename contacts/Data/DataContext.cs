using Microsoft.EntityFrameworkCore;

namespace contacts.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        
        DbSet<Models.Contact> Contacts { get; set; }
    }
}