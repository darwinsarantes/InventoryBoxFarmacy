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
    public class TipoDeUbicacionLN
    {

        public string Error { set; get; }

        private TipoDeUbicacionAD oTipoDeUbicacionAD = new TipoDeUbicacionAD();

        public bool Agregar(TipoDeUbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTipoDeUbicacionAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oTipoDeUbicacionAD.Error;
                return false;
            }

        }

        public bool Actualizar(TipoDeUbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idTipoDeUbicacion.ToString()) || oREgistroEN.idTipoDeUbicacion == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oTipoDeUbicacionAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeUbicacionAD.Error;
                return false;
            }

        }

        public bool Eliminar(TipoDeUbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idTipoDeUbicacion.ToString()) || oREgistroEN.idTipoDeUbicacion == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oTipoDeUbicacionAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeUbicacionAD.Error;
                return false;
            }

        }

        public bool Listado(TipoDeUbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTipoDeUbicacionAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeUbicacionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(TipoDeUbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTipoDeUbicacionAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeUbicacionAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(TipoDeUbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTipoDeUbicacionAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeUbicacionAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(TipoDeUbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTipoDeUbicacionAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeUbicacionAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(TipoDeUbicacionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oTipoDeUbicacionAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oTipoDeUbicacionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(TipoDeUbicacionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oTipoDeUbicacionAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oTipoDeUbicacionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oTipoDeUbicacionAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oTipoDeUbicacionAD.TraerDatos().Rows.Count;
        }



    }
}
