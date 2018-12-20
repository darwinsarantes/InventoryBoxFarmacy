using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ProveedorLaboratorioEN
    {     
        public int idProveedorLaboratorio { set; get; }
       
        public int IdUsuarioDeCreacion { set; get; }
        public DateTime FechaDeCreacion { set; get; }
        public int IdUsuarioDeModificacion { set; get; }
        public DateTime FechaDeModificacion { set; get; }
        public string TablaDeReferencia { set; get; }
        public string ProveedorLaboratorio { set; get; }

        public LoginEN oLoginEN = new LoginEN();
        public ProveedorEN oProveedorEN = new ProveedorEN();
        public LaboratorioEN oLaboratorioEN = new LaboratorioEN();

        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public string SubTituloDelReporte { set; get; }

    }
}
