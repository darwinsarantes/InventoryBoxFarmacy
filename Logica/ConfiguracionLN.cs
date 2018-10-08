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
    public class ConfiguracionLN
    {

        public string Error { set; get; }

        private ConfiguracionAD oConfiguracionAD = new ConfiguracionAD();

        public bool Agregar(ConfiguracionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oConfiguracionAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oConfiguracionAD.Error;
                return false;
            }

        }

        public bool Actualizar(ConfiguracionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.IdConfiguracion.ToString()) || oREgistroEN.IdConfiguracion == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oConfiguracionAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oConfiguracionAD.Error;
                return false;
            }

        }

        public bool Eliminar(ConfiguracionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.IdConfiguracion.ToString()) || oREgistroEN.IdConfiguracion == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oConfiguracionAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oConfiguracionAD.Error;
                return false;
            }

        }

        public bool Listado(ConfiguracionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oConfiguracionAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oConfiguracionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ConfiguracionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oConfiguracionAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oConfiguracionAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(ConfiguracionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oConfiguracionAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oConfiguracionAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(ConfiguracionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oConfiguracionAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oConfiguracionAD.Error;
                return false;
            }

        }
               
        public DataTable TraerDatos() {

            return oConfiguracionAD.TraerDatos();

        }
        

    }
}
