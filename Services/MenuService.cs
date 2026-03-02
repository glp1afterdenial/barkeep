using System.Text.Json;
using BarKeep.Models;

namespace BarKeep.Services;

public class MenuService : IMenuService
{
    private readonly List<MenuItem> _menuItems;

    public MenuService(IWebHostEnvironment env)
    {
        var path = Path.Combine(env.ContentRootPath, "Data", "menu.json");
        var json = File.ReadAllText(path);
        _menuItems = JsonSerializer.Deserialize<List<MenuItem>>(json) ?? new();
    }

    public List<MenuItem> GetAll() =>
        _menuItems.Where(m => m.IsAvailable).ToList();

    public List<MenuItem> GetByCategory(Category category) =>
        _menuItems.Where(m => m.IsAvailable && m.Category == category).ToList();

    public MenuItem? GetById(int id) =>
        _menuItems.FirstOrDefault(m => m.Id == id);

    public List<MenuItem> Search(string query) =>
        _menuItems.Where(m => m.IsAvailable &&
            (m.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
             m.Description.Contains(query, StringComparison.OrdinalIgnoreCase)))
        .ToList();
}
