using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace CustomerLibrary.ValidationAttributes
{
    public class MinLengthListAttribute : ValidationAttribute
    {
        private readonly int _minLength;
        public MinLengthListAttribute(int minLength)
        {
            _minLength = minLength;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                int length = (value as ICollection)?.Count ?? 0;

                if (length < _minLength)
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
