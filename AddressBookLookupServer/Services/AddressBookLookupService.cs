using AddressBookLookupDomain.Abstractions;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Protos;
using Domain = AddressBookLookupDomain;

namespace FindPersonService.Services
{
    public class AddressBookLookupService : AddressBookLookup.AddressBookLookupBase
    {
        private readonly Domain.Abstractions.ILogger _logger;
        private readonly IRepo<Domain.Model.Person> _personRepo;

        public AddressBookLookupService(Domain.Abstractions.ILogger logger, IRepo<Domain.Model.Person> personRepo)
        {
            _logger = logger;
            _personRepo = personRepo;
        }

        public override Task<AddressBook> GetAddressBook(GetAddressBookRequest request, ServerCallContext context)
        {
            return Task.FromResult(GetAddressBook());
        }

        public override async Task GetPersons(GetPersonsRequest request, IServerStreamWriter<Person> responseStream, ServerCallContext context)
        {
            _logger.WriteInformation(new Domain.Resources.LogDetail { Message = $"Searching after {request.Person}." });

            foreach (var person in _personRepo
                .GetAll()
                .Select(p => ConvertToProto(p))
                .Where(p => Match(p, request.Person, request.FieldMask)))
                await responseStream.WriteAsync(person);
        }

        private AddressBook GetAddressBook()
        {
            var addressBook = new AddressBook();
            foreach (var person in _personRepo.GetAll().Select(p => ConvertToProto(p)))
                addressBook.Persons.Add(person);

            return addressBook;
        }

        private Person ConvertToProto(Domain.Model.Person p)
        {
            var address = p.Address != null ? new Address
            {
                City = p.Address.City,
                HomeNumber = p.Address.HomeNumber,
                Street = p.Address.Street
            } : null;

            return new Person
            {
                Name = p.Name,
                Surname = p.Surname,
                Address = address
            };
        }

        private bool Match(Person person1, Person person2, FieldMask mask)
        {
            foreach (var path in mask.Paths)
            {
                if (path.Equals("Name", StringComparison.OrdinalIgnoreCase) && !person1.Name.Equals(person2.Name))
                    return false;

                if (path.Equals("Surname", StringComparison.OrdinalIgnoreCase) && !person1.Surname.Equals(person2.Surname))
                    return false;

                if (path.Equals("Address", StringComparison.OrdinalIgnoreCase) && !AreEqual(person1.Address, person2.Address))
                    return false;
            }

            return true;
        }

        private static bool AreEqual(Address addresse1, Address addresse2)
        {
            return
                addresse1.Street.Equals(addresse2.Street) &&
                addresse1.HomeNumber == addresse2.HomeNumber &&
                addresse1.City.Equals(addresse2.City);
        }
    }
}
