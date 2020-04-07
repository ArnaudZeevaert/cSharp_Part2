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
                if (_emplacement != value)
                {
                    _emplacement = value;
                    OnPropertyChanged();
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region CONSTRUCTEURS       
        public MyPersonalMapData() : this("NOM", "PRENOM", "E-MAIL", new ObservableCollection<ICartoObj>())
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
            MyPersonalMapData ret_val = new MyPersonalMapData(); ;
            try
            {
                if (cheminDacces.Contains(".az"))
                {
                    if (File.Exists(cheminDacces))
                    {
                        BinaryFormatter binFormat = new BinaryFormatter();
                        try
                        {
                            Stream fStream = new FileStream(cheminDacces, FileMode.Open, FileAccess.Read);
                            if (fStream == null)
                                throw new LoadSaveException("LoadFile fStream == null");
                            ret_val.Prenom = (string)binFormat.Deserialize(fStream);
                            ret_val.Nom = (string)binFormat.Deserialize(fStream);
                            ret_val.Email = (string)binFormat.Deserialize(fStream);
                            ret_val.Emplacement = cheminDacces;
                            ret_val.SETObservableCollection((ObservableCollection<ICartoObj>)binFormat.Deserialize(fStream));
                            fStream.Close();
                        }
                        catch (Exception e)
                        {
                            throw new LoadSaveException("LoadFile (try/catch de fStream) : Erreur : " + e.Message);
                        }
                    }
                    else
                        throw new LoadSaveException("LoadFile : le répertoire : " + cheminDacces + " n existe pas !!!");
                }
                else
                    throw new LoadSaveException("LoadFile : nom de fichier invlaide !!! --> " + cheminDacces);
            }
            catch (Exception e)
            {
                throw new LoadSaveException("LoadFile : Erreur : " + e.Message);
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
                                MyPersonalMapData personneTMP = new MyPersonalMapData();
                                BinaryFormatter binFormat = new BinaryFormatter();
                                try
                                {
                                    Stream fStream = new FileStream(listeFicher[i], FileMode.Open, FileAccess.Read);
                                    if (fStream == null)
                                        throw new LoadSaveException("LoadPersonne fStream == null");
                                    personneTMP.Prenom = (string)binFormat.Deserialize(fStream);
                                    personneTMP.Nom = (string)binFormat.Deserialize(fStream);
                                    personneTMP.Email = (string)binFormat.Deserialize(fStream);
                                    personneTMP.Emplacement = listeFicher[i];
                                    personneTMP.SETObservableCollection((ObservableCollection<ICartoObj>)binFormat.Deserialize(fStream));
                                    fStream.Close();

                                    //Console.WriteLine("DEBUG recherche de personne :");
                                    //Console.WriteLine("path = " + listeFicher[i]);
                                    //Console.WriteLine("personneTMP = \n" + personneTMP.ToString());
                                    //Console.WriteLine("personneRecherchee = \n" + personneRecherchee.ToString());

                                    if (personneTMP.Nom == personneRecherchee.Nom && personneTMP.Prenom == personneRecherchee.Prenom && personneTMP.Email == personneRecherchee.Email)
                                    {
                                        ret_val = personneTMP;
                                        i = listeFicher.Length;
                                    }
                                }
                                catch (Exception e)
                                {
                                    throw new LoadSaveException("LoadPersonne (try/catch de fStream) : Erreur : " + e.Message);
                                }
                            }
                        }
                    }
                    else
                        throw new LoadSaveException("LoadObservableCollection : le répertoire" + emplacement + "est vide !!!");
                }
                else
                    throw new LoadSaveException("LoadObservableCollection : le répertoire" + emplacement + "n existe pas !!!");
            }
            catch (Exception e)
            {
                throw new LoadSaveException("LoadPersonne : Erreur : " + e.Message);
            }

            if (ret_val == null) throw new LoadSaveException("LoadPersonne : ret_val == null : personneRecherche pas trouvee :( ");
            return ret_val;
        }

        //INPUT :   un objet MyPersonalMapData a suauvegarder, l'emplacement de la sauvegarde (chemin d'accès avec nom de fichier)
        //PROCESS : sauvegarde MyPersonalMapData passé en paramètre à l'emplacement spécifie
        //          si path = null --> on sauvegarde uniquement à l'emplacement par défaut
        //          si path est différent de l'emplacement par défaut, on sauvagarde à l'emplacement spécifié + l'emplacement par défaut       
        public static MyPersonalMapData SavePersonne(MyPersonalMapData personneAsauvegardee, string path = null)
        {
            MyPersonalMapData ret_val = null;
            string nomFichier, directoryPath, emplacementSauvegrade = @"../../../sauvegarde/", emplacementVersionPrecedente = @"../../../sauvegarde/versionsPrecedentes/";

            nomFichier = personneAsauvegardee.Nom + personneAsauvegardee.Prenom + ".az";
            if (path != null)
            {
                try
                {
                    //nomFichier = Path.GetFileName(path);
                    directoryPath = Path.GetDirectoryName(path);
                    path = Path.Combine(directoryPath, nomFichier);
                }
                catch (Exception e)
                {
                    throw new LoadSaveException("SavePersonne (nomDeFichier/directoryPath) : " + e.Message);
                }
            }
            else
            {
                directoryPath = emplacementSauvegrade;
                path = emplacementSauvegrade + nomFichier;
            }

            emplacementSauvegrade += nomFichier;
            Console.WriteLine("DEBUG : nom du fichier : {0} --- path : {1} --- emplacementSauvegarde : {2}", nomFichier, path, emplacementSauvegrade);

            try
            {
                if (File.Exists(path))
                {
                    try
                    {
                        deplacementFichier(path, emplacementVersionPrecedente);
                    }
                    catch (LoadSaveException deplacementFichierMessage)
                    {
                        throw new LoadSaveException(deplacementFichierMessage.Message);
                    }
                }
                if (File.Exists(emplacementSauvegrade))
                {
                    try
                    {
                        deplacementFichier(emplacementSauvegrade, emplacementVersionPrecedente);
                    }
                    catch (LoadSaveException deplacementFichierMessage)
                    {
                        throw new LoadSaveException(deplacementFichierMessage.Message);
                    }
                }
            }
            catch (Exception e)
            {
                throw new LoadSaveException("un fichier au nom de cette personne existe déja, la suppression de l'ancien fichier à échouée:\n" + e.Message);
            }



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

                if (!File.Exists(emplacementSauvegrade))
                {
                    Console.WriteLine("DEBUG : copie du fichier dans : D:/C#projets/ARNAUD_ZEEVAERT_2226/sauvegarde si ce n'est déja pas l'emplacement actuel");
                    File.Copy(path, emplacementSauvegrade);
                }

                //sauvegarde de l'emplacement dans l'objet MyPersonnalMapData
                ret_val = personneAsauvegardee;
                ret_val.Emplacement = path;
            }
            catch (Exception e)
            {
                throw new LoadSaveException("SavePersonne (fStream) : Erreur : " + e.Message);
            }

            if (ret_val == null) throw new LoadSaveException("SavePersonne : ret_val == null : une erreur est survenue :( ");

            return ret_val;
        }

        public static void deplacementFichier(string pathActuel, string newDirectory)
        {
            string newfileName = Path.GetFileNameWithoutExtension(pathActuel);
            newfileName += IdFichier.GetAnNewId().ToString() + ".az";

            try
            {
                string destFile = Path.Combine(newDirectory, newfileName);
                Console.WriteLine("DEBUG : deplacementFichier : destFile = " + destFile);
                File.Move(pathActuel, destFile);
            }
            catch (Exception e)
            {
                throw new LoadSaveException("(deplacementFichier) ERREUR : " + e.Message);
            }
        }
        public override string ToString()
        {
            string CollectionDeCartoObjEn1Ligne = "";
            if (ObservableCollection != null && ObservableCollection.Count > 0)
            {
                foreach (CartoObj o in ObservableCollection)
                {
                    CollectionDeCartoObjEn1Ligne = CollectionDeCartoObjEn1Ligne + "\n" + o.Draw();
                }
            }
            else
            {
                CollectionDeCartoObjEn1Ligne = "Pas de ObservableCollection...";
            }
            return "--- Nom ---\n" + Nom + "\n--- Prenom ---\n" + Prenom + "\n--- Email ---\n" + Email + "\n--- Path ---\n" + Emplacement + "\n--- ObservableCollection ---\n" + CollectionDeCartoObjEn1Ligne;
        }

        public static void readCSVtrajet(MyPersonalMapData personne, string cheminDacces)
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

                    if(donnees.Length == 3)
                    {
                        //pour supprimer le retour à la ligne dans l'éventuelle description de POI
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
                        donnees[2] = nomSansRetourAlaLigne;
                        Console.WriteLine("DEBUG : apres remove : -" + donnees[2] + "-");


                        trajet._collectionDeCoordonnees.Add(new POI(Convert.ToDouble(donnees[0]), Convert.ToDouble(donnees[1]), donnees[2]));
                    }
                    else
                        trajet._collectionDeCoordonnees.Add(new POI(Convert.ToDouble(donnees[0]), Convert.ToDouble(donnees[1]), ""));
                }
                trajet.NomTrajet = Path.GetFileNameWithoutExtension(cheminDacces);
                personne.ObservableCollection.Add(trajet);
            }
            catch (Exception e)
            {
                throw new CSVexception("error readCSVtrajet : " + e.Message);
            }
        }

        public static void saveCSVtrajet(Polyline trajet, string cheminDacces)
        {
            Console.WriteLine("DEBUG (1) : chemin d acces = " + cheminDacces);
            try
            {
                cheminDacces = Path.GetDirectoryName(cheminDacces);
                cheminDacces = Path.Combine(cheminDacces, trajet.NomTrajet + IdFichier.GetAnNewId().ToString() + ".csv");

                Console.WriteLine("DEBUG (2) : chemin d acces = " + cheminDacces);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(cheminDacces, true))
                {
                    foreach (POI p in trajet._collectionDeCoordonnees)
                    {
                        if (p.Description == "")
                        {
                            Console.WriteLine("DEBUG : pas de description POI");
                            file.WriteLine(Convert.ToString(p.Latitude) + ";" + Convert.ToString(p.Longitude) + ";");
                        }
                        else
                        {
                            Console.WriteLine("DEBUG : il y a une description POI");
                            file.WriteLine(Convert.ToString(p.Latitude) + ";" + Convert.ToString(p.Longitude) + ";" + p.Description);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new CSVexception("error saveCSVfile POI : " + e.Message);
            }
        }
        #endregion
    }
}
