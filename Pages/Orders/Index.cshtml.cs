using Microsoft.AspNetCore.Mvc.RazorPages;
using BarKeep.Models;
using BarKeep.Services;

namespace BarKeep.Pages.Orders;

public class IndexModel : PageModel
{
    private readonly IOrderService _orderService;

    public IndexModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public List<Order> Orders { get; set; } = new();

    public void OnGet()
    {
        Orders = _orderService.GetOrders();
    }
}
