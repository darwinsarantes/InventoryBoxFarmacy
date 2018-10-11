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
    public class ProductoSustitutosAD
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

        public bool Agregar(ProductoSustitutosEN oRegistroEN, DatosDeConexionEN oDatos) {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"
                                
                INSERT INTO productosustitutos
                (idProducto, idSustituto, idUsuarioDeCreacion, FechaDeCreacion, 
                idUsuarioModificacion, FechaDeModificacion)
                VALUES
                (@idProducto, @idSustituto, @idUsuarioDeCreacion, current_timestamp(), 
                @idUsuarioModificacion, current_timestamp());

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;
                Comando.Parameters.Add(new MySqlParameter("@idSustituto", MySqlDbType.Int32)).Value = oRegistroEN.oSutitutoEN.idProducto;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioDeCreacion;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.idUsuarioModificacion;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idProductoSustitutos = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());
                
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
        
        public bool Actualizar(ProductoSustitutosEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"UPDATE productosustitutos SET
	                idProducto = @idProducto, idSustituto = @idSustituto, 
	                idUsuarioModificacion = @idUsuarioModificacion, FechaDeModificacion = current_timestamp()
                WHERE idProductoSustitutos = @idProductoSustitutos;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProductoSustitutos", MySqlDbType.Int32)).Value = oRegistroEN.idProductoSustitutos;
                Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;
                Comando.Parameters.Add(new MySqlParameter("@idSustituto", MySqlDbType.Int32)).Value = oRegistroEN.oSutitutoEN.idProducto;                
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

        public bool Eliminar(ProductoSustitutosEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from ProductoSustitutos Where idProductoSustitutos = @idProductoSustitutos;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idProductoSustitutos", MySqlDbType.Int32)).Value = oRegistroEN.idProductoSustitutos;
                
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

        public bool Listado(ProductoSustitutosEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT ps.idProductoSustitutos, ps.idProducto, ps.idSustituto, 
	                ps.Nombre as 'Producto', NombreGenerico, NombreComun, Descripcion,
                    ps.idUsuarioDeCreacion, ps.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
	                ps.idUsuarioModificacion, ps.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                 FROM  productosustitutos as ps
                 inner join producto as p on p.idProducto = ps.idSustituto
                inner join usuario as u on u.idUsuario = ps.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = ps.idUsuarioModificacion
                Where idProductoSustitutos > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoPorIdentificador(ProductoSustitutosEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT ps.idProductoSustitutos, ps.idProducto, ps.idSustituto, 
	                ps.Nombre as 'Producto', NombreGenerico, NombreComun, Descripcion,
                    ps.idUsuarioDeCreacion, ps.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
	                ps.idUsuarioModificacion, ps.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                    FROM  productosustitutos as ps
                    inner join producto as p on p.idProducto = ps.idSustituto
                inner join usuario as u on u.idUsuario = ps.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = ps.idUsuarioModificacion
                Where idProductoSustitutos = {0}", oRegistroEN.idProductoSustitutos);
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
       
        public bool ListadoParaReportes(ProductoSustitutosEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"SELECT ps.idProductoSustitutos, ps.idProducto, ps.idSustituto, 
	                ps.Nombre as 'Producto', NombreGenerico, NombreComun, Descripcion,
                    ps.idUsuarioDeCreacion, ps.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
	                ps.idUsuarioModificacion, ps.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                 FROM  productosustitutos as ps
                 inner join producto as p on p.idProducto = ps.idSustituto
                inner join usuario as u on u.idUsuario = ps.idUsuarioDeCreacion
                left join usuario as u1 on u1.idUsuario = ps.idUsuarioModificacion
                Where idProductoSustitutos > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarSiElRegistroEstaVinculado(ProductoSustitutosEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                Comando.Parameters.Add(new MySqlParameter("@CampoABuscar_", MySqlDbType.VarChar, 200)).Value = "idProductoSustitutos";
                Comando.Parameters.Add(new MySqlParameter("@ValorCampoABuscar", MySqlDbType.Int32)).Value = oRegistroEN.idProductoSustitutos;
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

        public bool ValidarRegistroDuplicado(ProductoSustitutosEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProductoSustitutos from ProductoSustitutos where idProducto = @idProducto) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idProductoSustitutos from ProductoSustitutos where idProducto = @idProducto and idProductoSustitutos <> @idProductoSustitutos) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@idProducto", MySqlDbType.Int32)).Value = oRegistroEN.oProductoEN.idProducto;
                        Comando.Parameters.Add(new MySqlParameter("@idProductoSustitutos", MySqlDbType.Int32)).Value = oRegistroEN.idProductoSustitutos;

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

        private TransaccionesEN InformacionDelaTransaccion(ProductoSustitutosEN oProductoSustitutos, String TipoDeOperacion, String Descripcion, String Estado)
        {
            TransaccionesEN oRegistroEN = new TransaccionesEN();

            oRegistroEN.idregistro = oProductoSustitutos.idProductoSustitutos;
            oRegistroEN.Modelo = "ProductoSustitutosAD";
            oRegistroEN.Modulo = "Producto";
            oRegistroEN.Tabla = "ProductoSustitutos";
            oRegistroEN.tipodeoperacion = TipoDeOperacion;
            oRegistroEN.Estado = Estado;
            oRegistroEN.ip = oProductoSustitutos.oLoginEN.NumeroIP;
            oRegistroEN.idusuario = oProductoSustitutos.oLoginEN.idUsuario;
            oRegistroEN.idusuarioaprueba = oProductoSustitutos.oLoginEN.idUsuario;
            oRegistroEN.descripciondelusuario = DescripcionDeOperacion;
            oRegistroEN.descripcioninterna = Descripcion;
            oRegistroEN.NombreDelEquipo = oProductoSustitutos.oLoginEN.NombreDelComputador;

            return oRegistroEN;
        }
        
        private string InformacionDelRegistro(ProductoSustitutosEN oRegistroEN) {
            string Cadena = @"idProductoSustitutos: {0}, idProducto: {1}, idSustituto: {2}, idUsuarioDeCreacion: {3}, FechaDeCreacion: {4}, idUsuarioModificacion: {5}, FechaDeModificacion: {6}";
            Cadena = string.Format(Cadena, oRegistroEN.idProductoSustitutos, oRegistroEN.oProductoEN.idProducto, oRegistroEN.oSutitutoEN.idProducto, oRegistroEN.idUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.idUsuarioModificacion, oRegistroEN.FechaDeModificacion);
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
