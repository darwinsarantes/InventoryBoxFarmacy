using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace InventoryBoxFarmacy.Formularios
{
    public partial class frmRestaurar : Form
    {
        public frmRestaurar()
        {
            InitializeComponent();
        }

        #region "funciones"

        private void CargarRutaDeRespaldo()
        {
            txtRutaRespaldos.Text = Program.oConfiguracionEN.RutaRespaldos;
        }
        private bool EsCorrectaRutaDeRespaldos()
        {
            bool Salida = true;

            //if (!Directory.Exists(txtRutaRespaldos.Text.Substring(1, txtRutaRespaldos.Text.LastIndexOf(@"\"))))
            if (!Directory.Exists(txtRutaRespaldos.Text))
            {
                Salida = false;
            }

            return Salida;
        }

        private bool DatosDeConexionConServidorCorrectos()
        {
            bool Salida = true;

            if (Program.oDatosDeConexion.Servidor.Length == 0 || Program.oDatosDeConexion.Usuario.Length == 0)
            {
                Salida = false;
            }

            return Salida;

        }

        private void LlenarListadoArchivosDeServidor()
        {
            dgvListaArchivosRespaldo.Rows.Clear();

            try
            {
                if (EsCorrectaRutaDeRespaldos() == false)
                {
                    MessageBox.Show("Verificar la ruta de respaldo", "Listado de archivos del servidor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                DirectoryInfo Directorio = new DirectoryInfo(txtRutaRespaldos.Text);
                FileSystemInfo[] files = Directorio.GetFiles("*.sql");
                var ArchivosOrdenados = files.OrderByDescending(f => f.CreationTime);

                dgvListaArchivosRespaldo.ColumnCount = 4;
                dgvListaArchivosRespaldo.Columns[0].Name = "Name";
                dgvListaArchivosRespaldo.Columns[1].Name = "CreationTime";
                dgvListaArchivosRespaldo.Columns[2].Name = "Extension";
                dgvListaArchivosRespaldo.Columns[3].Name = "FullName";


                foreach (FileInfo f in ArchivosOrdenados)
                {
                    dgvListaArchivosRespaldo.Rows.Add(f.Name, f.CreationTime, f.Extension, f.FullName);
                }

                FormatoDGV();
                dgvListaArchivosRespaldo.ClearSelection();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(string.Format("Error al intentar obtener el listado de archivos de respaldo de la ruta de red establecida. {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatoDGV()
        {
            try
            {
                this.dgvListaArchivosRespaldo.AllowUserToResizeRows = true;
                this.dgvListaArchivosRespaldo.AllowUserToAddRows = false;
                this.dgvListaArchivosRespaldo.AllowUserToDeleteRows = false;
                this.dgvListaArchivosRespaldo.AllowUserToOrderColumns = true;
                this.dgvListaArchivosRespaldo.DefaultCellStyle.BackColor = Color.White;
                this.dgvListaArchivosRespaldo.MultiSelect = false;
                this.dgvListaArchivosRespaldo.RowHeadersVisible = true;
                this.dgvListaArchivosRespaldo.DefaultCellStyle.Font = new Font("Segoe UI", 8);
                this.dgvListaArchivosRespaldo.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8);
                this.dgvListaArchivosRespaldo.DefaultCellStyle.SelectionBackColor = Color.Beige;
                this.dgvListaArchivosRespaldo.DefaultCellStyle.SelectionForeColor = Color.Black;
                this.dgvListaArchivosRespaldo.BackgroundColor = System.Drawing.SystemColors.Window;
                this.dgvListaArchivosRespaldo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                                
                this.dgvListaArchivosRespaldo.Columns["FullName"].Visible = false;
                
                this.dgvListaArchivosRespaldo.Columns["Name"].HeaderText = "Nombre de archivo de respaldo";
                this.dgvListaArchivosRespaldo.Columns["CreationTime"].HeaderText = "Fecha de creación";
                this.dgvListaArchivosRespaldo.Columns["Extension"].HeaderText = "Extensión";

                this.dgvListaArchivosRespaldo.Columns["Name"].Width = 300;
                this.dgvListaArchivosRespaldo.Columns["CreationTime"].Width = 150;
                this.dgvListaArchivosRespaldo.Columns["Extension"].Width = 90;

                this.dgvListaArchivosRespaldo.RowHeadersWidth = 25;

                this.dgvListaArchivosRespaldo.ReadOnly = true;

                this.dgvListaArchivosRespaldo.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvListaArchivosRespaldo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                this.dgvListaArchivosRespaldo.StandardTab = true;
                this.dgvListaArchivosRespaldo.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FormatoDGV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RestaurarCopiaDeSeguridad()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if(dgvListaArchivosRespaldo.RowCount == 0)
                {
                    MessageBox.Show("No hay respaldo de bases de datos listado, por favor realize un respaldo antes o refresque la lista", "Restaurar base de datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return;
                }

                if(dgvListaArchivosRespaldo.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debemos seleccionar un elemento de la lista", "Restaurar base de datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return;
                }

                string NombreArchivoRespaldo = "";              
                NombreArchivoRespaldo = dgvListaArchivosRespaldo.SelectedRows[0].Cells["FullName"].Value.ToString();

                MessageBox.Show(NombreArchivoRespaldo);                
                
                string PathMySQL = string.Format(@"{0}mysql", Program.oConfiguracionEN.PathMySQL);// "C:\wamp64\bin\mysql\mysql5.7.21\bin\\mysql";
                string cmd = string.Format("-u{0} -p{1} -h{2} {3} < {4}", Program.oDatosDeConexion.Usuario, Program.oDatosDeConexion.Contraseña, Program.oDatosDeConexion.Servidor, Program.oDatosDeConexion.BaseDeDatos, NombreArchivoRespaldo);

                MessageBox.Show(PathMySQL);
                ProcessStartInfo DBProcessStartInfo = new ProcessStartInfo(PathMySQL);         
                DBProcessStartInfo.Arguments = cmd;                
                DBProcessStartInfo.RedirectStandardOutput = true;
                DBProcessStartInfo.UseShellExecute = false;

                //Create the process and run it 
                Process dbProcess  = Process.Start(DBProcessStartInfo);

                dbProcess.WaitForExit();
                dbProcess.Close();

                MessageBox.Show("Copia de seguridad reestablecida correctamente con éxito");

            }
            catch(Exception ex)
            {
                MessageBox.Show(string.Format("Se produjo un error durante la restauración de la base de datos: {0} {1}", Environment.NewLine, ex.Message), "Restaurar base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void restaurarBBDDMySql(String path_fichero_mysqldump)
        {
            try
            {
                Process miProceso;
                miProceso = new Process();                
                //Es necesario que el fichero mysql.exe esté en el PATH del sistema
                miProceso.StartInfo.FileName = string.Format(@"{0}\\mysql", Program.oConfiguracionEN.PathMySQL); ;
                miProceso.StartInfo.Arguments = string.Format("-u{0} -p{1} -h{2} {3}", Program.oDatosDeConexion.Usuario, Program.oDatosDeConexion.Contraseña, Program.oDatosDeConexion.Servidor, Program.oDatosDeConexion.BaseDeDatos); ;
                miProceso.StartInfo.RedirectStandardInput = true;
                miProceso.StartInfo.UseShellExecute = false;
                miProceso.Start();

                StreamWriter myStreamWriter = miProceso.StandardInput;
                myStreamWriter.Write(File.ReadAllText(path_fichero_mysqldump, Encoding.Default));
                myStreamWriter.Close();

                miProceso.WaitForExit();
                miProceso.Close();
                MessageBox.Show("Se ha finalizado la restauración de datos con éxito");
            }
            catch (Exception exc)
            {
                MessageBox.Show(string.Format("Se ha producido un error. Mensaje: {0} {1} No se puede Respaldar el Fichero: {1} {2}", exc.Message, Environment.NewLine, path_fichero_mysqldump));
            }
        }

        #endregion

        private void cmdRespaldarBaseDatos_Click(object sender, EventArgs e)
        {
            //RestaurarCopiaDeSeguridad();
            this.Cursor = Cursors.WaitCursor;

            if (dgvListaArchivosRespaldo.RowCount == 0)
            {
                MessageBox.Show("No hay respaldo de bases de datos listado, por favor realize un respaldo antes o refresque la lista", "Restaurar base de datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }

            if (dgvListaArchivosRespaldo.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debemos seleccionar un elemento de la lista", "Restaurar base de datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }

            string NombreArchivoRespaldo = "";
            NombreArchivoRespaldo = dgvListaArchivosRespaldo.SelectedRows[0].Cells["FullName"].Value.ToString();
            restaurarBBDDMySql(NombreArchivoRespaldo);

            this.Cursor = Cursors.Default;
        }

        private void frmRestaurar_Shown(object sender, EventArgs e)
        {
            CargarRutaDeRespaldo();
            LlenarListadoArchivosDeServidor();
        }

        private void cmdRecargarRegistrosRespaldo_Click(object sender, EventArgs e)
        {
            LlenarListadoArchivosDeServidor();
        }
    }
}
