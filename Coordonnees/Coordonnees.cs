using System;

namespace MyCartographyObjects
{
    public class Coordonnees : CartoObj
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
        public override void Draw()
        {
            Console.WriteLine(this.ToString());
        }
        public override string ToString()
        {
            return base.ToString() + " " + string.Format(" (Lat/Long) ({0:0.000}/{1:0.000})", Latitude, Longitude);
        }
        #endregion

    }
}
