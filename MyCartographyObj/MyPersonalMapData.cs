using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;

namespace MyCartographyObj
{
    public class MyPersonalMapData : INotifyPropertyChanged
    {
        #region VARIABLES MEMBRES
        private string _nom;
        private string _prenom;
        private string _email;
        private ObservableCollection<CartoObj> _observableCollection;

        
        #endregion

        #region PROPRIETES
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
        public ObservableCollection<CartoObj> ObservableCollection
        {
            get { return _observableCollection; }
        }
        private ObservableCollection<CartoObj> SETObservableCollection
        {
            set
            {
                bool isICartoObj = true;
                if (_observableCollection != value)
                {
                    foreach(CartoObj o in value)
                    {
                        if (!(o is ICartoObj))
                        {
                            isICartoObj = false;//si 1 des CartoObj de la collection n'implémente pas ICartoObj, --> on n'ajoute pas la collection
                        }
                    }
                    if (isICartoObj)
                    {
                        _observableCollection = value;
                        OnPropertyChanged();
                    }
                    else if (_observableCollection == null)
                    {
                        _observableCollection = new ObservableCollection<CartoObj>();
                    }
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
        public MyPersonalMapData() : this("NOM", "PRENOM", "E-MAIL" , new ObservableCollection<CartoObj>())
        {

        }
        public MyPersonalMapData(string newNom, string newPrenom, string newEmail, ObservableCollection<CartoObj> newObservableCollection)
        {
            Nom = newNom;
            Prenom = newPrenom;
            Email = newEmail;
            SETObservableCollection = newObservableCollection;
        }
        #endregion

        #region METHODE
        public void ResetObservableCollection()
        {
            SETObservableCollection = new ObservableCollection<CartoObj>();
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
                        MyPersonalMapData personneTMP = new MyPersonalMapData();
                        using (BinaryReader readFile = new BinaryReader(File.Open(cheminDacces, FileMode.Open)))
                        {
                            //lecture du nom...
                            personneTMP.Nom = readFile.ReadString();

                            //lecture du prénom...
                            personneTMP.Prenom = readFile.ReadString();

                            //lecture du Email...
                            personneTMP.Email = readFile.ReadString();

                            //(pas encore fait) --> lire la collection de cartoObj

                            ret_val = personneTMP;
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
        public static MyPersonalMapData LoadPersonne(MyPersonalMapData personneRecherchee, string emplacement = @"D:\C#projets\ARNAUD_ZEEVAERT_2226\sauvegarde", string nomFichier = "")
        {
            MyPersonalMapData ret_val = null;
            try
            {
                if(Directory.Exists(emplacement))
                {
                    Console.WriteLine("DEBUG : LoadObservableCollection : le répertoire {0} existe...", emplacement);
                    string[] listeFicher;
                    listeFicher = Directory.GetFiles(emplacement);
                    if(listeFicher.Length > 0)
                    {
                        if (nomFichier == "")//rechercher dans tous les fichiers du répertoire
                        {
                            for (int i = 0; i < listeFicher.Length; i++)
                            {
                                if (File.Exists(listeFicher[i]))
                                {
                                    MyPersonalMapData personneTMP = new MyPersonalMapData();
                                    using (BinaryReader readFile = new BinaryReader(File.Open(listeFicher[i], FileMode.Open)))
                                    {
                                        //lecture du nom...
                                        personneTMP.Nom = readFile.ReadString();

                                        //lecture du prénom...
                                        personneTMP.Prenom = readFile.ReadString();

                                        //lecture du Email...
                                        personneTMP.Email = readFile.ReadString();

                                        //(pas encore fait) --> lire la collection de cartoObj

                                        if (personneTMP.Nom == personneRecherchee.Nom && personneTMP.Prenom == personneRecherchee.Prenom && personneTMP.Email == personneRecherchee.Email)
                                        {
                                            ret_val = personneTMP;
                                            i = listeFicher.Length;
                                        }
                                    }
                                }
                            }
                        }
                        else//rechercher le fichier spécifié en paramètre
                        {
                            emplacement = emplacement + @"\" + nomFichier;
                            Console.WriteLine("DEBUG : LoadObservableCollection : recherche d un fichier specifique : nom du fichier = {0}", emplacement);

                        }
                    }
                    else
                    {
                        Console.WriteLine("DEBUG : LoadObservableCollection : le répertoire {0} est vide !!!", emplacement);
                        if (File.Exists(emplacement))
                        {
                            MyPersonalMapData personneTMP = new MyPersonalMapData();
                            using (BinaryReader readFile = new BinaryReader(File.Open(emplacement, FileMode.Open)))
                            {
                                //lecture du nom...
                                personneTMP.Nom = readFile.ReadString();

                                //lecture du prénom...
                                personneTMP.Prenom = readFile.ReadString();

                                //lecture du Email...
                                personneTMP.Email = readFile.ReadString();

                                //(pas encore fait) --> lire la collection de cartoObj

                                if (personneTMP.Nom == personneRecherchee.Nom && personneTMP.Prenom == personneRecherchee.Prenom && personneTMP.Email == personneRecherchee.Email)
                                {
                                    ret_val = personneTMP;                              
                                }
                            }
                        }
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
        public static bool SavePersonne(MyPersonalMapData personneAsauvegardee, string emplacement = @"D:\C#projets\ARNAUD_ZEEVAERT_2226\sauvegarde", bool nomDeFichierIncluDansL_Emplacement = false)
        {
            bool ret_val = true;
            string nomFichier;

            if (nomDeFichierIncluDansL_Emplacement) nomFichier = emplacement;
            else nomFichier = emplacement + @"\" + personneAsauvegardee.Prenom + personneAsauvegardee.Nom + ".az";      

            if(File.Exists(nomFichier))//si un fichier existe déja dans le même emplacement, on le supprime (rip l'ancien fichier)
            {
                File.Delete(nomFichier);
            }
            
            Console.WriteLine("DEBUG : SavePersonne : emplacement (avec nom de fichier) = {0}", nomFichier);
            
            try
            {
                using (BinaryWriter ecritureFichier = new BinaryWriter(File.Open(nomFichier, FileMode.Create)))
                {
                    //écriture du nom...
                    ecritureFichier.Write(personneAsauvegardee.Nom);

                    //écriture du prénom...
                    ecritureFichier.Write(personneAsauvegardee.Prenom);

                    //écriture du Email...
                    ecritureFichier.Write(personneAsauvegardee.Email);

                    //(pas encore fait) --> sauvegarder la collection de cartoObj
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("SavePersonne : Erreur : {0}", e.Message);
                return false;
            }

            Console.WriteLine("Debug : SavePersonne : save ok ? : {0}", ret_val);

            return ret_val;
        }
        public override string ToString()
        {
            string CollectionDeCartoObjEn1Ligne = "";
            if (ObservableCollection.Count >0)
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
            return "Nom : " + Nom + " Prenom : " + Prenom + " Email : " + Email + "\nObservableCollection : " + CollectionDeCartoObjEn1Ligne;
        }

        #endregion

    }
}
