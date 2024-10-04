using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Protos;

namespace FindPersonService.Services
{
    public class AddressBookLookupService : AddressBookLookup.AddressBookLookupBase
    {
        private readonly ILogger<AddressBookLookupService> _logger;
        private readonly AddressBook _addressBook;

        public AddressBookLookupService(ILogger<AddressBookLookupService> logger)
        {
            _logger = logger;

            _addressBook = new AddressBook();
            _addressBook.Persons.Add(
                [
                new Person
                {
                    Name = "Pierre",
                    Address = new Address
                    {
                        City = "Nuremberg",
                        HomeNumber = 2,
                        Street = "le bas hil"
                    }
                },
                new Person
                {
                    Name = "Pierre",
                    Address = new Address
                    {
                        City = "osse",
                        HomeNumber = 4,
                        Street = "le bas hil"
                    }
                },
                new Person
                {
                    Name = "Laura",
                    Address = new Address
                    {
                        City = "Mogo",
                        HomeNumber = 3,
                        Street = "findel"
                    }
                },
                new Person
                {
                    Name = "Chloe",
                    Address = new Address
                    {
                        City = "Rennes",
                        HomeNumber = 3,
                        Street = "rue des chavagnes"
                    }
                }
                ]);
        }

        public override Task<AddressBook> GetAddressBook(GetAddressBookRequest request, ServerCallContext context)
        {
            return Task.FromResult(_addressBook);
        }

        public override async Task GetPersons(GetPersonsRequest request, IServerStreamWriter<Person> responseStream, ServerCallContext context)
        {
            var searchedPerson = request.Person;
            var mask = request.FieldMask;

            foreach (var person in _addressBook.Persons.Where(p => Match(p, searchedPerson, mask)))
                await responseStream.WriteAsync(person);
        }

        private bool Match(Person person1, Person person2, FieldMask mask)
        {
            foreach (var path in mask.Paths)
            {
                if (path.Equals("Name", StringComparison.OrdinalIgnoreCase) && !person1.Name.Equals(person2.Name))
                    return false;

                if (path.Equals("Surname", StringComparison.OrdinalIgnoreCase) && !person1.Surname.Equals(person2.Surname))
                    return false;

                if (path.Equals("Address", StringComparison.OrdinalIgnoreCase) && !AreEqual(person1.Address, person2.Address))
                    return false;
            }

            return true;
        }

        private static bool AreEqual(Address addresse1, Address addresse2)
        {
            return
                addresse1.Street.Equals(addresse2.Street) &&
                addresse1.HomeNumber == addresse2.HomeNumber &&
                addresse1.City.Equals(addresse2.City);
        }
    }
}
