using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class TransaccionesEN
    {     
        public int idtransaccion { set; get; }
        public int idusuario { set; get; }
        
        public string ip { set; get; }

        public string NombreDelEquipo { set; get; }

        public int idregistro { set; get; }
        public string tipodeoperacion {set; get;}

        public string descripcioninterna { set; get; }
        public string Estado { set; get; }
        public string Modelo { set; get; }
        public string Modulo { set; get; }
        public string Tabla { set; get; }
        public string descripciondelusuario { set; get; }
        public int idusuarioaprueba { set; get; }

        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public string SubTituloDelReporte { set; get; }

    }
}
