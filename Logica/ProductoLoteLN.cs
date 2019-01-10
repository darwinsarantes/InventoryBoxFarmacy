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
    public class ProductoLoteLN
    {

        public string Error { set; get; }

        private ProductoLoteAD oProductoLoteAD = new ProductoLoteAD();

        public bool Agregar(ProductoLoteEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoLoteAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oProductoLoteAD.Error;
                return false;
            }

        }
        
        public bool Actualizar(ProductoLoteEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idLoteDelProducto.ToString()) || oREgistroEN.idLoteDelProducto == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoLoteAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoLoteAD.Error;
                return false;
            }

        }
        
        public bool Eliminar(ProductoLoteEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idLoteDelProducto.ToString()) || oREgistroEN.idLoteDelProducto == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoLoteAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoLoteAD.Error;
                return false;
            }

        }
        
        public bool Listado(ProductoLoteEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoLoteAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoLoteAD.Error;
                return false;
            }

        }

        public bool ListadoDePruductosDelLotePorIdProducto(ProductoLoteEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoLoteAD.ListadoDePruductosDelLotePorIdProducto(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoLoteAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ProductoLoteEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoLoteAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoLoteAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(ProductoLoteEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoLoteAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoLoteAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(ProductoLoteEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoLoteAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoLoteAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(ProductoLoteEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoLoteAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoLoteAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }
        
        public bool ValidarSiElRegistroEstaVinculado(ProductoLoteEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoLoteAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoLoteAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oProductoLoteAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oProductoLoteAD.TraerDatos().Rows.Count;
        }
        
    }
}
