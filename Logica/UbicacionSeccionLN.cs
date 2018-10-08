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
    public class UbicacionSeccionLN
    {

        public string Error { set; get; }

        private UbicacionSeccionAD oUbicacionSeccionAD = new UbicacionSeccionAD();

        public bool Agregar(UbicacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUbicacionSeccionAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oUbicacionSeccionAD.Error;
                return false;
            }

        }

        public bool Actualizar(UbicacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idUbicacionSeccion.ToString()) || oREgistroEN.idUbicacionSeccion == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oUbicacionSeccionAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUbicacionSeccionAD.Error;
                return false;
            }

        }

        public bool Eliminar(UbicacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idUbicacionSeccion.ToString()) || oREgistroEN.idUbicacionSeccion == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oUbicacionSeccionAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUbicacionSeccionAD.Error;
                return false;
            }

        }

        public bool Listado(UbicacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUbicacionSeccionAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUbicacionSeccionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(UbicacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUbicacionSeccionAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUbicacionSeccionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificadorDelaUbicacion(UbicacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUbicacionSeccionAD.ListadoPorIdentificadorDelaUbicacion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUbicacionSeccionAD.Error;
                return false;
            }

        }

        public bool ListadoPorID_UbicacionInformacion(UbicacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUbicacionSeccionAD.ListadoPorID_UbicacionInformacion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUbicacionSeccionAD.Error;
                return false;
            }

        }
        
        public bool ListadoParaCombos(UbicacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUbicacionSeccionAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUbicacionSeccionAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(UbicacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oUbicacionSeccionAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oUbicacionSeccionAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(UbicacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oUbicacionSeccionAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oUbicacionSeccionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(UbicacionSeccionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oUbicacionSeccionAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oUbicacionSeccionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oUbicacionSeccionAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oUbicacionSeccionAD.TraerDatos().Rows.Count;
        }



    }
}
