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
    public class ModuloInterfazLN
    {

        public string Error { set; get; }

        private ModuloInterfazAD oModuloInterfazAD = new ModuloInterfazAD();

        public bool Agregar(ModuloInterfazEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oModuloInterfazAD.Error;
                return false;
            }

        }

        public bool Actualizar(ModuloInterfazEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idModuloInterfaz.ToString()) || oREgistroEN.idModuloInterfaz == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oModuloInterfazAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazAD.Error;
                return false;
            }

        }

        public bool Eliminar(ModuloInterfazEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idModuloInterfaz.ToString()) || oREgistroEN.idModuloInterfaz == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oModuloInterfazAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazAD.Error;
                return false;
            }

        }

        public bool Listado(ModuloInterfazEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ModuloInterfazEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(ModuloInterfazEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(ModuloInterfazEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazAD.Error;
                return false;
            }

        }

        public bool ValidarRegistroDuplicado(ModuloInterfazEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oModuloInterfazAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oModuloInterfazAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }
        
        public DataTable TraerDatos() {

            return oModuloInterfazAD.TraerDatos();

        }
        

    }
}
