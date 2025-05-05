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
        try
        {
            var products = await _productService.GetAllProductsAsync();
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(products);
            return response;
        }
        catch (Exception ex)
        {
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync($"An error occurred: {ex.Message}");
            return errorResponse;
        }
    }

    [Function("GetProductsById")]
    public async Task<HttpResponseData> GetProductsById(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "products/{id:int}")] HttpRequestData req, int id)
    {
        try
        {
            var products = await _productService.GetProductByIdAsync(id);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(products);
            return response;
        }
        catch (Exception ex)
        {
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync($"An error occurred: {ex.Message}");
            return errorResponse;
        }
    }

    [Function("AddProduct")]
    public async Task<HttpResponseData> AddProduct(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "products")] HttpRequestData req)
    {

        try
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
        catch (Exception ex)
        {
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync($"An error occurred: {ex.Message}");
            return errorResponse;
        }

    }

    [Function("UpdateProduct")]
    public async Task<HttpResponseData> UpdateProduct(
            [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "products")] HttpRequestData req)
    {

        try
        {
            var product = await JsonSerializer.DeserializeAsync<Product>(req.Body);
            if (product == null)
            {
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequest.WriteStringAsync("Invalid product data.");
                return badRequest;
            }

            await _productService.UpdateProductAsync(product);
            var response = req.CreateResponse(HttpStatusCode.Created);
            return response;
        }
        catch (Exception ex)
        {
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync($"An error occurred: {ex.Message}");
            return errorResponse;
        }

    }

}