using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace Application.Validations;

[AttributeUsage(AttributeTargets.Property)]
public class AllowedFileFormatsAttribute : ValidationAttribute
{
    private IEnumerable<string> AllowedExtensions { get; set; }

    public AllowedFileFormatsAttribute(params string[] valideTypen)
    {
        AllowedExtensions = valideTypen.Select(c => c.Trim().ToLower());
        ErrorMessage = $"Nur folgende Datei-Typen sind erlaubt: {string.Join(",", AllowedExtensions)}";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IBrowserFile browserFile && !AllowedExtensions.Any(c => browserFile.Name.ToLower().EndsWith(c)))
        {
            return new ValidationResult(ErrorMessage, [validationContext.MemberName]);
        }

        return ValidationResult.Success;
    }
}