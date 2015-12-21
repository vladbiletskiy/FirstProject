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
    public partial class Level1Selecter : Window
    {
        public Level1Selecter()
        {
            InitializeComponent();
            ColorScheme.GetColorScheme(this);
             if (MainMenu.CurrentAcc.Level_Avalible < 2)
                 foreach (var element in Board.Children)
                 {
                     if (element is Image)
                     {
                         if (int.Parse(((Image)element).Name.Substring(6)) > MainMenu.CurrentAcc.Task_Avalible)
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
            Level1 l=new Level1(Convert.ToInt16(((Image)sender).Name.Substring(6)));
            l.Show();
            this.Close();
        }
        private void locked_MouseDown(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Задание еще не открыто! Пройдите предыдущее задание для разблокировки");
            return;
        }
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
