namespace WareHouse.Models;

public class Product_WareHouse
{
    public int IdProductWareHouse { get; set; }
    public int IdWareHouse { get; set; }
    public int IdProduct { get; set; }
    public int IdOrder { get; set; }
    public int Amount { get; set; }
    public double Price { get; set; }
    public DateTime CreatedAt { get; set; }
}