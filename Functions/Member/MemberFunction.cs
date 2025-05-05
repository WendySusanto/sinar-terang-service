using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Text.Json;
using Workspace.Models;

public class MemberFunction
{
    private readonly IMemberService _MemberService;

    public MemberFunction(IMemberService MemberService)
    {
        _MemberService = MemberService;
    }

    [Function("GetMembers")]
    public async Task<HttpResponseData> GetMembers(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Members")] HttpRequestData req)
    {
        try
        {
            var Members = await _MemberService.GetAllMembersAsync();
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(Members);
            return response;
        }
        catch (Exception ex)
        {
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync($"An error occurred: {ex.Message}");
            return errorResponse;
        }

    }

    [Function("GetMembersById")]
    public async Task<HttpResponseData> GetMembersById(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Members/{id:int}")] HttpRequestData req, int id)
    {
        try
        {
            var Members = await _MemberService.GetMemberByIdAsync(id);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(Members);
            return response;
        }
        catch (Exception ex)
        {
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync($"An error occurred: {ex.Message}");
            return errorResponse;
        }

    }

    [Function("AddMember")]
    public async Task<HttpResponseData> AddMember(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Members")] HttpRequestData req)
    {
        try
        {
            var Member = await JsonSerializer.DeserializeAsync<Member>(req.Body);
            if (Member == null)
            {
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequest.WriteStringAsync("Invalid Member data.");
                return badRequest;
            }

            await _MemberService.AddMemberAsync(Member);
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