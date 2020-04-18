using System;
using System.Windows.Media;

namespace PersonalMap_Manager
{
    public class OptionsEventArgs : EventArgs
    {
        public Brush CouleurDeFond { get; set; }
        public Brush CouleurText { get; set; }
        public string DossierDeTravail { get; set; }
    }   
}
