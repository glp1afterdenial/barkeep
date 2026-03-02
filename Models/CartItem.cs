namespace BarKeep.Models;

public class CartItem
{
    public int MenuItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 1;
    public string? SpecialInstructions { get; set; }

    public decimal Subtotal => Price * Quantity;
}
