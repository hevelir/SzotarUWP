using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Maps;

namespace Szotar.Models
{

    public class Languages
    {
        public List<string> LanguagePairs { get; set; }

        // Dictionary minden nyelvrol es a hozzajuk tartozo nyelvekrol
        public Dictionary<string, List<string>> languageMap;

        //Elkesziti a nyelvekhez tartozó nyelvpárokat (pl. angol nyelvhez milyen fordítások lehetségesek)
        public void makeList() {
            languageMap = new Dictionary<string, List<string>>();
            foreach (string item in LanguagePairs)
            {
                string[] splittedLanguagePairs = item.Split("-");
                string fromLanguage = splittedLanguagePairs[0];
                string toLanguage = splittedLanguagePairs[1];

                if (languageMap.ContainsKey(fromLanguage)) {

                     languageMap[fromLanguage].Add(toLanguage);
                }
                else {
                    languageMap.Add(fromLanguage, new List<string>() { toLanguage });
                }
            }
        }
     
    }

}
