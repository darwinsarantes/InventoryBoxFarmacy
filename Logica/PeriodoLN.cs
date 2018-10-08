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
    public class PeriodoLN
    {

        public string Error { set; get; }

        private PeriodoAD oPeriodoAD = new PeriodoAD();

        public bool Agregar(PeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oPeriodoAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oPeriodoAD.Error;
                return false;
            }

        }

        public bool Actualizar(PeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idPeriodo.ToString()) || oREgistroEN.idPeriodo == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oPeriodoAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oPeriodoAD.Error;
                return false;
            }

        }

        public bool Eliminar(PeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idPeriodo.ToString()) || oREgistroEN.idPeriodo == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oPeriodoAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oPeriodoAD.Error;
                return false;
            }

        }

        public bool Listado(PeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oPeriodoAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oPeriodoAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(PeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oPeriodoAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oPeriodoAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(PeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oPeriodoAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oPeriodoAD.Error;
                return false;
            }

        }

        public bool ListadoDeLosAñosEnPeriodosCerrasdos(PeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oPeriodoAD.ListadoDeLosAñosEnPeriodosCerrasdos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oPeriodoAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(PeriodoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oPeriodoAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oPeriodoAD.Error;
                return false;
            }

        }

        public bool ValidarRegistroDuplicado(PeriodoEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oPeriodoAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oPeriodoAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool RegistroDuplicadoPorFecha(PeriodoEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oPeriodoAD.RegistroDuplicadoPorFecha(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oPeriodoAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool RegistroDuplicadoPorFecha_contenidos(PeriodoEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oPeriodoAD.RegistroDuplicadoPorFecha_contenidos(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oPeriodoAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(PeriodoEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oPeriodoAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oPeriodoAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool EvaluarSiHayRegistrosEnTransacciones(DatosDeConexionEN oDatosConexionEN)
        {
            if (oPeriodoAD.EvaluarSiHayRegistrosEnTransacciones(oDatosConexionEN))
            {
                this.Error = null;
                return true;
            }
            else
            {
                this.Error = oPeriodoAD.Error;
                return false;
            }
        }

        public bool ListarFechaInicialDelPeriodo(DatosDeConexionEN oDatosConexionEN)
        {
            if (oPeriodoAD.ListarFechaInicialDelPeriodo(oDatosConexionEN))
            {
                this.Error = null;
                return true;
            }
            else
            {
                this.Error = oPeriodoAD.Error;
                return false;
            }
        }

        public bool EvaluarCuantosMovimientosCeranAfectadosPorElCierreDePeriodo(PeriodoEN oRegistroEN,DatosDeConexionEN oDatosConexionEN)
        {
            if (oPeriodoAD.EvaluarCuantosMovimientosCeranAfectadosPorElCierreDePeriodo(oRegistroEN, oDatosConexionEN))
            {
                this.Error = null;
                return true;
            }
            else
            {
                this.Error = oPeriodoAD.Error;
                return false;
            }
        }


        public DataTable TraerDatos() {

            return oPeriodoAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oPeriodoAD.TraerDatos().Rows.Count;
        }



    }
}
