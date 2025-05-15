using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.Author;

namespace Application.Validations;

[AttributeUsage(AttributeTargets.Property)]
public class NoFutureDateAttribute : ValidationAttribute
{
    public NoFutureDateAttribute()
    {
        if (string.IsNullOrEmpty(ErrorMessage))
            ErrorMessage = "Datum liegt in der Zukunft";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        //var vm = (AuthorForCreateViewModel) validationContext.ObjectInstance;
        
        if (value is DateOnly dateToCheck)
        {
            if (dateToCheck > DateOnly.FromDateTime(DateTime.Today))
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}