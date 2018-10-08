using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using AccesoDatos;
using System.Data;

namespace Logica
{
    public class LoginLN
    {

        public string Error { set; get; }

        private LoginAD oLoginAD = new LoginAD();

        public bool IniciarLaSesionDelUsuario(LoginEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            if (oLoginAD.IniciarLaSesionDelUsuario(oRegistroEN, oDatos))
            {

                Error = string.Empty;
                return true;

            }
            else {
                Error = oLoginAD.Error;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oLoginAD.TraerDatos();

        }
        

    }
}
