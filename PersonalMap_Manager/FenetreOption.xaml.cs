using System.Windows;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Drawing;
using Color = System.Windows.Media.Color;
using Brush = System.Windows.Media.Brush;
using ColorConverter = System.Windows.Media.ColorConverter;
using Polyline = MyCartographyObj.Polyline;

namespace PersonalMap_Manager
{
    /// <summary>
    /// Interaction logic for FenetreOption.xaml
    /// </summary>
    public partial class FenetreOption : Window, INotifyPropertyChanged
    {

        #region VARIABLES MEMEBRES
        private string _dossierDeTravailDepart;
        private string _dossierDeTravail;
        private Brush _couleurFondListBoxDepart;
        private Brush _couleurFondListBox;
        private Brush _couleurTextListBoxDepart;
        private Brush _couleurTextListBox;

        private string _couleurFondSelectionne;
        private string _couleurTextSelectionne;
        #endregion

        #region PROPRIETES
        public string CouleurFondSelectionne
        {
            set
            {
                _couleurFondSelectionne = value;
                OnPropertyChanged();
            }
            get { return _couleurFondSelectionne; }
        }
        public string CouleurTextSelectionne
        {
            set
            {
                _couleurTextSelectionne = value;
                OnPropertyChanged();
            }
            get { return _couleurTextSelectionne; }
        }
        public string DossierDeTravail
        {
            set
            {
                if (value != _dossierDeTravail)
                {
                    _dossierDeTravail = value;
                    OnOptionsChangements();
                }
            }
            get { return _dossierDeTravail; }
        }
        public Brush CouleurFondListBox
        {
            set
            {
                _couleurFondListBox = value;
                OnOptionsChangements();
            }
            get { return _couleurFondListBox; }
        }
        public Brush CouleurTextListBox
        {
            set
            {
                _couleurTextListBox = value;
                OnOptionsChangements();
            }
            get { return _couleurTextListBox; }
        }
        #endregion
               
        #region CONSTRUCTEURS        
        public FenetreOption(string dossierDeTravail, Brush couleurDeFond, Brush couleurText)
        {
            InitializeComponent();

            DataContext = this;

            DossierDeTravail = dossierDeTravail;
            CouleurFondListBox = couleurDeFond;
            CouleurTextListBox = couleurText;

            _dossierDeTravailDepart = dossierDeTravail;
            _couleurFondListBoxDepart = couleurDeFond;
            _couleurTextListBoxDepart = couleurText;

            dossierDeTravailLabel.Content = DossierDeTravail;

            //remplissage des possibilités de couleur des comboBox 
            foreach (PropertyInfo property in typeof(System.Drawing.Color).GetProperties(BindingFlags.Static | BindingFlags.Public))
            {
                if (property.PropertyType == typeof(System.Drawing.Color))
                {
                    couleurDeFondCombo.Items.Add(property.Name);
                    couleurDeTextCombo.Items.Add(property.Name);
                }
            }
                        
            CouleurFondSelectionne = FenetrePrincipale.GetBrushName((SolidColorBrush)CouleurFondListBox);            
            CouleurTextSelectionne = FenetrePrincipale.GetBrushName((SolidColorBrush)CouleurTextListBox );
        }
        #endregion

        #region CLIQUES
        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ModifierDossierDeTravail_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            CouleurFondListBox = _couleurFondListBoxDepart;
            CouleurTextListBox = _couleurTextListBoxDepart;
            DossierDeTravail = _dossierDeTravailDepart;

            dossierDeTravailLabel.Content = DossierDeTravail;

            CouleurFondSelectionne = FenetrePrincipale.GetBrushName((SolidColorBrush)CouleurFondListBox);
            CouleurTextSelectionne = FenetrePrincipale.GetBrushName((SolidColorBrush)CouleurTextListBox);           
        }
        private void ButtonAppliquer_Click(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }
        private void ButtunOk_Click(object sender, RoutedEventArgs e)
        {
            UpdateData();

            Close();
        }
        #endregion

        #region METHODES
        private void MainHeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }
        private void UpdateData()
        {            
            try
            {
                //object tmp = couleurDeFondCombo.SelectedValue;
                //Color color = (Color)ColorConverter.ConvertFromString(tmp.ToString());                       
                CouleurFondListBox = (Brush)new BrushConverter().ConvertFromString(CouleurFondSelectionne);
            }
            catch (Exception e)
            {
                MessageBox.Show("(UpdateData)Erreur encodage de la couleur de fond :\n" + e.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }           

           
            try
            {
                //object tmp = couleurDeTextCombo.SelectedValue;
                //Color color = (Color)ColorConverter.ConvertFromString(tmp.ToString());
                CouleurTextListBox = (Brush)new BrushConverter().ConvertFromString(CouleurTextSelectionne);
            }
            catch (Exception e)
            {
                MessageBox.Show("(UpdateData)Erreur encodage de la couleur de text :\n" + e.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }
        #endregion

        #region EVENT
        public delegate void OptionsEventHandler(object source, OptionsEventArgs args);

        public event OptionsEventHandler OptionsChangements;

        protected virtual void OnOptionsChangements()
        {
            if(OptionsChangements != null)
            {
                OptionsEventArgs args = new OptionsEventArgs();
                args.CouleurDeFond = CouleurFondListBox;
                args.CouleurText = CouleurTextListBox;
                args.DossierDeTravail = DossierDeTravail;
                OptionsChangements(this, args);
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion        
    }
}
