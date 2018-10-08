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
    public class ProveedorContactoLN
    {

        public string Error { set; get; }

        private ProveedorContactoAD oProveedorContactoAD = new ProveedorContactoAD();

        public bool Agregar(ProveedorContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorContactoAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oProveedorContactoAD.Error;
                return false;
            }

        }

        public bool Actualizar(ProveedorContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProveedorContacto.ToString()) || oREgistroEN.idProveedorContacto == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProveedorContactoAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorContactoAD.Error;
                return false;
            }

        }

        public bool Eliminar(ProveedorContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProveedorContacto.ToString()) || oREgistroEN.idProveedorContacto == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProveedorContactoAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorContactoAD.Error;
                return false;
            }

        }

        public bool Listado(ProveedorContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorContactoAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorContactoAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ProveedorContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorContactoAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorContactoAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificadorDelContacto(ProveedorContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorContactoAD.ListadoPorIdentificadorDelContacto(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorContactoAD.Error;
                return false;
            }
            
        }

        public bool ListadoPorIdentificadorDelContactoInformacion(ProveedorContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorContactoAD.ListadoPorIdentificadorDelContactoInformacion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorContactoAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificadorDelProveedor(ProveedorContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorContactoAD.ListadoPorIdentificadorDelProveedor(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorContactoAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(ProveedorContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorContactoAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorContactoAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(ProveedorContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorContactoAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorContactoAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(ProveedorContactoEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProveedorContactoAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProveedorContactoAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(ProveedorContactoEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProveedorContactoAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProveedorContactoAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oProveedorContactoAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oProveedorContactoAD.TraerDatos().Rows.Count;
        }



    }
}
