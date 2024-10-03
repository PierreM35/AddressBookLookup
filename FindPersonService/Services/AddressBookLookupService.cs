using Grpc.Core;
using Grpc.Net.Client;
using Protos;

namespace FindPersonService.Services
{
    public class AddressBookLookupService : AddressBookLookup.AddressBookLookupBase
    {
        private readonly ILogger<AddressBookLookupService> _logger;

        public AddressBookLookupService(ILogger<AddressBookLookupService> logger)
        {
            _logger = logger;
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

        public override async Task FindPersons(Person searchedPerson, IServerStreamWriter<Person> responseStream, ServerCallContext context)
        {
            //var channel = GrpcChannel.ForAddress("http://localhost:5091");

            //var client = new PersonLookup.PersonLookupClient(channel);
            //var reply = await client.GetAddressBookAsync(new AddressBookRequest());
            //foreach (var person in reply.Persons)
            //{
            //    await responseStream.WriteAsync(person);
            //}

            await responseStream.WriteAsync(
                new Person
                {
                    Surname = "Pierre",
                    Address = new Address
                    {
                        Street = "Findelwiesenstr",
                        HomeNumber = 13,
                        City = "Nuremberg"
                    }
                });

            await responseStream.WriteAsync(
                new Person
                {
                    Surname = "Laura",
                    Address = new Address
                    {
                        Street = "Findelwiesenstr",
                        HomeNumber = 13,
                        City = "Mogo"
                    }
                });
            await responseStream.WriteAsync(
                new Person
                {
                    Surname = "Marc",
                    Address = new Address
                    {
                        Street = "Findelwiesenstr",
                        HomeNumber = 13,
                        City = "Rennes"
                    }
                });
        }
    }
}
