using System.Security.AccessControl;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
public class ProductService : IProductService
{


    public List<Product> GetProducts()
    {
        List<Product> productsList = FilesReadWrite.ReadFromJsonFile();
        return productsList;
    }



    public void AddProduct(IList<Product> products)
    {
        if (products.Count != 0)
        {
            FilesReadWrite.CreateJsonFile(products);
        }

    }

    public decimal PriceCalculator(IList<int> productIds)
    {
        decimal totalPrice = 0;
        if (productIds == null || productIds?.Count == 0)
        {
            return totalPrice;
        }
        var PODetails = productIds.GroupBy(n => n)
                                   .Select(group => new { Id = group.Key, Count = group.Count() });


        var getProductPrice = (int quantity, Product product) =>
       {
           decimal price = product.Price * quantity;
           return price;
       };
        var getDiscountPrice = (int quantity, Product product) =>
         {
             int balance;
             int discountDiv = Math.DivRem(quantity, product.discount.count, out balance);
             decimal price = discountDiv * product.discount.price;
             if (balance > 0)
             {
                 price += getProductPrice(balance, product);
             }

             return price;
         };
        List<Product> productDetails = GetProducts();
        foreach (var item in PODetails)
        {
            Product product = productDetails.Where(obj => obj.Id == item.Id).FirstOrDefault();
            if (product != null)
            {
                int quantity = item.Count;
                decimal productPrice = product.isDiscountApply ? getDiscountPrice(quantity, product) :
                getProductPrice(quantity, product);
                totalPrice += productPrice;
            }

        }
        return totalPrice;

    }

}