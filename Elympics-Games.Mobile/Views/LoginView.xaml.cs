using Elympics_Games.Mobile.ViewModels;

namespace Elympics_Games.Mobile.Views;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
        BindingContext = MauiProgram.ServiceProvider.GetService<LoginViewModel>();
        Shell.SetNavBarIsVisible(this, false);
    }
}