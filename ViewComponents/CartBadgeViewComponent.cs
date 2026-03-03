using Microsoft.AspNetCore.Mvc;
using BarKeep.Services;

namespace BarKeep.ViewComponents;

public class CartBadgeViewComponent : ViewComponent
{
    private readonly ICartService _cartService;

    public CartBadgeViewComponent(ICartService cartService)
    {
        _cartService = cartService;
    }

    public IViewComponentResult Invoke()
    {
        var count = _cartService.GetItemCount();
        return View(count);
    }
}
