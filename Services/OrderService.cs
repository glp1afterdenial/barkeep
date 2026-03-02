using System.Text.Json;
using BarKeep.Models;

namespace BarKeep.Services;

public class OrderService : IOrderService
{
    private const string OrdersKey = "Orders";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session => _httpContextAccessor.HttpContext!.Session;

    private List<Order> LoadOrders()
    {
        var json = Session.GetString(OrdersKey);
        return json is null ? new() : JsonSerializer.Deserialize<List<Order>>(json) ?? new();
    }

    private void SaveOrders(List<Order> orders)
    {
        Session.SetString(OrdersKey, JsonSerializer.Serialize(orders));
    }

    public Order PlaceOrder(List<CartItem> items, string customerName, string? tableNumber)
    {
        var order = new Order
        {
            Items = items,
            CustomerName = customerName,
            TableNumber = tableNumber,
            PlacedAt = DateTime.Now
        };

        var orders = LoadOrders();
        orders.Insert(0, order);
        SaveOrders(orders);

        return order;
    }

    public List<Order> GetOrders() => LoadOrders();

    public Order? GetOrderById(Guid id) =>
        LoadOrders().FirstOrDefault(o => o.Id == id);
}
