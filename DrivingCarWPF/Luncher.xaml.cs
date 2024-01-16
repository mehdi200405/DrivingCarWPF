using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Logique d'interaction pour Luncher.xaml
    /// </summary>
    public partial class Luncher : Window
    {
        private SoundPlayer soundPlayer;
        public Luncher()
        {
            InitializeComponent();

            ImageBrush fondCanvas = new ImageBrush();
            fondCanvas.ImageSource = new BitmapImage(new Uri("img\\fondVoitureLuncher.jpg", UriKind.Relative));
            canvasAccueil.Background = fondCanvas;

            ImageBrush bruchparametre = new ImageBrush();
            bruchparametre.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\parametres.png"));
            butParametre.Background = bruchparametre;
            soundPlayer = new SoundPlayer("D:\\Utilisateurs\\formation\\Documents\\IUT\\Code WPF\\DrivingCarWPF\\DrivingCarWPF\\sons\\sonsPageAccueil.wav");
            soundPlayer.Play();

        }

        private void buttonJouer_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            soundPlayer.Stop();
            
        }

        private void choixNiveau_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void butParametre_Click(object sender, RoutedEventArgs e)
        {
            DialogCommandeJeux choixParametre = new DialogCommandeJeux();
            choixParametre.Show();
        }
    }
}
