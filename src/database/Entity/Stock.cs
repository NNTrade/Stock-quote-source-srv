using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace database.Entity;

[Index(nameof(Code), IsUnique = true)]
public class Stock
{
  [Key]
  public int Id { get; set; }
  [Required]
  [MaxLength(10)]
  public string Code { get; set; }

  [Required]
  [MaxLength(100)]
  public string Name{get;set;}
  
}