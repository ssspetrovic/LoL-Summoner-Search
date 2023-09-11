using System;

namespace LeagueSummonerSearch.DTO;

internal class RankDto
{
    public Uri? EmblemSource { get; set; }
    public string? RankTier { get; set; }
    public string? Stats { get; set; }

    public RankDto(Uri? emblemSource, string? rankTier, string? stats)
    {
        EmblemSource = emblemSource;
        RankTier = rankTier;
        Stats = stats;
    }
}