using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Office.Interop;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Entidad;
using Logica;

using Funciones;

namespace InventoryBoxFarmacy.Formularios
{
    public partial class frmExportarProveedores : Form
    {
        private Boolean ImportarRegistros = false;

        public frmExportarProveedores()
        {
            InitializeComponent();
        }

        private void btnImportarDesdeExcel_Click(object sender, EventArgs e)
        {

            try
            {
                
                if (Controles.IsNullOEmptyElControl(txtNombreDeLaHoja))
                {
                    throw new ArgumentException("se debe ingresar el nombre de la hoja que exportaremos desde el archivo de excel");
                }

                if (ImportarRegistros == true)
                {
                    string mensaje = "Hay registros pendiente para importación, desea continuar con la carga del Archivo.";
                    if(MessageBox.Show(mensaje, "Importar archivo de excel", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        ImportarRegistros = false;
                    }else
                    {
                        return;
                    }
                }

                ImportarArchivoDeExcel();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Importar Datos desde archivo de excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void ImportarArchivoDeExcel()
        {
            try
            {

                string Hoja = txtNombreDeLaHoja.Text.Trim();

                OpenFileDialog oFileDialogo = new OpenFileDialog();

                oFileDialogo.Filter = "Excel files |*.xlsx";
                oFileDialogo.Title = "Abrir Archivo";

                if (oFileDialogo.ShowDialog() == DialogResult.OK)
                {

                    if (oFileDialogo.FileName.ToString().Trim().Length > 0)
                    {
                        string ArchivoDeExcel = oFileDialogo.FileName.ToString();

                        string CadenaDeConexion = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;data source='{0}';Extended Properties='Excel 12.0 Xml;HDR=Yes'", ArchivoDeExcel);
                        System.Data.OleDb.OleDbConnection Conexion = new System.Data.OleDb.OleDbConnection(CadenaDeConexion);
                        Conexion.Open();

                        string Consulta = string.Format("SELECT * FROM [{0}$] order by Codigo", Hoja);
                        System.Data.OleDb.OleDbCommand Comando = new System.Data.OleDb.OleDbCommand();
                        Comando.Connection = Conexion;
                        Comando.CommandType = CommandType.Text;
                        Comando.CommandText = Consulta;

                        System.Data.OleDb.OleDbDataAdapter Adaptador = new System.Data.OleDb.OleDbDataAdapter();
                        Adaptador.SelectCommand = Comando;
                        DataTable DatosExcel = new DataTable();

                        Adaptador.Fill(DatosExcel);

                        dgvListar.DataSource = DatosExcel;

                        FormatearDGV();

                        Conexion.Close();

                        ImportarRegistros = true;
                    }

                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Importación del archivo de excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FormatearDGV()
        {
            try
            {
                this.dgvListar.AllowUserToResizeRows = false;
                this.dgvListar.AllowUserToAddRows = false;
                this.dgvListar.AllowUserToDeleteRows = false;
                this.dgvListar.DefaultCellStyle.BackColor = Color.White;
                
                this.dgvListar.MultiSelect = false;
                this.dgvListar.RowHeadersVisible = true;
                
                this.dgvListar.DefaultCellStyle.Font = new Font("Segoe UI", 8);
                this.dgvListar.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8);
                this.dgvListar.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
                this.dgvListar.BackgroundColor = System.Drawing.SystemColors.Window;
                this.dgvListar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

                FormatearColumnasDelDGV();

                this.dgvListar.RowHeadersWidth = 25;

                this.dgvListar.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvListar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                this.dgvListar.StandardTab = true;
                this.dgvListar.ReadOnly = false;
                this.dgvListar.CellBorderStyle = DataGridViewCellBorderStyle.Raised;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FormatoDGV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearColumnasDelDGV()
        {
            if (dgvListar.Columns.Count > 0)
            {

                foreach (DataGridViewColumn c1 in dgvListar.Columns)
                {
                    if (c1.Visible == true)
                    {
                        if (c1.Name.Trim().ToUpper() != "Seleccionar".ToUpper())
                        {
                            FormatoDGV oFormato = new FormatoDGV(c1.Name.Trim());
                            if (oFormato.ValorEncontrado == false)
                            {
                                oFormato = new FormatoDGV(c1.Name.Trim(), "Proveedor");
                            }

                            if (oFormato.ValorEncontrado != false)
                            {
                                c1.HeaderText = oFormato.Descripcion;
                                c1.Width = oFormato.Tamano;
                                c1.DefaultCellStyle.Alignment = oFormato.Alineacion;
                                c1.HeaderCell.Style.Alignment = oFormato.AlineacionDelEncabezado;
                                c1.ReadOnly = oFormato.SoloLectura;
                            }
                        }
                    }
                }

            }
        }

        private void btnCerrarVentana_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                btnImportarDesdeExcel.Enabled = false;
                btnCerrarVentana.Enabled = false;

                if (dgvListar.Rows.Count > 0)
                {
                    pbBarra.Visible = true;
                    lbEtiqueta.Visible = true;
                    pbBarra.Maximum = dgvListar.Rows.Count;
                    pbBarra.Minimum = 1;

                    foreach(DataGridViewRow Fila in dgvListar.Rows)
                    {
                        pbBarra.Value = Fila.Index + 1;
                        Fila.Selected = true;
                        ProveedorEN oRegistroEN = InformacionDelProveedor(Fila);
                        ProveedorLN oRegistroLN = new ProveedorLN();

                        if (oRegistroLN.ValidarRegistroDuplicado(oRegistroEN, Program.oDatosDeConexion, "AGREGAR"))
                        {
                            string mensaje = string.Format("Se ha encontrado el siguiente error: {0} {1} {0} Desea continuar ingresando la información", Environment.NewLine, oRegistroLN.Error);

                            if (MessageBox.Show(mensaje, "Guardando información del proveedor", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel ) {
                                throw new ArgumentException("La operación asido cancelada por el usuario");                                
                            }
                            continue;
                        }

                        /*Primero agregaremos la entidad mayor...*/

                        EntidadEN oEntidadEN = informacionDeLaEntidad();
                        EntidadLN oEntidadLN = new EntidadLN();

                        if (oEntidadLN.Agregar(oEntidadEN, Program.oDatosDeConexion))
                        {
                            oRegistroEN.idProveedor = oEntidadEN.idEntidad;
                            if(oRegistroLN.Agregar(oRegistroEN, Program.oDatosDeConexion) == false)
                            {
                                string mensaje = string.Format("Se ha encontrado el siguiente error al Guardar la iformación del Proveedor: {0} {1} {0} Desea continuar ingresando la información", Environment.NewLine, oRegistroLN.Error);
                                oEntidadLN.Eliminar(oEntidadEN, Program.oDatosDeConexion);
                                if (MessageBox.Show(mensaje, "Guardando información del proveedor", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                                {
                                    throw new ArgumentException("La operación asido cancelada por el usuario");
                                }
                            }

                        }else
                        {
                            string mensaje = string.Format("Se ha encontrado el siguiente error: {0} {1} {0} Desea continuar ingresando la información", Environment.NewLine, oEntidadLN.Error);

                            if (MessageBox.Show(mensaje, "Guardando información del proveedor", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                            {
                                throw new ArgumentException("La operación asido cancelada por el usuario");
                            }
                        }



                        /* if (oRegistroLN.AgregarUtilizandoLaMismaConexion(oRegistroEN, Program.oDatosDeConexion) == false)
                         {
                             string mensaje = string.Format("Se ha encontrado el siguiente error: {0} {1} {0} Desea continuar ingresando la información", Environment.NewLine, oRegistroLN.Error);

                             if (MessageBox.Show(mensaje, "Guardando información del proveedor", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                             {
                                 throw new ArgumentException("La operación asido cancelada por el usuario");
                             }
                         }*/

                        lbEtiqueta.Text = string.Format("{0} Registros Guardados de {1}", (Fila.Index + 1).ToString(), dgvListar.Rows.Count);

                    }

                }

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Guardar información del proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                pbBarra.Visible = false;
                btnImportarDesdeExcel.Enabled = true;
                btnCerrarVentana.Enabled = true;
                lbEtiqueta.Visible = false;
                this.Cursor = Cursors.Default;
            }
        }

        private ProveedorEN InformacionDelProveedor(DataGridViewRow Fila)
        {

            ProveedorEN oRegistroEN = new ProveedorEN();
            
            oRegistroEN.Codigo = GenerarCodigoDelProveedor();
            oRegistroEN.idProveedor = 0;
            oRegistroEN.Nombre = Fila.Cells[1].Value.ToString().Trim();
            oRegistroEN.Direccion = Fila.Cells[2].Value.ToString().Trim();
            oRegistroEN.NoRUC = Fila.Cells[3].Value.ToString().Trim();
            oRegistroEN.SitioWeb = Fila.Cells[4].Value.ToString().Trim();
            oRegistroEN.Telefono = Fila.Cells[5].Value.ToString().Trim();
            oRegistroEN.Movil = Fila.Cells[6].Value.ToString().Trim();
            oRegistroEN.Observaciones = Fila.Cells[7].Value.ToString().Trim();
            oRegistroEN.Estado = "ACTIVO";
            oRegistroEN.FechaDeCumpleanos = "";
            oRegistroEN.Messenger = "";
            oRegistroEN.Twitter = "";
            oRegistroEN.Facebook = "";
            oRegistroEN.Skype = "";
            oRegistroEN.Foto = null;
            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.Correo = "";
            oRegistroEN.IdUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.IdUsuarioDeModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;

        }

        private EntidadEN informacionDeLaEntidad()
        {
            EntidadEN oRegistroEN = new EntidadEN();
            
            try
            {
                oRegistroEN.oTipoDeEntidadEN.Nombre = "Proveedor";
                oRegistroEN.oTipoDeEntidadEN.NombreInterno = "proveedor";
                oRegistroEN.oLoginEN = Program.oLoginEN;
                oRegistroEN.IdUsuarioDeCreacion = Program.oLoginEN.idUsuario;
                oRegistroEN.IdUsuarioDeModificacion = Program.oLoginEN.idUsuario;
                oRegistroEN.FechaDeCreacion = System.DateTime.Now;
                oRegistroEN.FechaDeModificacion = System.DateTime.Now;

                return oRegistroEN;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Información de la entidad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return oRegistroEN;
            }
        }

        private string GenerarCodigoDelProveedor()
        {
            
            try
            {
                string Codigo = "";
                ProveedorEN oRegistroEN = new ProveedorEN();
                ProveedorLN oRegistroLN = new ProveedorLN();

                if (oRegistroLN.GenerarCodigoDelProveedor(oRegistroEN, Program.oDatosDeConexion))
                {
                    Codigo = oRegistroEN.Codigo;
                }

                return Codigo;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Generar codigo Automatico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "";
            }
        }

        private void dgvListar_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvListar.IsCurrentCellDirty)
            {
                dgvListar.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

    }

}
