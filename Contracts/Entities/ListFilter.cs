using System.ComponentModel.DataAnnotations;
using Services.DbEnum;

namespace Contracts;

public class ListFilter
{
    [Required]
    public Int32 Page { get; set; } = 1;
    [Required]
    public Int32 PageSize { get; set; } = 10;
    [MaxLength(255)]
    public String SearchName { get; set; } = String.Empty;
    public OrderTypeEnum OrderType { get; set; }
}