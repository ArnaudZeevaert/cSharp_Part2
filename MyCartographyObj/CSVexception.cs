using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCartographyObj
{
    public class CSVexception : Exception
    {
        public CSVexception(string message) : base(message)
        {

        }
    }
}
