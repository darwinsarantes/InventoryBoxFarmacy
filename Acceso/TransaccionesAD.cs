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
    public class TransaccionesAD
    {

        public string Error { set; get; }
        private MySqlConnection Cnn = null;
        private MySqlCommand Comando = null;
        //private MySqlDataAdapter Adaptador = null;
        string Consultas;
        /*Definicion de variables publicas a la clase*/
        public DateTime Fecha_creacion { set; get; }
        public string NombreDelEquipo { set; get; }
        public string modelo { set; get; }
        public string modulo { set; get; }
        public string tabla { set; get; }
        private DataTable DT { set; get; }

        #region "Funciones para datos dll"
        /// <summary>
        /// Permite agregar una transaccion por cada uno de los eventos que realizemos sobre operaciones como, Agregar, Eliminar y Actualizar
        /// </summary>
        /// <param name="idusuario">Identificador del usuario que esta realizando la operación</param>
        /// <param name="ip">Dirección ip del equipo que esta realizando la transacción</param>
        /// <param name="idregistro">Identificador del registro que estamos Agregando, Actualizando o Eliminando</param>
        /// <param name="tipodeoperacion">Tipo de Operación que estamos realizando</param>
        /// <param name="descripcioninterna">Describir el evento evento que estamos realizando</param>
        /// <param name="estado">Estado del operación según el operador</param>
        /// <param name="descripciondelusuario">Descripción del operador</param>
        /// <param name="idusuarioaprueba">usuario que permite las pruebas</param>
        /// <param name="oDatos">Datos de la conexion con el Servidor</param>
        /// <returns></returns>
        public bool Agregar(int idusuario,string ip,int idregistro,string tipodeoperacion,string descripcioninterna,string estado,string descripciondelusuario,int idusuarioaprueba, DatosDeConexionEN oDatos) {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"insert into inventariotransacciones.transaccionesdeusuario
                (idusuario, Fecha_creacion, ip, NombreDelEquipo, idregistro, tipodeoperacion, 
                descripcioninterna, estado, modelo, modulo, tabla, descripciondelusuario, idusuarioaprueba)
                values
                (@idusuario, @Fecha_creacion, @ip, @NombreDelEquipo, @idregistro, @tipodeoperacion, 
                @descripcioninterna, @estado, @modelo, @modulo, @tabla, @descripciondelusuario, @idusuarioaprueba)
                 ";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idusuario", MySqlDbType.Int32)).Value = idusuario;
                Comando.Parameters.Add(new MySqlParameter("@Fecha_creacion", MySqlDbType.DateTime)).Value = Fecha_creacion;
                Comando.Parameters.Add(new MySqlParameter("@ip", MySqlDbType.VarChar,ip.Trim().Length)).Value = ip.Trim();
                Comando.Parameters.Add(new MySqlParameter("@NombreDelEquipo", MySqlDbType.VarChar, NombreDelEquipo.Trim().Length)).Value = NombreDelEquipo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@idregistro", MySqlDbType.Int32)).Value = idregistro;
                Comando.Parameters.Add(new MySqlParameter("@tipodeoperacion", MySqlDbType.VarChar, tipodeoperacion.Trim().Length)).Value = tipodeoperacion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@descripcioninterna", MySqlDbType.VarChar, descripcioninterna.Trim().Length)).Value = descripcioninterna.Trim();
                Comando.Parameters.Add(new MySqlParameter("@estado", MySqlDbType.VarChar, estado.Trim().Length)).Value = estado.Trim();
                Comando.Parameters.Add(new MySqlParameter("@modelo", MySqlDbType.VarChar, modelo.Trim().Length)).Value = modelo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@modulo", MySqlDbType.VarChar, modulo.Trim().Length)).Value = modulo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@tabla", MySqlDbType.VarChar, tabla.Trim().Length)).Value = tabla.Trim();
                Comando.Parameters.Add(new MySqlParameter("@descripciondelusuario", MySqlDbType.VarChar, descripciondelusuario.Trim().Length)).Value = descripciondelusuario.Trim();
                Comando.Parameters.Add(new MySqlParameter("@idusuarioaprueba", MySqlDbType.Int32)).Value = idusuarioaprueba;

                Comando.ExecuteNonQuery();                

                return true;

            }
            catch (Exception ex)
            {
                this.Error = ex.Message;
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

            }

        }

        public bool Agregar(TransaccionesEN oRegistroEN, DatosDeConexionEN oDatos)
        {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"insert into inventariotransacciones.transaccionesdeusuario
                (idusuario, Fecha_creacion, ip, NombreDelEquipo, idregistro, tipodeoperacion, 
                descripcioninterna, estado, modelo, modulo, tabla, descripciondelusuario, idusuarioaprueba)
                values
                (@idusuario, current_timestamp(), @ip, @NombreDelEquipo, @idregistro, @tipodeoperacion, 
                @descripcioninterna, @estado, @modelo, @modulo, @tabla, @descripciondelusuario, @idusuarioaprueba)
                 ";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@idusuario", MySqlDbType.Int32)).Value = oRegistroEN.idusuario;                
                Comando.Parameters.Add(new MySqlParameter("@ip", MySqlDbType.VarChar, oRegistroEN.ip.Trim().Length)).Value = oRegistroEN.ip.Trim();
                Comando.Parameters.Add(new MySqlParameter("@NombreDelEquipo", MySqlDbType.VarChar, oRegistroEN.NombreDelEquipo.Trim().Length)).Value = oRegistroEN.NombreDelEquipo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@idregistro", MySqlDbType.Int32)).Value = oRegistroEN.idregistro;
                Comando.Parameters.Add(new MySqlParameter("@tipodeoperacion", MySqlDbType.VarChar, oRegistroEN.tipodeoperacion.Trim().Length)).Value = oRegistroEN.tipodeoperacion.Trim();
                Comando.Parameters.Add(new MySqlParameter("@descripcioninterna", MySqlDbType.VarChar, oRegistroEN.descripcioninterna.Trim().Length)).Value = oRegistroEN.descripcioninterna.Trim();
                Comando.Parameters.Add(new MySqlParameter("@estado", MySqlDbType.VarChar, oRegistroEN.Estado.Trim().Length)).Value = oRegistroEN.Estado.Trim();
                Comando.Parameters.Add(new MySqlParameter("@modelo", MySqlDbType.VarChar, oRegistroEN.Modelo.Trim().Length)).Value = oRegistroEN.Modelo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@modulo", MySqlDbType.VarChar, oRegistroEN.Modulo.Trim().Length)).Value = oRegistroEN.Modulo.Trim();
                Comando.Parameters.Add(new MySqlParameter("@tabla", MySqlDbType.VarChar, oRegistroEN.Tabla.Trim().Length)).Value = oRegistroEN.Tabla.Trim();
                Comando.Parameters.Add(new MySqlParameter("@descripciondelusuario", MySqlDbType.VarChar, oRegistroEN.descripciondelusuario.Trim().Length)).Value = oRegistroEN.descripciondelusuario.Trim();
                Comando.Parameters.Add(new MySqlParameter("@idusuarioaprueba", MySqlDbType.Int32)).Value = oRegistroEN.idusuarioaprueba;

                Comando.ExecuteNonQuery();

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

            }

        }

        public String ConvertirValorDeLaCadena(string cadena)
        {
            string CadenaFormateada = "";

            if (cadena.ToUpper() == "ninguna".ToUpper())
            {
                return "";
            }
            else
            {
                string[] ArregloDeCadena = cadena.Split(',');

                foreach (string item in ArregloDeCadena)
                {

                    switch (item.Trim().ToUpper())
                    {
                        case "PROVEEDOR":
                            CadenaFormateada += ", Proveedor / Empresa";
                            break;

                        case "CONFIGURACION":
                            CadenaFormateada += ", Configuración";
                            break;

                        case "ENTIDAD":
                            CadenaFormateada += ", Entidad";
                            break;
                            
                        case "EMPRESA":
                            CadenaFormateada += ", Empresa";
                            break;

                        case "CONTACTO":
                            CadenaFormateada += ", contacto";
                            break;

                        case "INTERFAZ":
                            CadenaFormateada += ", Interfaz";
                            break;

                        case "MODULO":
                            CadenaFormateada += ", Modulo";
                            break;

                        case "MODULOINTERFAZ":
                            CadenaFormateada += ", Modulo interfaz";
                            break;

                        case "MODULOINTERFAZROL":
                            CadenaFormateada += ", Modulo interfaz rol";
                            break;

                        case "MODULOINTERFAZUSUARIO":
                            CadenaFormateada += ", Modulo interfaz usuario";
                            break;

                        case "MONEDA":
                            CadenaFormateada += ", Mondea";
                            break;

                        case "PRIVILEGIO":
                            CadenaFormateada += ", Privilegio";
                            break;

                        case "ROL":
                            CadenaFormateada += ", Rol";
                            break;

                        case "TASADECAMBIO":
                            CadenaFormateada += ", Tasa de cambio";
                            break;

                        case "TIPODEENTIDAD":
                            CadenaFormateada += ", Tipo de entidad";
                            break;

                        case "USUARIO":
                            CadenaFormateada += ", Usuario";
                            break;

                        case "PROVEEDORCONTACTO":
                            CadenaFormateada += ", Proveedor_Contacto";
                            break;

                        case "ALMACEN":
                            CadenaFormateada += ", Almacen";
                            break;

                        case "BODEGA":
                            CadenaFormateada += ", Bodega";
                            break;

                        case "LOCACION":
                            CadenaFormateada += ", Ubicacion del producto";
                            break;

                        case "SECCION":
                            CadenaFormateada += ", Sección";
                            break;

                        case "CONTENEDOR":
                            CadenaFormateada += ", Contenedor de productos";
                            break;

                        case "SECCIONCONTENEDOR":
                            CadenaFormateada += ", Contenedores de la sección";
                            break;

                        case "LOCACIONSECCION":
                            CadenaFormateada += ", Secciones dentro de la locación";
                            break;

                        case "BODEGALOCACION":
                            CadenaFormateada += ", Locaciones de productos dentro de la bodega";
                            break;

                        case "BODEGAALMACEN":
                            CadenaFormateada += ", Bodegas dentro de los almacenes";
                            break;

                        default:
                            CadenaFormateada = " opción no definida."; break;

                    }

                }

                CadenaFormateada = CadenaFormateada.Replace(",", " \n ");

            }

            return CadenaFormateada;
        }

        #endregion

        #region "Funcion para retornar información"

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
