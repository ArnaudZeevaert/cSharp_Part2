using System;
using MyCartographyObj;
using System.Windows.Media;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;

namespace ConsoleApp1
{
    class test
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("---------créer et afficher 2 objets de chaque sorte---------");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("------------------COORDONNEES------------------");
            Coordonnees C1 = new Coordonnees();
            Console.WriteLine("Constructeur par defaut de Coordonnees : C1");
            Console.WriteLine(C1.ToString());
            Console.WriteLine("Methode DRAW");
            C1.Draw();

            Console.WriteLine("C1 lat = 1, long = 1");
            C1.Latitude = 1;
            C1.Longitude = 1;
            Console.WriteLine(C1.ToString());
            Console.WriteLine("Methode DRAW");
            C1.Draw();

            Coordonnees C2 = new Coordonnees(1, 2);
            Console.WriteLine("Constructeur initialisation Coordonnees : C2(1, 2)");
            Console.WriteLine(C2.ToString());
            Console.WriteLine("Methode DRAW");
            C2.Draw();

            bool returnIsPointClose;
            Console.WriteLine("IsPointClose entre C1(lat/long) : " + C1.Latitude + "/" + C1.Longitude + " et C2(lat/long) : " + C2.Latitude + " / " + C2.Longitude);
            Console.WriteLine("Precision = 1.0");
            returnIsPointClose = C1.IsPointClose(C2, 1.0);
            if (returnIsPointClose) { Console.WriteLine("Point proche..."); }
            else { Console.WriteLine("Point pas proche..."); }

            Console.WriteLine("IsPointClose entre C1(lat/long) : " + C1.Latitude + "/" + C1.Longitude + " et C2(lat/long) : " + C2.Latitude + " / " + C2.Longitude);
            Console.WriteLine("Precision = 0.1");
            returnIsPointClose = C1.IsPointClose(C2, 0.1);
            if (returnIsPointClose) { Console.WriteLine("Point proche..."); }
            else { Console.WriteLine("Point pas proche..."); }

            Console.WriteLine("------------------POI------------------");
            POI Sraing = new POI();
            Console.WriteLine(Sraing.ToString());
            Console.WriteLine("Latitude" + Sraing.Latitude);
            Console.WriteLine("Longitude" + Sraing.Longitude);
            Console.WriteLine("Methode DRAW");
            Sraing.Draw();

            Console.WriteLine("IsPointClose entre Sraing(lat/long) : " + Sraing.Latitude + "/" + Sraing.Longitude + " et C2(lat/long) : " + C2.Latitude + " / " + C2.Longitude);
            Console.WriteLine("Precision = 100");
            returnIsPointClose = Sraing.IsPointClose(C2, 100);
            if (returnIsPointClose) { Console.WriteLine("Point proche..."); }
            else { Console.WriteLine("Point pas proche..."); }

            Console.WriteLine("IsPointClose entre Sraing(lat/long) : " + Sraing.Latitude + "/" + Sraing.Longitude + " et C2(lat/long) : " + C2.Latitude + " / " + C2.Longitude);
            Console.WriteLine("Precision = 10");
            returnIsPointClose = Sraing.IsPointClose(C2, 10);
            if (returnIsPointClose) { Console.WriteLine("Point proche..."); }
            else { Console.WriteLine("Point pas proche..."); }

            Console.WriteLine("------------------POLYLINE------------------");
            List<Coordonnees> tabCoordonnees = new List<Coordonnees>();
            tabCoordonnees.Add(C1);
            tabCoordonnees.Add(C2);
            Polyline P1 = new Polyline(tabCoordonnees, Colors.Red, 5);
            Console.WriteLine("Polyline avec C1 et C2, red, 5...");
            Console.WriteLine(P1);
            Console.WriteLine("Methode DRAW");
            P1.Draw();

            Polyline P2 = new Polyline();
            Console.WriteLine("Polyline constructeur par defaut");
            Console.WriteLine(P2.ToString());

