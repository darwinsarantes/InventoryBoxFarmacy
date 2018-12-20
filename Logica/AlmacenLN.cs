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
    public class AlmacenLN
    {

        public string Error { set; get; }

        private AlmacenAD oAlmacenAD = new AlmacenAD();

        public bool Agregar(AlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oAlmacenAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oAlmacenAD.Error;
                return false;
            }

        }
               

        public bool Actualizar(AlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idAlmacen.ToString()) || oREgistroEN.idAlmacen == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oAlmacenAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oAlmacenAD.Error;
                return false;
            }

        }

        public bool ActualizarVodegaPorDefecto(AlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idAlmacen.ToString()) || oREgistroEN.idAlmacen == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oAlmacenAD.ActualizarVodegaPorDefecto(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oAlmacenAD.Error;
                return false;
            }

        }

        public bool Eliminar(AlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idAlmacen.ToString()) || oREgistroEN.idAlmacen == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oAlmacenAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oAlmacenAD.Error;
                return false;
            }

        }
        
        public bool Listado(AlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oAlmacenAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oAlmacenAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(AlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oAlmacenAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oAlmacenAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(AlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oAlmacenAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oAlmacenAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(AlmacenEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oAlmacenAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oAlmacenAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(AlmacenEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oAlmacenAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oAlmacenAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarCodigo(AlmacenEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oAlmacenAD.ValidarCodigo(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oAlmacenAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(AlmacenEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oAlmacenAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oAlmacenAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool VerificarSiLaEntidadEstaAsociadaAProducto(AlmacenEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oAlmacenAD.VerificarSiLaEntidadEstaAsociadaAProducto(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oAlmacenAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oAlmacenAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oAlmacenAD.TraerDatos().Rows.Count;
        }
        
    }
}
