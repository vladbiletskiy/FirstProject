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
    public partial class MyAccount : Window
    {
        public MyAccount(Account acc)
        {
            int[] total = new int[] { 33, 32, 20 };
            InitializeComponent();
            ColorScheme.GetColorScheme(this);
            nameLabel.Content = acc.Name;
            scoreLabel.Content = "Уровень: " + acc.Level_Avalible + ", задание: " + acc.Task_Avalible;
            double t = 0;
            for (int i = 0; i < acc.Level_Avalible; i++)
            {
                t += total[i];
            }
            t += acc.Task_Avalible;
            totalLabel.Content = Math.Round(((t / (33 + 32 + 20)) * 100)) + "%";
        }
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
