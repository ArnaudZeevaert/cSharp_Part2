using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathUtil;

namespace MyCartographyObj
{
    public class POI : Coordonnees, IisPointClose, ICartoObj
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
        #endregion
    }
}
