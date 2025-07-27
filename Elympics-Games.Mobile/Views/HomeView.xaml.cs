using Elympics_Games.Mobile.ViewModels;

namespace Elympics_Games.Mobile.Views;

public partial class HomeView : ContentPage
{
	public HomeView()
	{
		InitializeComponent();
        BindingContext = MauiProgram.ServiceProvider.GetService<HomeViewModel>();
        Shell.SetNavBarIsVisible(this, false);
    }
}