using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfChickenApplication.Windows
{
    public partial class LetterView : Window
    {
        string alph = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        int currentIndex = 0;
        public LetterView(string s)
        {
            s=s.ToLowerInvariant();
            InitializeComponent();
            ColorScheme.GetColorScheme(this);
            Letter.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Letters/"+s+".png"));
            currentIndex = alph.IndexOf(s[0]);
            if (MainMenu.CurrentAcc.Task_Avalible <= currentIndex + 1 && MainMenu.CurrentAcc.Level_Avalible < 3)
            {
                if (alph.IndexOf(s[0]) != alph.Length - 1) MainMenu.CurrentAcc.Task_Avalible =currentIndex+1;
                else
                {
                    MainMenu.CurrentAcc.Task_Avalible = 1;
                    MainMenu.CurrentAcc.Level_Avalible++;
                }
            }
            player.Source = new Uri("Resources/LetterSounds/" + s.ToUpperInvariant() + ".wav", UriKind.Relative);
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Alphabet a = new Alphabet();
            a.Show();
            this.Close();
        }
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex+1 > 32) return;
            LetterView next = new LetterView(Convert.ToString(alph[currentIndex+1]));
            next.Show();
            this.Close();
        }
        private void play_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Windows.Media.Effects.DropShadowEffect effect = new System.Windows.Media.Effects.DropShadowEffect();
            effect.ShadowDepth = 15;
            ((Image)sender).Effect = effect;
        }
        private void play_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Image)sender).Effect = null;
        }
        private void play_MouseDown(object sender, MouseButtonEventArgs e)
        {
            player.Play();
        }
    }
}
