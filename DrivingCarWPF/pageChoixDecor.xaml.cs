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
        public int choixImage = 0;
        public pageChoixDecor()
        {
            InitializeComponent();

            ImageBrush imageDecor1 = new ImageBrush();
            imageDecor1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\ApercuDecor1.jpg"));
            butDecor1.Background = imageDecor1;


            ImageBrush imageDecor2 = new ImageBrush();
            imageDecor2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\ApercuDecor2.jpg"));
            butDecor2.Background = imageDecor2;
        }

        private void butDecor1_Click(object sender, RoutedEventArgs e)
        {
            this.choixImage = 1;
            this.DialogResult = true;
        }

        private void butDecor2_Click(object sender, RoutedEventArgs e)
        {
            this.choixImage = 2;
            this.DialogResult = true;
        }

        private void butRetour_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
