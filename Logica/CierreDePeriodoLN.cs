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
    public class CierreDePeriodoLN
    {

        public string Error { set; get; }

        private CierreDePeriodoAD oCierreDePeriodoAD = new CierreDePeriodoAD();

        public bool Agregar(CierreDePeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCierreDePeriodoAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oCierreDePeriodoAD.Error;
                return false;
            }

        }

        public bool AgregarUtilizandoLaMismaConexion(CierreDePeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCierreDePeriodoAD.AgregarUtilizandoLaMismaConexion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCierreDePeriodoAD.Error;
                return false;
            }

        }

        public bool Actualizar(CierreDePeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idCierreDePeriodo.ToString()) || oREgistroEN.idCierreDePeriodo == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oCierreDePeriodoAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCierreDePeriodoAD.Error;
                return false;
            }

        }

        public bool Eliminar(CierreDePeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idCierreDePeriodo.ToString()) || oREgistroEN.idCierreDePeriodo == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oCierreDePeriodoAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCierreDePeriodoAD.Error;
                return false;
            }

        }

        public bool Listado(CierreDePeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCierreDePeriodoAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCierreDePeriodoAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(CierreDePeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCierreDePeriodoAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCierreDePeriodoAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(CierreDePeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCierreDePeriodoAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCierreDePeriodoAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(CierreDePeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCierreDePeriodoAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCierreDePeriodoAD.Error;
                return false;
            }

        }

        public bool ValidarRegistroDuplicado(CierreDePeriodoEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oCierreDePeriodoAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oCierreDePeriodoAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(CierreDePeriodoEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oCierreDePeriodoAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oCierreDePeriodoAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oCierreDePeriodoAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oCierreDePeriodoAD.TraerDatos().Rows.Count;
        }



    }
}
