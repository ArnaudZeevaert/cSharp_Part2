﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCartographyObj
{
    public interface IisPointClose
    {
        bool IsPointClose(Coordonnees coordonneesPoint, double precision);
    }
}
