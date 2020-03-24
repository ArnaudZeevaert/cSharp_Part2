using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathUtil
{
    public class MathUtilFct
    {
        public static double LongueurD_unSegment(double XA, double YA, double XB, double YB)
        {
            double res;
            //Console.WriteLine("DEBUG longueur d'un segment: Math.Sqrt(Math.Pow(({0} - {1}), 2) + Math.Pow(({2} - {3}), 2));", XB, XA, YB, YA);
            res = Math.Sqrt(Math.Pow((XB - XA), 2) + Math.Pow((YB - YA), 2));
            //Console.WriteLine("résultat : {0}", res);
            return res;
        }

       

    }
}
