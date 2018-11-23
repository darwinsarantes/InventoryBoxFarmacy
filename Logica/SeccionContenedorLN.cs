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
    public class SeccionContenedorLN
    {

        public string Error { set; get; }

        private SeccionContenedorAD oSeccionContenedorAD = new SeccionContenedorAD();

        public bool Agregar(SeccionContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oSeccionContenedorAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oSeccionContenedorAD.Error;
                return false;
            }

        }

        public bool Actualizar(SeccionContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idSeccionContenedor.ToString()) || oREgistroEN.idSeccionContenedor == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oSeccionContenedorAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionContenedorAD.Error;
                return false;
            }

        }

        public bool Eliminar(SeccionContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idSeccionContenedor.ToString()) || oREgistroEN.idSeccionContenedor == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oSeccionContenedorAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionContenedorAD.Error;
                return false;
            }

        }

        public bool Listado(SeccionContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oSeccionContenedorAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionContenedorAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(SeccionContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oSeccionContenedorAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionContenedorAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdContendor(SeccionContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oSeccionContenedorAD.ListadoPorIdContendor(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionContenedorAD.Error;
                return false;
            }
            
        }

        public bool ListadoPorIdDeSeccion(SeccionContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oSeccionContenedorAD.ListadoPorIdDeSeccion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionContenedorAD.Error;
                return false;
            }

        }
        
        public bool ListadoParaCombos(SeccionContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oSeccionContenedorAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionContenedorAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(SeccionContenedorEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oSeccionContenedorAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oSeccionContenedorAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(SeccionContenedorEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oSeccionContenedorAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oSeccionContenedorAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(SeccionContenedorEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oSeccionContenedorAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oSeccionContenedorAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oSeccionContenedorAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oSeccionContenedorAD.TraerDatos().Rows.Count;
        }



    }
}
