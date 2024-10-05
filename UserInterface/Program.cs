
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Protos;

var channel = GrpcChannel.ForAddress("http://localhost:5091");
var addressBookClient = new AddressBookLookup.AddressBookLookupClient(channel);

var addressBook = addressBookClient.GetAddressBook(new GetAddressBookRequest());

var cancellationtokensource = new CancellationTokenSource();
var getPersonRequest = new GetPersonsRequest 
{ 
    Person = new Person { Name = "Pierre" }, 
    FieldMask = new FieldMask { Paths = { "name" } }
};
using (var call = addressBookClient.GetPersons(getPersonRequest))
{
    while (await call.ResponseStream.MoveNext(cancellationtokensource.Token))
    {
        var person = call.ResponseStream.Current;
        Console.WriteLine($"{person.Name} {person.Address.City} {person.Address.Street}");
        ProcessPerson(person);
    }
}

Console.ReadLine();

async Task ProcessPerson(Person person)
{
    await Task.Delay(2000);
    Console.WriteLine($"{person.Name} processed");
}

