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
    public class ProductoConfiguracionAD
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

        public bool Agregar(ProductoConfiguracionEN oRegistroEN, DatosDeConexionEN oDatos) {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"
                                
                INSERT INTO productoconfiguracion
                (idProducto, ActivarPromocion, AplicarComisiones, 
                MostrarContenidoDeObservacionesENFactura, MostrarImagenAlFacturar, 
                PreguntarNumeroDeSerieAlFacturar, PreguntarFechaDeVencimientoAlFacturar, 
                PreguntarPorResetaAlFacturar, NoUsarComisionesParaEsteProducto, 
                UsarComisionesDefinidasEnElregistroDelVendedor, MontoFijoPorVenta, 
                PorcentajeDeLaVenta, PorcentajeDeLaGanacia, Comision, ComisionMaxima, 
                idUsuarioDeCreacion, FechaDeCreacion, 
                idUsuarioModificacion, FechaDeModificacion)
                VALUES
                (@idProducto, @ActivarPromocion, @AplicarComisiones, 
                @MostrarContenidoDeObservacionesENFactura, @MostrarImagenAlFacturar, 
                @PreguntarNumeroDeSerieAlFacturar, @PreguntarFechaDeVencimientoAlFacturar, 
                @PreguntarPorResetaAlFacturar, @NoUsarComisionesParaEsteProducto, 
                @UsarComisionesDefinidasEnElregistroDelVendedor, @MontoFijoPorVenta, 
                @PorcentajeDeLaVenta, @PorcentajeDeLaGanacia, @Comision, @ComisionMaxima, 
                @idUsuarioDeCreacion, current_timestamp(), 
                @idUsuarioModificacion, current_timestamp());

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;
                Comando.Parameters.Add(new MySqlParameter("@ActivarPromocion", MySqlDbType.Int32)).Value = oRegistroEN.ActivarPromocion;
                Comando.Parameters.Add(new MySqlParameter("@AplicarComisiones", MySqlDbType.Int32)).Value = oRegistroEN.AplicarComisiones;
                Comando.Parameters.Add(new MySqlParameter("@MostrarImagenAlFacturar", MySqlDbType.Int32)).Value = oRegistroEN.MostrarImagenAlFacturar;
                Comando.Parameters.Add(new MySqlParameter("@MostrarContenidoDeObservacionesENFactura", MySqlDbType.Int32)).Value = oRegistroEN.MostrarContenidoDeObservacionesENFactura;
                Comando.Parameters.Add(new MySqlParameter("@PreguntarNumeroDeSerieAlFacturar", MySqlDbType.Int32)).Value = oRegistroEN.PreguntarNumeroDeSerieAlFacturar;
                Comando.Parameters.Add(new MySqlParameter("@PreguntarFechaDeVencimientoAlFacturar", MySqlDbType.Int32)).Value = oRegistroEN.PreguntarFechaDeVencimientoAlFacturar;
                Comando.Parameters.Add(new MySqlParameter("@PreguntarPorResetaAlFacturar", MySqlDbType.Int32)).Value = oRegistroEN.PreguntarPorResetaAlFacturar;
                Comando.Parameters.Add(new MySqlParameter("@NoUsarComisionesParaEsteProducto", MySqlDbType.Int32)).Value = oRegistroEN.NoUsarComisionesParaEsteProducto;
                Comando.Parameters.Add(new MySqlParameter("@UsarComisionesDefinidasEnElregistroDelVendedor", MySqlDbType.Int32)).Value = oRegistroEN.UsarComisionesDefinidasEnElregistroDelVendedor;
                Comando.Parameters.Add(new MySqlParameter("@MontoFijoPorVenta", MySqlDbType.Int32)).Value = oRegistroEN.MontoFijoPorVenta;
                Comando.Parameters.Add(new MySqlParameter("@PorcentajeDeLaVenta", MySqlDbType.Int32)).Value = oRegistroEN.PorcentajeDeLaVenta;
                Comando.Parameters.Add(new MySqlParameter("@PorcentajeDeLaGanacia", MySqlDbType.Int32)).Value = oRegistroEN.PorcentajeDeLaGanacia;
                Comando.Parameters.Add(new MySqlParameter("@Comision", MySqlDbType.Decimal)).Value = oRegistroEN.Comision;
                Comando.Parameters.Add(new MySqlParameter("@ComisionMaxima", MySqlDbType.Decimal)).Value = oRegistroEN.ComisionMaxima;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioDeCreacion;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioModificacion;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idProductoConfiguracion = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());
                
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
        
        public bool Actualizar(ProductoConfiguracionEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"UPDATE productoconfiguracion SET
	                idProducto = @idProducto, ActivarPromocion = @ActivarPromocion, AplicarComisiones = @AplicarComisiones, 
	                MostrarContenidoDeObservacionesENFactura = @MostrarContenidoDeObservacionesENFactura,
                    MostrarImagenAlFacturar = @MostrarImagenAlFacturar, 
	                PreguntarNumeroDeSerieAlFacturar = @PreguntarNumeroDeSerieAlFacturar, 
                    PreguntarFechaDeVencimientoAlFacturar = @PreguntarFechaDeVencimientoAlFacturar, 
	                PreguntarPorResetaAlFacturar =@PreguntarPorResetaAlFacturar, 
                    NoUsarComisionesParaEsteProducto = @NoUsarComisionesParaEsteProducto, 
	                UsarComisionesDefinidasEnElregistroDelVendedor = @UsarComisionesDefinidasEnElregistroDelVendedor,
                    MontoFijoPorVenta = @MontoFijoPorVenta, 
	                PorcentajeDeLaVenta = @PorcentajeDeLaVenta, 
                    PorcentajeDeLaGanacia = @PorcentajeDeLaGanacia, Comision = @Comision, ComisionMaxima = @ComisionMaxima, 	
	                idUsuarioModificacion = @idUsuarioModificacion, FechaDeModificacion = current_timestamp()
                WHERE idProductoConfiguracion = @idProductoConfiguracion;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProductoConfiguracion", MySqlDbType.Int32)).Value = oRegistroEN.idProductoConfiguracion;
                Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;
                Comando.Parameters.Add(new MySqlParameter("@ActivarPromocion", MySqlDbType.Int32)).Value = oRegistroEN.ActivarPromocion;
                Comando.Parameters.Add(new MySqlParameter("@AplicarComisiones", MySqlDbType.Int32)).Value = oRegistroEN.AplicarComisiones;
                Comando.Parameters.Add(new MySqlParameter("@MostrarImagenAlFacturar", MySqlDbType.Int32)).Value = oRegistroEN.MostrarImagenAlFacturar;
                Comando.Parameters.Add(new MySqlParameter("@MostrarContenidoDeObservacionesENFactura", MySqlDbType.Int32)).Value = oRegistroEN.MostrarContenidoDeObservacionesENFactura;
                Comando.Parameters.Add(new MySqlParameter("@PreguntarNumeroDeSerieAlFacturar", MySqlDbType.Int32)).Value = oRegistroEN.PreguntarNumeroDeSerieAlFacturar;
                Comando.Parameters.Add(new MySqlParameter("@PreguntarFechaDeVencimientoAlFacturar", MySqlDbType.Int32)).Value = oRegistroEN.PreguntarFechaDeVencimientoAlFacturar;
                Comando.Parameters.Add(new MySqlParameter("@PreguntarPorResetaAlFacturar", MySqlDbType.Int32)).Value = oRegistroEN.PreguntarPorResetaAlFacturar;
                Comando.Parameters.Add(new MySqlParameter("@NoUsarComisionesParaEsteProducto", MySqlDbType.Int32)).Value = oRegistroEN.NoUsarComisionesParaEsteProducto;
                Comando.Parameters.Add(new MySqlParameter("@UsarComisionesDefinidasEnElregistroDelVendedor", MySqlDbType.Int32)).Value = oRegistroEN.UsarComisionesDefinidasEnElregistroDelVendedor;
                Comando.Parameters.Add(new MySqlParameter("@MontoFijoPorVenta", MySqlDbType.Int32)).Value = oRegistroEN.MontoFijoPorVenta;
                Comando.Parameters.Add(new MySqlParameter("@PorcentajeDeLaVenta", MySqlDbType.Int32)).Value = oRegistroEN.PorcentajeDeLaVenta;
                Comando.Parameters.Add(new MySqlParameter("@PorcentajeDeLaGanacia", MySqlDbType.Int32)).Value = oRegistroEN.PorcentajeDeLaGanacia;
                Comando.Parameters.Add(new MySqlParameter("@Comision", MySqlDbType.Decimal)).Value = oRegistroEN.Comision;
                Comando.Parameters.Add(new MySqlParameter("@ComisionMaxima", MySqlDbType.Decimal)).Value = oRegistroEN.ComisionMaxima;                
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

        public bool Eliminar(ProductoConfiguracionEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from ProductoConfiguracion Where idProductoConfiguracion = @idProductoConfiguracion;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProductoConfiguracion", MySqlDbType.Int32)).Value = oRegistroEN.idProductoConfiguracion;
                
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

        public bool Listado(ProductoConfiguracionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT idProductoConfiguracion, pc.idProducto, pc.ActivarPromocion, 
                pc.AplicarComisiones, pc.MostrarContenidoDeObservacionesENFactura, 
                pc.MostrarImagenAlFacturar, pc.PreguntarNumeroDeSerieAlFacturar, 
                pc.PreguntarFechaDeVencimientoAlFacturar, pc.PreguntarPorResetaAlFacturar, 
                pc.NoUsarComisionesParaEsteProducto, pc.UsarComisionesDefinidasEnElregistroDelVendedor,
                pc.MontoFijoPorVenta, pc.PorcentajeDeLaVenta, pc.PorcentajeDeLaGanacia, pc.Comision, 
                pc.ComisionMaxima, 
                pc.idUsuarioDeCreacion, pc.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                pc.idUsuarioModificacion, pc.FechaDeModificacion , u1.Nombre as 'UsuarioDeModificacion'
                FROM productoconfiguracion as pc
                inner join usuario as u on u.idUsuario = pc.idUsuarioDeCreacion
                inner join usuario as u1 on u1.idUsuario = pc.idUsuarioModificacion
                Where idProductoConfiguracion > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoPorIdentificador(ProductoConfiguracionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT idProductoConfiguracion, pc.idProducto, pc.ActivarPromocion, 
                pc.AplicarComisiones, pc.MostrarContenidoDeObservacionesENFactura, 
                pc.MostrarImagenAlFacturar, pc.PreguntarNumeroDeSerieAlFacturar, 
                pc.PreguntarFechaDeVencimientoAlFacturar, pc.PreguntarPorResetaAlFacturar, 
                pc.NoUsarComisionesParaEsteProducto, pc.UsarComisionesDefinidasEnElregistroDelVendedor,
                pc.MontoFijoPorVenta, pc.PorcentajeDeLaVenta, pc.PorcentajeDeLaGanacia, pc.Comision, 
                pc.ComisionMaxima, 
                pc.idUsuarioDeCreacion, pc.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                pc.idUsuarioModificacion, pc.FechaDeModificacion , u1.Nombre as 'UsuarioDeModificacion'
                FROM productoconfiguracion as pc
                inner join usuario as u on u.idUsuario = pc.idUsuarioDeCreacion
                inner join usuario as u1 on u1.idUsuario = pc.idUsuarioModificacion
                Where idProductoConfiguracion = {0} ", oRegistroEN.idProductoConfiguracion);
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
       
        public bool ListadoParaReportes(ProductoConfiguracionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT idProductoConfiguracion, pc.idProducto, pc.ActivarPromocion, 
                pc.AplicarComisiones, pc.MostrarContenidoDeObservacionesENFactura, 
                pc.MostrarImagenAlFacturar, pc.PreguntarNumeroDeSerieAlFacturar, 
                pc.PreguntarFechaDeVencimientoAlFacturar, pc.PreguntarPorResetaAlFacturar, 
                pc.NoUsarComisionesParaEsteProducto, pc.UsarComisionesDefinidasEnElregistroDelVendedor,
                pc.MontoFijoPorVenta, pc.PorcentajeDeLaVenta, pc.PorcentajeDeLaGanacia, pc.Comision, 
                pc.ComisionMaxima, 
                pc.idUsuarioDeCreacion, pc.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                pc.idUsuarioModificacion, pc.FechaDeModificacion , u1.Nombre as 'UsuarioDeModificacion'
                FROM productoconfiguracion as pc
                inner join usuario as u on u.idUsuario = pc.idUsuarioDeCreacion
                inner join usuario as u1 on u1.idUsuario = pc.idUsuarioModificacion
                Where idProductoConfiguracion > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarSiElRegistroEstaVinculado(ProductoConfiguracionEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                Comando.Parameters.Add(new MySqlParameter("@CampoABuscar_", MySqlDbType.VarChar, 200)).Value = "idProductoConfiguracion";
                Comando.Parameters.Add(new MySqlParameter("@ValorCampoABuscar", MySqlDbType.Int32)).Value = oRegistroEN.idProductoConfiguracion;
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

        public bool ValidarRegistroDuplicado(ProductoConfiguracionEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProductoConfiguracion from ProductoConfiguracion where idProducto = @idProducto) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProductoConfiguracion from ProductoConfiguracion where idProducto = @idProducto and idProductoConfiguracion <> @idProductoConfiguracion) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;
                        Comando.Parameters.Add(new MySqlParameter("@idProductoConfiguracion", MySqlDbType.Int32)).Value = oRegistroEN.idProductoConfiguracion;

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

        private TransaccionesEN InformacionDelaTransaccion(ProductoConfiguracionEN oProductoConfiguracion, String TipoDeOperacion, String Descripcion, String Estado)
        {
            TransaccionesEN oRegistroEN = new TransaccionesEN();

            oRegistroEN.idregistro = oProductoConfiguracion.idProductoConfiguracion;
            oRegistroEN.Modelo = "ProductoConfiguracionAD";
            oRegistroEN.Modulo = "Producto";
            oRegistroEN.Tabla = "ProductoConfiguracion";
            oRegistroEN.tipodeoperacion = TipoDeOperacion;
            oRegistroEN.Estado = Estado;
            oRegistroEN.ip = oProductoConfiguracion.oLoginEN.NumeroIP;
            oRegistroEN.idusuario = oProductoConfiguracion.oLoginEN.idUsuario;
            oRegistroEN.idusuarioaprueba = oProductoConfiguracion.oLoginEN.idUsuario;
            oRegistroEN.descripciondelusuario = DescripcionDeOperacion;
            oRegistroEN.descripcioninterna = Descripcion;
            oRegistroEN.NombreDelEquipo = oProductoConfiguracion.oLoginEN.NombreDelComputador;

            return oRegistroEN;
        }


        private string InformacionDelRegistro(ProductoConfiguracionEN oRegistroEN) {
            string Cadena = @"idProductoConfiguracion: {0}, idProducto: {1}, ActivarPromocion: {2}, AplicarComisiones: {3}, MostrarContenidoDeObservacionesENFactura: {4}, MostrarImagenAlFacturar: {5}, PreguntarNumeroDeSerieAlFacturar: {6}, PreguntarFechaDeVencimientoAlFacturar: {7}, PreguntarPorResetaAlFacturar: {8}, NoUsarComisionesParaEsteProducto: {9}, UsarComisionesDefinidasEnElregistroDelVendedor: {10}, MontoFijoPorVenta: {11}, PorcentajeDeLaVenta: {12}, PorcentajeDeLaGanacia: {13}, Comision: {14}, ComisionMaxima: {15}, idUsuarioDeCreacion: {16}, FechaDeCreacion: {17}, idUsuarioModificacion: {18}, FechaDeModificacion: {19}";
            Cadena = string.Format(Cadena, oRegistroEN.idProductoConfiguracion, oRegistroEN.oProductoEN.idProducto, oRegistroEN.ActivarPromocion, oRegistroEN.AplicarComisiones, oRegistroEN.MostrarContenidoDeObservacionesENFactura, oRegistroEN.MostrarImagenAlFacturar,oRegistroEN.PreguntarNumeroDeSerieAlFacturar,oRegistroEN.PreguntarFechaDeVencimientoAlFacturar,oRegistroEN.PreguntarPorResetaAlFacturar, oRegistroEN.NoUsarComisionesParaEsteProducto, oRegistroEN.UsarComisionesDefinidasEnElregistroDelVendedor, oRegistroEN.MontoFijoPorVenta, oRegistroEN.PorcentajeDeLaVenta, oRegistroEN.PorcentajeDeLaGanacia, oRegistroEN.Comision, oRegistroEN.ComisionMaxima, oRegistroEN.idUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.idUsuarioModificacion, oRegistroEN.FechaDeModificacion);
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
