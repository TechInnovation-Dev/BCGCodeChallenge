using System.Security.AccessControl;
using System.Collections.Generic;
using System.Linq;

public class ProductService : IProductService
{
       

    public List<Product> GetProducts()
    {
       List<Product> productsList = FilesReadWrite.ReadFromJsonFile();
        return productsList;
    }

    

    public void AddProduct(IList<Product> products)
    {
        if(products.Count != 0){
            FilesReadWrite.CreateJsonFile(products);
        }
              
    }

   
}