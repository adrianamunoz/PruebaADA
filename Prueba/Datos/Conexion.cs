using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba.Datos
{
    public class Conexion
    {
        private string cadenaSQL = string.Empty;

        public Conexion()
        {
            cadenaSQL = "Persist Security Info=False;User ID=Ubd-Pru-appsweb;Password=Oprh73.6LnQw;Initial Catalog=LC-PRU-Enla_com;Server=TEST1\\CLBDSTEST01";

        }

        public string getCadenaSql()
        {
            return cadenaSQL;
        }
    }
}