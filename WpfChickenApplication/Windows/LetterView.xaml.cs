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
            if (alph.IndexOf(s[0]) != alph.Length - 1) MainMenu.CurrentAcc.Task_Avalible=alph.IndexOf(s[0])+1;
            else
            {
                MainMenu.CurrentAcc.Task_Avalible = 1;
                MainMenu.CurrentAcc.Level_Avalible++;
            }
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            LetterView next = new LetterView(Convert.ToString(alph[MainMenu.CurrentAcc.Task_Avalible]));
            next.Show();
            this.Close();
        }
    }
}
