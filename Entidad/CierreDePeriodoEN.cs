using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class CierreDePeriodoEN
    {        
        public int idCierreDePeriodo { set; get; }
        public DateTime FechaDeCierre { set; get; }
        public string Descripcion { set; get; }
        
        public int IdUsuarioDeCreacion { set; get; }
        public DateTime FechaDeCreacion { set; get; }
        public int IdUsuarioDeModificacion { set; get; }
        public DateTime FechaDeModificacion { set; get; }

        public LoginEN oLoginEN = new LoginEN();
        public PeriodoEN oPeriodoEN = new PeriodoEN();
        public UsuarioEN oUsuarioDeCierre = new UsuarioEN();
        public TasaDeCambioEN oTasaDeCambioEN = new TasaDeCambioEN();
        
        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public string SubTituloDelReporte { set; get; }

    }

}
