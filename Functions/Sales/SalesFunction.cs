using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Text.Json;
using Workspace.Models;

public class SalesFunction
{
    private readonly ISalesService _SalesService;

    public SalesFunction(ISalesService SalesService)
    {
        _SalesService = SalesService;
    }

    [Function("GetPagedSales")]
    public async Task<HttpResponseData> GetPagedProducts(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "sales/page")] HttpRequestData req)
    {
        try
        {
            // Parse query parameters for pagination
            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            int pageNumber = int.TryParse(query["pageNumber"], out var pn) ? pn : 1; // Default to page 1
            int pageSize = int.TryParse(query["pageSize"], out var ps) ? ps : 30;   // Default to 30 items per page

            // Get paginated products
            var products = await _SalesService.GetPagedSalesAsync(pageNumber, pageSize);

            // Create response
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

    [Function("GetSales")]
    public async Task<HttpResponseData> GetSales(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "sales")] HttpRequestData req)
    {
        try
        {
            var sales = await _SalesService.GetAllSalessAsync();
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(sales);
            return response;
        }
        catch (Exception ex)
        {
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync($"An error occurred: {ex.Message}");
            return errorResponse;
        }
    }

    [Function("GetSalesById")]
    public async Task<HttpResponseData> GetSalesById(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "sales/{id:int}")] HttpRequestData req, int id)
    {
        try
        {
            var sales = await _SalesService.GetSalesByIdAsync(id);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(sales);
            return response;
        }
        catch (Exception ex)
        {
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync($"An error occurred: {ex.Message}");
            return errorResponse;
        }
    }

    [Function("AddSales")]
    public async Task<HttpResponseData> AddSales(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "sales")] HttpRequestData req)
    {

        try
        {
            var sales = await JsonSerializer.DeserializeAsync<SalesAddDTO>(req.Body);
            if (sales == null)
            {
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequest.WriteStringAsync("Invalid Sales data.");
                return badRequest;
            }

            await _SalesService.AddSalesAsync(sales);
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