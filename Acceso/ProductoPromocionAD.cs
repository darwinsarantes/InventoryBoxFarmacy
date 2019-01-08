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
    public class ProductoPromocionAD
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

        public bool Agregar(ProductoPromocionEN oRegistroEN, DatosDeConexionEN oDatos) {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.StoredProcedure;

                Consultas = @"AgregarPromocionDelProducto";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProducto_", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;
                Comando.Parameters.Add(new MySqlParameter("@PrecioDelProducto_", MySqlDbType.Decimal)).Value = oRegistroEN.PrecioDelProducto;
                Comando.Parameters.Add(new MySqlParameter("@FechaDeInicio_", MySqlDbType.DateTime)).Value = oRegistroEN.FechaDeInicio;
                Comando.Parameters.Add(new MySqlParameter("@FechaDeFinalizacion_", MySqlDbType.DateTime)).Value = oRegistroEN.FechaDeFinalizacion;
                Comando.Parameters.Add(new MySqlParameter("@Estado_", MySqlDbType.VarChar, oRegistroEN.Estado.Trim().Length)).Value = oRegistroEN.Estado.Trim();
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCreacion_", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioDeCreacion;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion_", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioModificacion;
                Comando.Parameters.Add(new MySqlParameter("@Descripcion_", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idProductoPromocion = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());
                
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
        
        public bool Actualizar(ProductoPromocionEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.StoredProcedure;

                Consultas = @"ActualizarPromocionDelProducto";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProductoPromocion_", MySqlDbType.Int32)).Value = oRegistroEN.idProductoPromocion;
                Comando.Parameters.Add(new MySqlParameter("@idProducto_", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;
                Comando.Parameters.Add(new MySqlParameter("@PrecioDelProducto_", MySqlDbType.Decimal)).Value = oRegistroEN.PrecioDelProducto;
                Comando.Parameters.Add(new MySqlParameter("@FechaDeInicio_", MySqlDbType.DateTime)).Value = oRegistroEN.FechaDeInicio;
                Comando.Parameters.Add(new MySqlParameter("@FechaDeFinalizacion_", MySqlDbType.DateTime)).Value = oRegistroEN.FechaDeFinalizacion;
                Comando.Parameters.Add(new MySqlParameter("@Estado_", MySqlDbType.VarChar, oRegistroEN.Estado.Trim().Length)).Value = oRegistroEN.Estado.Trim();                
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion_", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioModificacion;
                
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

        public bool Eliminar(ProductoPromocionEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from ProductoPromocion Where idProductoPromocion = @idProductoPromocion;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProductoPromocion", MySqlDbType.Int32)).Value = oRegistroEN.idProductoPromocion;
                
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

        public bool Listado(ProductoPromocionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"select idProductoPromocion, ppm.idProducto, ppm.PrecioDelProducto, 
                ppm.FechaDeInicio, ppm.FechaDeFinalizacion, ppm.Estado, 
                ppm.idUsuarioDeCreacion, ppm.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                ppm.idUsuarioModificacion, ppm.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                from productopromocion as ppm
                inner join usuario as u on u.idUsuario = ppm.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = ppm.idUsuarioModificacion
                Where idProductoPromocion > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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


        public bool ListadoPromocionesXProducto(ProductoPromocionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idProductoPromocion, idProducto, PrecioDelProducto, FechaDeInicio,
                 FechaDeFinalizacion, Estado, Descripcion
                from productopromocion 
                where idProductoPromocion > 0 and idProducto = {0} {1} ", oRegistroEN.oProductoEN.idProducto, oRegistroEN.OrderBy);
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


        public bool ListadoPorIdentificador(ProductoPromocionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"select idProductoPromocion, ppm.idProducto, ppm.PrecioDelProducto, 
                ppm.FechaDeInicio, ppm.FechaDeFinalizacion, ppm.Estado, 
                ppm.idUsuarioDeCreacion, ppm.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                ppm.idUsuarioModificacion, ppm.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                from productopromocion as ppm
                inner join usuario as u on u.idUsuario = ppm.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = ppm.idUsuarioModificacion
                Where idProductoPromocion = {0} ", oRegistroEN.idProductoPromocion);
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
       
        public bool ListadoParaReportes(ProductoPromocionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"select idProductoPromocion, ppm.idProducto, ppm.PrecioDelProducto, 
                ppm.FechaDeInicio, ppm.FechaDeFinalizacion, ppm.Estado, 
                ppm.idUsuarioDeCreacion, ppm.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                ppm.idUsuarioModificacion, ppm.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                from productopromocion as ppm
                inner join usuario as u on u.idUsuario = ppm.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = ppm.idUsuarioModificacion
                Where idProductoPromocion > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarSiElRegistroEstaVinculado(ProductoPromocionEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                Comando.Parameters.Add(new MySqlParameter("@CampoABuscar_", MySqlDbType.VarChar, 200)).Value = "idProductoPromocion";
                Comando.Parameters.Add(new MySqlParameter("@ValorCampoABuscar", MySqlDbType.Int32)).Value = oRegistroEN.idProductoPromocion;
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

        public bool ValidarRegistroDuplicado(ProductoPromocionEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProductoPromocion from ProductoPromocion where idProducto = @idProducto) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProductoPromocion from ProductoPromocion where idProducto = @idProducto and idProductoPromocion <> @idProductoPromocion) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;
                        Comando.Parameters.Add(new MySqlParameter("@idProductoPromocion", MySqlDbType.Int32)).Value = oRegistroEN.idProductoPromocion;

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

        public bool ValidarFechaDelRegistro(ProductoPromocionEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                switch (TipoDeOperacion.Trim().ToUpper())
                {

                    case "AGREGAR":

                        /*Consultas = @"SELECT CASE WHEN EXISTS(SELECT idProductoPromocion FROM productopromocion where (SoloFecha(FechaDeInicio) >= SoloFecha(@FechaDeInicio) and SoloFecha(FechaDeInicio) <= SoloFecha(@FechaDeFinalizacion)) 
or (SoloFecha(FechaDeFinalizacion) >= SoloFecha(@FechaDeInicio) and SoloFecha(FechaDeFinalizacion) <= SoloFecha(@FechaDeFinalizacion)) and idProducto = @idProducto) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;
                        Comando.Parameters.Add(new MySqlParameter("@FechaDeInicio", MySqlDbType.DateTime)).Value = oRegistroEN.FechaDeInicio;
                        Comando.Parameters.Add(new MySqlParameter("@FechaDeFinalizacion", MySqlDbType.DateTime)).Value = oRegistroEN.FechaDeFinalizacion;*/

                        Consultas = @"SELECT CASE WHEN EXISTS(SELECT idProductoPromocion FROM productopromocion where (@FechaDeInicio between FechaDeInicio and FechaDeFinalizacion) or
(@FechaDeFinalizacion between FechaDeInicio and FechaDeFinalizacion) and idProducto = @idProducto) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;
                        Comando.Parameters.Add(new MySqlParameter("@FechaDeInicio", MySqlDbType.DateTime)).Value = oRegistroEN.FechaDeInicio;
                        Comando.Parameters.Add(new MySqlParameter("@FechaDeFinalizacion", MySqlDbType.DateTime)).Value = oRegistroEN.FechaDeFinalizacion;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(SELECT idProductoPromocion FROM productopromocion where (SoloFecha(FechaDeInicio) >= SoloFecha(@FechaDeInicio) and SoloFecha(FechaDeInicio) <= SoloFecha(@FechaDeFinalizacion)) 
or (SoloFecha(FechaDeFinalizacion) >= SoloFecha(@FechaDeInicio) and SoloFecha(FechaDeFinalizacion) <= SoloFecha(@FechaDeFinalizacion)) and idProducto = @idProducto and idProductoPromocion <> @idProductoPromocion) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;
                        Comando.Parameters.Add(new MySqlParameter("@idProductoPromocion", MySqlDbType.Int32)).Value = oRegistroEN.idProductoPromocion;
                        Comando.Parameters.Add(new MySqlParameter("@FechaDeInicio", MySqlDbType.DateTime)).Value = oRegistroEN.FechaDeInicio;
                        Comando.Parameters.Add(new MySqlParameter("@FechaDeFinalizacion", MySqlDbType.DateTime)).Value = oRegistroEN.FechaDeFinalizacion;
                        break;

                    default:
                        throw new ArgumentException("La aperación solicitada no esta disponible");

                }

                Comando.CommandText = Consultas;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                if (Convert.ToInt32(DT.Rows[0]["RES"].ToString()) > 0)
                {

                    DescripcionDeOperacion = string.Format("Ya existe información de la Fecha del Registro dentro de nuestro sistema: {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));
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

        private TransaccionesEN InformacionDelaTransaccion(ProductoPromocionEN oProductoPromocion, String TipoDeOperacion, String Descripcion, String Estado)
        {
            TransaccionesEN oRegistroEN = new TransaccionesEN();

            oRegistroEN.idregistro = oProductoPromocion.idProductoPromocion;
            oRegistroEN.Modelo = "ProductoPromocionAD";
            oRegistroEN.Modulo = "Producto";
            oRegistroEN.Tabla = "ProductoPromocion";
            oRegistroEN.tipodeoperacion = TipoDeOperacion;
            oRegistroEN.Estado = Estado;
            oRegistroEN.ip = oProductoPromocion.oLoginEN.NumeroIP;
            oRegistroEN.idusuario = oProductoPromocion.oLoginEN.idUsuario;
            oRegistroEN.idusuarioaprueba = oProductoPromocion.oLoginEN.idUsuario;
            oRegistroEN.descripciondelusuario = DescripcionDeOperacion;
            oRegistroEN.descripcioninterna = Descripcion;
            oRegistroEN.NombreDelEquipo = oProductoPromocion.oLoginEN.NombreDelComputador;

            return oRegistroEN;
        }


        private string InformacionDelRegistro(ProductoPromocionEN oRegistroEN) {
            string Cadena = @"idProductoPromocion: {0}, idProducto: {1}, PrecioDelProducto: {2}, FechaDeInicio: {3}, FechaDeFinalizacion: {4}, Estado: {5}, idUsuarioDeCreacion: {6}, FechaDeCreacion: {7}, idUsuarioModificacion: {8}, FechaDeModificacion: {9}";
            Cadena = string.Format(Cadena, oRegistroEN.idProductoPromocion, oRegistroEN.oProductoEN.idProducto, oRegistroEN.PrecioDelProducto, oRegistroEN.FechaDeInicio, oRegistroEN.FechaDeFinalizacion, oRegistroEN.Estado, oRegistroEN.idUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.idUsuarioModificacion, oRegistroEN.FechaDeModificacion);
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
