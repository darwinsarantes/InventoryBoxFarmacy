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
    public class LoginAD
    {

        public string Error { set; get; }
        private MySqlConnection Cnn = null;
        private MySqlCommand Comando = null;
        private MySqlDataAdapter Adaptador = null;
        string Consultas;
        private DataTable DT { set; get; }

        #region "Funciones para datos dll"

        public bool IniciarLaSesionDelUsuario(LoginEN oRegistroEN, DatosDeConexionEN oDatos) {

            try
            {

                Cnn = new MySqlConnection(TraerCadenaDeConexion(oDatos));
                Cnn.Open();

                Comando = new MySqlCommand();
                Comando.Connection = Cnn;
                Comando.CommandType = CommandType.Text;

                Consultas = @"Select u.idUsuario, u.idRol, u.Nombre as 'Usuario', u.Email, u.Estado, r.Nombre as 'TipoDeCuenta' from usuario as u
                inner join rol as r on r.idRol = u.idRol
                where r.Estado = 'ACTIVO' and u.Estado = 'ACTIVO' and  upper(trim(u.Login)) = upper(@Login) and  u.Contrasena = @Contrasena ";

                Comando.CommandText = Consultas;

                Comando.Parameters.Add(new MySqlParameter("@Login", MySqlDbType.VarChar, oRegistroEN.Login.Trim().Length)).Value = oRegistroEN.Login.Trim();
                Comando.Parameters.Add(new MySqlParameter("@Contrasena", MySqlDbType.VarChar, oRegistroEN.contraseña.Trim().Length)).Value = oRegistroEN.contraseña;
                                
                Adaptador = new MySqlDataAdapter();
                DT = new DataTable();

                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DT);

                if (DT.Rows.Count > 0) {

                    oRegistroEN.idUsuario = Convert.ToInt32( DT.Rows[0]["idUsuario"].ToString());
                    oRegistroEN.Usuario = DT.Rows[0]["Usuario"].ToString();
                    oRegistroEN.TipoDeCuenta = DT.Rows[0]["TipoDeCuenta"].ToString();

                }

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
                Adaptador = null;

            }

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
