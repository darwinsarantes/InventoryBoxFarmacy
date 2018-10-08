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
    public class CategoriaLN
    {

        public string Error { set; get; }

        private CategoriaAD oCategoriaAD = new CategoriaAD();

        public bool Agregar(CategoriaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCategoriaAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oCategoriaAD.Error;
                return false;
            }

        }

        public bool Actualizar(CategoriaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idCategoria.ToString()) || oREgistroEN.idCategoria == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oCategoriaAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCategoriaAD.Error;
                return false;
            }

        }

        public bool Eliminar(CategoriaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idCategoria.ToString()) || oREgistroEN.idCategoria == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oCategoriaAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCategoriaAD.Error;
                return false;
            }

        }

        public bool Listado(CategoriaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCategoriaAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCategoriaAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(CategoriaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCategoriaAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCategoriaAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(CategoriaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCategoriaAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCategoriaAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(CategoriaEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oCategoriaAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oCategoriaAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(CategoriaEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oCategoriaAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oCategoriaAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(CategoriaEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oCategoriaAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oCategoriaAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oCategoriaAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oCategoriaAD.TraerDatos().Rows.Count;
        }



    }
}
