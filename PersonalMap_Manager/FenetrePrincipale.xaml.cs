using Microsoft.Win32;
using MyCartographyObj;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Polygon = MyCartographyObj.Polygon;
using Polyline = MyCartographyObj.Polyline;

namespace PersonalMap_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FenetrePrincipale : Window
    {               
        #region VARIABLES MEMBRES
        private MyPersonalMapData _personneConnectee;
        private string _dossierDeTravail = null;
        private string _couleurFondListBox;
        private string _couleurTextListBox;        
        #endregion

        #region PROPRIETES
        public MyPersonalMapData PersonneConnectee
        {
            get { return _personneConnectee; }
            set 
            {
                if (_personneConnectee != value)
                    _personneConnectee = value;

            }
        }
        public string DossierDeTravail
        {
            get { return _dossierDeTravail; }
            set 
            {
                if (_dossierDeTravail != value)
                    _dossierDeTravail = value;
            }
        }
        public string CouleurFondListBox
        {
            get { return _couleurFondListBox; }
            set
            {
                if (_couleurFondListBox != value)
                    _couleurFondListBox = value;
            }
        }
        public string CouleurTextListBox
        {
            get { return _couleurTextListBox; }
            set
            {
                if (_couleurTextListBox != value)
                    _couleurTextListBox = value;
            }
        }
        #endregion

        #region CONSTRUCTEURS
        public FenetrePrincipale()
        {           
            InitializeComponent();
            PersonneConnectee = new MyPersonalMapData();
            TextDebug.Content = PersonneConnectee.ToString();
            
            UpdateListBox();
        }

        public FenetrePrincipale(MyPersonalMapData personne)
        {
            PersonneConnectee = personne;            
            InitializeComponent();
            TextDebug.Content = PersonneConnectee.ToString();            
        }
        #endregion

        #region Fermeture Fenetre
        private void Window_Closed(object sender, EventArgs e)
        {
            QuitterSauvegarde();
        }
        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            QuitterSauvegarde();
        }

        private void QuitterSauvegarde()
        {
            MyPersonalMapData personneTMP = null;
            bool modif = true;
            if (DossierDeTravail != null && PersonneConnectee.Emplacement == null)
                PersonneConnectee.Emplacement = DossierDeTravail;

            if (PersonneConnectee.Emplacement != null)
            {
                try
                {
                    personneTMP = MyPersonalMapData.LoadFile(PersonneConnectee.Emplacement);
                }
                catch (LoadSaveException messageErreurLoadFile)
                {
                    //MessageBox.Show("ERREUR OUVERTURE", messageErreurLoadFile.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                    Console.WriteLine("(QuitterSauvegarde) ERREUR OUVERTURE : " + messageErreurLoadFile.Message);
                    personneTMP = null;
                }

                if (personneTMP != null)
                {
                    if (personneTMP.ObservableCollection.Equals(PersonneConnectee.ObservableCollection))
                        modif = false;
                }                               
            }

            if (modif)
            {
                MessageBoxResult resultNewClient = MessageBox.Show("Voulez-vous sauvegarder vos modifications ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (resultNewClient)
                {
                    case MessageBoxResult.Yes:
                        try
                        {
                            personneTMP = MyPersonalMapData.SavePersonne(PersonneConnectee, PersonneConnectee.Emplacement);
                        }
                        catch (LoadSaveException messageErreurSave)
                        {
                            MessageBox.Show(messageErreurSave.Message, "ERREUR SAUVEGARDE", MessageBoxButton.OK, MessageBoxImage.Error);
                            personneTMP = null;
                        }
                        if (personneTMP != null)
                        {
                            PersonneConnectee = personneTMP;
                            MessageBox.Show("SAUVEGARDE REUSSIE", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                        break;
                        /*case MessageBoxResult.No:

                            break;*/
                }
            }
            else
                this.Close();
        }
        #endregion

        #region Files and Tools
        private void MenuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "MyCartographyObj files(*.az) | *.az";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFile.ShowDialog();

            //openFile.di

            MyPersonalMapData personneTMP = null;
            try
            {
                personneTMP = MyPersonalMapData.LoadFile(openFile.FileName);
            }
            catch (LoadSaveException messageErreurLoadFile)
            {
                MessageBox.Show(messageErreurLoadFile.Message, "ERREUR OUVERTURE", MessageBoxButton.OK, MessageBoxImage.Error);
                personneTMP = null;
            }

            if (personneTMP != null)
            {
                MessageBox.Show("OUVERTURE REUSSIE", "", MessageBoxButton.OK, MessageBoxImage.Information);
                PersonneConnectee = personneTMP;
                TextDebug.Content = PersonneConnectee.ToString();
                UpdateListBox();
            }
        }
        private void MenuFileSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFile.Filter = "MyCartographyObj files(*.az) | *.az";
            saveFile.ShowDialog();
            MyPersonalMapData personneTMP = null;

            try
            {
                personneTMP = MyPersonalMapData.SavePersonne(PersonneConnectee, saveFile.FileName);
            }
            catch (LoadSaveException messageErreurSave)
            {
                MessageBox.Show(messageErreurSave.Message, "ERREUR SAUVEGARDE", MessageBoxButton.OK, MessageBoxImage.Error);
                personneTMP = null;
            }
            if (personneTMP != null)
            {
                PersonneConnectee = personneTMP;
                TextDebug.Content = PersonneConnectee.ToString();
                MessageBox.Show("SAUVEGARDE REUSSIE", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void MenuToolsOptions_Click(object sender, RoutedEventArgs e)
        {
            FenetreOption fenetreOption = new FenetreOption();
            fenetreOption.Show();

            Console.WriteLine("Debug MenuToolsOptions_Click : avant récupération des changements");

            Console.WriteLine("Debug MenuToolsOptions_Click : apres récupération des changements");
        }

        private void MenuToolsAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
        }

        private void MenuFilePOIImport_Click(object sender, RoutedEventArgs e)
        {
            bool erreurImport = false;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Fichier CSV files(*.csv) | *.csv";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFile.ShowDialog();

            POI poiLoad = null;
            try
            {
                poiLoad = POI.readCSVfile(openFile.FileName);
            }
            catch (CSVexception messageErreurLoadPoi)
            {
                MessageBox.Show(messageErreurLoadPoi.Message, "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
                erreurImport = true;
            }

            if(!erreurImport)
            {
                PersonneConnectee.ObservableCollection.Add(poiLoad);
                TextDebug.Content = PersonneConnectee.ToString();
                UpdateListBox();
                MessageBox.Show("OUVERTURE REUSSIE", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MenuFilePOIExport_Click(object sender, RoutedEventArgs e)
        {                       
            bool erreurExport = false;
            ICartoObj o = (ICartoObj)ListBox.SelectedItem;
            if (o == null)
            {
                MessageBox.Show("Aucun élément de la ListBox est sélectionné", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (o is POI)
                {
                    POI p = o as POI;
                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    saveFile.Filter = "Fichier CSV files(*.csv) | *.csv";
                    saveFile.ShowDialog();
                    try
                    {                        
                        POI.saveCSVfile(p, saveFile.FileName);
                    }
                    catch (CSVexception messageErreurSavePOI)
                    {
                        MessageBox.Show(messageErreurSavePOI.Message, "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
                        erreurExport = true;
                    }
                    if (!erreurExport)
                        MessageBox.Show("Exportation réussie", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("L'élément sélectionné n'est pas un POI", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuFileTrajetImport_Click(object sender, RoutedEventArgs e)
        {
            bool ouvertureOK = true;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Fichier CSV files(*.csv) | *.csv";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFile.ShowDialog();

            try
            {
                MyPersonalMapData.readCSVtrajet(PersonneConnectee, openFile.FileName);
            }
            catch(CSVexception messageErreurCSV)
            {
                MessageBox.Show(messageErreurCSV.Message, "ERREUR OUVERTURE", MessageBoxButton.OK, MessageBoxImage.Error);
                ouvertureOK = false;
            }
            if(ouvertureOK)
            {
                UpdateListBox();
                TextDebug.Content = PersonneConnectee.ToString();
                MessageBox.Show("OUVERTURE REUSSIE", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MenuFileTrajetExport_Click(object sender, RoutedEventArgs e)
        {
            bool erreurExport = false;
            ICartoObj o = (ICartoObj)ListBox.SelectedItem;
            if (o == null)
            {                
                MessageBox.Show("Aucun élément de la ListBox est sélectionné", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);                
            }
            else
            {                
                if (o is Polyline)
                {
                    Polyline p = o as Polyline;

                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    saveFile.Filter = "Fichier CSV files(*.csv) | *.csv";
                    saveFile.ShowDialog();

                    try
                    {
                        MyPersonalMapData.saveCSVtrajet(p, saveFile.FileName);
                    }
                    catch(CSVexception messageErreurSaveTrajet)
                    {
                        MessageBox.Show(messageErreurSaveTrajet.Message, "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
                        erreurExport = true;
                    }
                    if (!erreurExport)
                        MessageBox.Show("Exportation réussie", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("L'élément sélectionné n'est pas un trajet (polyline)", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);                
            }            
        }
        #endregion

        #region ToolBar
        private void DeleteSelectItem_Click(object sender, RoutedEventArgs e)
        {
            bool suppressionOK = false;
            ICartoObj o = (ICartoObj)ListBox.SelectedItem;
            if (o == null)
            {
                suppressionOK = true;
                MessageBox.Show("Aucun élément de la ListBox est sélectionné", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (o is POI)
                {
                    POI p = o as POI;
                    foreach (ICartoObj oInCollection in PersonneConnectee.ObservableCollection)
                    {
                        if (oInCollection is POI)
                        {
                            POI poiInCollection = oInCollection as POI;
                            if (poiInCollection.Id == p.Id)
                            {
                                if (PersonneConnectee.ObservableCollection.Remove(poiInCollection))
                                {
                                    MessageBox.Show("Suppression du POI OK", "", MessageBoxButton.OK, MessageBoxImage.Information);
                                    suppressionOK = true;
                                }
                                break;
                            }
                        }
                    }
                }
                if (o is Polyline)
                {
                    Polyline p = o as Polyline;
                    foreach (ICartoObj oInCollection in PersonneConnectee.ObservableCollection)
                    {
                        if (oInCollection is Polyline)
                        {
                            Polyline polylineInCollection = oInCollection as Polyline;
                            if (polylineInCollection.Id == p.Id)
                            {
                                if (PersonneConnectee.ObservableCollection.Remove(polylineInCollection))
                                {
                                    MessageBox.Show("Suppression du polyline OK", "", MessageBoxButton.OK, MessageBoxImage.Information);
                                    suppressionOK = true;
                                }
                                break;
                            }
                        }
                    }

                }
                if (o is Polygon)
                {
                    Polygon p = o as Polygon;
                    foreach (ICartoObj oInCollection in PersonneConnectee.ObservableCollection)
                    {
                        if (oInCollection is Polygon)
                        {
                            Polygon polygonInCollection = oInCollection as Polygon;
                            if (polygonInCollection.Id == p.Id)
                            {
                                if (PersonneConnectee.ObservableCollection.Remove(polygonInCollection))
                                {
                                    MessageBox.Show("Suppression du polyline OK", "", MessageBoxButton.OK, MessageBoxImage.Information);
                                    suppressionOK = true;
                                }
                                break;
                            }
                        }
                    }
                }

                UpdateListBox();
                TextDebug.Content = PersonneConnectee.ToString();

            }

            if (!suppressionOK)
                MessageBox.Show("La suppresion à échouée", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void tbDetails_Click(object sender, RoutedEventArgs e)
        {            
            ICartoObj o = (ICartoObj)ListBox.SelectedItem;
            string titreFenetre = "", messageFenetre = ""; 
            if (o == null)
            {                
                MessageBox.Show("Aucun élément de la ListBox est sélectionné", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (o is POI)
                {
                    POI p = o as POI;
                    titreFenetre = "Caractéristiques détaillées du POI sélectionné";
                    messageFenetre = p.Draw();
                }
                if (o is Polyline)
                {
                    Polyline p = o as Polyline;
                    titreFenetre = "Caractéristiques détaillées du trajet (Polyline) sélectionné";
                    messageFenetre = p.Draw();
                }
                if (o is Polygon)
                {
                    Polygon p = o as Polygon;
                    titreFenetre = "Caractéristiques détaillées de la surface (Polygon) sélectionné";
                    messageFenetre = p.Draw();
                }
                MessageBox.Show(messageFenetre, titreFenetre, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void tbModifier_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region METHODES
        private void UpdateListBox()
        {
            ListBox.Items.Clear();
            foreach (ICartoObj o in PersonneConnectee.ObservableCollection)
            {
                if (o is POI)
                {
                    POI p = o as POI;
                    //ListBox.Items.Add("POI: " + p.Id + " / " + p.Description);                    
                    ListBox.Items.Add(p);
                }
                if (o is Polyline)
                {
                    Polyline p = o as Polyline;
                    //ListBox.Items.Add("Trajet: " + p.Id);
                    ListBox.Items.Add(p);
                }
                if (o is Polygon)
                {
                    Polygon p = o as Polygon;
                    //ListBox.Items.Add("Surface: " + p.Id);
                    ListBox.Items.Add(p);
                }
            }
        }       
        #endregion        
    }
}
