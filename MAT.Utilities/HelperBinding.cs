using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAT.Utilities
{
    public class HelperBinding
    {
        public class Habitacion
        {
            private static string _IdSelect = "";

            public static string IdSelect
            {
                get
                {
                    return _IdSelect;
                }
                set
                {
                    _IdSelect = value;
                }
            }
        }
    }
}
