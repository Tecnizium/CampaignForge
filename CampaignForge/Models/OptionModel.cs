using Newtonsoft.Json;

namespace CampaignForge.Models;

public class OptionModel
{
    //OptionModel
    [JsonProperty("id")]
    public string? Text { get; set; }
}