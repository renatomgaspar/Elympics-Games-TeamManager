using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elympics_Games.Mobile.Models;
using Elympics_Games.Mobile.Services;
using System.Collections.ObjectModel;

namespace Elympics_Games.Mobile.ViewModels
{
    public partial class ShowTeamsViewModel : ObservableObject
    {
        private readonly TeamService _teamService;

        public ObservableCollection<Team> Teams { get; set; } = new ObservableCollection<Team>();

        [ObservableProperty] private string _country;
        [ObservableProperty] private string _name;
        [ObservableProperty] private string _elementsNumber;
        [ObservableProperty] private bool _isBusy;

        public ShowTeamsViewModel(TeamService teamService)
        {
            _teamService = teamService;

            LoadTeamsAsync();
        }

        public async void LoadTeamsAsync()
        {
            IsBusy = true;

            try
            {
                var teamList = await _teamService.GetAllTeamsAsync();

                Teams.Clear();
                foreach (var team in teamList)
                {
                    Teams.Add(team);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading Teams: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task NavigateToHomeAsync()
        {
            await Shell.Current.GoToAsync("//HomeView");
        }
    }
}
