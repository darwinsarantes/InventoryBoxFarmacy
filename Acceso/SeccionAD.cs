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
    public class SeccionAD
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

        public bool Agregar(SeccionEN oRegistroEN, DatosDeConexionEN oDatos) {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"
                                
                insert into Seccion 
                (idSeccion,Codigo,Nombre, Descripcion, 
                idUsuarioDeCreacion, FechaDeCreacion, idUsuarioModificacion, FechaDeModificacion)
                values
                (@idSeccion,@Codigo,@Nombre, @Descripcion, 
                @idUsuarioDeCreacion, current_timestamp(), @idUsuarioModificacion, current_timestamp());
                ";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.idSeccion;
                Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();
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

        public bool Agregar(SeccionEN oRegistroEN, DatosDeConexionEN oDatos, ref MySqlConnection Cnn_Existente, ref MySqlTransaction Transaccion_Existente)
        {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Comando = new MySqlCommand();
                Comando.Connection = Cnn_Existente;
                Comando.Transaction = Transaccion_Existente;
                Comando.CommandType = CommandType.Text;
                
                Consultas = @"
                                
                insert into Seccion 
                (idSeccion, Codigo,Nombre, Descripcion, 
                idUsuarioDeCreacion, FechaDeCreacion, idUsuarioModificacion, FechaDeModificacion)
                values
                (@idSeccion, @Codigo,@Nombre, @Descripcion, 
                @idUsuarioDeCreacion, current_timestamp(), @idUsuarioModificacion, current_timestamp()); ";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.idSeccion;
                Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();
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

        public bool AgregarUtilizandoLaMismaConexion(SeccionEN oRegistroEN, DatosDeConexionEN oDatos)
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
                oEntidadEN.oTipoDeEntidadEN.Nombre = "Seccion";
                oEntidadEN.oTipoDeEntidadEN.NombreInterno = "seccion";
                oEntidadEN.oLoginEN = oRegistroEN.oLoginEN;
                oEntidadEN.IdUsuarioDeCreacion = oRegistroEN.oLoginEN.idUsuario;
                oEntidadEN.IdUsuarioDeModificacion = oRegistroEN.oLoginEN.idUsuario;
                oEntidadEN.FechaDeCreacion = oRegistroEN.FechaDeCreacion;
                oEntidadEN.FechaDeModificacion = oRegistroEN.FechaDeModificacion;

                EntidadAD oEntidadAD = new EntidadAD();

                if (oEntidadAD.Agregar(oEntidadEN, oDatos, ref Cnn, ref oMySqlTransaction))
                {
                    oRegistroEN.idSeccion = oEntidadEN.idEntidad;
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
                    mensaje = String.Format("Error : '{1}', {0} producido al intentar guardar la información de la seccion. ", Environment.NewLine, this.Error);
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

        public bool Actualizar(SeccionEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"update Seccion set

	                Nombre = @Nombre, 
                    Codigo = @Codigo,
                    Descripcion = @Descripcion, 
	                idUsuarioModificacion = @idUsuarioModificacion, 
                    FechaDeModificacion = current_timestamp()

                Where idSeccion = @idSeccion;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.idSeccion;
                Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();                
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

        public bool Eliminar(SeccionEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from Seccion Where idSeccion = @idSeccion;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.idSeccion;
                
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

        public bool Eliminar(SeccionEN oRegistroEN, DatosDeConexionEN oDatos, ref MySqlConnection Cnn_Existente, ref MySqlTransaction Transaccion_Existente)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {                                
                Comando = new MySqlCommand();
                Comando.Connection = Cnn_Existente;
                Comando.Transaction = Transaccion_Existente;
                Comando.CommandType = CommandType.Text;

                System.Diagnostics.Debug.Print(String.Format("{0} VALOR DEL REGISTRO: {1} {0}", Environment.NewLine, oRegistroEN.idSeccion));

                Consultas = @"Delete from seccion where idSeccion = @idSeccion;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.idSeccion;

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

        public bool EliminarUtilizandoLaMismaConexion(SeccionEN oRegistroEN, DatosDeConexionEN oDatos)
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
                oEntidadEN.idEntidad = oRegistroEN.idSeccion;
                oEntidadEN.oTipoDeEntidadEN.Nombre = "Seccion";
                oEntidadEN.oTipoDeEntidadEN.NombreInterno = "seccion";
                oEntidadEN.oLoginEN = oRegistroEN.oLoginEN;
                oEntidadEN.IdUsuarioDeCreacion = oRegistroEN.oLoginEN.idUsuario;
                oEntidadEN.IdUsuarioDeModificacion = oRegistroEN.oLoginEN.idUsuario;
                oEntidadEN.FechaDeCreacion = oRegistroEN.FechaDeCreacion;
                oEntidadEN.FechaDeModificacion = oRegistroEN.FechaDeModificacion;

                EntidadAD oEntidadAD = new EntidadAD();
                
                if (Eliminar(oRegistroEN, oDatos, ref Cnn, ref oMySqlTransaction))
                {
                    Errores = EvaluarTextoError(Errores, "ELIMINAR", this.Error);
                }
                else
                {
                    mensaje = String.Format("Error : '{1}', {0} producido al intentar eliminar la información en la sección. ", Environment.NewLine, this.Error);
                    throw new System.ArgumentException(mensaje);
                }

                if (oEntidadAD.Eliminar(oEntidadEN, oDatos, ref Cnn, ref oMySqlTransaction))
                {
                    Errores = EvaluarTextoError(Errores, "ELIMINAR", oEntidadAD.Error);
                }
                else
                {
                    mensaje = String.Format("Error : '{1}', {0} producido al intentar Eliminar la información de la Entidad superior del obejto. ", Environment.NewLine, oEntidadAD.Error);
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

        public bool Listado(SeccionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idSeccion,s.Codigo, s.Nombre, Descripcion, s.idUsuarioDeCreacion, 
                s.FechaDeCreacion, s.idUsuarioModificacion, s.FechaDeModificacion 
                from Seccion as s
                inner join usuario as u on u.idUsuario = s.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = s.idUsuarioModificacion
                where idSeccion > 0  {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoDeContenedoresPorSeccion(SeccionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select scc.idSeccionContenedor, scc.idContenedor,
                cr.Codigo, cr.Nombre, cr.Descripcion
                from seccioncontenedor scc
                inner join seccion as sc on sc.idSeccion = scc.idSeccion
                inner join contenedor as cr on cr.idContenedor = scc.idContenedor
                where scc.idSeccion = {0} Order By cr.Codigo ", oRegistroEN.idSeccion);
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

        public bool ListadoPorIdentificador(SeccionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idSeccion,s.Codigo, s.Nombre, Descripcion, s.idUsuarioDeCreacion, 
                s.FechaDeCreacion, s.idUsuarioModificacion, s.FechaDeModificacion 
                from Seccion as s
                inner join usuario as u on u.idUsuario = s.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = s.idUsuarioModificacion
                where idSeccion = {0} ", oRegistroEN.idSeccion);
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

        public bool ListadoParaCombos(SeccionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idSeccion, s.Nombre, concat(s.Codigo, '', s.Nombre) as 'Seccion',
                from Seccion as s                
                where idSeccion > 0  {0} {1} ; ", oRegistroEN.Where, oRegistroEN.OrderBy);
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
               
        public bool ListadoParaReportes(SeccionEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idSeccion, s.Codigo,s.Nombre, Descripcion, s.idUsuarioDeCreacion, 
                s.FechaDeCreacion, s.idUsuarioModificacion, s.FechaDeModificacion 
                from Seccion as s
                inner join usuario as u on u.idUsuario = s.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = s.idUsuarioModificacion
                where idSeccion > 0  {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarSiElRegistroEstaVinculado(SeccionEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                Comando.Parameters.Add(new MySqlParameter("@CampoABuscar_", MySqlDbType.VarChar, 200)).Value = "idSeccion";
                Comando.Parameters.Add(new MySqlParameter("@ValorCampoABuscar", MySqlDbType.Int32)).Value = oRegistroEN.idSeccion;
                Comando.Parameters.Add(new MySqlParameter("@ExcluirTabla_", MySqlDbType.VarChar, 200)).Value = "'Entidad'";

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

        public bool ValidarRegistroDuplicado(SeccionEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idSeccion from Seccion where upper( trim(Nombre) ) = upper(trim(@Nombre))) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idSeccion from Seccion where upper( trim(Nombre) ) = upper(trim(@Nombre)) and idSeccion <> @idSeccion) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre;
                        Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.idSeccion;

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

        public bool ValidarCodigo(SeccionEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idSeccion from Seccion where upper( trim(Codigo) ) = upper(trim(@Codigo))) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idSeccion from Seccion where upper( trim(Codigo) ) = upper(trim(@Codigo)) and idSeccion <> @idSeccion) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo;
                        Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.idSeccion;

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

        private TransaccionesEN InformacionDelaTransaccion(SeccionEN oSeccion, String TipoDeOperacion, String Descripcion, String Estado)
        {
            TransaccionesEN oRegistroEN = new TransaccionesEN();

            oRegistroEN.idregistro = oSeccion.idSeccion;
            oRegistroEN.Modelo = "General";
            oRegistroEN.Modulo = "SeccionAD";
            oRegistroEN.Tabla = "Seccion";
            oRegistroEN.tipodeoperacion = TipoDeOperacion;
            oRegistroEN.Estado = Estado;
            oRegistroEN.ip = oSeccion.oLoginEN.NumeroIP;
            oRegistroEN.idusuario = oSeccion.oLoginEN.idUsuario;
            oRegistroEN.idusuarioaprueba = oSeccion.oLoginEN.idUsuario;
            oRegistroEN.descripciondelusuario = DescripcionDeOperacion;
            oRegistroEN.descripcioninterna = Descripcion;
            oRegistroEN.NombreDelEquipo = oSeccion.oLoginEN.NombreDelComputador;

            return oRegistroEN;
        }
        
        private string InformacionDelRegistro(SeccionEN oRegistroEN) {
            string Cadena = @"idSeccion: {0}, Codigo: {7}, Nombre: {1}, Descripcion: {2}, idUsuarioDeCreacion: {3}, FechaDeCreacion: {4}, idUsuarioModificacion: {5}, FechaDeModificacion: {6}";
            Cadena = string.Format(Cadena, oRegistroEN.idSeccion, oRegistroEN.Nombre, oRegistroEN.Descripcion, oRegistroEN.idUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.idUsuarioModificacion, oRegistroEN.FechaDeModificacion, oRegistroEN.Codigo);
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
