using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerLibrary
{
    [Serializable]
    public abstract class Person
    {
        [StringLength(50, ErrorMessage = "First name must be max 50 chars long.")]
        public abstract string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name must be max 50 chars long.")]
        public abstract string LastName { get; set; }
    }
}