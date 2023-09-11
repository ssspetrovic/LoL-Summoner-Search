using System.Collections.Generic;

namespace LeagueSummonerSearch.Configuration;

internal static class Constants
{
    public const string DefaultRegion = "eun1";
    public const string DefaultName = "TheLesserEvil";
    public const string ApiKey = "RGAPI-256006e6-2ecd-4ddc-aa39-cce7e36aee3c";
    public const string SoloQueue = "RANKED_SOLO_5x5";
    public const string FlexQueue = "RANKED_FLEX_SR";
    public const string DefaultProfileIconSource = "https://ddragon-webp.lolmath.net/latest/img/profileicon/29.webp";

    public static readonly List<string> Regions = new()
    {
        "BR1", "EUN1", "EUW1", "JP1", "KR", "LA1", "LA2", "NA1", "OC1", "PH2", "RU", "SG2", "TH2", "TR1", "TW2",
        "VN2"
    };
}