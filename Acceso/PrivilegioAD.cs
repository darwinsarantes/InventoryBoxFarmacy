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
    public class PrivilegioAD
    {

        public string Error { set; get; }
        private MySqlConnection Cnn = null;
        private MySqlCommand Comando = null;
        private MySqlDataAdapter Adaptador = null;
        private TransaccionesAD oTransaccionesAD = null;
        string Consultas;
        string DescripcionDeOperacion;
        private DataTable DT { set; get; }

        public PrivilegioAD(){

            oTransaccionesAD = new TransaccionesAD();
            oTransaccionesAD.modulo = "General";
            oTransaccionesAD.modelo = "PrivilegioAD";
            oTransaccionesAD.NombreDelEquipo = Environment.MachineName;
            oTransaccionesAD.Fecha_creacion = System.DateTime.Now;
            oTransaccionesAD.tabla = "Privilegio";

        }

        #region "Funciones para datos dll"

        public bool Agregar(PrivilegioEN oRegistroEN, DatosDeConexionEN oDatos) {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"insert into privilegio
                (idModuloInterfaz, Nombre, IdUsuarioDeCreacion, FechaDeCreacion, IdUsuarioDeModificacion, FechaDeModificacion)
                values
                (@idModuloInterfaz, @Nombre, @IdUsuarioDeCreacion, current_timestamp(), @IdUsuarioDeModificacion, current_timestamp());

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();                
                Comando.Parameters.Add(new MySqlParameter("@idModuloInterfaz", MySqlDbType.Int32)).Value = oRegistroEN.oModuloInterfazEN.idModuloInterfaz;
                Comando.Parameters.Add(new MySqlParameter("@IdUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@IdUsuarioDeModificacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idPrivilegio = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());

                DescripcionDeOperacion = string.Format("El registro fue Insertado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP,oRegistroEN.idPrivilegio, "AGREGAR", "INFORMACIÓN DE LA PRIVILEGIO AGREGADA CORRECTAMENTE", "CORRECTA", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);
                
                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al insertar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idPrivilegio, "AGREGAR", "ERROR AL AGREGAR LA INFORMACIÓN DE LA Privilegio", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);
                
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

        public bool Actualizar(PrivilegioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"UPDATE privilegio set
	                idModuloInterfaz = @idModuloInterfaz, Nombre = @Nombre, IdUsuarioDeModificacion = @IdUsuarioDeModificacion, FechaDeModificacion = current_timestamp()
                where idPrivilegio = @idPrivilegio";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idPrivilegio", MySqlDbType.Int32)).Value = oRegistroEN.idPrivilegio;
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@idModuloInterfaz", MySqlDbType.Int32)).Value = oRegistroEN.oModuloInterfazEN.idModuloInterfaz;                
                Comando.Parameters.Add(new MySqlParameter("@IdUsuarioDeModificacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

                Comando.ExecuteNonQuery();
                
                DescripcionDeOperacion = string.Format("El registro fue Actualizado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idPrivilegio, "ACTUALIZAR", "INFORMACIÓN DE LA PRIVILEGIO ACTUALIZADA", "CORRECTA", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al actualizar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idPrivilegio, "ACTUALIZAR", "ERROR AL ACTUALIZAR LA INFORMACIÓN DE LA PRIVILEGIO", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

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

        public bool Eliminar(PrivilegioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from Privilegio where idPrivilegio = @idPrivilegio;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idPrivilegio", MySqlDbType.Int32)).Value = oRegistroEN.idPrivilegio;
                
                Comando.ExecuteNonQuery();

                DescripcionDeOperacion = string.Format("El registro fue Eliminado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idPrivilegio, "ELIMINAR", "INFORMACIÓN DE LA PRIVILEGIO ELIMINADA", "CORRECTA", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al eliminar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idPrivilegio, "ELIMINAR", "ERROR AL ELIMINAR LA INFORMACIÓN DE LA Privilegio", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

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

        public bool Listado(PrivilegioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT p.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                i.Nombre as 'Interfaz', i.NombreAMostrar, m.Nombre as 'Modulo' FROM privilegio as p
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where p.idPrivilegio > 0  {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoPorIdentificador(PrivilegioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT p.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                i.Nombre as 'Interfaz', i.NombreAMostrar, m.Nombre as 'Modulo' FROM privilegio as p
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where p.idPrivilegio = {0}  ", oRegistroEN.idPrivilegio);
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

        public bool ListadoParaCombos(PrivilegioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT p.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                i.Nombre as 'Interfaz', i.NombreAMostrar, m.Nombre as 'Modulo' FROM privilegio as p
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where p.idPrivilegio > 0  {0} {1}; ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoParaReportes(PrivilegioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT p.idPrivilegio, p.idModuloInterfaz, mi.idInterfaz, mi.idModulo,
                i.Nombre as 'Interfaz', i.NombreAMostrar, m.Nombre as 'Modulo' FROM privilegio as p
                inner join modulointerfaz as mi on mi.idModuloInterfaz = p.idModuloInterfaz
                inner join interfaz as i on i.idInterfaz = mi.idInterfaz
                inner join modulo as m on m.idModulo = mi.idModulo
                where p.idPrivilegio > 0  {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarRegistroDuplicado(PrivilegioEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idPrivilegio from privilegio where idModuloInterfaz = @idModuloInterfaz and upper(trim( Nombre ) ) = upper( trim ( @Nombre ) )) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                        Comando.Parameters.Add(new MySqlParameter("@idModuloInterfaz", MySqlDbType.Int32)).Value = oRegistroEN.oModuloInterfazEN.idModuloInterfaz;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idPrivilegio from privilegio where idModuloInterfaz = @idModuloInterfaz and upper(trim( Nombre ) ) = upper( trim ( @Nombre ) ) and idPrivilegio <> @idPrivilegio) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                        Comando.Parameters.Add(new MySqlParameter("@idModuloInterfaz", MySqlDbType.Int32)).Value = oRegistroEN.oModuloInterfazEN.idModuloInterfaz;
                        Comando.Parameters.Add(new MySqlParameter("@idPrivilegio", MySqlDbType.Int32)).Value = oRegistroEN.idPrivilegio;

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
                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.idPrivilegio, "VALIDAR", "ERROR AL VALIDAR LA INFORMACIÓN DE LA PRIVILEGIO", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

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

        private string InformacionDelRegistro(PrivilegioEN oRegistroEN) {

            string Cadena = @"idPrivilegio: {0}, idModuloInterfaz: {1}, Nombre: {2}, IdUsuarioDeCreacion: {3}, FechaDeCreacion: {4}, IdUsuarioDeModificacion: {5}, FechaDeModificacion: {6}";
            Cadena = string.Format(Cadena, oRegistroEN.idPrivilegio,oRegistroEN.oModuloInterfazEN.idModuloInterfaz, oRegistroEN.Nombre , oRegistroEN.IdUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.IdUsuarioDeModificacion, oRegistroEN.FechaDeModificacion);
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
