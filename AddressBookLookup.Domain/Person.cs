using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookLookup.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }

        public Person(int id, string name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
    }
}
