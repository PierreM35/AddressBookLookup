using Grpc.Core;
using Protos;

namespace AdressBookService.Services
{
    public class AddressBookGetterService : PersonLookup.PersonLookupBase
    {
        //private IEnumerable<Person> _persons;

        public AddressBookGetterService()
        {

        }

        public override Task<AddressBook> GetAddressBook(AddressBookRequest request, ServerCallContext context)
        {
            var addressBook = new AddressBook();
            addressBook.Persons.Add(
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

            return Task.FromResult(addressBook);
        }
    }
}
