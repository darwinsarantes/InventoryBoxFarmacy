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
    public class ContenedorAD
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

        public bool Agregar(ContenedorEN oRegistroEN, DatosDeConexionEN oDatos) {

            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"
                                
                insert into  contenedor
                (idSeccion, Codigo, Nombre, Descripcion, 
                idUsuarioDeCreacion, FechaDeCreacion, idUsuarioModificacion, FechaDeModificacion)
                values
                (@idSeccion, @Codigo, @Nombre, @Descripcion, 
                @idUsuarioDeCreacion, current_timestamp(), @idUsuarioModificacion, current_timestamp());

                Select last_insert_id() as 'ID';
                ";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.oSeccionEN.idSeccion;
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
        
        public bool Actualizar(ContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"update contenedor set
                idSeccion = @idSeccion, Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, 
                idUsuarioModificacion = @idUsuarioModificacion, 
                FechaDeModificacion = current_timestamp()
                where idContenedor = @idContenedor;";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idContenedor", MySqlDbType.Int32)).Value = oRegistroEN.idContenedor;
                Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.oSeccionEN.idSeccion;
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

        public bool Eliminar(ContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {
            oTransaccionesAD = new TransaccionesAD();

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Delete from Contenedor Where idContenedor = @idContenedor;";
                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idContenedor", MySqlDbType.Int32)).Value = oRegistroEN.idContenedor;
                
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
        
        public bool Listado(ContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select c.idContenedor, c.idSeccion,
                concat(a.Codigo,'-', b.Codigo, '-', l.Codigo,'-',s.Codigo,'-',c.Codigo) as 'CodigoDeAlmacenaje',
                c.Codigo, c.Nombre, c.Descripcion

                from contenedor as c 
                inner join seccion as s on s.idSeccion = c.idSeccion
                inner join locacion as l on l.idLocacion = s.idLocacion
                inner join bodega as b on b.idBodega = l.idBodega
                inner join almacen as a on a.idAlmacen = b.idAlmacen     
                Where c.idContenedor > 0     {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ListadoPorIdSeccion(ContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idContenedor, idSeccion, Codigo, Nombre, Descripcion
                from contenedor
                Where idSeccion = 0  {0} {1} ", oRegistroEN.oSeccionEN.idSeccion, oRegistroEN.OrderBy);
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

        public bool ListadoPorInclusionDeSecciones(ContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idContenedor, idSeccion, Codigo, Nombre, Descripcion
                from contenedor
                Where idContenedor > 0  {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
                Comando.CommandText = Consultas;

                System.Diagnostics.Debug.Print(Consultas);

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

        public bool ListadoPorIdentificador(ContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select c.idContenedor, c.idSeccion,l.idLocacion,l.idBodega, b.idAlmacen,
                concat(a.Codigo,'-', b.Codigo, '-', l.Codigo,'-',s.Codigo,'-',c.Codigo) as 'CodigoDeAlmacenaje',
                c.Codigo, c.Nombre, c.Descripcion

                from contenedor as c 
                inner join seccion as s on s.idSeccion = c.idSeccion
                inner join locacion as l on l.idLocacion = s.idLocacion
                inner join bodega as b on b.idBodega = l.idBodega
                inner join almacen as a on a.idAlmacen = b.idAlmacen     
                Where c.idContenedor = {0} ", oRegistroEN.idContenedor);
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

        public bool ListadoParaCombos(ContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select idContenedor, cr.Nombre, concat(cr.Codigo, '', cr.Nombre) as 'Contenedor'
                from Contenedor as cr                
                where idContenedor > 0  {0} {1} ; ", oRegistroEN.Where, oRegistroEN.OrderBy);
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
               
        public bool ListadoParaReportes(ContenedorEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = string.Format(@"Select c.idContenedor, c.idSeccion,l.idLocacion,l.idBodega, b.idAlmacen,
                concat(a.Codigo,'-', b.Codigo, '-', l.Codigo,'-',s.Codigo,'-',c.Codigo) as 'CodigoDeAlmacenaje',
                c.Codigo, c.Nombre, c.Descripcion,
                concat(a.Codigo, '-', a.Nombre) as 'Almacen',
                concat(b.Codigo, '-',b.Nombre) as 'Bodega',
                concat(l.Codigo, '-',l.Nombre) as 'Locacion',
                concat(s.Codigo, '-',s.Nombre) as 'Seccion'
                from contenedor as c 
                inner join seccion as s on s.idSeccion = c.idSeccion
                inner join locacion as l on l.idLocacion = s.idLocacion
                inner join bodega as b on b.idAlmacen = l.idBodega
                inner join almacen as a on a.idAlmacen = b.idAlmacen     
                Where c.idContenedor > 0   {0} {1} ", oRegistroEN.Where, oRegistroEN.OrderBy);
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

        public bool ValidarSiElRegistroEstaVinculado(ContenedorEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                Comando.Parameters.Add(new MySqlParameter("@CampoABuscar_", MySqlDbType.VarChar, 200)).Value = "idContenedor";
                Comando.Parameters.Add(new MySqlParameter("@ValorCampoABuscar", MySqlDbType.Int32)).Value = oRegistroEN.idContenedor;
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

        public bool VerificarSiLaEntidadEstaAsociadaAProducto(ContenedorEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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
                where p.TablaDeReferenciaDeAlmacenaje = 'Contenedor' and p.idAlmacenEntidad = {0}) then 1 else 0 end as 'ID'", oRegistroEN.idContenedor);
                
                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                if ( Convert.ToInt32( DT.Rows[0].ItemArray[0].ToString()) == 0)
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

        public bool ValidarRegistroDuplicado(ContenedorEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idContenedor from Contenedor where idSeccion = @idSeccion and upper( trim(Nombre) ) = upper(trim(@Nombre))) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre;
                        Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.oSeccionEN.idSeccion;

                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idContenedor from Contenedor where idSeccion = @idSeccion and upper( trim(Nombre) ) = upper(trim(@Nombre)) and idContenedor <> @idContenedor) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Nombre", MySqlDbType.VarChar, oRegistroEN.Nombre.Trim().Length)).Value = oRegistroEN.Nombre;
                        Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.oSeccionEN.idSeccion;
                        Comando.Parameters.Add(new MySqlParameter("@idContenedor", MySqlDbType.Int32)).Value = oRegistroEN.idContenedor;

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

        public bool ValidarCodigo(ContenedorEN oRegistroEN, DatosDeConexionEN oDatos, string TipoDeOperacion)
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

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idContenedor from Contenedor where idSeccion = @idSeccion and upper( trim(Codigo) ) = upper(trim(@Codigo))) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo;
                        Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.oSeccionEN.idSeccion;
                        break;

                    case "ACTUALIZAR":

                        Consultas = @"SELECT CASE WHEN EXISTS(Select idContenedor from Contenedor where idSeccion = @idSeccion and upper( trim(Codigo) ) = upper(trim(@Codigo)) and idContenedor <> @idContenedor) THEN 1 ELSE 0 END AS 'RES'";
                        Comando.Parameters.Add(new MySqlParameter("@Codigo", MySqlDbType.VarChar, oRegistroEN.Codigo.Trim().Length)).Value = oRegistroEN.Codigo;
                        Comando.Parameters.Add(new MySqlParameter("@idSeccion", MySqlDbType.Int32)).Value = oRegistroEN.oSeccionEN.idSeccion;
                        Comando.Parameters.Add(new MySqlParameter("@idContenedor", MySqlDbType.Int32)).Value = oRegistroEN.idContenedor;

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

        private TransaccionesEN InformacionDelaTransaccion(ContenedorEN oContenedor, String TipoDeOperacion, String Descripcion, String Estado)
        {
            TransaccionesEN oRegistroEN = new TransaccionesEN();

            oRegistroEN.idregistro = oContenedor.idContenedor;
            oRegistroEN.Modelo = "General";
            oRegistroEN.Modulo = "ContenedorAD";
            oRegistroEN.Tabla = "Contenedor";
            oRegistroEN.tipodeoperacion = TipoDeOperacion;
            oRegistroEN.Estado = Estado;
            oRegistroEN.ip = oContenedor.oLoginEN.NumeroIP;
            oRegistroEN.idusuario = oContenedor.oLoginEN.idUsuario;
            oRegistroEN.idusuarioaprueba = oContenedor.oLoginEN.idUsuario;
            oRegistroEN.descripciondelusuario = DescripcionDeOperacion;
            oRegistroEN.descripcioninterna = Descripcion;
            oRegistroEN.NombreDelEquipo = oContenedor.oLoginEN.NombreDelComputador;

            return oRegistroEN;
        }
        
        private string InformacionDelRegistro(ContenedorEN oRegistroEN) {
            string Cadena = @"idContenedor: {0}, Codigo: {7}, Nombre: {1}, Descripcion: {2}, idUsuarioDeCreacion: {3}, FechaDeCreacion: {4}, idUsuarioModificacion: {5}, FechaDeModificacion: {6}";
            Cadena = string.Format(Cadena, oRegistroEN.idContenedor, oRegistroEN.Nombre, oRegistroEN.Descripcion, oRegistroEN.idUsuarioDeCreacion, oRegistroEN.FechaDeCreacion, oRegistroEN.idUsuarioModificacion, oRegistroEN.FechaDeModificacion, oRegistroEN.Codigo);
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
