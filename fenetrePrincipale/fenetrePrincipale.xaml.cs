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
using Microsoft.Win32;


using MyCartographyObj;

namespace fenetrePrincipale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FenetrePrincipale : Window
    {
        #region VARIABLES MEMBRES
        private MyPersonalMapData _personneConnectee = new MyPersonalMapData();
        #endregion

        #region PROPRIETES
        public MyPersonalMapData PersonneConnectee
        {
            get { return _personneConnectee; }
            set { _personneConnectee = value; }
        }
        #endregion

        #region CONSTRUCTEURS
        public FenetrePrincipale()
        {
            
            InitializeComponent();
            TextDebug.Content = PersonneConnectee.ToString();
        }

        public FenetrePrincipale(MyPersonalMapData personne)
        {
            PersonneConnectee = personne;      
            InitializeComponent();
            TextDebug.Content = PersonneConnectee.ToString();
        }
        #endregion

        #region EVENT
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

        private void MenuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "MyCartographyObj files(*.az) | *.az";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFile.ShowDialog();           

            MyPersonalMapData personneTMP;            
            if ((personneTMP = MyPersonalMapData.LoadFile(openFile.FileName)) == null)
            {
                MessageBox.Show("ERREUR OUVERTURE", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("OUVERTURE REUSSIE", "", MessageBoxButton.OK, MessageBoxImage.Information);
                PersonneConnectee = personneTMP;
                TextDebug.Content = PersonneConnectee.ToString();
            }
        }

        private void MenuFileSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFile.Filter = "MyCartographyObj files(*.az) | *.az";
            saveFile.ShowDialog();

            if(MyPersonalMapData.SavePersonne(PersonneConnectee, saveFile.FileName, true))
            {
                MessageBox.Show("SAUVEGARDE REUSSIE", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {                
                MessageBox.Show("ERREUR SAUVEGARDE", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuToolsOptions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuToolsAbout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuFilePOIImport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuFilePOIExport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuFileTrajetImport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuFileTrajetExport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
