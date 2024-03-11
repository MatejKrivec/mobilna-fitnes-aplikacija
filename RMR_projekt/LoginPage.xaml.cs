using Newtonsoft.Json;
using RMR_projekt.Models;

namespace RMR_projekt;

public partial class LoginPage : ContentPage
{
    public string response1 = string.Empty;

	public LoginPage()
	{
		InitializeComponent();
        // ODKOMENTIRI ZA LOGIN AVTENTIKACIJO
        //   BindingContext = new ViewModels.VpisViewModel(Navigation);

        // Retrieve the saved preferences
        bool lightModeEnabled = Preferences.Get("LightModeEnabled", false);
        bool darkModeEnabled = Preferences.Get("DarkModeEnabled", false);

        // Set the initial background color based on preferences
        if (darkModeEnabled == true)
        {
            this.BackgroundColor = Colors.Black;
            emailEntry.TextColor = Colors.Black;
            passwordEntry.TextColor = Colors.Black;
            PrijavaBtn.BackgroundColor = Colors.Gray;
            PrijavaBtn.TextColor = Colors.Black;
            RegLabel.TextColor = Colors.Blue;
            Backbtn.Background = Colors.Gray;
            Backbtn.TextColor = Colors.Black;
            Backbtn.ImageSource = "images2.jpg";
        }
        else
        {
            this.BackgroundColor = Colors.White;
            emailEntry.TextColor = Colors.White;
            passwordEntry.TextColor = Colors.White;
            PrijavaBtn.BackgroundColor = Colors.White;
            PrijavaBtn.TextColor = Colors.Gray;
            RegLabel.TextColor = Colors.White;
            Backbtn.Background = Colors.White;
            Backbtn.TextColor = Colors.Gray;
            Backbtn.ImageSource = "images.jpg";
        }
        bool Language = Preferences.Get("Language", false);

        if (Language == true)
        {
            emailEntry.Placeholder = "Eposta";
            passwordEntry.Placeholder = "Geslo";
            PrijavaBtn.Text = "Prijava";
            RegLabel.Text = "Registracija";
            response1 = "Napa?ea epošta ali geslo!"; 

        }
        else
        {
            emailEntry.Placeholder = "Email";
            passwordEntry.Placeholder = "Password";
            PrijavaBtn.Text = "Login";
            RegLabel.Text = "Register";
            response1 = "Invalid email or password!";
        }
    }


    public int userID;
    private async void Login_Clicked(object sender, EventArgs e)
    {

        var httpClient = new HttpClient();

        // Get the entered email and password
        var email = emailEntry.Text;
        var password = passwordEntry.Text;

        // Send a GET request to your API endpoint
        // var response = await httpClient.GetAsync($"http://10.0.2.2:5106/uporabnik?email={email}&password={password}"); moj api

        var response = await httpClient.GetAsync($"http://10.0.2.2:5062/uporabnik?email={email}&password={password}");

        // Check if the request was successful (status code 2xx)
        if (response.IsSuccessStatusCode)
        {
            // Parse the response body
            var responseBody = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<Uporabnik>>(responseBody);

            // Check if the user exists
            if (users.Any(u => u.eposta == email && u.geslo == password))
            {
                // The user exists, get the user id
                var userId = users.First(u => u.eposta == email && u.geslo == password).id_uporabnik;

                // Store the user id to preferences
                Preferences.Set("userId", userId);
                // The user exists, navigate to the new page
                await Navigation.PushAsync(new NewPage2());
            }
            else
            {
                // The user does not exist, show an error message
                await DisplayAlert("Error", response1, "OK");
            }
        }
        else
        {
            // The request failed, show an error message
            await DisplayAlert("Error", $"Error: {response.StatusCode} - {response.ReasonPhrase}", "OK");
        }

      //  await Navigation.PushAsync(new NewPage2());
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}