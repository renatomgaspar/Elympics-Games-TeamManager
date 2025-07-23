using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elympics_Games.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elympics_Games.Mobile.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly UserService _userService = new UserService();

        [ObservableProperty] private string _email;
        [ObservableProperty] private string _password;

        [RelayCommand]
        private async Task AuthAsync()
        {
            
        }

        [RelayCommand]
        private async Task NavigateToSignUpAsync()
        {
            await Shell.Current.GoToAsync("//SignupView");
        }

        [RelayCommand]
        private async Task RecoverAccountAsync()
        {
            // await Shell.Current.GoToAsync("//RecoverAccountView");
        }
    }
}
