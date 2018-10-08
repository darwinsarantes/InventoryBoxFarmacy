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
    public class InterfazLN
    {

        public string Error { set; get; }

        private InterfazAD oInterfazAD = new InterfazAD();

        public bool Agregar(InterfazEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oInterfazAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oInterfazAD.Error;
                return false;
            }

        }

        public bool Actualizar(InterfazEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idInterfaz.ToString()) || oREgistroEN.idInterfaz == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oInterfazAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oInterfazAD.Error;
                return false;
            }

        }

        public bool Eliminar(InterfazEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idInterfaz.ToString()) || oREgistroEN.idInterfaz == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oInterfazAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oInterfazAD.Error;
                return false;
            }

        }

        public bool Listado(InterfazEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oInterfazAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oInterfazAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(InterfazEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oInterfazAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oInterfazAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(InterfazEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oInterfazAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oInterfazAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(InterfazEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oInterfazAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oInterfazAD.Error;
                return false;
            }

        }

        public bool ValidarRegistroDuplicado(InterfazEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oInterfazAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oInterfazAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }
        
        public DataTable TraerDatos() {

            return oInterfazAD.TraerDatos();

        }
        

    }
}
