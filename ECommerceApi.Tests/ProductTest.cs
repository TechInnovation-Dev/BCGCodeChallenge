namespace ECommerceApi.Tests;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

public class ProductControllerTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly WebApplicationFactory<Startup> _factory;

    public ProductControllerTests(WebApplicationFactory<Startup> factory)
    {
        _factory = factory;
    }

    
    // Add more tests for other CRUD operation
}