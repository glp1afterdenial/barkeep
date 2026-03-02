using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BarKeep.Models;
using BarKeep.Services;

namespace BarKeep.Pages.Orders;

public class CreateModel : PageModel
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;

    public CreateModel(ICartService cartService, IOrderService orderService)
    {
        _cartService = cartService;
        _orderService = orderService;
    }

    public List<CartItem> CartItems { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Please enter your name.")]
    [StringLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [BindProperty]
    [StringLength(10)]
    public string? TableNumber { get; set; }

    public IActionResult OnGet()
    {
        LoadCart();
        if (CartItems.Count == 0)
            return RedirectToPage("/Cart/Index");
        return Page();
    }

    public IActionResult OnPost()
    {
        LoadCart();
        if (CartItems.Count == 0)
            return RedirectToPage("/Cart/Index");

        if (!ModelState.IsValid)
            return Page();

        var order = _orderService.PlaceOrder(CartItems, CustomerName, TableNumber);
        _cartService.Clear();

        return RedirectToPage("/Orders/Confirmation", new { id = order.Id });
    }

    private void LoadCart()
    {
        CartItems = _cartService.GetItems();
        Subtotal = CartItems.Sum(c => c.Subtotal);
        Tax = Math.Round(Subtotal * 0.08m, 2);
        Total = Subtotal + Tax;
    }
}
