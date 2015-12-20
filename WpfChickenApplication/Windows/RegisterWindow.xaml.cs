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
using WpfChickenApplication.Windows;

namespace WpfChickenApplication.Windows
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            ColorScheme.GetColorScheme(this);

        }

        private void canselButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text == "")
            {
                MessageBox.Show("Имя не может быть пустым");
                return;
            }
            if (LoginWindow.Account_List.Contains(nameBox.Text))
            {
                MessageBox.Show("Такое имя пользователя уже зарегистрировано");
                return;
            }
            LoginWindow.Account_List.Add(new Account(nameBox.Text));
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream("acc_base.bin", FileMode.OpenOrCreate);
            formatter.Serialize(fs, LoginWindow.Account_List);
            fs.Close();
            MessageBox.Show("Регистрация успешно завершена");
            this.Close();
        }
    }
}
