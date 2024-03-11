using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMR_projekt.ViewModels
{
    class VpisViewModel
    {
        public string webApiKey = "AIzaSyBSJa7APVL7soJkgIbWmNic47bODNgH9xg";
        private INavigation _navigation;
        public Command RegistracijaBtn { get; }
        public Command PrijavaBtn { get; }
        public string email { get; set; }
        public string geslo { get; set; }
        public VpisViewModel(INavigation navigation)
        {
            _navigation = navigation;
            RegistracijaBtn = new Command(RegistracijaBtnTappedAsync);
            PrijavaBtn = new Command(PrijavaBtnTappedAsync);
        }

        private async void PrijavaBtnTappedAsync(object obj)
        {
            FirebaseAuthProvider ponudnikAvtorizacije = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            try
            {
                FirebaseAuthLink auth = await ponudnikAvtorizacije.SignInWithEmailAndPasswordAsync(email, geslo);
                FirebaseAuthLink vsebina = await auth.GetFreshAuthAsync();

                var serializiranaVsebina = JsonConvert.SerializeObject(vsebina);
                Preferences.Set("SvezToken", serializiranaVsebina);
                //await this._navigation.PushAsync(new TreningPage());
               // await this._navigation.PushAsync(new NewPage2());
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
                else if (firebaseEx.Reason == Firebase.Auth.AuthErrorReason.InvalidEmailAddress)
                {
                    await App.Current.MainPage.DisplayAlert("Opozorilo", "Nepravilen email ali geslo.", "OK");
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

        private async void RegistracijaBtnTappedAsync(object obj)
        {
            await this._navigation.PushAsync(new RegisterPage());
        }
    }
}
