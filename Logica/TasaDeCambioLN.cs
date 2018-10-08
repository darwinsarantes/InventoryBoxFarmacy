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
    public class TasaDeCambioLN
    {

        public string Error { set; get; }

        private TasaDeCambioAD oTasaDeCambioAD = new TasaDeCambioAD();

        public bool Agregar(TasaDeCambioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTasaDeCambioAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oTasaDeCambioAD.Error;
                return false;
            }

        }

        public bool Actualizar(TasaDeCambioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idTasaDeCambio.ToString()) || oREgistroEN.idTasaDeCambio == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oTasaDeCambioAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTasaDeCambioAD.Error;
                return false;
            }

        }

        public bool Eliminar(TasaDeCambioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idTasaDeCambio.ToString()) || oREgistroEN.idTasaDeCambio == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oTasaDeCambioAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTasaDeCambioAD.Error;
                return false;
            }

        }

        public bool Listado(TasaDeCambioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTasaDeCambioAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTasaDeCambioAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(TasaDeCambioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTasaDeCambioAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTasaDeCambioAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(TasaDeCambioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTasaDeCambioAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTasaDeCambioAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(TasaDeCambioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTasaDeCambioAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTasaDeCambioAD.Error;
                return false;
            }

        }

        public bool TraerTasaDeCambioDelMesEnBaseATransacciones(TasaDeCambioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTasaDeCambioAD.TraerTasaDeCambioDelMesEnBaseATransacciones(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTasaDeCambioAD.Error;
                return false;
            }

        }

        public bool TraerTasaDeCambioDelMesPorFecha(TasaDeCambioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTasaDeCambioAD.TraerTasaDeCambioDelMesPorFecha(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTasaDeCambioAD.Error;
                return false;
            }

        }

        public bool ValidarRegistroDuplicado(TasaDeCambioEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oTasaDeCambioAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oTasaDeCambioAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(TasaDeCambioEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oTasaDeCambioAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oTasaDeCambioAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oTasaDeCambioAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oTasaDeCambioAD.TraerDatos().Rows.Count;
        }



    }
}
