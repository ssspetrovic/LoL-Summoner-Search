using LeagueSummonerSearch.ViewModel;

namespace LeagueSummonerSearch.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SummonerInfoView
    {
        public SummonerInfoView()
        {
            InitializeComponent();
            DataContext = new SummonerInfoViewModel();
        }
    }
}
