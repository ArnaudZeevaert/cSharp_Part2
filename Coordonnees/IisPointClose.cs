using System;
using System.Collections.Generic;
using System.Text;

namespace MyCartographyObjects
{
    public interface IisPointClose
    {
        bool IsPointClose(Coordonnees coordonneesPoint, double precision);
    }
}
