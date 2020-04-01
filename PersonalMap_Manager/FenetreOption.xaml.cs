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
using System.Windows.Shapes;

using System.Windows.Controls.Primitives;

namespace PersonalMap_Manager
{
    /// <summary>
    /// Interaction logic for FenetreOption.xaml
    /// </summary>
    public partial class FenetreOption : Window
    {
        #region VARIABLES MEMEBRES
        private string _dossierDeTravail;
        private string _couleurFondListBox;
        private string _couleurTextListBox;
        public string NewDossierDeTravail;
        public string NewCouleurFondListBox;
        public string NewCoueleurTextListBox;
        #endregion

        #region CONSTRUCTEURS
        public FenetreOption()
        {
            InitializeComponent();
        }
        
        public FenetreOption(string dossierDeTravail, string couleurDeFond, string couleurText)
        {
            InitializeComponent();
            _dossierDeTravail = dossierDeTravail;
            _couleurFondListBox = couleurDeFond;
            _couleurTextListBox = couleurText;            
        }
        #endregion

        private void MainHeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }
        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
