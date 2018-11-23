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
    public class ConfiguracionAD
    {

        public string Error { set; get; }
        private MySqlConnection Cnn = null;
        private MySqlCommand Comando = null;
        private MySqlDataAdapter Adaptador = null;
        private TransaccionesAD oTransaccionesAD = null;
        string Consultas;
        string DescripcionDeOperacion;
        private DataTable DT { set; get; }

        public ConfiguracionAD(){

            oTransaccionesAD = new TransaccionesAD();
            oTransaccionesAD.modulo = "General";
            oTransaccionesAD.modelo = "ConfiguracionAD";
            oTransaccionesAD.NombreDelEquipo = Environment.MachineName;
            oTransaccionesAD.Fecha_creacion = System.DateTime.Now;
            oTransaccionesAD.tabla = "Configuracion";

        }

        #region "Funciones para datos dll"

        public bool Agregar(ConfiguracionEN oRegistroEN, DatosDeConexionEN oDatos) {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"insert into configuracion
                (RutaRespaldos, RutaRespaldosDeExcel,PathMysSQLDump,PathMySQL, 
                NombreDelSistema, TiempoDeRespaldo,@PrecioPorDefecto
                )
                values
                (@RutaRespaldos, @RutaRespaldosDeExcel, @PathMysSQLDump, 
                @PathMySQL, @NombreDelSistema, 
                @TiempoDeRespaldo,@PrecioPorDefecto);

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@RutaRespaldos", MySqlDbType.VarChar, oRegistroEN.RutaRespaldos.Trim().Length)).Value = oRegistroEN.RutaRespaldos.Trim();
                Comando.Parameters.Add(new MySqlParameter("@RutaRespaldosDeExcel", MySqlDbType.VarChar, oRegistroEN.RutaRespaldosDeExcel.Trim().Length)).Value = oRegistroEN.RutaRespaldosDeExcel.Trim();                
                Comando.Parameters.Add(new MySqlParameter("@PathMysSQLDump", MySqlDbType.VarChar, oRegistroEN.PathMysSQLDump.Trim().Length)).Value = oRegistroEN.PathMysSQLDump.Trim();
                Comando.Parameters.Add(new MySqlParameter("@PathMySQL", MySqlDbType.VarChar, oRegistroEN.PathMySQL.Trim().Length)).Value = oRegistroEN.PathMySQL.Trim();                
                Comando.Parameters.Add(new MySqlParameter("@NombreDelSistema", MySqlDbType.VarChar, oRegistroEN.NombreDelSistema.Trim().Length)).Value = oRegistroEN.NombreDelSistema.Trim();
                Comando.Parameters.Add(new MySqlParameter("@TiempoDeRespaldo", MySqlDbType.Int32)).Value = oRegistroEN.TiempoDeRespaldo;
                Comando.Parameters.Add(new MySqlParameter("@PrecioPorDefecto", MySqlDbType.Int32)).Value = oRegistroEN.PrecioPorDefecto;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.IdConfiguracion = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());

                DescripcionDeOperacion = string.Format("El registro fue Insertado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP,oRegistroEN.IdConfiguracion, "AGREGAR", "INFORMACIÓN DE LA CONFIGURACIÓN AGREGADA CORRECTAMENTE", "CORRECTA", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);
                
                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al insertar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.IdConfiguracion, "AGREGAR", "ERROR AL AGREGAR LA INFORMACIÓN DE LA CONFIGURACIÓN", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);
                
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

        public bool Actualizar(ConfiguracionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"UPDATE configuracion SET
	                RutaRespaldos = @RutaRespaldos, RutaRespaldosDeExcel = @RutaRespaldosDeExcel, 
                    PathMysSQLDump = @PathMysSQLDump, 
                    PathMySQL= @PathMySQL, 
                    NombreDelSistema = @NombreDelSistema,                    
                    TiempoDeRespaldo = @TiempoDeRespaldo,
                    PrecioPorDefecto = @PrecioPorDefecto
                    

                WHERE IdConfiguracion = @IdConfiguracion;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@IdConfiguracion", MySqlDbType.Int32)).Value = oRegistroEN.IdConfiguracion;
                Comando.Parameters.Add(new MySqlParameter("@RutaRespaldos", MySqlDbType.VarChar, oRegistroEN.RutaRespaldos.Trim().Length)).Value = oRegistroEN.RutaRespaldos.Trim();
                Comando.Parameters.Add(new MySqlParameter("@RutaRespaldosDeExcel", MySqlDbType.VarChar, oRegistroEN.RutaRespaldosDeExcel.Trim().Length)).Value = oRegistroEN.RutaRespaldosDeExcel.Trim();                
                Comando.Parameters.Add(new MySqlParameter("@PathMysSQLDump", MySqlDbType.VarChar, oRegistroEN.PathMysSQLDump.Trim().Length)).Value = oRegistroEN.PathMysSQLDump.Trim();
                Comando.Parameters.Add(new MySqlParameter("@PathMySQL", MySqlDbType.VarChar, oRegistroEN.PathMySQL.Trim().Length)).Value = oRegistroEN.PathMySQL.Trim();                
                Comando.Parameters.Add(new MySqlParameter("@NombreDelSistema", MySqlDbType.VarChar, oRegistroEN.NombreDelSistema.Trim().Length)).Value = oRegistroEN.NombreDelSistema.Trim();                
                Comando.Parameters.Add(new MySqlParameter("@TiempoDeRespaldo", MySqlDbType.Int32)).Value = oRegistroEN.TiempoDeRespaldo;
                Comando.Parameters.Add(new MySqlParameter("@PrecioPorDefecto", MySqlDbType.Int32)).Value = oRegistroEN.PrecioPorDefecto;

                Comando.ExecuteNonQuery();
                
                DescripcionDeOperacion = string.Format("El registro fue Actualizado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.IdConfiguracion, "ACTUALIZAR", "INFORMACIÓN DE LA CONFIGURACIÓN ACTUALIZADA", "CORRECTA", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al actualizar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.IdConfiguracion, "ACTUALIZAR", "ERROR AL ACTUALIZAR LA INFORMACIÓN DE LA CONFIGURACIÓN", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

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

        public bool Eliminar(ConfiguracionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from Configuracion where IdConfiguracion = @IdConfiguracion;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@IdConfiguracion", MySqlDbType.Int32)).Value = oRegistroEN.IdConfiguracion;
                
                Comando.ExecuteNonQuery();

                DescripcionDeOperacion = string.Format("El registro fue Eliminado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.IdConfiguracion, "ELIMINAR", "INFORMACIÓN DE LA CONFIGURACIÓN ELIMINADA", "CORRECTA", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al eliminar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.IdConfiguracion, "ELIMINAR", "ERROR AL ELIMINAR LA INFORMACIÓN DE LA CONFIGURACIÓN", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

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

        public bool Listado(ConfiguracionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select IdConfiguracion, RutaRespaldos, RutaRespaldosDeExcel,PathMysSQLDump,PathMySQL, NombreDelSistema, TiempoDeRespaldo, PrecioPorDefecto from configuracion where IdConfiguracion > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoPorIdentificador(ConfiguracionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select IdConfiguracion, RutaRespaldos, RutaRespaldosDeExcel,PathMysSQLDump,PathMySQL,NombreDelSistema,TiempoDeRespaldo,PrecioPorDefecto from configuracion where IdConfiguracion = {0} ", oRegistroEN.IdConfiguracion);
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

        public bool ListadoParaCombos(ConfiguracionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select IdConfiguracion, RutaRespaldos, RutaRespaldosDeExcel,PathMysSQLDump,PathMySQL,NombreDelSistema, TiempoDeRespaldo,PrecioPorDefecto from configuracion where IdConfiguracion > 0 {0} {1}; ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoParaReportes(ConfiguracionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select IdConfiguracion, RutaRespaldos, RutaRespaldosDeExcel,PathMysSQLDump,PathMySQL,NombreDelSistema, TiempoDeRespaldo,PrecioPorDefecto from configuracion where IdConfiguracion > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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
        
        #region "Funciones que retornan información"

        private string InformacionDelRegistro(ConfiguracionEN oRegistroEN) {

            string Cadena = @"IdConfiguracion: {0}, RutaRespaldos: {1}, RutaRespaldosDeExcel: {2}, PathMysSQLDump: {3}, PathMySQL: {4},NombreDelSistema: {5}, PrecioPorDefecto: {6}";
            Cadena = string.Format(Cadena, oRegistroEN.IdConfiguracion, oRegistroEN.RutaRespaldos, oRegistroEN.RutaRespaldosDeExcel,oRegistroEN.PathMysSQLDump, oRegistroEN.PathMySQL, oRegistroEN.NombreDelSistema, oRegistroEN.PrecioPorDefecto);
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
