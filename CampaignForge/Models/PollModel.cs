using Newtonsoft.Json;

namespace CampaignForge.Models;

public class PollModel
{
    //PollModel
    [JsonProperty("id")]
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string? ManagerId { get; set; }
    public string? CampaignId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<QuestionModel>? Questions { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public PollStatus Status { get; set; }
    
}

//Enum Poll Status
public enum PollStatus
{
    Open,
    Closed,
    Archived
}
