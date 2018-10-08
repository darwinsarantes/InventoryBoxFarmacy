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
    public class ModuloInterfazUsuariosLN
    {

        public string Error { set; get; }

        private ModuloInterfazUsuariosAD oModuloInterfazUsuariosAD = new ModuloInterfazUsuariosAD();

        public bool Agregar(ModuloInterfazUsuariosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazUsuariosAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oModuloInterfazUsuariosAD.Error;
                return false;
            }

        }

        public bool Actualizar(ModuloInterfazUsuariosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idModuloInterfazUsuario.ToString()) || oREgistroEN.idModuloInterfazUsuario == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oModuloInterfazUsuariosAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazUsuariosAD.Error;
                return false;
            }

        }

        public bool Eliminar(ModuloInterfazUsuariosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idModuloInterfazUsuario.ToString()) || oREgistroEN.idModuloInterfazUsuario == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oModuloInterfazUsuariosAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazUsuariosAD.Error;
                return false;
            }

        }

        public bool Listado(ModuloInterfazUsuariosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazUsuariosAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazUsuariosAD.Error;
                return false;
            }

        }

        public bool ListadoPrivilegiosDelUsuario(ModuloInterfazUsuariosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazUsuariosAD.ListadoPrivilegiosDelUsuario(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazUsuariosAD.Error;
                return false;
            }

        }

        public bool ListadoPrivilegiosDelUsuariosPorIntefaz(ModuloInterfazUsuariosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazUsuariosAD.ListadoPrivilegiosDelUsuariosPorIntefaz(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazUsuariosAD.Error;
                return false;
            }

        }

        public bool ListadoPrivilegiosDelUsuariosPorModulo(ModuloInterfazUsuariosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazUsuariosAD.ListadoPrivilegiosDelUsuariosPorModulo(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazUsuariosAD.Error;
                return false;
            }

        }

        public bool VerificarSiTengoAcceso(string Privilegio)
        {
            DataTable DT = oModuloInterfazUsuariosAD.TraerDatos();
            
            var res = from R in DT.AsEnumerable() where R.Field<string>("Privilegio").Trim().ToLower() == Privilegio.Trim().ToLower() select R.Field<Int32>("Acceso");

            if (res.Count() > 0)
            {
                int valor = res.First();
                if (valor == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        public bool VerificarSiTengoAccesoDeInterfaz(string Interfaz)
        {
            DataTable DT = oModuloInterfazUsuariosAD.TraerDatos();

            var res = from R in DT.AsEnumerable() where R.Field<string>("Interfaz").Trim().ToLower() == Interfaz.Trim().ToLower() select R.Field<Int32>("Acceso");

            if (res.Count() > 0)
            {
                int valor = res.First();
                if (valor == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        public bool ListadoPorIdentificador(ModuloInterfazUsuariosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazUsuariosAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazUsuariosAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(ModuloInterfazUsuariosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazUsuariosAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazUsuariosAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(ModuloInterfazUsuariosEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazUsuariosAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazUsuariosAD.Error;
                return false;
            }

        }

        public bool ValidarRegistroDuplicado(ModuloInterfazUsuariosEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oModuloInterfazUsuariosAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oModuloInterfazUsuariosAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }

        public DataTable PrivilegiosDelUsuarioEnDT() {

            DataTable DataDT = null;

            try
            {                

                DataTable DT = oModuloInterfazUsuariosAD.TraerDatos();

                if (DT.Rows.Count > 0)
                {
                    DataDT = new DataTable();
                    
                    DataDT.Columns.Add("Eliminar", typeof(Boolean));
                    DataDT.Columns.Add("Marcar", typeof(Boolean));
                    DataDT.Columns.Add("idModuloInterfazUsuario", typeof(Int32));
                    DataDT.Columns.Add("idPrivilegio", typeof(Int32));                    
                    DataDT.Columns.Add("Privilegio", typeof(string));
                    DataDT.Columns.Add("NombreAMostrar", typeof(string));
                    DataDT.Columns.Add("Modulo", typeof(string));
                    DataDT.Columns.Add("Interfaz", typeof(string));                   
                    DataDT.Columns.Add("Acceso", typeof(Int32));
                    DataDT.Columns.Add("Actualizar", typeof(Boolean));
                    
                    foreach (DataRow row in DT.Rows)
                    {
                        Boolean Acceso = false;

                        if (Convert.ToInt32(row["Acceso"]) == 1)
                            Acceso = true;

                        DataDT.Rows.Add(false,
                                       Acceso,
                                       row["idModuloInterfazUsuario"],
                                       row["idPrivilegio"],
                                       row["Privilegio"],
                                       row["NombreAMostrar"],
                                       row["Modulo"],
                                       row["Interfaz"],                                       
                                       Convert.ToInt32(row["Acceso"]),                                     
                                       true);

                    }
                    

                }
                else
                {
                    return DataDT;
                }
                
            }
            catch (Exception ex) {
                this.Error = ex.Message;
            }

            return DataDT;
        }

        public DataTable TraerDatos() {

            return oModuloInterfazUsuariosAD.TraerDatos();

        }

        public int TotalRegistros()
        {
            return oModuloInterfazUsuariosAD.TraerDatos().Rows.Count;
        }
        

    }
}
