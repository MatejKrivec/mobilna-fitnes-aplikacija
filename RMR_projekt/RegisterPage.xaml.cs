using Microsoft.Maui.ApplicationModel.Communication;
using Newtonsoft.Json;
using RMR_projekt.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace RMR_projekt;

public partial class RegisterPage : ContentPage
{
    public string response1 = string.Empty;
    public string response2 = string.Empty;
    public string response3 = string.Empty;
    public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new ViewModels.RegistracijaViewModel(Navigation);

        // Retrieve the saved preferences
        bool lightModeEnabled = Preferences.Get("LightModeEnabled", false);
        bool darkModeEnabled = Preferences.Get("DarkModeEnabled", false);

        // Set the initial background color based on preferences
        if (darkModeEnabled == true)
        {
            this.BackgroundColor = Colors.Black;
            FirstNameEntry.TextColor = Colors.Black; // Added for FirstNameEntry
            LastNameEntry.TextColor = Colors.Black; // Added for LastNameEntry
            UsernameEntry.TextColor = Colors.Black;
            EmailEntry.TextColor = Colors.Black;
            PasswordEntry.TextColor = Colors.Black;
            ConfirmPasswordEntry.TextColor = Colors.Black;
            RegistrationButton.BackgroundColor = Colors.Gray;
            RegistrationButton.TextColor = Colors.Black;
            Backbtn.Background = Colors.Gray;
            Backbtn.TextColor = Colors.Black;
            Backbtn.ImageSource = "images2.jpg";
        }
        else
        {
            this.BackgroundColor = Colors.White;
            FirstNameEntry.TextColor = Colors.Gray; 
            LastNameEntry.TextColor = Colors.Gray; 
            UsernameEntry.TextColor = Colors.Gray;
            EmailEntry.TextColor = Colors.Gray;
            PasswordEntry.TextColor = Colors.Gray;
            ConfirmPasswordEntry.TextColor = Colors.Gray;
            RegistrationButton.BackgroundColor = Colors.White;
            RegistrationButton.TextColor = Colors.Gray;
            Backbtn.Background = Colors.White;
            Backbtn.TextColor = Colors.Gray;
            Backbtn.ImageSource = "images.jpg";

            
        }

        bool language = Preferences.Get("Language", false);

        if (language)
        {
            FirstNameEntry.Placeholder = "Ime";
            LastNameEntry.Placeholder = "Priimek";
            UsernameEntry.Placeholder = "Uporabniško ime";
            EmailEntry.Placeholder = "Email";
            PasswordEntry.Placeholder = "Geslo";
            ConfirmPasswordEntry.Placeholder = "Potrdi geslo";
            RegistrationButton.Text = "Registracija";
            response1 = "Izpolnite vsa polja";
            response2 = "Prosim vnesite veljavni email naslov";
            response3 = "Gesli se ne ujemata";
        }
        else
        {
            FirstNameEntry.Placeholder = "First Name";
            LastNameEntry.Placeholder = "Last Name";
            UsernameEntry.Placeholder = "Username";
            EmailEntry.Placeholder = "Email";
            PasswordEntry.Placeholder = "Password";
            ConfirmPasswordEntry.Placeholder = "Confirm Password";
            RegistrationButton.Text = "Register";
            response1 = "Please fill out all fields";
            response2 = "Please enter a valid email address.";
            response3 = "Passwords do not match.";
        }

    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void RegistrationButton_Clicked(object sender, EventArgs e)
    {
        var httpClient = new HttpClient();

        Uporabnik uporabnik = new Uporabnik
        {
            ime = FirstNameEntry.Text,
            priimek = LastNameEntry.Text,
            uporabnisko_ime = UsernameEntry.Text,
            eposta = EmailEntry.Text,
            geslo = PasswordEntry.Text

        };

        if (string.IsNullOrWhiteSpace(uporabnik.ime) ||
            string.IsNullOrWhiteSpace(uporabnik.priimek) ||
            string.IsNullOrWhiteSpace(uporabnik.uporabnisko_ime) ||
            string.IsNullOrWhiteSpace(uporabnik.eposta) ||
            string.IsNullOrWhiteSpace(uporabnik.geslo))
        {
            await DisplayAlert("Error", response1, "OK");
            return;
        }

        if (!IsValidEmail(uporabnik.eposta))
        {
            await DisplayAlert("Error", response2, "OK");
            return;
        }

        if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
        {
            await DisplayAlert("Error", response3, "OK");
            return;
        }

        var uporabnikJson = JsonConvert.SerializeObject(uporabnik);
        var content = new StringContent(uporabnikJson, Encoding.UTF8, "application/json");

        // var response = await httpClient.PostAsync("http://10.0.2.2:5106/uporabnikPost", content); moj api
        var response = await httpClient.PostAsync("http://10.0.2.2:5062/uporabnikPost", content);

        // Check if the request was successful (status code 2xx)
        if (response.IsSuccessStatusCode)
        {
           // string responseData = await response.Content.ReadAsStringAsync();
           await Navigation.PushAsync(new LoginPage());
        }
        else
        {
            await DisplayAlert("Error", $"Error: {response.StatusCode} - {response.ReasonPhrase}", "OK");
        }

    }

    // Email validation method using regular expression
    private bool IsValidEmail(string email)
    {
        // This regular expression checks for a basic email format
        string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        return Regex.IsMatch(email, emailPattern);
    }
}