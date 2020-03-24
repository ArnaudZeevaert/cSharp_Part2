using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FctUtilesPourFenetres
{
    public class fctUtilesPourFenetres
    {
        private bool validerNom(string newNom)
        {
            string ret_val = true;
            string lettreAutorisee = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZéè";

            if (newNom.Length > 0)
            {
                foreach (char c in newNom)
                {
                    if (!lettreAutorisee.Contains(c.ToString())
                        return false;
                }
            }
            retrun ret_val;
        }
    }
}
