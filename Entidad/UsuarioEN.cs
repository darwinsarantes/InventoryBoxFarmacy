using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class UsuarioEN
    {

        public int idUsuario { set; get; }
        public string Nombre { set; get; }
        public string Login { set; get; }
        public string Contrasena { set; get; }
        public string Email { set; get; }
        public string Estado { set; get; }
        
        public int IdUsuarioDeCreacion { set; get; }
        public DateTime FechaDeCreacion { set; get; }
        public int IdUsuarioDeModificacion { set; get; }
        public DateTime FechaDeModificacion { set; get; }

        public LoginEN oLoginEN = new LoginEN();
        public RolEN oRolEN = new RolEN();

        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public string SubTiuloDelReporte { set; get; }

    }
}
