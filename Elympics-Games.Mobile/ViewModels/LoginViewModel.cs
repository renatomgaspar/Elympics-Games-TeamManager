using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elympics_Games.Mobile.DTOs.User;
using Elympics_Games.Mobile.Services;
using System.Xml.Linq;

namespace Elympics_Games.Mobile.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly UserService _userService;
        private readonly PasswordService<AuthUserDto> _passwordService;

        public LoginViewModel(UserService userService, PasswordService<AuthUserDto> passwordService)
        {
            _userService = userService;
            _passwordService = passwordService;
        }

        [ObservableProperty] private string _email;
        [ObservableProperty] private string _password;
        [ObservableProperty] private bool _isBusy;

        [RelayCommand]
        private async Task AuthAsync()
        {
            if (IsBusy)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("❌ Error", "You must write in all the fields!", "OK");
                return;
            }

            IsBusy = true;

            try
            {
                var authDto = new AuthUserDto
                {
                    Email = Email,
                    Password = Password
                };

                var result = await _userService.AuthUserAsync(authDto);

                IsBusy = false;

                if (result.Success)
                {
                    await Shell.Current.DisplayAlert("✅ Success", result.Message, "OK");

                    await Shell.Current.GoToAsync("//HomeView");
                }
                else
                {
                    await Shell.Current.DisplayAlert("❌ Error", result.Message, "OK");
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                IsBusy = false;
            }
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
