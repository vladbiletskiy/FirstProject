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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WpfChickenApplication.Windows;

namespace WpfChickenApplication
{
    public partial class LoginWindow : Window
    {
        public static AccountList Account_List;
        public LoginWindow()
        {
            InitializeComponent();
            ColorScheme.GetColorScheme(this);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream("acc_base.bin", FileMode.OpenOrCreate);
            try
            {
                Account_List = (AccountList)formatter.Deserialize(fs);
            }
            catch
            {
                Account_List = new AccountList();
            }
            finally
            {
                fs.Close();
            }
            foreach (var element in Account_List) nameBox.Items.Add(element.Name);
        }
        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Выйти из программы?", "Выход", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                Application.Current.Shutdown();
        }
        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow rf = new RegisterWindow();
            this.Hide();
            rf.ShowDialog();
            this.Show();
            if (Account_List.Count!=0&&!nameBox.Items.Contains(Account_List.Last().Name)) nameBox.Items.Add(Account_List.Last().Name);
        }
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            Account acc = Account_List.GetAccount((string)nameBox.SelectedItem);
            MainMenu mm;
            if (acc == null)
            {
                MessageBox.Show("Сначала нужно выбрать имя");
                return;
            }
            mm = new MainMenu(acc);
            this.Hide();
            mm.ShowDialog();
            this.Show();
        }
        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            exitButton.StrokeThickness = 5;
        }
        private void Ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
            exitButton.StrokeThickness = 1;
        }
    }
}
