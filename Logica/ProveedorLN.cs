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
    public class ProveedorLN
    {

        public string Error { set; get; }

        private ProveedorAD oProveedorAD = new ProveedorAD();

        public bool Agregar(ProveedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oProveedorAD.Error;
                return false;
            }

        }

        public bool AgregarUtilizandoLaMismaConexion(ProveedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorAD.AgregarUtilizandoLaMismaConexion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorAD.Error;
                return false;
            }

        }

        public bool Actualizar(ProveedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProveedor.ToString()) || oREgistroEN.idProveedor == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProveedorAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorAD.Error;
                return false;
            }

        }

        public bool Eliminar(ProveedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProveedor.ToString()) || oREgistroEN.idProveedor == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProveedorAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorAD.Error;
                return false;
            }

        }

        public bool Listado(ProveedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ProveedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(ProveedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(ProveedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorAD.Error;
                return false;
            }

        }

        public bool GenerarCodigoDelProveedor(ProveedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorAD.GenerarCodigoDelProveedor(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorAD.Error;
                return false;
            }

        }

        public bool ValidarRegistroDuplicado(ProveedorEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProveedorAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProveedorAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(ProveedorEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProveedorAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProveedorAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oProveedorAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oProveedorAD.TraerDatos().Rows.Count;
        }



    }
}
