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
        }
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
