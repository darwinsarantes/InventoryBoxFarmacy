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
    public class TipoDeSalidaLN
    {

        public string Error { set; get; }

        private TipoDeSalidaAD oTipoDeSalidaAD = new TipoDeSalidaAD();

        public bool Agregar(TipoDeSalidaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTipoDeSalidaAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oTipoDeSalidaAD.Error;
                return false;
            }

        }

        public bool Actualizar(TipoDeSalidaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idTipoDeSalida.ToString()) || oREgistroEN.idTipoDeSalida == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oTipoDeSalidaAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeSalidaAD.Error;
                return false;
            }

        }

        public bool Eliminar(TipoDeSalidaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idTipoDeSalida.ToString()) || oREgistroEN.idTipoDeSalida == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oTipoDeSalidaAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeSalidaAD.Error;
                return false;
            }

        }

        public bool Listado(TipoDeSalidaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTipoDeSalidaAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeSalidaAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(TipoDeSalidaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTipoDeSalidaAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeSalidaAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(TipoDeSalidaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTipoDeSalidaAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeSalidaAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(TipoDeSalidaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oTipoDeSalidaAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oTipoDeSalidaAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(TipoDeSalidaEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oTipoDeSalidaAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oTipoDeSalidaAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(TipoDeSalidaEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oTipoDeSalidaAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oTipoDeSalidaAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oTipoDeSalidaAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oTipoDeSalidaAD.TraerDatos().Rows.Count;
        }



    }
}
