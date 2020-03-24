using System;
using System.Collections.Generic;
using System.Text;
using MathUtil;

namespace MyCartographyObjects
{
    public class POI : Coordonnees, IisPointClose
    {
        #region VARIABLES MEMBRES   
        private string _description;
        #endregion

        #region PROPRIETES
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        #endregion

        #region CONSTRUCTEURS
        public POI() : this(50.610745, 5.510437, "IN.PR.E.S.")
        {

        }

        public POI(double latitude, double longitude, string description) : base(latitude, longitude)
        {
            Description = description;
        }
        #endregion

        #region METHODES
        public override void Draw()
        {
            Console.WriteLine(this.ToString());
        }
        public override string ToString()
        {
            return base.ToString() + " Description : " + Description;
        }
        public bool IsPointClose(Coordonnees coordonneesPoint, double precision)
        {
            /*double differenceLatitude, differenceLongitude;
            differenceLatitude = Math.Abs(this.Latitude - coordonneesPoint.Latitude);
            differenceLongitude = Math.Abs(this.Longitude - coordonneesPoint.Longitude);

            if (differenceLatitude <= precision && differenceLongitude <= precision)
            {
                return true;
            }
            else
                return false;*/
            double x1, y1, distance;

            x1 = base.Latitude;
            y1 = base.Longitude;
            
            distance = MathUtilFct.Distance2Points(coordonneesPoint.Latitude, coordonneesPoint.Longitude, x1, y1);
            if (distance <= precision)
            {
                //Console.WriteLine("Le point est proche !");
                return true;
            }
            //Console.WriteLine("Le point est trop eloigné !");
            return false;
        }
        #endregion
    }
}
