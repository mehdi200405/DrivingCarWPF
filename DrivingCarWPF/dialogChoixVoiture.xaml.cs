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

namespace DrivingCarWPF
{
    /// <summary>
    /// Logique d'interaction pour dialogChoixVoiture.xaml
    /// </summary>
    public partial class dialogChoixVoiture : Window
    {
        public dialogChoixVoiture()
        {
            InitializeComponent();

            ImageBrush fondCanvas = new ImageBrush();
            fondCanvas.ImageSource = new BitmapImage(new Uri("img\\fondVoitureLuncher.jpg", UriKind.Relative));
            canvasChoixVoiture.Background = fondCanvas;
        }
    }
}
