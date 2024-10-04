﻿using AddressBookLookupDomain;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Protos;

namespace FindPersonService.Services
{
    public class AddressBookLookupService : AddressBookLookup.AddressBookLookupBase
    {
        private readonly ILogger<AddressBookLookupService> _logger;
        private readonly IRepo<AddressBookLookupDomain.Model.Person> _personRepo;

        public AddressBookLookupService(ILogger<AddressBookLookupService> logger, IRepo<AddressBookLookupDomain.Model.Person> personRepo)
        {
            _logger = logger;
            _personRepo = personRepo;
        }

        public override Task<AddressBook> GetAddressBook(GetAddressBookRequest request, ServerCallContext context)
        {
            return Task.FromResult(GetAddresssBook());
        }

        public override async Task GetPersons(GetPersonsRequest request, IServerStreamWriter<Person> responseStream, ServerCallContext context)
        {
            foreach (var person in _personRepo
                .GetAll()
                .Select(p => ConvertToProto(p))
                .Where(p => Match(p, request.Person, request.FieldMask)))
                await responseStream.WriteAsync(person);
        }

        private AddressBook GetAddresssBook()
        {
            var addressBook = new AddressBook();
            foreach (var person in _personRepo.GetAll().Select(p => ConvertToProto(p)))
                addressBook.Persons.Add(person);

            return addressBook;
        }

        private Person ConvertToProto(AddressBookLookupDomain.Model.Person p)
        {
            return new Person
            {
                Name = p.Name,
                Surname = p.Surname,
                Address = new Address
                {
                    City = p.Address.City,
                    HomeNumber = p.Address.HomeNumber,
                    Street = p.Address.Street
                }
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
