using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace database.Entity;

[Index(nameof(StockId), nameof(SourceId), IsUnique = true)]
public class Quote
{
  [Key]
  public int Id { get; set; }
  [Required]
  public DateTime DateTime {get;set;}

  public int OpenValue { get; set; }
  public int OpenDecimalLen { get; set; }
  public double Open => OpenValue / (10 * OpenDecimalLen);

  public int CloseValue { get; set; }
  public int CloseDecimalLen { get; set; }
  public double Close => CloseValue / (10 * CloseDecimalLen);

  public int HighValue { get; set; }
  public int HighDecimalLen { get; set; }
  public double High => HighValue / (10 * HighDecimalLen);

  public int LowValue { get; set; }
  public int LowDecimalLen { get; set; }
  public double Low => LowValue / (10 * LowDecimalLen);

  public int VolumeValue { get; set; }
  public int VolumeDecimalLen { get; set; }
  public double Volume => VolumeValue / (10 * VolumeDecimalLen);

  [Required]
  public TimeFrame TimeFrame {get;set;}

  [Required]
  public Source Source { get; set; }
  public short SourceId{get;set;}
  
  [Required]
  public Stock Stock {get;set;}
  public int StockId{get;set;}
}
