using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Szotar.Models;
using Windows.UI.Popups;

namespace Szotar.Service
{
    public class TranslateService
    {
        private readonly Uri serverUrl = new Uri("https://dictionary.yandex.net");
        private readonly string ApiKey = "dict.1.1.20200513T134106Z.c397f62b0847b06f.eaf71c168dc657d9ab1157908fe45c9c4971c943";

        // Egy adott szó fordítását kéri le az APIból aszinkron módon, így amíg megérkezik az adat nem fagy be az alkalmazás.
        public async Task<WordResult> GetWordResultAsync(TranslateData data)
        {
            return await GetAsync<WordResult>(new Uri(serverUrl, "api/v1/dicservice.json/" + "lookup?key=" + ApiKey + "&lang=" + data.FromLanguage + "-" + data.ToLanguage + "&text=" + data.Word));
        }

        // Az összes nyelvpárokat lekéri az API getlangs metódusával.
        public async Task<List<string>> GetLanguagesAsync()
        {
            return await GetAsync<List<string>>(new Uri(serverUrl, "api/v1/dicservice.json/" + "getLangs?key=" + ApiKey));
        }

        // A kérés megvalósítása hibakezeléssel, ha nem megfelelő címet adunk meg, akkor messageDialogban értesít és üres adattal tér vissza
        private async Task<T> GetAsync<T>(Uri uri)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(uri);
                  
                    var json = await response.Content.ReadAsStringAsync();
                    T result = JsonConvert.DeserializeObject<T>(json);
                    return result;
                 
                }
                catch {
                    var messageDialog = new MessageDialog("Http failure response for: " + uri.ToString());
                    string json = "";
                    await messageDialog.ShowAsync();
                    T result = JsonConvert.DeserializeObject<T>(json);
                    return result;
                }
                
            }
        }
    }
}
