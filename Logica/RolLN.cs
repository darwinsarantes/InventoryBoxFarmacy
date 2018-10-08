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
    public class RolLN
    {

        public string Error { set; get; }

        private RolAD oRolAD = new RolAD();

        public bool Agregar(RolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oRolAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oRolAD.Error;
                return false;
            }

        }

        public bool Actualizar(RolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idRol.ToString()) || oREgistroEN.idRol == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oRolAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oRolAD.Error;
                return false;
            }

        }

        public bool Eliminar(RolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idRol.ToString()) || oREgistroEN.idRol == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oRolAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oRolAD.Error;
                return false;
            }

        }

        public bool Listado(RolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oRolAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oRolAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(RolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oRolAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oRolAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(RolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oRolAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oRolAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(RolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oRolAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oRolAD.Error;
                return false;
            }

        }

        public bool ValidarRegistroDuplicado(RolEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oRolAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oRolAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }
        
        public DataTable TraerDatos() {

            return oRolAD.TraerDatos();

        }

        public int TotalRegistros()
        {

            return oRolAD.TraerDatos().Rows.Count;

        }



    }
}