            Console.WriteLine("Polyline nbr de coordonnees differentes : ");
            Console.WriteLine("P2: ");
            Console.WriteLine(P2.GetNbPoints());
            Console.WriteLine("P1: ");
            Console.WriteLine(P1.GetNbPoints());
            Console.WriteLine("P1 --> changement de coordonnees : Coordonnees[] tabCoordonnees2 = { C1, C2, C1 };");
            List<Coordonnees> tabCoordonnees2 = new List<Coordonnees>();
            tabCoordonnees2.Add(C1);
            tabCoordonnees2.Add(C2);
            tabCoordonnees2.Add(C1);
            P1._collectionDeCoordonnees = tabCoordonnees2;
            Console.WriteLine("P1: ");
            Console.WriteLine(P1.GetNbPoints());

            Console.WriteLine("Polyline P3 avec comme coordonnees (lat,long): (1,1) / (2,3) / (3,1)");
            List<Coordonnees> triangle = new List<Coordonnees>();
            triangle.Add(new Coordonnees(1, 1));
            triangle.Add(new Coordonnees(2, 3));
            triangle.Add(new Coordonnees(3, 1));
            Polyline P3 = new Polyline(triangle, Colors.Green, 1);
            Coordonnees pointEN22 = new Coordonnees(2, 2);
            Console.WriteLine("IsPointClose entre P3 et pointEN22 (lat/long) : " + pointEN22.Latitude + " / " + pointEN22.Longitude);
            Console.WriteLine("Precision = 0.5");
            returnIsPointClose = P3.IsPointClose(pointEN22, 0.5);
            if (returnIsPointClose) { Console.WriteLine("Point proche..."); }
            else { Console.WriteLine("Point pas proche..."); }

            Console.WriteLine("Meme IsPointClose MAIS avec une precision de 1.0");
            returnIsPointClose = P3.IsPointClose(pointEN22, 1);         
            if (returnIsPointClose) { Console.WriteLine("Point proche..."); }
            else { Console.WriteLine("Point pas proche..."); }

            Polyline droiteTest = new Polyline();
            droiteTest._collectionDeCoordonnees.Add(new Coordonnees(1, 1));
            droiteTest._collectionDeCoordonnees.Add(new Coordonnees(4, 2));
            Console.WriteLine("Coordonnées de la droite : ");
            droiteTest.Draw();
            Console.WriteLine("IsPointClose avec cette droite : avec 2,2 et une precision de 1");
            Console.WriteLine("return de isPointClose : {0}", droiteTest.IsPointClose(new Coordonnees(2, 2), 1.0));
            Console.WriteLine();
            Console.WriteLine("IsPointClose avec cette droite : {0} avec 2,3 et une precision de 1", droiteTest.ToString());
            Console.WriteLine("return de isPointClose : {0}", droiteTest.IsPointClose(new Coordonnees(2, 3), 1.0));


            Console.WriteLine("------------------POLYGON------------------");
            Polygon PN1 = new Polygon(tabCoordonnees, Colors.BurlyWood, Colors.Transparent, 0.5);
            Console.WriteLine("Polygon avec C1 et C2, couleur remplissage=BurlyWood, couleur de contour=Transparent, opacite = 0.5");
            Console.WriteLine(PN1);
            Console.WriteLine("Methode DRAW");
            PN1.Draw();

            Console.WriteLine("set de opacite à 10 (interdit)");
            PN1.Opacite = 10;
            Console.WriteLine("nouvelle valeur de opacite : " + PN1.Opacite);

            Console.WriteLine("constructeur avec opacite à -1 (interdit)");
            Polygon PN3 = new Polygon(tabCoordonnees, Colors.BurlyWood, Colors.Transparent, -1);
            Console.WriteLine(PN3);

            Polygon PN2 = new Polygon();
            Console.WriteLine("Polygon constructeur par defaut");
            Console.WriteLine(PN2);
            Console.WriteLine("Methode DRAW");
            PN2.Draw();

