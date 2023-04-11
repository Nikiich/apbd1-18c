using System.ComponentModel.DataAnnotations;

namespace WareHouse.Models
{
    public class Product
    {
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public override string ToString()
        {
            return $"{IdProduct}, {Name}, {Description}, {Price}";
        }
    }
    
}
