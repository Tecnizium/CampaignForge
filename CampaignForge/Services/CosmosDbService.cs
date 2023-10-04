using CampaignForge.Models;
using Microsoft.Azure.Cosmos;

namespace CampaignForge.Services;

public class CosmosDbService : ICosmosDbService
{
    private readonly ILogger<CosmosDbService> _logger;
    private readonly Container _polls;
    
    public CosmosDbService(ILogger<CosmosDbService> logger, IConfiguration configuration)
    {
        _logger = logger;
        var client = new CosmosClient(Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? configuration["CosmosDb:ConnectionString"]!);
        var database = client.GetDatabase(configuration["CosmosDb:DatabaseName"]);
        _polls = database.GetContainer(configuration["CosmosDb:PollsContainerName"]);
    }
    
    //CRUD for PollModel
    
    public Task<List<PollModel>> GetPollsAsyncByCampaignId(string campaignId)
    {
        var pollQueryList = _polls.GetItemLinqQueryable<PollModel>(true).Where(e => e.CampaignId == campaignId).ToList();
        return Task.FromResult(pollQueryList);
    }
    
    public Task<List<PollModel>> GetPollsAsyncByManagerId(string managerId)
    {
        var pollQueryList = _polls.GetItemLinqQueryable<PollModel>(true).Where(e => e.ManagerId == managerId).ToList();

        return Task.FromResult(pollQueryList);
    }
    
    public async Task<PollModel?> GetPollAsync(string id)
    {
        var pollQueryList = _polls.GetItemLinqQueryable<PollModel>(true).Where(e => e.Id == id).ToList();
        var pollQuery = pollQueryList.FirstOrDefault();

        return await Task.FromResult(pollQuery);
    }
    
    public async Task<PollModel> CreatePollAsync(PollModel pollModel)
    {
        await _polls.CreateItemAsync(pollModel);
        return pollModel;
    }
    
    public async Task<PollModel?> UpdatePollAsync(PollModel poll)
    {
        var pollQueryList = _polls.GetItemLinqQueryable<PollModel>(true).Where(e => e.Id == poll.Id).ToList();
        var pollQuery = pollQueryList.FirstOrDefault();
        if (pollQuery != null)
        {
            await _polls.ReplaceItemAsync(poll, poll.Id);
        }
        return await Task.FromResult(poll);
    }
    
    public async Task<bool> DeletePollAsync(string id)
    {
        var pollQueryList = _polls.GetItemLinqQueryable<PollModel>(true).Where(e => e.Id == id).ToList();
        var pollQuery = pollQueryList.FirstOrDefault();
        if (pollQuery != null)
        {
            await _polls.DeleteItemAsync<PollModel>(pollQuery.Id, new PartitionKey(pollQuery.Id));
            return true;
        }
        return false;
    }
    
}