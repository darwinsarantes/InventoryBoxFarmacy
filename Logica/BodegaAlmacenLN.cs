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
    public class BodegaAlmacenLN
    {

        public string Error { set; get; }

        private BodegaAlmacenAD oBodegaAlmacenAD = new BodegaAlmacenAD();

        public bool Agregar(BodegaAlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaAlmacenAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oBodegaAlmacenAD.Error;
                return false;
            }

        }

        public bool Actualizar(BodegaAlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idBodegaAlmacen.ToString()) || oREgistroEN.idBodegaAlmacen == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oBodegaAlmacenAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAlmacenAD.Error;
                return false;
            }

        }

        public bool Eliminar(BodegaAlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idBodegaAlmacen.ToString()) || oREgistroEN.idBodegaAlmacen == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oBodegaAlmacenAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAlmacenAD.Error;
                return false;
            }

        }

        public bool Listado(BodegaAlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaAlmacenAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAlmacenAD.Error;
                return false;
            }

        }

        public bool ListadoDeAlmacenesPorBodega(BodegaAlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaAlmacenAD.ListadoDeAlmacenesPorBodega(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAlmacenAD.Error;
                return false;
            }

        }

        public bool ListadoDeBodegasPorAlmacen(BodegaAlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaAlmacenAD.ListadoDeBodegasPorAlmacen(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAlmacenAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(BodegaAlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaAlmacenAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAlmacenAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(BodegaAlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaAlmacenAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAlmacenAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(BodegaAlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaAlmacenAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAlmacenAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(BodegaAlmacenEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oBodegaAlmacenAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oBodegaAlmacenAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(BodegaAlmacenEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oBodegaAlmacenAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oBodegaAlmacenAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oBodegaAlmacenAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oBodegaAlmacenAD.TraerDatos().Rows.Count;
        }



    }
}
