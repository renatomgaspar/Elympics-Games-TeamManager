using Elympics_Games.Mobile.ViewModels;

namespace Elympics_Games.Mobile.Views;

public partial class ManageTeamsView : ContentPage
{
	public ManageTeamsView()
	{
		InitializeComponent();
        BindingContext = MauiProgram.ServiceProvider.GetService<ManageTeamsViewModel>();
        Shell.SetNavBarIsVisible(this, false);
    }
}