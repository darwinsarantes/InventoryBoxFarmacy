using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Entidad;
namespace AccesoDatos
{
    public class ModuloInterfazRolAD
    {

        public string Error { set; get; }
        private MySqlConnection Cnn = null;
        private MySqlCommand Comando = null;
        private MySqlDataAdapter Adaptador = null;
        private TransaccionesAD oTransaccionesAD = null;
        string Consultas;
        string DescripcionDeOperacion;
        private DataTable DT { set; get; }

        public ModuloInterfazRolAD(){

            oTransaccionesAD = new TransaccionesAD();
            oTransaccionesAD.modulo = "General";
            oTransaccionesAD.modelo = "ModuloInterfazRolAD";
            oTransaccionesAD.NombreDelEquipo = Environment.MachineName;
            oTransaccionesAD.Fecha_creacion = System.DateTime.Now;
            oTransaccionesAD.tabla = "ModuloInterfazRol";

        }

        #region "Funciones para datos dll"

        public bool Agregar(ModuloInterfazRolEN oRegistroEN, DatosDeConexionEN oDatos) {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"INSERT INTO modulointerfazrol
                (idPrivilegio, idRol, Acceso, IdUsuarioDeCreacion, FechaDeCreacion, IdUsuarioDeModificacion, FechaDeModificacion)
                values
                (@idPrivilegio, @idRol, @Acceso, @IdUsuarioDeCreacion, current_timestamp(), @IdUsuarioDeModificacion, current_timestamp());

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idPrivilegio", MySqlDbType.Int32)).Value = oRegistroEN.oPrivilegioEN.idPrivilegio;
                Comando.Parameters.Add(new MySqlParameter("@idRol", MySqlDbType.Int32)).Value = oRegistroEN.oRolEN.idRol;
                Comando.Parameters.Add(new MySqlParameter("@Acceso", MySqlDbType.Int32)).Value = oRegistroEN.Acceso;
                Comando.Parameters.Add(new MySqlParameter("@IdUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@IdUsuarioDeModificacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idModuloInterfazRol = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());

                DescripcionDeOperacion = string.Format("El registro fue Insertado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                //oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP,oRegistroEN.idModuloInterfazRol, "AGREGAR", "INFORMACIÓN DE LA MODULO INTERFAZ ROL AGREGADA CORRECTAMENTE", "CORRECTA", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);
                
                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al insertar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                //oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idModuloInterfazRol, "AGREGAR", "ERROR AL AGREGAR LA INFORMACIÓN DE LA ModuloInterfazRol", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);
                
                return false;
            }
            finally {

                if (Cnn != null) {

                    if (Cnn.State == ConnectionState.Open) {

                        Cnn.Close();

                    }

                }

                Cnn = null;
                Comando = null;
                Adaptador = null;
                oTransaccionesAD = null;

            }

        }

        public bool Actualizar(ModuloInterfazRolEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"UPDATE modulointerfazrol set
	                idPrivilegio = @idPrivilegio, idRol = @idRol, Acceso = @Acceso, IdUsuarioDeModificacion = @IdUsuarioDeModificacion, FechaDeModificacion = current_timestamp()
                Where idModuloInterfazRol = @idModuloInterfazRol;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idModuloInterfazRol", MySqlDbType.Int32)).Value = oRegistroEN.idModuloInterfazRol;
                Comando.Parameters.Add(new MySqlParameter("@idPrivilegio", MySqlDbType.Int32)).Value = oRegistroEN.oPrivilegioEN.idPrivilegio;
                Comando.Parameters.Add(new MySqlParameter("@idRol", MySqlDbType.Int32)).Value = oRegistroEN.oRolEN.idRol;
                Comando.Parameters.Add(new MySqlParameter("@Acceso", MySqlDbType.Int32)).Value = oRegistroEN.Acceso;                
                Comando.Parameters.Add(new MySqlParameter("@IdUsuarioDeModificacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

                Comando.ExecuteNonQuery();
                
                DescripcionDeOperacion = string.Format("El registro fue Actualizado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                //oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idModuloInterfazRol, "ACTUALIZAR", "INFORMACIÓN DE LA MODULO INTERFAZ ROL ACTUALIZADA", "CORRECTA", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al actualizar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                //oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idModuloInterfazRol, "ACTUALIZAR", "ERROR AL ACTUALIZAR LA INFORMACIÓN DE LA MODULO INTERFAZ ROL", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

                return false;
            }
            finally
            {

                if (Cnn != null)
                {

                    if (Cnn.State == ConnectionState.Open)
                    {

                        Cnn.Close();

                    }

                }

                Cnn = null;
                Comando = null;
                Adaptador = null;
                oTransaccionesAD = null;

            }

        }

        public bool Eliminar(ModuloInterfazRolEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from modulointerfazrol where idModuloInterfazRol = @idModuloInterfazRol;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idModuloInterfazRol", MySqlDbType.Int32)).Value = oRegistroEN.idModuloInterfazRol;
                
                Comando.ExecuteNonQuery();

                DescripcionDeOperacion = string.Format("El registro fue Eliminado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                //oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idModuloInterfazRol, "ELIMINAR", "INFORMACIÓN DE LA MODULO INTERFAZ ROL ELIMINADA", "CORRECTA", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al eliminar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                //oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idModuloInterfazRol, "ELIMINAR", "ERROR AL ELIMINAR LA INFORMACIÓN DE LA MODULO INTERFAZ ROL", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

                return false;
            }
            finally
            {

                if (Cnn != null)
                {

                    if (Cnn.State == ConnectionState.Open)
                    {

                        Cnn.Close();

                    }

                }

                Cnn = null;
                Comando = null;
                Adaptador = null;
                oTransaccionesAD = null;

            }

        }

        public bool Listado(ModuloInterfazRolEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT mir.idModuloInterfazRol, mir.idRol,mir.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                i.Nombre as 'Interfaz', i.NombreAMostrar, m.Nombre as 'Modulo' , mir.Acceso
                FROM modulointerfazrol as mir
                inner join privilegio as p on p.idPrivilegio = mir.idPrivilegio
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where mir.idModuloInterfazRol > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
                Comando.CommandText = Consultas;
                
                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
                
                return false;
            }
            finally
            {

                if (Cnn != null)
                {

                    if (Cnn.State == ConnectionState.Open)
                    {

                        Cnn.Close();

                    }

                }

                Cnn = null;
                Comando = null;
                Adaptador = null;              

            }

        }

        public bool ListadoDePrivilegiosParaELUsuario(ModuloInterfazRolEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT 0 as 'idModuloInterfazUsuario',p.idPrivilegio, 
                mir.Acceso, p.Nombre as 'Privilegio', i.NombreAMostrar,i.Nombre as 'Interfaz', m.Nombre as 'Modulo' 
                FROM modulointerfazrol as mir
                inner join privilegio as p on p.idPrivilegio = mir.idPrivilegio
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where mir.idModuloInterfazRol > 0 and mir.idRol = {0}
                union
                Select 0 as 'idModuloInterfazUsuario', p.idPrivilegio, 0 as 'Acceso', 
                p.Nombre as 'Privilegio', i.NombreAMostrar,i.Nombre as 'Interfaz', m.Nombre as 'Modulo' 
                 from privilegio as p
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where p.idPrivilegio not in (Select idPrivilegio from modulointerfazrol where idRol = {0}) ", oRegistroEN.oRolEN.idRol);
                Comando.CommandText = Consultas;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                return false;
            }
            finally
            {

                if (Cnn != null)
                {

                    if (Cnn.State == ConnectionState.Open)
                    {

                        Cnn.Close();

                    }

                }

                Cnn = null;
                Comando = null;
                Adaptador = null;

            }

        }

        public bool ListadoDePrivilegioDelRol(ModuloInterfazRolEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT mir.idModuloInterfazRol,p.idPrivilegio, 
                mir.Acceso, p.Nombre as 'Privilegio', i.NombreAMostrar,i.Nombre as 'Interfaz', m.Nombre as 'Modulo' 
                FROM modulointerfazrol as mir
                inner join privilegio as p on p.idPrivilegio = mir.idPrivilegio
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where mir.idModuloInterfazRol > 0 and mir.idRol = {0} {1}
                union
                Select 0 as 'idModuloInterfazRol', p.idPrivilegio, 0 as 'Acceso', 
                p.Nombre as 'Privilegio', i.NombreAMostrar,i.Nombre as 'Interfaz', m.Nombre as 'Modulo' 
                 from privilegio as p
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where p.idPrivilegio not in (Select idPrivilegio from modulointerfazrol where idRol = {0}) {1} {2}", oRegistroEN.oRolEN.idRol, oRegistroEN.Where, oRegistroEN.OrderBy);
                System.Diagnostics.Debug.Print(Consultas);
                Comando.CommandText = Consultas;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                return false;
            }
            finally
            {

                if (Cnn != null)
                {

                    if (Cnn.State == ConnectionState.Open)
                    {

                        Cnn.Close();

                    }

                }

                Cnn = null;
                Comando = null;
                Adaptador = null;

            }

        }

        public bool ListadoPorIdentificador(ModuloInterfazRolEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT mir.idModuloInterfazRol, mir.idRol,mir.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                i.Nombre as 'Interfaz', i.NombreAMostrar, m.Nombre as 'Modulo' , mir.Acceso
                FROM modulointerfazrol as mir
                inner join privilegio as p on p.idPrivilegio = mir.idPrivilegio
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where mir.idModuloInterfazRol = {0} ", oRegistroEN.idModuloInterfazRol);
                Comando.CommandText = Consultas;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                return false;
            }
            finally
            {

                if (Cnn != null)
                {

                    if (Cnn.State == ConnectionState.Open)
                    {

                        Cnn.Close();

                    }

                }

                Cnn = null;
                Comando = null;
                Adaptador = null;

            }

        }

