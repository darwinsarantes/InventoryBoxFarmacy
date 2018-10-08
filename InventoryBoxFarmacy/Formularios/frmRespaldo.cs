using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace InventoryBoxFarmacy.Formularios
{
    public partial class frmRespaldo : Form
    {
        public frmRespaldo()
        {
            InitializeComponent();
        }

        #region "Funciones"

        private void TraerInformacion() {
            txtRutaRespaldos.Text = Program.oConfiguracionEN.RutaRespaldos;
        }

        private void copia_de_seguridad()
        {
            try
            {

                //String linea;
                String NombreDelfichero = string.Format("{0}\\{1}_{2}.sql", txtRutaRespaldos.Text.Trim(), Program.oDatosDeConexion.BaseDeDatos.Trim().ToUpper(), System.DateTime.Now.ToString("yyyyMMdd_hms"));
                
                ProcessStartInfo proc = new ProcessStartInfo();                
                proc.UseShellExecute = false;
                proc.RedirectStandardInput = false;
                proc.RedirectStandardOutput = true;
                proc.FileName = string.Format(@"{0}mysqldump", Program.oConfiguracionEN.PathMysSQLDump);// "C:\wamp64\bin\mysql\mysql5.7.21\bin\\mysqldump";
                proc.Arguments = string.Format("-u{0} -p{1} -h{2} {3} > {4}", Program.oDatosDeConexion.Usuario, Program.oDatosDeConexion.Contraseña, Program.oDatosDeConexion.Servidor, Program.oDatosDeConexion.BaseDeDatos, "D:\\dbpruebas\\backup.sql");
                                
                Process miProceso = Process.Start(proc);
                                                
                string res;
                StreamWriter file = new StreamWriter(NombreDelfichero);
                res = miProceso.StandardOutput.ReadToEnd();
                file.WriteLine(res);
                miProceso.WaitForExit();
                file.Close();

                MessageBox.Show("Copia de seguridad realizada con éxito");
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Se ha producido un error al realizar la copia de seguridad: {0} {1}", Environment.NewLine, ex.Message), "Realizar copia se seguiridad de la base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void copia_de_seguridad1()
        {
            try
            {
                String NombreDelfichero = string.Format("{0}\\{1}_{2}.sql", txtRutaRespaldos.Text.Trim(), Program.oDatosDeConexion.BaseDeDatos.Trim().ToUpper(), System.DateTime.Now.ToString("yyyyMMdd_hms"));

                String linea;                        
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.FileName = string.Format(@"{0}mysqldump.exe", Program.oConfiguracionEN.PathMysSQLDump);
                proc.StartInfo.Arguments = string.Format(" {3} -single-transaction -h{2} -u{0} -p{1} ", Program.oDatosDeConexion.Usuario, Program.oDatosDeConexion.Contraseña, Program.oDatosDeConexion.Servidor, Program.oDatosDeConexion.BaseDeDatos);
                Process miProceso;
                miProceso = Process.Start(proc.StartInfo);
                StreamReader sr = miProceso.StandardOutput;
                TextWriter tw = new StreamWriter(NombreDelfichero, false, Encoding.Default);
                while ((linea = sr.ReadLine()) != null)
                {
                    tw.WriteLine(linea);
                }
                tw.Close();

                MessageBox.Show("Copia de seguridad realizada con éxito");
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Se ha producido un error al realizar la copia de seguridad: {0} {1}", Environment.NewLine, ex.Message), "Realizar copia se seguiridad de la base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void copia_de_seguridad2()
        {
            try
            {
                String NombreDelfichero = string.Format("{0}\\{1}_{2}.sql", txtRutaRespaldos.Text.Trim(), Program.oDatosDeConexion.BaseDeDatos.Trim().ToUpper(), System.DateTime.Now.ToString("yyyyMMdd_hms"));                           
                string Pathmysqldump = string.Format(@"{0}mysqldump", Program.oConfiguracionEN.PathMysSQLDump);
                string PathServer = string.Format(" {3} --host={2} --user={0} --password={1} ", Program.oDatosDeConexion.Usuario, Program.oDatosDeConexion.Contraseña, Program.oDatosDeConexion.Servidor, Program.oDatosDeConexion.BaseDeDatos);

                string Conexion = string.Format("{0} {1} > {2}", Pathmysqldump, PathServer, NombreDelfichero);

                System.Diagnostics.Process.Start("CMD.exe", Conexion);

                MessageBox.Show("Copia de seguridad realizada con éxito");

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Se ha producido un error al realizar la copia de seguridad: {0} {1}", Environment.NewLine, ex.Message), "Realizar copia se seguiridad de la base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RespaldarBaseDeDatos()
        {
            try
            {
                CarpetaDeRespaldo();

                //String NombreDelfichero = string.Format("{0}\\{1}_{2}.sql", txtRutaRespaldos.Text.Trim(), Program.oDatosDeConexion.BaseDeDatos.Trim().ToUpper(), System.DateTime.Now.ToString("yyyyMMdd_hms"));
                String NombreDelfichero = string.Format("{0}\\{1}_{2}.sql", txtRutaRespaldos.Text.Trim(), Program.oDatosDeConexion.BaseDeDatos.Trim().ToUpper(), System.DateTime.Now.ToString("yyyyMMdd_hms"));

                //ValidarSiExisteElFichero(NombreDelfichero);

                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";//string.Format(@"{0}mysqldump", Program.oConfiguracionEN.PathMysSQLDump);
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = false;
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

                MessageBox.Show("Copia de seguridad realizada con éxito", "Respaldo de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch(Exception ex)
            {
                MessageBox.Show(string.Format("Se ha producido un error al realizar la copia de seguridad: {0} {1}", Environment.NewLine, ex.Message), "Realizar copia se seguiridad de la base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarpetaDeRespaldo()
        {
            try
            {
                if (!Directory.Exists(txtRutaRespaldos.Text.Trim()))
                {
                    Directory.CreateDirectory(txtRutaRespaldos.Text.Trim());
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
                if (File.Exists(Archivo))
                {
                    File.Delete(Archivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validar si existe el FicheroDeRespaldo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        #endregion

        private void frmRespaldo_Shown(object sender, EventArgs e)
        {
            TraerInformacion();
        }

        private void cmdRespaldarBaseDatos_Click(object sender, EventArgs e)
        {            
            RespaldarBaseDeDatos();
        }
    }
}
