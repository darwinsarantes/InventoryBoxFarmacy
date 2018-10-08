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
    public class AlmacenUbicacionLN
    {

        public string Error { set; get; }

        private AlmacenUbicacionAD oAlmacenUbicacionAD = new AlmacenUbicacionAD();

        public bool Agregar(AlmacenUbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oAlmacenUbicacionAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oAlmacenUbicacionAD.Error;
                return false;
            }

        }

        public bool Actualizar(AlmacenUbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idAlmacenUbicacion.ToString()) || oREgistroEN.idAlmacenUbicacion == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oAlmacenUbicacionAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oAlmacenUbicacionAD.Error;
                return false;
            }

        }

        public bool Eliminar(AlmacenUbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idAlmacenUbicacion.ToString()) || oREgistroEN.idAlmacenUbicacion == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oAlmacenUbicacionAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oAlmacenUbicacionAD.Error;
                return false;
            }

        }

        public bool Listado(AlmacenUbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oAlmacenUbicacionAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oAlmacenUbicacionAD.Error;
                return false;
            }

        }

        public bool ListadoDeUbicacionesPorAlmacen(AlmacenUbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oAlmacenUbicacionAD.ListadoDeUbicacionesPorAlmacen(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oAlmacenUbicacionAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(AlmacenUbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oAlmacenUbicacionAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oAlmacenUbicacionAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(AlmacenUbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oAlmacenUbicacionAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oAlmacenUbicacionAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(AlmacenUbicacionEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oAlmacenUbicacionAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oAlmacenUbicacionAD.Error;
                return false;
            }

        }
        
        public bool ValidarRegistroDuplicado(AlmacenUbicacionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oAlmacenUbicacionAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oAlmacenUbicacionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public bool ValidarSiElRegistroEstaVinculado(AlmacenUbicacionEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oAlmacenUbicacionAD.ValidarSiElRegistroEstaVinculado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oAlmacenUbicacionAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable TraerDatos() {

            return oAlmacenUbicacionAD.TraerDatos();

        }

        public int TotalRegistros() {
            return oAlmacenUbicacionAD.TraerDatos().Rows.Count;
        }



    }
}
