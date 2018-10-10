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
    public class ProductoPrecioLN
    {

        public string Error { set; get; }

        private ProductoPrecioAD oProductoPrecioAD = new ProductoPrecioAD();

        public bool Agregar(ProductoPrecioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoPrecioAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oProductoPrecioAD.Error;
                return false;
            }

        }

        public bool Actualizar(ProductoPrecioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoPrecio.ToString()) || oREgistroEN.idProductoPrecio == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoPrecioAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPrecioAD.Error;
                return false;
            }

        }

        public bool Eliminar(ProductoPrecioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoPrecio.ToString()) || oREgistroEN.idProductoPrecio == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoPrecioAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPrecioAD.Error;
                return false;
            }

        }

        public bool Listado(ProductoPrecioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoPrecioAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPrecioAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ProductoPrecioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoPrecioAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPrecioAD.Error;
                return false;
            }

        }
        
        public bool ListadoParaReportes(ProductoPrecioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoPrecioAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPrecioAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(ProductoPrecioEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoPrecioAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoPrecioAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(ProductoPrecioEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoPrecioAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoPrecioAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oProductoPrecioAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oProductoPrecioAD.TraerDatos().Rows.Count;
        }



    }
}
