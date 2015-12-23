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

namespace WpfChickenApplication.Windows
{
    public partial class Level3 : Window
    {
        string[] images = System.IO.Directory.GetFiles("../../Resources/Level2Images");
        List<List<string>> groups = new List<List<string>>();
        bool[,] right;
        object Captured = null;
        int currentIndex;
        List<Image> Boxes = new List<Image>();
        List<Image> Letters = new List<Image>();
        Point prevPoint;
        public Level3(int num)
        {
            Random rand = new Random();
            InitializeComponent();
            ColorScheme.GetColorScheme(this);
            string[] temp = File.ReadAllLines("../../Resources/Groups.txt",Encoding.Default);
            foreach (string element in temp)
            {
                groups.Add(new List<string>());
                groups.Last().Add(element.Split(':')[0]);
                groups.Last().Add(element.Split(':')[1].Split(',')[0]);
                groups.Last().Add(element.Split(':')[1].Split(',')[1]);
            }
            currentIndex = num;
            right = new bool[3,2];
            #region создение ячеек для картинок и подписей для ячеек
            Boxes.AddRange(new Image[] { Box0, Box1, Box2 });
            Label1.Content = groups[(num-1) * 3][0];
            Label2.Content = groups[(num-1) * 3+1][0];
            Label3.Content = groups[(num-1) * 3+2][0];
            #endregion
            #region создание картинок
            for (int i = (num - 1) * 3; i < num * 3; i++)
            {
                for (int j = 1; j < groups[i].Count; j++)
                {
                    Board.Children.Add(new Image());
                    ((Image)Board.Children[Board.Children.Count - 1]).Name = ("Word" + (j-1)%3+"_") + i%3;
                    ((Image)Board.Children[Board.Children.Count - 1]).Source = new BitmapImage(new Uri("pack://application:,,,/Resources/" + groups[i][j]+".png"));
                    ((Image)Board.Children[Board.Children.Count - 1]).Width = 103;
                    ((Image)Board.Children[Board.Children.Count - 1]).Height = 140;
                    ((Image)Board.Children[Board.Children.Count - 1]).Margin = new Thickness(rand.Next(670), rand.Next(370), rand.Next(600), rand.Next(300));
                    ((Image)Board.Children[Board.Children.Count - 1]).MouseDown += letter_MouseDown;
                    Letters.Add((Image)Board.Children[Board.Children.Count - 1]);
                }
            }
            #endregion
            this.MouseUp += Board_MouseUp;
            this.MouseMove += Board_MouseMove;
        }
        private void Board_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Captured != null)
            {
                GeneralTransform boxT;
                Rect boxR;
                GeneralTransform t2 = ((Image)Captured).TransformToVisual(this);
                Rect r2 = t2.TransformBounds(new Rect(0, 0, ((Image)Captured).ActualWidth, ((Image)Captured).ActualHeight));
                ((Image)Captured).Effect = null;
                foreach (Image box in Boxes)
                {
                    boxT = box.TransformToVisual(this);
                    boxR = boxT.TransformBounds(new Rect(0, 0, box.ActualWidth, box.ActualHeight));
                    if (boxR.IntersectsWith(r2))
                    {
                        if (box.Name.Substring(3) == ((Image)Captured).Name.Split('_')[1])
                        {
                            box.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Backgrounds/rightAnswer.png"));
                            right[Boxes.IndexOf(box), Convert.ToInt16(((Image)Captured).Name.Split('_')[0].Substring(4))] = true;
                            break;
                        }
                    }
                    else right[Convert.ToInt16(((Image)Captured).Name.Split('_')[1]), Convert.ToInt16(((Image)Captured).Name.Split('_')[0].Substring(4))] = false;
                }
                for (int i = 0; i < right.GetLength(0); i++)
                {
                    if (right[i, 0] & right[i, 1])
                        Boxes[i].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Backgrounds/rightAnswer.png"));
                    else if (right[i, 0] | right[i, 1])
                        Boxes[i].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Backgrounds/halfAnswer.png"));
                    else Boxes[i].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Backgrounds/wrongAnswer.png"));
                }
                Captured = null;
                if (IsAllRight()) nextButton.Visibility = Visibility.Visible;
                else nextButton.Visibility = Visibility.Hidden;
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
            Level3Selecter l = new Level3Selecter();
            l.Show();
            this.Close();
        }
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex < 9)
            {
                Level3 l = new Level3(currentIndex + 1);
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
        private bool IsAllRight()
        {
            foreach (bool element in right) if (!element) return false;
            if (MainMenu.CurrentAcc.Task_Avalible <= currentIndex + 1)
            {
                if (currentIndex < 19) MainMenu.CurrentAcc.Task_Avalible = currentIndex + 1;
            }
            return true;
        }
    }
}
