using AddressBookLookupDomain.Abstractions;
using AddressBookLookupDomain.Model;

namespace AddressBookLookupPersistence
{
    public class PersonRepo : IRepo<Person>
    {
        private List<Person> _people;

        public PersonRepo()
        {
            _people = new List<Person>
            {
                new Person("Pierre", "", new Address("Nuremberg", 2, "le bas hil")),  
                new Person("Pierre", "", new Address("osse", 4, "le bas hil")),  
                new Person("Laura", "", new Address("Mogo", 2, "findelwiesen")),
                new Person("Chloe", "", new Address("Rennes", 3, "rue des chavagnes"))
            };
        }

        public void Add(Person data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> Get(Func<Person, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetAll() => _people;

        public void Remove(Func<Person, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
