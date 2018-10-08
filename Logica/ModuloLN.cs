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
    public class ModuloLN
    {

        public string Error { set; get; }

        private ModuloAD oModuloAD = new ModuloAD();

        public bool Agregar(ModuloEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oModuloAD.Error;
                return false;
            }

        }

        public bool Actualizar(ModuloEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idModulo.ToString()) || oREgistroEN.idModulo == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oModuloAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloAD.Error;
                return false;
            }

        }

        public bool Eliminar(ModuloEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idModulo.ToString()) || oREgistroEN.idModulo == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oModuloAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloAD.Error;
                return false;
            }

        }

        public bool Listado(ModuloEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ModuloEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(ModuloEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(ModuloEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloAD.Error;
                return false;
            }

        }

        public bool ValidarRegistroDuplicado(ModuloEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oModuloAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oModuloAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }
        
        public DataTable TraerDatos() {

            return oModuloAD.TraerDatos();

        }
        

    }
}
