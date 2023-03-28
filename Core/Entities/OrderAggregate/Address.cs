namespace Core.Entities.OrderAggregate
{
    public class Address
    {
        // Entity Framework requires a paramterless constructor
        public Address()
        {
        }

        // Constructor 2
        public Address(string firstName, string lastName, string street, string city, string state, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        // No property of Id as this is an owned property of the order entity and is separate
        // It doesn't have it's own table and is part of the order table
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }   
    }
}