            Console.WriteLine("Polygon nbr de coordonnees differentes : ");
            Console.WriteLine("PN2: ");
            Console.WriteLine(PN2.GetNbPoints());
            Console.WriteLine("PN1: ");
            Console.WriteLine(PN1.GetNbPoints());
            Console.WriteLine("PN1 --> changement de coordonnees : Coordonnees[] tabCoordonnees3 = { C1, C2, C1, new Coordonnees(19, 9) };");
            List<Coordonnees> tabCoordonnees3 = new List<Coordonnees>();
            tabCoordonnees3.Add(C1);
            tabCoordonnees3.Add(C2);
            tabCoordonnees3.Add(C1);
            tabCoordonnees3.Add(new Coordonnees(19, 9));
            PN1._collectionDeCoordonnees = tabCoordonnees3;
            Console.WriteLine("PN1: ");
            Console.WriteLine(PN1.GetNbPoints());

            Console.WriteLine("Polygon PNtriangle avec comme coordonnees (lat,long): (1,1) / (2,3) / (3,1)");            
            Polygon PNtriangle = new Polygon(triangle, Colors.DarkRed, Colors.Blue, 50.0);
            Coordonnees pointEN42 = new Coordonnees(4,2);
            Console.WriteLine("IsPointClose entre PNtriangle et pointEN42 (lat/long) : " + pointEN42.Latitude + " / " + pointEN42.Longitude);
            Console.WriteLine("Precision = 1.0");
            returnIsPointClose = PNtriangle.IsPointClose(pointEN42, 1.0);
            if (returnIsPointClose) { Console.WriteLine("Point proche..."); }
            else { Console.WriteLine("Point pas proche..."); }

            Console.WriteLine("IsPointClose entre PNtriangle et pointEN22 (lat/long) : " + pointEN22.Latitude + " / " + pointEN22.Longitude);
            Console.WriteLine("Precision = 1.0");
            returnIsPointClose = PNtriangle.IsPointClose(pointEN22, 1.0);
            if (returnIsPointClose) { Console.WriteLine("Point proche..."); }
            else { Console.WriteLine("Point pas proche..."); }

            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            Console.WriteLine("---------ajouter ses objets dans une liste générique d'objet de type CartoOjb (List<CartoObj>)---------");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            List<CartoObj> listeDeCartoObj = new List<CartoObj>();
            listeDeCartoObj.Add(C1);
            listeDeCartoObj.Add(C2);
            listeDeCartoObj.Add(Sraing);
            listeDeCartoObj.Add(P1);
            listeDeCartoObj.Add(P2);
            listeDeCartoObj.Add(PN1);
            listeDeCartoObj.Add(PN2);
            listeDeCartoObj.Add(PN3);

            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("---------afficher cette liste en utilisant le mot clé foreach---------");
            Console.WriteLine("----------------------------------------------------------------------");
            int i = 0;
            foreach(CartoObj o in listeDeCartoObj)
            {
                Console.Write("Elm[{0}]", i);
                o.Draw();
                Console.WriteLine();
                i++;
            }

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("---------afficher la liste des objets implémentant l'interface IPointy---------");
            Console.WriteLine("-------------------------------------------------------------------------------");
            i = 0;
            foreach (CartoObj o in listeDeCartoObj)
            {
                if (o is IPointy)
                { 
                    Console.Write("Elm[{0}]", i);
                    i++;
                    o.Draw();
                    Console.WriteLine();
                }                              
            }

            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine("---------afficher la liste des objets qui n'implémentent pas l'interface IPointy---------");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            i = 0;
            foreach (CartoObj o in listeDeCartoObj)
            {
                if (!(o is IPointy))
                {
                    Console.Write("Elm[{0}]", i);
                    i++;
                    o.Draw();
                    Console.WriteLine();
                }                               
            }

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("---------Créer une liste générique de 5 Polyline, l’afficher---------");
            Console.WriteLine("---------------------------------------------------------------------");
            List<Polyline> listeGeneriquePolyline = new List<Polyline>();
            listeGeneriquePolyline.Add(P1);
            P2._collectionDeCoordonnees = tabCoordonnees3;
            listeGeneriquePolyline.Add(P2);
            listeGeneriquePolyline.Add(P3);
            listeGeneriquePolyline.Add(new Polyline(tabCoordonnees, Colors.Black, 19));

