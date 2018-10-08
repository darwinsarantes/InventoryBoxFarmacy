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
    public class PrivilegioLN
    {

        public string Error { set; get; }

        private PrivilegioAD oPrivilegioAD = new PrivilegioAD();

        public bool Agregar(PrivilegioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oPrivilegioAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oPrivilegioAD.Error;
                return false;
            }

        }

        public bool Actualizar(PrivilegioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idPrivilegio.ToString()) || oREgistroEN.idPrivilegio == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oPrivilegioAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oPrivilegioAD.Error;
                return false;
            }

        }

        public bool Eliminar(PrivilegioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idPrivilegio.ToString()) || oREgistroEN.idPrivilegio == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oPrivilegioAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oPrivilegioAD.Error;
                return false;
            }

        }

        public bool Listado(PrivilegioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oPrivilegioAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oPrivilegioAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(PrivilegioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oPrivilegioAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oPrivilegioAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(PrivilegioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oPrivilegioAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oPrivilegioAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(PrivilegioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oPrivilegioAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oPrivilegioAD.Error;
                return false;
            }

        }

        public bool ValidarRegistroDuplicado(PrivilegioEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oPrivilegioAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oPrivilegioAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }
        
        public DataTable TraerDatos() {

            return oPrivilegioAD.TraerDatos();

        }
        

    }
}
