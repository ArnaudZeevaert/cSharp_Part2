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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyCartographyObj;
using System.Threading;

namespace PersonalMap_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class connexionWindow : Window
    {
       /* public connexionWindow()
        {
            
            DataContext = Personne;
            InitializeComponent();
        }*/

        public connexionWindow(MyPersonalMapData personneConnectee)
        {
            Personne = personneConnectee;
            DataContext = Personne;
            InitializeComponent();
        }

        private MyPersonalMapData _personne;
        public MyPersonalMapData Personne
        {
            get { return _personne; }
            set { _personne = value; }
        }

        //public MyPersonalMapData personne = new MyPersonalMapData();

        private void VALIDER_bouton_Click(object sender, RoutedEventArgs e)
        {
            
            if (PrenomTB.Text.Length == 0 || NomTB.Text.Length == 0 || EmailTB.Text.Length == 0)
            {
                if (PrenomTB.Text.Length == 0)                
                    Prenom_label.Foreground = Brushes.Red;                
                else
                {
                    Prenom_label.Foreground = Brushes.Green;
                    Personne.Prenom = PrenomTB.Text;
                }


                if (NomTB.Text.Length == 0)                
                    Nom_label.Foreground = Brushes.Red;                
                else
                {
                    Nom_label.Foreground = Brushes.Green;
                    Personne.Nom = NomTB.Text;
                }


                if (EmailTB.Text.Length == 0)                
                    Email_label.Foreground = Brushes.Red;                
                else
                {
                    Email_label.Foreground = Brushes.Green;
                    Personne.Email = EmailTB.Text;
                }
                MessageBox.Show("erreur encodage ...", "econdage error", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            }
            else
            {
                Personne.Prenom = PrenomTB.Text;
                Personne.Nom = NomTB.Text;
                Personne.Email = EmailTB.Text;

                Prenom_label.Foreground = Brushes.Green;
                Nom_label.Foreground = Brushes.Green;
                Email_label.Foreground = Brushes.Green;

                VALIDER_bouton.Content = "Bonjour, " + Personne.Prenom + " " + Personne.Nom + " " + Personne.Email;
                VALIDER_bouton.Foreground = Brushes.Red;
                VALIDER_bouton.FontSize = 19;                       
                



                Close();
            }

            Console.WriteLine("DEBUG : Bouton Valider : objet Personne = nom:{0}, prenom:{1}, email:{2}", Personne.Nom, Personne.Prenom, Personne.Email);
        }
    }
}
