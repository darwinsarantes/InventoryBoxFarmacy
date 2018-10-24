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
    public class TiposDeEntradasLN
    {

        public string Error { set; get; }

        private TiposDeEntradasAD oTiposDeEntradasAD = new TiposDeEntradasAD();

        public bool Agregar(TiposDeEntradasEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTiposDeEntradasAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oTiposDeEntradasAD.Error;
                return false;
            }

        }

        public bool Actualizar(TiposDeEntradasEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idTipoDeEntrada.ToString()) || oREgistroEN.idTipoDeEntrada == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oTiposDeEntradasAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTiposDeEntradasAD.Error;
                return false;
            }

        }

        public bool Eliminar(TiposDeEntradasEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idTipoDeEntrada.ToString()) || oREgistroEN.idTipoDeEntrada == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oTiposDeEntradasAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTiposDeEntradasAD.Error;
                return false;
            }

        }

        public bool Listado(TiposDeEntradasEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTiposDeEntradasAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTiposDeEntradasAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(TiposDeEntradasEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTiposDeEntradasAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTiposDeEntradasAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(TiposDeEntradasEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTiposDeEntradasAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTiposDeEntradasAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(TiposDeEntradasEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTiposDeEntradasAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTiposDeEntradasAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(TiposDeEntradasEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oTiposDeEntradasAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oTiposDeEntradasAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(TiposDeEntradasEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oTiposDeEntradasAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oTiposDeEntradasAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oTiposDeEntradasAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oTiposDeEntradasAD.TraerDatos().Rows.Count;
        }



    }
}
