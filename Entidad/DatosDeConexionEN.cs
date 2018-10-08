using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class DatosDeConexionEN
    {
        public string Servidor { set; get; }
        public string Usuario { set; get; }
        public string Contraseña { set; get; }
        public string BaseDeDatos { set; get; }

        public string PuertoDeConexionDelServidor { set; get; }
    }
}
