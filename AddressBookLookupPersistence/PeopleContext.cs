using AddressBookLookupDomain.Model;
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
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            _dbPath = Path.Join(path, "Persons.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={_dbPath}");
    }
}
