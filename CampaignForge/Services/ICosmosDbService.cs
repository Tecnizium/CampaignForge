using CampaignForge.Models;

namespace CampaignForge.Services;

public interface ICosmosDbService
{
    //CRUD for PollModel
    Task<List<PollModel>> GetPollsAsyncByCampaignId(string campaignId);
    Task<List<PollModel>> GetPollsAsyncByManagerId(string managerId);
    Task<PollModel?> GetPollAsync(string id);
    Task<PollModel> CreatePollAsync(PollModel pollModel);
    Task<PollModel?> UpdatePollAsync(PollModel pollModel);
    Task<bool> DeletePollAsync(string id);
}