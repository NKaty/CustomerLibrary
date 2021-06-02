using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace CustomerLibrary
{
    public static class AddressValidator
    {
        public static List<string> Validate(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("Address object is null.");
            }

            var results = new List<ValidationResult>();
            var context = new ValidationContext(address);
            Validator.TryValidateObject(address, context, results, true);
            return results.Select(r => r.ErrorMessage).ToList();
        }
    }
}
