using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Deployment.Application;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using Entidad;
using Logica;
using Funciones;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace InventoryBoxFarmacy.Formularios
{
    public partial class frmLogin : Form
    {
        #region "Procesos y funciones del programador"

        private void CargarInformacionDeLaConfiguracion()
        {

            try
            {

                ConfiguracionEN oRegistroEN = new ConfiguracionEN();
                ConfiguracionLN oRegistroLN = new ConfiguracionLN();

                oRegistroEN.IdConfiguracion = 1;

                if (oRegistroLN.ListadoPorIdentificador(oRegistroEN, Program.oDatosDeConexion))
                {

                    DataRow Fila = oRegistroLN.TraerDatos().Rows[0];

                    Program.oConfiguracionEN.RutaRespaldos = Fila["RutaRespaldos"].ToString();
                    Program.oConfiguracionEN.RutaRespaldosDeExcel = Fila["RutaRespaldosDeExcel"].ToString();
                    Program.oConfiguracionEN.PathMysSQLDump = Fila["PathMysSQLDump"].ToString();
                    Program.oConfiguracionEN.PathMySQL = Fila["PathMySQL"].ToString();
                    Program.oConfiguracionEN.NombreDelSistema = Fila["NombreDelSistema"].ToString();
                    Program.oConfiguracionEN.TiempoDeRespaldo = Convert.ToInt32(Fila["TiempoDeRespaldo"].ToString());
                    Program.oConfiguracionEN.PrecioPorDefecto = Convert.ToInt32(Fila["PrecioPorDefecto"].ToString());

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0} ", ex.Message), "Traer información del Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CargarDatosDeConexionDesDeElXML()
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

                    if (Program.oDatosDeConexion == null)
                    {
                        Program.oDatosDeConexion = new DatosDeConexionEN();
                    }

                    Program.oDatosDeConexion.BaseDeDatos = oDatos.BaseDeDatos;
                    Program.oDatosDeConexion.Contraseña = oDatos.Contraseña;
                    Program.oDatosDeConexion.Usuario = oDatos.Usuario;
                    Program.oDatosDeConexion.Servidor = oDatos.Servidor;
                    Program.oDatosDeConexion.PuertoDeConexionDelServidor = oDatos.PuertoDeConexionDelServidor;

                    txtServidor.Text = oDatos.Servidor;

                    oDatos = null;

                }
                else
                {
                    txtServidor.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Obtener los datos de la conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ObtenerDireccionIP()
        {
            if (Program.oLoginEN == null) { Program.oLoginEN = new LoginEN(); }

            string strHostName = string.Empty;
            strHostName = Dns.GetHostName();
            IPAddress[] hostIPs = Dns.GetHostAddresses(strHostName);

            foreach (IPAddress ip in hostIPs)
            {

                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Program.oLoginEN.NumeroIP = ip.ToString();
                    break;
                }

            }

            Program.oLoginEN.NombreDelComputador = Environment.MachineName.ToString();

        }

        #endregion

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            CargarDatosDeConexionDesDeElXML();
            ObtenerDireccionIP();

            if (!ApplicationDeployment.IsNetworkDeployed)
            {
                txtUsuario.Text = "Administrador";
                txtContraseña.Text = "admin";
            }

        }

        private void frmLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4) {
                this.Hide();
                frmLoginMySQL ofrmLoginMySQL = new frmLoginMySQL();
                ofrmLoginMySQL.ShowDialog();

                CargarDatosDeConexionDesDeElXML();

                this.Show();

            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            LoginUsuario();
        }

        private void LoginUsuario()
        {
            EP.Clear();

            if (string.IsNullOrEmpty(txtUsuario.Text) || txtUsuario.Text.Trim().Length == 0)
            {
                EP.SetError(txtUsuario, "Este campo no puede quedar vacio");
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtContraseña.Text) || txtContraseña.Text.Trim().Length == 0)
            {
                EP.SetError(txtContraseña, "Este campo no puede quedar vacio");
                txtContraseña.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            Program.oLoginEN.contraseña = CifrarCadenas.EncriptarCadena(txtContraseña.Text);
            Program.oLoginEN.Login = txtUsuario.Text.Trim();

            LoginLN oLoginLN = new LoginLN();

            if (oLoginLN.IniciarLaSesionDelUsuario(Program.oLoginEN, Program.oDatosDeConexion))
            {

                if (oLoginLN.TraerDatos().Rows.Count > 0)
                {

                    Program.Iniciar = true;
                    this.Cursor = Cursors.Default;
                    CargarInformacionDeLaConfiguracion();
                    this.Close();
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    Program.Iniciar = false;
                    MessageBox.Show("Usuario o contraseña inválidos: " + oLoginLN.Error, "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUsuario.Focus();
                }

            }
            else
            {

                this.Cursor = Cursors.Default;
                Program.Iniciar = false;
                MessageBox.Show("Usuario o contraseña inválidos: " + oLoginLN.Error, "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUsuario.Focus();

            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Program.Iniciar = false;
            this.Close();
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Program.Iniciar = false;
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                LoginUsuario();
            }
        }
    }
}
