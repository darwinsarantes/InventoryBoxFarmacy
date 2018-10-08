using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class EntidadEN
    {        //idEntidad, idTipoDeEntidad, idUsuarioDeCreacion, FechaDeCreacion, idUsuarioModificacion, FechaDeModificacion
        public int idEntidad { set; get; }
        
        public int IdUsuarioDeCreacion { set; get; }
        public DateTime FechaDeCreacion { set; get; }
        public int IdUsuarioDeModificacion { set; get; }
        public DateTime FechaDeModificacion { set; get; }
        public string Estado { set; get; }

        public LoginEN oLoginEN = new LoginEN();
        public TipoDeEntidadEN oTipoDeEntidadEN = new TipoDeEntidadEN();

        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public string SubTituloDelReporte { set; get; }

    }
}
