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
    public class EntidadLN
    {

        public string Error { set; get; }

        private EntidadAD oEntidadAD = new EntidadAD();

        public bool Agregar(EntidadEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oEntidadAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oEntidadAD.Error;
                return false;
            }

        }

        public bool Actualizar(EntidadEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idEntidad.ToString()) || oREgistroEN.idEntidad == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oEntidadAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oEntidadAD.Error;
                return false;
            }

        }

        public bool Eliminar(EntidadEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idEntidad.ToString()) || oREgistroEN.idEntidad == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oEntidadAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oEntidadAD.Error;
                return false;
            }

        }

        public bool Listado(EntidadEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oEntidadAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oEntidadAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(EntidadEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oEntidadAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oEntidadAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(EntidadEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oEntidadAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oEntidadAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(EntidadEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oEntidadAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oEntidadAD.Error;
                return false;
            }

        }
        
        public bool ValidarSiElRegistroEstaVinculado(EntidadEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oEntidadAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oEntidadAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oEntidadAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oEntidadAD.TraerDatos().Rows.Count;
        }
        
    }
}
