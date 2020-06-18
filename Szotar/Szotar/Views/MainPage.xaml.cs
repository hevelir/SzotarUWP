using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Szotar.Models;
using Szotar.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Szotar
{

    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();

        }

       
        private async void translateButton_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.translate(ViewModel.NewWord);
        }

  

        // A következő két függvényben kezeli a comboboxban történő nyelvkiválasztást
        private void inputLang_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedLanguage = inputLang.SelectedItem.ToString();
            ViewModel.NewWord.FromLanguage = inputLang.SelectedItem.ToString();

            ViewModel.pushPairs();
        }

        private void outputLang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (outputLang.SelectedItem != null) { 
    
                ViewModel.NewWord.ToLanguage = outputLang.SelectedItem.ToString();
            }

        }

        private async void SynonymButton_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.getSynonym(ViewModel.NewWord);
        }
    }
}
