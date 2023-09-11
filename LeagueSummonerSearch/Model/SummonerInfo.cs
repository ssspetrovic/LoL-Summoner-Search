using System.Text.Json.Serialization;

namespace LeagueSummonerSearch.Model;

internal class SummonerInfo
{
    public string? Id { get; set; }
    public string? Puuid { get; set; }
    public string? Name { get; set; }
    public int ProfileIconId { get; set; }

    [JsonPropertyName("summonerLevel")]
    public int Level { get; set; }

    public RankInfo? Solo { get; set; }
    public RankInfo? Flex { get; set; }

    public override string ToString()
    {
        return $"account id: {Id}" + 
               $"puuid: {Puuid}" +
               $"\nname: {Name}" +
               $"\nicon id: {ProfileIconId}" +
               $"\nlevel: {Level}";
    }
}