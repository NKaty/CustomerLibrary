using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace CustomerLibrary
{
    public static class CustomerValidator
    {
        public static List<string> Validate(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException();
            }

            var results = new List<ValidationResult>();
            var context = new ValidationContext(customer);
            Validator.TryValidateObject(customer, context, results, true);
            var errors = results.Select(r => r.ErrorMessage).ToList();

            if (customer.Addresses != null)
            {
                foreach (var address in customer.Addresses)
                {
                    errors.AddRange(AddressValidator.Validate(address));
                }
            }

            return errors;
        }
    }
}
