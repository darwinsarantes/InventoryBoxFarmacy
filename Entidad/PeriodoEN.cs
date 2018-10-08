using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class PeriodoEN
    {      
        public int idPeriodo { set; get; }
        public string Codigo { set; get; }
        public DateTime Desde { set; get; }
        public DateTime Hasta { set; get; }
        public string Nombre { set; get; }
        public string Obsevaciones { set; get; }
        public string Estado { set; get; }

        public int IdUsuarioDeCreacion { set; get; }
        public DateTime FechaDeCreacion { set; get; }
        public int IdUsuarioDeModificacion { set; get; }
        public DateTime FechaDeModificacion { set; get; }

        public LoginEN oLoginEN = new LoginEN();

        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public string SubTituloDelReporte { set; get; }

    }
}
