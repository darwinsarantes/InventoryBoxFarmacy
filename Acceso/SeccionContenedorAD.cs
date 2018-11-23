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
    public class SeccionContenedorAD
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

        public bool Agregar(SeccionContenedorEN oRegistroEN, DatosDeConexionEN oDatos) {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"
                                
                insert into seccioncontenedor
                (idSeccion, idContenedor, idUsuarioDeCreacion, FechaDeCreacion, idUsuarioModificacion, FechaDeModificacion)
                values
                (@idSeccion, @idContenedor, @idUsuarioDeCreacion, current_timestamp(), @idUsuarioModificacion, current_timestamp());

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;                               

                Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.oSeccionEN.idSeccion;
                Comando.Parameters.Add(new MySqlParameter("@idContenedor", MySqlDbType.Int32)).Value = oRegistroEN.oContenedorEN.idContenedor;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioDeCreacion;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioModificacion;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idSeccionContenedor = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());
                
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
        
        public bool Actualizar(SeccionContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"update seccioncontenedor set
	                idSeccion = @idSeccion, idContenedor= @idContenedor, idUsuarioModificacion= @idUsuarioModificacion, FechaDeModificacion = current_timestamp()
                where idSeccionContenedor = @idSeccionContenedor;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idSeccionContenedor", MySqlDbType.Int32)).Value = oRegistroEN.idSeccionContenedor;
                Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.oSeccionEN.idSeccion;
                Comando.Parameters.Add(new MySqlParameter("@idContenedor", MySqlDbType.Int32)).Value = oRegistroEN.oContenedorEN.idContenedor;                
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioModificacion;


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

        public bool Eliminar(SeccionContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from SeccionContenedor Where idSeccionContenedor = @idSeccionContenedor;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idSeccionContenedor", MySqlDbType.Int32)).Value = oRegistroEN.idSeccionContenedor;
                
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

        public bool Listado(SeccionContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select 
                scc.idSeccionContenedor, scc.idSeccion, scc.idContenedor, 
                sc.Codigo as 'CodigoDeSeccion', sc.Nombre as 'Seccion', sc.Descripcion as 'DescSeccion',
                cr.Codigo as 'CodigoDelContenedor', cr.Nombre as 'Contenedor', cr.Descripcion as 'DescContenedor',
                scc.idUsuarioDeCreacion, scc.FechaDeCreacion, 
                scc.idUsuarioModificacion, scc.FechaDeModificacion
                from seccioncontenedor  as scc
                inner join seccion as sc on sc.idSeccion = scc.idSeccion
                inner join contenedor as cr on cr.idContenedor = scc.idContenedor
                Where scc.idSeccionContenedor > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoPorIdentificador(SeccionContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select 
                scc.idSeccionContenedor, scc.idSeccion, scc.idContenedor, 
                sc.Codigo as 'CodigoDeSeccion', sc.Nombre as 'Seccion', sc.Descripcion as 'DescSeccion',
                cr.Codigo as 'CodigoDelContenedor', cr.Nombre as 'Contenedor', cr.Descripcion as 'DescContenedor',
                scc.idUsuarioDeCreacion, scc.FechaDeCreacion, 
                scc.idUsuarioModificacion, scc.FechaDeModificacion
                from seccioncontenedor  as scc
                inner join seccion as sc on sc.idSeccion = scc.idSeccion
                inner join contenedor as cr on cr.idContenedor = scc.idContenedor
                Where scc.idSeccionContenedor = {0} ", oRegistroEN.idSeccionContenedor);
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

        public bool ListadoPorIdDeSeccion(SeccionContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select 
                scc.idSeccionContenedor, scc.idSeccion, scc.idContenedor, 
                sc.Codigo as 'CodigoDeSeccion', sc.Nombre as 'Seccion', sc.Descripcion as 'DescSeccion',
                cr.Codigo as 'CodigoDelContenedor', cr.Nombre as 'Contenedor', cr.Descripcion as 'DescContenedor',
                scc.idUsuarioDeCreacion, scc.FechaDeCreacion, 
                scc.idUsuarioModificacion, scc.FechaDeModificacion
                from seccioncontenedor  as scc
                inner join seccion as sc on sc.idSeccion = scc.idSeccion
                inner join contenedor as cr on cr.idContenedor = scc.idContenedor
                Where scc.idSeccion ", oRegistroEN.oSeccionEN.idSeccion);
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

        public bool ListadoPorIdContendor(SeccionContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select 
                scc.idSeccionContenedor, scc.idSeccion, scc.idContenedor, 
                sc.Codigo as 'CodigoDeSeccion', sc.Nombre as 'Seccion', sc.Descripcion as 'DescSeccion',
                cr.Codigo as 'CodigoDelContenedor', cr.Nombre as 'Contenedor', cr.Descripcion as 'DescContenedor',
                scc.idUsuarioDeCreacion, scc.FechaDeCreacion, 
                scc.idUsuarioModificacion, scc.FechaDeModificacion
                from seccioncontenedor  as scc
                inner join seccion as sc on sc.idSeccion = scc.idSeccion
                inner join contenedor as cr on cr.idContenedor = scc.idContenedor
                Where scc.idContenedor > 0 ", oRegistroEN.oContenedorEN.idContenedor);
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
        
        public bool ListadoParaCombos(SeccionContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select 
                scc.idSeccionContenedor, scc.idSeccion, scc.idContenedor, 
                sc.Codigo as 'CodigoDeSeccion', sc.Nombre as 'Seccion', sc.Descripcion as 'DescSeccion',
                cr.Codigo as 'CodigoDelContenedor', cr.Nombre as 'Contenedor', cr.Descripcion as 'DescContenedor',
                scc.idUsuarioDeCreacion, scc.FechaDeCreacion, 
                scc.idUsuarioModificacion, scc.FechaDeModificacion
                from seccioncontenedor  as scc
                inner join seccion as sc on sc.idSeccion = scc.idSeccion
                inner join contenedor as cr on cr.idContenedor = scc.idContenedor
                Where scc.idSeccionContenedor > 0 {0} {1} ; ", oRegistroEN.Where, oRegistroEN.OrderBy);
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
               
        public bool ListadoParaReportes(SeccionContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select 
                scc.idSeccionContenedor, scc.idSeccion, scc.idContenedor, 
                sc.Codigo as 'CodigoDeSeccion', sc.Nombre as 'Seccion', sc.Descripcion as 'DescSeccion',
                cr.Codigo as 'CodigoDelContenedor', cr.Nombre as 'Contenedor', cr.Descripcion as 'DescContenedor',
                scc.idUsuarioDeCreacion, scc.FechaDeCreacion, 
                scc.idUsuarioModificacion, scc.FechaDeModificacion
                from seccioncontenedor  as scc
                inner join seccion as sc on sc.idSeccion = scc.idSeccion
                inner join contenedor as cr on cr.idContenedor = scc.idContenedor
                Where scc.idSeccionContenedor > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarSiElRegistroEstaVinculado(SeccionContenedorEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                Comando.Parameters.Add(new MySqlParameter("@CampoABuscar_", MySqlDbType.VarChar, 200)).Value = "idSeccionContenedor";
                Comando.Parameters.Add(new MySqlParameter("@ValorCampoABuscar", MySqlDbType.Int32)).Value = oRegistroEN.idSeccionContenedor;
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

        public bool ValidarRegistroDuplicado(SeccionContenedorEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idSeccionContenedor from SeccionContenedor where idSeccion = @idSeccion and idContenedor = @idContenedor) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.oSeccionEN.idSeccion;
                        Comando.Parameters.Add(new MySqlParameter("@idContenedor", MySqlDbType.Int32)).Value = oRegistroEN.oContenedorEN.idContenedor;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idSeccionContenedor from SeccionContenedor where idSeccion = @idSeccion and idContenedor = @idContenedor and idSeccionContenedor <> @idSeccionContenedor) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.oSeccionEN.idSeccion;
                        Comando.Parameters.Add(new MySqlParameter("@idContenedor", MySqlDbType.Int32)).Value = oRegistroEN.oContenedorEN.idContenedor;
                        Comando.Parameters.Add(new MySqlParameter("@idSeccionContenedor", MySqlDbType.Int32)).Value = oRegistroEN.idSeccionContenedor;

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

        private TransaccionesEN InformacionDelaTransaccion(SeccionContenedorEN oSeccionContenedor, String TipoDeOperacion, String Descripcion, String Estado)
        {
            TransaccionesEN oRegistroEN = new TransaccionesEN();

            oRegistroEN.idregistro = oSeccionContenedor.idSeccionContenedor;
            oRegistroEN.Modelo = "SeccionContenedorAD";
            oRegistroEN.Modulo = "Logistica del Producto";
            oRegistroEN.Tabla = "SeccionContenedor";
            oRegistroEN.tipodeoperacion = TipoDeOperacion;
            oRegistroEN.Estado = Estado;
            oRegistroEN.ip = oSeccionContenedor.oLoginEN.NumeroIP;
            oRegistroEN.idusuario = oSeccionContenedor.oLoginEN.idUsuario;
            oRegistroEN.idusuarioaprueba = oSeccionContenedor.oLoginEN.idUsuario;
            oRegistroEN.descripciondelusuario = DescripcionDeOperacion;
            oRegistroEN.descripcioninterna = Descripcion;
            oRegistroEN.NombreDelEquipo = oSeccionContenedor.oLoginEN.NombreDelComputador;

            return oRegistroEN;
        }


        private string InformacionDelRegistro(SeccionContenedorEN oRegistroEN) {
            string Cadena = @"idSeccionContenedor: {0}, idSeccion: {1}, idContenedor: {2}, idUsuarioDeCreacion: {3}, FechaDeCreacion: {4}, idUsuarioModificacion: {5}, FechaDeModificacion: {6}";
            Cadena = string.Format(Cadena, oRegistroEN.idSeccionContenedor, oRegistroEN.oSeccionEN.idSeccion, oRegistroEN.oContenedorEN.idContenedor, oRegistroEN.idUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.idUsuarioModificacion, oRegistroEN.FechaDeModificacion);
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
