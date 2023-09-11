using LeagueSummonerSearch.Command;
using LeagueSummonerSearch.Configuration;
using LeagueSummonerSearch.DTO;
using LeagueSummonerSearch.Model;
using LeagueSummonerSearch.Service;
using System;
using System.Collections.Generic;

namespace LeagueSummonerSearch.ViewModel;

internal class SummonerInfoViewModel : BaseViewModel
{
    private string? _name;
    public string? Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    private string? _selectedRegion;
    public string? SelectedRegion
    {
        get => _selectedRegion;
        set
        {
            _selectedRegion = value;
            OnPropertyChanged();
        }
    }

    private RankDto? _solo;
    public RankDto? Solo
    {
        get => _solo;
        set
        {
            _solo = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsUnrankedSolo));
        }
    }

    private RankDto? _flex;
    public RankDto? Flex
    {
        get => _flex;
        set
        {
            _flex = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsUnrankedFlex));
        }
    }

    private RankDto? _tft;
    public RankDto? Tft
    {
        get => _tft;
        set
        {
            _tft = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsUnrankedTft));
        }
    }

    private SummonerDto? _summoner;
    public SummonerDto? Summoner
    {
        get => _summoner;
        set
        {
            _summoner = value;
            OnPropertyChanged();
        }
    }

    private Uri? _profileIconSource;
    public Uri? ProfileIconSource
    {
        get => _profileIconSource;
        set
        {
            _profileIconSource = value;
            OnPropertyChanged();
        }
    }

    private bool _isUnrankedSolo;
    public bool IsUnrankedSolo
    {
        get => Solo is null;
        set
        {
            _isUnrankedSolo = value;
            OnPropertyChanged();
        }
    }

    private bool _isUnrankedFlex;
    public bool IsUnrankedFlex
    {
        get => Flex is null;
        set
        {
            _isUnrankedFlex = value;
            OnPropertyChanged();
        }
    }

    private bool _isUnrankedTft;
    public bool IsUnrankedTft
    {
        get => Tft is null;
        set
        {
            _isUnrankedTft = value;
            OnPropertyChanged();
        }
    }

    private readonly SummonerService _summonerService;
    public List<RankInfo>? RankInfos { get; set; }
    public RankInfo? RankInfoTft { get; set; }
    public static List<string> Regions => Constants.Regions;
    public RelayCommand SearchCommand { get; set; }

    public SummonerInfoViewModel()
    {
        _summonerService = new SummonerService(this);
        SearchCommand = new RelayCommand(Execute_SearchCommand);

        _summonerService.ResetSummonerInfo();
        Name = string.Empty;
        SelectedRegion = Regions[1];
    }

    private async void Execute_SearchCommand(object parameter)
    {
        await _summonerService.UpdateSummoner();
    }
}