using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class CuentaDeBancosEN
    {
        
        public int idCuentaDeBancos { set; get; }
        public string NumeroDeCuenta { set; get; }
        public string Descripcion { set; get; }
        
        public LoginEN oLoginEN = new LoginEN();
        public BancosEN oBancosEN = new BancosEN();

        public int idUsuarioDeCreacion { set; get; }
        public DateTime FechaDeCreacion { set; get; }
        public int idUsuarioModificacion { set; get; }
        public DateTime FechaDeModificacion { set; get; }

        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public String SubTituloDelReporte { set; get; }

    }
}
