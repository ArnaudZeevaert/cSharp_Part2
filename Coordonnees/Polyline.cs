using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace MyCartographyObjects
{
    public class Polyline : CartoObj, IPointy
    {
        #region VARIABLES MEMBRES
        private Coordonnees[] _collectionDeCoordonnees;
        private Color _couleur;
        private int _epaisseur;
        private int _nbPoints;
        #endregion

        #region PROPRIETE
        public Coordonnees[] CollectionDeCoordonnes
        {
            set { 
                    _collectionDeCoordonnees = value;
                    CompteurDeCoordonneesDifferentes();
                }
            get { return _collectionDeCoordonnees; }
        }
        public Color Couleur
        {
            set { _couleur = value; }
            get { return _couleur; }
        }
        public int Epaisseur
        {
            set { _epaisseur = value; }
            get { return _epaisseur; }
        }

        public int GetNbPoints()
        { return _nbPoints; }
        #endregion

        #region CONSTRUCTEURS
        public Polyline() : this(null, Colors.Black, 1)
        {

        }
        public Polyline(Coordonnees[] newCoordonnees, Color newColeur, int newEpaisseur) : base()
        {
            CollectionDeCoordonnes = newCoordonnees;
            Couleur = newColeur;
            Epaisseur = newEpaisseur;
            CompteurDeCoordonneesDifferentes();
        }
        #endregion

        #region METHODE
        public override string ToString()
        {
            return "Collection de coordonnees --> voir Draw" + " / Couleur : " + Couleur + string.Format(" / Epaisseur: {0}", Epaisseur);
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
            if (CollectionDeCoordonnes == null) { Console.WriteLine("Pas de Coordonnees (CompteurDeCoordonneesDifferentes, POLYLINE)"); }
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
        #endregion
    }
}
