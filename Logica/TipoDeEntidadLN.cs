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
    public class TipoDeEntidadLN
    {

        public string Error { set; get; }

        private TipoDeEntidadAD oTipoDeEntidadAD = new TipoDeEntidadAD();

        public bool Agregar(TipoDeEntidadEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTipoDeEntidadAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oTipoDeEntidadAD.Error;
                return false;
            }

        }

        public bool Actualizar(TipoDeEntidadEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idTipoDeEntidad.ToString()) || oREgistroEN.idTipoDeEntidad == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oTipoDeEntidadAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeEntidadAD.Error;
                return false;
            }

        }

        public bool Eliminar(TipoDeEntidadEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idTipoDeEntidad.ToString()) || oREgistroEN.idTipoDeEntidad == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oTipoDeEntidadAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeEntidadAD.Error;
                return false;
            }

        }

        public bool Listado(TipoDeEntidadEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTipoDeEntidadAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeEntidadAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(TipoDeEntidadEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTipoDeEntidadAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeEntidadAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(TipoDeEntidadEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTipoDeEntidadAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeEntidadAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(TipoDeEntidadEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTipoDeEntidadAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeEntidadAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(TipoDeEntidadEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oTipoDeEntidadAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oTipoDeEntidadAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(TipoDeEntidadEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oTipoDeEntidadAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oTipoDeEntidadAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oTipoDeEntidadAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oTipoDeEntidadAD.TraerDatos().Rows.Count;
        }



    }
}
