using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCartographyObj
{
    [Serializable]
    public abstract class CartoObj
    {
        #region VARIABLES MEMBRES   
        private int _id;
        private static int s_count;
        #endregion

        #region PROPRIETES
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        public static int Count
        {
            get { return s_count; }
        }
        #endregion

        #region CONSTRUCTEURS
        public CartoObj()
        {
            s_count++;

            Id = Count;
        }
        #endregion

        #region METHODES
        public virtual void Draw()
        {
            Console.WriteLine(this.ToString());
        }
        public override string ToString()
        {
            return string.Format("id({0})", Id);
        }

        //output : true si la valeur recherchée est dans la liste, false si elle n'est pas dans la liste
        public static bool ContainsValue(List<int> list, int valeurRecherchee)
        {
            if (list == null) { return false; }//si la liste est vide, la valeur recherchée est forcément pas présente
            else
            {
                foreach (int number in list)
                {
                    if (number == valeurRecherchee)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        protected static bool IsPointClosePourUneDroiteAB (Coordonnees coordonneesPoint, Coordonnees pointA, Coordonnees pointB, double precision)
        {
            //1 tester si le point recherché est proche de A ou de B ?
            if (pointA.IsPointClose(coordonneesPoint, precision)) { return true; }
            if (pointB.IsPointClose(coordonneesPoint, precision)) { return true; }

            //2 si le point recherché n'est pas proche du point A ou B, --> tester s'il est proche de la droite

            //recherche des max et min sur l'axe des x et y
            double maxLat, minLat, maxLong, minLong;
            //maxLat
            if (pointA.Latitude > pointB.Latitude) { maxLat = pointA.Latitude; }
            else { maxLat = pointB.Latitude; }

            //minLat
            if (pointA.Latitude < pointB.Latitude) { minLat = pointA.Latitude; }
            else { minLat = pointB.Latitude; }

            //maxLong
            if (pointA.Longitude > pointB.Longitude) { maxLong = pointA.Longitude; }
            else { maxLong = pointB.Longitude; }

            //minLong
            if (pointA.Longitude < pointB.Longitude) { minLong = pointA.Longitude; }
            else { minLong = pointB.Longitude; }

            //cas d'une droite verticale
            if (pointA.Latitude == pointB.Latitude)
            {
                Console.WriteLine("DEBUG IsPointClosePourUneDroiteAB, Droite verticale (maxLat{0}, minLat{1}, maxLong{2}, minLong{3})", maxLat, minLat, maxLong, minLong);

                if (coordonneesPoint.Longitude <= maxLong && coordonneesPoint.Longitude >= minLong)
                {
                    double differenceLatitude;
                    differenceLatitude = Math.Abs(pointA.Latitude - coordonneesPoint.Latitude);
                    if (differenceLatitude <= precision) { return true; }
                    else { return false; }
                }
                else { return false; }
            }

            //cas d'une droite horizontale
            if (pointA.Longitude == pointB.Longitude)
            {
                Console.WriteLine("DEBUG IsPointClosePourUneDroiteAB, Droite Horizontale (maxLat{0}, minLat{1}, maxLong{2}, minLong{3})", maxLat, minLat, maxLong, minLong);

                if (coordonneesPoint.Latitude <= maxLat && coordonneesPoint.Latitude >= minLat)
                {
                    double differenceLongitude;
                    differenceLongitude = Math.Abs(pointA.Longitude - coordonneesPoint.Longitude);
                    if (differenceLongitude <= precision) { return true; }
                    else { return false; }
                }
                else { return false; }
            }

            //cas d'une droite oblique

            //y = mx +p
            double m = (pointB.Longitude - pointA.Longitude) / (pointB.Latitude - pointA.Latitude);
            double p = pointA.Longitude - (m * pointA.Latitude);

            Coordonnees pointDeLaDroite = new Coordonnees();
            for (double x = minLat; x <= maxLat; x += precision)
            {
                double y = m * x + p;
                pointDeLaDroite.Latitude = x;
                pointDeLaDroite.Longitude = y;
                Console.WriteLine("DEBUG : IsPointClosePourUneDroiteAB, droite oblique : coordonnees courante : {0}", pointDeLaDroite.ToString());
                if (pointDeLaDroite.IsPointClose(coordonneesPoint, precision)) { return true; }
            }
        
            //si on arrive ici c'est que le point n'est pas proche de la  droite AB
            return false;
        }

        #endregion
    }
}
