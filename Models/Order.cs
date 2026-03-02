namespace BarKeep.Models;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<CartItem> Items { get; set; } = new();
    public DateTime PlacedAt { get; set; } = DateTime.Now;
    public string CustomerName { get; set; } = string.Empty;
    public string? TableNumber { get; set; }
    public decimal Total => Items.Sum(i => i.Subtotal);
    public decimal Tax => Math.Round(Total * 0.08m, 2);
    public decimal GrandTotal => Total + Tax;
}
