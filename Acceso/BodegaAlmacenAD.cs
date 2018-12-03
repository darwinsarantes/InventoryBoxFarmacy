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
    public class BodegaAlmacenAD
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

        public bool Agregar(BodegaAlmacenEN oRegistroEN, DatosDeConexionEN oDatos) {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"
                                
                INSERT INTO BodegaAlmacen
                (idBodega, idAlmacen, 
                idUsuarioDeCreacion, FechaDeCreacion, 
                idUsuarioModificacion, FechaDeModificacion)
                VALUES
                (@idBodega, @idAlmacen, 
                @idUsuarioDeCreacion, current_timestamp(), 
                @idUsuarioModificacion, current_timestamp());

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;                               

                Comando.Parameters.Add(new MySqlParameter("@idBodega", MySqlDbType.Int32)).Value = oRegistroEN.oBodegaEN.idBodega;
                Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.oAlmacenEN.idAlmacen;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioDeCreacion;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioModificacion;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idBodegaAlmacen = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());
                
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
        
        public bool Actualizar(BodegaAlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"UPDATE BodegaAlmacen SET
	                idBodega = @idBodega, idAlmacen = @idAlmacen, 	
	                idUsuarioModificacion = @idUsuarioModificacion, FechaDeModificacion = current_timestamp()
                WHERE idBodegaAlmacen = @idBodegaAlmacen ;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idBodegaAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.idBodegaAlmacen;
                Comando.Parameters.Add(new MySqlParameter("@idBodega", MySqlDbType.Int32)).Value = oRegistroEN.oBodegaEN.idBodega;
                Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.oAlmacenEN.idAlmacen;                
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

        public bool Eliminar(BodegaAlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from BodegaAlmacen Where idBodegaAlmacen = @idBodegaAlmacen;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idBodegaAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.idBodegaAlmacen;
                
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

        public bool Listado(BodegaAlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select ba.idBodegaAlmacen, ba.idAlmacen, ba.idBodega, 
                al.Codigo as 'CodigoAlmacen', al.Nombre as 'Almacen', 
                al.Descripcion as 'DescAlmacen', al.PorDefecto,
                bg.Codigo as 'CodigoBodega', bg.Nombre as 'Bodega', 
                bg.Descripcion as 'DescBodega', bg.PorDefectoParaFacturacion,
                ba.idUsuarioDeCreacion, ba.FechaDeCreacion, 
                ba.idUsuarioModificacion, ba.FechaDeModificacion
                from bodegaalmacen as ba
                inner join bodega as bg on bg.idBodega = ba.idBodega
                inner join almacen as al on al.idAlmacen = ba.idAlmacen
                where ba.idBodegaAlmacen > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoDeBodegasPorAlmacen(BodegaAlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select ba.idBodegaAlmacen, ba.idAlmacen, ba.idBodega, 
                bg.Codigo, bg.Nombre, bg.Descripcion, bg.PorDefectoParaFacturacion,
                ba.idUsuarioDeCreacion, ba.FechaDeCreacion, 
                ba.idUsuarioModificacion, ba.FechaDeModificacion
                from bodegaalmacen as ba
                inner join bodega as bg on bg.idBodega = ba.idBodega
                inner join almacen as al on al.idAlmacen = ba.idAlmacen
                where ba.idBodegaAlmacen > 0 and ba.idAlmacen = {0} ", oRegistroEN.oAlmacenEN.idAlmacen);
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

        public bool ListadoDeAlmacenesPorBodega(BodegaAlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select ba.idBodegaAlmacen, ba.idAlmacen, ba.idBodega, 
                al.Codigo, al.Nombre, al.Descripcion, al.PorDefecto,
                ba.idUsuarioDeCreacion, ba.FechaDeCreacion, 
                ba.idUsuarioModificacion, ba.FechaDeModificacion
                from bodegaalmacen as ba
                inner join bodega as bg on bg.idBodega = ba.idBodega
                inner join almacen as al on al.idAlmacen = ba.idAlmacen
                where ba.idBodegaAlmacen > 0 and ba.idBodega = {0} ", oRegistroEN.oBodegaEN.idBodega);
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

        public bool ListadoPorIdentificador(BodegaAlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select ba.idBodegaAlmacen, ba.idAlmacen, ba.idBodega, 
                al.Codigo as 'CodigoAlmacen', al.Nombre as 'Almacen', 
                al.Descripcion as 'DescAlmacen', al.PorDefecto,
                bg.Codigo as 'CodigoBodega', bg.Nombre as 'Bodega', 
                bg.Descripcion as 'DescBodega', bg.PorDefectoParaFacturacion,
                ba.idUsuarioDeCreacion, ba.FechaDeCreacion, 
                ba.idUsuarioModificacion, ba.FechaDeModificacion
                from bodegaalmacen as ba
                inner join bodega as bg on bg.idBodega = ba.idBodega
                inner join almacen as al on al.idAlmacen = ba.idAlmacen
                where ba.idBodegaAlmacen = {0} ", oRegistroEN.idBodegaAlmacen);
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

        public bool ListadoParaCombos(BodegaAlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select ba.idBodegaAlmacen, ba.idAlmacen, ba.idBodega, 
                al.Codigo as 'CodigoAlmacen', al.Nombre as 'Almacen', 
                al.Descripcion as 'DescAlmacen', al.PorDefecto,
                bg.Codigo as 'CodigoBodega', bg.Nombre as 'Bodega', 
                bg.Descripcion as 'DescBodega', bg.PorDefectoParaFacturacion,
                ba.idUsuarioDeCreacion, ba.FechaDeCreacion, 
                ba.idUsuarioModificacion, ba.FechaDeModificacion
                from bodegaalmacen as ba
                inner join bodega as bg on bg.idBodega = ba.idBodega
                inner join almacen as al on al.idAlmacen = ba.idAlmacen
                where ba.idBodegaAlmacen > 0 {0} {1} ; ", oRegistroEN.Where, oRegistroEN.OrderBy);
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
               
        public bool ListadoParaReportes(BodegaAlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select ba.idBodegaAlmacen, ba.idAlmacen, ba.idBodega, 
                al.Codigo as 'CodigoAlmacen', al.Nombre as 'Almacen', 
                al.Descripcion as 'DescAlmacen', al.PorDefecto,
                bg.Codigo as 'CodigoBodega', bg.Nombre as 'Bodega', 
                bg.Descripcion as 'DescBodega', bg.PorDefectoParaFacturacion,
                ba.idUsuarioDeCreacion, ba.FechaDeCreacion, 
                ba.idUsuarioModificacion, ba.FechaDeModificacion
                from bodegaalmacen as ba
                inner join bodega as bg on bg.idBodega = ba.idBodega
                inner join almacen as al on al.idAlmacen = ba.idAlmacen
                where ba.idBodegaAlmacen > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarSiElRegistroEstaVinculado(BodegaAlmacenEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                Comando.Parameters.Add(new MySqlParameter("@CampoABuscar_", MySqlDbType.VarChar, 200)).Value = "idBodegaAlmacen";
                Comando.Parameters.Add(new MySqlParameter("@ValorCampoABuscar", MySqlDbType.Int32)).Value = oRegistroEN.idBodegaAlmacen;
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

        public bool ValidarRegistroDuplicado(BodegaAlmacenEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idBodegaAlmacen from almacenubicacion Where idBodega = @idBodega and idAlmacen = @idAlmacen) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idBodega", MySqlDbType.Int32)).Value = oRegistroEN.oBodegaEN.idBodega;
                        Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.oAlmacenEN.idAlmacen;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idBodegaAlmacen from almacenubicacion Where idBodega = @idBodega and idAlmacen = @idAlmacen and idBodegaAlmacen <> @idBodegaAlmacen) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idBodega", MySqlDbType.Int32)).Value = oRegistroEN.oBodegaEN.idBodega;
                        Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.oAlmacenEN.idAlmacen;
                        Comando.Parameters.Add(new MySqlParameter("@idBodegaAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.idBodegaAlmacen;

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

        private TransaccionesEN InformacionDelaTransaccion(BodegaAlmacenEN oBodegaAlmacen, String TipoDeOperacion, String Descripcion, String Estado)
        {
            TransaccionesEN oRegistroEN = new TransaccionesEN();

            oRegistroEN.idregistro = oBodegaAlmacen.idBodegaAlmacen;
            oRegistroEN.Modelo = "BodegaAlmacensAD";
            oRegistroEN.Modulo = "Ubicacion";
            oRegistroEN.Tabla = "BodegaAlmacen";
            oRegistroEN.tipodeoperacion = TipoDeOperacion;
            oRegistroEN.Estado = Estado;
            oRegistroEN.ip = oBodegaAlmacen.oLoginEN.NumeroIP;
            oRegistroEN.idusuario = oBodegaAlmacen.oLoginEN.idUsuario;
            oRegistroEN.idusuarioaprueba = oBodegaAlmacen.oLoginEN.idUsuario;
            oRegistroEN.descripciondelusuario = DescripcionDeOperacion;
            oRegistroEN.descripcioninterna = Descripcion;
            oRegistroEN.NombreDelEquipo = oBodegaAlmacen.oLoginEN.NombreDelComputador;

            return oRegistroEN;
        }


        private string InformacionDelRegistro(BodegaAlmacenEN oRegistroEN) {
            string Cadena = @"idBodegaAlmacen: {0}, idBodega: {1}, idAlmacen: {2}";
            Cadena = string.Format(Cadena, oRegistroEN.idBodegaAlmacen, oRegistroEN.oBodegaEN.idBodega, oRegistroEN.oAlmacenEN.idAlmacen);
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
