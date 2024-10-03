using Grpc.Core;
using Grpc.Net.Client;
using Protos;

namespace FindPersonService.Services
{
    public class PersonsLookupService : PersonLookup.PersonLookupBase
    {
        private readonly ILogger<PersonsLookupService> _logger;

        public PersonsLookupService(ILogger<PersonsLookupService> logger)
        {
            _logger = logger;
        }

        public override async Task FindPersons(SearchedPerson request, IServerStreamWriter<Person> responseStream, ServerCallContext context)
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
