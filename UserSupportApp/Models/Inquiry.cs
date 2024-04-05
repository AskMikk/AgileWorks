using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UserSupportApp.Models;

using System.ComponentModel.DataAnnotations;

public class Inquiry
{
    [BindNever] 
    public int Id { get; set; }

    [DisplayName("Kirjeldus")]
    [Required, StringLength(500, MinimumLength = 10)]
    public string Description { get; init; } = string.Empty; 

    [BindNever]
    public DateTime SubmissionTime { get; init; } = DateTime.Now;

    [Required]
    [DisplayName("Lahendamise tähtaeg")]
    public DateTime ResolutionDeadline { get; init; }

    [BindNever]
    public bool IsResolved { get; set; }
}