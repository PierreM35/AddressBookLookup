
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Protos;
using System.Runtime.CompilerServices;

var channel = GrpcChannel.ForAddress("http://localhost:5091");
var addressBookClient = new AddressBookLookup.AddressBookLookupClient(channel);

Console.WriteLine("gRPC AddressBook app");

while (true)
{
    string? answer = GetUserWish();

    if (answer.Equals("1"))
        ListPeople();
    else if (answer.Equals("2"))
        await SearchSpecificPerson();
    else if (answer.Equals("3"))
        break;

    Console.WriteLine();
}

async Task ProcessPerson(Person person)
{
    await Task.Delay(2000);
    Console.WriteLine($"{person.Name} processed");
}

static string? GetUserWish()
{
    Console.WriteLine("Give in your wish: Get all persons from Address book (1), search for specific person (2), exit the app (3)?");
    return Console.ReadLine();
}

async Task SearchSpecificPerson()
{
    var cancellationtokensource = new CancellationTokenSource();
    var getPersonRequest = new GetPersonsRequest
    {
        Person = AskSearchedPerson(),
        FieldMask = GetMask()
    };
    using (var call = addressBookClient.GetPersons(getPersonRequest))
    {
        while (await call.ResponseStream.MoveNext(cancellationtokensource.Token))
        {
            var person = call.ResponseStream.Current;
            ExposePerson(person);
            ProcessPerson(person);
        }
    }
}

FieldMask GetMask()
{
    Console.WriteLine($"What search criteria shall be used?");
    Console.WriteLine($"Give your criterias separated by \",\" 1:name, 2:surname, 3:address");
    var criterias = Console.ReadLine().Split(',');

    var mask = new FieldMask();
    if (criterias.Contains("1"))
        mask.Paths.Add("name");

    if (criterias.Contains("2"))
        mask.Paths.Add("surname");

    if (criterias.Contains("3"))
        mask.Paths.Add("address");

    return mask;
}

Person AskSearchedPerson()
{
    Console.WriteLine($"What person are you looking for?");

    return new Person
    {
        Name = (string?)Get("Name"),
        Surname = (string?)Get("Surname"),
        Address = new Address
        {
            City = (string?)Get("City"),
            Street = (string?)Get("Street"),
            HomeNumber = Get("Home number")
        }
    };
}

void ListPeople()
{
    var addressBook = addressBookClient.GetAddressBook(new GetAddressBookRequest());
    foreach (var person in addressBook.Persons)
        ExposePerson(person);
}

static void ExposePerson(Person person)
{
    Console.WriteLine($"{person.Name} {person.Address.City} {person.Address.Street}");
}

static string Get(string what)
{
    Console.Write($"{what}:");
    return Console.ReadLine();
}