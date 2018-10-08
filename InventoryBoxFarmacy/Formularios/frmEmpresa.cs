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
using Logica;
using Funciones;
using System.Diagnostics;

namespace InventoryBoxFarmacy.Formularios
{
    public partial class frmEmpresa : Form
    {
        public frmEmpresa()
        {
            InitializeComponent();
        }

        int IdEmpresa = 1;

        #region "Funciones"

        private EmpresaEN InformacionDelRegistro() {

            EmpresaEN oRegistroEN = null;

            try
            {
                oRegistroEN = new EmpresaEN();
                oRegistroEN.IdEmpresa = IdEmpresa;
                oRegistroEN.Nombre = txtNombre.Text.Trim();
                oRegistroEN.Descripcion = txtDescripcion.Text.Trim();
                oRegistroEN.Direccion = txtDireccion.Text.Trim();
                oRegistroEN.Telefonos = txtTelefonos.Text.Trim();
                oRegistroEN.Celular = txtCelular.Text.Trim();
                oRegistroEN.Email = txtEmail.Text.Trim();
                oRegistroEN.SitioWeb = txtSitioWeb.Text.Trim();
                oRegistroEN.NRuc = txtNRUC.Text.Trim();

                oRegistroEN.oLoginEN = Program.oLoginEN;
                oRegistroEN.ALogo = Imagenes.ProcesarImagenToByte((Bitmap)(pbxImagen.Image));

                return oRegistroEN;
            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message, "Información del registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return oRegistroEN;

            }

        }

        private void TraerInformacionDelRegisto()
        {

            try {

                EmpresaEN oRegistroEN = new EmpresaEN();
                EmpresaLN oRegistroLN = new EmpresaLN();

                oRegistroEN.IdEmpresa = IdEmpresa;

                if (oRegistroLN.ListadoPorIdentificador(oRegistroEN, Program.oDatosDeConexion)) {

                    DataRow Fila = oRegistroLN.TraerDatos().Rows[0];
                                        
                    txtNombre.Text = Fila["Nombre"].ToString();
                    txtDireccion.Text = Fila["Direccion"].ToString();
                    txtTelefonos.Text = Fila["Telefonos"].ToString();
                    txtNRUC.Text = Fila["NRuc"].ToString();
                    txtCelular.Text = Fila["Celular"].ToString();
                    txtEmail.Text = Fila["Email"].ToString();
                    txtSitioWeb.Text = Fila["SitioWeb"].ToString();
                    txtDescripcion.Text = Fila["Descripcion"].ToString();
                    
                    if (Fila["Logo"] != DBNull.Value)
                    {
                        pbxImagen.Image = Imagenes.ProcesarImagenToBitMap((object)(Fila["Logo"]));
                    }

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0} ", ex.Message), "Traer información del Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        
        private bool LosDatosIngresadosSonCorrectos()
        {

            EP.Clear();
                        
            if ( Controles.IsNullOEmptyElControl(txtNombre)) {
                EP.SetError(txtNombre, "Este valor no puede quedar vacio");
                txtNombre.Focus();
                return false;

            }

            return true;

        }
        
        #endregion

        private void frmEmpresa_Shown(object sender, EventArgs e)
        {
            TraerInformacionDelRegisto();
        }

        private void cmdGuardar_Click(object sender, EventArgs e)
        {

            try
            {

                if (LosDatosIngresadosSonCorrectos())
                {
                    
                    EmpresaEN oRegistroEN = InformacionDelRegistro();
                    EmpresaLN oRegistroLN = new EmpresaLN();

                    if (oRegistroLN.Actualizar(oRegistroEN, Program.oDatosDeConexion))
                    {
                        MessageBox.Show("Registro actualizado correctamente", "Guardar inforamción del registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (chkCerrarVentana.CheckState == CheckState.Checked)
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        throw new ArgumentException(oRegistroLN.Error);
                    }

                }

            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message, "Guardar Información del registro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tsbBuscarLogotipo1_Click(object sender, EventArgs e)
        {
            OpenFileDialog Abrir = new OpenFileDialog();
            Image ImagenLogo;

            //Abrir.InitialDirectory = "c:\\";
            Abrir.Filter = "Archivos de imágenes (*.jpg)|*.jpg|Archivos de imágenes (*.png)|*.png|Archivos de imágenes (*.gif)|*.gif";
            Abrir.FilterIndex = 1;
            Abrir.RestoreDirectory = true;
            Abrir.Title = "Buscar archivos de imágenes compatibles";

            if (Abrir.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((Abrir.OpenFile()) != null)
                    {
                        ImagenLogo = new Bitmap(Abrir.FileName);
                        this.pbxImagen.Image = (Image)ImagenLogo;                                          
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al intentar abrir el archivo: " + ex.Message, "cmdBuscar_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
