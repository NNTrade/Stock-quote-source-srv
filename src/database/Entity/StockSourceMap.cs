using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace database.Entity;

[Index(nameof(StockId), nameof(SourceId), IsUnique = true)]
public class StockSourceMap
{
  [Key]
  public int Id { get; set; }

  [Required]
  public string SourceCode { get; set; }

  [Required]
  public Stock Stock { get; set; }
  public int StockId { get; set; }

  [Required]
  public Source Source { get; set; }
  public short SourceId{get;set;}
}