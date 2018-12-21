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
    public class ProductoAD
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

        public bool Agregar(ProductoEN oRegistroEN, DatosDeConexionEN oDatos) {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"
                                
                insert into producto
                (Codigo, CodigoDeBarra, Nombre, NombreGenerico, NombreComun, 
                Descripcion, Observaciones, Existencias, Minimo, Maximo, 
                idProductoUnidadDeMedida, idProductoPresentacion, idCategoria, 
                idUsuarioDeCreacion, FechaDeCreacion, idUsuarioModificacion, FechaDeModificacion, 
                idAlmacenEntidad, idPLEntidad)
                values
                (@Codigo, @CodigoDeBarra, @Nombre, @NombreGenerico, @NombreComun, 
                @Descripcion, @Observaciones, @Existencias, @Minimo, @Maximo, 
                @idProductoUnidadDeMedida, @idProductoPresentacion, @idCategoria, 
                @idUsuarioDeCreacion, current_timestamp(), @idUsuarioModificacion, current_timestamp(), 
                @idAlmacenEntidad, @idPLEntidad);

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@CodigoDeBarra", MySqlDbType.VarChar, oRegistroEN.CodigoDeBarra.Trim().Length)).Value = oRegistroEN.CodigoDeBarra.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@NombreGenerico", MySqlDbType.VarChar, oRegistroEN.NombreGenerico.Trim().Length)).Value = oRegistroEN.NombreGenerico.Trim();
                Comando.Parameters.Add(new MySqlParameter("@NombreComun", MySqlDbType.VarChar, oRegistroEN.NombreComun.Trim().Length)).Value = oRegistroEN.NombreComun.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Observaciones", MySqlDbType.VarChar, oRegistroEN.Observaciones.Trim().Length)).Value = oRegistroEN.Observaciones.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Existencias", MySqlDbType.Decimal)).Value = oRegistroEN.Existencias;
                Comando.Parameters.Add(new MySqlParameter("@Minimo", MySqlDbType.Decimal)).Value = oRegistroEN.Minimo;
                Comando.Parameters.Add(new MySqlParameter("@Maximo", MySqlDbType.Decimal)).Value = oRegistroEN.Maximo;
                Comando.Parameters.Add(new MySqlParameter("@idProductoUnidadDeMedida", MySqlDbType.Int32)).Value = oRegistroEN.oUnidadDeMedida.idProductoUnidadDeMedida;
                Comando.Parameters.Add(new MySqlParameter("@idProductoPresentacion", MySqlDbType.Int32)).Value = oRegistroEN.oPresentacion.idProductoPresentacion;
                Comando.Parameters.Add(new MySqlParameter("@idCategoria", MySqlDbType.Int32)).Value = oRegistroEN.oCategoria.idCategoria;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioDeCreacion;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioModificacion;
                Comando.Parameters.Add(new MySqlParameter("@idAlmacenEntidad", MySqlDbType.Int32)).Value = oRegistroEN.idAlmacenEntidad;
                Comando.Parameters.Add(new MySqlParameter("@idPLEntidad", MySqlDbType.Int32)).Value = oRegistroEN.idPLEntidad;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idProducto = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());
                
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

        public bool Agregar(ProductoEN oRegistroEN, DatosDeConexionEN oDatos, ref MySqlConnection Cnn_Existente, ref MySqlTransaction Transaccion_Existente)
        {

            oTransaccionesAD = new TransaccionesAD();

            try
            {
                
                Comando = new MySqlCommand();
                Comando.Connection = Cnn_Existente;
                Comando.Transaction = Transaccion_Existente;
                Comando.CommandType = CommandType.Text;

                Consultas = @"
                                
                insert into producto 
                (Codigo, CodigoDeBarra, Nombre, NombreGenerico, NombreComun, 
                Descripcion, Observaciones, Existencias, Minimo, Maximo, 
                idProductoUnidadDeMedida, idProductoPresentacion, idCategoria, 
                idUsuarioDeCreacion, FechaDeCreacion, idUsuarioModificacion, FechaDeModificacion, 
                idAlmacenEntidad, idPLEntidad, TablaDeReferenciaDeAlmacenaje, TablaDeRefereciaDeProveedorOLaboratorio, Estado)
                values
                (GenerarCodigoDelProducto(), @CodigoDeBarra, @Nombre, @NombreGenerico, @NombreComun, 
                @Descripcion, @Observaciones, @Existencias, @Minimo, @Maximo, 
                @idProductoUnidadDeMedida, @idProductoPresentacion, @idCategoria, 
                @idUsuarioDeCreacion, current_timestamp(), @idUsuarioModificacion, current_timestamp(), 
                @idAlmacenEntidad, @idPLEntidad, @TablaDeReferenciaDeAlmacenaje, @TablaDeRefereciaDeProveedorOLaboratorio, @Estado);

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;
                                
                Comando.Parameters.Add(new MySqlParameter("@CodigoDeBarra", MySqlDbType.VarChar, oRegistroEN.CodigoDeBarra.Trim().Length)).Value = oRegistroEN.CodigoDeBarra.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@NombreGenerico", MySqlDbType.VarChar, oRegistroEN.NombreGenerico.Trim().Length)).Value = oRegistroEN.NombreGenerico.Trim();
                Comando.Parameters.Add(new MySqlParameter("@NombreComun", MySqlDbType.VarChar, oRegistroEN.NombreComun.Trim().Length)).Value = oRegistroEN.NombreComun.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Observaciones", MySqlDbType.VarChar, oRegistroEN.Observaciones.Trim().Length)).Value = oRegistroEN.Observaciones.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Existencias", MySqlDbType.Decimal)).Value = oRegistroEN.Existencias;
                Comando.Parameters.Add(new MySqlParameter("@Minimo", MySqlDbType.Decimal)).Value = oRegistroEN.Minimo;
                Comando.Parameters.Add(new MySqlParameter("@Maximo", MySqlDbType.Decimal)).Value = oRegistroEN.Maximo;
                Comando.Parameters.Add(new MySqlParameter("@idProductoUnidadDeMedida", MySqlDbType.Int32)).Value = oRegistroEN.oUnidadDeMedida.idProductoUnidadDeMedida;
                Comando.Parameters.Add(new MySqlParameter("@idProductoPresentacion", MySqlDbType.Int32)).Value = oRegistroEN.oPresentacion.idProductoPresentacion;
                Comando.Parameters.Add(new MySqlParameter("@idCategoria", MySqlDbType.Int32)).Value = oRegistroEN.oCategoria.idCategoria;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioDeCreacion;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioModificacion;
                Comando.Parameters.Add(new MySqlParameter("@idAlmacenEntidad", MySqlDbType.Int32)).Value = oRegistroEN.idAlmacenEntidad;
                Comando.Parameters.Add(new MySqlParameter("@idPLEntidad", MySqlDbType.Int32)).Value = oRegistroEN.idPLEntidad;
                Comando.Parameters.Add(new MySqlParameter("@TablaDeReferenciaDeAlmacenaje", MySqlDbType.VarChar, oRegistroEN.TablaDeReferenciaDeAlmacenaje.Length)).Value = oRegistroEN.TablaDeReferenciaDeAlmacenaje.Trim();
                Comando.Parameters.Add(new MySqlParameter("@TablaDeRefereciaDeProveedorOLaboratorio", MySqlDbType.VarChar, oRegistroEN.TablaDeRefereciaDeProveedorOLaboratorio.Length)).Value = oRegistroEN.TablaDeRefereciaDeProveedorOLaboratorio.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Estado", MySqlDbType.VarChar, oRegistroEN.Estado.Length)).Value = oRegistroEN.Estado.Trim();

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idProducto = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());

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
            finally
            {                                
                Comando = null;
                Adaptador = null;
                oTransaccionesAD = null;

            }

        }

        public bool AgregarUtilizandoLaMismaConexion(ProductoCompletoEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            oTransaccionesAD = new TransaccionesAD();
            Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
            Cnn.Open();

            MySqlTransaction oMySqlTransaction;
            oMySqlTransaction = Cnn.BeginTransaction();

            try
            {

                String mensaje = "";
                string Errores = string.Empty;
                  
                if (Agregar(oRegistroEN.oProductoEN, oDatos, ref Cnn, ref oMySqlTransaction))
                {
                    Errores = EvaluarTextoError(Errores, "GUARDAR", this.Error);
                }
                else
                {
                    mensaje = String.Format("Error : '{1}', {0} producido al intentar guardar la información del producto. ", Environment.NewLine, this.Error);
                    throw new System.ArgumentException(mensaje);
                }

                ProductoConfiguracionAD oConfiguracionAD = new ProductoConfiguracionAD();
                oRegistroEN.oConfiguracionEN.oProductoEN = oRegistroEN.oProductoEN;

                if (oConfiguracionAD.Agregar(oRegistroEN.oConfiguracionEN, oDatos, ref Cnn, ref oMySqlTransaction))
                {
                    Errores = EvaluarTextoError(Errores, "GUARDAR", this.Error);
                }
                else
                {
                    mensaje = String.Format("Error : '{1}', {0} producido al intentar guardar la información de configuración del producto. ", Environment.NewLine, this.Error);
                    throw new System.ArgumentException(mensaje);
                }

                ProductoPromocionAD oPromocionAD = new ProductoPromocionAD();
                oRegistroEN.oPromocionEN.oProductoEN = oRegistroEN.oProductoEN;

                if (oPromocionAD.Agregar(oRegistroEN.oPromocionEN, oDatos, ref Cnn, ref oMySqlTransaction))
                {
                    Errores = EvaluarTextoError(Errores, "GUARDAR", this.Error);
                }
                else
                {
                    mensaje = String.Format("Error : '{1}', {0} producido al intentar guardar la información de la promoción del producto. ", Environment.NewLine, this.Error);
                    throw new System.ArgumentException(mensaje);
                }

                ProductoPrecioAD oPrecioAD = new ProductoPrecioAD();
                oRegistroEN.oPrecioEN.oProductoEN = oRegistroEN.oProductoEN;

                if (oPrecioAD.Agregar(oRegistroEN.oPrecioEN, oDatos, ref Cnn, ref oMySqlTransaction))
                {
                    Errores = EvaluarTextoError(Errores, "GUARDAR", this.Error);
                }
                else
                {
                    mensaje = String.Format("Error : '{1}', {0} producido al intentar guardar la información del precio del producto. ", Environment.NewLine, this.Error);
                    throw new System.ArgumentException(mensaje);
                }

                oMySqlTransaction.Commit();

                oConfiguracionAD = null;
                oPromocionAD = null;

                this.Error = Errores;
                
                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
                oMySqlTransaction.Rollback();

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al insertar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN.oProductoEN), ex.Message);
                //Agregamos la Transacción....
                TransaccionesEN oTran = InformacionDelaTransaccion(oRegistroEN.oProductoEN, "Agregar", "Agregar Nuevo Registro", "ERROR");
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

        public bool Actualizar(ProductoEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"update producto set
	                Codigo = @Codigo, CodigoDeBarra = @CodigoDeBarra, 
                    Nombre = @Nombre, NombreGenerico = @NombreGenerico, 
	                NombreComun = @NombreComun, Descripcion = @Descripcion, 
                    Observaciones = @Observaciones, Existencias = @Existencias,
                    Minimo = @Minimo, Maximo = @Maximo, 
                idProductoUnidadDeMedida = @idProductoUnidadDeMedida, 
                idProductoPresentacion = @idProductoPresentacion, idCategoria = @idCategoria, 
                idUsuarioModificacion = @idUsuarioModificacion, FechaDeModificacion = current_timestamp()
                where idProducto = @idProducto;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.idProducto;
                Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@CodigoDeBarra", MySqlDbType.VarChar, oRegistroEN.CodigoDeBarra.Trim().Length)).Value = oRegistroEN.CodigoDeBarra.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@NombreGenerico", MySqlDbType.VarChar, oRegistroEN.NombreGenerico.Trim().Length)).Value = oRegistroEN.NombreGenerico.Trim();
                Comando.Parameters.Add(new MySqlParameter("@NombreComun", MySqlDbType.VarChar, oRegistroEN.NombreComun.Trim().Length)).Value = oRegistroEN.NombreComun.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Observaciones", MySqlDbType.VarChar, oRegistroEN.Observaciones.Trim().Length)).Value = oRegistroEN.Observaciones.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Existencias", MySqlDbType.Decimal)).Value = oRegistroEN.Existencias;
                Comando.Parameters.Add(new MySqlParameter("@Minimo", MySqlDbType.Decimal)).Value = oRegistroEN.Minimo;
                Comando.Parameters.Add(new MySqlParameter("@Maximo", MySqlDbType.Decimal)).Value = oRegistroEN.Maximo;
                Comando.Parameters.Add(new MySqlParameter("@idProductoUnidadDeMedida", MySqlDbType.Int32)).Value = oRegistroEN.oUnidadDeMedida.idProductoUnidadDeMedida;
                Comando.Parameters.Add(new MySqlParameter("@idProductoPresentacion", MySqlDbType.Int32)).Value = oRegistroEN.oPresentacion.idProductoPresentacion;
                Comando.Parameters.Add(new MySqlParameter("@idCategoria", MySqlDbType.Int32)).Value = oRegistroEN.oCategoria.idCategoria;                
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

        public bool Actualizar(ProductoEN oRegistroEN, DatosDeConexionEN oDatos, ref MySqlConnection Cnn_Existente, ref MySqlTransaction Transaccion_Existente)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Comando = new MySqlCommand();
                Comando.Connection = Cnn_Existente;
                Comando.Transaction = Transaccion_Existente;
                Comando.CommandType = CommandType.Text;

                Consultas = @"update producto set
	                Codigo = @Codigo, CodigoDeBarra = @CodigoDeBarra, 
                    Nombre = @Nombre, NombreGenerico = @NombreGenerico, 
	                NombreComun = @NombreComun, Descripcion = @Descripcion, 
                    Observaciones = @Observaciones, Existencias = @Existencias,
                    Minimo = @Minimo, Maximo = @Maximo, 
                idProductoUnidadDeMedida = @idProductoUnidadDeMedida, 
                idProductoPresentacion = @idProductoPresentacion, idCategoria = @idCategoria, 
                idUsuarioModificacion = @idUsuarioModificacion, FechaDeModificacion = current_timestamp()
                where idProducto = @idProducto;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.idProducto;
                Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@CodigoDeBarra", MySqlDbType.VarChar, oRegistroEN.CodigoDeBarra.Trim().Length)).Value = oRegistroEN.CodigoDeBarra.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@NombreGenerico", MySqlDbType.VarChar, oRegistroEN.NombreGenerico.Trim().Length)).Value = oRegistroEN.NombreGenerico.Trim();
                Comando.Parameters.Add(new MySqlParameter("@NombreComun", MySqlDbType.VarChar, oRegistroEN.NombreComun.Trim().Length)).Value = oRegistroEN.NombreComun.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Observaciones", MySqlDbType.VarChar, oRegistroEN.Observaciones.Trim().Length)).Value = oRegistroEN.Observaciones.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Existencias", MySqlDbType.Decimal)).Value = oRegistroEN.Existencias;
                Comando.Parameters.Add(new MySqlParameter("@Minimo", MySqlDbType.Decimal)).Value = oRegistroEN.Minimo;
                Comando.Parameters.Add(new MySqlParameter("@Maximo", MySqlDbType.Decimal)).Value = oRegistroEN.Maximo;
                Comando.Parameters.Add(new MySqlParameter("@idProductoUnidadDeMedida", MySqlDbType.Int32)).Value = oRegistroEN.oUnidadDeMedida.idProductoUnidadDeMedida;
                Comando.Parameters.Add(new MySqlParameter("@idProductoPresentacion", MySqlDbType.Int32)).Value = oRegistroEN.oPresentacion.idProductoPresentacion;
                Comando.Parameters.Add(new MySqlParameter("@idCategoria", MySqlDbType.Int32)).Value = oRegistroEN.oCategoria.idCategoria;
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

                Cnn = null;
                Comando = null;
                Adaptador = null;
                oTransaccionesAD = null;

            }

        }

        public bool Eliminar(ProductoEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from Producto Where idProducto = @idProducto;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.idProducto;
                
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

        public bool Eliminar(ProductoEN oRegistroEN, DatosDeConexionEN oDatos, ref MySqlConnection Cnn_Existente, ref MySqlTransaction Transaccion_Existente)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {
                
                Comando = new MySqlCommand();
                Comando.Connection = Cnn_Existente;
                Comando.Transaction = Transaccion_Existente;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from Producto Where idProducto = @idProducto;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.idProducto;

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
                               
                Comando = null;
                Adaptador = null;
                oTransaccionesAD = null;

            }

        }

        public bool Listado(ProductoEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select p.idProducto, p.Codigo, p.CodigoDeBarra, p.Nombre, p.NombreGenerico, p.NombreComun, 
p.Descripcion, p.Observaciones,pps.Nombre as 'Presentacion', ppc.Costo, ppc.PorcentajeDelPrecio1, ppc.PorcentajeDelPrecio2, 
ppc.PorcentajeDelPrecio3, ppc.PorcentajeDelPrecio4, ppc.PorcentajeDelPrecio5, ppc.Precio1, ppc.Precio2, ppc.Precio3, 
ppc.Precio4, ppc.Precio5, ppc.AplicarElIva, ppc.ValorDelIvaEnProcentaje, ppc.ValorDelIva, 
pum.Nombre as 'UnidadDeMedida', p.Existencias, p.Minimo, p.Maximo, 
p.idProductoUnidadDeMedida, p.idProductoPresentacion, p.idCategoria, 
p.idUsuarioDeCreacion, p.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
p.idUsuarioModificacion, p.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
from Producto as p
inner join productounidaddemedida as pum on pum.idProductoUnidadDeMedida = p.idProductoUnidadDeMedida
inner join productopresentacion as pps on pps.idProductoPresentacion = p.idProductoPresentacion
inner join productoprecio as ppc on ppc.idProducto =  p.idProducto
inner join usuario as u on u.idUsuario = p.idUsuarioDeCreacion
left join usuario as u1 on u1.idUsuario = p.idUsuarioModificacion
Where p.idProducto > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoPorIdentificador(ProductoEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select p.idProducto,p.idProductoUnidadDeMedida, p.idProductoPresentacion, p.idCategoria, ppc.idProductoPrecio, 
                pcg.idProductoConfiguracion,p.Codigo, p.CodigoDeBarra, p.Nombre, p.NombreGenerico, p.NombreComun, 
                p.Descripcion, p.Observaciones,pps.Nombre as 'Presentacion', ppc.Costo, ppc.PorcentajeDelPrecio1, ppc.PorcentajeDelPrecio2, 
                ppc.PorcentajeDelPrecio3, ppc.PorcentajeDelPrecio4, ppc.PorcentajeDelPrecio5, ppc.Precio1, ppc.Precio2, ppc.Precio3, 
                ppc.Precio4, ppc.Precio5, ppc.AplicarElIva, ppc.ValorDelIvaEnProcentaje, ppc.ValorDelIva, 
                pum.Nombre as 'UnidadDeMedida', p.Existencias, p.Minimo, p.Maximo,
                pcg.ActivarPromocion, pcg.AplicarComisiones, pcg.MostrarContenidoDeObservacionesENFactura, pcg.MostrarImagenAlFacturar, 
                pcg.PreguntarNumeroDeSerieAlFacturar, pcg.PreguntarFechaDeVencimientoAlFacturar, pcg.PreguntarPorResetaAlFacturar, 
                pcg.NoUsarComisionesParaEsteProducto, pcg.UsarComisionesDefinidasEnElregistroDelVendedor, pcg.MontoFijoPorVenta, 
                pcg.PorcentajeDeLaVenta, pcg.PorcentajeDeLaGanacia, pcg.Comision, pcg.ComisionMaxima,
                ctg.Nombre as 'Categoria',                 
                p.idUsuarioDeCreacion, p.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                p.idUsuarioModificacion, p.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                from Producto as p
                inner join productounidaddemedida as pum on pum.idProductoUnidadDeMedida = p.idProductoUnidadDeMedida
                inner join productopresentacion as pps on pps.idProductoPresentacion = p.idProductoPresentacion
                inner join productoprecio as ppc on ppc.idProducto =  p.idProducto and ppc.Estado = 'ACTIVO'
                inner join productoconfiguracion as pcg on pcg.idProducto = p.idProducto                
                inner join usuario as u on u.idUsuario = p.idUsuarioDeCreacion                
                inner join categoria as ctg on ctg.idCategoria = p.idCategoria
                left join usuario as u1 on u1.idUsuario = p.idUsuarioModificacion
                Where p.idProducto = {0} ", oRegistroEN.idProducto);
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

        public bool ListadoParaCombos(ProductoEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idProducto, tu.Nombre
from Producto tu
Where idProducto > 0 {0} {1} ; ", oRegistroEN.Where, oRegistroEN.OrderBy);
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
               
        public bool ListadoParaReportes(ProductoEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select p.idProducto,p.idProductoUnidadDeMedida, p.idProductoPresentacion, p.idCategoria, ppc.idProductoPrecio, pcg.idProductoConfiguracion,
                pim.idProductoImagenes, p.Codigo, p.CodigoDeBarra, p.Nombre, p.NombreGenerico, p.NombreComun, 
                p.Descripcion, p.Observaciones,pps.Nombre as 'Presentacion', ppc.Costo, ppc.PorcentajeDelPrecio1, ppc.PorcentajeDelPrecio2, 
                ppc.PorcentajeDelPrecio3, ppc.PorcentajeDelPrecio4, ppc.PorcentajeDelPrecio5, ppc.Precio1, ppc.Precio2, ppc.Precio3, 
                ppc.Precio4, ppc.Precio5, ppc.AplicarElIva, ppc.ValorDelIvaEnProcentaje, ppc.ValorDelIva, 
                pum.Nombre as 'UnidadDeMedida', p.Existencias, p.Minimo, p.Maximo,
                pcg.ActivarPromocion, pcg.AplicarComisiones, pcg.MostrarContenidoDeObservacionesENFactura, pcg.MostrarImagenAlFacturar, 
                pcg.PreguntarNumeroDeSerieAlFacturar, pcg.PreguntarFechaDeVencimientoAlFacturar, pcg.PreguntarPorResetaAlFacturar, 
                pcg.NoUsarComisionesParaEsteProducto, pcg.UsarComisionesDefinidasEnElregistroDelVendedor, pcg.MontoFijoPorVenta, 
                pcg.PorcentajeDeLaVenta, pcg.PorcentajeDeLaGanacia, pcg.Comision, pcg.ComisionMaxima,
                pim.Nombre, pim.extension, pim.Ruta, pim.Size, pim.Foto,
                p.idUsuarioDeCreacion, p.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                p.idUsuarioModificacion, p.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                from Producto as p
                inner join productounidaddemedida as pum on pum.idProductoUnidadDeMedida = p.idProductoUnidadDeMedida
                inner join productopresentacion as pps on pps.idProductoPresentacion = p.idProductoPresentacion
                inner join productoprecio as ppc on ppc.idProducto =  p.idProducto
                inner join productoconfiguracion as pcg on pcg.idProducto = p.idProducto
                inner join productoimagenes as pim on pim.idProducto = p.idProducto
                inner join usuario as u on u.idUsuario = p.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = p.idUsuarioModificacion
                Where p.idProducto > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarSiElRegistroEstaVinculado(ProductoEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                Comando.Parameters.Add(new MySqlParameter("@CampoABuscar_", MySqlDbType.VarChar, 200)).Value = "idProducto";
                Comando.Parameters.Add(new MySqlParameter("@ValorCampoABuscar", MySqlDbType.Int32)).Value = oRegistroEN.idProducto;
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

        public bool ValidarRegistroDuplicado(ProductoEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProducto from Producto where upper( trim(Nombre) ) = upper(trim(@Nombre))) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProducto from Producto where upper( trim(Nombre) ) = upper(trim(@Nombre)) and idProducto <> @idProducto) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre;
                        Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.idProducto;

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

        public bool ValidarRegistroDuplicadoCodigoDeBarra(ProductoEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProducto from Producto where upper( trim(CodigoDeBarra) ) = upper(trim(@CodigoDeBarra))) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@CodigoDeBarra", MySqlDbType.VarChar, oRegistroEN.CodigoDeBarra.Trim().Length)).Value = oRegistroEN.CodigoDeBarra;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProducto from Producto where upper( trim(CodigoDeBarra) ) = upper(trim(@CodigoDeBarra)) and idProducto <> @idProducto) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@CodigoDeBarra", MySqlDbType.VarChar, oRegistroEN.CodigoDeBarra.Trim().Length)).Value = oRegistroEN.CodigoDeBarra;
                        Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.idProducto;

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

        private TransaccionesEN InformacionDelaTransaccion(ProductoEN oProducto, String TipoDeOperacion, String Descripcion, String Estado)
        {
            TransaccionesEN oRegistroEN = new TransaccionesEN();

            oRegistroEN.idregistro = oProducto.idProducto;
            oRegistroEN.Modelo = "ProductoAD";
            oRegistroEN.Modulo = "Productos";
            oRegistroEN.Tabla = "Producto";
            oRegistroEN.tipodeoperacion = TipoDeOperacion;
            oRegistroEN.Estado = Estado;
            oRegistroEN.ip = oProducto.oLoginEN.NumeroIP;
            oRegistroEN.idusuario = oProducto.oLoginEN.idUsuario;
            oRegistroEN.idusuarioaprueba = oProducto.oLoginEN.idUsuario;
            oRegistroEN.descripciondelusuario = DescripcionDeOperacion;
            oRegistroEN.descripcioninterna = Descripcion;
            oRegistroEN.NombreDelEquipo = oProducto.oLoginEN.NombreDelComputador;

            return oRegistroEN;
        }

        private string InformacionDelRegistro(ProductoEN oRegistroEN) {
            string Cadena = @"idProducto: {0}, Codigo: {1}, CodigoDeBarra: {2}, Nombre: {3}, NombreGenerico: {4}, 
NombreComun: {5}, Descripcion: {6}, Observaciones: {7}, Existencias: {8}, Minimo: {9}, Maximo: {10}, 
idProductoUnidadDeMedida: {11}, idProductoPresentacion: {12}, idCategoria: {13}, idUsuarioDeCreacion: {14}, 
FechaDeCreacion: {15}, idUsuarioModificacion: {16}, FechaDeModificacion: {17}, idAlmacenEntidad: {18}, idPLEntidad: {19}, 
TablaDeReferenciaDeAlmacenaje: {20}, TablaDeRefereciaDeProveedorOLaboratorio: {21}, Estado: {22}";
            Cadena = string.Format(Cadena, oRegistroEN.idProducto,oRegistroEN.Codigo,oRegistroEN.CodigoDeBarra,
                oRegistroEN.Nombre, oRegistroEN.NombreGenerico, oRegistroEN.NombreComun, oRegistroEN.Descripcion, 
                oRegistroEN.Observaciones,oRegistroEN.Existencias,oRegistroEN.Minimo,oRegistroEN.Maximo,
                oRegistroEN.oUnidadDeMedida.idProductoUnidadDeMedida, oRegistroEN.oPresentacion.idProductoPresentacion,
                oRegistroEN.oCategoria.idCategoria,oRegistroEN.idUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.idUsuarioModificacion, 
                oRegistroEN.FechaDeModificacion,oRegistroEN.idAlmacenEntidad, oRegistroEN.idPLEntidad, oRegistroEN.TablaDeReferenciaDeAlmacenaje,
                oRegistroEN.TablaDeRefereciaDeProveedorOLaboratorio, oRegistroEN.Estado);
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

        private string EvaluarTextoError(string Cadena, string operacion, string StringError)
        {
            string valor = string.Empty;

            if (string.IsNullOrEmpty(StringError) || StringError.Trim().Length == 0)
            {
                valor = string.Empty;

            }
            else
            {
                valor = string.Format("Error producido al Momento de '{0}', la Transacción no se completo: {1} {2}", operacion, Environment.NewLine, StringError);
            }

            if (Cadena.Trim().Length == 0 || string.IsNullOrEmpty(Cadena))
            {
                if (string.IsNullOrEmpty(valor))
                {
                    Cadena = string.Empty;
                }
                else { Cadena = valor; }
            }
            else
            {
                if (string.IsNullOrEmpty(valor) == false)
                {
                    Cadena = string.Format("{0} {1} {2}", Cadena, System.Environment.NewLine, valor);
                }

            }

            return Cadena;

        }

        #endregion


    }
}
