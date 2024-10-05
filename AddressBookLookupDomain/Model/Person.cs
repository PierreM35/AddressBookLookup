namespace AddressBookLookupDomain.Model
{
    public class Person
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public Address Address { get; }

        public Person(string name, string surname, Address address)
        {
            Name = name;
            Surname = surname;
            Address = address;
        }
    }
}
