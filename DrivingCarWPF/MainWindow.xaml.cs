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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Media;


namespace DrivingCarWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool gauche, droit, reculer, avancer = false;      
        private int vitesseJoueur = 10;                                     //10 Plus le chiffre est petit plus la vitesse de déplacement est faible
        private DispatcherTimer minuterie = new DispatcherTimer();
        private readonly int vitesse = 5;                                   //5 Plus le chiffre est petit plus la vitesse sera élever
        private bool rotation = false;
        private SoundPlayer sonVoiture;
        private SoundPlayer sonPneu;
        
        // Rectangle de collision de la pièce


        public MainWindow()
        {
            InitializeComponent();

            Luncher dialogLuncher = new Luncher();
            dialogLuncher.ShowDialog();

            pageChoixDecor dialogPageChoix = new pageChoixDecor();
            dialogPageChoix.ShowDialog();
       

            minuterie.Tick += GameEngine;
            minuterie.Interval = TimeSpan.FromMilliseconds(16);             // rafraissement toutes les 16 milliseconds
            minuterie.Start();                                              // lancement du timer

            ImageBrush imgRoute1 = new ImageBrush();
            imgRoute1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\road_0.png"));
            route1.Fill = imgRoute1;

            ImageBrush imgRoute2 = new ImageBrush();
            imgRoute2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\road_0.png"));
            route2.Fill = imgRoute2;

            ImageBrush imgVoiture = new ImageBrush();
            imgVoiture.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\LamborghiniRevento.png"));
            voiture.Fill = imgVoiture;

            sonVoiture = new SoundPlayer("D:\\Utilisateurs\\formation\\Documents\\IUT\\Code WPF\\DrivingCarWPF\\DrivingCarWPF\\sons\\sonVoitureAccélération.wav");
            sonVoiture.Play();

            sonPneu = new SoundPlayer("D:\\Utilisateurs\\formation\\Documents\\IUT\\Code WPF\\DrivingCarWPF\\DrivingCarWPF\\sons\\Crissement-de-pneus.wav");
            

            deplacementRouteInfinie();
            
        }

        
        private void deplacementRouteInfinie()
        {
            TimeSpan duree = TimeSpan.FromSeconds(vitesse);                 // Durée de l'animation

            
            DoubleAnimation animation1 = new DoubleAnimation                // Création d'une animation pour route1
            {
                From = -1,
                To = 684,                                                   // Hauteur du rectangle
                Duration = duree,
                RepeatBehavior = RepeatBehavior.Forever
            };

                                                                            // Création d'une animation pour route2
            DoubleAnimation animation2 = new DoubleAnimation
            {
                From = -684,                                                // Début au-dessus de l'écran
                To = 1,
                Duration = duree,
                RepeatBehavior = RepeatBehavior.Forever
            };

                                                                            // Appliquer les animations à Canvas.Top des rectangles
            Storyboard.SetTarget(animation1, route1);
            Storyboard.SetTargetProperty(animation1, new PropertyPath(Canvas.TopProperty));

            Storyboard.SetTarget(animation2, route2);
            Storyboard.SetTargetProperty(animation2, new PropertyPath(Canvas.TopProperty));

                                                                            // Créer et lancer le storyboard
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation1);
            storyboard.Children.Add(animation2);
            storyboard.Begin();
        }
        private void voiture_KeyDown(object sender, KeyEventArgs e)
        {
                                                                            // on gère les booléens gauche et droite en fonction de l’appui de la touche
            if (e.Key == Key.Left)
            {
                gauche = true;
                sonPneu.Play();
                
            }
            if (e.Key == Key.Right)
            {
                droit = true;
                sonPneu.Play();
                
            }
            if (e.Key == Key.Down)
            {
                reculer = true;
            }
            if (e.Key == Key.Up)
            {
                avancer = true;
            }

        }

        private void voiture_KeyUp(object sender, KeyEventArgs e)
        {
                                                                                                    // on gère les booléens gauche et droite en fonction du relâchement de la touche
            if (e.Key == Key.Left)
            {
                gauche = false;
                sonPneu.Stop();
                sonVoiture.Play();
            }
            if (e.Key == Key.Right)
            {
                droit = false;
                sonPneu.Stop();
                sonVoiture.Play();
            }
            if (e.Key == Key.Down)
            {
                reculer = false;
            }
            if (e.Key == Key.Up)
            {
                avancer = false;
            }
        }

        private void GameEngine(object sender, EventArgs e)
        {
            
                                           // création d’un rectangle joueur pour la détection de collision
            Rect joueur = new Rect(Canvas.GetLeft(voiture), Canvas.GetTop(voiture), voiture.Width, voiture.Height);
            
            // déplacement à gauche et droite de vitessePlayer avec vérification des limites de fenêtre gauche et droite
            if (gauche && Canvas.GetLeft(voiture) > 0)
            {
                commenceRotationAnimation(-15);
                Canvas.SetLeft(voiture, Canvas.GetLeft(voiture) - vitesseJoueur);
                
            }
            else if (droit && Canvas.GetLeft(voiture) + voiture.Width < Application.Current.MainWindow.Width)
            {
                Console.WriteLine("je suis dans le if droite");

                commenceRotationAnimation(15);                                                         // Commencer l'animation de rotation vers la droite
                Canvas.SetLeft(voiture, Canvas.GetLeft(voiture) + vitesseJoueur);
                
            }
            else
            {
                if (rotation)
                {
                    Console.WriteLine("StopRotation");
                    StopRotationAnimation();
                }
                // Arrêter l'animation de rotation si la touche n'est pas maintenue
            }

            if (avancer && Canvas.GetTop(voiture) > 0)
            {
                Canvas.SetTop(voiture, Canvas.GetTop(voiture) - vitesseJoueur);
            }
            else if (reculer && Canvas.GetTop(voiture) + voiture.Height < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(voiture, Canvas.GetTop(voiture) + vitesseJoueur);
            }
           
            

        }

        private void commenceRotationAnimation(double angle)
        {
            if (!rotation)
            {
                                                                            // Créer une animation de rotation continue pour la voiture
                RotateTransform tourne = new RotateTransform();
                tourne.CenterX = voiture.Width / 2;
                tourne.CenterY = voiture.Height / 2;

                DoubleAnimation rotateAnimation = new DoubleAnimation
                {
                    From = tourne.Angle,
                    To = angle,
                    Duration = TimeSpan.FromSeconds(0.2),                   // Durée de chaque rotation
                    RepeatBehavior = RepeatBehavior.Forever                 // Répéter l'animation indéfiniment
                };

                                                                            
                voiture.RenderTransform = tourne;                           // Appliquer l'animation à la voiture
                tourne.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
                rotation = true;
            }
        }

        private void StopRotationAnimation()
        {
            if (rotation)
            {                                                               // Arrêter l'animation de rotation
                voiture.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
                rotation = false;
            }
        }

    }
}