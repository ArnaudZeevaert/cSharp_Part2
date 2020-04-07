using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathUtil;

namespace MyCartographyObj
{
    [Serializable]
    public class Coordonnees : CartoObj, IisPointClose
    {
        #region VARIABLES_MEMBRES
        protected double _latitude;
        protected double _longitude;
        #endregion

        #region PROPRIETES
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        #endregion

        #region CONSTRUCTEURS
        public Coordonnees() : this(0, 0)
        {

        }
        public Coordonnees(double latitude, double longitude) : base()
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        #endregion

        #region METHODE
        public override string Draw()
        {
            return this.ToString();
        }
        public override string ToString()
        {
            return base.ToString() + " " + string.Format(" (Lat/Long) ({0:0.000}/{1:0.000})", Latitude, Longitude);
        }
        public bool IsPointClose(Coordonnees coordonneesPoint, double precision)
        {
            double differenceLatitude, differenceLongitude;
            differenceLatitude = Math.Abs(this.Latitude - coordonneesPoint.Latitude);
            differenceLongitude = Math.Abs(this.Longitude - coordonneesPoint.Longitude);

            if (differenceLatitude <= precision && differenceLongitude <= precision)
            {
                return true;
            }
            else
                return false;

            /*double longueurSegment = MathUtilFct.LongueurD_unSegment(this.Latitude, this.Longitude, coordonneesPoint.Latitude, coordonneesPoint.Longitude);
            if(longueurSegment <= precision) { return true; }
            else { return false; }*/
        }
        #endregion

    }
}
