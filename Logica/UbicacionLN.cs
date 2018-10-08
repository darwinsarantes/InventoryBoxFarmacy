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
    public class UbicacionLN
    {

        public string Error { set; get; }

        private UbicacionAD oUbicacionAD = new UbicacionAD();

        public bool Agregar(UbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
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

        public bool Actualizar(UbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idUbicacion.ToString()) || oREgistroEN.idUbicacion == 0) {

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

        public bool Eliminar(UbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idUbicacion.ToString()) || oREgistroEN.idUbicacion == 0)
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

        public bool Listado(UbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
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

        public bool ListadoPorIdentificador(UbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
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

        public bool ListadoParaCombos(UbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
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

        public bool ListadoParaReportes(UbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
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
        
        public bool ValidarRegistroDuplicado(UbicacionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

        public bool ValidarSiElRegistroEstaVinculado(UbicacionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

        public DataTable TraerDatos() {

            return oUbicacionAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oUbicacionAD.TraerDatos().Rows.Count;
        }



    }
}
