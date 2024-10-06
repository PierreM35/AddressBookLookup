using AddressBookLookupDomain.Model;
using AddressBookLookupDomain.Resources;
using Microsoft.EntityFrameworkCore;

namespace AddressBookLookupPersistence
{
    public class PeopleContext : DbContext
    {
        private readonly string _dbPath;

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public PeopleContext()
        {
            _dbPath = GetDbPath();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={_dbPath}");

        private string GetDbPath()
        {
            var exeDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            for (var i = 0; i < 4; i++)
                exeDir = exeDir.Parent;

            return Path.Combine(exeDir.FullName, @"AddressBookLookupPersistence\Persons.db");
        }
    }
}
