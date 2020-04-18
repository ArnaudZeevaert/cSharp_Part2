using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathUtil;
using getNewId;
using System.IO;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyCartographyObj
{
    [Serializable]
    public class POI : Coordonnees, IisPointClose, ICartoObj, INotifyPropertyChanged
    {
        #region VARIABLES MEMBRES   
        private string _description;
        #endregion

        #region PROPRIETES
        public string Description
        {
            set 
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
            get { return _description; }
        }
        #endregion

        #region CONSTRUCTEURS
        public POI() : this(50.610745, 5.510437, "InPrEs")
        {

        }

        public POI(double latitude, double longitude, string description) : base(latitude, longitude)
        {
            Description = description;
        }
        #endregion

        #region METHODES
        public override string Draw()
        {
            return this.ToString() + string.Format(" (Lat/Long) ({0:0.000}/{1:0.000})", Latitude, Longitude);
        }
        public override string ToString()
        {
            return string.Format("id({0})", Id) + " Description : " + Description;
        }

        public static POI readCSVfile(string cheminDacces = @"../../../PersonalMap_Manager/Ressources/fichiersCSV/HEPL Seraing POI.csv")
        {
            POI ret_val = null;
            string [] donnees;

            try
            {
                string fichier = System.IO.File.ReadAllText(cheminDacces);
                donnees = fichier.Split(';');                               

                if (donnees.Length == 3)
                {
                    Console.WriteLine("DEBUG : nom du poi contient un retour de ligne : -" + donnees[2] + "-");

                    string nomSansRetourAlaLigne = "";
                    foreach (char c in donnees[2])
                    {
                        if (c != '\n' && c != '\r' && c != '\t')
                        {
                            nomSansRetourAlaLigne += c.ToString();
                        }
                        else
                            break;
                    }

                    //donnees[2] = donnees[2].Replace("\n", string.Empty);
                    donnees[2] = nomSansRetourAlaLigne;
                    Console.WriteLine("DEBUG : apres remove : -" + donnees[2] + "-");
                    ret_val = new POI(Convert.ToDouble(donnees[0]), Convert.ToDouble(donnees[1]), donnees[2]);
                }
                else
                {
                    ret_val = new POI(Convert.ToDouble(donnees[0]), Convert.ToDouble(donnees[1]), "");
                }

            }
            catch(Exception e)
            {
                throw new CSVexception("error READallTEXT POI : " + e.Message);
            }

            return ret_val;
        }

        public static void saveCSVfile(POI pOI, string cheminDacces)
        {
            string nomFichier = pOI.Description + IdFichier.GetAnNewId().ToString() + ".csv";            

            try
            {
                cheminDacces = Path.GetDirectoryName(cheminDacces);
                cheminDacces = Path.Combine(cheminDacces, nomFichier);

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(cheminDacces, true))
                {
                    file.WriteLine(Convert.ToString(pOI.Latitude) + ";" + Convert.ToString(pOI.Longitude) + ";" + pOI.Description);                    
                }
            }
            catch(Exception e)
            {
                throw new CSVexception("error saveCSVfile POI : " + e.Message);                                
            }            
        }
        #endregion
    }
}
