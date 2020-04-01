using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathUtil;

namespace MyCartographyObj
{
    [Serializable]
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

        public static POI readCSVfile(string cheminDacces = @"../../../PersonalMap_Manager\Ressources\fichiersCSV\HEPL Seraing POI.csv")
        {
            POI ret_val = null;
            string [] donnees;

            try
            {
                string fichier = System.IO.File.ReadAllText(cheminDacces);
                donnees = fichier.Split(';');
                
                if(donnees.Length==3)
                    ret_val = new POI(Convert.ToDouble(donnees[0]), Convert.ToDouble(donnees[1]), donnees[2]);                                
            }
            catch(Exception e)
            {
                Console.WriteLine("error READallTEXT POI : " + e.Message);
                return null;
            }

            return ret_val;
        }

        public static bool saveCSVfile(POI pOI, string cheminDacces = @"../../../PersonalMap_Manager\Ressources\fichiersCSV\")
        {
            bool ret_val = false;
            cheminDacces += pOI.Description + ".csv";
            try
            {
                using(System.IO.StreamWriter file = new System.IO.StreamWriter(cheminDacces, true))
                {
                    file.WriteLine(Convert.ToString(pOI.Latitude) + ";" + Convert.ToString(pOI.Longitude) + ";" + pOI.Description);
                    ret_val = true;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("error saveCSVfile POI : " + e.Message);
                return false;
            }
            return ret_val;
        }
        #endregion
    }
}
