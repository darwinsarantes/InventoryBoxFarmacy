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
    public class LocacionSeccionLN
    {

        public string Error { set; get; }

        private LocacionSeccionAD oLocacionSeccionAD = new LocacionSeccionAD();

        public bool Agregar(LocacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oLocacionSeccionAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oLocacionSeccionAD.Error;
                return false;
            }

        }

        public bool Actualizar(LocacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idLocacionSeccion.ToString()) || oREgistroEN.idLocacionSeccion == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oLocacionSeccionAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLocacionSeccionAD.Error;
                return false;
            }

        }

        public bool Eliminar(LocacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idLocacionSeccion.ToString()) || oREgistroEN.idLocacionSeccion == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oLocacionSeccionAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLocacionSeccionAD.Error;
                return false;
            }

        }

        public bool Listado(LocacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oLocacionSeccionAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLocacionSeccionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(LocacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oLocacionSeccionAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLocacionSeccionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdDeLocacion(LocacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oLocacionSeccionAD.ListadoPorIdDeLocacion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLocacionSeccionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdDeSeccion(LocacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oLocacionSeccionAD.ListadoPorIdDeSeccion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLocacionSeccionAD.Error;
                return false;
            }

        }
        
        public bool ListadoParaCombos(LocacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oLocacionSeccionAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLocacionSeccionAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(LocacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oLocacionSeccionAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oLocacionSeccionAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(LocacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oLocacionSeccionAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oLocacionSeccionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(LocacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oLocacionSeccionAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oLocacionSeccionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oLocacionSeccionAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oLocacionSeccionAD.TraerDatos().Rows.Count;
        }        

    }
}
