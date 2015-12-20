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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WpfChickenApplication.Windows
{
    public partial class MainMenu : Window
    {
        public static Account CurrentAcc;
        string[] images = new string[] { "pack://application:,,,/Resources/Menu/обучалка.png", "pack://application:,,,/Resources/Menu/уровень1.png", "pack://application:,,,/Resources/Menu/уровень2.png", "pack://application:,,,/Resources/Menu/уровень3.png" };
        public MainMenu(Account acc)
        {
            InitializeComponent();
            ColorScheme.GetColorScheme(this);
            CurrentAcc = acc;
            accButton.Content = acc.Name;
        }
        private void levelButton_MouseEnter(object sender, MouseEventArgs e)
        {
            chickenImage.Source = new BitmapImage(new Uri(images[Convert.ToInt16(((Button)sender).Name[5])-48]));
        }
        private void accButton_Click(object sender, RoutedEventArgs e)
        {
            MyAccount my = new MyAccount(CurrentAcc);
            my.Show();
        }
        private void level0Button_Click(object sender, RoutedEventArgs e)
        {
            Alphabet al = new Alphabet();
            al.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LoginWindow.Account_List[LoginWindow.Account_List.IndexOf(CurrentAcc)] = CurrentAcc;
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream("acc_base.bin", FileMode.OpenOrCreate);
            formatter.Serialize(fs, LoginWindow.Account_List);
        }
        private void level1Button_Click(object sender, RoutedEventArgs e)
        {
            Level1Selecter ls = new Level1Selecter();
            ls.Show();
        }
    }
}
