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
    public class ProductoPromocionLN
    {

        public string Error { set; get; }

        private ProductoPromocionAD oProductoPromocionAD = new ProductoPromocionAD();

        public bool Agregar(ProductoPromocionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoPromocionAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oProductoPromocionAD.Error;
                return false;
            }

        }

        public bool Actualizar(ProductoPromocionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoPromocion.ToString()) || oREgistroEN.idProductoPromocion == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoPromocionAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPromocionAD.Error;
                return false;
            }

        }

        public bool Eliminar(ProductoPromocionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoPromocion.ToString()) || oREgistroEN.idProductoPromocion == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoPromocionAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPromocionAD.Error;
                return false;
            }

        }

        public bool Listado(ProductoPromocionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoPromocionAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPromocionAD.Error;
                return false;
            }

        }

        public bool ListadoPromocionesXProducto(ProductoPromocionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoPromocionAD.ListadoPromocionesXProducto(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPromocionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ProductoPromocionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoPromocionAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPromocionAD.Error;
                return false;
            }

        }
        
        public bool ListadoParaReportes(ProductoPromocionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoPromocionAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPromocionAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(ProductoPromocionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoPromocionAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoPromocionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(ProductoPromocionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoPromocionAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoPromocionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oProductoPromocionAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oProductoPromocionAD.TraerDatos().Rows.Count;
        }
        
    }
}
