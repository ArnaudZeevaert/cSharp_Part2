using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MathUtil;

namespace MyCartographyObj
{
    [Serializable]
    public class Polygon : CartoObj, IPointy, IisPointClose, ICartoObj
    {
        #region VARIABLE MEMBRES
        public List<Coordonnees> _collectionDeCoordonnees;
        private string _couleurDeRemplissage;
        private string _couleurDeContour;
        private double _opacite;
        private int _nbPoints;
        #endregion

        #region PROPRIETE
        public Color CouleurDeRemplissage
        {
            set { _couleurDeRemplissage = value.ToString(); }
            get { return (Color)ColorConverter.ConvertFromString(_couleurDeRemplissage); }
        }
        public string CouleurDeRemplissageString
        {
            set { _couleurDeRemplissage = value; }
            get { return _couleurDeRemplissage; }
        }
        public Color CouleurDeContour
        {
            set { _couleurDeContour = value.ToString(); }
            get { return (Color)ColorConverter.ConvertFromString(_couleurDeContour); }
        }
        public string CouleurDeContourString
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
        public Polygon() : this(new List<Coordonnees>(), Colors.Red, Colors.White, 0)
        {
            
        }
        public Polygon(List<Coordonnees> newCoordonnees, Color newCouleurDeRemplissage, Color newCouleurDeContour, double newOpacite)
        {
            _collectionDeCoordonnees = newCoordonnees;
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
            return base.ToString() + " Collection de coordonnees --> voir Draw" + " / Couleur de remplissage : " + CouleurDeRemplissage + " / Couleur de Contour : " + CouleurDeContour + string.Format(" / Opacite: {0}", Opacite);
        }
        public override void Draw()
        {
            Console.WriteLine(this.ToString());
            if (_collectionDeCoordonnees == null) { Console.WriteLine("Pas de Coordonnees"); }
            else
            {
                int i = 0;
                foreach (Coordonnees c in _collectionDeCoordonnees)
                {
                    Console.WriteLine(string.Format("Coordonnees({0})", i) + string.Format(" (Lat/Long) ({0:0.000}/{1:0.000})", c.Latitude, c.Longitude));
                    i++;
                }
            }
        }
        private void CompteurDeCoordonneesDifferentes()
        {
            int i = 0;
            if (_collectionDeCoordonnees == null) { Console.WriteLine("Pas de Coordonnees (CompteurDeCoordonneesDifferentes, POLYGON)"); }
            else
            {
                List<int> id_differents = new List<int>();
                foreach (Coordonnees c in _collectionDeCoordonnees)
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
            double valMax_X = 0, valMax_Y = 0, valMin_X, valMin_Y;
            int i;
            for (i = 0; i < _collectionDeCoordonnees.Count; i++)
            {
                if (valMax_X < _collectionDeCoordonnees[i].Latitude)
                    valMax_X = _collectionDeCoordonnees[i].Latitude;
                if (valMax_Y < _collectionDeCoordonnees[i].Longitude)
                    valMax_Y = _collectionDeCoordonnees[i].Longitude;
            }

            valMin_X = valMax_X;
            valMin_Y = valMax_Y;
            for (i = 0; i < _collectionDeCoordonnees.Count; i++)
            {
                if (valMin_X > _collectionDeCoordonnees[i].Latitude)
                    valMin_X = _collectionDeCoordonnees[i].Latitude;
                if (valMin_Y > _collectionDeCoordonnees[i].Longitude)
                    valMin_Y = _collectionDeCoordonnees[i].Longitude;
            }

            //coordonnées des sommets du rectangle ou carré (bouding Box)
            //A._________.B
            // |         |
            // |         |
            //C._________.D
            /*Coordonnees coordonneesA = new Coordonnees(valMin_X, valMax_Y);
            Coordonnees coordonneesB = new Coordonnees(valMax_X, valMax_Y);
            Coordonnees coordonneesC = new Coordonnees(valMin_X, valMin_Y);
            Coordonnees coordonneesD = new Coordonnees(valMax_X, valMin_Y);*/

            if (coordonneesPoint.Latitude <= valMax_X && coordonneesPoint.Latitude >= valMin_X)
            {
                if (coordonneesPoint.Longitude <= valMax_Y && coordonneesPoint.Longitude >= valMin_Y)
                {
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }
        #endregion

    }
}
