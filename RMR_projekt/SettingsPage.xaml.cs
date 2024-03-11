using Microsoft.Maui.ApplicationModel.Communication;

namespace RMR_projekt;

public partial class SettingsPage : ContentPage
{
    private const string LightModeKey = "LightModeEnabled";
    private const string DarkModeKey = "DarkModeEnabled";





    public bool LightModeEnabled
    {
        get => Preferences.Get(LightModeKey, false);
        set
        {
            Preferences.Set(LightModeKey, value);
            OnPropertyChanged();
        }
    }

    public bool DarkModeEnabled
    {
        get => Preferences.Get(DarkModeKey, false);
        set
        {
            Preferences.Set(DarkModeKey, value);
            OnPropertyChanged();
        }
    }

    private const string LanguageKey = "Language";

    public bool IsSlovenianLanguage
    {
        get => Preferences.Get(LanguageKey, false);
    }

    public SettingsPage()
    {
        InitializeComponent();


        lightModeSwitch.Toggled += OnLightModeToggled;
        darkModeSwitch.Toggled += OnDarkModeToggled;

        UpdateBackgroundColor();

        UpdateLanguage();

        bool lightModeEnabled = Preferences.Get("LightModeEnabled", false);
        bool darkModeEnabled = Preferences.Get("DarkModeEnabled", false);

        // Set the initial background color based on preferences
        if (darkModeEnabled == true)
        {
            SelectLanguageLabel.TextColor = Colors.Black;
            LightModeText.TextColor = Colors.Black;
            DarkModeText.TextColor = Colors.Black;

            lightModeSwitch.ThumbColor = Colors.Black;
            darkModeSwitch.ThumbColor = Colors.Black;

            darkModeSwitch.IsToggled = true;
        }
        else
        {
            SelectLanguageLabel.TextColor = Colors.White;
            LightModeText.TextColor = Colors.White;
            DarkModeText.TextColor = Colors.White;

            lightModeSwitch.ThumbColor = Colors.DarkGray;
            darkModeSwitch.ThumbColor = Colors.DarkGray;

            lightModeSwitch.IsToggled = true;
        }


    }

    private void OnLightModeToggled(object sender, ToggledEventArgs e)
    {
        LightModeEnabled = e.Value;

        // If light mode is enabled, turn off dark mode
        if (LightModeEnabled)
        {
            DarkModeEnabled = false;
            darkModeSwitch.IsToggled = false;
        }

        UpdateBackgroundColor();
    }

    private void OnDarkModeToggled(object sender, ToggledEventArgs e)
    {
        DarkModeEnabled = e.Value;

        // If dark mode is enabled, turn off light mode
        if (DarkModeEnabled)
        {
            LightModeEnabled = false;
            lightModeSwitch.IsToggled = false;
        }

        UpdateBackgroundColor();
    }

    private void UpdateBackgroundColor()
    {
        if (DarkModeEnabled)
        {
            this.BackgroundColor = Colors.Black;
            SelectLanguageLabel.TextColor = Colors.Black;
            LightModeText.TextColor = Colors.Black;
            DarkModeText.TextColor = Colors.Black;

            lightModeSwitch.ThumbColor = Colors.Black;
            darkModeSwitch.ThumbColor = Colors.Black;

            Backbtn.Background = Colors.Gray;
            Backbtn.TextColor = Colors.Black;

            SaveBtn.Background = Colors.Gray;
            SaveBtn.TextColor = Colors.Black;
            Backbtn.ImageSource = "images2.jpg";
        }
        else if (LightModeEnabled)
        {
            this.BackgroundColor = Colors.White;
            SelectLanguageLabel.TextColor = Colors.White;
            LightModeText.TextColor = Colors.White;
            DarkModeText.TextColor = Colors.White;

            lightModeSwitch.ThumbColor = Colors.DarkGray;
            darkModeSwitch.ThumbColor = Colors.DarkGray;

            Backbtn.Background = Colors.White;
            Backbtn.TextColor = Colors.Gray;

            SaveBtn.Background = Colors.White;
            SaveBtn.TextColor = Colors.Gray;

            Backbtn.ImageSource = "images.jpg";
        }

    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewPage2());
    }

    private void UpdateLanguage()
    {
        if (IsSlovenianLanguage)
        {
            // Slovenian language
            SelectLanguageLabel.Text = "Izberite jezik";
            LightModeText.Text = "Svetli način";
            DarkModeText.Text = "Temni način";
        }
        else
        {

            // English language
            SelectLanguageLabel.Text = "Select language";
            LightModeText.Text = "Light mode";
            DarkModeText.Text = "Dark mode";
        }
    }

    private void SL_Button_Clicked(object sender, EventArgs e)
    {
        // Set language to Slovenian
        Preferences.Set(LanguageKey, true);
        UpdateLanguage();
    }

    private void GB_Button_Clicked(object sender, EventArgs e)
    {
        Preferences.Set(LanguageKey, false);
        UpdateLanguage();
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}