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
    public class ProveedorLaboratorioAD
    {

        public string Error { set; get; }
        private MySqlConnection Cnn = null;
        private MySqlCommand Comando = null;
        private MySqlDataAdapter Adaptador = null;
        private TransaccionesAD oTransaccionesAD = null;
        string Consultas;
        string DescripcionDeOperacion;
        private DataTable DT { set; get; }
              

        #region "Funciones para datos dll"

        public bool Agregar(ProveedorLaboratorioEN oRegistroEN, DatosDeConexionEN oDatos) {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"
                                
                insert into ProveedorLaboratorio
                (idProveedor, idLaboratorio, 
                idUsuarioDeCreacion, FechaDeCreacion, 
                idUsuarioModificacion, FechaDeModificacion)
                values
                (@idProveedor, @idLaboratorio, 
                @idUsuarioDeCreacion, current_timestamp(), 
                @idUsuarioModificacion, current_timestamp());

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;                               

                Comando.Parameters.Add(new MySqlParameter("@idProveedor", MySqlDbType.Int32)).Value = oRegistroEN.oProveedorEN.idProveedor;
                Comando.Parameters.Add(new MySqlParameter("@idLaboratorio", MySqlDbType.Int32)).Value = oRegistroEN.oLaboratorioEN.idLaboratorio;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.IdUsuarioDeCreacion;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.IdUsuarioDeModificacion;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idProveedorLaboratorio = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());
                
                DescripcionDeOperacion = string.Format("El registro fue Insertado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                //Agregamos la Transacción....
                TransaccionesEN oTran = InformacionDelaTransaccion(oRegistroEN, "Agregar", "Agregar Nuevo Registro", "CORRECTO");
                oTransaccionesAD.Agregar(oTran, oDatos);

                return true;


            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al insertar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);

                //Agregamos la Transacción....
                TransaccionesEN oTran = InformacionDelaTransaccion(oRegistroEN, "Agregar", "Agregar Nuevo Registro", "ERROR");
                oTransaccionesAD.Agregar(oTran, oDatos);

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
        
        public bool Actualizar(ProveedorLaboratorioEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"update ProveedorLaboratorio set

	                idProveedor = @idProveedor, 
                    idLaboratorio = @idLaboratorio, 
	                idUsuarioModificacion = @idUsuarioModificacion, 
                    FechaDeModificacion = current_timestamp()

                where idProveedorLaboratorio = @idProveedorLaboratorio;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProveedorLaboratorio", MySqlDbType.Int32)).Value = oRegistroEN.idProveedorLaboratorio;
                Comando.Parameters.Add(new MySqlParameter("@idProveedor", MySqlDbType.Int32)).Value = oRegistroEN.oProveedorEN.idProveedor;
                Comando.Parameters.Add(new MySqlParameter("@idLaboratorio", MySqlDbType.Int32)).Value = oRegistroEN.oLaboratorioEN.idLaboratorio;                
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.IdUsuarioDeModificacion;


                Comando.ExecuteNonQuery();
                
                DescripcionDeOperacion = string.Format("El registro fue Actualizado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                //Agregamos la Transacción....
                TransaccionesEN oTran = InformacionDelaTransaccion(oRegistroEN, "Actualizar", "Actualizar Registro", "CORRECTO");
                oTransaccionesAD.Agregar(oTran, oDatos);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al actualizar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);

                //Agregamos la Transacción....
                TransaccionesEN oTran = InformacionDelaTransaccion(oRegistroEN, "Actualizar", "Actualizar Registro", "ERROR");
                oTransaccionesAD.Agregar(oTran, oDatos);

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

        public bool Eliminar(ProveedorLaboratorioEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from ProveedorLaboratorio Where idProveedorLaboratorio = @idProveedorLaboratorio;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProveedorLaboratorio", MySqlDbType.Int32)).Value = oRegistroEN.idProveedorLaboratorio;
                
                Comando.ExecuteNonQuery();

                DescripcionDeOperacion = string.Format("El registro fue Eliminado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                //Agregamos la Transacción....
                TransaccionesEN oTran = InformacionDelaTransaccion(oRegistroEN, "Eliminar", "Elminar Registro", "CORRECTO");
                oTransaccionesAD.Agregar(oTran, oDatos);

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al eliminar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);

                //Agregamos la Transacción....
                TransaccionesEN oTran = InformacionDelaTransaccion(oRegistroEN, "Eliminar", "Eliminar Registro", "ERROR");
                oTransaccionesAD.Agregar(oTran, oDatos);

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

        public bool Listado(ProveedorLaboratorioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select pc.idProveedorLaboratorio, pc.idProveedor, pc.idLaboratorio,
                pc.idUsuarioDeCreacion, pc.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                pc.idUsuarioModificacion, pc.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                from ProveedorLaboratorio as pc 
                inner join usuario as u on u.idUsuario = pc.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = pc.idUsuarioModificacion
                Where pc.idProveedorLaboratorio > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoDeProveedoresLaboratorio(ProveedorLaboratorioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "ListadoDeProveedoresLaboratorio";
                Comando.Parameters.Add(new MySqlParameter("@SWhere", MySqlDbType.VarChar, oRegistroEN.Where.Length)).Value = oRegistroEN.Where;

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

        public bool ListadoPorIdentificador(ProveedorLaboratorioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select pc.idProveedorLaboratorio, pc.idProveedor, pc.idLaboratorio,
                pc.idUsuarioDeCreacion, pc.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                pc.idUsuarioModificacion, pc.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                from ProveedorLaboratorio as pc 
                inner join usuario as u on u.idUsuario = pc.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = pc.idUsuarioModificacion
                Where pc.idProveedorLaboratorio = {0} ", oRegistroEN.idProveedorLaboratorio);
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

        public bool ListadoPorIdentificadorDelLaboratorio(ProveedorLaboratorioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select pc.idProveedorLaboratorio, pc.idProveedor, pc.idLaboratorio                
                from ProveedorLaboratorio as pc                 
                Where pc.idLaboratorio = {0} ", oRegistroEN.oLaboratorioEN.idLaboratorio);
                Comando.CommandText = Consultas;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                if(DT.Rows.Count > 0)
                {
                    oRegistroEN.idProveedorLaboratorio = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());
                    oRegistroEN.oProveedorEN.idProveedor = Convert.ToInt32(DT.Rows[0].ItemArray[1].ToString());
                }

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

        public bool ListadoPorID_LabortoriosInformacion(ProveedorLaboratorioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select pc.idProveedorLaboratorio, pc.idProveedor, pc.idLaboratorio, concat(p.Codigo, ' - ', p.Nombre) as 'Proveedor'                  
                from ProveedorLaboratorio as pc              
                inner join proveedor as p on p.idProveedor = pc.idProveedor
                Where pc.idLaboratorio = {0} ", oRegistroEN.oLaboratorioEN.idLaboratorio);
                Comando.CommandText = Consultas;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    oRegistroEN.idProveedorLaboratorio = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());
                    oRegistroEN.oProveedorEN.idProveedor = Convert.ToInt32(DT.Rows[0].ItemArray[1].ToString());
                }

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

        public bool ListadoPorIdentificadorDelProveedor(ProveedorLaboratorioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select pc.idProveedorLaboratorio, pc.idLaboratorio, Codigo, c.Nombre, c.NoRUC,Direccion, Telefono, Movil,
                Observaciones, Correo, SitioWeb, FechaDeCumpleanos, Messenger, Skype, 
                Twitter, Facebook, c.Estado
                from ProveedorLaboratorio pc 
                inner join Laboratorio as c on c.idLaboratorio = pc.idLaboratorio
                inner join usuario as u on u.idUsuario = c.idUsuarioDeCreacion
                left join usuario as c1 on c1.idUsuario = c.idUsuarioModificacion
                where pc.idProveedor = {0} ", oRegistroEN.oProveedorEN.idProveedor);

                //System.Diagnostics.Debug.Print(string.Format("Consulta: {0} {1} {0}", Environment.NewLine,Consultas));
                Comando.CommandText = Consultas;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    oRegistroEN.idProveedorLaboratorio = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());
                    oRegistroEN.oProveedorEN.idProveedor = Convert.ToInt32(DT.Rows[0].ItemArray[1].ToString());
                }

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

        public bool ListadoParaCombos(ProveedorLaboratorioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select pc.idProveedorLaboratorio, pc.idProveedor, pc.idLaboratorio,
                pc.idUsuarioDeCreacion, pc.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                pc.idUsuarioModificacion, pc.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                from ProveedorLaboratorio as pc 
                inner join usuario as u on u.idUsuario = pc.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = pc.idUsuarioModificacion
                Where pc.idProveedorLaboratorio > 0 {0} {1} ; ", oRegistroEN.Where, oRegistroEN.OrderBy);
                Comando.CommandText = Consultas;

                System.Diagnostics.Debug.Print("Consultas de Tipo de transaccion: " + Consultas);

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
               
        public bool ListadoParaReportes(ProveedorLaboratorioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select pc.idProveedorLaboratorio, pc.idProveedor, pc.idLaboratorio,
                pc.idUsuarioDeCreacion, pc.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                pc.idUsuarioModificacion, pc.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                from ProveedorLaboratorio as pc 
                inner join usuario as u on u.idUsuario = pc.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = pc.idUsuarioModificacion
                Where pc.idProveedorLaboratorio > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarSiElRegistroEstaVinculado(ProveedorLaboratorioEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "ValidarSiElRegistroEstaVinculado";

                Comando.Parameters.Add(new MySqlParameter("@CampoABuscar_", MySqlDbType.VarChar, 200)).Value = "idProveedorLaboratorio";
                Comando.Parameters.Add(new MySqlParameter("@ValorCampoABuscar", MySqlDbType.Int32)).Value = oRegistroEN.idProveedorLaboratorio;
                Comando.Parameters.Add(new MySqlParameter("@ExcluirTabla_", MySqlDbType.VarChar, 200)).Value = string.Empty;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                if (DT.Rows[0].ItemArray[0].ToString().ToUpper() == "NINGUNA".ToUpper())
                {
                    return false;
                }
                else
                {

                    this.Error = String.Format("La Operación: '{1}', {0} no se puede completar por que el registro: {0} '{2}', {0} se encuentra asociado con: {0} {3}",Environment.NewLine, TipoDeOperacion, InformacionDelRegistro(oRegistroEN), oTransaccionesAD.ConvertirValorDeLaCadena(DT.Rows[0].ItemArray[0].ToString()));
                    DescripcionDeOperacion = this.Error;

                    //Agregamos la Transacción....
                    TransaccionesEN oTran = InformacionDelaTransaccion(oRegistroEN, "VALIDAR", "VALIDAR SI EL REGISTRO ESTA VINCULADO", "CORRECTO");
                    oTransaccionesAD.Agregar(oTran, oDatos);

                    return true;
                }

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al validar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);

                //Agregamos la Transacción....
                TransaccionesEN oTran = InformacionDelaTransaccion(oRegistroEN, "VALIDAR", "VALIDAR SI EL REGISTRO ESTA VINCULADO", "ERROR");
                oTransaccionesAD.Agregar(oTran, oDatos);

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

        public bool ValidarRegistroDuplicado(ProveedorLaboratorioEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                switch (TipoDeOperacion.Trim().ToUpper()){

                    case "AGREGAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProveedorLaboratorio from ProveedorLaboratorio where idLaboratorio = @idLaboratorio and idProveedor = @idProveedor) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idLaboratorio", MySqlDbType.Int32)).Value = oRegistroEN.oLaboratorioEN.idLaboratorio;
                        Comando.Parameters.Add(new MySqlParameter("@idProveedor", MySqlDbType.Int32)).Value = oRegistroEN.oProveedorEN.idProveedor;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProveedorLaboratorio from ProveedorLaboratorio where idLaboratorio = @idLaboratorio and idProveedor = @idProveedor and idProveedorLaboratorio <> @idProveedorLaboratorio) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idLaboratorio", MySqlDbType.Int32)).Value = oRegistroEN.oLaboratorioEN.idLaboratorio;
                        Comando.Parameters.Add(new MySqlParameter("@idProveedor", MySqlDbType.Int32)).Value = oRegistroEN.oProveedorEN.idProveedor;
                        Comando.Parameters.Add(new MySqlParameter("@idProveedorLaboratorio", MySqlDbType.Int32)).Value = oRegistroEN.idProveedorLaboratorio;

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

                //Agregamos la Transacción....
                TransaccionesEN oTran = InformacionDelaTransaccion(oRegistroEN, "VALIDAR", "REGISTRO DUPLICADO DENTRO DE LA BASE DE DATOS", "ERROR");
                oTransaccionesAD.Agregar(oTran, oDatos);

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

        private TransaccionesEN InformacionDelaTransaccion(ProveedorLaboratorioEN oProveedorLaboratorio, String TipoDeOperacion, String Descripcion, String Estado)
        {
            TransaccionesEN oRegistroEN = new TransaccionesEN();

            oRegistroEN.idregistro = oProveedorLaboratorio.idProveedorLaboratorio;
            oRegistroEN.Modelo = "TransaccionAD";
            oRegistroEN.Modulo = "Transacciones";
            oRegistroEN.Tabla = "ProveedorLaboratorios";
            oRegistroEN.tipodeoperacion = TipoDeOperacion;
            oRegistroEN.Estado = Estado;
            oRegistroEN.ip = oProveedorLaboratorio.oLoginEN.NumeroIP;
            oRegistroEN.idusuario = oProveedorLaboratorio.oLoginEN.idUsuario;
            oRegistroEN.idusuarioaprueba = oProveedorLaboratorio.oLoginEN.idUsuario;
            oRegistroEN.descripciondelusuario = DescripcionDeOperacion;
            oRegistroEN.descripcioninterna = Descripcion;
            oRegistroEN.NombreDelEquipo = oProveedorLaboratorio.oLoginEN.NombreDelComputador;

            return oRegistroEN;
        }


        private string InformacionDelRegistro(ProveedorLaboratorioEN oRegistroEN) {
            string Cadena = @"idProveedorLaboratorio: {0}, idProveedor: {1}, idLaboratorio: {2}, idUsuarioDeCreacion: {3}, FechaDeCreacion: {4}, idUsuarioModificacion: {5}, FechaDeModificacion: {6}";
            Cadena = string.Format(Cadena, oRegistroEN.idProveedorLaboratorio, oRegistroEN.oProveedorEN.idProveedor, oRegistroEN.oLaboratorioEN.idLaboratorio, oRegistroEN.IdUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.IdUsuarioDeModificacion, oRegistroEN.FechaDeModificacion);
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
