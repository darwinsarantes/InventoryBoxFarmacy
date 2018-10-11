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
    public class ProductoLaboratoriosLN
    {

        public string Error { set; get; }

        private ProductoLaboratoriosAD oProductoLaboratoriosAD = new ProductoLaboratoriosAD();

        public bool Agregar(ProductoLaboratoriosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoLaboratoriosAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oProductoLaboratoriosAD.Error;
                return false;
            }

        }

        public bool Actualizar(ProductoLaboratoriosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoLaboratorios.ToString()) || oREgistroEN.idProductoLaboratorios == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoLaboratoriosAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoLaboratoriosAD.Error;
                return false;
            }

        }

        public bool Eliminar(ProductoLaboratoriosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoLaboratorios.ToString()) || oREgistroEN.idProductoLaboratorios == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoLaboratoriosAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoLaboratoriosAD.Error;
                return false;
            }

        }

        public bool Listado(ProductoLaboratoriosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoLaboratoriosAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoLaboratoriosAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ProductoLaboratoriosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoLaboratoriosAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoLaboratoriosAD.Error;
                return false;
            }

        }
        
        public bool ListadoParaReportes(ProductoLaboratoriosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoLaboratoriosAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoLaboratoriosAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(ProductoLaboratoriosEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoLaboratoriosAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoLaboratoriosAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(ProductoLaboratoriosEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoLaboratoriosAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoLaboratoriosAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oProductoLaboratoriosAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oProductoLaboratoriosAD.TraerDatos().Rows.Count;
        }

    }
}
