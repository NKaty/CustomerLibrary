using System.ComponentModel.DataAnnotations;

namespace CustomerLibrary.ValidationAttributes
{
    public class CountryAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string country = value.ToString();
                if (country == "United States" || country == "Canada")
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "Country must be United States or Canada.";
                    return false;
                }
            }
            return true;
        }
    }
}
