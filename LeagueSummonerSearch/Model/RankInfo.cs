namespace LeagueSummonerSearch.Model;

internal class RankInfo
{
    public string? QueueType { get; set; }
    public string? Tier { get; set; }
    public string? Rank { get; set; }
    public int LeaguePoints { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
}