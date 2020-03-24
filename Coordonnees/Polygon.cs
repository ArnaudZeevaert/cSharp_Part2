using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace MyCartographyObjects
{
    public class Polygon : CartoObj, IPointy, IisPointClose
    {
        #region VARIABLE MEMBRES
        private Coordonnees[] _collectionDeCoordonnees;
        private Color _couleurDeRemplissage;
        private Color _couleurDeContour;
        private double _opacite;
        private int _nbPoints;
        #endregion

        #region PROPRIETE
        public Coordonnees[] CollectionDeCoordonnes
        {
            set
            {
                _collectionDeCoordonnees = value;
                CompteurDeCoordonneesDifferentes();
            }
            get { return _collectionDeCoordonnees; }
        }
        public Color CouleurDeRemplissage
        {
            set { _couleurDeRemplissage = value; }
            get { return _couleurDeRemplissage; }
        }
        public Color CouleurDeContour
        {
            set { _couleurDeContour = value; }
            get { return _couleurDeContour; }
        }
        public double Opacite
        {
            set
            {
                if (value >= 0 && value <= 1)
                {
                    _opacite = value;
                }
            }
            get { return _opacite; }
        }

        public int GetNbPoints()
        { return _nbPoints; }
        #endregion

        #region CONSTRUCTEURS
        public Polygon(): this(null, Colors.Red, Colors.White, 0)
        {

        }
        public Polygon(Coordonnees[] newCoordonnees, Color newCouleurDeRemplissage, Color newCouleurDeContour, double newOpacite)
        {
            CollectionDeCoordonnes = newCoordonnees;
            CouleurDeRemplissage = newCouleurDeRemplissage;
            CouleurDeContour = newCouleurDeContour;
            if (newOpacite >= 0 && newOpacite <= 1)
            {
                Opacite = newOpacite;
            }
            else
                Opacite = 0;

            Opacite = newOpacite;
        }
        #endregion

        #region METHODES
        public override string ToString()
        {
            return "Collection de coordonnees --> voir Draw" + " / Couleur de remplissage : " + CouleurDeRemplissage + " / Couleur de Contour : " + CouleurDeContour + string.Format(" / Opacite: {0}", Opacite);
        }
        public override void Draw()
        {
            Console.WriteLine(this.ToString());
            if (CollectionDeCoordonnes == null) { Console.WriteLine("Pas de Coordonnees"); }
            else
            {
                int i = 0;
                foreach (Coordonnees c in CollectionDeCoordonnes)
                {
                    Console.WriteLine(string.Format("Coordonnees({0})", i) + string.Format(" (Lat/Long) ({0:0.000}/{1:0.000})", c.Latitude, c.Longitude));
                    i++;
                }
            }
        }
        private void CompteurDeCoordonneesDifferentes()
        {
            int i = 0;
            if (CollectionDeCoordonnes == null) { Console.WriteLine("Pas de Coordonnees (CompteurDeCoordonneesDifferentes, POLYGON)"); }
            else
            {
                List<int> id_differents = new List<int>();
                foreach (Coordonnees c in CollectionDeCoordonnes)
                {
                    var value = id_differents;
                    if (!ContainsValue(value, c.Id))
                    {
                        i++;
                    }
                    id_differents.Add(c.Id);
                }
            }
            _nbPoints = i;
        }

        public bool IsPointClose(Coordonnees coordonneesPoint, double precision)
        {
            return true;
        }
        #endregion

    }
}
