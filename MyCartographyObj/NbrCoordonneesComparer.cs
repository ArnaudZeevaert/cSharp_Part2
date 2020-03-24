using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCartographyObj
{
    public class NbrCoordonneesComparer : IComparer<Polyline>
    {
        public int Compare(Polyline x, Polyline y)
        {
            return x.GetNbPoints().CompareTo(y.GetNbPoints());
        }
    }
}
