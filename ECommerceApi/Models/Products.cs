using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

public class Product
{
    [Required(ErrorMessage = "Product Id is required.")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Product name is required.")]
    public string Name { get; set; }
    
     public bool isDiscountApply { get; set; } = false;
    [Required(ErrorMessage = "Price is required.")]
    public decimal Price { get; set; }
    [CustomRequired("isDiscountApply", ErrorMessage = "The Discount Field is required.")]
    public Discount? discount { get; set; }
     
    
}
public class Discount 
{
   
    
    public int count { get; set; }
    
    public decimal price { get; set; }
}
