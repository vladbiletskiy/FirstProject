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
    public partial class Level2 : Window
    {
        string[] images = System.IO.Directory.GetFiles("../../Resources/Level2Images");

        string name;
        bool[] right;
        object Captured = null;
        int currentIndex;
        List<Image> Boxes = new List<Image>();
        List<Image> Letters = new List<Image>();
        Point prevPoint;
        public Level2(int num)
        {
            
            Random rand = new Random();
            InitializeComponent();
            ColorScheme.GetColorScheme(this);
            for (int i = 0; i < images.Length; i++)
            {
                images[i] = "pack://application:,,,/Resources/Level2Images/" + images[i].Substring(29);
            }
            name = images[num - 1].Substring(46, images[num - 1].Length - 50);
            string alph = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            alph = new String(alph.Except(name).ToArray()).ToUpperInvariant();
            Word.Source = new BitmapImage(new Uri(images[num - 1]));
            currentIndex = num;
            name = name.ToUpperInvariant();
            right = new bool[name.Length];
            #region создение ячеек для букв
            for (int i = 0; i < name.Length; i++)
            {
                Board.Children.Add(new Image());
                ((Image)Board.Children[Board.Children.Count - 1]).Name = "Box" + (i);
                ((Image)Board.Children[Board.Children.Count - 1]).Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Backgrounds/wrongAnswer.png"));
                ((Image)Board.Children[Board.Children.Count - 1]).Width = 123;
                ((Image)Board.Children[Board.Children.Count - 1]).Height = 160;
                ((Image)Board.Children[Board.Children.Count - 1]).Margin = new Thickness((i - 4.5) * 250, 140, 0, 0);
                Boxes.Add((Image)Board.Children[Board.Children.Count - 1]);
            }
            #endregion
            #region создание карт с буквами
            for (int i = 0; i < name.Length; i++)
            {
                Board.Children.Add(new Image());
                ((Image)Board.Children[Board.Children.Count - 1]).Name = "Letter" + (i);
                ((Image)Board.Children[Board.Children.Count - 1]).Source = new BitmapImage(new Uri("pack://application:,,,/Resources/LetterButtons/" + name[i] + ".png"));
                ((Image)Board.Children[Board.Children.Count - 1]).Width = 93;
                ((Image)Board.Children[Board.Children.Count - 1]).Height = 140;
                ((Image)Board.Children[Board.Children.Count - 1]).Margin = new Thickness(rand.Next(670), rand.Next(370), rand.Next(600), rand.Next(300));
                ((Image)Board.Children[Board.Children.Count - 1]).MouseDown += letter_MouseDown;
                Letters.Add((Image)Board.Children[Board.Children.Count - 1]);
            }
            #endregion
            #region создание дополнительных случайных букв (не содержащихся в слове)
            for (int i = 0; i < 4; i++)
            {
                Board.Children.Add(new Image());
                ((Image)Board.Children[Board.Children.Count - 1]).Name = "Letter" + (name.Length+i);
                ((Image)Board.Children[Board.Children.Count - 1]).Source = new BitmapImage(new Uri("pack://application:,,,/Resources/LetterButtons/" + alph[rand.Next(0,alph.Length)] + ".png"));
                ((Image)Board.Children[Board.Children.Count - 1]).Width = 93;
                ((Image)Board.Children[Board.Children.Count - 1]).Height = 140;
                ((Image)Board.Children[Board.Children.Count - 1]).Margin = new Thickness(rand.Next(670), rand.Next(370), rand.Next(600), rand.Next(300));
                ((Image)Board.Children[Board.Children.Count - 1]).MouseDown += letter_MouseDown;
                Letters.Add((Image)Board.Children[Board.Children.Count - 1]);
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
                        if (box.Name.Substring(3) == ((Image)Captured).Name.Substring(6))
                        {
                            box.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Backgrounds/rightAnswer.png"));
                            right[Convert.ToInt16(box.Name.Substring(3))] = true;
                            break;
                        }
                        else if (int.Parse(((Image)Captured).Name.Substring(6))<name.Length&&name[int.Parse(box.Name.Substring(3))] == name[int.Parse(((Image)Captured).Name.Substring(6))])
                        {
                            Thickness temp = ((Image)Captured).Margin;
                            ((Image)Captured).Margin = Letters[int.Parse(box.Name.Substring(3))].Margin;
                            Letters[int.Parse(box.Name.Substring(3))].Margin = temp;
                            //ЗАКРОЙМЕНЯ!!!!!
                        }
                    }
                    else if(int.Parse(((Image)Captured).Name.Substring(6))<name.Length) right[Convert.ToInt16(((Image)Captured).Name.Substring(6))] = false;
                }
                for (int i = 0; i < right.Length; i++)
                {
                    if (!right[i]) Boxes[i].Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Backgrounds/wrongAnswer.png"));
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
            Level2Selecter l = new Level2Selecter();
            l.Show();
            this.Close();
        }
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex < images.Length)
            {
                Level2 l = new Level2(currentIndex + 1);
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
            if (MainMenu.CurrentAcc.Task_Avalible <= currentIndex + 1&&MainMenu.CurrentAcc.Level_Avalible<3)
            {
                if (currentIndex < 19) MainMenu.CurrentAcc.Task_Avalible = currentIndex + 1;
                else
                {
                    MainMenu.CurrentAcc.Task_Avalible = 1;
                    MainMenu.CurrentAcc.Level_Avalible++;
                }
            }
            return true;
        }
    }
}
