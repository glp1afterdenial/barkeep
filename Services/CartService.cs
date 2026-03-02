using System.Text.Json;
using BarKeep.Models;

namespace BarKeep.Services;

public class CartService : ICartService
{
    private const string CartKey = "Cart";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session => _httpContextAccessor.HttpContext!.Session;

    private List<CartItem> LoadCart()
    {
        var json = Session.GetString(CartKey);
        return json is null ? new() : JsonSerializer.Deserialize<List<CartItem>>(json) ?? new();
    }

    private void SaveCart(List<CartItem> items)
    {
        Session.SetString(CartKey, JsonSerializer.Serialize(items));
    }

    public List<CartItem> GetItems() => LoadCart();

    public void AddItem(int menuItemId, string name, decimal price, int quantity = 1, string? specialInstructions = null)
    {
        var cart = LoadCart();
        var existing = cart.FirstOrDefault(c => c.MenuItemId == menuItemId);
        if (existing is not null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            cart.Add(new CartItem
            {
                MenuItemId = menuItemId,
                Name = name,
                Price = price,
                Quantity = quantity,
                SpecialInstructions = specialInstructions
            });
        }
        SaveCart(cart);
    }

    public void UpdateQuantity(int menuItemId, int quantity)
    {
        var cart = LoadCart();
        var item = cart.FirstOrDefault(c => c.MenuItemId == menuItemId);
        if (item is not null)
        {
            if (quantity <= 0)
                cart.Remove(item);
            else
                item.Quantity = quantity;
            SaveCart(cart);
        }
    }

    public void RemoveItem(int menuItemId)
    {
        var cart = LoadCart();
        cart.RemoveAll(c => c.MenuItemId == menuItemId);
        SaveCart(cart);
    }

    public void Clear()
    {
        Session.Remove(CartKey);
    }

    public int GetItemCount() => LoadCart().Sum(c => c.Quantity);

    public decimal GetTotal() => LoadCart().Sum(c => c.Subtotal);
}