            i = 0;
            foreach(Polyline P in listeGeneriquePolyline)
            {
                Console.WriteLine("listeGeneriquePolyline[{0}]", i);
                P.Draw();
                Console.WriteLine();
                i++;
            }

            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine("---------la trier par ordre de longueur croissante1 , l’afficher à nouveau---------");
            Console.WriteLine("-----------------------------------------------------------------------------------");
            listeGeneriquePolyline.Sort();
            i = 0;
            foreach (Polyline P in listeGeneriquePolyline)
            {
                Console.WriteLine("listeGeneriquePolyline[{0}]", i);
                Console.WriteLine("longueur totale : {0}", P.longueurPolyline());
                P.Draw();
                Console.WriteLine();
                i++;
            }

            

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.WriteLine("---------la trier par ordre croissante de surface de bounding box , l’afficher à nouveau---------");
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            MyPolylineBoundingBoxComparer triParSurface = new MyPolylineBoundingBoxComparer();
            listeGeneriquePolyline.Sort(triParSurface);
            i = 0;
            foreach (Polyline P in listeGeneriquePolyline)
            {
                Console.WriteLine("listeGeneriquePolyline[{0}]", i);
                Console.WriteLine("surface : {0}", P.surfaceBoundingBox());
                P.Draw();
                Console.WriteLine();
                i++;
            }

            Console.WriteLine("Find P1 ? {0}", listeGeneriquePolyline.Find(x => x.Id  == P1.Id));

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("---------la trier par ordre croissante de nombre d'objet (différent) de coordonnees , l’afficher à nouveau---------");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            NbrCoordonneesComparer triParNbrDeCoordonnees = new NbrCoordonneesComparer();
            listeGeneriquePolyline.Sort(triParNbrDeCoordonnees);
            i = 0;
            foreach (Polyline P in listeGeneriquePolyline)
            {
                Console.WriteLine("listeGeneriquePolyline[{0}]", i);
                Console.WriteLine("nbr de coordonnees differentes : {0}", P.GetNbPoints());
                P.Draw();
                Console.WriteLine();
                i++;
            }


            Console.WriteLine();
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("---------MyPersonalMapData---------");
            Console.WriteLine("-----------------------------------");

            MyPersonalMapData TestmyPersonalMapData = new MyPersonalMapData();
            Console.WriteLine("MyPersonalMapData : constructeur par defaut");
            Console.WriteLine(TestmyPersonalMapData.ToString());

            Console.WriteLine("\nConstructeur avec paramètres...");
            string nom;
            Console.Write("Saisir votre nom : "); nom = Console.ReadLine();
            string prenom;
            Console.Write("Saisir votre prenom : "); prenom = Console.ReadLine();
            string email;
            Console.Write("Saisir votre email : "); email = Console.ReadLine();

            
            ObservableCollection<CartoObj> newObservableCollectionCartoObj = new ObservableCollection<CartoObj>();
            foreach (CartoObj o in listeDeCartoObj)
            {
                newObservableCollectionCartoObj.Add(o);
            }
            MyPersonalMapData Test2myPersonalMapData = new MyPersonalMapData("zeevaert", "arnaud", "az@gmail.com", newObservableCollectionCartoObj);
            Console.WriteLine("MyPersonalMapData : new MyPersonalMapData(zeevaert, arnaud, az@gmail.com, listeDeCartoObj);");
            Console.WriteLine(Test2myPersonalMapData.ToString());

            /*Console.WriteLine("Test de save personne");         
            if (MyPersonalMapData.SavePersonne(Test2myPersonalMapData))
                Console.WriteLine("save ok");
            else
                Console.WriteLine("save pas ok");

            Console.WriteLine("pause...");
            string inutile;
            inutile = Console.ReadLine();

            Console.WriteLine("Test de load la meme personne");
            MyPersonalMapData personneTMP;
            if ((personneTMP = MyPersonalMapData.LoadPersonne(Test2myPersonalMapData)) == null)
            {
                Console.WriteLine("personne pas trouve :(");
            }
            else
                Console.WriteLine("Personne trouve :)");*/


            inutile = Console.ReadLine();
        }
    }
}
