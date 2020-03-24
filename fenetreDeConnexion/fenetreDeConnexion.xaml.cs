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

using MyCartographyObj;
using fenetrePrincipale;


namespace fenetreDeConnexion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FenetreDeConnexion : Window
    {
        #region CONSTRUCTEURS
        public FenetreDeConnexion()
        {
            Personne = new MyPersonalMapData();
            InitializeComponent();
        }

        #endregion

        #region VARIABLES MEMBRES
        private MyPersonalMapData _personne;
        public MyPersonalMapData Personne
        {
            get { return _personne; }
            set { _personne = value; }
        }
        #endregion

        //OUTPUT : true si le nom ou prenom est valide/ false s'il est invalide
        private bool validerNomPrenom(string newNom)
        {          
            string lettreAutorisee = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZéè";

            if (newNom.Length > 0)
            {
                foreach (char c in newNom)
                {
                    if (!lettreAutorisee.Contains(c.ToString()))
                        return false;
                }
            }
            return true;
        }

        //OUTPUT : true si l'e-mail est valide/ false s'il est invalide
        private bool validerEmail(string newEmail)
        {
            bool contientUnArobase = false, contientUnPoint = false;
            string lettreAutorisee = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZéè";

            if (newEmail.Length > 0)
            {
                foreach (char c in newEmail)
                {
                    if (!lettreAutorisee.Contains(c.ToString()))
                    {
                        if (c == '@')
                        {
                            if (contientUnArobase == false) contientUnArobase = true;
                            else return false;
                        }
                        else if (c == '.')
                        {
                            if (contientUnPoint == false) contientUnPoint = true;
                            else return false;
                        }
                        else return false;
                    }                        
                }
                if(contientUnArobase == false || contientUnPoint == false)
                {
                    return false;
                }
            }
            return true;
        }

        private void Button_LOGIN_Click(object sender, RoutedEventArgs e)
        {
            bool erreurEncodage = false;
            string messageErreur = "";

            if(!validerNomPrenom(NOM_TextBox.Text))
            {
                erreurEncodage = true;
                messageErreur = "Nom invalide";
            }

            if(!validerNomPrenom(PRENOM_TextBox.Text))
            {
                erreurEncodage = true;
                messageErreur += " Prenom invalide";
            }

            if(!validerEmail(EMAIL_TextBox.Text))
            {
                erreurEncodage = true;
                messageErreur += " Email invalide";
            }

            if(erreurEncodage)
            {
                MessageBox.Show(messageErreur, "ERREUR ENCODAGE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {           
                Personne.Nom = NOM_TextBox.Text;
                Personne.Prenom = PRENOM_TextBox.Text;
                Personne.Email = EMAIL_TextBox.Text;

                MyPersonalMapData personneTMP;
                if((personneTMP = MyPersonalMapData.LoadPersonne(Personne)) == null)
                {
                    MessageBoxResult resultNewClient = MessageBox.Show("Vous n'existez pas dans la base de donnée...\nVoulez-vous vous inscrire ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch(resultNewClient)
                    {
                        case MessageBoxResult.Yes:

                            FenetrePrincipale fenetrePrincipale = new FenetrePrincipale(Personne);
                            fenetrePrincipale.Show();
                            Close();
                            break;
                        /*case MessageBoxResult.No:
                            
                            break;*/
                    }
                }
                else
                {
                    Personne = personneTMP;
                    string message = "RE Bonjour, " + Personne.Nom + " " + Personne.Prenom + ":)"; 
                    MessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Information);

                    FenetrePrincipale fenetrePrincipale = new FenetrePrincipale(Personne);                                        
                    fenetrePrincipale.Show();                   
                    Close();
                }                                 
            }
        }

        private void QUITTER_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
