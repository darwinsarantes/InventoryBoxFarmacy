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
    public class BodegaLocacionLN
    {

        public string Error { set; get; }

        private BodegaLocacionAD oBodegaLocacionAD = new BodegaLocacionAD();

        public bool Agregar(BodegaLocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaLocacionAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oBodegaLocacionAD.Error;
                return false;
            }

        }

        public bool Actualizar(BodegaLocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idBodegaLocacion.ToString()) || oREgistroEN.idBodegaLocacion == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oBodegaLocacionAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaLocacionAD.Error;
                return false;
            }

        }

        public bool Eliminar(BodegaLocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idBodegaLocacion.ToString()) || oREgistroEN.idBodegaLocacion == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oBodegaLocacionAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaLocacionAD.Error;
                return false;
            }

        }

        public bool Listado(BodegaLocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaLocacionAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaLocacionAD.Error;
                return false;
            }

        }

        public bool ListadoDeBodegasPorLocacion(BodegaLocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaLocacionAD.ListadoDeBodegasPorLocacion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaLocacionAD.Error;
                return false;
            }

        }

        public bool ListadoDeLocacionesPorBodega(BodegaLocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaLocacionAD.ListadoDeLocacionesPorBodega(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaLocacionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(BodegaLocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaLocacionAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaLocacionAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(BodegaLocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaLocacionAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaLocacionAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(BodegaLocacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaLocacionAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaLocacionAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(BodegaLocacionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oBodegaLocacionAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oBodegaLocacionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(BodegaLocacionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oBodegaLocacionAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oBodegaLocacionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oBodegaLocacionAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oBodegaLocacionAD.TraerDatos().Rows.Count;
        }



    }
}