        public bool ListadoParaCombos(ModuloInterfazRolEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT mir.idModuloInterfazRol, mir.idRol,mir.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                i.Nombre as 'Interfaz', i.NombreAMostrar, m.Nombre as 'Modulo' , mir.Acceso
                FROM modulointerfazrol as mir
                inner join privilegio as p on p.idPrivilegio = mir.idPrivilegio
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where mir.idModuloInterfazRol > 0 {0} {1}; ", oRegistroEN.Where, oRegistroEN.OrderBy);
                Comando.CommandText = Consultas;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                return false;
            }
            finally
            {

                if (Cnn != null)
                {

                    if (Cnn.State == ConnectionState.Open)
                    {

                        Cnn.Close();

                    }

                }

                Cnn = null;
                Comando = null;
                Adaptador = null;

            }

        }

        public bool ListadoParaReportes(ModuloInterfazRolEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT mir.idModuloInterfazRol, mir.idRol,mir.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                i.Nombre as 'Interfaz', i.NombreAMostrar, m.Nombre as 'Modulo' , mir.Acceso
                FROM modulointerfazrol as mir
                inner join privilegio as p on p.idPrivilegio = mir.idPrivilegio
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where mir.idModuloInterfazRol > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
                Comando.CommandText = Consultas;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                return false;
            }
            finally
            {

                if (Cnn != null)
                {

                    if (Cnn.State == ConnectionState.Open)
                    {

                        Cnn.Close();

                    }

                }

                Cnn = null;
                Comando = null;
                Adaptador = null;

            }

        }

        public bool ListadoPorIdentificadorDelRol(ModuloInterfazRolEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT mir.idModuloInterfazRol, mir.idRol,mir.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                i.Nombre as 'Interfaz', i.NombreAMostrar, m.Nombre as 'Modulo' , mir.Acceso
                FROM modulointerfazrol as mir
                inner join privilegio as p on p.idPrivilegio = mir.idPrivilegio
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where mir.idRol = {0} ", oRegistroEN.oRolEN.idRol);
                Comando.CommandText = Consultas;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                return false;
            }
            finally
            {

                if (Cnn != null)
                {

                    if (Cnn.State == ConnectionState.Open)
                    {

                        Cnn.Close();

                    }

                }

                Cnn = null;
                Comando = null;
                Adaptador = null;

            }

        }

        #endregion

        #region "Funciones de Validación"

        public bool ValidarRegistroDuplicado(ModuloInterfazRolEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                switch (TipoDeOperacion.Trim().ToUpper()){

                    case "AGREGAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idModuloInterfazRol from modulointerfazrol where idPrivilegio = @idPrivilegio and idRol = @idRol) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idPrivilegio", MySqlDbType.Int32)).Value = oRegistroEN.oPrivilegioEN.idPrivilegio;
                        Comando.Parameters.Add(new MySqlParameter("@idRol", MySqlDbType.Int32)).Value = oRegistroEN.oRolEN.idRol;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idModuloInterfazRol from modulointerfazrol where idPrivilegio = @idPrivilegio and idRol = @idRol and idModuloInterfazRol <> @idModuloInterfazRol) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idPrivilegio", MySqlDbType.Int32)).Value = oRegistroEN.oPrivilegioEN.idPrivilegio;
                        Comando.Parameters.Add(new MySqlParameter("@idRol", MySqlDbType.Int32)).Value = oRegistroEN.oRolEN.idRol;
                        Comando.Parameters.Add(new MySqlParameter("@idModuloInterfazRol", MySqlDbType.Int32)).Value = oRegistroEN.idModuloInterfazRol;

                        break;

                    default:
                        throw new ArgumentException( "La aperación solicitada no esta disponible");                        

                }
                
                Comando.CommandText = Consultas;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                if (Convert.ToInt32(DT.Rows[0]["RES"].ToString()) > 0) {
                    
                    DescripcionDeOperacion = string.Format("Ya existe información del Registro dentro de nuestro sistema: {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));
                    this.Error = DescripcionDeOperacion;
                    return true;

                }

                return false;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al validar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idModuloInterfazRol, "VALIDAR", "ERROR AL VALIDAR LA INFORMACIÓN DE LA MODULO INTERFAZ ROL", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

                return false;
            }
            finally
            {

                if (Cnn != null)
                {

                    if (Cnn.State == ConnectionState.Open)
                    {

                        Cnn.Close();

                    }

                }

                Cnn = null;
                Comando = null;
                Adaptador = null;
                oTransaccionesAD = null;

            }

        }

        #endregion

        #region "Funciones que retornan información"

        private string InformacionDelRegistro(ModuloInterfazRolEN oRegistroEN) {

            string Cadena = @"idModuloInterfazRol: {0}, idPrivilegio: {1}, idRol: {2}, Acceso: {3}, IdUsuarioDeCreacion: {4}, FechaDeCreacion: {5}, IdUsuarioDeModificacion: {6}, FechaDeModificacion: {7}";
            Cadena = string.Format(Cadena, oRegistroEN.idModuloInterfazRol, oRegistroEN.oPrivilegioEN, oRegistroEN.oRolEN.idRol, oRegistroEN.Acceso, oRegistroEN.IdUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.IdUsuarioDeModificacion, oRegistroEN.FechaDeModificacion);
            Cadena = Cadena.Replace(",", Environment.NewLine);
            return Cadena;            

        }

        private string TraerCadenaDeConexion(DatosDeConexionEN oDatos) {
            string cadena = string.Format("Data Source='{0}';Initial Catalog='{1}';Persist Security Info=True;User ID='{2}';Password='{3}'", oDatos.Servidor, oDatos.BaseDeDatos, oDatos.Usuario, oDatos.Contraseña);
            return cadena;
        }

        public DataTable TraerDatos() {
            return DT;
        }

        #endregion


    }
}
