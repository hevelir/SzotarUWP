using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Szotar.Models;
using Szotar.Service;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Globalization;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http.Headers;

namespace Szotar.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        // A forditando szo adatait tartalmazza: magat a szot, milyen nyelvrol milyen nyelvre forditsunk
        public TranslateData NewWord { get; set; }

        public string Word { get { return NewWord.Word; } set { NewWord.Word = value; } }

        public string translatedWord;

        public string TranslatedWord { get { return translatedWord; } set {  translatedWord = value; this.NotifyPropertyChanged(); } }

        public WordResult wordResult;

        //Bindolt adatok, aminek a szerepe a pagen való adatok tárolása, ezek kezelése
       // public WordResult WordResult { get { return this.wordResult; } set {  wordResult = value; this.NotifyPropertyChanged(); } }
        public ObservableCollection<string> Languages { get; set; } = new ObservableCollection<string>(); 
        public ObservableCollection<string> LanguagesToSelectedLanguage { get; set; } = new ObservableCollection<string>();

        //A comboboxba kiválasztot fordítási nyelv
        private string selectedLanguage;

        public string SelectedLanguage { get { return selectedLanguage; } set { selectedLanguage = value; } }

        public Languages lg = new Languages();

        public MainPageViewModel() {
            getLangs();
            NewWord = new TranslateData() { Word = "example", FromLanguage = "en", ToLanguage = "cs" };

        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Felhasználja a TranslateService szolgáltatásait, lekéri a viewmodelbe a nyelveket, majd feltölti a comboboxot a nyelvekkel.
        public async void getLangs()
        {
            var service = new TranslateService();
            var languagesFromJson = await service.GetLanguagesAsync();
            if (languagesFromJson != null)
            {
                //Languages lg = new Languages();
                lg.LanguagePairs = languagesFromJson;

                lg.makeList();
                foreach (KeyValuePair<string, List<string>> entry in lg.languageMap)
                {
                    // Debug.WriteLine("meste: " + entry.Key);
                    Languages.Add(entry.Key);
                }
                selectedLanguage = Languages[0];

                foreach (string item in lg.languageMap[selectedLanguage])
                {
                    LanguagesToSelectedLanguage.Add(item);
                }
            }
        }

        //A fordítandó nyelvhez tartozó nyelvekkel tölti fel a comboboxot, itt lehet majd kiválasztani milyen nyelvre fordítunk
        public void pushPairs() {
            LanguagesToSelectedLanguage.Clear();
            foreach (string item in lg.languageMap[selectedLanguage])
            {
                LanguagesToSelectedLanguage.Add(item);
            }
        }

        // Translate gombnyomásakor hívódik meg. Szerepe az, hogy miután az APIból megérkeztek az adatok, a wordResult tagváltozóba elmenti az értékeket.
        public async Task translate(TranslateData data)
        {
            var service = new TranslateService();
            var wordResults = await service.GetWordResultAsync(data);
            //  Debug.WriteLine(wordResults.Def[0].Tr[0].Text);
            if (wordResults != null && wordResults.Def != null && wordResults.Def.Count != 0)
                TranslatedWord = wordResults.Def[0].Tr[0].Text;
            else { TranslatedWord = "Not found."; }
        }
        
        //Synonym keresésénél használatos függvény, a TranslateService szolgáltatásait használja.
        public async Task getSynonym(TranslateData data) {
            string tolang = data.ToLanguage;
            data.ToLanguage = data.FromLanguage;
            var service = new TranslateService();
            var wordResults = await service.GetWordResultAsync(data);
            if (wordResults != null &&  wordResults.Def != null && wordResults.Def.Count != 0)
                TranslatedWord = wordResults.Def[0].Tr[0].Text;
            else { TranslatedWord = "No synonym found."; }
            data.ToLanguage = tolang;
        }

    }
}
