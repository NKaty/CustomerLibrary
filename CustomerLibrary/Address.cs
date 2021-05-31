namespace CustomerLibrary
{
    public class Address
    {
        public string AddressLine { get; set; }

        public string AddressLine2 { get; set; }

        public AddressTypes? AddressType { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string State { get; set; }

        public string Country { get; set; }
    }
}