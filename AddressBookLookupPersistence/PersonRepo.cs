using AddressBookLookupDomain.Abstractions;
using AddressBookLookupDomain.Model;
using Microsoft.EntityFrameworkCore;

namespace AddressBookLookupPersistence
{
    public class PersonRepo : IRepo<Person>
    {
        private PeopleContext _dbContext;

        public PersonRepo()
        {
            _dbContext = new PeopleContext();
        }

        public void Add(Person data)
        {
            _dbContext.Add(data);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Person> Get(Func<Person, bool> predicate) => _dbContext.Persons.Include(p => p.Address).Where(predicate);

        public IEnumerable<Person> GetAll() => _dbContext.Persons.Include(p => p.Address);

        public void Remove(Func<Person, bool> predicate)
        {
            foreach (var person in _dbContext.Persons.Where(predicate))
                _dbContext.Persons.Remove(person);

            _dbContext.SaveChanges();
        }
    }
}
