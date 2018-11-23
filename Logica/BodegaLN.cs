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
    public class BodegaLN
    {

        public string Error { set; get; }

        private BodegaAD oBodegaAD = new BodegaAD();

        public bool Agregar(BodegaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oBodegaAD.Error;
                return false;
            }

        }

        public bool AgregarUtilizandoLaMismaConexion(BodegaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaAD.AgregarUtilizandoLaMismaConexion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAD.Error;
                return false;
            }

        }

        public bool Actualizar(BodegaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idBodega.ToString()) || oREgistroEN.idBodega == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oBodegaAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAD.Error;
                return false;
            }

        }

        public bool Eliminar(BodegaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idBodega.ToString()) || oREgistroEN.idBodega == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oBodegaAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAD.Error;
                return false;
            }

        }

        public bool EliminarUtilizandoLaMismaConexion(BodegaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idBodega.ToString()) || oREgistroEN.idBodega == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oBodegaAD.EliminarUtilizandoLaMismaConexion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAD.Error;
                return false;
            }

        }


        public bool Listado(BodegaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(BodegaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(BodegaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(BodegaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oBodegaAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oBodegaAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(BodegaEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oBodegaAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oBodegaAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarCodigo(BodegaEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oBodegaAD.ValidarCodigo(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oBodegaAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(BodegaEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oBodegaAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oBodegaAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oBodegaAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oBodegaAD.TraerDatos().Rows.Count;
        }
        
    }
}
