using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BarKeep.Models;
using BarKeep.Services;

namespace BarKeep.Pages.Cart;

public class IndexModel : PageModel
{
    private readonly ICartService _cartService;
    private readonly IMenuService _menuService;

    public IndexModel(ICartService cartService, IMenuService menuService)
    {
        _cartService = cartService;
        _menuService = menuService;
    }

    public List<CartItem> CartItems { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }

    public void OnGet()
    {
        LoadCart();
    }

    public IActionResult OnPostAdd(int menuItemId)
    {
        var menuItem = _menuService.GetById(menuItemId);
        if (menuItem is not null)
        {
            _cartService.AddItem(menuItem.Id, menuItem.Name, menuItem.Price);
            TempData["Toast"] = $"{menuItem.Name} added to cart!";
            TempData["ToastType"] = "success";
        }
        return RedirectToPage("/Menu/Index");
    }

    public IActionResult OnPostUpdate(int menuItemId, int quantity)
    {
        _cartService.UpdateQuantity(menuItemId, quantity);
        return RedirectToPage();
    }

    public IActionResult OnPostRemove(int menuItemId)
    {
        _cartService.RemoveItem(menuItemId);
        TempData["Toast"] = "Item removed from cart.";
        TempData["ToastType"] = "danger";
        return RedirectToPage();
    }

    public IActionResult OnPostClear()
    {
        _cartService.Clear();
        TempData["Toast"] = "Cart cleared.";
        TempData["ToastType"] = "danger";
        return RedirectToPage();
    }

    private void LoadCart()
    {
        CartItems = _cartService.GetItems();
        Subtotal = CartItems.Sum(c => c.Subtotal);
        Tax = Math.Round(Subtotal * 0.08m, 2);
        Total = Subtotal + Tax;
    }
}
