using AddressBookLookupDomain.Abstractions;
using AddressBookLookupDomain.Model;
using AddressBookLookupDomain.Resources;
using AddressBookLookupPersistence;
using FindPersonService.Services;
using ILogger = AddressBookLookupDomain.Abstractions.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<IRepo<Person>, PersonRepo>();
builder.Services.AddSingleton<ILogger, Logger>((iServiceProvider) => new Logger(Path.Combine(Path.GetTempPath(), "AddressBookLookupLog.txt")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AddressBookLookupService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
