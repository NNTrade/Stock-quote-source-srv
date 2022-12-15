using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace database.Entity;

public class TimeFrame
{
  [Key]
  public short Id { get; set; }
  [Required]
  [MaxLength(2)]
  public string Code { get; set; }
  [Required]
  [MaxLength(10)]
  public string Name {get;set;}
  [Required]
  public int Seconds {get;set;}

  public static void OnModelCreating(EntityTypeBuilder<TimeFrame> entityBuilder)
  {
    entityBuilder.HasData(new TimeFrame(){Id = 1, Code = "1D", Name = "Day", Seconds = 24*60*60});
    entityBuilder.HasData(new TimeFrame(){Id = 2, Code = "1H", Name = "Hour", Seconds = 60*60});
  }
}