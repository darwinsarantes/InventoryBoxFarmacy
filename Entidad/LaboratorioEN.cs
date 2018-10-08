using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class LaboratorioEN
    {        //Twitter, idUsuarioDeCreacion, FechaDeCreacion, idUsuarioModificacion, FechaDeModificacion
        public int idLaboratorio { set; get; }
        public string Codigo { set; get; }
        public string Nombre { set; get; }
        public string Direccion { set; get; }
        public string NoRUC { set; get; }
        public string SitioWeb { set; get; }
        public string Telefono { set; get; }
        public string Movil { set; get; }
        public string Observaciones { set; get; }
        public string Correo { set; get; }
        public string FechaDeCumpleanos { set; get; }
        public string Messenger { set; get; }
        public string Skype { set; get; }
        public string Twitter { set; get; }
        public string Facebook { set; get; }
        public string Estado { set; get; }

        /// <summary>
        /// Variable tipo objeto para el objeto de la imagen de la empresa
        /// </summary>
        public Object Foto { set; get; }
        /// <summary>
        /// Arreglo de datos para la imagen de la empresa
        /// </summary>
        public byte[] AFoto { set; get; }

        public int IdUsuarioDeCreacion { set; get; }
        public DateTime FechaDeCreacion { set; get; }
        public int IdUsuarioDeModificacion { set; get; }
        public DateTime FechaDeModificacion { set; get; }

        public LoginEN oLoginEN = new LoginEN();
        public EntidadEN oEntidadEN = new EntidadEN();

        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public string SubTituloDelReporte { set; get; }

    }

}
