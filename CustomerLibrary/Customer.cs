using System.Collections.Generic;

namespace CustomerLibrary
{
    public class Customer : Person
    {
        public override string FirstName { get; set; }

        public override string LastName { get; set; }

        public List<Address> Addresses { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public List<string> Notes { get; set; }

        public decimal? TotalPurchasesAmount { get; set; }
    }
}
