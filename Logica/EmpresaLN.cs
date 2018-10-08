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
    public class EmpresaLN
    {

        public string Error { set; get; }

        private EmpresaAD oEmpresaAD = new EmpresaAD();

        public bool Agregar(EmpresaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oEmpresaAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oEmpresaAD.Error;
                return false;
            }

        }

        public bool Actualizar(EmpresaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.IdEmpresa.ToString()) || oREgistroEN.IdEmpresa == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oEmpresaAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oEmpresaAD.Error;
                return false;
            }

        }

        public bool Eliminar(EmpresaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.IdEmpresa.ToString()) || oREgistroEN.IdEmpresa == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oEmpresaAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oEmpresaAD.Error;
                return false;
            }

        }

        public bool Listado(EmpresaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oEmpresaAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oEmpresaAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(EmpresaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oEmpresaAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oEmpresaAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(EmpresaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oEmpresaAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oEmpresaAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(EmpresaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oEmpresaAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oEmpresaAD.Error;
                return false;
            }

        }

        public bool ValidarRegistroDuplicado(EmpresaEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oEmpresaAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oEmpresaAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }
        
        public DataTable TraerDatos() {

            return oEmpresaAD.TraerDatos();

        }
        

    }
}
