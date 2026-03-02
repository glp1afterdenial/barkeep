using BarKeep.Models;

namespace BarKeep.Services;

public interface ICartService
{
    List<CartItem> GetItems();
    void AddItem(int menuItemId, string name, decimal price, int quantity = 1, string? specialInstructions = null);
    void UpdateQuantity(int menuItemId, int quantity);
    void RemoveItem(int menuItemId);
    void Clear();
    int GetItemCount();
    decimal GetTotal();
}
