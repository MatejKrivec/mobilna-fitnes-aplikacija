
using RMR_projekt.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace RMR_projekt;

public partial class KonecPage : ContentPage
{
    private FizioloskiPodatki fizioloskiPodatki = new FizioloskiPodatki("Male", 190, 80);

    public int ID;

    public KonecPage(TimeSpan overalTime, double caloriesBurned, List<ExerciseItem> exerciseList)
    {
        InitializeComponent();

        // Retrieve the saved preferences
        bool lightModeEnabled = Preferences.Get("LightModeEnabled", false);
        bool darkModeEnabled = Preferences.Get("DarkModeEnabled", false);

        // Set the initial background color based on preferences
        if (darkModeEnabled == true)
        {
            this.BackgroundColor = Colors.Black;
            congrats.TextColor = Colors.Black;
            timeLabel.TextColor = Colors.Black;
            caloriesLabel.TextColor = Colors.Black;
            konecBtn.BackgroundColor = Colors.Gray;
            konecBtn.TextColor = Colors.Black;
        }
        else
        {
            this.BackgroundColor = Colors.White;
            congrats.TextColor = Colors.Gray;
            timeLabel.TextColor = Colors.Gray;
            caloriesLabel.TextColor = Colors.Gray;
            konecBtn.BackgroundColor = Colors.White;
            konecBtn.TextColor = Colors.Gray;
        }
        bool Language = Preferences.Get("Language", false);

        if (Language == true)
        {
            congrats.Text = "Cestitke!";
            timeLabel.Text = $"Koncni cas vadbe: {overalTime.ToString("mm':'ss")}";
            caloriesLabel.Text = $"Porabljene kalorije: {Math.Round(caloriesBurned).ToString()}";
            konecBtn.Text = "Nadaljuj";

        }
        else
        {
            congrats.Text = "Congratulations!";
            timeLabel.Text = $"Total Time: {overalTime.ToString("mm':'ss")}";
            caloriesLabel.Text = $"Calories burned: {Math.Round(caloriesBurned).ToString()}";
            konecBtn.Text = "Continue";
        }

        var userId = Preferences.Get("userId", -1);
        ID = userId;

        // Retrieve FizioloskiPodatki from Preferences
        string fizioloskiPodatkiJson = Preferences.Get("FizioloskiPodatki", "");
        if (!string.IsNullOrEmpty(fizioloskiPodatkiJson))
        {
            fizioloskiPodatki = JsonSerializer.Deserialize<FizioloskiPodatki>(fizioloskiPodatkiJson);
        }

        timeLabel.Text = $"Total Time: {overalTime.ToString("mm':'ss")}";
        caloriesLabel.Text = $"Calories burned: {Math.Round(caloriesBurned).ToString()}";
        SendDataToApiAsync(overalTime, caloriesBurned, exerciseList);

    }
    private async void continueButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewPage2());
    }
    private async void SendDataToApiAsync(TimeSpan overalTime, double caloriesBurned, List<ExerciseItem> exerciseList)
    {
        var treningData = new Trening
        {
            id_trening = 0,
            datum = DateTime.Now,
            trajanje = overalTime,
            porabljene_kalorije = (int)Math.Round(caloriesBurned),
            tk_uporabnik = ID
        };

        using (HttpClientHandler handler = new HttpClientHandler())
        {
            //Ignore SSL certificate validation (for testing purposes only)
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            using (HttpClient client = new HttpClient(handler))
            {
                var treningJson = Newtonsoft.Json.JsonConvert.SerializeObject(treningData);
                var content = new StringContent(treningJson, System.Text.Encoding.UTF8, "application/json");

                var treningResponse = await client.PostAsync("http://10.0.2.2:5062/trening", content);
                treningResponse.EnsureSuccessStatusCode();

                var treningId = Newtonsoft.Json.JsonConvert.DeserializeObject<Trening>(await treningResponse.Content.ReadAsStringAsync()).id_trening;
                foreach (var exerciseItem in exerciseList)
                {
                    var vadbaData = new Vadba
                    {
                        id_vadba = 0,
                        naziv = exerciseItem.Name,
                        kalorije = (int)Math.Round(exerciseItem.CalculateCaloriesPoCasu(fizioloskiPodatki)),
                        work = exerciseItem.WorkDuration,
                        rest = exerciseItem.RestDuration,
                        sets = exerciseItem.Sets,
                        tk_trening = treningId
                    };

                    var vadbaJson = Newtonsoft.Json.JsonConvert.SerializeObject(vadbaData);
                    content = new StringContent(vadbaJson, System.Text.Encoding.UTF8, "application/json");

                    var vadbaResponse = await client.PostAsync("http://10.0.2.2:5062/vadba", content);
                    vadbaResponse.EnsureSuccessStatusCode();

                }
            }
        }
    }
}
