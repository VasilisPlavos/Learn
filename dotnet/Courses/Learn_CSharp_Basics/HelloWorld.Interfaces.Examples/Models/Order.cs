namespace HelloWorld.Interfaces.Examples.Models;

public class Order
{
    public DateTime DatePlaced { get; set; }
    public float TotalPrice { get; set; }

    public bool IsShipped => Shipment != null;
    public Shipment? Shipment { get; set; }
}