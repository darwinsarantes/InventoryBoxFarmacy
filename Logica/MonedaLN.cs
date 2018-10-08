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
    public class MonedaLN
    {

        public string Error { set; get; }

        private MonedaAD oMonedaAD = new MonedaAD();

        public bool Agregar(MonedaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oMonedaAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oMonedaAD.Error;
                return false;
            }

        }

        public bool Actualizar(MonedaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idMoneda.ToString()) || oREgistroEN.idMoneda == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oMonedaAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {//
                Error = oMonedaAD.Error;
                return false;
            }

        }

        public bool Eliminar(MonedaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idMoneda.ToString()) || oREgistroEN.idMoneda == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oMonedaAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oMonedaAD.Error;
                return false;
            }

        }

        public bool Listado(MonedaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oMonedaAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oMonedaAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(MonedaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oMonedaAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oMonedaAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(MonedaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oMonedaAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oMonedaAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(MonedaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oMonedaAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oMonedaAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(MonedaEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oMonedaAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oMonedaAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(MonedaEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oMonedaAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oMonedaAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oMonedaAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oMonedaAD.TraerDatos().Rows.Count;
        }



    }
}
