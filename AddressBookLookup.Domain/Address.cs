using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookLookup.Domain
{
    public class Address
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }

        public Address(string street, int number, string city)
        {
            Street = street;
            Number = number;
            City = city;
        }
    }
}
