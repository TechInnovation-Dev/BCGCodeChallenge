using System.Collections;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public ActionResult<List<Product>> GetProducts()
    {
        return Ok(_productService.GetProducts());
    }

    
    [HttpPost]
    public ActionResult AddProduct([FromBody] IList<Product> products)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _productService.AddProduct(products);
        return Ok(products);
        
    }

    
}
