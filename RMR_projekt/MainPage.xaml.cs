using Microsoft.Maui.ApplicationModel.Communication;
using Plugin.LocalNotification;

namespace RMR_projekt
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            string title;
            string subtitle;
            string Description;

            bool Language = Preferences.Get("Language", false);

            if (Language == true)
            {
                Naslov.Text = "Vadbeni interval tajmer";
                Welcome.Text = "Dobrodošli!";
                LoginBtn.Text = "Prijava";
                RegisterBtn.Text = "Registracija";
                title = "Telovadba";
                subtitle = "Opomnik";
                Description = "Danes ne pozabi trenirat!";

            }
            else
            {
                Naslov.Text = "Practice Intervalni Timer";
                Welcome.Text = "Welcome!";
                LoginBtn.Text = "Login";
                RegisterBtn.Text = "Register";
                title = "Workout";
                subtitle = "Reminder";
                Description = "Did you work out today?";
            }

            var request = new NotificationRequest
            {
                NotificationId = 1000,
                Title = title,
                Subtitle = subtitle,
                Description = Description,
                BadgeNumber = 42,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(15),
                    NotifyRepeatInterval = TimeSpan.FromHours(12),
                }
            };
            LocalNotificationCenter.Current.Show(request);

            // Retrieve the saved preferences
            bool lightModeEnabled = Preferences.Get("LightModeEnabled", false);
            bool darkModeEnabled = Preferences.Get("DarkModeEnabled", false);

            // Set the initial background color based on preferences
            if (darkModeEnabled == true)
            {
                this.BackgroundColor = Colors.Black; // PaleVioletRed in Hex
                Naslov.TextColor = Colors.Black;
                Welcome.TextColor = Colors.Black;
                LoginBtn.BackgroundColor = Colors.Gray;
                LoginBtn.TextColor = Colors.Black;
                RegisterBtn.BackgroundColor = Colors.Gray;
                RegisterBtn.TextColor = Colors.Black;
            }
            else
            {
                this.BackgroundColor = Colors.White;
                Naslov.TextColor = Colors.White;
                Welcome.TextColor = Colors.White;
                LoginBtn.BackgroundColor = Colors.White;
                LoginBtn.TextColor = Colors.Gray;
                RegisterBtn.BackgroundColor = Colors.White;
                RegisterBtn.TextColor = Colors.Gray;
            }

        }

        private async void Register_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new RegisterPage());

        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new LoginPage());
        }



    }

}
