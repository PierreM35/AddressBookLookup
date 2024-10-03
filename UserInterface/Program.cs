﻿
using FindPersonService;
using Grpc.Net.Client;

var channel = GrpcChannel.ForAddress("http://localhost:5091");

var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(new HelloRequest { Name = "Tim" });
Console.WriteLine(reply.Message);

var personClient = new PersonsFinder.PersonsFinderClient(channel);
var cancellationtokensource = new CancellationTokenSource();
using (var call = personClient.FindPersons(new PersonLookup { Name = "rsg" }))
{
    while (await call.ResponseStream.MoveNext(cancellationtokensource.Token))
    {
        var person = call.ResponseStream.Current;
        Console.WriteLine($"{person.Surname} {person.Address.City} {person.Address.Street}");
    }
}

Console.ReadLine();
