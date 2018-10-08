using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidad;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace InventoryBoxFarmacy
{
    public partial class frmLoginMySQL : Form
    {
        public frmLoginMySQL()
        {
            InitializeComponent();
        }

        #region "Procesos y funciones"

        private Boolean TestDeConexion()
        {
            MySql.Data.MySqlClient.MySqlConnection Cnn = new MySql.Data.MySqlClient.MySqlConnection(GenerarCadenaDeConexion());

            try {

                Cnn.Open();

                MessageBox.Show("Conexión establecida correctamente", "Test de conexión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;

            }catch(Exception ex)
            {
                MessageBox.Show(string.Format("Error al realizar la conexión: {0} {1}", Environment.NewLine,ex.Message), "Test de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {

                if (Cnn.State == ConnectionState.Open) {
                    Cnn.Close();
                }

                Cnn = null;

            }

        }

        private string GenerarCadenaDeConexion() {

            string Cadena = string.Format("Data Source='{0}';Initial Catalog='{1}';Persist Security Info=True;User ID='{2}';Password='{3}'", txtServidor.Text.Trim(),txtBaseDeDatos.Text.Trim(), txtUsuario.Text.Trim(), txtContraseña.Text.Trim());
            return Cadena;

        }

        private void CargarDatosDeConexionV1()
        {

            try
            {

                if (Program.oDatosDeConexion == null)
                {

                    Program.oDatosDeConexion = new DatosDeConexionEN();

                }

                Program.oDatosDeConexion.Servidor = txtServidor.Text;
                Program.oDatosDeConexion.Usuario = txtUsuario.Text;
                Program.oDatosDeConexion.BaseDeDatos = txtBaseDeDatos.Text;
                Program.oDatosDeConexion.Contraseña = txtContraseña.Text;
                Program.oDatosDeConexion.PuertoDeConexionDelServidor = txtPuertoDeServidor.Text;

            }
            catch (Exception ex)
            {

                MessageBox.Show(string.Format("Error al guardar la información de los datos de conexión: {0} {1}", Environment.NewLine, ex.Message), "Guardar información de los datos de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void LerXMLConexion()
        {

            try
            {

                string RutaDelSistemaDelArchivoDeConexion = string.Format("{0}\\ConexionDeDatos1.xml", Application.StartupPath.ToString());

                if (System.IO.File.Exists(RutaDelSistemaDelArchivoDeConexion))
                {

                    XmlSerializer ser = new XmlSerializer(typeof(DatosDeConexionEN));
                    FileStream fs = new FileStream(RutaDelSistemaDelArchivoDeConexion, FileMode.Open);
                    XmlReader reader = XmlReader.Create(fs);

                    DatosDeConexionEN oDatos;

                    oDatos = (DatosDeConexionEN)ser.Deserialize(reader);
                    fs.Close();

                    txtBaseDeDatos.Text = oDatos.BaseDeDatos;
                    txtContraseña.Text = oDatos.Contraseña;
                    txtUsuario.Text = oDatos.Usuario;
                    txtServidor.Text = oDatos.Servidor;
                    txtPuertoDeServidor.Text = oDatos.PuertoDeConexionDelServidor;


                }
                else
                {
                    txtBaseDeDatos.Text = string.Empty;
                    txtContraseña.Text = string.Empty;
                    txtPuertoDeServidor.Text = "3306";
                    txtServidor.Text = string.Empty;
                    txtUsuario.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Leer Datos para conexión con el servidor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void GuardarInformacionDelSerVidorEnXML()
        {
            try
            {
                string RutaDelSistemaDelArchivoDeConexion = string.Format("{0}\\ConexionDeDatos1.xml", Application.StartupPath.ToString());

                if (System.IO.File.Exists(RutaDelSistemaDelArchivoDeConexion))
                {
                    System.IO.File.Delete(RutaDelSistemaDelArchivoDeConexion);
                }

                XmlSerializer ser = new XmlSerializer(Program.oDatosDeConexion.GetType());
                TextWriter Writer = new StreamWriter(RutaDelSistemaDelArchivoDeConexion);
                ser.Serialize(Writer, Program.oDatosDeConexion);
                Writer.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Guardar Información de la conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void GuardarXMLConexion()
        {
            if (TestDeConexion())
            {
                CargarDatosDeConexionV1();
                GuardarInformacionDelSerVidorEnXML();
                this.Close();
            }
        }


        #endregion

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTestDeConexion_Click(object sender, EventArgs e)
        {
            GuardarXMLConexion();
        }

        private void frmLoginMySQL_Shown(object sender, EventArgs e)
        {
            LerXMLConexion();
        }
       
    }
}
