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
    public class EmpresaAD
    {

        public string Error { set; get; }
        private MySqlConnection Cnn = null;
        private MySqlCommand Comando = null;
        private MySqlDataAdapter Adaptador = null;
        private TransaccionesAD oTransaccionesAD = null;
        string Consultas;
        string DescripcionDeOperacion;
        private DataTable DT { set; get; }

        public EmpresaAD(){

            oTransaccionesAD = new TransaccionesAD();
            oTransaccionesAD.modulo = "General";
            oTransaccionesAD.modelo = "EmpresaAD";
            oTransaccionesAD.NombreDelEquipo = Environment.MachineName;
            oTransaccionesAD.Fecha_creacion = System.DateTime.Now;
            oTransaccionesAD.tabla = "Empresa";

        }

        #region "Funciones para datos dll"

        public bool Agregar(EmpresaEN oRegistroEN, DatosDeConexionEN oDatos) {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"insert into Empresa 
                (Nombre, Direccion, Telefonos, NRuc, Logo, Celular, Email, SitioWeb, Descripcion)
                values
                (@Nombre, @Direccion, @Telefonos, @NRuc, @Logo, @Celular, @Email, @SitioWeb, @Descripcion);

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Direccion", MySqlDbType.VarChar, oRegistroEN.Direccion.Trim().Length)).Value = oRegistroEN.Direccion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Telefonos", MySqlDbType.VarChar, oRegistroEN.Telefonos.Trim().Length)).Value = oRegistroEN.Telefonos.Trim();
                Comando.Parameters.Add(new MySqlParameter("@NRuc", MySqlDbType.VarChar, oRegistroEN.NRuc.Trim().Length)).Value = oRegistroEN.NRuc.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Celular", MySqlDbType.VarChar, oRegistroEN.Celular.Trim().Length)).Value = oRegistroEN.Celular.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar, oRegistroEN.Email.Trim().Length)).Value = oRegistroEN.Email.Trim();
                Comando.Parameters.Add(new MySqlParameter("@SitioWeb", MySqlDbType.VarChar, oRegistroEN.SitioWeb.Trim().Length)).Value = oRegistroEN.SitioWeb.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Logo", MySqlDbType.Binary)).Value = oRegistroEN.ALogo;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.IdEmpresa = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());

                DescripcionDeOperacion = string.Format("El registro fue Insertado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP,oRegistroEN.IdEmpresa, "AGREGAR", "INFORMACIÓN DE LA EMPRESA AGREGADA CORRECTAMENTE", "CORRECTA", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);
                
                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al insertar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.IdEmpresa, "AGREGAR", "ERROR AL AGREGAR LA INFORMACIÓN DE LA EMPRESA", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);
                
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

        public bool Actualizar(EmpresaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"UPDATE empresa set
	                Nombre = @Nombre, Direccion = @Direccion, Telefonos = @Telefonos, 
                    NRuc = @NRuc, Logo = @Logo, Celular = @Celular, Email = @Email, 
                    SitioWeb = @SitioWeb, Descripcion = @Descripcion
                where IdEmpresa = @IdEmpresa;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@IdEmpresa", MySqlDbType.Int32)).Value = oRegistroEN.IdEmpresa;
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Direccion", MySqlDbType.VarChar, oRegistroEN.Direccion.Trim().Length)).Value = oRegistroEN.Direccion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Telefonos", MySqlDbType.VarChar, oRegistroEN.Telefonos.Trim().Length)).Value = oRegistroEN.Telefonos.Trim();
                Comando.Parameters.Add(new MySqlParameter("@NRuc", MySqlDbType.VarChar, oRegistroEN.NRuc.Trim().Length)).Value = oRegistroEN.NRuc.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Celular", MySqlDbType.VarChar, oRegistroEN.Celular.Trim().Length)).Value = oRegistroEN.Celular.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar, oRegistroEN.Email.Trim().Length)).Value = oRegistroEN.Email.Trim();
                Comando.Parameters.Add(new MySqlParameter("@SitioWeb", MySqlDbType.VarChar, oRegistroEN.SitioWeb.Trim().Length)).Value = oRegistroEN.SitioWeb.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Logo", MySqlDbType.Binary)).Value = oRegistroEN.ALogo;

                Comando.ExecuteNonQuery();
                
                DescripcionDeOperacion = string.Format("El registro fue Actualizado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.IdEmpresa, "ACTUALIZAR", "INFORMACIÓN DE LA EMPRESA ACTUALIZADA", "CORRECTA", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al actualizar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.IdEmpresa, "ACTUALIZAR", "ERROR AL ACTUALIZAR LA INFORMACIÓN DE LA EMPRESA", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

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

        public bool Eliminar(EmpresaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from empresa where IdEmpresa = @IdEmpresa;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@IdEmpresa", MySqlDbType.Int32)).Value = oRegistroEN.IdEmpresa;
                
                Comando.ExecuteNonQuery();

                DescripcionDeOperacion = string.Format("El registro fue Eliminado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.IdEmpresa, "ELIMINAR", "INFORMACIÓN DE LA EMPRESA ELIMINADA", "CORRECTA", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al eliminar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.IdEmpresa, "ELIMINAR", "ERROR AL ELIMINAR LA INFORMACIÓN DE LA EMPRESA", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

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

        public bool Listado(EmpresaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select IdEmpresa, Nombre, Direccion, Telefonos, NRuc, Logo, 
                Celular, Email, SitioWeb, Descripcion from empresa 
                where IdEmpresa > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
                Comando.CommandText = Consultas;
                
                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();               

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                LlenarCamposEnEntidad(oRegistroEN);

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

        private void LlenarCamposEnEntidad(EmpresaEN oEmpresaEN)
        {
            oEmpresaEN.Nombre = DT.Rows[0].ItemArray[1].ToString();
            oEmpresaEN.Direccion = DT.Rows[0].ItemArray[2].ToString();
            oEmpresaEN.Telefonos = DT.Rows[0].ItemArray[3].ToString();
            oEmpresaEN.NRuc = DT.Rows[0].ItemArray[4].ToString();
            oEmpresaEN.oLogo = DT.Rows[0].ItemArray[5];            
        }

        public bool ListadoPorIdentificador(EmpresaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select IdEmpresa, Nombre, Direccion, Telefonos, NRuc, Logo, 
                Celular, Email, SitioWeb, Descripcion from empresa 
                where IdEmpresa = {0} ", oRegistroEN.IdEmpresa);
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

        public bool ListadoParaCombos(EmpresaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select IdEmpresa, concat(IdEmpresa, ' - ', Nombre) as 'Empresa' from empresa 
                where IdEmpresa > 0 {0} {1}; ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoParaReportes(EmpresaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select IdEmpresa, Nombre, Direccion, Telefonos, NRuc, Logo, 
                Celular, Email, SitioWeb, Descripcion from empresa 
                where IdEmpresa > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarRegistroDuplicado(EmpresaEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(SELECT IdEmpresa FROM empresa WHERE upper(trim(Nombre)) = upper(@Nombre)) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(SELECT IdEmpresa FROM empresa WHERE upper(trim(Nombre)) = upper(@Nombre) and IdEmpresa <> @IdEmpresa) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                        Comando.Parameters.Add(new MySqlParameter("@IdEmpresa", MySqlDbType.Int32)).Value = oRegistroEN.IdEmpresa;

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
                oTransaccionesAD.Agregar(oRegistroEN.oLoginEN.idUsuario, oRegistroEN.oLoginEN.NumeroIP, oRegistroEN.IdEmpresa, "VALIDAR", "ERROR AL VALIDAR LA INFORMACIÓN DE LA EMPRESA", "ERROR", DescripcionDeOperacion, oRegistroEN.oLoginEN.idUsuario, oDatos);

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

        private string InformacionDelRegistro(EmpresaEN oRegistroEN) {

            string Cadena = @"IdEmpresa: {0}, Nombre: {1}, Direccion: {2}, Telefonos: {3}, NRuc: {4}, Logo: {5}, Celular: {6}, Email: {7}, SitioWeb: {8}, Descripcion: {9}";
            Cadena = string.Format(Cadena, oRegistroEN.IdEmpresa, oRegistroEN.Nombre, oRegistroEN.Direccion, oRegistroEN.Telefonos, oRegistroEN.NRuc, oRegistroEN.ALogo, oRegistroEN.Celular, oRegistroEN.Email, oRegistroEN.SitioWeb, oRegistroEN.Descripcion);
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
