using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class BodegaAlmacenEN
    { 
        public int idBodegaAlmacen { set; get; }
        
        public int idUsuarioDeCreacion { set; get; }
        public DateTime FechaDeCreacion { set; get; }
        public int idUsuarioModificacion { set; get; }
        public DateTime FechaDeModificacion { set; get; }
        
        public LoginEN oLoginEN = new LoginEN();
        public BodegaEN oBodegaEN = new BodegaEN();
        public AlmacenEN oAlmacenEN = new AlmacenEN();

        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public String SubTituloDelReporte { set; get; }

    }

}
