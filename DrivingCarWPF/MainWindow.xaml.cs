﻿using System;
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
using System.Numerics;


namespace DrivingCarWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool gauche, droit, reculer, avancer, pause = false;      
        private int vitesseJoueur = 10;                                     //10 Plus le chiffre est petit plus la vitesse de déplacement est faible
        private DispatcherTimer minuterie = new DispatcherTimer();
        private readonly int vitesse = 5;                                   //5 Plus le chiffre est petit plus la vitesse sera élever
        private bool rotation = false;
        private SoundPlayer sonVoiture;
        private SoundPlayer sonPneu;
        private int imageDecor = 0;
        private ImageBrush imgHuile = new ImageBrush();
        private bool premiereIteration = true;
        private Storyboard storyboard = new Storyboard();
        private int imageVoiture = 0;

        // Rectangle de collision de la pièce


        public MainWindow()
        {
            InitializeComponent();

            Luncher dialogLuncher = new Luncher();
            dialogLuncher.ShowDialog();

            pageChoixDecor dialogPageChoix = new pageChoixDecor();
            dialogPageChoix.ShowDialog();

            this.imageDecor = dialogPageChoix.choixImageDecor;
            
            if (this.imageDecor == 1)
            {
                ImageBrush imgRoute1 = new ImageBrush();
                imgRoute1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\road_0.png"));
                route1.Fill = imgRoute1;
                route2.Fill = imgRoute1;
            }
            else
            {
                ImageBrush imgRoute1 = new ImageBrush();
                imgRoute1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\road_0neige.png"));
                route1.Fill = imgRoute1;
                route2.Fill = imgRoute1;
            }

            dialogChoixVoiture pageChoixVoiture = new dialogChoixVoiture();
            pageChoixVoiture.ShowDialog();

            this.imageVoiture = pageChoixVoiture.choixImageVoiture;

            ImageBrush imgVoiture = new ImageBrush();
            string imagePath = AppDomain.CurrentDomain.BaseDirectory + "img\\voiture";

            switch (this.imageVoiture)
            {
                case 1:
                    imgVoiture.ImageSource = new BitmapImage(new Uri(imagePath + "1.png"));
                    break;
                case 2:
                    imgVoiture.ImageSource = new BitmapImage(new Uri(imagePath + "2.png"));
                    break;
                case 3:
                    imgVoiture.ImageSource = new BitmapImage(new Uri(imagePath + "3.png"));
                    break;
                case 4:
                    imgVoiture.ImageSource = new BitmapImage(new Uri(imagePath + "4.png"));
                    break;
                
                default:
                    
                    break;
            }

            voiture.Fill = imgVoiture;



            ImageBrush imghuile = new ImageBrush();
            imghuile.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "img\\huile1.png"));
            huileMoteur.Fill = imghuile;
            

            minuterie.Tick += GameEngine;
            minuterie.Interval = TimeSpan.FromMilliseconds(16);             // rafraissement toutes les 16 milliseconds
            minuterie.Start();                                              // lancement du timer

            sonVoiture = new SoundPlayer("D:\\Utilisateurs\\formation\\Documents\\IUT\\Code WPF\\DrivingCarWPF\\DrivingCarWPF\\sons\\sonVoitureAccélération.wav");
            sonVoiture.PlayLooping();

            sonPneu = new SoundPlayer("D:\\Utilisateurs\\formation\\Documents\\IUT\\Code WPF\\DrivingCarWPF\\DrivingCarWPF\\sons\\Crissement-de-pneus.wav");
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
            if (e.Key == Key.P)
            {
                if (minuterie.IsEnabled)
                {
                    pause = true;
                    minuterie.Stop();
                    storyboard.Pause();
                    canvasPause.Visibility = Visibility.Visible;
                }
                else
                {
                    pause = false;
                    minuterie.Start();
                    storyboard.Resume();
                    canvasPause.Visibility = Visibility.Collapsed;
                }

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
            if (e.Key == Key.P)
            {
                pause = false;
            }
        }

        private void butRejouer_Click(object sender, RoutedEventArgs e)
        {
            storyboard.Resume();
            canvasPerdu.Visibility = Visibility.Collapsed;
            
            // Restart the timer
            minuterie.Start();
        }

        private void butReprendre_Click(object sender, RoutedEventArgs e)
        {
            pause = false;
            minuterie.Start();
            storyboard.Resume();
            canvasPause.Visibility = Visibility.Collapsed;
        }


        private void GameEngine(object sender, EventArgs e)
        {
            Canvas.SetTop(huileMoteur, Canvas.GetTop(huileMoteur) + 3.5);
            // création d’un rectangle joueur pour la détection de collision
            Rect obstacle = new Rect(Canvas.GetLeft(huileMoteur), Canvas.GetTop(huileMoteur), huileMoteur.Width, huileMoteur.Height);

            Rect joueur = new Rect(Canvas.GetLeft(voiture), Canvas.GetTop(voiture), voiture.Width, voiture.Height);
            if (premiereIteration && !pause)
            {
                deplacementRouteInfinie();
                premiereIteration = false;
            }

            if (joueur.IntersectsWith(obstacle))
            {
                minuterie.Stop();
                storyboard.Pause();
                canvasPerdu.Visibility = Visibility.Visible;
            }

            
            
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