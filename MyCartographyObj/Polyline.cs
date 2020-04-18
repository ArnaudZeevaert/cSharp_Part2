using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MathUtil;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyCartographyObj
{
    [Serializable]
    public class Polyline : CartoObj, IPointy, IisPointClose, IComparable<Polyline>, IEquatable<Polyline>, ICartoObj, INotifyPropertyChanged
    {
        #region VARIABLES MEMBRES
        private List<Coordonnees> _collectionDeCoordonnees;
        private string _couleur;
        private int _epaisseur;
        private int _nbPoints;
        private string _nomTrajet;
        #endregion

        #region PROPRIETE
        public List<Coordonnees> CollectionDeCoordonnes
        {
            set
            {
                if(value != _collectionDeCoordonnees)
                {
                    if(value == null)
                    {
                        _collectionDeCoordonnees = new List<Coordonnees>();
                        OnPropertyChanged();
                    }
                    else
                    {
                        _collectionDeCoordonnees = value;
                        OnPropertyChanged();
                    }
                }
            }
            get { return _collectionDeCoordonnees; }
        }
        public Color Couleur
        {
            set 
            { 
                _couleur = value.ToString();
                OnPropertyChanged();
            }
            get { return (Color)ColorConverter.ConvertFromString(_couleur); }
        }
        public string CouleurString
        {
            set 
            { 
                _couleur = value;
                OnPropertyChanged();
            }
            get { return _couleur; }
        }
        public int Epaisseur
        {
            set 
            { 
                _epaisseur = value;
                OnPropertyChanged();
            }
            get { return _epaisseur; }
        }
        public string NomTrajet
        {
            set 
            { 
                _nomTrajet = value;
                OnPropertyChanged();
            }
            get { return _nomTrajet; }
        }

        public int GetNbPoints()
        { return _nbPoints; }
        #endregion

        #region CONSTRUCTEURS
        public Polyline() : this(new List<Coordonnees>(), Colors.Black, 1)
        {

        }
        public Polyline(List<Coordonnees> newCoordonnees, Color newColeur, int newEpaisseur) : base()
        {
            CollectionDeCoordonnes = newCoordonnees;
            Couleur = newColeur;
            Epaisseur = newEpaisseur;
            iDeCoordonneesDifferentes();
        }
        #endregion

        #region METHODE
        public override string ToString()
        {
            return string.Format("id({0})", Id) + " Nom trajet : " + NomTrajet;            
        }
        public override string Draw()
        {
            string collectionEnUneLigne = "";
            foreach (CartoObj o in CollectionDeCoordonnes)
            {
                collectionEnUneLigne += "\n\t" + o.Draw();
            }
            collectionEnUneLigne += "\n";
            return this.ToString() + " / Couleur : " + Couleur + string.Format(" / Epaisseur: {0}", Epaisseur) + collectionEnUneLigne;
        }
        private void iDeCoordonneesDifferentes()
        {
            int i = 0;
            if (CollectionDeCoordonnes == null) { Console.WriteLine("Pas de Coordonnees (iDeCoordonneesDifferentes, POLYLINE)"); }
            else
            {
                List<int> id_differents = new List<int>();
                foreach (Coordonnees c in CollectionDeCoordonnes)
                {
                    var value = id_differents;
                    if (!ContainsValue(value, c.Id))//si l'id du point de coordonnées n'est pas dans la liste, --> incrémenter le nombre de points
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
            int i;
            bool ret_val;
            for (i = 0; i < (CollectionDeCoordonnes.Count - 1); i++)
            {
                Coordonnees pointA = new Coordonnees(this.CollectionDeCoordonnes[i].Latitude, this.CollectionDeCoordonnes[i].Longitude);
                Coordonnees pointB = new Coordonnees(this.CollectionDeCoordonnes[i+1].Latitude, this.CollectionDeCoordonnes[i+1].Longitude);
                
                ret_val = CartoObj.IsPointClosePourUneDroiteAB(coordonneesPoint, pointA, pointB, precision);

                if (ret_val) { return true; }  
            }
            //si on arrive ici c'est que le point n'est pas proche d'aucune droite de la collection
            return false;
        }
        public double surfaceBoundingBox()
        {
            double valMax_X = 0, valMax_Y = 0, valMin_X, valMin_Y;
            int i;
            for (i = 0; i < CollectionDeCoordonnes.Count; i++)
            {
                if (valMax_X < CollectionDeCoordonnes[i].Latitude)
                    valMax_X = CollectionDeCoordonnes[i].Latitude;
                if (valMax_Y < CollectionDeCoordonnes[i].Longitude)
                    valMax_Y = CollectionDeCoordonnes[i].Longitude;
            }

            valMin_X = valMax_X;
            valMin_Y = valMax_Y;
            for (i = 0; i < CollectionDeCoordonnes.Count; i++)
            {
                if (valMin_X > CollectionDeCoordonnes[i].Latitude)
                    valMin_X = CollectionDeCoordonnes[i].Latitude;
                if (valMin_Y > CollectionDeCoordonnes[i].Longitude)
                    valMin_Y = CollectionDeCoordonnes[i].Longitude;
            }

            double [] longueurCote = new double[4];
            Coordonnees coordonneesA = new Coordonnees(valMin_X, valMax_Y);
            Coordonnees coordonneesB = new Coordonnees(valMax_X, valMax_Y);
            Coordonnees coordonneesC = new Coordonnees(valMin_X, valMin_Y);
            Coordonnees coordonneesD = new Coordonnees(valMax_X, valMin_Y);
            longueurCote[0] = MathUtilFct.LongueurD_unSegment(coordonneesA.Latitude, coordonneesA.Longitude, coordonneesB.Latitude, coordonneesB.Longitude);//cote A-B
            longueurCote[1] = MathUtilFct.LongueurD_unSegment(coordonneesB.Latitude, coordonneesB.Longitude, coordonneesD.Latitude, coordonneesD.Longitude);//cote B-D
            longueurCote[2] = MathUtilFct.LongueurD_unSegment(coordonneesD.Latitude, coordonneesD.Longitude, coordonneesC.Latitude, coordonneesC.Longitude);//cote D-C
            longueurCote[3] = MathUtilFct.LongueurD_unSegment(coordonneesC.Latitude, coordonneesC.Longitude, coordonneesA.Latitude, coordonneesA.Longitude);//cote C-A

            double plusGrandCote=longueurCote[0], plusPetitCote;
            for(i = 1; i<4; i++)
            {                
                if(plusGrandCote < longueurCote[i])
                {
                    plusGrandCote = longueurCote[i];
                }
            }
            plusPetitCote = plusGrandCote;
            for (i = 0; i < 4; i++)
            {
                if (plusPetitCote > longueurCote[i])
                {
                    plusPetitCote = longueurCote[i];
                }
            }

            double surfaceBoundingBox;
            surfaceBoundingBox = plusPetitCote * plusGrandCote;
            
            return surfaceBoundingBox;
        }
        public double longueurPolyline()
        {
            int i = 0;
            double somme = 0;

            for(i=0; i<(CollectionDeCoordonnes.Count-1); i++)
            {                
                somme = somme + MathUtilFct.LongueurD_unSegment(this.CollectionDeCoordonnes[i].Latitude, this.CollectionDeCoordonnes[i].Longitude, this.CollectionDeCoordonnes[i + 1].Latitude, this.CollectionDeCoordonnes[i + 1].Longitude);               
            }
            
            return somme;
        }
        public int CompareTo(Polyline other)
        {            
            return longueurPolyline().CompareTo(other.longueurPolyline());
        }

        public bool Equals(Polyline other)
        {
            if(other != null)
            {
                if (this.Id == other.Id && this.CollectionDeCoordonnes == other.CollectionDeCoordonnes && this.Couleur == other.Couleur && this.Epaisseur == other.Epaisseur)
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        #endregion
    }
}
