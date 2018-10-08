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
    public class UsuarioLN
    {

        public string Error { set; get; }

        private UsuarioAD oUsuarioAD = new UsuarioAD();

        public bool Agregar(UsuarioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUsuarioAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oUsuarioAD.Error;
                return false;
            }

        }

        public bool Actualizar(UsuarioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idUsuario.ToString()) || oREgistroEN.idUsuario == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oUsuarioAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUsuarioAD.Error;
                return false;
            }

        }

        public bool Eliminar(UsuarioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idUsuario.ToString()) || oREgistroEN.idUsuario == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oUsuarioAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUsuarioAD.Error;
                return false;
            }

        }

        public bool Listado(UsuarioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUsuarioAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUsuarioAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(UsuarioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUsuarioAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUsuarioAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(UsuarioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUsuarioAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUsuarioAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(UsuarioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUsuarioAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUsuarioAD.Error;
                return false;
            }

        }

        public bool ValidarRegistroDuplicadoPorContrasena(UsuarioEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oUsuarioAD.ValidarRegistroDuplicadoPorContrasena(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oUsuarioAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarRegistroDuplicadoPorNombre(UsuarioEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oUsuarioAD.ValidarRegistroDuplicadoPorNombre(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oUsuarioAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarRegistroDuplicadoPorNombreDeSesion(UsuarioEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oUsuarioAD.ValidarRegistroDuplicadoPorNombreDeSesion(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oUsuarioAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oUsuarioAD.TraerDatos();

        }

        public int TotalRegistros()
        {
            return oUsuarioAD.TraerDatos().Rows.Count;

        }


    }
}
