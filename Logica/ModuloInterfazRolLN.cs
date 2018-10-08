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
    public class ModuloInterfazRolLN
    {

        public string Error { set; get; }

        private ModuloInterfazRolAD oModuloInterfazRolAD = new ModuloInterfazRolAD();

        public bool Agregar(ModuloInterfazRolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazRolAD.Agregar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else {
                Error = oModuloInterfazRolAD.Error;
                return false;
            }

        }

        public bool Actualizar(ModuloInterfazRolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idModuloInterfazRol.ToString()) || oREgistroEN.idModuloInterfazRol == 0) {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oModuloInterfazRolAD.Actualizar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazRolAD.Error;
                return false;
            }

        }

        public bool Eliminar(ModuloInterfazRolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (string.IsNullOrEmpty(oREgistroEN.idModuloInterfazRol.ToString()) || oREgistroEN.idModuloInterfazRol == 0)
            {

                this.Error = @"Se debe de seleccionar un elemento de la lista";
                return false;
            }

            if (oModuloInterfazRolAD.Eliminar(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazRolAD.Error;
                return false;
            }

        }

        public bool Listado(ModuloInterfazRolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazRolAD.Listado(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazRolAD.Error;
                return false;
            }

        }

        public bool ListadoDePrivilegiosParaELUsuario(ModuloInterfazRolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazRolAD.ListadoDePrivilegiosParaELUsuario(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazRolAD.Error;
                return false;
            }

        }

        public bool ListadoDePrivilegioDelRol(ModuloInterfazRolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazRolAD.ListadoDePrivilegioDelRol(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazRolAD.Error;
                return false;
            }

        }

        public bool ListadoPorIdentificador(ModuloInterfazRolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazRolAD.ListadoPorIdentificador(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazRolAD.Error;
                return false;
            }

        }

        public bool ListadoParaCombos(ModuloInterfazRolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazRolAD.ListadoParaCombos(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazRolAD.Error;
                return false;
            }

        }

        public bool ListadoParaReportes(ModuloInterfazRolEN oREgistroEN, DatosDeConexionEN oDatos)
        {

            if (oModuloInterfazRolAD.ListadoParaReportes(oREgistroEN, oDatos))
            {
                Error = string.Empty;
                return true;
            }
            else
            {
                Error = oModuloInterfazRolAD.Error;
                return false;
            }

        }

        public bool ValidarRegistroDuplicado(ModuloInterfazRolEN oREgistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            if (oModuloInterfazRolAD.ValidarRegistroDuplicado(oREgistroEN, oDatos, TipoDeOperacion))
            {
                Error = oModuloInterfazRolAD.Error;
                return true;
            }
            else
            {
                Error = string.Empty;
                return false;
            }

        }
        
        public DataTable TraerDatos() {

            return oModuloInterfazRolAD.TraerDatos();

        }

        public int TotalRegistros()
        {

            return oModuloInterfazRolAD.TraerDatos().Rows.Count;

        }

        public DataTable PrivilegiosDelRolParaElUsuarioDT()
        {

            DataTable DataDT = null;

            try
            {

                DataTable DT = oModuloInterfazRolAD.TraerDatos();

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
            catch (Exception ex)
            {
                this.Error = ex.Message;
            }

            return DataDT;
        }

        public DataTable PrivilegiosDelRolDT()
        {

            DataTable DataDT = null;

            try
            {

                DataTable DT = oModuloInterfazRolAD.TraerDatos();

                if (DT.Rows.Count > 0)
                {
                    DataDT = new DataTable();

                    DataDT.Columns.Add("Eliminar", typeof(Boolean));
                    DataDT.Columns.Add("Marcar", typeof(Boolean));
                    DataDT.Columns.Add("idModuloInterfazRol", typeof(Int32));
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
                                       row["idModuloInterfazRol"],
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
            catch (Exception ex)
            {
                this.Error = ex.Message;
            }

            return DataDT;
        }



    }
}
