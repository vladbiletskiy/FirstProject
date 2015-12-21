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

    public partial class Alphabet : Window
    {
        string alph = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        public Alphabet()
        {
            InitializeComponent();
            ColorScheme.GetColorScheme(this);
            if (MainMenu.CurrentAcc.Level_Avalible < 1)
                foreach (var element in Board.Children)
                {
                    if (element is Image)
                    {
                        if (alph.IndexOf(((Image)element).Name.ToLowerInvariant()[0]) > MainMenu.CurrentAcc.Task_Avalible)
                        {
                            ((Image)element).Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Backgrounds/lock.png"));
                            ((Image)element).MouseDown -= letter_MouseDown;
                            ((Image)element).MouseDown += locked_MouseDown;
                        }
                    }
                }
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void letter_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Windows.Media.Effects.DropShadowEffect effect = new System.Windows.Media.Effects.DropShadowEffect();
            effect.ShadowDepth = 15;
            ((Image)sender).Effect = effect;
        }
        private void letter_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Image)sender).Effect = null;
        }
        private void letter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LetterView lv = new LetterView(((Image)sender).Name);
            lv.Show();
            this.Close();
        }
        private void locked_MouseDown(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Задание еще закрыто! Пройдите предыдущее задание");
            return;
        }
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainMenu.CurrentAcc.Level_Avalible == 0)
            {
                LetterView lv = new LetterView(alph[MainMenu.CurrentAcc.Task_Avalible].ToString().ToUpperInvariant());
                lv.Show();
            }
            else
            {
                MessageBox.Show("Вы уже прошли все задания данного уровня!");
            }
            this.Close();
        }
    }
}
