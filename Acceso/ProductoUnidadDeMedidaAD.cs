﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Entidad;
namespace AccesoDatos
{
    public class ProductoUnidadDeMedidaAD
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

        public bool Agregar(ProductoUnidadDeMedidaEN oRegistroEN, DatosDeConexionEN oDatos) {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"
                                
                insert into ProductoUnidadDeMedida
                (Nombre, Abreviatura, Descripcion, idUsuarioDeCreacion, FechaDeCreacion, idUsuarioModificacion, FechaDeModificacion)
                values
                (@Nombre, @Abreviatura, @Descripcion, @idUsuarioDeCreacion, current_timestamp(), @idUsuarioModificacion, current_timestamp());

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;                               

                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Abreviatura", MySqlDbType.VarChar, oRegistroEN.Abreviatura.Trim().Length)).Value = oRegistroEN.Abreviatura.Trim();
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idProductoUnidadDeMedida = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());
                
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
        
        public bool Actualizar(ProductoUnidadDeMedidaEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"update ProductoUnidadDeMedida set
	                Nombre = @Nombre, Abreviatura = @Abreviatura, Descripcion = @Descripcion,idUsuarioModificacion = @idUsuarioModificacion, FechaDeModificacion = current_timestamp()
                where idProductoUnidadDeMedida = @idProductoUnidadDeMedida;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProductoUnidadDeMedida", MySqlDbType.Int32)).Value = oRegistroEN.idProductoUnidadDeMedida;
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Abreviatura", MySqlDbType.VarChar, oRegistroEN.Abreviatura.Trim().Length)).Value = oRegistroEN.Abreviatura.Trim();                
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

        public bool Eliminar(ProductoUnidadDeMedidaEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from ProductoUnidadDeMedida Where idProductoUnidadDeMedida = @idProductoUnidadDeMedida;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProductoUnidadDeMedida", MySqlDbType.Int32)).Value = oRegistroEN.idProductoUnidadDeMedida;
                
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

        public bool Listado(ProductoUnidadDeMedidaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idProductoUnidadDeMedida, pum.Nombre, pum.Abreviatura, pum.Descripcion,
                pum.idUsuarioDeCreacion, pum.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                pum.idUsuarioModificacion, pum.FechaDeModificacion , u1.Nombre as 'UsuarioDeModificacion'
                from ProductoUnidadDeMedida as pum
                inner join usuario as u on u.idUsuario = pum.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = pum.idUsuarioModificacion
                Where idProductoUnidadDeMedida > 0  {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoPorIdentificador(ProductoUnidadDeMedidaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idProductoUnidadDeMedida, pum.Nombre, pum.Abreviatura, pum.Descripcion,
                pum.idUsuarioDeCreacion, pum.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                pum.idUsuarioModificacion, pum.FechaDeModificacion , u1.Nombre as 'UsuarioDeModificacion'
                from ProductoUnidadDeMedida as pum
                inner join usuario as u on u.idUsuario = pum.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = pum.idUsuarioModificacion
                Where idProductoUnidadDeMedida ={0} ", oRegistroEN.idProductoUnidadDeMedida);
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

        public bool ListadoParaCombos(ProductoUnidadDeMedidaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idProductoUnidadDeMedida, pum.Nombre, pum.Abreviatura, pum.Descripcion,
                concat(Nombre, ' - ', Abreviatura) as 'UMAbreviatura'
                from ProductoUnidadDeMedida as pum
                Where idProductoUnidadDeMedida > 0 {0} {1} ; ", oRegistroEN.Where, oRegistroEN.OrderBy);
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
               
        public bool ListadoParaReportes(ProductoUnidadDeMedidaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idProductoUnidadDeMedida, pum.Nombre, pum.Abreviatura, pum.Descripcion,
                pum.idUsuarioDeCreacion, pum.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                pum.idUsuarioModificacion, pum.FechaDeModificacion , u1.Nombre as 'UsuarioDeModificacion'
                from ProductoUnidadDeMedida as pum
                inner join usuario as u on u.idUsuario = pum.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = pum.idUsuarioModificacion
                Where idProductoUnidadDeMedida > 0   {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarSiElRegistroEstaVinculado(ProductoUnidadDeMedidaEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                Comando.Parameters.Add(new MySqlParameter("@CampoABuscar_", MySqlDbType.VarChar, 200)).Value = "idProductoUnidadDeMedida";
                Comando.Parameters.Add(new MySqlParameter("@ValorCampoABuscar", MySqlDbType.Int32)).Value = oRegistroEN.idProductoUnidadDeMedida;
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

        public bool ValidarRegistroDuplicado(ProductoUnidadDeMedidaEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProductoUnidadDeMedida from ProductoUnidadDeMedida where upper( trim(Nombre) ) = upper(trim(@Nombre))) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProductoUnidadDeMedida from ProductoUnidadDeMedida where upper( trim(Nombre) ) = upper(trim(@Nombre)) and idProductoUnidadDeMedida <> @idProductoUnidadDeMedida) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre;
                        Comando.Parameters.Add(new MySqlParameter("@idProductoUnidadDeMedida", MySqlDbType.Int32)).Value = oRegistroEN.idProductoUnidadDeMedida;

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

        private TransaccionesEN InformacionDelaTransaccion(ProductoUnidadDeMedidaEN oProductoUnidadDeMedida, String TipoDeOperacion, String Descripcion, String Estado)
        {
            TransaccionesEN oRegistroEN = new TransaccionesEN();

            oRegistroEN.idregistro = oProductoUnidadDeMedida.idProductoUnidadDeMedida;
            oRegistroEN.Modelo = "General";
            oRegistroEN.Modulo = "ProductoUnidadDeMedidaAD";
            oRegistroEN.Tabla = "ProductoUnidadDeMedida";
            oRegistroEN.tipodeoperacion = TipoDeOperacion;
            oRegistroEN.Estado = Estado;
            oRegistroEN.ip = oProductoUnidadDeMedida.oLoginEN.NumeroIP;
            oRegistroEN.idusuario = oProductoUnidadDeMedida.oLoginEN.idUsuario;
            oRegistroEN.idusuarioaprueba = oProductoUnidadDeMedida.oLoginEN.idUsuario;
            oRegistroEN.descripciondelusuario = DescripcionDeOperacion;
            oRegistroEN.descripcioninterna = Descripcion;
            oRegistroEN.NombreDelEquipo = oProductoUnidadDeMedida.oLoginEN.NombreDelComputador;

            return oRegistroEN;
        }
        
        private string InformacionDelRegistro(ProductoUnidadDeMedidaEN oRegistroEN) {
            string Cadena = @"idProductoUnidadDeMedida: {0}, Nombre: {1}, Descripcion: {2}, Abreviatura: {7}, idUsuarioDeCreacion: {3}, FechaDeCreacion: {4}, idUsuarioModificacion: {5}, FechaDeModificacion: {6}";
            Cadena = string.Format(Cadena, oRegistroEN.idProductoUnidadDeMedida, oRegistroEN.Nombre, oRegistroEN.Descripcion, oRegistroEN.idUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.idUsuarioModificacion, oRegistroEN.FechaDeModificacion,oRegistroEN.Abreviatura);
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
