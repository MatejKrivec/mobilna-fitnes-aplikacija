using Newtonsoft.Json;
using RMR_projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMR_projekt.ViewModels
{
    public class TreningViewModel
    {
        public List<Trening> Treningi { get; set; }

        public TreningViewModel()
        {
            Generiraj();
        }

        private async void Generiraj()
        {
            Treningi = new List<Trening>();
            await ApiConnect();
        }

        public async Task ApiConnect()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage responseMessage = await httpClient.GetAsync("http://10.0.2.2:5062/trening"); 

                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseData = await responseMessage.Content.ReadAsStringAsync();

                    Treningi = JsonConvert.DeserializeObject<List<Trening>>(responseData);
                }
                else
                {
                    await Console.Out.WriteLineAsync("Ne deluje");
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
    }
}
