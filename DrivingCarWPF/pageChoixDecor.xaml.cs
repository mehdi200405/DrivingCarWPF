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
    /// Logique d'interaction pour pageChoixDecor.xaml
    /// </summary>
    public partial class pageChoixDecor : Window
    {
        public pageChoixDecor()
        {
            InitializeComponent();

            ImageBrush brush1 = new ImageBrush();
            brush1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\ApercuDecor1.jpg"));
            butDecor1.Background = brush1;


            ImageBrush brush2 = new ImageBrush();
            brush2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\ApercuDecor2.jpg"));
            butDecor2.Background = brush2;
        }

        private void butDecor1_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void butDecor2_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
