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
            InitializeComponent();
            ColorScheme.GetColorScheme(this);
            nameLabel.Content = acc.Name;
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
