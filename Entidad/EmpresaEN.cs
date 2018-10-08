using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class EmpresaEN
    {
        //IdEmpresa, Nombre, Direccion, Telefonos, NRuc, Logo, Celular, Email, SitioWeb
        public int IdEmpresa { set; get; }
        public string Nombre { set; get; }
        public string Direccion { set; get; }
        public string Descripcion { set; get; }
        public string Telefonos { set; get; }
        public string NRuc { set; get; }
        public string Celular { set; get; }
        public string Email { set; get; }
        public string SitioWeb { set; get; }
        /// <summary>
        /// Variable tipo objeto para el objeto de la imagen de la empresa
        /// </summary>
        public Object oLogo { set; get; }
        /// <summary>
        /// Arreglo de datos para la imagen de la empresa
        /// </summary>
        public byte[] ALogo { set; get; }

        public LoginEN oLoginEN = new LoginEN();

        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public string SubTituloDelReporte { set; get; }


    }
}
