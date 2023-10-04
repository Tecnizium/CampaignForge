using System.Security.Claims;
using CampaignForge.Models;
using CampaignForge.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampaignForge.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PollController : Controller
{
    private readonly ILogger<PollController> _logger;
    private readonly ICosmosDbService _cosmosDbService;
    
    public PollController(ILogger<PollController> logger, ICosmosDbService cosmosDbService)
    {
        _logger = logger;
        _cosmosDbService = cosmosDbService;
    }
    
    //CRUD for PollModel
    [HttpGet("GetPollsByCampaign", Name = "GetPollsByCampaign")]
    public async Task<List<PollModel>> GetPollsAsync([FromHeader] string campaignId)
    {
        return await _cosmosDbService.GetPollsAsyncByCampaignId(campaignId);
    }
    [HttpGet("GetPollsByManager", Name = "GetPollsByManager")]
    public async Task<List<PollModel>> GetPollsByManagerAsync()
    {
        var id = User.FindFirst(ClaimTypes.Sid)?.Value;
        return await _cosmosDbService.GetPollsAsyncByManagerId(id!);
    }
    [HttpGet("GetPoll", Name = "GetPoll")]
    public async Task<PollModel?> GetPollAsync([FromHeader] string pollId)
    {
        return await _cosmosDbService.GetPollAsync(pollId);
    }
    [HttpPost("CreatePoll", Name = "CreatePoll")]
    public async Task<PollModel> CreatePollAsync(PollModel pollModel)
    {
        var id = User.FindFirst(ClaimTypes.Sid)?.Value;
        pollModel.ManagerId = id;
        return await _cosmosDbService.CreatePollAsync(pollModel);
    }
    [HttpPut("UpdatePoll", Name = "UpdatePoll")]
    public async Task<PollModel?> UpdatePollAsync(PollModel pollModel)
    {
        return await _cosmosDbService.UpdatePollAsync(pollModel);
    }
    [HttpDelete("DeletePoll/{pollId}", Name = "DeletePoll")]
    public async Task<bool> DeletePollAsync(string pollId)
    {
        return await _cosmosDbService.DeletePollAsync(pollId);
    }
    
}