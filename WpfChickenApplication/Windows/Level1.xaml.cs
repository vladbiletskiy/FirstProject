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
    public partial class Level1 : Window
    {
        string[] images = System.IO.Directory.GetFiles("../../Resources/Level1Images");
        string name;
        object Captured = null;
        int CurrentNum;
        Point prevPoint;
        public Level1(int num)
        {
            Random rand = new Random();
            InitializeComponent();
            ColorScheme.GetColorScheme(this);
            for (int i = 0; i < images.Length; i++)
            {
                images[i] = "pack://application:,,,/Resources/Level1Images/" + images[i].Substring(29);
            }
            name=images[num-1].Substring(46,images[num-1].Length-50);
            Word.Source = new BitmapImage(new Uri(images[num-1]));
            CurrentNum = num;
            name = name.ToUpperInvariant();
            for (int i = 0; i < name.Length; i++)
            {
                Board.Children.Add(new Image());
                ((Image)Board.Children[Board.Children.Count - 1]).Name = "Box" + (i);
                ((Image)Board.Children[Board.Children.Count - 1]).Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Backgrounds/wrongAnswer.png"));
                ((Image)Board.Children[Board.Children.Count - 1]).Width = 123;
                ((Image)Board.Children[Board.Children.Count - 1]).Height = 160;
                ((Image)Board.Children[Board.Children.Count - 1]).Margin = new Thickness((i-4.5)*250,140,0,0);
            }
            for (int i = 0; i < name.Length; i++)
            {
                Board.Children.Add(new Image());
                ((Image)Board.Children[Board.Children.Count - 1]).Name = "Letter" + (i);
                ((Image)Board.Children[Board.Children.Count - 1]).Source = new BitmapImage(new Uri("pack://application:,,,/Resources/LetterButtons/" + name[i] + ".png"));
                ((Image)Board.Children[Board.Children.Count - 1]).Width = 93;
                ((Image)Board.Children[Board.Children.Count - 1]).Height = 140;
                ((Image)Board.Children[Board.Children.Count - 1]).Margin = new Thickness(rand.Next(670), rand.Next(370), rand.Next(600), rand.Next(300));
                ((Image)Board.Children[Board.Children.Count - 1]).MouseDown+=letter_MouseDown;
                ((Image)Board.Children[Board.Children.Count - 1]).MouseUp += letter_MouseUp;
                ((Image)Board.Children[Board.Children.Count - 1]).MouseMove += letter_MouseMove;
            }
            this.MouseUp += letter_MouseUp;
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentNum < images.Length)
            {
                Level1 l = new Level1(CurrentNum+1);
                l.Show();
                this.Close();
            }
        }
        private void letter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Captured = sender;
            prevPoint = e.GetPosition(this);
        }
        private void letter_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Captured = new object();
        }
        private void letter_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender.Equals(Captured))
            {
                double Y = e.GetPosition(this).Y - prevPoint.Y;
                double X = e.GetPosition(this).X - prevPoint.X;
                ((Image)sender).Margin = new Thickness(((Image)sender).Margin.Left+X, ((Image)sender).Margin.Top+Y, ((Image)sender).Margin.Right - X, ((Image)sender).Margin.Bottom-Y);
            }
        }
    }
}
