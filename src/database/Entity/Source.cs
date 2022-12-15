using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace database.Entity;

[Index(nameof(Priority), IsUnique = true)]
public class Source
{
  [Key]
  public short Id { get; set; }

  [Required]
  public string Name { get; set; }

  [Required]
  public short Priority { get; set; }

  [Required]
  public Uri BaseClientUri {get;set;}

  public IList<Quote>? Quotes {get;set;}
}