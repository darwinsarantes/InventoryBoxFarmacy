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
    public class LaboratorioLN
    {

        public string Error { set; get; }

        private LaboratorioAD oLaboratorioAD = new LaboratorioAD();

        public bool Agregar(LaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oLaboratorioAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oLaboratorioAD.Error;
                return false;
            }

        }

        public bool AgregarUtilizandoLaMismaConexion(LaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oLaboratorioAD.AgregarUtilizandoLaMismaConexion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLaboratorioAD.Error;
                return false;
            }

        }

        public bool Actualizar(LaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idLaboratorio.ToString()) || oREgistroEN.idLaboratorio == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oLaboratorioAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLaboratorioAD.Error;
                return false;
            }

        }

        public bool Eliminar(LaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idLaboratorio.ToString()) || oREgistroEN.idLaboratorio == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oLaboratorioAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLaboratorioAD.Error;
                return false;
            }

        }

        public bool Listado(LaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oLaboratorioAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLaboratorioAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(LaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oLaboratorioAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLaboratorioAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(LaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oLaboratorioAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLaboratorioAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(LaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oLaboratorioAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLaboratorioAD.Error;
                return false;
            }

        }

        public bool GenerarCodigoDelLaboratorio(LaboratorioEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oLaboratorioAD.GenerarCodigoDelLaboratorio(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLaboratorioAD.Error;
                return false;
            }

        }

        public bool ValidarRegistroDuplicado(LaboratorioEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oLaboratorioAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oLaboratorioAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarRegistroDuplicadoNoRUC(LaboratorioEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oLaboratorioAD.ValidarRegistroDuplicadoNoRUC(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oLaboratorioAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(LaboratorioEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oLaboratorioAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oLaboratorioAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oLaboratorioAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oLaboratorioAD.TraerDatos().Rows.Count;
        }



    }
}
