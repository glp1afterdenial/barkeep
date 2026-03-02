using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BarKeep.Models;
using BarKeep.Services;

namespace BarKeep.Pages.Orders;

public class ConfirmationModel : PageModel
{
    private readonly IOrderService _orderService;

    public ConfirmationModel(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public Order? Order { get; set; }

    public IActionResult OnGet(Guid id)
    {
        Order = _orderService.GetOrderById(id);
        if (Order is null)
            return RedirectToPage("/Menu/Index");
        return Page();
    }
}
