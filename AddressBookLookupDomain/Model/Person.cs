using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AddressBookLookupDomain.Model
{
    public class Person
    {
        [Key] 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [ManyToOne]
        public Address Address { get; set; }

        public Person()
        {
        }

        public Person(Guid id, string name, string surname, Address address)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Address = address;
        }
    }
}
