using RMR_projekt.Models;
using System.Text.Json;

namespace RMR_projekt;

public partial class FizioloskiP : ContentPage
{
    private string[] genderOptionsSlovenian = { "Moški", "Ženska" };
    private string[] genderOptionsEnglish = { "Male", "Female" };

    public FizioloskiP()
	{
		InitializeComponent();

        // Load saved FizioloskiPodatki instance from Preferences
        string serializedData = Preferences.Get("FizioloskiPodatki", string.Empty);
        var podatki = JsonSerializer.Deserialize<FizioloskiPodatki>(serializedData);
        if (!string.IsNullOrWhiteSpace(serializedData))
        {
            

            // Set Picker and Slider values based on loaded data
            genderPicker.SelectedItem = podatki.Spol;
            heightSlider.Value = podatki.visina_cm;
            weightSlider.Value = podatki.teza_kg;
        }

        bool darkModeEnabled = Preferences.Get("DarkModeEnabled", false);
        bool Language = Preferences.Get("Language", false);

        if (darkModeEnabled)
        {
            this.BackgroundColor = Colors.Black;
            Backbtn.BackgroundColor = Colors.Gray;
            Backbtn.TextColor = Colors.Black;
            SaveBtn.Background = Colors.Gray;
            SaveBtn.TextColor = Colors.Black;
            Backbtn.ImageSource = "images2.jpg";
        }
        else
        {
            this.BackgroundColor = Colors.White;
            Backbtn.BackgroundColor = Colors.White;
            Backbtn.TextColor = Colors.Gray;
            SaveBtn.Background = Colors.White;
            SaveBtn.TextColor = Colors.Gray;
            Backbtn.ImageSource = "images.jpg";
        }

        if (Language)
        {
            genderPicker.ItemsSource = genderOptionsSlovenian;
            genderPicker.SelectedItem = podatki != null && genderOptionsSlovenian.Contains(podatki.Spol) ? podatki.Spol : genderOptionsSlovenian.FirstOrDefault();

            heightLabel.Text = $"Višina: {podatki.visina_cm} cm"; ;
            weightLabel.Text = $"Teža: {podatki.teza_kg} kg";
            SaveBtn.Text = "Shrani";
        }
        else
        {
            genderPicker.ItemsSource = genderOptionsEnglish;
            genderPicker.SelectedItem = podatki != null && genderOptionsEnglish.Contains(podatki.Spol) ? podatki.Spol : genderOptionsEnglish.FirstOrDefault();

            heightLabel.Text = $"Height: {podatki.visina_cm} cm"; ;
            weightLabel.Text = $"Weight: {podatki.teza_kg} kg";
            SaveBtn.Text = "Save";
        }
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(genderPicker.SelectedItem?.ToString()))
        {
            DisplayAlert("Error", "Please select a gender.", "OK");
            return;
        }

        // Map Slovenian display values to English values
        string selectedGenderDisplay = genderPicker.SelectedItem?.ToString();
        string selectedGenderToSave;

        
        if (selectedGenderDisplay == "Moški")
        {
           selectedGenderToSave = "Male";
        }
        else if (selectedGenderDisplay == "Ženska")
        {
           selectedGenderToSave = "Female";
        }
        else
        { selectedGenderToSave = selectedGenderDisplay;}
       

        // Update the singleton instance with the new data

        FizioloskiPodatki.Instance.Spol = selectedGenderToSave;
        FizioloskiPodatki.Instance.visina_cm = (int)heightSlider.Value;
        FizioloskiPodatki.Instance.teza_kg = (int)weightSlider.Value;

        // Save to Preferences
        Preferences.Set("FizioloskiPodatki", JsonSerializer.Serialize(FizioloskiPodatki.Instance));

        // Navigate back to the previous page or perform other actions
        Navigation.PopAsync();
    }


    private void OnHeightValueChanged(object sender, ValueChangedEventArgs e)
    {
        int heightValue = (int)e.NewValue;
        heightLabel.Text = $"Height: {heightValue} cm";
    }

    private void OnWeightValueChanged(object sender, ValueChangedEventArgs e)
    {
        int weightValue = (int)e.NewValue;
        weightLabel.Text = $"Weight: {weightValue} kg";
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
      //  await Navigation.PopAsync();
        await Navigation.PushAsync(new NewPage2());
    }
}