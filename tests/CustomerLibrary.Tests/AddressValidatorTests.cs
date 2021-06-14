using Xunit;
using FluentValidation.TestHelper;

namespace CustomerLibrary.Tests
{
    public class AddressValidatorTests
    {
        [Fact]
        public void ShouldNotBeErrorsIfAddressIsValid()
        {
            Address address = new Address()
            {
                AddressLine = "Street",
                AddressType = AddressTypes.Shipping,
                City = "New York",
                Country = "United States",
                State = "New York",
                PostalCode = "123457"
            };

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void ShouldReturnListOfErrorsIfAddressIsNotValid()
        {
            Address address = new Address();

            var validator = new AddressValidator();
            var result = validator.Validate(address);
            Assert.Equal(6, result.Errors.Count);
        }

        [Fact]
        public void ShouldAddressLineThrowRequiredError()
        {
            Address address = new Address();

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.AddressLine).WithErrorCode("NotNullValidator");
        }

        [Fact]
        public void ShouldAddressLineThrowEmptyError()
        {
            Address address = new Address() {AddressLine = ""};

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.AddressLine).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void ShouldAddressLineThrowMaxLengthError()
        {
            Address address = new Address()
            {
                AddressLine =
                    "75 PARK PLACE 75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE"
            };

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.AddressLine).WithErrorCode("MaximumLengthValidator");
        }

        [Fact]
        public void ShouldAddressLineBeValid()
        {
            Address address = new Address() {AddressLine = "75 PARK PLACE"};

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldNotHaveValidationErrorFor(a => a.AddressLine);
        }

        [Fact]
        public void ShouldAddressLine2ThrowMaxLengthError()
        {
            Address address = new Address()
            {
                AddressLine2 =
                    "75 PARK PLACE 75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE"
            };

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.AddressLine2).WithErrorCode("MaximumLengthValidator");
        }

        [Fact]
        public void ShouldAddressLine2BeValid()
        {
            Address address = new Address() {AddressLine2 = "75 PARK PLACE"};

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldNotHaveValidationErrorFor(a => a.AddressLine2);
        }

        [Fact]
        public void ShouldAddressLine2BeValidWithoutValue()
        {
            Address address = new Address();

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldNotHaveValidationErrorFor(a => a.AddressLine2);
        }

        [Fact]
        public void ShouldAddressTypeThrowRequiredError()
        {
            Address address = new Address();

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.AddressType).WithErrorCode("NotNullValidator");
        }

        [Fact]
        public void ShouldAddressTypeBeValid()
        {
            Address address = new Address() {AddressType = AddressTypes.Shipping};

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldNotHaveValidationErrorFor(a => a.AddressType);
        }

        [Fact]
        public void ShouldCityThrowRequiredError()
        {
            Address address = new Address();

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.City).WithErrorCode("NotNullValidator");
        }

        [Fact]
        public void ShouldCityThrowEmptyError()
        {
            Address address = new Address() {City = ""};

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.City).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void ShouldCityThrowMaxLengthError()
        {
            Address address = new Address()
            {
                City =
                    "75 PARK PLACE 75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE"
            };

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.City).WithErrorCode("MaximumLengthValidator");
        }

        [Fact]
        public void ShouldCityBeValid()
        {
            Address address = new Address() {City = "New York"};

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldNotHaveValidationErrorFor(a => a.City);
        }

        [Fact]
        public void ShouldPostalCodeThrowRequiredError()
        {
            Address address = new Address();

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.PostalCode).WithErrorCode("NotNullValidator");
        }

        [Fact]
        public void ShouldPostalCodeThrowEmptyError()
        {
            Address address = new Address() {PostalCode = ""};

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.PostalCode).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void ShouldPostalCodeThrowMaxLengthError()
        {
            Address address = new Address() {PostalCode = "1234567"};

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.PostalCode).WithErrorCode("MaximumLengthValidator");
        }

        [Fact]
        public void ShouldPostalCodeBeValid()
        {
            Address address = new Address() {PostalCode = "12345"};

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldNotHaveValidationErrorFor(a => a.PostalCode);
        }

        [Fact]
        public void ShouldStateThrowRequiredError()
        {
            Address address = new Address();

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.State).WithErrorCode("NotNullValidator");
        }

        [Fact]
        public void ShouldStateThrowEmptyError()
        {
            Address address = new Address() {State = ""};

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.State).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void ShouldStateThrowMaxLengthError()
        {
            Address address = new Address() {State = "ARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE"};

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.State).WithErrorCode("MaximumLengthValidator");
        }

        [Fact]
        public void ShouldStateBeValid()
        {
            Address address = new Address() {State = "New York"};

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldNotHaveValidationErrorFor(a => a.State);
        }

        [Fact]
        public void ShouldCountryThrowRequiredError()
        {
            Address address = new Address();

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.Country).WithErrorCode("NotNullValidator");
        }

        [Fact]
        public void ShouldCountryThrowEmptyError()
        {
            Address address = new Address() {Country = ""};

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.Country).WithErrorCode("NotEmptyValidator");
        }

        [Fact]
        public void ShouldCountryThrowCountryError()
        {
            Address address = new Address() {Country = "Some country"};

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldHaveValidationErrorFor(a => a.Country).WithErrorCode("PredicateValidator");
        }

        [Fact]
        public void ShouldCountryBeValid()
        {
            Address address = new Address() {Country = "Canada"};

            var validator = new AddressValidator();
            var result = validator.TestValidate(address);
            result.ShouldNotHaveValidationErrorFor(a => a.Country);
        }
    }
}