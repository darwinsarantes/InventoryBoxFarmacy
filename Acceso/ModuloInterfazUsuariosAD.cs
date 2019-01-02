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
    public class ModuloInterfazUsuariosAD
    {

        public string Error { set; get; }
        private MySqlConnection Cnn = null;
        private MySqlCommand Comando = null;
        private MySqlDataAdapter Adaptador = null;
        private TransaccionesAD oTransaccionesAD = null;
        string Consultas;
        string DescripcionDeOperacion;
        private DataTable DT { set; get; }

        public ModuloInterfazUsuariosAD(){

            oTransaccionesAD = new TransaccionesAD();
            oTransaccionesAD.modulo = "General";
            oTransaccionesAD.modelo = "ModuloInterfazUsuariosAD";
            oTransaccionesAD.NombreDelEquipo = Environment.MachineName;
            oTransaccionesAD.Fecha_creacion = System.DateTime.Now;
            oTransaccionesAD.tabla = "ModuloInterfazUsuarios";

        }

        #region "Funciones para datos dll"

        public bool Agregar(ModuloInterfazUsuariosEN oRegistroEN, DatosDeConexionEN oDatos) {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"INSERT INTO modulointerfazusuario
                (idPrivilegio, idUsuario, Acceso, IdUsuarioDeCreacion, FechaDeCreacion, IdUsuarioDeModificacion, FechaDeModificacion)
                values
                (@idPrivilegio, @idUsuario, @Acceso, @IdUsuarioDeCreacion, current_timestamp(), @IdUsuarioDeModificacion, current_timestamp());

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idPrivilegio", MySqlDbType.Int32)).Value = oRegistroEN.oPrivilegioEN.idPrivilegio;
                Comando.Parameters.Add(new MySqlParameter("@idUsuario", MySqlDbType.Int32)).Value = oRegistroEN.oUsuarioEN.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@Acceso", MySqlDbType.Int32)).Value = oRegistroEN.Acceso;
                Comando.Parameters.Add(new MySqlParameter("@IdUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@IdUsuarioDeModificacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idModuloInterfazUsuario = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());

                DescripcionDeOperacion = string.Format("El registro fue Insertado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP,oRegistroEN.idModuloInterfazUsuario, "AGREGAR", "INFORMACIÓN DE LA MODULO INTERFAZ USUARIO AGREGADA CORRECTAMENTE", "CORRECTA", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);
                
                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al insertar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idModuloInterfazUsuario, "AGREGAR", "ERROR AL AGREGAR LA INFORMACIÓN DE LA ModuloInterfazUsuarios", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);
                
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

        public bool Actualizar(ModuloInterfazUsuariosEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"UPDATE modulointerfazusuario set
	                idPrivilegio = @idPrivilegio, idUsuario = @idUsuario, Acceso = @Acceso, IdUsuarioDeModificacion = @IdUsuarioDeModificacion, FechaDeModificacion = current_timestamp()
                Where idModuloInterfazUsuario = @idModuloInterfazUsuario;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idModuloInterfazUsuario", MySqlDbType.Int32)).Value = oRegistroEN.idModuloInterfazUsuario;
                Comando.Parameters.Add(new MySqlParameter("@idPrivilegio", MySqlDbType.Int32)).Value = oRegistroEN.oPrivilegioEN.idPrivilegio;
                Comando.Parameters.Add(new MySqlParameter("@idUsuario", MySqlDbType.Int32)).Value = oRegistroEN.oUsuarioEN.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@Acceso", MySqlDbType.Int32)).Value = oRegistroEN.Acceso;                
                Comando.Parameters.Add(new MySqlParameter("@IdUsuarioDeModificacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

                Comando.ExecuteNonQuery();
                
                DescripcionDeOperacion = string.Format("El registro fue Actualizado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idModuloInterfazUsuario, "ACTUALIZAR", "INFORMACIÓN DE LA MODULO INTERFAZ USUARIO ACTUALIZADA", "CORRECTA", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al actualizar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idModuloInterfazUsuario, "ACTUALIZAR", "ERROR AL ACTUALIZAR LA INFORMACIÓN DE LA MODULO INTERFAZ USUARIO", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

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

        public bool Eliminar(ModuloInterfazUsuariosEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from modulointerfazusuario where idModuloInterfazUsuario = @idModuloInterfazUsuario;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idModuloInterfazUsuario", MySqlDbType.Int32)).Value = oRegistroEN.idModuloInterfazUsuario;
                
                Comando.ExecuteNonQuery();

                DescripcionDeOperacion = string.Format("El registro fue Eliminado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                //oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idModuloInterfazUsuario, "ELIMINAR", "INFORMACIÓN DE LA MODULO INTERFAZ USUARIO ELIMINADA", "CORRECTA", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al eliminar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                //oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idModuloInterfazUsuario, "ELIMINAR", "ERROR AL ELIMINAR LA INFORMACIÓN DE LA MODULO INTERFAZ USUARIO", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

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

        public bool Listado(ModuloInterfazUsuariosEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT miu.idModuloInterfazUsuario,p.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                miu.Acceso, p.Nombre as 'Privilegio', i.NombreAMostrar,i.Nombre as 'Interfaz', m.Nombre as 'Modulo' 
                FROM modulointerfazusuario as miu
                inner join privilegio as p on p.idPrivilegio = miu.idPrivilegio
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where miu.idModuloInterfazUsuario > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoPrivilegiosDelUsuario(ModuloInterfazUsuariosEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT miu.idModuloInterfazUsuario,p.idPrivilegio, 
                miu.Acceso, p.Nombre as 'Privilegio', i.NombreAMostrar,i.Nombre as 'Interfaz', m.Nombre as 'Modulo' 
                FROM modulointerfazusuario as miu
                inner join privilegio as p on p.idPrivilegio = miu.idPrivilegio
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where miu.idModuloInterfazUsuario > 0 and miu.idUsuario = {0} {1}
                union
                Select 0 as 'idModuloInterfazUsuario', p.idPrivilegio, 0 as 'Acceso', 
                p.Nombre as 'Privilegio', i.NombreAMostrar,i.Nombre as 'Interfaz', m.Nombre as 'Modulo' 
                 from privilegio as p
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where p.idPrivilegio not in (Select idPrivilegio from modulointerfazusuario where idUsuario = {0}) {1} {2} ", oRegistroEN.oUsuarioEN.idUsuario,oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoPrivilegiosDelUsuariosPorIntefaz(ModuloInterfazUsuariosEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT miu.idModuloInterfazUsuario,p.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                miu.Acceso, p.Nombre as 'Privilegio', i.NombreAMostrar,i.Nombre as 'Interfaz', m.Nombre as 'Modulo' 
                FROM modulointerfazusuario as miu
                inner join privilegio as p on p.idPrivilegio = miu.idPrivilegio
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where miu.idModuloInterfazUsuario > 0 and miu.idUsuario = {0} and upper(trim( i.Nombre)) = upper('{1}') ", oRegistroEN.oUsuarioEN.idUsuario, oRegistroEN.oPrivilegioEN.oModuloInterfazEN.oInterfazEN.Nombre.Trim());

                System.Diagnostics.Debug.Print(Consultas);
                Comando.CommandText = Consultas;

                Adaptador = new MySqlDataAdapter();                
                DT = new DataTable();
                DT.Clear();

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

        public bool ListadoPrivilegiosDelUsuariosPorModulo(ModuloInterfazUsuariosEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT miu.idModuloInterfazUsuario,p.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                miu.Acceso, p.Nombre as 'Privilegio', i.NombreAMostrar,i.Nombre as 'Interfaz', m.Nombre as 'Modulo' 
                FROM modulointerfazusuario as miu
                inner join privilegio as p on p.idPrivilegio = miu.idPrivilegio
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where miu.idModuloInterfazUsuario > 0 and miu.idUsuario = {0} and p.Nombre = 'Acceso' ; ", oRegistroEN.oUsuarioEN.idUsuario);

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

        public bool ListadoPorIdentificador(ModuloInterfazUsuariosEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT miu.idModuloInterfazUsuario,p.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                miu.Acceso, p.Nombre as 'Privilegio', i.NombreAMostrar,i.Nombre as 'Interfaz', m.Nombre as 'Modulo' 
                FROM modulointerfazusuario as miu
                inner join privilegio as p on p.idPrivilegio = miu.idPrivilegio
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where miu.idModuloInterfazUsuario = {0} ", oRegistroEN.idModuloInterfazUsuario);
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

        public bool ListadoParaCombos(ModuloInterfazUsuariosEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT miu.idModuloInterfazUsuario,p.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                miu.Acceso, p.Nombre as 'Privilegio', i.NombreAMostrar,i.Nombre as 'Interfaz', m.Nombre as 'Modulo' 
                FROM modulointerfazusuario as miu
                inner join privilegio as p on p.idPrivilegio = miu.idPrivilegio
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where miu.idModuloInterfazUsuario > 0 {0} {1}; ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoParaReportes(ModuloInterfazUsuariosEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT miu.idModuloInterfazUsuario,p.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                miu.Acceso, p.Nombre as 'Privilegio', i.NombreAMostrar,i.Nombre as 'Interfaz', m.Nombre as 'Modulo' 
                FROM modulointerfazusuario as miu
                inner join privilegio as p on p.idPrivilegio = miu.idPrivilegio
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where miu.idModuloInterfazUsuario > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoPorIdentificadorDelUsuario(ModuloInterfazUsuariosEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT miu.idModuloInterfazUsuario,p.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                miu.Acceso, p.Nombre as 'Privilegio', i.NombreAMostrar,i.Nombre as 'Interfaz', m.Nombre as 'Modulo' 
                FROM modulointerfazusuario as miu
                inner join privilegio as p on p.idPrivilegio = miu.idPrivilegio
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where miu.idModuloInterfazUsuario > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarRegistroDuplicado(ModuloInterfazUsuariosEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idModuloInterfazUsuario from modulointerfazusuario where idPrivilegio = @idPrivilegio and idUsuario = @idUsuario) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idPrivilegio", MySqlDbType.Int32)).Value = oRegistroEN.oPrivilegioEN.idPrivilegio;
                        Comando.Parameters.Add(new MySqlParameter("@idUsuario", MySqlDbType.Int32)).Value = oRegistroEN.oUsuarioEN.idUsuario;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idModuloInterfazUsuario from modulointerfazusuario where idPrivilegio = @idPrivilegio and idUsuario = @idUsuario and idModuloInterfazUsuario <> @idModuloInterfazUsuario) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idPrivilegio", MySqlDbType.Int32)).Value = oRegistroEN.oPrivilegioEN.idPrivilegio;
                        Comando.Parameters.Add(new MySqlParameter("@idUsuario", MySqlDbType.Int32)).Value = oRegistroEN.oUsuarioEN.idUsuario;
                        Comando.Parameters.Add(new MySqlParameter("@idModuloInterfazUsuario", MySqlDbType.Int32)).Value = oRegistroEN.idModuloInterfazUsuario;

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
                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idModuloInterfazUsuario, "VALIDAR", "ERROR AL VALIDAR LA INFORMACIÓN DE LA MODULO INTERFAZ USUARIO", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

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

        private string InformacionDelRegistro(ModuloInterfazUsuariosEN oRegistroEN) {

            string Cadena = @"idModuloInterfazUsuario: {0}, idPrivilegio: {1}, idUsuario: {2}, Acceso: {3}, IdUsuarioDeCreacion: {4}, FechaDeCreacion: {5}, IdUsuarioDeModificacion: {6}, FechaDeModificacion: {7}";
            Cadena = string.Format(Cadena, oRegistroEN.idModuloInterfazUsuario, oRegistroEN.oPrivilegioEN, oRegistroEN.oUsuarioEN.idUsuario, oRegistroEN.Acceso, oRegistroEN.IdUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.IdUsuarioDeModificacion, oRegistroEN.FechaDeModificacion);
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
