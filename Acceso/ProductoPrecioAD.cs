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
    public class ProductoPrecioAD
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

        public bool Agregar(ProductoPrecioEN oRegistroEN, DatosDeConexionEN oDatos) {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"
                                
                INSERT INTO productoprecio
                (idProducto, Costo, PorcentajeDelPrecio1, PorcentajeDelPrecio2, 
                PorcentajeDelPrecio3, PorcentajeDelPrecio4, PorcentajeDelPrecio5, Precio1, 
                Precio2, Precio3, Precio4, Precio5, AplicarElIva, 
                idUsuarioDeCreacion, FechaDeCreacion, idUsuarioModificacion, FechaDeModificacion, 
                ValorDelIvaEnProcentaje, ValorDelIva)
                VALUES
                (@idProducto, @Costo, @PorcentajeDelPrecio1, @PorcentajeDelPrecio2, 
                @PorcentajeDelPrecio3, @PorcentajeDelPrecio4, @PorcentajeDelPrecio5, @Precio1, 
                @Precio2, @Precio3, @Precio4, @Precio5, @AplicarElIva, 
                @idUsuarioDeCreacion, current_timestamp(), @idUsuarioModificacion, current_timestamp(), 
                @ValorDelIvaEnProcentaje, @ValorDelIva)

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;
                
                Comando.Parameters.Add(new MySqlParameter("@ValorDelIva", MySqlDbType.Decimal)).Value = oRegistroEN.ValorDelIva;
                Comando.Parameters.Add(new MySqlParameter("@ValorDelIvaEnProcentaje", MySqlDbType.Decimal)).Value = oRegistroEN.ValorDelIvaEnProcentaje;
                Comando.Parameters.Add(new MySqlParameter("@AplicarElIva", MySqlDbType.Int32)).Value = oRegistroEN.AplicarElIva;
                Comando.Parameters.Add(new MySqlParameter("@Precio5", MySqlDbType.Decimal)).Value = oRegistroEN.Precio5;
                Comando.Parameters.Add(new MySqlParameter("@Precio4", MySqlDbType.Decimal)).Value = oRegistroEN.Precio4;
                Comando.Parameters.Add(new MySqlParameter("@Precio3", MySqlDbType.Decimal)).Value = oRegistroEN.Precio3;
                Comando.Parameters.Add(new MySqlParameter("@Precio2", MySqlDbType.Decimal)).Value = oRegistroEN.Precio2;
                Comando.Parameters.Add(new MySqlParameter("@Precio1", MySqlDbType.Decimal)).Value = oRegistroEN.Precio1;
                Comando.Parameters.Add(new MySqlParameter("@PorcentajeDelPrecio5", MySqlDbType.Decimal)).Value = oRegistroEN.PorcentajeDelPrecio5;
                Comando.Parameters.Add(new MySqlParameter("@PorcentajeDelPrecio4", MySqlDbType.Decimal)).Value = oRegistroEN.PorcentajeDelPrecio4;
                Comando.Parameters.Add(new MySqlParameter("@PorcentajeDelPrecio3", MySqlDbType.Decimal)).Value = oRegistroEN.PorcentajeDelPrecio3;
                Comando.Parameters.Add(new MySqlParameter("@PorcentajeDelPrecio2", MySqlDbType.Decimal)).Value = oRegistroEN.PorcentajeDelPrecio2;
                Comando.Parameters.Add(new MySqlParameter("@PorcentajeDelPrecio1", MySqlDbType.Decimal)).Value = oRegistroEN.PorcentajeDelPrecio1;
                Comando.Parameters.Add(new MySqlParameter("@Costo", MySqlDbType.Decimal)).Value = oRegistroEN.Costo;
                Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;
                
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioDeCreacion;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioModificacion;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idProductoPrecio = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());
                
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
        
        public bool Actualizar(ProductoPrecioEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"UPDATE productoprecio set
	                idProducto = @idProducto, Costo = @Costo, PorcentajeDelPrecio1 = @PorcentajeDelPrecio1, 
                    PorcentajeDelPrecio2 = @PorcentajeDelPrecio2, PorcentajeDelPrecio3 = @PorcentajeDelPrecio3, 
                    PorcentajeDelPrecio4 = @PorcentajeDelPrecio4, PorcentajeDelPrecio5 = @PorcentajeDelPrecio5, 
                    Precio1 = @Precio1, Precio2 = @Precio2, Precio3 = @Precio3, Precio4 = @Precio4, Precio5 = @Precio5, 
                    AplicarElIva = @AplicarElIva, idUsuarioModificacion = @idUsuarioModificacion, 
                    FechaDeModificacion = current_timestamp(), ValorDelIvaEnProcentaje = @ValorDelIvaEnProcentaje, 
                    ValorDelIva = @ValorDelIva
                where idProductoPrecio = @idProductoPrecio;"; 

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProductoPrecio", MySqlDbType.Int32)).Value = oRegistroEN.idProductoPrecio;
                Comando.Parameters.Add(new MySqlParameter("@ValorDelIva", MySqlDbType.Decimal)).Value = oRegistroEN.ValorDelIva;
                Comando.Parameters.Add(new MySqlParameter("@ValorDelIvaEnProcentaje", MySqlDbType.Decimal)).Value = oRegistroEN.ValorDelIvaEnProcentaje;
                Comando.Parameters.Add(new MySqlParameter("@AplicarElIva", MySqlDbType.Int32)).Value = oRegistroEN.AplicarElIva;
                Comando.Parameters.Add(new MySqlParameter("@Precio5", MySqlDbType.Decimal)).Value = oRegistroEN.Precio5;
                Comando.Parameters.Add(new MySqlParameter("@Precio4", MySqlDbType.Decimal)).Value = oRegistroEN.Precio4;
                Comando.Parameters.Add(new MySqlParameter("@Precio3", MySqlDbType.Decimal)).Value = oRegistroEN.Precio3;
                Comando.Parameters.Add(new MySqlParameter("@Precio2", MySqlDbType.Decimal)).Value = oRegistroEN.Precio2;
                Comando.Parameters.Add(new MySqlParameter("@Precio1", MySqlDbType.Decimal)).Value = oRegistroEN.Precio1;
                Comando.Parameters.Add(new MySqlParameter("@PorcentajeDelPrecio5", MySqlDbType.Decimal)).Value = oRegistroEN.PorcentajeDelPrecio5;
                Comando.Parameters.Add(new MySqlParameter("@PorcentajeDelPrecio4", MySqlDbType.Decimal)).Value = oRegistroEN.PorcentajeDelPrecio4;
                Comando.Parameters.Add(new MySqlParameter("@PorcentajeDelPrecio3", MySqlDbType.Decimal)).Value = oRegistroEN.PorcentajeDelPrecio3;
                Comando.Parameters.Add(new MySqlParameter("@PorcentajeDelPrecio2", MySqlDbType.Decimal)).Value = oRegistroEN.PorcentajeDelPrecio2;
                Comando.Parameters.Add(new MySqlParameter("@PorcentajeDelPrecio1", MySqlDbType.Decimal)).Value = oRegistroEN.PorcentajeDelPrecio1;
                Comando.Parameters.Add(new MySqlParameter("@Costo", MySqlDbType.Decimal)).Value = oRegistroEN.Costo;
                Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;
                
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

        public bool Eliminar(ProductoPrecioEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from ProductoPrecio Where idProductoPrecio = @idProductoPrecio;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProductoPrecio", MySqlDbType.Int32)).Value = oRegistroEN.idProductoPrecio;
                
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

        public bool Listado(ProductoPrecioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idProductoPrecio, ppc.idProducto, ppc.Costo, ppc.PorcentajeDelPrecio1, 
                ppc.PorcentajeDelPrecio2, ppc.PorcentajeDelPrecio3, ppc.PorcentajeDelPrecio4, 
                ppc.PorcentajeDelPrecio5, ppc.Precio1, ppc.Precio2, ppc.Precio3, ppc.Precio4, 
                ppc.Precio5, ppc.AplicarElIva, ppc.idUsuarioDeCreacion, ppc.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                ppc.idUsuarioModificacion, ppc.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion',
                ppc.ValorDelIvaEnProcentaje, ppc.ValorDelIva
                from productoprecio as ppc 
                inner join usuario as u on u.idUsuario = ppc.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = ppc.idUsuarioModificacion
                where idProductoPrecio > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoPorIdentificador(ProductoPrecioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idProductoPrecio, ppc.idProducto, ppc.Costo, ppc.PorcentajeDelPrecio1, 
                ppc.PorcentajeDelPrecio2, ppc.PorcentajeDelPrecio3, ppc.PorcentajeDelPrecio4, 
                ppc.PorcentajeDelPrecio5, ppc.Precio1, ppc.Precio2, ppc.Precio3, ppc.Precio4, 
                ppc.Precio5, ppc.AplicarElIva, ppc.idUsuarioDeCreacion, ppc.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                ppc.idUsuarioModificacion, ppc.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion',
                ppc.ValorDelIvaEnProcentaje, ppc.ValorDelIva
                from productoprecio as ppc 
                inner join usuario as u on u.idUsuario = ppc.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = ppc.idUsuarioModificacion
                where idProductoPrecio = {0} ", oRegistroEN.idProductoPrecio);
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
        
        public bool ListadoParaReportes(ProductoPrecioEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idProductoPrecio, ppc.idProducto, ppc.Costo, ppc.PorcentajeDelPrecio1, 
                ppc.PorcentajeDelPrecio2, ppc.PorcentajeDelPrecio3, ppc.PorcentajeDelPrecio4, 
                ppc.PorcentajeDelPrecio5, ppc.Precio1, ppc.Precio2, ppc.Precio3, ppc.Precio4, 
                ppc.Precio5, ppc.AplicarElIva, ppc.idUsuarioDeCreacion, ppc.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                ppc.idUsuarioModificacion, ppc.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion',
                ppc.ValorDelIvaEnProcentaje, ppc.ValorDelIva
                from productoprecio as ppc 
                inner join usuario as u on u.idUsuario = ppc.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = ppc.idUsuarioModificacion
                where idProductoPrecio > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarSiElRegistroEstaVinculado(ProductoPrecioEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                Comando.Parameters.Add(new MySqlParameter("@CampoABuscar_", MySqlDbType.VarChar, 200)).Value = "idProductoPrecio";
                Comando.Parameters.Add(new MySqlParameter("@ValorCampoABuscar", MySqlDbType.Int32)).Value = oRegistroEN.idProductoPrecio;
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

        public bool ValidarRegistroDuplicado(ProductoPrecioEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProductoPrecio from ProductoPrecio where idProducto = @idProducto) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProductoPrecio from ProductoPrecio where idProducto = @idProducto and idProductoPrecio <> @idProductoPrecio) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;
                        Comando.Parameters.Add(new MySqlParameter("@idProductoPrecio", MySqlDbType.Int32)).Value = oRegistroEN.idProductoPrecio;

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

        private TransaccionesEN InformacionDelaTransaccion(ProductoPrecioEN oProductoPrecio, String TipoDeOperacion, String Descripcion, String Estado)
        {
            TransaccionesEN oRegistroEN = new TransaccionesEN();

            oRegistroEN.idregistro = oProductoPrecio.idProductoPrecio;
            oRegistroEN.Modelo = "ProductoPreciosAD";
            oRegistroEN.Modulo = "Producto";
            oRegistroEN.Tabla = "ProductoPrecios";
            oRegistroEN.tipodeoperacion = TipoDeOperacion;
            oRegistroEN.Estado = Estado;
            oRegistroEN.ip = oProductoPrecio.oLoginEN.NumeroIP;
            oRegistroEN.idusuario = oProductoPrecio.oLoginEN.idUsuario;
            oRegistroEN.idusuarioaprueba = oProductoPrecio.oLoginEN.idUsuario;
            oRegistroEN.descripciondelusuario = DescripcionDeOperacion;
            oRegistroEN.descripcioninterna = Descripcion;
            oRegistroEN.NombreDelEquipo = oProductoPrecio.oLoginEN.NombreDelComputador;

            return oRegistroEN;
        }


        private string InformacionDelRegistro(ProductoPrecioEN oRegistroEN) {
            string Cadena = @"idProductoPrecio: {0}, idProducto: {1}, Costo: {2}, PorcentajeDelPrecio1: {3}, PorcentajeDelPrecio2: {4}, 
            PorcentajeDelPrecio3: {5}, PorcentajeDelPrecio4: {6}, PorcentajeDelPrecio5: {7}, Precio1: {8}, Precio2: {9}, Precio3: {10},
            Precio4: {11}, Precio5: {12}, AplicarElIva: {13}, idUsuarioDeCreacion: {14}, FechaDeCreacion: {15}, idUsuarioModificacion: {16}, 
            FechaDeModificacion: {17}, ValorDelIvaEnProcentaje: {18}, ValorDelIva: {20}";
            Cadena = string.Format(Cadena, oRegistroEN.idProductoPrecio, oRegistroEN.Costo, oRegistroEN.PorcentajeDelPrecio1, 
                oRegistroEN.PorcentajeDelPrecio2, oRegistroEN.PorcentajeDelPrecio3, oRegistroEN.PorcentajeDelPrecio4, oRegistroEN.PorcentajeDelPrecio5,
                oRegistroEN.Precio1, oRegistroEN.Precio2, oRegistroEN.Precio3, oRegistroEN.Precio4, oRegistroEN.Precio5,
                oRegistroEN.AplicarElIva,oRegistroEN.idUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, 
                oRegistroEN.idUsuarioModificacion, oRegistroEN.FechaDeModificacion, oRegistroEN.ValorDelIvaEnProcentaje, oRegistroEN.ValorDelIva);
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
