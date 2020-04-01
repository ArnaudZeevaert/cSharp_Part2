using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace getNewId
{
    public class IdFichier
    {
        public static int GetAnNewId()
        {
            int ret_val = - 1;
            string nomFichier = @"id.dat";

            if (File.Exists(nomFichier))
            {
                try
                {
                    using (BinaryReader readFile = new BinaryReader(File.Open(nomFichier, FileMode.Open)))
                    {
                        ret_val = readFile.ReadInt32();
                    }

                    using (BinaryWriter writer = new BinaryWriter(File.Open(nomFichier, FileMode.Open)))
                    {
                        writer.Write(ret_val + 1);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("ERROR : GetAnNewId : " + e.Message);
                    Environment.Exit(1);
                }
            }
            else
            {
                try
                {                    
                    using (BinaryWriter writer = new BinaryWriter(File.Open(nomFichier, FileMode.Create)))
                    {
                        writer.Write(2);//on écrit 2 puisque le premier id = 1 et est déja return 
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR : GetAnNewId : creation du fichier : " + e.Message);
                    Environment.Exit(1);
                }

                ret_val = 1;
            }
            return ret_val;
        }
    }
}
