namespace RMR_projekt;

using RMR_projekt.ViewModels;


public partial class StatistikePage : ContentPage
{

    public int ID { get; set; }

    public StatistikePage()
    {
        var userId = Preferences.Get("userId", -1);
        ID = userId;
        InitializeComponent();

        // Retrieve the saved preferences
        bool lightModeEnabled = Preferences.Get("LightModeEnabled", false);
        bool darkModeEnabled = Preferences.Get("DarkModeEnabled", false);

        // Set the initial background color based on preferences
        if (darkModeEnabled == true)
        {
            this.BackgroundColor = Colors.Black;
            Backbtn.Background = Colors.Gray;
            Backbtn.TextColor = Colors.Black;
            Backbtn.ImageSource = "images2.jpg";
        }
        else
        {
            this.BackgroundColor = Colors.White;
            Backbtn.Background = Colors.White;
            Backbtn.TextColor = Colors.Gray;
            Backbtn.ImageSource = "images.jpg";
        }
        bool Language = Preferences.Get("Language", false);

        if (Language == true)
        {
            Graf.Title = "Statistika porabe kalorij na trening";

        }
        else
        {
            Graf.Title = "Statistics of usage of calories per training";
        }

        ApiConnect();
    }


    private async Task ApiConnect()
    {
        TreningViewModel treningData = new TreningViewModel();
        await treningData.ApiConnect();

        CS.ItemsSource = treningData.Treningi.Where(x => x.tk_uporabnik == ID);
    }

    private async void Backbtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}