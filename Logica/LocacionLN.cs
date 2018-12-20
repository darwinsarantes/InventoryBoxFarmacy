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
    public class LocacionLN
    {

        public string Error { set; get; }

        private LocacionAD oUbicacionAD = new LocacionAD();

        public bool Agregar(LocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUbicacionAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oUbicacionAD.Error;
                return false;
            }

        }
        
        public bool Actualizar(LocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idLocacion.ToString()) || oREgistroEN.idLocacion == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oUbicacionAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUbicacionAD.Error;
                return false;
            }

        }

        public bool Eliminar(LocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idLocacion.ToString()) || oREgistroEN.idLocacion == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oUbicacionAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUbicacionAD.Error;
                return false;
            }

        }
        
        public bool Listado(LocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUbicacionAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUbicacionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdDeBodega(LocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUbicacionAD.ListadoPorIdDeBodega(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUbicacionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(LocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUbicacionAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUbicacionAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(LocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUbicacionAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUbicacionAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(LocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUbicacionAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUbicacionAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(LocacionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oUbicacionAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oUbicacionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarCodigo(LocacionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oUbicacionAD.ValidarCodigo(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oUbicacionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(LocacionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oUbicacionAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oUbicacionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool VerificarSiLaEntidadEstaAsociadaAProducto(LocacionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oUbicacionAD.VerificarSiLaEntidadEstaAsociadaAProducto(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oUbicacionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oUbicacionAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oUbicacionAD.TraerDatos().Rows.Count;
        }



    }
}
