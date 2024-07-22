using Microsoft.EntityFrameworkCore;
using DataProtectionProvider.Model;

namespace DataProtectionProvider.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<PersonalData> PersonalData { get; set; }
    }
}