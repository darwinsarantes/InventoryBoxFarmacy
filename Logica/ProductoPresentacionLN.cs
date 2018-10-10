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
    public class ProductoPresentacionLN
    {

        public string Error { set; get; }

        private ProductoPresentacionAD oProductoPresentacionAD = new ProductoPresentacionAD();

        public bool Agregar(ProductoPresentacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoPresentacionAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oProductoPresentacionAD.Error;
                return false;
            }

        }

        public bool Actualizar(ProductoPresentacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoPresentacion.ToString()) || oREgistroEN.idProductoPresentacion == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoPresentacionAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPresentacionAD.Error;
                return false;
            }

        }

        public bool Eliminar(ProductoPresentacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idProductoPresentacion.ToString()) || oREgistroEN.idProductoPresentacion == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oProductoPresentacionAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPresentacionAD.Error;
                return false;
            }

        }

        public bool Listado(ProductoPresentacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoPresentacionAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPresentacionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ProductoPresentacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoPresentacionAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPresentacionAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(ProductoPresentacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoPresentacionAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPresentacionAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(ProductoPresentacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oProductoPresentacionAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oProductoPresentacionAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(ProductoPresentacionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoPresentacionAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoPresentacionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(ProductoPresentacionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oProductoPresentacionAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oProductoPresentacionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oProductoPresentacionAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oProductoPresentacionAD.TraerDatos().Rows.Count;
        }



    }
}
