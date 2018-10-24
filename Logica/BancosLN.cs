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
    public class BancosLN
    {

        public string Error { set; get; }

        private BancosAD oBancosAD = new BancosAD();

        public bool Agregar(BancosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBancosAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oBancosAD.Error;
                return false;
            }

        }

        public bool Actualizar(BancosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idBancos.ToString()) || oREgistroEN.idBancos == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oBancosAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBancosAD.Error;
                return false;
            }

        }

        public bool Eliminar(BancosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idBancos.ToString()) || oREgistroEN.idBancos == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oBancosAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBancosAD.Error;
                return false;
            }

        }

        public bool Listado(BancosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBancosAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBancosAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(BancosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBancosAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBancosAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(BancosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBancosAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBancosAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(BancosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBancosAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBancosAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(BancosEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oBancosAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oBancosAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(BancosEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oBancosAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oBancosAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oBancosAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oBancosAD.TraerDatos().Rows.Count;
        }



    }
}
