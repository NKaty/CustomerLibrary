using System.Collections.Generic;
using Xunit;
using System.ComponentModel.DataAnnotations;

namespace CustomerLibrary.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void ShouldCreateCustomer()
        {
            Address address1 = new Address
            {
                CustomerId = 1,
                AddressId = 1,
                AddressLine = "75 PARK PLACE",
                AddressLine2 = "45 BROADWAY",
                AddressType = AddressTypes.Shipping,
                City = "New York",
                Country = "United States",
                State = "New York",
                PostalCode = "123456"
            };
            Address address2 = new Address
            {
                CustomerId = 1,
                AddressId = 2,
                AddressLine = "100 PARK PLACE",
                AddressLine2 = "866 BROADWAY",
                AddressType = AddressTypes.Billing,
                City = "Some city",
                Country = "Canada",
                State = "Some state",
                PostalCode = "3459"
            };
            Note note = new Note {NoteId = 1, CustomerId = 1, NoteText = "Note1"};
            Customer customer = new Customer
            {
                CustomerId = 1,
                FirstName = "Bob",
                LastName = "Smith",
                Addresses = new List<Address> {address1, address2},
                Email = "bob@gmail.com",
                PhoneNumber = "",
                Notes = new List<Note> {note},
                TotalPurchasesAmount = 100.84M
            };

            Assert.Equal(1, address1.AddressId);
            Assert.Equal(1, address1.CustomerId);
            Assert.Equal("75 PARK PLACE", customer.Addresses[0].AddressLine);
            Assert.Equal("45 BROADWAY", customer.Addresses[0].AddressLine2);
            Assert.Equal(AddressTypes.Shipping, customer.Addresses[0].AddressType);
            Assert.Equal("New York", customer.Addresses[0].City);
            Assert.Equal("United States", customer.Addresses[0].Country);
            Assert.Equal("New York", customer.Addresses[0].State);
            Assert.Equal("123456", customer.Addresses[0].PostalCode);

            Assert.Equal(2, address2.AddressId);
            Assert.Equal(1, address2.CustomerId);
            Assert.Equal("100 PARK PLACE", customer.Addresses[1].AddressLine);
            Assert.Equal("866 BROADWAY", customer.Addresses[1].AddressLine2);
            Assert.Equal(AddressTypes.Billing, customer.Addresses[1].AddressType);
            Assert.Equal("Some city", customer.Addresses[1].City);
            Assert.Equal("Canada", customer.Addresses[1].Country);
            Assert.Equal("Some state", customer.Addresses[1].State);
            Assert.Equal("3459", customer.Addresses[1].PostalCode);

            Assert.Equal(1, note.NoteId);
            Assert.Equal(1, note.CustomerId);
            Assert.Equal("Note1", note.NoteText);

            Assert.Equal(1, customer.CustomerId);
            Assert.Equal("Bob", customer.FirstName);
            Assert.Equal("Smith", customer.LastName);
            Assert.Equal(address1, customer.Addresses[0]);
            Assert.Equal(address2, customer.Addresses[1]);
            Assert.Equal("bob@gmail.com", customer.Email);
            Assert.Equal("", customer.PhoneNumber);
            Assert.Equal(note, customer.Notes[0]);
            Assert.Equal(100.84M, customer.TotalPurchasesAmount);
        }

        [Fact]
        public void ShouldFirstNameThrowMaxLengthError()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "FirstName"};
            bool isValid = Validator.TryValidateProperty(
                "Bob Bob Bob Bob Bob Bob Bob BobBob Bob Bob BobBob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob BobBob Bob Bob Bob Bob Bob Bob Bob",
                context, errors);
            Assert.False(isValid);
            Assert.Equal("First name must be max 50 chars long.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldFirstNameBeValid()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "FirstName"};
            bool isValid = Validator.TryValidateProperty("Bob", context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldFirstNameBeValidWithoutValue()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "FirstName"};
            bool isValid = Validator.TryValidateProperty(null, context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldLastNameThrowRequiredError()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "LastName"};
            bool isValid = Validator.TryValidateProperty(null, context, errors);
            Assert.False(isValid);
            Assert.Equal("Last name is required.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldLastNameThrowMaxLengthError()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "LastName"};
            bool isValid = Validator.TryValidateProperty(
                "Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith",
                context, errors);
            Assert.False(isValid);
            Assert.Equal("Last name must be max 50 chars long.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldLastNameBeValid()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "LastName"};
            bool isValid = Validator.TryValidateProperty("Smith", context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldAddressesThrowRequiredError()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "Addresses"};
            bool isValid = Validator.TryValidateProperty(null, context, errors);
            Assert.False(isValid);
            Assert.Equal("Addresses is required.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldAddressesThrowMinLengthError()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "Addresses"};
            bool isValid = Validator.TryValidateProperty(new List<Address>(), context, errors);
            Assert.False(isValid);
            Assert.Equal("There must be at least one address.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldAddressesBeValid()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "Addresses"};
            bool isValid = Validator.TryValidateProperty(new List<Address>() {new Address()}, context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldPhoneNumberThrowIncorrectPhoneError()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "PhoneNumber"};
            bool isValid = Validator.TryValidateProperty("456565", context, errors);
            Assert.False(isValid);
            Assert.Equal("Incorrect phone number.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldPhoneNumberBeValid()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "PhoneNumber"};
            bool isValid = Validator.TryValidateProperty("+123456789", context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldPhoneNumberBeValidWithoutValue()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "PhoneNumber"};
            bool isValid = Validator.TryValidateProperty(null, context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldEmailThrowIncorrectEmailError()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "Email"};
            bool isValid = Validator.TryValidateProperty("bobgmail.com", context, errors);
            Assert.False(isValid);
            Assert.Equal("Incorrect email.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldEmailBeValid()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "Email"};
            bool isValid = Validator.TryValidateProperty("bob@gmail.com", context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldEmailBeValidWithoutValue()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "Email"};
            bool isValid = Validator.TryValidateProperty(null, context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldNotesThrowRequiredError()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "Notes"};
            bool isValid = Validator.TryValidateProperty(null, context, errors);
            Assert.False(isValid);
            Assert.Equal("Notes is required.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldNotesThrowMinLengthError()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "Notes"};
            bool isValid = Validator.TryValidateProperty(new List<Note>(), context, errors);
            Assert.False(isValid);
            Assert.Equal("There must be at least one note.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldNotesBeValid()
        {
            Customer customer = new Customer();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(customer) {MemberName = "Notes"};
            bool isValid = Validator.TryValidateProperty(new List<Note>() {new Note()}, context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldTotalPurchasesAmountBeValidWithoutValue()
        {
            Customer customer = new Customer();

            Assert.Null(customer.TotalPurchasesAmount);
        }
    }
}