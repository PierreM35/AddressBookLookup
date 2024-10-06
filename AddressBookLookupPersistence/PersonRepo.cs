using AddressBookLookupDomain.Abstractions;
using AddressBookLookupDomain.Model;

namespace AddressBookLookupPersistence
{
    public class PersonRepo : IRepo<Person>
    {
        private PeopleContext _dbContext;

        public PersonRepo()
        {
            //var _people = new List<Person>
            //{
            //    new Person(Guid.NewGuid(), "Pierre", "", new Address(Guid.NewGuid(), "Nuremberg", "2", "le bas hil")),
            //    new Person(Guid.NewGuid(), "Pierre", "", new Address(Guid.NewGuid(), "osse", "4", "le bas hil")),
            //    new Person(Guid.NewGuid(), "Laura", "", new Address(Guid.NewGuid(), "Mogo", "2", "findelwiesen")),
            //    new Person(Guid.NewGuid(), "Chloe", "", new Address(Guid.NewGuid(), "Rennes", "3", "rue des chavagnes"))
            //};

            _dbContext = new PeopleContext();
            //var dbExists = _dbContext.Database.EnsureCreated();
            //_dbContext.Persons.AddRange(_people);
            //_dbContext.SaveChanges();
        }

        public void Add(Person data)
        {
            _dbContext.Add(data);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Person> Get(Func<Person, bool> predicate) => _dbContext.Persons.Where(predicate);

        public IEnumerable<Person> GetAll() => _dbContext.Persons;

        public void Remove(Func<Person, bool> predicate)
        {
            foreach (var person in _dbContext.Persons.Where(predicate))
                _dbContext.Persons.Remove(person);

            _dbContext.SaveChanges();
        }
    }
}
