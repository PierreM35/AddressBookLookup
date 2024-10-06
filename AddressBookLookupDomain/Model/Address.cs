using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AddressBookLookupDomain.Model
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
        public string City { get; set; }
        public string HomeNumber { get; set; }
        public string Street { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Person> Persons { get; set; }

        public Address()
        {
        }

        public Address(Guid id, string city, string homeNumber, string street)
        {
            Id = id;
            City = city;
            HomeNumber = homeNumber;
            Street = street;
        }
    }
}
