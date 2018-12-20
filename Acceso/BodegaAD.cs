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
    public class BodegaAD
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

        public bool Agregar(BodegaEN oRegistroEN, DatosDeConexionEN oDatos) {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"                                
                insert into bodega 
                (idAlmacen, Codigo, Nombre, Descripcion, PorDefectoParaFacturacion, 
                idUsuarioDeCreacion, FechaDeCreacion, idUsuarioModificacion, FechaDeModificacion)
                values
                (@idAlmacen, @Codigo, @Nombre, @Descripcion, @PorDefectoParaFacturacion, 
                @idUsuarioDeCreacion, current_timestamp(), @idUsuarioModificacion, current_timestamp());

                Select last_insert_id() as 'ID';";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.oAlmacenEN.idAlmacen;
                Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@PorDefectoParaFacturacion", MySqlDbType.Int32)).Value = oRegistroEN.PorDefectoParaFacturacion;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioDeCreacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;
                Comando.Parameters.Add(new MySqlParameter("@idUsuarioModificacion", MySqlDbType.Int32)).Value = oRegistroEN.oLoginEN.idUsuario;

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                oRegistroEN.idBodega = Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString());

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
        
        public bool Actualizar(BodegaEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"update bodega set

                idAlmacen = @idAlmacen, 
                Codigo = @Codigo, 
                Nombre = @Nombre, Descripcion = @Descripcion, 
                PorDefectoParaFacturacion = @PorDefectoParaFacturacion, 
                idUsuarioModificacion = @idUsuarioModificacion, 
                FechaDeModificacion = current_timestamp()

                Where idBodega = @idBodega;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idBodega", MySqlDbType.Int32)).Value = oRegistroEN.idBodega;
                Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.oAlmacenEN.idAlmacen;
                Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Descripcion", MySqlDbType.VarChar, oRegistroEN.Descripcion.Trim().Length)).Value = oRegistroEN.Descripcion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@PorDefectoParaFacturacion", MySqlDbType.Int32)).Value = oRegistroEN.PorDefectoParaFacturacion;
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

        public bool Eliminar(BodegaEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from Bodega Where idBodega = @idBodega;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idBodega", MySqlDbType.Int32)).Value = oRegistroEN.idBodega;

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
        
        public bool Listado(BodegaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select 
                b.idBodega, b.idAlmacen, 
                concat(a.Codigo, '-', b.Codigo) as 'CodigoDeAlmacenaje',
                b.Codigo, b.Nombre, b.Descripcion, b.PorDefectoParaFacturacion, 
                concat(a.Codigo, ' - ', a.Nombre) as 'Almacen',
                b.idUsuarioDeCreacion, b.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                b.idUsuarioModificacion, b.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                from bodega as b
                inner join almacen as a on a.idAlmacen = b.idAlmacen
                inner join usuario as u on u.idUsuario = b.idUsuarioDeCreacion
                inner join usuario as u1 on u1.idUsuario = b.idUsuarioModificacion
                where b.idBodega > 0  {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoBodegaPorIdAlmacen(BodegaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select b.idBodega, b.idAlmacen, b.Codigo, b.Nombre, b.Descripcion
                from Bodega as b
                inner join almacen as a on a.idAlmacen = b.idAlmacen
                Where b.idAlmacen = {0} {1} ", oRegistroEN.oAlmacenEN.idAlmacen, oRegistroEN.OrderBy);
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

        public bool ListadoParaAlmacenajeDelProducto(BodegaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select ba.idBodegaAlmacen, ba.idAlmacen, ba.idBodega,
                bl.idBodegaLocacion, bl.idLocacion,ls.idLocacionSeccion, 
                ls.idSeccion,sc.idSeccionContenedor, sc.idContenedor,
                a.Codigo as 'CodigoDeAlmacen',a.Nombre as 'NombreDelAlmacen',
                b.Codigo as 'CodigoDelaBodega', b.Nombre as 'NombreDeLaBodega', 
                l.Codigo as 'CodigoLocacion', l.Nombre as 'NombreLocacion', 
                s.Codigo as 'CodigoDeSeccion', s.Nombre	 as 'NombreDeLaSeccion', 
                c.Codigo as 'CodigoDecontenedor', c.Nombre as 'NombreDelContenedor',
                concat(a.Codigo,'-', b.Codigo,'-', l.Codigo, '-', s.Codigo,'-', c.Codigo) as 'CodigoDeAlmacenaje',
                concat(a.Codigo, ' - ', a.Nombre) as 'Almacen', 
                concat(b.Codigo , ' - ', b.Nombre) as 'Bodega',
                concat(l.Codigo, ' - ', l.Nombre) as 'Locacion',
                concat(s.Codigo, ' - ', s.Nombre) as 'Seccion', 
                concat(c.Codigo, ' - ', c.Nombre) as 'Contenedor'
                from bodegaalmacen as ba
                inner join almacen as a on a.idAlmacen = ba.idAlmacen
                inner join bodega as b on b.idBodega = ba.idBodega
                inner join bodegalocacion as bl on bl.idBodega = b.idBodega
                inner join locacion as l on l.idLocacion = bl.idLocacion
                inner join locacionseccion as ls on ls.idLocacion = l.idLocacion
                inner join seccion as s on s.idSeccion = ls.idSeccion
                inner join seccioncontenedor as sc on sc.idSeccion = s.idSeccion
                inner join contenedor as c on c.idContenedor = sc.idContenedor
                Where ba.idBodegaAlmacen > 0
                 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoDeAlmacenajeDelProducto(BodegaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.StoredProcedure;

                Consultas = "ListadoDeAlmacenajeDelProducto";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@SWhere", MySqlDbType.VarChar, oRegistroEN.Where.Length)).Value = oRegistroEN.Where;

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

        public bool ListadoPorIdentificador(BodegaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select 
                b.idBodega, b.idAlmacen, 
                concat(a.Codigo, '-', b.Codigo) as 'CodigoDeAlmacenaje',
                b.Codigo, b.Nombre, b.Descripcion, b.PorDefectoParaFacturacion, 
                b.idUsuarioDeCreacion, b.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                b.idUsuarioModificacion, b.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                from bodega as b
                inner join almacen as a on a.idAlmacen = b.idAlmacen
                inner join usuario as u on u.idUsuario = b.idUsuarioDeCreacion
                inner join usuario as u1 on u1.idUsuario = b.idUsuarioModificacion
                where b.idBodega  = {0} ", oRegistroEN.idBodega);
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

        public bool ListadoParaCombos(BodegaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select a.idBodega, a.Codigo, a.Nombre, concat(a.Codigo , '- ', a.Nombre) as 'Bodega'
                from bodega as a
                where a.idBodega > 0  {0} {1} ; ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoParaReportes(BodegaEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select 
                b.idBodega, b.idAlmacen, 
                concat(a.Codigo, '-', b.Codigo) as 'CodigoDeAlmacenaje',
                b.Codigo, b.Nombre, b.Descripcion, b.PorDefectoParaFacturacion, 
                b.idUsuarioDeCreacion, b.FechaDeCreacion, u.Nombre as 'UsuarioDeCreacion',
                b.idUsuarioModificacion, b.FechaDeModificacion, u1.Nombre as 'UsuarioDeModificacion'
                from bodega as b
                inner join almacen as a on a.idAlmacen = b.idAlmacen
                inner join usuario as u on u.idUsuario = b.idUsuarioDeCreacion
                inner join usuario as u1 on u1.idUsuario = b.idUsuarioModificacion
                where b.idBodega > 0 {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarSiElRegistroEstaVinculado(BodegaEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = "ValidarSiElRegistroEstaVinculadoUnaTabla";

                Comando.Parameters.Add(new MySqlParameter("@CampoABuscar_", MySqlDbType.VarChar, 200)).Value = "idBodega";
                Comando.Parameters.Add(new MySqlParameter("@ValorCampoABuscar", MySqlDbType.Int32)).Value = oRegistroEN.idBodega;
                Comando.Parameters.Add(new MySqlParameter("@ExcluirTabla_", MySqlDbType.VarChar, 200)).Value = "Bodega";

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

                    this.Error = String.Format("La Operación: '{1}', {0} no se puede completar por que el registro: {0} '{2}', {0} se encuentra asociado con: {0} {3}", Environment.NewLine, TipoDeOperacion, InformacionDelRegistro(oRegistroEN), oTransaccionesAD.ConvertirValorDeLaCadena(DT.Rows[0].ItemArray[0].ToString()));
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

        public bool ValidarRegistroDuplicado(BodegaEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                switch (TipoDeOperacion.Trim().ToUpper()) {

                    case "AGREGAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idBodega from Bodega where and idAlmacen = @idAlmacen and upper( trim(Nombre) ) = upper(trim(@Nombre))) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre;
                        Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.oAlmacenEN.idAlmacen;
                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idBodega from Bodega where idAlmacen= @idAlmacen and upper( trim(Nombre) ) = upper(trim(@Nombre)) and idBodega <> @idBodega) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre;
                        Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.oAlmacenEN.idAlmacen;
                        Comando.Parameters.Add(new MySqlParameter("@idBodega", MySqlDbType.Int32)).Value = oRegistroEN.idBodega;

                        break;

                    default:
                        throw new ArgumentException("La aperación solicitada no esta disponible");

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
        
        public bool VerificarSiLaEntidadEstaAsociadaAProducto(BodegaEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;
                Comando.CommandText = string.Format(@"Select case when exists(
                Select idProducto from producto as p
                where p.TablaDeReferenciaDeAlmacenaje = 'Bodega' and p.idAlmacenEntidad = {0}) then 1 else 0 end as 'ID'", oRegistroEN.idBodega);

                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                if (Convert.ToInt32(DT.Rows[0].ItemArray[0].ToString()) == 0)
                {
                    return false;
                }
                else
                {

                    this.Error = string.Format("La Operacion: '{1}', {0} no se puede completar por que el registro: {0} '{2}', ya tiene asociado un producto", Environment.NewLine, TipoDeOperacion, InformacionDelRegistro(oRegistroEN));
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

        public bool ValidarCodigo(BodegaEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idBodega from Bodega where idAlmacen = @idAlmacen and upper( trim(Codigo) ) = upper(trim(@Codigo))) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo;
                        Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.oAlmacenEN.idAlmacen;
                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idBodega from Bodega where idAlmacen = @idAlmacen and upper( trim(Codigo) ) = upper(trim(@Codigo)) and idBodega <> @idBodega) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo;
                        Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.oAlmacenEN.idAlmacen;
                        Comando.Parameters.Add(new MySqlParameter("@idBodega", MySqlDbType.Int32)).Value = oRegistroEN.idBodega;
                        

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

        public bool ValidarCodigoDelRegistros(BodegaEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idBodega from Bodega where idAlmacen = @idAlmacen and upper( trim(Codigo) ) = upper(trim(@Codigo))) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo;
                        Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.oAlmacenEN.idAlmacen;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idBodega from Bodega where idAlmacen = @idAlmacen and upper( trim(Codigo) ) = upper(trim(@Codigo)) and idBodega <> @idBodega) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo;
                        Comando.Parameters.Add(new MySqlParameter("@idAlmacen", MySqlDbType.Int32)).Value = oRegistroEN.oAlmacenEN.idAlmacen;
                        Comando.Parameters.Add(new MySqlParameter("@idBodega", MySqlDbType.Int32)).Value = oRegistroEN.idBodega;

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

        private TransaccionesEN InformacionDelaTransaccion(BodegaEN oBodega, String TipoDeOperacion, String Descripcion, String Estado)
        {
            TransaccionesEN oRegistroEN = new TransaccionesEN();

            oRegistroEN.idregistro = oBodega.idBodega;
            oRegistroEN.Modelo = "BodegaAD";
            oRegistroEN.Modulo = "Ubicacion";
            oRegistroEN.Tabla = "Bodega";
            oRegistroEN.tipodeoperacion = TipoDeOperacion;
            oRegistroEN.Estado = Estado;
            oRegistroEN.ip = oBodega.oLoginEN.NumeroIP;
            oRegistroEN.idusuario = oBodega.oLoginEN.idUsuario;
            oRegistroEN.idusuarioaprueba = oBodega.oLoginEN.idUsuario;
            oRegistroEN.descripciondelusuario = DescripcionDeOperacion;
            oRegistroEN.descripcioninterna = Descripcion;
            oRegistroEN.NombreDelEquipo = oBodega.oLoginEN.NombreDelComputador;

            return oRegistroEN;
        }


        private string InformacionDelRegistro(BodegaEN oRegistroEN) {
            string Cadena = @"idBodega: {0}, Nombre: {1}, Descripcion: {2}, PorDefectoParaFacturacion: {3}, idUsuarioDeCreacion: {4}, FechaDeCreacion: {5}, idUsuarioModificacion: {6}, FechaDeModificacion: {7}";
            Cadena = string.Format(Cadena, oRegistroEN.idBodega, oRegistroEN.Nombre, oRegistroEN.Descripcion, oRegistroEN.PorDefectoParaFacturacion, oRegistroEN.idUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.idUsuarioModificacion, oRegistroEN.FechaDeModificacion);
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
