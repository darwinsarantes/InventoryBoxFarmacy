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
    public class CierreDePeriodoAD
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

        public bool Agregar(CierreDePeriodoEN oRegistroEN, DatosDeConexionEN oDatos) {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"insert into cierredeperiodo
                (idPeriodo, idUsuarioDeCierre, FechaDeCierre, Descripcion, IdUsuarioDeCreacion, FechaDeCreacion, IdUsuarioDeModificacion, FechaDeModificacion)
                values
                (@idPeriodo, @idUsuarioDeCierre, @FechaDeCierre, @Descripcion, @IdUsuarioDeCreacion, current_timestamp(), @IdUsuarioDeModificacion, current_timestamp());

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idPeriodo", MySqlDbType.Int32)).Value = oRegistroEN.oPeriodoEN.idPeriodo;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCierre", MySqlDbType.Int32)).Value = oRegistroEN.oUsuarioDeCierre.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@FechaDeCierre", MySqlDbType.DateTime)).Value = oRegistroEN.FechaDeCierre;
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();

                Comando.Parameters.Add(new MySqlParameter("@IdUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@IdUsuarioDeModificacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idCierreDePeriodo = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());
                
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

        public bool Agregar(CierreDePeriodoEN oRegistroEN, DatosDeConexionEN oDatos, ref MySqlConnection Cnn_Existente, ref MySqlTransaction Transaccion_Existente)
        {

            oTransaccionesAD = new TransaccionesAD();

            try
            {
                
                Comando = new MySqlCommand();
                Comando.Connection = Cnn_Existente;
                Comando.Transaction = Transaccion_Existente;
                Comando.CommandType = CommandType.Text;

                Consultas = @"insert into cierredeperiodo
                (idPeriodo, idUsuarioDeCierre, FechaDeCierre, Descripcion, IdUsuarioDeCreacion, FechaDeCreacion, IdUsuarioDeModificacion, FechaDeModificacion, idTasaDeCambio)
                values
                (@idPeriodo, @idUsuarioDeCierre, @FechaDeCierre, @Descripcion, @IdUsuarioDeCreacion, current_timestamp(), @IdUsuarioDeModificacion, current_timestamp(), @idTasaDeCambio);

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idPeriodo", MySqlDbType.Int32)).Value = oRegistroEN.oPeriodoEN.idPeriodo;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCierre", MySqlDbType.Int32)).Value = oRegistroEN.oUsuarioDeCierre.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@FechaDeCierre", MySqlDbType.DateTime)).Value = oRegistroEN.FechaDeCierre;
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();

                Comando.Parameters.Add(new MySqlParameter("@IdUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@IdUsuarioDeModificacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@idTasaDeCambio", MySqlDbType.Int32)).Value = oRegistroEN.oTasaDeCambioEN.idTasaDeCambio;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idCierreDePeriodo = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());

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

        public bool AplicarSaldoAlCierre(CierreDePeriodoEN oRegistroEN, DatosDeConexionEN oDatos, ref MySqlConnection Cnn_Existente, ref MySqlTransaction Transaccion_Existente)
        {

            oTransaccionesAD = new TransaccionesAD();

            try
            {
                                
                Comando = new MySqlCommand();
                Comando.Connection = Cnn_Existente;
                Comando.Transaction = Transaccion_Existente;

                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "AplicarCierreDelPeriodo";

                Comando.Parameters.Add(new MySqlParameter("@idCierreDePeriodo_", MySqlDbType.Int32)).Value = oRegistroEN.idCierreDePeriodo;
                Comando.Parameters.Add(new MySqlParameter("@IdUsuarioDeModificacion_", MySqlDbType.Int32)).Value = oRegistroEN.IdUsuarioDeModificacion;                

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idCierreDePeriodo = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());

                DescripcionDeOperacion = string.Format("El registro fue Insertado Correctamente. {0} {1}", Environment.NewLine, InformacionDelRegistro(oRegistroEN));

                //Agregamos la Transacción....
                TransaccionesEN oTran = InformacionDelaTransaccion(oRegistroEN, "Cierre de Saldos", "Calcular Saldos al cierre", "CORRECTO");
                oTransaccionesAD.Agregar(oTran, oDatos);

                return true;


            }
            catch (Exception ex)
            {
                this.Error = ex.Message;

                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al insertar el registro. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);

                //Agregamos la Transacción....
                TransaccionesEN oTran = InformacionDelaTransaccion(oRegistroEN, "Cierre de Saldos", "Calcular Saldos al cierre", "ERROR");
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

        public bool AgregarUtilizandoLaMismaConexion(CierreDePeriodoEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
            Cnn.Open();

            MySqlTransaction MysqlTransaccion;
            MysqlTransaccion = Cnn.BeginTransaction();

            try
            {
                String mensaje = "";

                TasaDeCambioAD oTasaDeCambioAD = new TasaDeCambioAD();

                if(oTasaDeCambioAD.Agregar(oRegistroEN.oTasaDeCambioEN, oDatos, ref Cnn, ref MysqlTransaccion) == false)
                {
                    mensaje = String.Format("Error : '{0}', producido al intentar guardar la tasa de cambio", oTasaDeCambioAD.Error);
                    throw new System.ArgumentException(mensaje);
                }

                if (Agregar(oRegistroEN, oDatos, ref Cnn, ref MysqlTransaccion) == false)
                {
                    mensaje = String.Format("Error : '{0}', producido al intentar guardar el cierre del periodo", this.Error);
                    throw new System.ArgumentException(mensaje);
                }

                PeriodoAD oPeriodoAD = new PeriodoAD();

                if(oPeriodoAD.ActualizarElEstadoDelPeriodo(oRegistroEN.oPeriodoEN, oDatos, ref Cnn, ref MysqlTransaccion) == false)
                {                
                    mensaje = String.Format("Error : '{0}', producido al intentar actualizar el estado del periodo", oPeriodoAD.Error);
                    throw new System.ArgumentException(mensaje);
                }

                if (AplicarSaldoAlCierre(oRegistroEN, oDatos, ref Cnn, ref MysqlTransaccion) == false)
                {
                    mensaje = String.Format("Error : '{0}', producido al intentar realizar el cierre del periodo", this.Error);
                    throw new System.ArgumentException(mensaje);
                }

                oPeriodoAD = null;
                MysqlTransaccion.Commit();
                return true;


            }catch(Exception ex)
            {
                this.Error = ex.Message;

                MysqlTransaccion.Rollback();

                oTransaccionesAD = new TransaccionesAD();
                DescripcionDeOperacion = string.Format("Se produjo el seguiente error: '{2}' al aplicar el cierre del periodo. {0} {1} ", Environment.NewLine, InformacionDelRegistro(oRegistroEN), ex.Message);

                //Agregamos la Transacción....
                TransaccionesEN oTran = InformacionDelaTransaccion(oRegistroEN, "Cerrar Periodo", "Cerrar periodo de Movimiento de cuentas", "ERROR");
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
                MysqlTransaccion = null;

            }

        }


        public bool Actualizar(CierreDePeriodoEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"update cierredeperiodo set

	                idPeriodo = @idPeriodo, idUsuarioDeCierre = @idUsuarioDeCierre, FechaDeCierre = @FechaDeCierre, Descripcion = @Descripcion, 
                    IdUsuarioDeModificacion = @IdUsuarioDeModificacion, FechaDeModificacion = current_timestamp()

                where idCierreDePeriodo = @idCierreDePeriodo;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idCierreDePeriodo", MySqlDbType.Int32)).Value = oRegistroEN.idCierreDePeriodo;
                Comando.Parameters.Add(new MySqlParameter("@idPeriodo", MySqlDbType.Int32)).Value = oRegistroEN.oPeriodoEN.idPeriodo;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCierre", MySqlDbType.Int32)).Value = oRegistroEN.oUsuarioDeCierre.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@FechaDeCierre", MySqlDbType.DateTime)).Value = oRegistroEN.FechaDeCierre;
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();

                Comando.Parameters.Add(new MySqlParameter("@IdUsuarioDeModificacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

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

        public bool Eliminar(CierreDePeriodoEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from CierreDePeriodo where idCierreDePeriodo = @idCierreDePeriodo;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idCierreDePeriodo", MySqlDbType.Int32)).Value = oRegistroEN.idCierreDePeriodo;
                
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

        public bool Listado(CierreDePeriodoEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select 
                 cp.idCierreDePeriodo, cp.idPeriodo, cp.idUsuarioDeCierre, u.Nombre as 'UsuarioDelCierre', r.Nombre as 'Rol', cp.FechaDeCierre, cp.Descripcion,
                 cp.IdUsuarioDeCreacion, cp.FechaDeCreacion, cp.IdUsuarioDeModificacion, cp.FechaDeModificacion
                 from cierredeperiodo as cp
                inner join periodo as p on p.idPeriodo = cp.idPeriodo
                inner join usuario as u on u.idUsuario = cp.idUsuarioDeCierre
                inner join rol as r on r.idRol = u.idRol
                Where cp.idCierreDePeriodo > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoPorIdentificador(CierreDePeriodoEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select 
                 cp.idCierreDePeriodo, cp.idPeriodo, cp.idUsuarioDeCierre, u.Nombre as 'UsuarioDelCierre', r.Nombre as 'Rol', cp.FechaDeCierre, cp.Descripcion,
                 cp.IdUsuarioDeCreacion, cp.FechaDeCreacion, cp.IdUsuarioDeModificacion, cp.FechaDeModificacion
                 from cierredeperiodo as cp
                inner join periodo as p on p.idPeriodo = cp.idPeriodo
                inner join usuario as u on u.idUsuario = cp.idUsuarioDeCierre
                inner join rol as r on r.idRol = u.idRol
                Where cp.idCierreDePeriodo = {0} ", oRegistroEN.idCierreDePeriodo);
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

        public bool ListadoParaCombos(CierreDePeriodoEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select 
                 cp.idCierreDePeriodo, cp.idPeriodo, cp.idUsuarioDeCierre, u.Nombre as 'UsuarioDelCierre', r.Nombre as 'Rol', cp.FechaDeCierre, cp.Descripcion,
                 cp.IdUsuarioDeCreacion, cp.FechaDeCreacion, cp.IdUsuarioDeModificacion, cp.FechaDeModificacion
                 from cierredeperiodo as cp
                inner join periodo as p on p.idPeriodo = cp.idPeriodo
                inner join usuario as u on u.idUsuario = cp.idUsuarioDeCierre
                inner join rol as r on r.idRol = u.idRol
                Where cp.idCierreDePeriodo > 0 {0} {1} ; ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoParaReportes(CierreDePeriodoEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select 
                 cp.idCierreDePeriodo, cp.idPeriodo, cp.idUsuarioDeCierre, u.Nombre as 'UsuarioDelCierre', r.Nombre as 'Rol', cp.FechaDeCierre, cp.Descripcion,
                 cp.IdUsuarioDeCreacion, cp.FechaDeCreacion, cp.IdUsuarioDeModificacion, cp.FechaDeModificacion
                 from cierredeperiodo as cp
                inner join periodo as p on p.idPeriodo = cp.idPeriodo
                inner join usuario as u on u.idUsuario = cp.idUsuarioDeCierre
                inner join rol as r on r.idRol = u.idRol
                Where cp.idCierreDePeriodo > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarSiElRegistroEstaVinculado(CierreDePeriodoEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                Comando.Parameters.Add(new MySqlParameter("@CampoABuscar_", MySqlDbType.VarChar, 200)).Value = "idCierreDePeriodo";
                Comando.Parameters.Add(new MySqlParameter("@ValorCampoABuscar", MySqlDbType.Int32)).Value = oRegistroEN.idCierreDePeriodo;
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

        public bool ValidarRegistroDuplicado(CierreDePeriodoEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idCierreDePeriodo from cierredeperiodo where idPeriodo = @idPeriodo and idUsuarioDeCierre = @idUsuarioDeCierre) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idCierreDePeriodo", MySqlDbType.Int32)).Value = oRegistroEN.oUsuarioDeCierre.idUsuario;
                        Comando.Parameters.Add(new MySqlParameter("@idPeriodo", MySqlDbType.Int32)).Value = oRegistroEN.oPeriodoEN.idPeriodo;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idCierreDePeriodo from cierredeperiodo where idPeriodo = @idPeriodo and idUsuarioDeCierre = @idUsuarioDeCierre and idCierreDePeriodo <> @idCierreDePeriodo) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idCierreDePeriodo", MySqlDbType.Int32)).Value = oRegistroEN.oUsuarioDeCierre.idUsuario;
                        Comando.Parameters.Add(new MySqlParameter("@idPeriodo", MySqlDbType.Int32)).Value = oRegistroEN.oPeriodoEN.idPeriodo;
                        Comando.Parameters.Add(new MySqlParameter("@idCierreDePeriodo", MySqlDbType.Int32)).Value = oRegistroEN.idCierreDePeriodo;

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

        private TransaccionesEN InformacionDelaTransaccion(CierreDePeriodoEN oCierreDePeriodo, String TipoDeOperacion, String Descripcion, String Estado)
        {
            TransaccionesEN oRegistroEN = new TransaccionesEN();

            oRegistroEN.idregistro = oCierreDePeriodo.idCierreDePeriodo;
            oRegistroEN.Modelo = "TransaccionAD";
            oRegistroEN.Modulo = "Transacciones";
            oRegistroEN.Tabla = "CierreDePeriodoes";
            oRegistroEN.tipodeoperacion = TipoDeOperacion;
            oRegistroEN.Estado = Estado;
            oRegistroEN.ip = oCierreDePeriodo.oLoginEN.NumeroIP;
            oRegistroEN.idusuario = oCierreDePeriodo.oLoginEN.idUsuario;
            oRegistroEN.idusuarioaprueba = oCierreDePeriodo.oLoginEN.idUsuario;
            oRegistroEN.descripciondelusuario = DescripcionDeOperacion;
            oRegistroEN.descripcioninterna = Descripcion;
            oRegistroEN.NombreDelEquipo = oCierreDePeriodo.oLoginEN.NombreDelComputador;

            return oRegistroEN;
        }


        private string InformacionDelRegistro(CierreDePeriodoEN oRegistroEN) {
            string Cadena = @"idCierreDePeriodo: {0}, idPeriodo: {1}, idUsuarioDeCierre: {2}, FechaDeCierre: {3}, Descripcion: {4}, IdUsuarioDeCreacion: {5}, FechaDeCreacion: {6}, IdUsuarioDeModificacion: {7}, FechaDeModificacion: {8}";
            Cadena = string.Format(Cadena, oRegistroEN.idCierreDePeriodo, oRegistroEN.oPeriodoEN.idPeriodo, oRegistroEN.oUsuarioDeCierre.idUsuario, oRegistroEN.FechaDeCierre, oRegistroEN.Descripcion, oRegistroEN.IdUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.IdUsuarioDeModificacion, oRegistroEN.FechaDeModificacion);
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
