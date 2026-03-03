using Microsoft.AspNetCore.Mvc.RazorPages;
using BarKeep.Models;
using BarKeep.Services;

namespace BarKeep.Pages;

public class IndexModel : PageModel
{
    private readonly IMenuService _menuService;

    public IndexModel(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public List<MenuItem> FeaturedItems { get; set; } = new();

    public void OnGet()
    {
        var all = _menuService.GetAll();
        FeaturedItems = all
            .GroupBy(m => m.Category)
            .SelectMany(g => g.Take(1))
            .Take(4)
            .ToList();
    }
}
