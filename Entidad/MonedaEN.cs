using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class MonedaEN
    {        //idMoneda, Nombre, Abreviatura
        public int idMoneda { set; get; }
        public string Nombre { set; get; }
        public string Abreviatura { set; get; }
        
        public LoginEN oLoginEN = new LoginEN();

        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public String SubTituloDelReporte { set; get; }

    }
}
