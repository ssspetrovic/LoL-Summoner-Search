using LeagueSummonerSearch.Configuration;
using LeagueSummonerSearch.DTO;
using LeagueSummonerSearch.Model;
using LeagueSummonerSearch.ViewModel;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using static System.Text.Json.JsonSerializer;

namespace LeagueSummonerSearch.Service;

internal class SummonerService
{
    private readonly SummonerInfoViewModel _viewModel;
    private readonly RankService _rankedService;
    private static readonly HttpClient Client = new();

    public SummonerService(SummonerInfoViewModel viewModel)
    {
        _viewModel = viewModel;
        _rankedService = new RankService();
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    private async Task<SummonerInfo> GetSummonerByNameAsync(string? name = Constants.DefaultName, string? region = Constants.DefaultRegion)
    {
        var url = $"https://{region}.api.riotgames.com/lol/summoner/v4/summoners/by-name/{name}?api_key={Constants.ApiKey}";

        var response = await Client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var responseBodyStream = await response.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var summonerInfo = await DeserializeAsync<SummonerInfo>(responseBodyStream, options);
            return summonerInfo ?? throw new Exception("Failed to deserialize request!");
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new Exception("Summoner doesn't exist in selected region.");
        }

        throw new Exception($"{(int)response.StatusCode} - {response.ReasonPhrase}");
    }

    public void ResetSummonerInfo()
    {
        _viewModel.ProfileIconSource = new Uri(Constants.DefaultProfileIconSource);
        _viewModel.Solo = null;
        _viewModel.Flex = null;
        _viewModel.Tft = null;
        _viewModel.Summoner = null;
    }

    public async Task UpdateSummoner()
    {
        ResetSummonerInfo();

        if (string.IsNullOrEmpty(_viewModel.Name))
        {
            MessageBox.Show("Please enter a summoner name first!");
            return;
        }

        try
        {
            await UpdateSummonerInfoAsync();
        }
        catch (HttpRequestException ex)
        {
            HandleHttpRequestException(ex);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}");
        }

        UpdateRanks();
    }

    public void UpdateRanks()
    {
        if (_viewModel.RankInfoTft is not null)
        {
            _viewModel.Tft = RankService.GetRankDto(_viewModel.RankInfoTft);
        }

        if (_viewModel.RankInfos is null) return;

        foreach (var rankInfo in _viewModel.RankInfos)
        {
            switch (rankInfo.QueueType)
            {
                case Constants.SoloQueue:
                    _viewModel.Solo = RankService.GetRankDto(rankInfo);
                    break;
                case Constants.FlexQueue:
                    _viewModel.Flex = RankService.GetRankDto(rankInfo);
                    break;
            }
        }
    }

    private async Task UpdateSummonerInfoAsync()
    {
        var summoner = await GetSummonerByNameAsync(name: _viewModel.Name, region: _viewModel.SelectedRegion ?? Constants.DefaultRegion);
        _viewModel.Summoner = new SummonerDto(summoner.Name, summoner.Level);
        _viewModel.ProfileIconSource = new Uri($"https://raw.communitydragon.org/latest/game/assets/ux/summonericons/profileicon{summoner.ProfileIconId}.png");
        _viewModel.RankInfos = await _rankedService.GetRankById(id: summoner.Id, region: _viewModel.SelectedRegion ?? Constants.DefaultRegion);
        _viewModel.RankInfoTft = await _rankedService.GetTftRankById(id: summoner.Id,
            region: _viewModel.SelectedRegion ?? Constants.DefaultRegion);
    }

    private void HandleHttpRequestException(HttpRequestException ex)
    {
        if (ex.StatusCode == HttpStatusCode.NotFound)
        {
            MessageBox.Show(_viewModel.Summoner is null ? "Summoner doesn't exist in selected region." : "Could not find ranked information about this summoner.");
        }
        else
        {
            MessageBox.Show($"An error occurred: {ex.Message}");
        }
    }
}