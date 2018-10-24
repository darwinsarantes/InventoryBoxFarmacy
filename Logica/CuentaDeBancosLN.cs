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
    public class CuentaDeBancosLN
    {

        public string Error { set; get; }

        private CuentaDeBancosAD oCuentaDeBancosAD = new CuentaDeBancosAD();

        public bool Agregar(CuentaDeBancosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCuentaDeBancosAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oCuentaDeBancosAD.Error;
                return false;
            }

        }

        public bool Actualizar(CuentaDeBancosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idCuentaDeBancos.ToString()) || oREgistroEN.idCuentaDeBancos == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oCuentaDeBancosAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCuentaDeBancosAD.Error;
                return false;
            }

        }

        public bool Eliminar(CuentaDeBancosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idCuentaDeBancos.ToString()) || oREgistroEN.idCuentaDeBancos == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oCuentaDeBancosAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCuentaDeBancosAD.Error;
                return false;
            }

        }

        public bool Listado(CuentaDeBancosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCuentaDeBancosAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCuentaDeBancosAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(CuentaDeBancosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCuentaDeBancosAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCuentaDeBancosAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(CuentaDeBancosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCuentaDeBancosAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCuentaDeBancosAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(CuentaDeBancosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCuentaDeBancosAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCuentaDeBancosAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(CuentaDeBancosEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oCuentaDeBancosAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oCuentaDeBancosAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(CuentaDeBancosEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oCuentaDeBancosAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oCuentaDeBancosAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oCuentaDeBancosAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oCuentaDeBancosAD.TraerDatos().Rows.Count;
        }



    }
}
