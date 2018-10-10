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
    public class ProductoImagenesLN
    {

        public string Error { set; get; }

        private ProductoImagenesAD oProductoImagenesAD = new ProductoImagenesAD();

        public bool Agregar(ProductoImagenesEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoImagenesAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oProductoImagenesAD.Error;
                return false;
            }

        }

        public bool Actualizar(ProductoImagenesEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoImagenes.ToString()) || oREgistroEN.idProductoImagenes == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoImagenesAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoImagenesAD.Error;
                return false;
            }

        }

        public bool Eliminar(ProductoImagenesEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoImagenes.ToString()) || oREgistroEN.idProductoImagenes == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoImagenesAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoImagenesAD.Error;
                return false;
            }

        }

        public bool Listado(ProductoImagenesEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoImagenesAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoImagenesAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ProductoImagenesEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoImagenesAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoImagenesAD.Error;
                return false;
            }

        }
        
        public bool ListadoParaReportes(ProductoImagenesEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoImagenesAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoImagenesAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(ProductoImagenesEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoImagenesAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoImagenesAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(ProductoImagenesEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoImagenesAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoImagenesAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oProductoImagenesAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oProductoImagenesAD.TraerDatos().Rows.Count;
        }



    }
}
