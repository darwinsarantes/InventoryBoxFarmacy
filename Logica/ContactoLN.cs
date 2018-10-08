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
    public class ContactoLN
    {

        public string Error { set; get; }

        private ContactoAD oContactoAD = new ContactoAD();

        public bool Agregar(ContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oContactoAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oContactoAD.Error;
                return false;
            }

        }

        public bool AgregarUtilizandoLaMismaConexion(ContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oContactoAD.AgregarUtilizandoLaMismaConexion(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContactoAD.Error;
                return false;
            }

        }

        public bool Actualizar(ContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idContacto.ToString()) || oREgistroEN.idContacto == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oContactoAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContactoAD.Error;
                return false;
            }

        }

        public bool Eliminar(ContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idContacto.ToString()) || oREgistroEN.idContacto == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oContactoAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContactoAD.Error;
                return false;
            }

        }

        public bool Listado(ContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oContactoAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContactoAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oContactoAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContactoAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(ContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oContactoAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContactoAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(ContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oContactoAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContactoAD.Error;
                return false;
            }

        }

        public bool GenerarCodigoDelContacto(ContactoEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oContactoAD.GenerarCodigoDelContacto(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oContactoAD.Error;
                return false;
            }

        }

        public bool ValidarRegistroDuplicado(ContactoEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oContactoAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oContactoAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarRegistroDuplicadoParaCedula(ContactoEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oContactoAD.ValidarRegistroDuplicadoParaCedula(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oContactoAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(ContactoEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oContactoAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oContactoAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oContactoAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oContactoAD.TraerDatos().Rows.Count;
        }



    }
}
