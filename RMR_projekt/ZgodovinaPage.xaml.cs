using MediaBrowser.Model.Services;
using Newtonsoft.Json;
using RMR_projekt.Models;
using RMR_projekt.ViewModels;
using System.Collections.ObjectModel;
namespace RMR_projekt;

public partial class ZgodovinaPage : ContentPage
{
    public int ID { get; set; }

    public ObservableCollection<Trening> Treningi { get; set; }

    public ZgodovinaPage()
    {
        var userId = Preferences.Get("userId", -1);

        Treningi = new ObservableCollection<Trening>();

        BindingContext = this;

        // Store the user id to a public variable named ID
        ID = userId;



        //Treningi = new ObservableCollection<Trening>();
        InitializeComponent();
        ApiConnect();

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
            Zgodovina.Text = "Zgodovina";

        }
        else
        {
            Zgodovina.Text = "History";
        }
    }

    private async Task ApiConnect()
    {

      /*  var trening1 = new Trening(1, DateTime.Now, TimeSpan.FromMinutes(30), 200, 1);
        var trening2 = new Trening(2, DateTime.Now.AddDays(-1), TimeSpan.FromMinutes(45), 300, 1);
        var trening3 = new Trening(3, DateTime.Now.AddDays(-2), TimeSpan.FromMinutes(60), 400, 1);

        // Add them to the ObservableCollection
        Treningi.Add(trening1);
        Treningi.Add(trening2);
        Treningi.Add(trening3);

        trainingListView.ItemsSource = Treningi;*/

         try
         {
             var httpClient = new HttpClient();
             var response = await httpClient.GetAsync("http://10.0.2.2:5062/trening");

             if (response.IsSuccessStatusCode)
             {
                 string responseData = await response.Content.ReadAsStringAsync();
                 var treningi = JsonConvert.DeserializeObject<List<Trening>>(responseData);

                 // Assuming Treningi is a property in ZgodovinaPage
                 Treningi = new ObservableCollection<Trening>(treningi.Where(t => t.tk_uporabnik == ID));

                 // Add debug output
                 foreach (var item in Treningi)
                 {
                     Console.WriteLine($"Trening item: {item.id_trening}, {item.datum}, {item.trajanje}, {item.porabljene_kalorije}, {item.tk_uporabnik}");
                 }

                 // Update the ListView's ItemsSource on the main thread
                 Device.BeginInvokeOnMainThread(() =>
                 {
                     trainingCollectionView.ItemsSource = Treningi;
                 });
             }
             else
             {
                 await DisplayAlert("Error", $"Error: {response.StatusCode} - {response.ReasonPhrase}", "OK");
             }
         }
         catch (Exception ex)
         {
             await DisplayAlert("Error", $"Exception: {ex.Message}", "OK");
         }
    }

    private async void Backbtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}