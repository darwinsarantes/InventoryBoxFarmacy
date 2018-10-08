using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ConfiguracionEN
    {
        //IdConfiguracion, RutaRespaldos, RutaRespaldosDeExcel, NombreDelSistema
        public int IdConfiguracion { set; get; }
        public string RutaRespaldos { set; get; }
        public string RutaRespaldosDeExcel { set; get; }        
        public string PathMysSQLDump { set; get; }
        public string PathMySQL { set; get; }
        public int TiempoDeRespaldo { set; get; }
        public string NombreDelSistema { set; get; }

        public LoginEN oLoginEN = new LoginEN();
        
        public string Where { set; get; }
        public string OrderBy { set; get; }

        public string TituloDelReporte { set; get; }
        public string SubTituloDelReporte { set; get; }

    }
}
