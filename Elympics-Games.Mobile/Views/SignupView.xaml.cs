using Elympics_Games.Mobile.ViewModels;

namespace Elympics_Games.Mobile.Views;

public partial class SignupView : ContentPage
{
    public SignupView()
    {
        InitializeComponent();
        BindingContext = new SignupViewModel();
        Shell.SetNavBarIsVisible(this, false);
    }
}