using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elympics_Games.Mobile.DTOs.User;
using Elympics_Games.Mobile.Services;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Elympics_Games.Mobile.ViewModels
{
    public partial class SignupViewModel : ObservableObject
    {
        private readonly UserService _userService = new UserService();

        [ObservableProperty] private string _email;
        [ObservableProperty] private string _name;
        [ObservableProperty] private string _password;
        [ObservableProperty] private string _confirmPassword;

        [RelayCommand]
        public async Task CreateUserAsync()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("❌ Error", "You must write in all the fields!", "OK");
                return;
            }

            if (!IsValidEmail(Email))
            {
                await Shell.Current.DisplayAlert("❌ Error", "Invalid email format!", "OK");
                return;
            }

            if (Password != ConfirmPassword)
            {
                await Shell.Current.DisplayAlert("❌ Error", "Password must be the same!", "OK");
                return;
            }

            var newUser = new CreateUserDto
            {
                Name = Name,
                Email = Email,
                Password = Password
            };

            var result = await _userService.CreateUserAsync(newUser);

            if (result.Success)
            {
                await Shell.Current.DisplayAlert("✅ Success", result.Message, "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("❌ Error", result.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task NavigateToLoginAsync()
        {
            await Shell.Current.GoToAsync("//LoginView");
        }

        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
        }
    }
}
