using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elympics_Games.Mobile.Models;
using Elympics_Games.Mobile.Services;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Elympics_Games.Mobile.ViewModels
{
    public partial class ManageTeamsViewModel : ObservableObject
    {
        private readonly TeamService _teamService;

        public ManageTeamsViewModel(TeamService teamService)
        {
            _teamService = teamService;
            LoadTeamsAsync();
        }

        [ObservableProperty] private string teamName;
        [ObservableProperty] string teamCountry;
        [ObservableProperty] private string elementsNumber;
        [ObservableProperty] private ObservableCollection<Team> teams = new();
        [ObservableProperty] private bool _isBusy;

        private Team editingTeam = null;

        [RelayCommand]
        private async void LoadTeamsAsync()
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
        private async Task SaveTeamAsync()
        {
            if (string.IsNullOrWhiteSpace(TeamName) || string.IsNullOrWhiteSpace(TeamCountry) || string.IsNullOrWhiteSpace(ElementsNumber))
            {
                await Shell.Current.DisplayAlert("❌ Error", "You must write in all the fields!", "OK");
                return;
            }

            var team = new Team
            {
                Name = TeamName,
                Country = TeamCountry,
                ElementsNumber = int.TryParse(ElementsNumber, out var num) ? num : 0
            };

            if (IsDuplicateCountry(team) && editingTeam == null)
            {
                await App.Current.MainPage.DisplayAlert("❌ Error", "There is already a Team With that Country!", "OK");
                return;
            }

            bool success = false;

            if (editingTeam == null)
            {
                success = await _teamService.AddTeamAsync(team);
            }
            else
            {
                team.Id = editingTeam.Id;
                success = await _teamService.UpdateTeamAsync(team);
            }

            if (success)
            {
                LoadTeamsAsync();
                await ClearFormAsync();
            }
        }

        [RelayCommand]
        private Task EditTeamAsync(Team team)
        {
            TeamName = team.Name;
            TeamCountry = team.Country;
            ElementsNumber = team.ElementsNumber.ToString();
            editingTeam = team;
            return Task.CompletedTask;
        }

        [RelayCommand]
        private async Task DeleteTeamAsync(Team team)
        {
            var success = await _teamService.DeleteTeamAsync(team.Id);
            if (success)
            {
                LoadTeamsAsync();
                if (editingTeam?.Id == team.Id)
                    await ClearFormAsync();
            }
        }

        private bool IsDuplicateCountry(Team newTeam)
        {
            return Teams.Any(t =>
                t.Country.Equals(newTeam.Country, StringComparison.OrdinalIgnoreCase) &&
                t.Id != newTeam.Id);
        }

            [RelayCommand]
        private Task ClearFormAsync()
        {
            TeamName = TeamCountry = ElementsNumber = string.Empty;
            editingTeam = null;
            return Task.CompletedTask;
        }

        [RelayCommand]
        private async Task NavigateToHomeAsync()
        {
            await Shell.Current.GoToAsync("//HomeView");
        }
    }
}
