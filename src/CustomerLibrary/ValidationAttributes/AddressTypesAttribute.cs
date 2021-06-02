using System.ComponentModel.DataAnnotations;

namespace CustomerLibrary.ValidationAttributes
{
    public class AddressTypesAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (value.GetType() == typeof(AddressTypes))
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "Address type must be Shipping or Billing.";
                    return false;
                }
            }
            return true;
        }
    }
}
