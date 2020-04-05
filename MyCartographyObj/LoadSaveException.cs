using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCartographyObj
{
    public class LoadSaveException : Exception
    {
        public LoadSaveException(string message) : base(message)
        { }
    }
}
