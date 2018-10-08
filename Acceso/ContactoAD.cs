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
    public class ContactoAD
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

        public bool Agregar(ContactoEN oRegistroEN, DatosDeConexionEN oDatos) {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.StoredProcedure;

                Consultas = @"AgregarInformacionDelContacto";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idContacto_", MySqlDbType.Int32)).Value = oRegistroEN.idContacto;
                Comando.Parameters.Add(new MySqlParameter("@Codigo_", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Nombre_", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Direccion_", MySqlDbType.VarChar, oRegistroEN.Direccion.Trim().Length)).Value = oRegistroEN.Direccion.Trim();                                
                Comando.Parameters.Add(new MySqlParameter("@Telefono_", MySqlDbType.VarChar, oRegistroEN.Telefono.Trim().Length)).Value = oRegistroEN.Telefono.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Movil_", MySqlDbType.VarChar, oRegistroEN.Movil.Trim().Length)).Value = oRegistroEN.Movil.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Observaciones_", MySqlDbType.VarChar, oRegistroEN.Observaciones.Trim().Length)).Value = oRegistroEN.Observaciones.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Correo_", MySqlDbType.VarChar, oRegistroEN.Correo.Trim().Length)).Value = oRegistroEN.Correo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@FechaDeCumpleanos_", MySqlDbType.VarChar, oRegistroEN.FechaDeCumpleanos.Trim().Length)).Value = oRegistroEN.FechaDeCumpleanos.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Messenger_", MySqlDbType.VarChar, oRegistroEN.Messenger.Trim().Length)).Value = oRegistroEN.Messenger.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Skype_", MySqlDbType.VarChar, oRegistroEN.Skype.Trim().Length)).Value = oRegistroEN.Skype.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Twitter_", MySqlDbType.VarChar, oRegistroEN.Twitter.Trim().Length)).Value = oRegistroEN.Twitter.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Facebook_", MySqlDbType.VarChar, oRegistroEN.Facebook.Trim().Length)).Value = oRegistroEN.Facebook.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Estado_", MySqlDbType.VarChar, oRegistroEN.Estado.Trim().Length)).Value = oRegistroEN.Estado.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Sexo_", MySqlDbType.VarChar, oRegistroEN.Sexo.Trim().Length)).Value = oRegistroEN.Sexo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Cedula_", MySqlDbType.VarChar, oRegistroEN.Cedula.Trim().Length)).Value = oRegistroEN.Cedula.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Foto_", MySqlDbType.Binary)).Value = oRegistroEN.AFoto;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCreacion_", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion_", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.Codigo = DT.Rows[0].ItemArray[0].ToString();
                
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

        public bool Agregar(ContactoEN oRegistroEN, DatosDeConexionEN oDatos, ref MySqlConnection Cnn_Existente, ref MySqlTransaction Transaccion_Existen)
        {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn_Existente;
                Comando.Transaction = Transaccion_Existen;
                Comando.CommandType = CommandType.StoredProcedure;

                Consultas = @"AgregarInformacionDelContacto";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idContacto_", MySqlDbType.Int32)).Value = oRegistroEN.oEntidadEN.idEntidad;
                Comando.Parameters.Add(new MySqlParameter("@Codigo_", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Nombre_", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Direccion_", MySqlDbType.VarChar, oRegistroEN.Direccion.Trim().Length)).Value = oRegistroEN.Direccion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Telefono_", MySqlDbType.VarChar, oRegistroEN.Telefono.Trim().Length)).Value = oRegistroEN.Telefono.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Movil_", MySqlDbType.VarChar, oRegistroEN.Movil.Trim().Length)).Value = oRegistroEN.Movil.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Observaciones_", MySqlDbType.VarChar, oRegistroEN.Observaciones.Trim().Length)).Value = oRegistroEN.Observaciones.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Correo_", MySqlDbType.VarChar, oRegistroEN.Correo.Trim().Length)).Value = oRegistroEN.Correo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@FechaDeCumpleanos_", MySqlDbType.VarChar, oRegistroEN.FechaDeCumpleanos.Trim().Length)).Value = oRegistroEN.FechaDeCumpleanos.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Messenger_", MySqlDbType.VarChar, oRegistroEN.Messenger.Trim().Length)).Value = oRegistroEN.Messenger.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Skype_", MySqlDbType.VarChar, oRegistroEN.Skype.Trim().Length)).Value = oRegistroEN.Skype.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Twitter_", MySqlDbType.VarChar, oRegistroEN.Twitter.Trim().Length)).Value = oRegistroEN.Twitter.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Facebook_", MySqlDbType.VarChar, oRegistroEN.Facebook.Trim().Length)).Value = oRegistroEN.Facebook.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Estado_", MySqlDbType.VarChar, oRegistroEN.Estado.Trim().Length)).Value = oRegistroEN.Estado.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Sexo_", MySqlDbType.VarChar, oRegistroEN.Sexo.Trim().Length)).Value = oRegistroEN.Sexo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Cedula_", MySqlDbType.VarChar, oRegistroEN.Cedula.Trim().Length)).Value = oRegistroEN.Cedula.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Foto_", MySqlDbType.Binary)).Value = oRegistroEN.AFoto;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCreacion_", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion_", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.Codigo = DT.Rows[0].ItemArray[0].ToString();

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

        public bool AgregarUtilizandoLaMismaConexion(ContactoEN oRegistroEN, DatosDeConexionEN oDatos)
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
                oEntidadEN.oTipoDeEntidadEN.Nombre = "Contacto";
                oEntidadEN.oTipoDeEntidadEN.NombreInterno = "contacto";
                EntidadAD oEntidadAD = new EntidadAD();

                if (oEntidadAD.Agregar(oEntidadEN, oDatos, ref Cnn, ref oMySqlTransaction))
                {
                    oRegistroEN.idContacto = oEntidadEN.idEntidad;
                    Errores = EvaluarTextoError(Errores, "GUARDAR", oEntidadAD.Error);
                }
                else
                {
                    mensaje = String.Format("Error : '{1}', {0} producido al intentar guardar la información en la Entidad. ", Environment.NewLine, oEntidadAD.Error);
                    throw new System.ArgumentException(mensaje);
                }

                oEntidadAD = null;
                oEntidadEN = null;

                if (Agregar(oRegistroEN, oDatos, ref Cnn, ref oMySqlTransaction))
                {
                    oRegistroEN.idContacto = oEntidadEN.idEntidad;
                    Errores = EvaluarTextoError(Errores, "GUARDAR", this.Error);
                }
                else
                {
                    mensaje = String.Format("Error : '{1}', {0} producido al intentar guardar la información del concto. ", Environment.NewLine, this.Error);
                    throw new System.ArgumentException(mensaje);
                }

                this.Error = Errores;

                oMySqlTransaction.Commit();

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

        public bool Actualizar(ContactoEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"update contacto set

                Nombre = @Nombre, Direccion = @Direccion, 
                Telefono = @Telefono, Movil = @Movil, Observaciones = @Observaciones, 
                Correo = @Correo, FechaDeCumpleanos = @FechaDeCumpleanos, 
                Messenger = @Messenger, Skype = @Skype, Twitter = @Twitter, 
                Facebook = @Facebook, Estado = @Estado, Foto = @Foto, 
                idUsuarioModificacion = @idUsuarioModificacion, 
                FechaDeModificacion = current_timestamp(),
                Sexo = @Sexo, Cedula = @Cedula

                where idContacto = @idContacto;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idContacto", MySqlDbType.Int32)).Value = oRegistroEN.idContacto;                
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Direccion", MySqlDbType.VarChar, oRegistroEN.Direccion.Trim().Length)).Value = oRegistroEN.Direccion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Telefono", MySqlDbType.VarChar, oRegistroEN.Telefono.Trim().Length)).Value = oRegistroEN.Telefono.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Movil", MySqlDbType.VarChar, oRegistroEN.Movil.Trim().Length)).Value = oRegistroEN.Movil.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Observaciones", MySqlDbType.VarChar, oRegistroEN.Observaciones.Trim().Length)).Value = oRegistroEN.Observaciones.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Correo", MySqlDbType.VarChar, oRegistroEN.Correo.Trim().Length)).Value = oRegistroEN.Correo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@FechaDeCumpleanos", MySqlDbType.VarChar, oRegistroEN.FechaDeCumpleanos.Trim().Length)).Value = oRegistroEN.FechaDeCumpleanos.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Messenger", MySqlDbType.VarChar, oRegistroEN.Messenger.Trim().Length)).Value = oRegistroEN.Messenger.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Skype", MySqlDbType.VarChar, oRegistroEN.Skype.Trim().Length)).Value = oRegistroEN.Skype.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Twitter", MySqlDbType.VarChar, oRegistroEN.Twitter.Trim().Length)).Value = oRegistroEN.Twitter.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Facebook", MySqlDbType.VarChar, oRegistroEN.Facebook.Trim().Length)).Value = oRegistroEN.Facebook.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Estado", MySqlDbType.VarChar, oRegistroEN.Estado.Trim().Length)).Value = oRegistroEN.Estado.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Sexo", MySqlDbType.VarChar, oRegistroEN.Sexo.Trim().Length)).Value = oRegistroEN.Sexo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Cedula", MySqlDbType.VarChar, oRegistroEN.Cedula.Trim().Length)).Value = oRegistroEN.Cedula.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Foto", MySqlDbType.Binary)).Value = oRegistroEN.AFoto;                
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

        public bool Eliminar(ContactoEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from Contacto Where idContacto = @idContacto;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idContacto", MySqlDbType.Int32)).Value = oRegistroEN.idContacto;
                
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

        public bool Listado(ContactoEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idContacto, Codigo, c.Nombre,c.Cedula, Direccion, Telefono, Movil, Sexo,
                Observaciones, Correo, FechaDeCumpleanos, Messenger, Skype, 
                Twitter, Facebook, c.Estado, Foto, 
                c.idUsuarioDeCreacion, c.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                c.idUsuarioModificacion, c.FechaDeModificacion, c1.Nombre as 'UsuarioDeModificacion'
                from contacto as c
                inner join usuario as u on u.idUsuario = c.idUsuarioDeCreacion
                left join usuario as c1 on c1.idUsuario = c.idUsuarioModificacion
                where c.idContacto > 0  {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoPorIdentificador(ContactoEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idContacto, Codigo, c.Nombre,c.Cedula, Direccion, Telefono, Movil, Sexo, 
                Observaciones, Correo, FechaDeCumpleanos, Messenger, Skype, 
                Twitter, Facebook, c.Estado, Foto, 
                c.idUsuarioDeCreacion, c.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                c.idUsuarioModificacion, c.FechaDeModificacion, c1.Nombre as 'UsuarioDeModificacion'
                from contacto as c
                inner join usuario as u on u.idUsuario = c.idUsuarioDeCreacion
                left join usuario as c1 on c1.idUsuario = c.idUsuarioModificacion
                where c.idContacto = {0} ", oRegistroEN.idContacto);
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

        public bool ListadoParaCombos(ContactoEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idContacto, Codigo, c.Nombre, c.Cedula, Direccion, Telefono, Movil, Sexo,
                Observaciones, Correo, FechaDeCumpleanos, Messenger, Skype, 
                Twitter, Facebook, c.Estado, Foto, 
                c.idUsuarioDeCreacion, c.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                c.idUsuarioModificacion, c.FechaDeModificacion, c1.Nombre as 'UsuarioDeModificacion'
                from contacto as c
                inner join usuario as u on u.idUsuario = c.idUsuarioDeCreacion
                left join usuario as c1 on c1.idUsuario = c.idUsuarioModificacion
                where c.idContacto > 0  {0} {1} ; ", oRegistroEN.Where, oRegistroEN.OrderBy);
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
               
        public bool ListadoParaReportes(ContactoEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idContacto, Codigo, c.Nombre, c.Cedula, Direccion, Telefono, Movil, Sexo,
                Observaciones, Correo, FechaDeCumpleanos, Messenger, Skype, 
                Twitter, Facebook, c.Estado, Foto, 
                c.idUsuarioDeCreacion, c.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                c.idUsuarioModificacion, c.FechaDeModificacion, c1.Nombre as 'UsuarioDeModificacion'
                from contacto as c
                inner join usuario as u on u.idUsuario = c.idUsuarioDeCreacion
                left join usuario as c1 on c1.idUsuario = c.idUsuarioModificacion
                where c.idContacto > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool GenerarCodigoDelContacto(ContactoEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select GenerarCodigoDelContacto() as 'Codigo'; ", oRegistroEN.Where, oRegistroEN.OrderBy);
                Comando.CommandText = Consultas;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();
                
                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.Codigo = DT.Rows[0].ItemArray[0].ToString();

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

        public bool ValidarSiElRegistroEstaVinculado(ContactoEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                Comando.Parameters.Add(new MySqlParameter("@CampoABuscar_", MySqlDbType.VarChar, 200)).Value = "idContacto";
                Comando.Parameters.Add(new MySqlParameter("@ValorCampoABuscar", MySqlDbType.Int32)).Value = oRegistroEN.idContacto;
                Comando.Parameters.Add(new MySqlParameter("@ExcluirTabla_", MySqlDbType.VarChar, 200)).Value = "Entidad";

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

        public bool ValidarRegistroDuplicado(ContactoEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idContacto from Contacto where upper( trim(Nombre) ) = upper(trim(@Nombre))) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idContacto from Contacto where upper( trim(Nombre) ) = upper(trim(@Nombre)) and idContacto <> @idContacto) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre;
                        Comando.Parameters.Add(new MySqlParameter("@idContacto", MySqlDbType.Int32)).Value = oRegistroEN.idContacto;

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

        public bool ValidarRegistroDuplicadoParaCedula(ContactoEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idContacto from Contacto where upper( trim(Cedula) ) = upper(trim(@Cedula))) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Cedula", MySqlDbType.VarChar, oRegistroEN.Cedula.Trim().Length)).Value = oRegistroEN.Cedula;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idContacto from Contacto where upper( trim(Cedula) ) = upper(trim(@Cedula)) and idContacto <> @idContacto) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Cedula", MySqlDbType.VarChar, oRegistroEN.Cedula.Trim().Length)).Value = oRegistroEN.Cedula;
                        Comando.Parameters.Add(new MySqlParameter("@idContacto", MySqlDbType.Int32)).Value = oRegistroEN.idContacto;

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

        private TransaccionesEN InformacionDelaTransaccion(ContactoEN oContacto, String TipoDeOperacion, String Descripcion, String Estado)
        {
            TransaccionesEN oRegistroEN = new TransaccionesEN();

            oRegistroEN.idregistro = oContacto.idContacto;
            oRegistroEN.Modelo = "TransaccionAD";
            oRegistroEN.Modulo = "Transacciones";
            oRegistroEN.Tabla = "Contactos";
            oRegistroEN.tipodeoperacion = TipoDeOperacion;
            oRegistroEN.Estado = Estado;
            oRegistroEN.ip = oContacto.oLoginEN.NumeroIP;
            oRegistroEN.idusuario = oContacto.oLoginEN.idUsuario;
            oRegistroEN.idusuarioaprueba = oContacto.oLoginEN.idUsuario;
            oRegistroEN.descripciondelusuario = DescripcionDeOperacion;
            oRegistroEN.descripcioninterna = Descripcion;
            oRegistroEN.NombreDelEquipo = oContacto.oLoginEN.NombreDelComputador;

            return oRegistroEN;
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

        private string InformacionDelRegistro(ContactoEN oRegistroEN) {
            string Cadena = @"idContacto, Codigo, Nombre, Direccion, Telefono, Movil, Observaciones, Correo, FechaDeCumpleanos, Messenger, Skype, Twitter, Facebook, Estado, idUsuarioDeCreacion, FechaDeCreacion, idUsuarioModificacion, FechaDeModificacion";
            Cadena = string.Format(Cadena, oRegistroEN.idContacto, oRegistroEN.Codigo, oRegistroEN.Nombre, oRegistroEN.Direccion, oRegistroEN.Telefono, oRegistroEN.Movil,
                oRegistroEN.Observaciones, oRegistroEN.Correo, oRegistroEN.FechaDeCumpleanos, oRegistroEN.Messenger, oRegistroEN.Skype, oRegistroEN.Twitter, oRegistroEN.Facebook, oRegistroEN.Estado,oRegistroEN.IdUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.IdUsuarioDeModificacion, oRegistroEN.FechaDeModificacion);
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
