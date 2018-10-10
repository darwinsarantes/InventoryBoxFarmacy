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
    public class ProductoLN
    {

        public string Error { set; get; }

        private ProductoAD oProductoAD = new ProductoAD();

        public bool Agregar(ProductoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oProductoAD.Error;
                return false;
            }

        }

        public bool Actualizar(ProductoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProducto.ToString()) || oREgistroEN.idProducto == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoAD.Error;
                return false;
            }

        }

        public bool Eliminar(ProductoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProducto.ToString()) || oREgistroEN.idProducto == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoAD.Error;
                return false;
            }

        }

        public bool Listado(ProductoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ProductoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(ProductoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(ProductoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(ProductoEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(ProductoEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oProductoAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oProductoAD.TraerDatos().Rows.Count;
        }

    }
}
