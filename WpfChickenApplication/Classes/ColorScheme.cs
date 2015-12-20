using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfChickenApplication
{
    public class ColorScheme
    {
        public static void GetColorScheme(System.Windows.Window window)
        {
            window.Background = new RadialGradientBrush(Color.FromArgb(255,150,255,43),Color.FromArgb(255,13,133,0));
        }
    }
}
