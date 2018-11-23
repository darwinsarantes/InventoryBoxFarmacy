using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace InventoryBoxFarmacy.Formularios
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        int Segundos = 10;
        bool Iniciado = true;

        private void frmSplash_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Enter))
            {
                this.Close();
            }

            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Escape))
            {
                this.Close();
            }
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            VerificarVersion();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Segundos == 0)
            {
                this.Close();
            }
            else
            {
                Segundos = Segundos - 1;
                lbaTiempoRestante.Text = "Enter para omitir ( " + Segundos.ToString() + " )";
            }
        }

        private void VerificarVersion()
        {

            string Version = "";
            Version = Application.ProductVersion;
            if (Version.Trim().Length > 0)
            {
                lbaVersion.Text = Version;
            }
            else
                lbaVersion.Text = "" + Program.VersionSistema;

        }

        private void lbaTiempoRestante_MouseMove(object sender, MouseEventArgs e)
        {
            if (Iniciado == true)
            {
                timer1.Stop();
                Iniciado = false;
            }
        }

        private void frmSplash_MouseMove(object sender, MouseEventArgs e)
        {
            if (Iniciado == false)
            {
                timer1.Start();
                Iniciado = true;
            }
        }

        private void frmSplash_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        #region "RESPALDO DE BASE DATOS"

        private void RespaldarBaseDeDatos()
        {
            try
            {
                CarpetaDeRespaldo();

                //String NombreDelfichero = string.Format("{0}\\{1}_{2}.sql", Program.oConfiguracionEN.RutaRespaldos.Trim(), Program.oDatosDeConexion.BaseDeDatos.Trim().ToUpper(), System.DateTime.Now.ToString("yyyyMMdd_hms"));
                String NombreDelfichero = string.Format("{0}\\{1}_{2}.sql", Program.oConfiguracionEN.RutaRespaldos.Trim(), Program.oDatosDeConexion.BaseDeDatos.Trim().ToUpper(), System.DateTime.Now.ToString("yyyyMMdd"));

                ValidarSiExisteElFichero(NombreDelfichero);

                System.Diagnostics.Process cmd = new System.Diagnostics.Process();
                cmd.StartInfo.FileName = "cmd.exe";//string.Format(@"{0}mysqldump", Program.oConfiguracionEN.PathMysSQLDump);
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true; //si aplica un true se puede ocultar la ventana o false para mostrarla.
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine(@"c:");
                cmd.StandardInput.Flush();
                cmd.StandardInput.WriteLine(string.Format(@"cd {0}", Program.oConfiguracionEN.PathMysSQLDump.Trim()));
                cmd.StandardInput.Flush();
                cmd.StandardInput.WriteLine(string.Format("mysqldump -h{0} -P{1} -u{2} -p{3} --opt --routines --add-drop-database --databases {4} > {5}",
                    Program.oDatosDeConexion.Servidor, Program.oDatosDeConexion.PuertoDeConexionDelServidor, Program.oDatosDeConexion.Usuario, Program.oDatosDeConexion.Contraseña,
                    Program.oDatosDeConexion.BaseDeDatos, NombreDelfichero));
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.Close();
                cmd = null;

                lbRespaldo.Text = "Copia de seguridad realizada con exito";
                lbRespaldo.ForeColor = Color.Blue;

            }
            catch (Exception ex)
            {
                lbRespaldo.Text = string.Format("Se ha produccido un error al realizar la copia de seguridad: {0} {1}", Environment.NewLine, ex.Message);
                lbRespaldo.ForeColor = Color.Red;
            }
        }

        private void CarpetaDeRespaldo()
        {
            try
            {
                if (!System.IO.Directory.Exists(Program.oConfiguracionEN.RutaRespaldos.Trim()))
                {
                   System.IO.Directory.CreateDirectory(Program.oConfiguracionEN.RutaRespaldos.Trim());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Caperta de respaldo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void ValidarSiExisteElFichero(string Archivo)
        {
            try
            {
                if (System.IO.File.Exists(Archivo))
                {
                    System.IO.File.Delete(Archivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validar si existe el FicheroDeRespaldo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void CargarInformacionDeLaConfiguracion()
        {

            try
            {

                Entidad.ConfiguracionEN oRegistroEN = new Entidad.ConfiguracionEN();
                Logica.ConfiguracionLN oRegistroLN = new Logica.ConfiguracionLN();

                oRegistroEN.IdConfiguracion = 1;

                if (oRegistroLN.ListadoPorIdentificador(oRegistroEN, Program.oDatosDeConexion))
                {

                    DataRow Fila = oRegistroLN.TraerDatos().Rows[0];

                    Program.oConfiguracionEN.RutaRespaldos = Fila["RutaRespaldos"].ToString();
                    Program.oConfiguracionEN.RutaRespaldosDeExcel = Fila["RutaRespaldosDeExcel"].ToString();
                    Program.oConfiguracionEN.PathMysSQLDump = Fila["PathMysSQLDump"].ToString();
                    Program.oConfiguracionEN.PathMySQL = Fila["PathMySQL"].ToString();
                    Program.oConfiguracionEN.NombreDelSistema = Fila["NombreDelSistema"].ToString();
                    Program.oConfiguracionEN.TiempoDeRespaldo = Convert.ToInt32( Fila["TiempoDeRespaldo"].ToString());
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

                    XmlSerializer ser = new XmlSerializer(typeof(Entidad.DatosDeConexionEN));
                    System.IO.FileStream fs = new System.IO.FileStream(RutaDelSistemaDelArchivoDeConexion, System.IO.FileMode.Open);
                    XmlReader reader = XmlReader.Create(fs);

                    Entidad.DatosDeConexionEN oDatos;

                    oDatos = (Entidad.DatosDeConexionEN)ser.Deserialize(reader);
                    fs.Close();

                    if (Program.oDatosDeConexion == null)
                    {
                        Program.oDatosDeConexion = new Entidad.DatosDeConexionEN();
                    }

                    Program.oDatosDeConexion.BaseDeDatos = oDatos.BaseDeDatos;
                    Program.oDatosDeConexion.Contraseña = oDatos.Contraseña;
                    Program.oDatosDeConexion.Usuario = oDatos.Usuario;
                    Program.oDatosDeConexion.Servidor = oDatos.Servidor;
                    Program.oDatosDeConexion.PuertoDeConexionDelServidor = oDatos.PuertoDeConexionDelServidor;
                    
                    oDatos = null;

                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Obtener los datos de la conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarInformaciónDeBaseDatosYRespaldos()
        {
            CargarDatosDeConexionDesDeElXML();
            CargarInformacionDeLaConfiguracion();            
            RespaldarBaseDeDatos();
        }

        #endregion

        private void frmSplash_Shown(object sender, EventArgs e)
        {
            CargarInformaciónDeBaseDatosYRespaldos();
        }

    }
}
