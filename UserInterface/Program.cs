
using Grpc.Net.Client;
using Protos;

var channel = GrpcChannel.ForAddress("http://localhost:5091");

var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(new HelloRequest { Name = "Tim" });
Console.WriteLine(reply.Message);

var addressBookClient = new AddressBookLookup.AddressBookLookupClient(channel);
var addressBook = addressBookClient.GetAddressBook(new GetAddressBookRequest());


var cancellationtokensource = new CancellationTokenSource();
using (var call = addressBookClient.GetPersons(new GetPersonRequest { Person = new Person { Name = "rsg" } }))
{
    while (await call.ResponseStream.MoveNext(cancellationtokensource.Token))
    {
        var person = call.ResponseStream.Current;
        Console.WriteLine($"{person.Surname} {person.Address.City} {person.Address.Street}");
    }
}

Console.ReadLine();
