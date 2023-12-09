public class ProductService : IProductService
{
    private readonly DB _db;

    public ProductService()
    {
        _db = new DB();
    }
    public ProductPageViewModel GetProducts(string query, int page)
    {
        var products = _db.ListProducts(query);
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
