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
    public class ProveedorLaboratorioLN
    {

        public string Error { set; get; }

        private ProveedorLaboratorioAD oProveedorLaboratorioAD = new ProveedorLaboratorioAD();

        public bool Agregar(ProveedorLaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorLaboratorioAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oProveedorLaboratorioAD.Error;
                return false;
            }

        }

        public bool Actualizar(ProveedorLaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProveedorLaboratorio.ToString()) || oREgistroEN.idProveedorLaboratorio == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProveedorLaboratorioAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorLaboratorioAD.Error;
                return false;
            }

        }

        public bool Eliminar(ProveedorLaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProveedorLaboratorio.ToString()) || oREgistroEN.idProveedorLaboratorio == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProveedorLaboratorioAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorLaboratorioAD.Error;
                return false;
            }

        }

        public bool Listado(ProveedorLaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorLaboratorioAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorLaboratorioAD.Error;
                return false;
            }

        }

        public bool ListadoDeProveedoresLaboratorio(ProveedorLaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorLaboratorioAD.ListadoDeProveedoresLaboratorio(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorLaboratorioAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ProveedorLaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorLaboratorioAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorLaboratorioAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificadorDelLaboratorio(ProveedorLaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorLaboratorioAD.ListadoPorIdentificadorDelLaboratorio(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorLaboratorioAD.Error;
                return false;
            }

        }

        public bool ListadoPorID_LabortoriosInformacion(ProveedorLaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorLaboratorioAD.ListadoPorID_LabortoriosInformacion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorLaboratorioAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificadorDelProveedor(ProveedorLaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorLaboratorioAD.ListadoPorIdentificadorDelProveedor(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorLaboratorioAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(ProveedorLaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorLaboratorioAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorLaboratorioAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(ProveedorLaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProveedorLaboratorioAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProveedorLaboratorioAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(ProveedorLaboratorioEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProveedorLaboratorioAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProveedorLaboratorioAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(ProveedorLaboratorioEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProveedorLaboratorioAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProveedorLaboratorioAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oProveedorLaboratorioAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oProveedorLaboratorioAD.TraerDatos().Rows.Count;
        }



    }
}
