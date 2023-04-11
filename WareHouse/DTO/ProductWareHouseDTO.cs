using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WareHouse.DTO;

public class ProductWareHouseDTO
{
    [Required] public int IdProduct { get; set; }
    [Required] public int IdWareHouse { get; set; }
    [Required] public int Amount { get; set; }
    [Required] public DateTime CreatedAt { get; set; }
}