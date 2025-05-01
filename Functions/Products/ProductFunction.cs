using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Text.Json;
using Workspace.Models;

public class ProductFunction
{
    private readonly IProductService _productService;

    public ProductFunction(IProductService productService)
    {
        _productService = productService;
    }

    [Function("GetProducts")]
    public async Task<HttpResponseData> GetProducts(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "products")] HttpRequestData req)
    {
        var products = await _productService.GetAllProductsAsync();
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(products);
        return response;
    }

    [Function("GetProductsById")]
    public async Task<HttpResponseData> GetProductsById(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "products/{id:int}")] HttpRequestData req, int id)
    {
        var products = await _productService.GetProductByIdAsync(id);
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(products);
        return response;
    }

    [Function("AddProduct")]
    public async Task<HttpResponseData> AddProduct(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "products")] HttpRequestData req)
    {
        var product = await JsonSerializer.DeserializeAsync<Product>(req.Body);
        if (product == null)
        {
            var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
            await badRequest.WriteStringAsync("Invalid product data.");
            return badRequest;
        }

        await _productService.AddProductAsync(product);
        var response = req.CreateResponse(HttpStatusCode.Created);
        return response;
    }
}