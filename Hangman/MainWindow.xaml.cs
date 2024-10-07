using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
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

namespace Hangman
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        string[] listmot = { "vache", "bache", "chat", "cheval", "ordinateur" };
        Random rnd = new Random();
        string motChoisi;  // Mot sélectionné
        char[] motAffiche;  // Mot avec les lettres devinées et les underscores
        int vie = 5;  // Nombre de vies restantes

        // Lors du chargement de la fenêtre
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartNewGame();  // Commencer une nouvelle partie
        }

        // Fonction pour démarrer une nouvelle partie
        private void StartNewGame()
        {
            motChoisi = listmot[rnd.Next(listmot.Length)];
            motAffiche = new string('_', motChoisi.Length).ToCharArray();  // Créer un tableau d'underscores pour le mot affiché
            vie = 5;  // Réinitialiser les vies
            UpdateMotAffiche();  // Mettre à jour l'affichage du mot
            EnableAllButtons();  // Activer tous les boutons
        }

        // Fonction appelée lors du clic sur un bouton de lettre
        private void BTN_click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string letter = btn.Name.ToString().ToLower();  // Récupérer la lettre du bouton (minuscule)
            btn.IsEnabled = false;  // Désactiver le bouton après le clic

            // Si la lettre fait partie du mot choisi
            if (motChoisi.Contains(letter))
            {
                for (int i = 0; i < motChoisi.Length; i++)
                {
                    if (motChoisi[i].ToString() == letter)
                    {
                        motAffiche[i] = motChoisi[i];  // Remplacer l'underscore par la lettre correcte
                    }
                }
            }
            else
            {
                vie--;  // Réduire le nombre de vies
                if (vie == 0)
                {
                    MessageBox.Show($"Perdu ! Le mot était : {motChoisi}");
                    StartNewGame();  // Redémarrer le jeu après la défaite
                    return;
                }
            }

            UpdateMotAffiche();  // Mettre à jour l'affichage du mot

            // Vérifier si le joueur a gagné
            if (new string(motAffiche) == motChoisi)
            {
                MessageBox.Show("Félicitations, vous avez deviné le mot !");
                StartNewGame();  // Redémarrer le jeu après la victoire
            }
        }

        // Mettre à jour l'affichage du mot dans la TextBox
        private void UpdateMotAffiche()
        {
            Textbox.Text = new string(motAffiche);  // Mettre à jour le texte avec les lettres devinées
        }

        // Réactiver tous les boutons des lettres (après redémarrage)
        private void EnableAllButtons()
        {
            foreach (UIElement element in LetterButtons.Children)
            {
                if (element is Button btn)  // Vérifier si l'élément est un bouton
                {
                    btn.IsEnabled = true;  // Réactiver tous les boutons
                }
            }
        }


    }
}
