using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18yearValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MemberShipTypeId == MemberShipType.Unknown || 
                customer.MemberShipTypeId == MemberShipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.BirthDate == null)
                return new ValidationResult("Birth date required");
            var diffYear = DateTime.Today.Year - customer.BirthDate.Value.Year;

            return (diffYear >= 18) ? 
                ValidationResult.Success 
                : new ValidationResult("Minimum 18 years old needed");
        }
    }
}