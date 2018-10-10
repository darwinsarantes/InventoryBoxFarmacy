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
    public class ProductoUnidadDeMedidaLN
    {

        public string Error { set; get; }

        private ProductoUnidadDeMedidaAD oProductoUnidadDeMedidaAD = new ProductoUnidadDeMedidaAD();

        public bool Agregar(ProductoUnidadDeMedidaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoUnidadDeMedidaAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oProductoUnidadDeMedidaAD.Error;
                return false;
            }

        }

        public bool Actualizar(ProductoUnidadDeMedidaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoUnidadDeMedida.ToString()) || oREgistroEN.idProductoUnidadDeMedida == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoUnidadDeMedidaAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoUnidadDeMedidaAD.Error;
                return false;
            }

        }

        public bool Eliminar(ProductoUnidadDeMedidaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoUnidadDeMedida.ToString()) || oREgistroEN.idProductoUnidadDeMedida == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoUnidadDeMedidaAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoUnidadDeMedidaAD.Error;
                return false;
            }

        }

        public bool Listado(ProductoUnidadDeMedidaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoUnidadDeMedidaAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoUnidadDeMedidaAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ProductoUnidadDeMedidaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoUnidadDeMedidaAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoUnidadDeMedidaAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(ProductoUnidadDeMedidaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoUnidadDeMedidaAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoUnidadDeMedidaAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(ProductoUnidadDeMedidaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoUnidadDeMedidaAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoUnidadDeMedidaAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(ProductoUnidadDeMedidaEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoUnidadDeMedidaAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoUnidadDeMedidaAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(ProductoUnidadDeMedidaEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoUnidadDeMedidaAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoUnidadDeMedidaAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oProductoUnidadDeMedidaAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oProductoUnidadDeMedidaAD.TraerDatos().Rows.Count;
        }
        
    }
}
