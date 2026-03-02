using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BarKeep.Models;
using BarKeep.Services;

namespace BarKeep.Pages.Menu;

public class IndexModel : PageModel
{
    private readonly IMenuService _menuService;

    public IndexModel(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public List<MenuItem> MenuItems { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public Category? SelectedCategory { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SearchQuery { get; set; }

    public void OnGet()
    {
        if (!string.IsNullOrWhiteSpace(SearchQuery))
        {
            MenuItems = _menuService.Search(SearchQuery);
        }
        else if (SelectedCategory.HasValue)
        {
            MenuItems = _menuService.GetByCategory(SelectedCategory.Value);
        }
        else
        {
            MenuItems = _menuService.GetAll();
        }
    }
}
