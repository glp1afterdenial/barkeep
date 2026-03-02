using BarKeep.Models;

namespace BarKeep.Services;

public interface IMenuService
{
    List<MenuItem> GetAll();
    List<MenuItem> GetByCategory(Category category);
    MenuItem? GetById(int id);
    List<MenuItem> Search(string query);
}
