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
        List<Image> Boxes = new List<Image>();
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
                Boxes.Add((Image)Board.Children[Board.Children.Count - 1]);
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
            }
            this.MouseUp += Board_MouseUp;
            this.MouseMove += Board_MouseMove;
        }
        private void Board_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Captured != null)
            {
                ((Image)Captured).Effect = null;
                Captured = null;
            }
        }
        private void Board_MouseMove(object sender, MouseEventArgs e)
        {
            if (Captured != null)
            {
                Point p = e.GetPosition(Board);
                ((Image)Captured).Margin = new Thickness(p.X, p.Y, this.Width - p.X - ((Image)Captured).ActualWidth - 20, this.Height - p.Y - ((Image)Captured).ActualHeight - 20);                
            }
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
            System.Windows.Media.Effects.DropShadowEffect effect = new System.Windows.Media.Effects.DropShadowEffect();
            effect.ShadowDepth = 15;
            ((Image)sender).Effect = effect;
            Captured = sender;
            prevPoint = e.GetPosition(this);
        }
    }
}
