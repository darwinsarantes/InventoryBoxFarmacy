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
    public class SeccionLN
    {

        public string Error { set; get; }

        private SeccionAD oSeccionAD = new SeccionAD();

        public bool Agregar(SeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oSeccionAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oSeccionAD.Error;
                return false;
            }

        }
        
        public bool Actualizar(SeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idSeccion.ToString()) || oREgistroEN.idSeccion == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oSeccionAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionAD.Error;
                return false;
            }

        }

        public bool Eliminar(SeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idSeccion.ToString()) || oREgistroEN.idSeccion == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oSeccionAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionAD.Error;
                return false;
            }

        }
        
        public bool Listado(SeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oSeccionAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdLocacion(SeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oSeccionAD.ListadoPorIdLocacion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionAD.Error;
                return false;
            }

        }

        public bool ListadoDeContenedoresPorSeccion(SeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oSeccionAD.ListadoDeContenedoresPorSeccion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(SeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oSeccionAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(SeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oSeccionAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(SeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oSeccionAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(SeccionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oSeccionAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oSeccionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool VerificarSiLaEntidadEstaAsociadaAProducto(SeccionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oSeccionAD.VerificarSiLaEntidadEstaAsociadaAProducto(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oSeccionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarCodigo(SeccionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oSeccionAD.ValidarCodigo(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oSeccionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(SeccionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oSeccionAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oSeccionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculadoParaActualizacion(SeccionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oSeccionAD.ValidarSiElRegistroEstaVinculadoParaActualizacion(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oSeccionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oSeccionAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oSeccionAD.TraerDatos().Rows.Count;
        }



    }
}
