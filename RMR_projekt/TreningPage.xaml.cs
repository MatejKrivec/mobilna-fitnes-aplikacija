namespace RMR_projekt;

public partial class TreningPage : ContentPage
{
	public TreningPage()
	{
		InitializeComponent();
	}

    private async void Zgodovina_Clicked(object sender, EventArgs e)
    {
      //  await Navigation.PushAsync(new ZgodovinaPage());
    }

    private async void Statistika_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StatistikePage());
    }

    private async void Settings_Clicked_2(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingsPage());
    }

    private async void Start_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SetPage());
    }
}