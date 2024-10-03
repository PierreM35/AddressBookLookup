using Grpc.Core;
using Protos;

namespace FindPersonService.Services
{
    public class PersonsLookupService : PersonsFinder.PersonsFinderBase
    {
        private readonly ILogger<PersonsLookupService> _logger;

        public PersonsLookupService(ILogger<PersonsLookupService> logger)
        {
            _logger = logger;
        }

        public override async Task FindPersons(PersonLookup request, IServerStreamWriter<Person> responseStream, ServerCallContext context)
        {
            //var channel = GrpcChannel.ForAddress("http://localhost:5091");

            //var client = new AdressBookGetter.AdressBookGetterClient(channel);
            //var reply = await client.GetAddressBookAsync(new AddressBookRequest());
            //foreach (var person in reply.AddressBook.Persons)
            //{
            //    await responseStream.WriteAsync(person);
            //}

            await responseStream.WriteAsync(
                new Person
                {
                    Surname = "Pierre",
                    Address = new Person.Types.Address
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
                    Address = new Person.Types.Address
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
                    Address = new Person.Types.Address
                    {
                        Street = "Findelwiesenstr",
                        HomeNumber = 13,
                        City = "Rennes"
                    }
                });
        }
    }
}
