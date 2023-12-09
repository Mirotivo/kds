public class CategoryService : ICategoryService
{
    private readonly DB _db;

    public CategoryService()
    {
        _db = new DB();
    }
    public List<Category> GetCategories()
    {
        var categories = _db.ListCategories();
        return categories;
    }

}
