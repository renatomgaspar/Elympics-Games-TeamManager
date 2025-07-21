namespace Elympics_Games.Mobile.Views;

public partial class SignupView : ContentPage
{
    public SignupView()
    {
        InitializeComponent();

        Shell.SetNavBarIsVisible(this, false); // Remove NavigationBar
    }

    private async void SignUp_ClickedEvent(object sender, EventArgs e)
    {

    }

    private async void LoginPage_ClickedEvent(object sender, EventArgs e)
    {
        // await Navigation.PushAsync(new LoginView());
    }
}