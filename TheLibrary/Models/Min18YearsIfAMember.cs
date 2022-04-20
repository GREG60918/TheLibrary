using System.ComponentModel.DataAnnotations;
using TheLibrary.DTOs;

namespace TheLibrary.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var customer = (CustomerDTO)validationContext.ObjectInstance;

            if(customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if(customer.BirthDate == null)
            {
                return new ValidationResult("Date of Birth is required");
            }

            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;

            return age >= 18 
                ? ValidationResult.Success 
                : new ValidationResult("Customer should be 18 to go onto a membership");
        }
    }
}
