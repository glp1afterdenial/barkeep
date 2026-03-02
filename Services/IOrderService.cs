using BarKeep.Models;

namespace BarKeep.Services;

public interface IOrderService
{
    Order PlaceOrder(List<CartItem> items, string customerName, string? tableNumber);
    List<Order> GetOrders();
    Order? GetOrderById(Guid id);
}
