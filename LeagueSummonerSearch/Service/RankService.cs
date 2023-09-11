using LeagueSummonerSearch.Configuration;
using LeagueSummonerSearch.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using LeagueSummonerSearch.Model;
using static System.Text.Json.JsonSerializer;

namespace LeagueSummonerSearch.Service;

internal class RankService
{
    private static readonly HttpClient Client = new();

    public RankService()
    {
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<RankInfo>> GetRankById(string? id, string? region = Constants.DefaultRegion)
    {
        var url = $"https://{region}.api.riotgames.com/lol/league/v4/entries/by-summoner/{id}?api_key={Constants.ApiKey}";

        var response = await Client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var responseBodyStream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var rankedInfo = await DeserializeAsync<List<RankInfo>>(responseBodyStream, options);
            return rankedInfo ?? throw new Exception("Failed to deserialize request!");
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new Exception("Could not find the ranked information about given summoner.");
        }

        throw new Exception($"{(int)response.StatusCode} - {response.ReasonPhrase}");
    }

    public async Task<RankInfo?> GetTftRankById(string? id, string? region = "eun1")
    {
        var url = $"https://eun1.api.riotgames.com/tft/league/v1/entries/by-summoner/{id}?api_key={Constants.ApiKey}";
        var response = await Client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var responseBodyStream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var rankedInfo = await DeserializeAsync<List<RankInfo>>(responseBodyStream, options);
            if (rankedInfo is null || rankedInfo.Count < 1) return null;

            return rankedInfo![0] ?? throw new Exception("Failed to deserialize request!");
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new Exception("Could not find the ranked information about given summoner.");
        }

        throw new Exception($"{(int)response.StatusCode} - {response.ReasonPhrase}");
    }

    public static RankDto GetRankDto(RankInfo rankedInfo)
    {
        var emblemSource = new Uri($"https://opgg-static.akamaized.net/images/medals_new/{rankedInfo.Tier!.ToLower()}.png");
        var rankTier = $"{rankedInfo.Tier} {rankedInfo.Rank}";
        var stats = $"{rankedInfo.Wins}W/{rankedInfo.Losses}L | {rankedInfo.LeaguePoints} LP";
        return new RankDto(emblemSource, rankTier, stats);
    }
}