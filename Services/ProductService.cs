using Microsoft.EntityFrameworkCore;

public class ProductService : IProductService
{
    private readonly DBContext _db;

    public ProductService(DBContext db)
    {
        _db = db;
    }
    public ProductPageViewModel GetProducts(string query, int page)
    {
        var products = _db.Products
            .Include(prod => prod.Category)
            .Where(c => query == null ||
            c.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase) ||
            c.Description.Contains(query, StringComparison.InvariantCultureIgnoreCase));
        int itemsPerPage = 4;
        int skip = (page - 1) * itemsPerPage;
        var pageProducts = products.Skip(skip).Take(itemsPerPage).ToList();
        int totalPages = (int)Math.Ceiling((double)products.Count() / itemsPerPage);
        var model = new ProductPageViewModel
        {
            CurrentPage = page,
            CurrentPageData = pageProducts,
            TotalPages = totalPages
        };

        return model;
    }

}
