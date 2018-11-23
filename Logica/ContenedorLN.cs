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
    public class ContenedorLN
    {

        public string Error { set; get; }

        private ContenedorAD oContenedorAD = new ContenedorAD();

        public bool Agregar(ContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oContenedorAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oContenedorAD.Error;
                return false;
            }

        }

        public bool AgregarUtilizandoLaMismaConexion(ContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oContenedorAD.AgregarUtilizandoLaMismaConexion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContenedorAD.Error;
                return false;
            }

        }

        public bool Actualizar(ContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idContenedor.ToString()) || oREgistroEN.idContenedor == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oContenedorAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContenedorAD.Error;
                return false;
            }

        }

        public bool Eliminar(ContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idContenedor.ToString()) || oREgistroEN.idContenedor == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oContenedorAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContenedorAD.Error;
                return false;
            }

        }

        public bool EliminarUtilizandoLaMismaConexion(ContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idContenedor.ToString()) || oREgistroEN.idContenedor == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oContenedorAD.EliminarUtilizandoLaMismaConexion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContenedorAD.Error;
                return false;
            }

        }

        public bool Listado(ContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oContenedorAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContenedorAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oContenedorAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContenedorAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(ContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oContenedorAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContenedorAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(ContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oContenedorAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContenedorAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(ContenedorEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oContenedorAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oContenedorAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarCodigo(ContenedorEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oContenedorAD.ValidarCodigo(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oContenedorAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(ContenedorEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oContenedorAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oContenedorAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oContenedorAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oContenedorAD.TraerDatos().Rows.Count;
        }



    }
}
