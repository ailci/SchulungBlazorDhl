using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Validations;
using Microsoft.AspNetCore.Components.Forms;

namespace Application.ViewModels.Author;

public class AuthorForCreateViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Bitte geben Sie einen Namen ein")]
    [Length(2, 150, ErrorMessage = "Name ist zu kurz/lang")]
    [DeniedValues(["admin","administrator","root","god"], ErrorMessage = "Der Name ist nicht erlaubt")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Bitte geben Sie eine Beschreibung ein")]
    [MinLength(2, ErrorMessage = "Die Beschreibung ist zu kurz")]
    public required string Description { get; set; }

    [NoFutureDate(ErrorMessage = "Geburtsdatum liegt in der Zukunt")]
    [DataType(DataType.Date)]
    public DateOnly? BirthDate { get; set; }

    [AllowedFileFormats(["jpg","jpeg","png","gif"])]
    public IBrowserFile? Photo { get; set; }
}