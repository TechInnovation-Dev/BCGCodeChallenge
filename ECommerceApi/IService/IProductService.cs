using System.Collections.Generic;

public interface IProductService
{
    List<Product> GetProducts();
    
    void AddProduct(IList<Product> products);
    decimal PriceCalculator(IList<int> productIds);
    
}