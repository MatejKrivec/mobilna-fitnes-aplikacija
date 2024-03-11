using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMR_projekt.ViewModels
{
    class RegistracijaViewModel
    {
        public string weApiKey = "AIzaSyBSJa7APVL7soJkgIbWmNic47bODNgH9xg";
        private INavigation _navigation;
        public Command RegistracijaUporabnika { get; }
        public string email { get; set; }
        public string geslo { get; set; }
        public RegistracijaViewModel(INavigation navigation)
        {
            this._navigation = navigation;
            RegistracijaUporabnika = new Command(RegistracijaUporabnikaTappedAsync);
        }

        private async void RegistracijaUporabnikaTappedAsync(object obj)
        {
            try
            {
                FirebaseAuthProvider ponudnikAvtorizacije = new FirebaseAuthProvider(new FirebaseConfig(weApiKey));
                FirebaseAuthLink auth = await ponudnikAvtorizacije.CreateUserWithEmailAndPasswordAsync(email, geslo);

                string token = auth.FirebaseToken;
                if (token != null)
                {
                    await App.Current.MainPage.DisplayAlert("Obvestilo", "Uporabnik uspešno registriran", "OK");
                    await this._navigation.PopAsync();
                }
            }
            catch (Firebase.Auth.FirebaseAuthException firebaseEx)
            {
                if (firebaseEx.Reason == Firebase.Auth.AuthErrorReason.MissingEmail)
                {
                    await App.Current.MainPage.DisplayAlert("Opozorilo", "Prosim vnesite email.", "OK");
                }
                else if (firebaseEx.Reason == Firebase.Auth.AuthErrorReason.MissingPassword)
                {
                    await App.Current.MainPage.DisplayAlert("Opozorilo", "Prosim vnesite geslo.", "OK");
                }
                else if (firebaseEx.Reason == Firebase.Auth.AuthErrorReason.EmailExists)
                {
                    await App.Current.MainPage.DisplayAlert("Opozorilo", "Email že obstaja.", "OK");
                }
                else if (firebaseEx.Reason == Firebase.Auth.AuthErrorReason.InvalidEmailAddress)
                {
                    await App.Current.MainPage.DisplayAlert("Opozorilo", "Email ni pravilen.", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Opozorilo", "Prišlo je do napake", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Opozorilo", "Prišlo je do napake", "OK");
            }
        }
    }
}
