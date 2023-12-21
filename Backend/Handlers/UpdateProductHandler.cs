using MediatR;
public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, int>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var productDetails = await _productRepository.GetProductByIdAsync(command.Id);
        if (productDetails == null)
            return default;

        productDetails.Url = command.Url;
        productDetails.Name = command.Name;
        productDetails.ImageUrl = command.ImageUrl;
        productDetails.CategoryID = command.CategoryID;
        productDetails.Title = command.Title;
        productDetails.Description = command.Description;
        productDetails.Price = command.Price;
        productDetails.Category = command.Category;

        return await _productRepository.UpdateProductAsync(productDetails);
    }
}
