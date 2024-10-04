namespace AddressBookLookupDomain.Model
{
    public class Address
    {
        public string? City { get; }
        public int HomeNumber { get; }
        public string? Street { get; }

        public Address(string? city, int homeNumber, string? street)
        {
            City = city;
            HomeNumber = homeNumber;
            Street = street;
        }
    }
}
