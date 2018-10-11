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
    public class ProductoConfiguracionLN
    {

        public string Error { set; get; }

        private ProductoConfiguracionAD oProductoConfiguracionAD = new ProductoConfiguracionAD();

        public bool Agregar(ProductoConfiguracionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoConfiguracionAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oProductoConfiguracionAD.Error;
                return false;
            }

        }

        public bool Actualizar(ProductoConfiguracionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoConfiguracion.ToString()) || oREgistroEN.idProductoConfiguracion == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoConfiguracionAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoConfiguracionAD.Error;
                return false;
            }

        }

        public bool Eliminar(ProductoConfiguracionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoConfiguracion.ToString()) || oREgistroEN.idProductoConfiguracion == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoConfiguracionAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoConfiguracionAD.Error;
                return false;
            }

        }

        public bool Listado(ProductoConfiguracionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoConfiguracionAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoConfiguracionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ProductoConfiguracionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoConfiguracionAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoConfiguracionAD.Error;
                return false;
            }

        }
        
        public bool ListadoParaReportes(ProductoConfiguracionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoConfiguracionAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoConfiguracionAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(ProductoConfiguracionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoConfiguracionAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoConfiguracionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(ProductoConfiguracionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoConfiguracionAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoConfiguracionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oProductoConfiguracionAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oProductoConfiguracionAD.TraerDatos().Rows.Count;
        }

    }
}
