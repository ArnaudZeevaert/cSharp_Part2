using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using getNewId;

namespace MyCartographyObj
{
    public class MyPersonalMapData : INotifyPropertyChanged
    {
        #region VARIABLES MEMBRES
        private string _nom;
        private string _prenom;
        private string _email;
        private string _emplacement;
        private ObservableCollection<ICartoObj> _observableCollection;

        
        #endregion

        #region PROPRIETES
        public string Emplacement
        {
            set 
            { 
                if(_emplacement != value)
                {
                    _emplacement = value;
                }
            }
            get { return _emplacement; }
        }
        public string Nom
        {
            get { return _nom; }
            set 
            {
                if (_nom != value)
                {
                    _nom = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Prenom
        {
            get { return _prenom; }
            set 
            {
                if (_prenom != value)
                {
                    _prenom = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Email
        {
            get { return _email; }
            set 
            { 
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                } 
            }
        }
        public ObservableCollection<ICartoObj> ObservableCollection
        {
            get { return _observableCollection; }
        }
        public void SETObservableCollection(ObservableCollection<ICartoObj> newObservableCollection)
        {                      
            if (_observableCollection != newObservableCollection)
            {
                if (newObservableCollection == null)
                {
                    _observableCollection = new ObservableCollection<ICartoObj>();
                }
                else
                {
                    _observableCollection = newObservableCollection;
                    OnPropertyChanged();
                }             
            }                             
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region CONSTRUCTEURS       
        public MyPersonalMapData() : this("NOM", "PRENOM", "E-MAIL" , new ObservableCollection<ICartoObj>())
        {

        }
        public MyPersonalMapData(string newNom, string newPrenom, string newEmail, ObservableCollection<ICartoObj> newObservableCollection)
        {
            Nom = newNom;
            Prenom = newPrenom;
            Email = newEmail;
            SETObservableCollection(newObservableCollection);
            Emplacement = null;
        }
        #endregion

        #region METHODE
        public void ResetObservableCollection()
        {
            SETObservableCollection(new ObservableCollection<ICartoObj>());
        }

        public static MyPersonalMapData LoadFile(string cheminDacces)
        {
            MyPersonalMapData ret_val = null;
            try
            {
                if (cheminDacces.Contains(".az"))
                {
                    if (File.Exists(cheminDacces))
                    {
                        BinaryFormatter binFormat = new BinaryFormatter();
                        ret_val = new MyPersonalMapData();
                        try
                        {
                            Stream fStream = new FileStream(cheminDacces, FileMode.Open, FileAccess.Read);
                            if (fStream == null)
                                return null;
                            ret_val.Prenom = (string)binFormat.Deserialize(fStream);
                            ret_val.Nom = (string)binFormat.Deserialize(fStream);
                            ret_val.Email = (string)binFormat.Deserialize(fStream);
                            ret_val.Emplacement = cheminDacces;
                            ret_val.SETObservableCollection((ObservableCollection<ICartoObj>)binFormat.Deserialize(fStream));
                            fStream.Close();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("LoadFile : Erreur : {0}", e.Message);
                            return null;
                        }
                    }
                    else
                    {
                        Console.WriteLine("DEBUG : LoadFile : le répertoire {0} n existe pas !!! : fin de la fct...", cheminDacces);
                    }
                }
                else
                    Console.WriteLine("DEBUG : LoadFile : nom de fichier invlaide !!! --> {0}", cheminDacces);
            }
            catch (Exception e)
            {
                Console.WriteLine("LoadFile : Erreur : {0}", e.Message);
                return null;
            }
            return ret_val;
        }

        //output : true si on trouve la personne à l'emplacement demandé, false si on ne la trouve pas
        public static MyPersonalMapData LoadPersonne(MyPersonalMapData personneRecherchee)
        {
            MyPersonalMapData ret_val = null;
            string emplacement = @"../../../sauvegarde/";
            try
            {                
                if (Directory.Exists(emplacement))
                {
                    Console.WriteLine("DEBUG : LoadObservableCollection : le répertoire {0} existe...", emplacement);
                    string[] listeFicher;
                    listeFicher = Directory.GetFiles(emplacement);
                    if (listeFicher.Length > 0)
                    {
                        for (int i = 0; i < listeFicher.Length; i++)
                        {
                            if (File.Exists(listeFicher[i]))
                            {
                                Console.WriteLine("path = " + listeFicher[i]);
                                MyPersonalMapData personneTMP = new MyPersonalMapData();
                                using (BinaryReader readFile = new BinaryReader(File.Open(listeFicher[i], FileMode.Open)))
                                {
                                    //lecture du nom...
                                    personneTMP.Nom = readFile.ReadString();

                                    //lecture du prénom...
                                    personneTMP.Prenom = readFile.ReadString();

                                    //lecture du Email...
                                    personneTMP.Email = readFile.ReadString();

                                    personneTMP.Emplacement = listeFicher[i];                                        

                                    if (personneTMP.Nom == personneRecherchee.Nom && personneTMP.Prenom == personneRecherchee.Prenom && personneTMP.Email == personneRecherchee.Email)
                                    {
                                        ret_val = personneTMP;
                                        i = listeFicher.Length;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("DEBUG : LoadObservableCollection : le répertoire {0} est vide !!!", emplacement);
                    }
                }
                else
                {
                    Console.WriteLine("DEBUG : LoadObservableCollection : le répertoire {0} n existe pas !!! : fin de la fct...", emplacement);
                }                
            }
            catch(Exception e)
            {
                Console.WriteLine("LoadObservableCollection : Erreur : {0}", e.Message);
                return null;
            }
            return ret_val;
        }

        //INPUT :   un objet MyPersonalMapData a suauvegarder, l'emplacement de la sauvegarde (chemin d'accès avec nom de fichier)
        //PROCESS : sauvegarde MyPersonalMapData passé en paramètre à l'emplacement spécifie
        //          si path = null --> on sauvegarde uniquement à l'emplacement par défaut
        //          si path est différent de l'emplacement par défaut, on sauvagarde à l'emplacement spécifié + l'emplacement par défaut
        //OUTPUT :  null en cas d'erreur, en cas de succès : return de MyPersonnalMapData passé en paramètre avec son emplacement "path"
        public static MyPersonalMapData SavePersonne(MyPersonalMapData personneAsauvegardee, string path = null)
        {            
            MyPersonalMapData ret_val = null;
            string nomFichier, directoryPath, emplacementSauvegrade = @"../../../sauvegarde/";

            nomFichier = personneAsauvegardee.Nom + personneAsauvegardee.Prenom + IdFichier.GetAnNewId().ToString() + ".az";
            if (path != null)
            {
                try
                {
                    //nomFichier = Path.GetFileName(path);
                    directoryPath = Path.GetDirectoryName(path);
                    path = directoryPath + nomFichier;
                }
                catch (Exception e)
                {
                    Console.WriteLine("EXCEPTION : SavePersonne (nomDeFichier/directoryPath) : " + e.Message);
                    return null;
                }
            }
            else
            {                
                directoryPath = emplacementSauvegrade;
                path = emplacementSauvegrade + nomFichier;
            }
            

            Console.WriteLine("DEBUG : nom du fichier : {0} --- directoryPath : {1} --- emplacementSauvegarde : {2}", nomFichier, directoryPath, emplacementSauvegrade);
            //if (nomDeFichierIncluDansL_Emplacement) nomFichier = emplacement;
            //else nomFichier = emplacement + @"\" + personneAsauvegardee.Prenom + personneAsauvegardee.Nom + ".az";      
            
            try
            {
                BinaryFormatter binFormat = new BinaryFormatter();
                Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                binFormat.Serialize(fStream, personneAsauvegardee.Prenom);
                binFormat.Serialize(fStream, personneAsauvegardee.Nom);
                binFormat.Serialize(fStream, personneAsauvegardee.Email);
                binFormat.Serialize(fStream, personneAsauvegardee.ObservableCollection);
                fStream.Close();

                //copie du fichier dans : @"D:\C#projets\ARNAUD_ZEEVAERT_2226\sauvegarde" si ce n'est déja pas l'emplacement actuel
                if(!path.Contains(emplacementSauvegrade))
                {
                    Console.WriteLine("DEBUG : copie du fichier dans : D:/C#projets/ARNAUD_ZEEVAERT_2226/sauvegarde si ce n'est déja pas l'emplacement actuel");
                    emplacementSauvegrade += nomFichier;
                    Console.WriteLine("DEBUG: emplacement sauvegarde = " + emplacementSauvegrade);
                    File.Copy(path, emplacementSauvegrade);
                }

                //sauvegarde de l'emplacement dans l'objet MyPersonnalMapData
                ret_val = personneAsauvegardee;
                ret_val.Emplacement = path;
            }
            catch (Exception e)
            {
                Console.WriteLine("SavePersonne : Erreur : {0}", e.Message);
                return null;
            }
            
            Console.WriteLine("Debug : SavePersonne : save ok ? : {0}", ret_val);

            return ret_val;
        }
        public override string ToString()
        {
            string CollectionDeCartoObjEn1Ligne = "";
            if (ObservableCollection != null && ObservableCollection.Count > 0)
            {
                foreach (CartoObj o in ObservableCollection)
                {
                    CollectionDeCartoObjEn1Ligne = CollectionDeCartoObjEn1Ligne + "\n" + o.ToString();
                }
            }
            else
            {
                CollectionDeCartoObjEn1Ligne = "Pas de ObservableCollection...";
            }
            return "--- Nom ---\n" + Nom + "\n--- Prenom ---\n" + Prenom + "\n--- Email ---\n" + Email + "\n--- Path ---\n" + Emplacement + "\n--- ObservableCollection ---\n" + CollectionDeCartoObjEn1Ligne;
        }       

        public static bool readCSVtrajet(MyPersonalMapData personne, string cheminDacces = @"../../../PersonalMap_Manager\Ressources\fichiersCSV\HEPL Seraing Liege Trajet.csv")
        {
            Polyline trajet = new Polyline();
            try
            {
                string[] fichier = System.IO.File.ReadAllLines(cheminDacces);
                
                for (int i = 0; i < fichier.Length; i++)
                {
                    Console.WriteLine("DEBUG line = " + fichier[i]);
                    string[] donnees;
                    donnees = fichier[i].Split(';');

                    if (donnees.Length == 3)
                    {
                        Console.WriteLine("DEBUG split string[0] = {0}, string[1] = {1}, string[2] = {2}", donnees[0], donnees[1], donnees[2]);
                        if (donnees[2] == "")
                            trajet._collectionDeCoordonnees.Add(new Coordonnees(Convert.ToDouble(donnees[0]), Convert.ToDouble(donnees[1])));
                        else
                            trajet._collectionDeCoordonnees.Add(new POI(Convert.ToDouble(donnees[0]), Convert.ToDouble(donnees[1]), donnees[2]));
                    }
                    else
                        trajet._collectionDeCoordonnees.Add(new Coordonnees(Convert.ToDouble(donnees[0]), Convert.ToDouble(donnees[1])));

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error readCSVtrajet : " + e.Message);
                return false;
            }

            personne.ObservableCollection.Add(trajet);
            return true;
        }
        #endregion

    }

    //public static bool saveCSVtrajet(MyPersonalMapData personne, string cheminDacces = @"../../../PersonalMap_Manager\Ressources\fichiersCSV\")
    //{
    //    bool ret_val = false;
    //    cheminDacces += personne.Nom + "." + personne.Prenom + "_observableCollection.csv";
    //    try
    //    {
    //        using (System.IO.StreamWriter file = new System.IO.StreamWriter(cheminDacces, true))
    //        {
    //            foreach(ICartoObj o in personne.ObservableCollection)
    //            {
    //                if(o is )
    //            }
    //            file.WriteLine(Convert.ToString(pOI.Latitude) + ";" + Convert.ToString(pOI.Longitude) + ";" + pOI.Description);
    //            ret_val = true;
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine("error saveCSVfile POI : " + e.Message);
    //        return false;
    //    }
    //    return ret_val;
    //}
}
