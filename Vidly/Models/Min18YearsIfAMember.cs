using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer) validationContext.ObjectInstance;

            if (customer.MembershipTypeId == Customer.Unknown || customer.MembershipTypeId == Customer.PayasYouGo)
            {
                return ValidationResult.Success;
            }

            if (customer.Birthdate == null)
            {
                return new ValidationResult("Birthdate is required.");
            }

            if ((DateTime.Now - customer.Birthdate.Value).TotalDays < 365*18)
            {
                return new ValidationResult("You must be at least 18 years of age for a membership.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}