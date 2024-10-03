//using AddressBookLookup.Domain;
using AdressBookService.Protos;
using Grpc.Core;

namespace AdressBookService.Services
{
    public class AddressBookGetterService : AdressBookGetter.AdressBookGetterBase
    {
        //private IEnumerable<Person> _persons;

        public AddressBookGetterService()
        {

        }

        public override Task<AddressBookResponse> GetAddressBook(AddressBookRequest request, ServerCallContext context)
        {
            var addressBook = new AddressBookResponse.Types.AddressBook();
            addressBook.Persons.Add(
                [
                new AddressBookResponse.Types.AddressBook.Types.Person
                {
                    Name = "Pierre",
                    Address = new AddressBookResponse.Types.AddressBook.Types.Person.Types.Address
                    {
                        City = "Nuremberg",
                        HomeNumber = 2,
                        Street = "le bas hil"
                    }
                },
                new AddressBookResponse.Types.AddressBook.Types.Person
                {
                    Name = "Laura",
                    Address = new AddressBookResponse.Types.AddressBook.Types.Person.Types.Address
                    {
                        City = "Mogo",
                        HomeNumber = 3,
                        Street = "findel"
                    }
                },
                new AddressBookResponse.Types.AddressBook.Types.Person
                {
                    Name = "Chloe",
                    Address = new AddressBookResponse.Types.AddressBook.Types.Person.Types.Address
                    {
                        City = "Rennes",
                        HomeNumber = 3,
                        Street = "rue des chavagnes"
                    }
                }
                ]);

            return Task.FromResult(new AddressBookResponse { AddressBook = addressBook });
        }
    }
}
