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
    public class AlmacenAD
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

        public bool Agregar(AlmacenEN oRegistroEN, DatosDeConexionEN oDatos) {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"
                                
                INSERT INTO almacen
                (idAlmacen, Codigo,Nombre, Descripcion, PorDefecto, 
                idUsuarioDeCreacion, FechaDeCreacion, idUsuarioModificacion, FechaDeModificacion)
                VALUES
                (@idAlmacen, @Codigo,@Nombre, @Descripcion, @PorDefecto, 
                @idUsuarioDeCreacion, current_timestamp(), 
                @idUsuarioModificacion, current_timestamp());

                ";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.idAlmacen;
                Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@PorDefecto", MySqlDbType.Int32)).Value = oRegistroEN.PorDefecto;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

                Comando.ExecuteNonQuery();
                
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

        public bool Agregar(AlmacenEN oRegistroEN, DatosDeConexionEN oDatos, ref MySqlConnection Cnn_Existente, ref MySqlTransaction Transaccion_Existente)
        {

            oTransaccionesAD = new TransaccionesAD();

            try
            {
                
                Comando = new MySqlCommand();
                Comando.Connection = Cnn_Existente;                
                Comando.Transaction = Transaccion_Existente;
                Comando.CommandType = CommandType.Text;

                Consultas = @"
                                
                INSERT INTO almacen
                (idAlmacen,Codigo,Nombre, Descripcion, PorDefecto, 
                idUsuarioDeCreacion, FechaDeCreacion, idUsuarioModificacion, FechaDeModificacion)
                VALUES
                (@idAlmacen,@Codigo,@Nombre, @Descripcion, @PorDefecto, 
                @idUsuarioDeCreacion, current_timestamp(), 
                @idUsuarioModificacion, current_timestamp());

                ";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.idAlmacen;
                Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@PorDefecto", MySqlDbType.Int32)).Value = oRegistroEN.PorDefecto;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

                Comando.ExecuteNonQuery();

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

        public bool AgregarUtilizandoLaMismaConexion(AlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
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

                //Debemos agrear la Entidad Correspondiente
                EntidadEN oEntidadEN = new EntidadEN();
                oEntidadEN.oTipoDeEntidadEN.Nombre = "Almacen";
                oEntidadEN.oTipoDeEntidadEN.NombreInterno = "almacen";
                oEntidadEN.oLoginEN = oRegistroEN.oLoginEN;
                oEntidadEN.IdUsuarioDeCreacion = oRegistroEN.oLoginEN.idUsuario;
                oEntidadEN.IdUsuarioDeModificacion = oRegistroEN.oLoginEN.idUsuario;
                oEntidadEN.FechaDeCreacion = oRegistroEN.FechaDeCreacion;
                oEntidadEN.FechaDeModificacion = oRegistroEN.FechaDeModificacion;

                EntidadAD oEntidadAD = new EntidadAD();

                if (oEntidadAD.Agregar(oEntidadEN, oDatos, ref Cnn, ref oMySqlTransaction))
                {
                    oRegistroEN.idAlmacen = oEntidadEN.idEntidad;
                    Errores = EvaluarTextoError(Errores, "GUARDAR", oEntidadAD.Error);
                }
                else
                {
                    mensaje = String.Format("Error : '{1}', {0} producido al intentar guardar la información en la Entidad. ", Environment.NewLine, oEntidadAD.Error);
                    throw new System.ArgumentException(mensaje);
                }

                if (Agregar(oRegistroEN, oDatos, ref Cnn, ref oMySqlTransaction))
                {
                    Errores = EvaluarTextoError(Errores, "GUARDAR", this.Error);
                }
                else
                {
                    mensaje = String.Format("Error : '{1}', {0} producido al intentar guardar la información del almacen. ", Environment.NewLine, this.Error);
                    throw new System.ArgumentException(mensaje);
                }

                oMySqlTransaction.Commit();

                this.Error = Errores;

                oEntidadEN = null;
                oEntidadAD = null;
                
                return true;
                
            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
                oMySqlTransaction.Rollback();

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al insertar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                //Agregamos la Transacción....
                TransaccionesEN oTran = InformacionDelaTransaccion(oRegistroEN, "Agregar", "Agregar Nuevo Registro", "ERROR");
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

        public bool Actualizar(AlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"UPDATE almacen SET
                    Codigo = @Codigo,
	                Nombre = @Nombre, Descripcion = @Descripcion, PorDefecto = @PorDefecto, 
	                idUsuarioModificacion = @idUsuarioModificacion, FechaDeModificacion = current_timestamp()
                WHERE idAlmacen = @idAlmacen;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.idAlmacen;
                Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@PorDefecto", MySqlDbType.Int32)).Value = oRegistroEN.PorDefecto;                
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

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

        public bool ActualizarVodegaPorDefecto(AlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"UPDATE almacen SET
                    PorDefecto = @PorDefecto, 
	                idUsuarioModificacion = @idUsuarioModificacion, 
                    FechaDeModificacion = current_timestamp()
                WHERE idAlmacen <> @idAlmacen;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.idAlmacen;
                Comando.Parameters.Add(new MySqlParameter("@PorDefecto", MySqlDbType.Int32)).Value = oRegistroEN.PorDefecto;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

                Comando.ExecuteNonQuery();

                DescripcionDeOperacion = string.Format("Valor por defecto actualizado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

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

        public bool Eliminar(AlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from Almacen Where idAlmacen = @idAlmacen;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.idAlmacen;
                
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

        public bool Eliminar(AlmacenEN oRegistroEN, DatosDeConexionEN oDatos, ref MySqlConnection Cnn_Existente, ref MySqlTransaction Transaccion_Existente)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {
                Comando = new MySqlCommand();
                Comando.Connection = Cnn_Existente;
                Comando.Transaction = Transaccion_Existente;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from Almacen Where idAlmacen = @idAlmacen;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.idAlmacen;

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

        public bool EliminarUtilizandoLaMismaConexion(AlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
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

                //Debemos agrear la Entidad Correspondiente
                EntidadEN oEntidadEN = new EntidadEN();
                oEntidadEN.idEntidad = oRegistroEN.idAlmacen;
                oEntidadEN.oTipoDeEntidadEN.Nombre = "Almacen";
                oEntidadEN.oTipoDeEntidadEN.NombreInterno = "almacen";
                oEntidadEN.oLoginEN = oRegistroEN.oLoginEN;
                oEntidadEN.IdUsuarioDeCreacion = oRegistroEN.oLoginEN.idUsuario;
                oEntidadEN.IdUsuarioDeModificacion = oRegistroEN.oLoginEN.idUsuario;
                oEntidadEN.FechaDeCreacion = oRegistroEN.FechaDeCreacion;
                oEntidadEN.FechaDeModificacion = oRegistroEN.FechaDeModificacion;

                EntidadAD oEntidadAD = new EntidadAD();

                if (Eliminar(oRegistroEN, oDatos, ref Cnn, ref oMySqlTransaction))
                {                    
                    Errores = EvaluarTextoError(Errores, "ELIMINAR", oEntidadAD.Error);
                }
                else
                {
                    mensaje = String.Format("Error : '{1}', {0} producido al intentar eliminar la información en el almacen. ", Environment.NewLine, oEntidadAD.Error);
                    throw new System.ArgumentException(mensaje);
                }

                if (oEntidadAD.Eliminar(oEntidadEN, oDatos, ref Cnn, ref oMySqlTransaction))
                {
                    Errores = EvaluarTextoError(Errores, "ELIMINAR", this.Error);
                }
                else
                {
                    mensaje = String.Format("Error : '{1}', {0} producido al intentar eliminar la información de la entidad superior. ", Environment.NewLine, this.Error);
                    throw new System.ArgumentException(mensaje);
                }

                oMySqlTransaction.Commit();

                this.Error = Errores;

                oEntidadEN = null;
                oEntidadAD = null;

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
                oMySqlTransaction.Rollback();

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al insertar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);
                //Agregamos la Transacción....
                TransaccionesEN oTran = InformacionDelaTransaccion(oRegistroEN, "ELIMINAR", "Eliminar Registro", "ERROR");
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

        public bool Listado(AlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT 
	                idAlmacen,a.Codigo, a.Nombre, a.Descripcion, a.PorDefecto, 
                    a.idUsuarioDeCreacion, a.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                    a.idUsuarioModificacion, a.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                FROM almacen AS a 
                inner join usuario as u on u.idUsuario = a.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = a.idUsuarioModificacion
                Where idAlmacen > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoPorIdentificador(AlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT 
	                idAlmacen, a.Codigo,a.Nombre, a.Descripcion, a.PorDefecto, 
                    a.idUsuarioDeCreacion, a.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                    a.idUsuarioModificacion, a.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                FROM almacen AS a 
                inner join usuario as u on u.idUsuario = a.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = a.idUsuarioModificacion
                Where idAlmacen = {0} ", oRegistroEN.idAlmacen);
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

        public bool ListadoParaCombos(AlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT 
	                idAlmacen, a.Codigo,a.Nombre, concat(a.Codigo, ' ', a.Nombre) as 'Almacen'
                FROM almacen AS a                 
                Where idAlmacen > 0 {0} {1} ; ", oRegistroEN.Where, oRegistroEN.OrderBy);
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
               
        public bool ListadoParaReportes(AlmacenEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT 
	                idAlmacen, a.Codigo,a.Nombre, a.Descripcion, a.PorDefecto, 
                    a.idUsuarioDeCreacion, a.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                    a.idUsuarioModificacion, a.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                FROM almacen AS a 
                inner join usuario as u on u.idUsuario = a.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = a.idUsuarioModificacion
                Where idAlmacen > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        #region "Funciones de Validación"

        public bool ValidarSiElRegistroEstaVinculado(AlmacenEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                Comando.Parameters.Add(new MySqlParameter("@CampoABuscar_", MySqlDbType.VarChar, 200)).Value = "idAlmacen";
                Comando.Parameters.Add(new MySqlParameter("@ValorCampoABuscar", MySqlDbType.Int32)).Value = oRegistroEN.idAlmacen;
                Comando.Parameters.Add(new MySqlParameter("@ExcluirTabla_", MySqlDbType.VarChar, 200)).Value = "'Entidad', 'bodegaalmacen'";

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

        public bool ValidarRegistroDuplicado(AlmacenEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idAlmacen from Almacen where upper( trim(Nombre) ) = upper(trim(@Nombre))) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idAlmacen from Almacen where upper( trim(Nombre) ) = upper(trim(@Nombre)) and idAlmacen <> @idAlmacen) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre;
                        Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.idAlmacen;

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

        public bool ValidarCodigo(AlmacenEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idAlmacen from Almacen where upper( trim(Codigo) ) = upper(trim(@Codigo))) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idAlmacen from Almacen where upper( trim(Codigo) ) = upper(trim(@Codigo)) and idAlmacen <> @idAlmacen) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo;
                        Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.idAlmacen;

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

                    DescripcionDeOperacion = string.Format("Ya existe información del Código dentro de nuestro sistema: {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));
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

        private TransaccionesEN InformacionDelaTransaccion(AlmacenEN oAlmacen, String TipoDeOperacion, String Descripcion, String Estado)
        {
            TransaccionesEN oRegistroEN = new TransaccionesEN();

            oRegistroEN.idregistro = oAlmacen.idAlmacen;
            oRegistroEN.Modelo = "AlmacenAD";
            oRegistroEN.Modulo = "Ubicacion";
            oRegistroEN.Tabla = "Almacen";
            oRegistroEN.tipodeoperacion = TipoDeOperacion;
            oRegistroEN.Estado = Estado;
            oRegistroEN.ip = oAlmacen.oLoginEN.NumeroIP;
            oRegistroEN.idusuario = oAlmacen.oLoginEN.idUsuario;
            oRegistroEN.idusuarioaprueba = oAlmacen.oLoginEN.idUsuario;
            oRegistroEN.descripciondelusuario = DescripcionDeOperacion;
            oRegistroEN.descripcioninterna = Descripcion;
            oRegistroEN.NombreDelEquipo = oAlmacen.oLoginEN.NombreDelComputador;

            return oRegistroEN;
        }


        private string InformacionDelRegistro(AlmacenEN oRegistroEN) {
            string Cadena = @"idAlmacen: {0}, Nombre: {1}, Descripcion: {2}, PorDefecto: {3}, idUsuarioDeCreacion: {4}, FechaDeCreacion: {5}, idUsuarioModificacion: {6}, FechaDeModificacion: {7}";
            Cadena = string.Format(Cadena, oRegistroEN.idAlmacen, oRegistroEN.Nombre, oRegistroEN.Descripcion, oRegistroEN.PorDefecto, oRegistroEN.idUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.idUsuarioModificacion, oRegistroEN.FechaDeModificacion);
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
