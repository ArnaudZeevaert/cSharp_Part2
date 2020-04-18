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

using Microsoft.Maps.MapControl.WPF;

using Polygon = MyCartographyObj.Polygon;
using Polyline = MyCartographyObj.Polyline;
using System.Reflection;

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
        private Brush _couleurFondListBox = null;
        private Brush _couleurTextListBox = null;            

        private bool _stopAddPoi = false;
        private bool _addPolylineEnCour = false;
        private Polyline _polylineEnCour = null;
        private FenetreOption _fenetreOption = null;
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
        public Brush CouleurFondListBox
        {
            set 
            {
                if(value != _couleurFondListBox)
                {
                    _couleurFondListBox = value;
                }
            }
            get 
            {
                if (_couleurFondListBox == null)
                    return Brushes.White;
                else
                    return _couleurFondListBox; 
            }
        }
        public Brush CouleurTextListBox
        {
            set
            {
                if (value != _couleurTextListBox)
                {
                    _couleurTextListBox = value;
                }
            }
            get 
            {
                if (_couleurTextListBox == null)
                    return Brushes.Black;
                else
                    return _couleurTextListBox; 
            }
        }

        public string DossierDeTravail
        {
            get 
            {
                if (_dossierDeTravail == null)
                    return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                else
                    return _dossierDeTravail; 
            }
            set
            {
                if (_dossierDeTravail != value)
                    _dossierDeTravail = value;
            }
        }
        #endregion

        #region CONSTRUCTEURS
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            UpdateListBox();
            Carte.Focus();
            Carte.Mode = new AerialMode(true);               

            tbStopAdd.Visibility = Visibility.Collapsed;
        }
        public FenetrePrincipale()
        {           
            InitializeComponent();
            PersonneConnectee = new MyPersonalMapData();                                   
        }

        public FenetrePrincipale(MyPersonalMapData personne)
        {            
            InitializeComponent();
            PersonneConnectee = personne;
        }
        #endregion

        #region MAP
        private void Map_DoubleClick(object sender, MouseEventArgs e)
        {
            e.Handled = true;
            if (comboNewElem.SelectedItem != null)//si on a sélectionné qqch dans la comboBox
            {
                //Get the mouse click coordinates
                Point mousePosition = e.GetPosition(Carte);
                //Convert the mouse coordinates to a locatoin on the map
                Location location = Carte.ViewportPointToLocation(mousePosition);

                if (comboNewElem.SelectedItem.ToString().Contains("POI"))
                {
                    POI p = new POI(location.Latitude, location.Longitude, "");

                    fenetrePOI fenetrePOI = new fenetrePOI(p);
                    fenetrePOI.Owner = this;
                    fenetrePOI.ShowDialog();
                    if (fenetrePOI.NewPOI != null)
                    {                                                                        
                        p.Latitude = fenetrePOI.NewPOI.Latitude;
                        p.Longitude = fenetrePOI.NewPOI.Longitude;
                        p.Description = fenetrePOI.NewPOI.Description;
                        
                        PersonneConnectee.ObservableCollection.Add(p);

                        Carte.Children.Clear();
                        afficherPOI(p);
                        UpdateListBox();                        
                    }
                    
                }
                else if(comboNewElem.SelectedItem.ToString().Contains("POLYLINE"))
                {
                    if(!_addPolylineEnCour)
                    {
                        _polylineEnCour = new Polyline();
                        _addPolylineEnCour = true;
                        Carte.Children.Clear();
                    }
                    
                    tbStopAdd.Visibility = Visibility.Visible;
                    if(!_stopAddPoi)
                    {
                        //ajouter des poi au trajet
                        POI p = new POI(location.Latitude, location.Longitude, "");

                        fenetrePOI fenetrePOI = new fenetrePOI(p);
                        fenetrePOI.Owner = this;
                        fenetrePOI.ShowDialog();
                        if (fenetrePOI.NewPOI != null)
                        {
                            p.Latitude = fenetrePOI.NewPOI.Latitude;
                            p.Longitude = fenetrePOI.NewPOI.Longitude;
                            p.Description = fenetrePOI.NewPOI.Description;

                            _polylineEnCour.CollectionDeCoordonnes.Add(p);

                            afficherPOI(p);                           
                        }
                    }
                    //else --> dans tbStopAdd_Click
                } 
            }
                        
        }
        private void afficherTrajet(Polyline trajet)
        {
            MapPolyline mapPolyline = new MapPolyline();
            mapPolyline.Opacity = 0.7;
            mapPolyline.StrokeThickness = trajet.Epaisseur;
            SolidColorBrush couleurTrajet = new SolidColorBrush(trajet.Couleur);
            mapPolyline.Stroke = couleurTrajet;
            mapPolyline.Locations = new LocationCollection();
            foreach (CartoObj o in trajet.CollectionDeCoordonnes)
            {
                if (o is POI)
                {
                    POI p = o as POI;
                    Pushpin pushpin = new Pushpin();
                    pushpin.ToolTip = p.Draw();
                    pushpin.Opacity = 0.7;
                    pushpin.Location = new Location(p.Latitude, p.Longitude);
                    pushpin.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                    Carte.Children.Add(pushpin);
                    mapPolyline.Locations.Add(new Location(p.Latitude, p.Longitude));
                }
            }
            Carte.Children.Add(mapPolyline);
        }
        private void afficherPOI(POI pOI)
        {
            Pushpin pushpin = new Pushpin();
            pushpin.ToolTip = pOI.Draw();
            pushpin.Opacity = 0.7;
            pushpin.Location = new Location(pOI.Latitude, pOI.Longitude);
            pushpin.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            Carte.Children.Add(pushpin);
        }
        private void tbStopAdd_Click(object sender, RoutedEventArgs e)
        {
            _stopAddPoi = true;

            if (comboNewElem.SelectedItem.ToString().Contains("POLYLINE"))
            {
                Carte.Children.Clear();
                //afficher le trajet dans la fentetre de polyline                   
                FenetrePolyline fenetrePolyline = new FenetrePolyline(_polylineEnCour);
                fenetrePolyline.Owner = this;
                fenetrePolyline.ShowDialog();
                if (fenetrePolyline.NewPolyline != null)//l'ajouter à la collection si on clique sur ok
                {
                    _polylineEnCour.Epaisseur = fenetrePolyline.NewPolyline.Epaisseur;
                    _polylineEnCour.Couleur = fenetrePolyline.NewPolyline.Couleur;
                    _polylineEnCour.NomTrajet = fenetrePolyline.NewPolyline.NomTrajet;
                    _polylineEnCour.CollectionDeCoordonnes = new List<Coordonnees>(fenetrePolyline.NewPolyline.CollectionDeCoordonnes);
                    PersonneConnectee.ObservableCollection.Add(_polylineEnCour);

                    afficherTrajet(_polylineEnCour);

                    UpdateListBox();                    
                }

                _stopAddPoi = false;
                tbStopAdd.Visibility = Visibility.Collapsed;
                _addPolylineEnCour = false;
                _polylineEnCour = null;
            }
        }
        #endregion

        #region Fermeture Fenetre
        private void Window_Closed(object sender, EventArgs e)
        {
            QuitterSauvegarde();
            if (_fenetreOption != null) _fenetreOption.Close();
        }
        private void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            QuitterSauvegarde();
            if (_fenetreOption != null) _fenetreOption.Close();
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

        #region EVENT
        public void OnOptionsChangements(object source, OptionsEventArgs args)
        {            
            try
            {
                ListBox.Background = args.CouleurDeFond;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERREUR SET BACKGROUND LISTBOX", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                        
            try
            {
                ListBox.Foreground = args.CouleurText;             
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERREUR SET FOREGROUND LISTBOX", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.DossierDeTravail = args.DossierDeTravail;
            //ListBox.Background = GetCouleurFondListBox;
            //SetFromStringCouleurFondListBox = args.CouleurDeFond;
            //SetFromStringCouleurTextListBox = args.CouleurText;
            //DossierDeTravail = args.DossierDeTravail;
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
                MessageBox.Show("SAUVEGARDE REUSSIE", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void MenuToolsOptions_Click(object sender, RoutedEventArgs e)
        {
            if(_fenetreOption == null)
            {
                _fenetreOption = new FenetreOption(DossierDeTravail, CouleurFondListBox, CouleurTextListBox);
                _fenetreOption.Show();
                _fenetreOption.OptionsChangements += this.OnOptionsChangements;
            }
            else
            {
                if (!_fenetreOption.IsLoaded)
                {
                    _fenetreOption = new FenetreOption(DossierDeTravail, CouleurFondListBox, CouleurTextListBox);
                    _fenetreOption.Show();
                    _fenetreOption.OptionsChangements += this.OnOptionsChangements;
                }
                else
                    _fenetreOption.Focus();
            }
        }

        private void MenuToolsAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutBox aboutBox = new AboutBox(PersonneConnectee);
            aboutBox.ShowDialog();
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
                Carte.Children.Clear();
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
            bool modificationOK = false;
            ICartoObj o = (ICartoObj)ListBox.SelectedItem;
            if (o == null)
            {
                modificationOK = true;
                MessageBox.Show("Aucun élément de la ListBox est sélectionné", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Carte.Children.Clear();
                if (o is POI)
                {
                    POI p = o as POI;                    

                    fenetrePOI fenetrePOI = new fenetrePOI(p);
                    fenetrePOI.Owner = this;
                    fenetrePOI.ShowDialog();
                    if (fenetrePOI.NewPOI != null)
                    {
                        foreach (ICartoObj oInCollection in PersonneConnectee.ObservableCollection)
                        {
                            if (oInCollection is POI)
                            {
                                POI poiInCollection = oInCollection as POI;
                                if (poiInCollection.Id == p.Id)
                                {
                                    if (PersonneConnectee.ObservableCollection.Remove(poiInCollection))
                                    {
                                        POI newPOI = new POI();
                                        newPOI.Id = p.Id;
                                        newPOI.Latitude = fenetrePOI.NewPOI.Latitude;
                                        newPOI.Longitude = fenetrePOI.NewPOI.Longitude;
                                        newPOI.Description = fenetrePOI.NewPOI.Description;
                                        Console.WriteLine("DEBUG newPoi to string : " + newPOI.Draw());
                                        PersonneConnectee.ObservableCollection.Add(newPOI);
                                        modificationOK = true;
                                    }
                                    break;
                                }
                            }
                        }                       
                    }
                    else modificationOK = true;                    
                }
                if (o is Polyline)
                {
                    Polyline p = o as Polyline;

                    Console.WriteLine("DEBUG polyline dans modifier fenetre principale:\n" + p.Draw());

                    FenetrePolyline fenetrePolyline = new FenetrePolyline(p);
                    fenetrePolyline.Owner = this;
                    fenetrePolyline.ShowDialog();
                    if (fenetrePolyline.NewPolyline != null)
                    {
                        foreach (ICartoObj oInCollection in PersonneConnectee.ObservableCollection)
                        {
                            if (oInCollection is Polyline)
                            {
                                Polyline polylineInCollection = oInCollection as Polyline;
                                if (polylineInCollection.Id == p.Id)
                                {
                                    if (PersonneConnectee.ObservableCollection.Remove(polylineInCollection))
                                    {
                                        Polyline newPolyline = new Polyline();
                                        newPolyline.Id = p.Id;
                                        newPolyline.Epaisseur = fenetrePolyline.NewPolyline.Epaisseur;
                                        newPolyline.Couleur = fenetrePolyline.NewPolyline.Couleur;
                                        newPolyline.NomTrajet = fenetrePolyline.NewPolyline.NomTrajet;
                                        newPolyline.CollectionDeCoordonnes = new List<Coordonnees>(fenetrePolyline.NewPolyline.CollectionDeCoordonnes);                                        
                                        PersonneConnectee.ObservableCollection.Add(newPolyline);
                                        modificationOK = true;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else modificationOK = true;
                }                

                //}
                //if (o is Polygon)
                //{
                //    Polygon p = o as Polygon;
                //    foreach (ICartoObj oInCollection in PersonneConnectee.ObservableCollection)
                //    {
                //        if (oInCollection is Polygon)
                //        {
                //            Polygon polygonInCollection = oInCollection as Polygon;
                //            if (polygonInCollection.Id == p.Id)
                //            {
                //                if (PersonneConnectee.ObservableCollection.Remove(polygonInCollection))
                //                {
                //                    MessageBox.Show("Suppression du polyline OK", "", MessageBoxButton.OK, MessageBoxImage.Information);
                //                    suppressionOK = true;
                //                }
                //                break;
                //            }
                //        }
                //    }
                //}
                UpdateListBox();                
            }
            
            if (!modificationOK)
            {
                MessageBox.Show("La modification à échouée", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tbAfficher_Click(object sender, RoutedEventArgs e)
        {
            ICartoObj o = (ICartoObj)ListBox.SelectedItem;            
            if (o == null)
            {
                MessageBox.Show("Aucun élément de la ListBox est sélectionné", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Carte.Children.Clear();
                if (o is POI)
                {
                    POI p = o as POI;
                    afficherPOI(p);
                }
                if (o is Polyline)
                {
                    Polyline p = o as Polyline;
                    afficherTrajet(p);
                }
                //if (o is Polygon)
                //{
                //    Polygon p = o as Polygon;
                    
                //}                
            }
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
        public static string GetColorName(Color col)
        {
            PropertyInfo colorProperty = typeof(Colors).GetProperties()
                .FirstOrDefault(p => Color.AreClose((Color)p.GetValue(null), col));
            return colorProperty != null ? colorProperty.Name : "Black";
        }
        public static string GetBrushName(SolidColorBrush brush)
        {
            var results = typeof(Colors).GetProperties().Where(
             p => (Color)p.GetValue(null, null) == brush.Color).Select(p => p.Name);
            return results.Count() > 0 ? results.First() : "Black";
        }


        #endregion


    }
}
