using Elympics_Games.Mobile.ViewModels;

namespace Elympics_Games.Mobile.Views;

public partial class ShowTeamsView : ContentPage
{
	public ShowTeamsView()
	{
		InitializeComponent();
        BindingContext = MauiProgram.ServiceProvider.GetService<ShowTeamsViewModel>();
        Shell.SetNavBarIsVisible(this, false);
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (BindingContext is ShowTeamsViewModel vm)
        {
            vm.LoadTeamsAsync();
        }
    }
}