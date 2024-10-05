namespace AddressBookLookupDomain.Model
{
    public class Address
    {
        public Guid Id { get; }
        public string? City { get; }
        public string HomeNumber { get; }
        public string? Street { get; }

        public Address(string? city, string homeNumber, string? street)
        {
            City = city;
            HomeNumber = homeNumber;
            Street = street;
        }
    }
}
