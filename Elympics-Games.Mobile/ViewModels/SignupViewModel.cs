using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;

namespace Elympics_Games.Mobile.ViewModels
{
    public partial class SignupViewModel : ObservableObject
    {
        [ObservableProperty] private string _email;
        [ObservableProperty] private string _name;
        [ObservableProperty] private string _password;
        [ObservableProperty] private string _confirmPassword;

        [RelayCommand]
        private async Task SignUpAsync()
        {
            
        }

        [RelayCommand]
        private async Task NavigateToLoginAsync()
        {
            // await Shell.Current.GoToAsync("//LoginView");
        }
    }
}
