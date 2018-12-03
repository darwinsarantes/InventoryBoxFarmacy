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
    public class ProductoSustitutosLN
    {

        public string Error { set; get; }

        private ProductoSustitutosAD oProductoSustitutosAD = new ProductoSustitutosAD();

        public bool Agregar(ProductoSustitutosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoSustitutosAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oProductoSustitutosAD.Error;
                return false;
            }

        }

        public bool Actualizar(ProductoSustitutosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoSustitutos.ToString()) || oREgistroEN.idProductoSustitutos == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoSustitutosAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoSustitutosAD.Error;
                return false;
            }

        }

        public bool Eliminar(ProductoSustitutosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoSustitutos.ToString()) || oREgistroEN.idProductoSustitutos == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoSustitutosAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoSustitutosAD.Error;
                return false;
            }

        }

        public bool Listado(ProductoSustitutosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoSustitutosAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoSustitutosAD.Error;
                return false;
            }

        }

        public bool ListadoDeProductosXIdProducto(ProductoSustitutosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoSustitutosAD.ListadoDeProductosXIdProducto(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoSustitutosAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ProductoSustitutosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoSustitutosAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoSustitutosAD.Error;
                return false;
            }

        }
        
        public bool ListadoParaReportes(ProductoSustitutosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoSustitutosAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoSustitutosAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(ProductoSustitutosEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoSustitutosAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoSustitutosAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(ProductoSustitutosEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoSustitutosAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoSustitutosAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oProductoSustitutosAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oProductoSustitutosAD.TraerDatos().Rows.Count;
        }

    }
}
