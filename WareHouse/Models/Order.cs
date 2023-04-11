using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Server.Kestrel.Https;

namespace WareHouse.Models
{
    public class Order
    {
        [Required] public int IdOrder { get; set; }
        [Required] public int IdProduct { get; set; }
        [Required] public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime FulfilledAt { get; set; }

        public override string ToString()
        {
            return $"{IdOrder}, {IdProduct}, {Amount}, {CreatedAt}";
        }
    }
}