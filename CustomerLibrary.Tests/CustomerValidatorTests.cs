using System.Collections.Generic;
using Xunit;
using FluentValidation.TestHelper;

namespace CustomerLibrary.Tests
{
    public class CustomerValidatorTests
    {
        [Fact]
        public void ShouldNotBeErrorsIfCustomerIsValid()
        {
            Address address1 = new Address()
            {
                AddressLine = "75 PARK PLACE",
                AddressLine2 = "45 BROADWAY",
                AddressType = AddressTypes.Shipping,
                City = "New York",
                Country = "United States",
                State = "New York",
                PostalCode = "12345"
            };
            Address address2 = new Address()
            {
                AddressLine = "100 PARK PLACE",
                AddressLine2 = "866 BROADWAY",
                AddressType = AddressTypes.Billing,
                City = "Some city",
                Country = "Canada",
                State = "Some state",
                PostalCode = "34588"
            };
            Customer customer = new Customer()
            {
                FirstName = "Bob",
                LastName = "Smith",
                Addresses = new List<Address>() { address1, address2 },
                Email = "bob@gmail.com",
                PhoneNumber = "+1234455",
                Notes = new List<string> { "Note" },
                TotalPurchasesAmount = 100.84M
            };

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void ShouldReturnListOfErrorsIfCustomerIsNotValid()
        {
            Customer customer = new Customer();

            var validator = new CustomerValidator();
            var result = validator.Validate(customer);
            Assert.Equal(3, result.Errors.Count);
        }

        [Fact]
        public void ShouldReturnListOfErrorsOfCustomerWithAddressErrorsIfAddressIsNotValid()
        {
            Customer customer = new Customer() { Addresses = new List<Address>() { new Address() } };

            var validator = new CustomerValidator();
            var result = validator.Validate(customer);
            Assert.Equal(8, result.Errors.Count);
        }

        [Fact]
        public void ShouldFirstNameThrowMaxLengthError()
        {
            Customer customer = new Customer() { FirstName = "Bob Bob Bob Bob Bob Bob Bob BobBob Bob Bob BobBob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob Bob BobBob Bob Bob Bob Bob Bob Bob Bob" };

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.FirstName).WithErrorCode("MaximumLengthValidator");
        }

        [Fact]
        public void ShouldFirstNameBeValid()
        {
            Customer customer = new Customer() { FirstName = "Bob" };

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldNotHaveValidationErrorFor(customer => customer.FirstName);
        }

        [Fact]
        public void ShouldFirstNameBeValidWithoutValue()
        {
            Customer customer = new Customer();

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldNotHaveValidationErrorFor(customer => customer.FirstName);
        }

        [Fact]
        public void ShouldLastNameThrowRequiredError()
        {
            Customer customer = new Customer();

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.LastName).WithErrorCode("NotNullValidator");
        }

        [Fact]
        public void ShouldLastNameThrowEmptyError()
        {
            Customer customer = new Customer() { LastName = "" };

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.LastName).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void ShouldLastNameThrowMaxLengthError()
        {
            Customer customer = new Customer() { LastName = "Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith Smith" };

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.LastName).WithErrorCode("MaximumLengthValidator");
        }

        [Fact]
        public void ShouldLastNameBeValid()
        {
            Customer customer = new Customer() { LastName = "Smith" };

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldNotHaveValidationErrorFor(customer => customer.LastName);
        }

        [Fact]
        public void ShouldAddressesThrowRequiredError()
        {
            Customer customer = new Customer();

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.Addresses).WithErrorCode("NotNullValidator");
        }

        [Fact]
        public void ShouldAddressesThrowMinLengthError()
        {
            Customer customer = new Customer() { Addresses = new List<Address>() };

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.Addresses).WithErrorCode("PredicateValidator");
        }

        [Fact]
        public void ShouldAddressesBeValid()
        {
            Customer customer = new Customer() { Addresses = new List<Address>() { new Address() } };

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldNotHaveValidationErrorFor(customer => customer.Addresses);
        }

        [Fact]
        public void ShouldPhoneNumberThrowIncorrectPhoneError()
        {
            Customer customer = new Customer() { PhoneNumber = "456565" };

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.PhoneNumber).WithErrorCode("RegularExpressionValidator");
        }

        [Fact]
        public void ShouldPhoneNumberBeValid()
        {
            Customer customer = new Customer() { PhoneNumber = "+123456789" };

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldNotHaveValidationErrorFor(customer => customer.PhoneNumber);
        }

        [Fact]
        public void ShouldPhoneNumberBeValidWithoutValue()
        {
            Customer customer = new Customer();

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldNotHaveValidationErrorFor(customer => customer.PhoneNumber);
        }

        [Fact]
        public void ShouldEmailThrowIncorrectEmailError()
        {
            Customer customer = new Customer() { Email = "bobgmail.com" };

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.Email).WithErrorCode("EmailValidator");
        }

        [Fact]
        public void ShouldEmailBeValid()
        {
            Customer customer = new Customer() { Email = "bob@gmail.com" };

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldNotHaveValidationErrorFor(customer => customer.Email);
        }

        [Fact]
        public void ShouldEmailBeValidWithoutValue()
        {
            Customer customer = new Customer();

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldNotHaveValidationErrorFor(customer => customer.Email);
        }

        [Fact]
        public void ShouldNotesThrowRequiredError()
        {
            Customer customer = new Customer();

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.Notes).WithErrorCode("NotNullValidator");
        }

        [Fact]
        public void ShouldNotesThrowMinLengthError()
        {
            Customer customer = new Customer() { Notes = new List<string>() };

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(customer => customer.Notes).WithErrorCode("PredicateValidator");
        }

        [Fact]
        public void ShouldNotesBeValid()
        {
            Customer customer = new Customer() { Notes = new List<string>() { "note" } };

            var validator = new CustomerValidator();
            var result = validator.TestValidate(customer);
            result.ShouldNotHaveValidationErrorFor(customer => customer.Notes);
        }

        [Fact]
        public void ShouldTotalPurchasesAmountBeValidWithoutValue()
        {
            Customer customer = new Customer();

            Assert.Null(customer.TotalPurchasesAmount);
        }
    }
}
