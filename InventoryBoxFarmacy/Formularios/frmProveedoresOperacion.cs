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

namespace InventoryBoxFarmacy.Formularios
{
    public partial class frmProveedoresOperacion : Form
    {
        public frmProveedoresOperacion()
        {
            InitializeComponent();
        }

        public string OperacionARealizar { set; get; }
        public string NOMBRE_ENTIDAD_PRIVILEGIO { set; get; }
        public string NombreEntidad { set; get; }
        public int ValorLlavePrimariaEntidad { set; get; }

        private bool CerrarVentana = false;

        private void frmProveedoresOperacion_Shown(object sender, EventArgs e)
        {
            configuracionDeVentana();
            ObtenerValoresDeConfiguracion();
            LlamarMetodoSegunOperacion();
            EstablecerTituloDeVentana();
            DeshabilitarControlesSegunOperacionesARealizar();

        }

        private void configuracionDeVentana()
        {
            try
            {

                tsbGuardar.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbActualizar.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbEliminar.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbLimpiarCampos.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbRecarRegistro.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbCerrarVentan.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbImprimir.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Configuración de la ventana", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        #region "Funciones"

        private void EvaluarErrorParaMensajeAPantalla(String Error, string TipoOperacion)
        {
            if (string.IsNullOrEmpty(Error) || Error.Trim().Length == 0)
            {
                Error = string.Empty;
                MessageBox.Show(string.Format("Operación '{0}' realizada correctamente", TipoOperacion), string.Format("{0} Registro", TipoOperacion), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string cadena = "";
                switch (TipoOperacion.ToUpper())
                {
                    case "GUARDAR":
                        cadena = "Registro Guardado correctamente pero con errores: ";
                        break;
                    case "ACTUALIZAR":
                        cadena = "Registros Actualizado correctamente pero con errores: ";
                        break;
                    case "ELIMINAR":
                        cadena = "Registro Eliminado Correctamente pero con errores: ";
                        break;
                }

                cadena = string.Format("{0} {1} {1} {2}", cadena, Environment.NewLine, Error);

                MessageBox.Show(cadena, string.Format("{0} Registro", TipoOperacion), MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void CargarPrivilegiosDelUsuario()
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;

                ModuloInterfazUsuariosEN oRegistroEN = new ModuloInterfazUsuariosEN();
                ModuloInterfazUsuariosLN oRegistroLN = new ModuloInterfazUsuariosLN();

                oRegistroEN.oUsuarioEN.idUsuario = Program.oLoginEN.idUsuario;
                oRegistroEN.oPrivilegioEN.oModuloInterfazEN.oInterfazEN.Nombre = NOMBRE_ENTIDAD_PRIVILEGIO;

                if (oRegistroLN.ListadoPrivilegiosDelUsuariosPorIntefaz(oRegistroEN, Program.oDatosDeConexion))
                {

                    tsbActualizar.Enabled = oRegistroLN.VerificarSiTengoAcceso("Actualizar");

                    if (tsbActualizar.Enabled == true) {

                        DeshabilitarControlesSegunOperacionesARealizar();

                    }
                    else
                    {
                        MessageBox.Show("No tiene privilegio para modificar el registro, la ventana se cerrara", "Privilegios de Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.Close();
                    }


                }
                else
                {

                    throw new ArgumentException(oRegistroLN.Error);

                }

                oRegistroEN = null;
                oRegistroLN = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Privilegios de Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void ObtenerValoresDeConfiguracion()
        {
            chkCerrarVentana.CheckState = (Properties.Settings.Default.ProveedoresVentanaDespuesDeOperacion == true ? CheckState.Checked : CheckState.Unchecked);
            this.CerrarVentana = (Properties.Settings.Default.ProveedoresVentanaDespuesDeOperacion == true ? true : false);
        }

        private void LlamarMetodoSegunOperacion()
        {
            switch (this.OperacionARealizar.ToUpper())
            {
                case "NUEVO":
                    Nuevo();
                    break;

                case "MODIFICAR":
                    Modificar();
                    break;

                case "ELIMINAR":
                    Eliminar();
                    break;

                case "CONSULTAR":
                    Consultar();
                    break;

                case "NUEVO A PARTIR DE REGISTRO SELECCIONADO":
                    NuevoAPartirDeRegistroSeleccionado();
                    break;

                default:
                    MessageBox.Show("La operación solicitada no está disponible.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

            }
        }

        private void DeshabilitarControlesSegunOperacionesARealizar()
        {
            switch (this.OperacionARealizar.ToUpper())
            {
                case "NUEVO":
                    tsbGuardar.Visible = true;
                    tsbLimpiarCampos.Visible = true;
                    tsbActualizar.Visible = false;
                    tsbEliminar.Visible = false;
                    tsbRecarRegistro.Visible = false;
                    tsbImprimir.Visible = false;

                    txtIdentificador.ReadOnly = true;
                    txtCodigo.ReadOnly = true;
                    txtDireccion.Text = string.Empty;

                    break;

                case "MODIFICAR":
                    tsbGuardar.Visible = false;
                    tsbLimpiarCampos.Visible = false;
                    tsbActualizar.Visible = true;
                    tsbEliminar.Visible = false;
                    tsbRecarRegistro.Visible = true;
                    tsbImprimir.Visible = true;

                    txtIdentificador.ReadOnly = true;
                    txtCodigo.ReadOnly = true;

                    break;

                case "ELIMINAR":
                    tsbGuardar.Visible = false;
                    tsbLimpiarCampos.Visible = false;
                    tsbActualizar.Visible = false;
                    tsbEliminar.Visible = true;
                    tsbRecarRegistro.Visible = false;
                    tsbImprimir.Visible = true;

                    chkCerrarVentana.CheckState = CheckState.Checked;
                    chkCerrarVentana.Enabled = false;
                    txtIdentificador.ReadOnly = true;

                    txtDireccion.ReadOnly = true;
                    
                    break;

                case "CONSULTAR":
                    tsbGuardar.Visible = false;
                    tsbLimpiarCampos.Visible = false;
                    tsbActualizar.Visible = false;
                    tsbEliminar.Visible = false;
                    tsbRecarRegistro.Visible = true;
                    tsbImprimir.Visible = true;

                    txtIdentificador.ReadOnly = true;

                    chkCerrarVentana.CheckState = CheckState.Checked;
                    chkCerrarVentana.Enabled = false;
                    txtDireccion.ReadOnly = true;


                    break;

                default:
                    MessageBox.Show("La operación solicitada no está disponible.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

            }
        }

        private void GenerarCodigoDelProveedor()
        {
            try
            {

                ProveedorEN oRegistroEN = new ProveedorEN();
                ProveedorLN oRegistroLN = new ProveedorLN();

                if (oRegistroLN.GenerarCodigoDelProveedor(oRegistroEN, Program.oDatosDeConexion))
                {
                    txtCodigo.Text = oRegistroEN.Codigo;
                }

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Generar codigo Automatico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Nuevo()
        {
            GenerarCodigoDelProveedor();
            CrearColumnasDGVContactos();
            CrearColumnasDGVLaboratorios();
        }

        private void Modificar()
        {
            txtIdentificador.Text = ValorLlavePrimariaEntidad.ToString();
            LlenarCamposDesdeBaseDatosSegunID();
        }

        private void Eliminar()
        {
            txtIdentificador.Text = ValorLlavePrimariaEntidad.ToString();
            LlenarCamposDesdeBaseDatosSegunID();
        }

        private void Consultar()
        {
            txtIdentificador.Text = ValorLlavePrimariaEntidad.ToString();
            LlenarCamposDesdeBaseDatosSegunID();
        }

        private void NuevoAPartirDeRegistroSeleccionado()
        {
            LlenarCamposDesdeBaseDatosSegunID();
        }

        private void LlenarCamposDesdeBaseDatosSegunID()
        {
            this.Cursor = Cursors.WaitCursor;

            ProveedorEN oRegistrosEN = new ProveedorEN();
            ProveedorLN oRegistrosLN = new ProveedorLN();

            oRegistrosEN.idProveedor = ValorLlavePrimariaEntidad;

            if (oRegistrosLN.ListadoPorIdentificador(oRegistrosEN, Program.oDatosDeConexion))
            {
                if (oRegistrosLN.TraerDatos().Rows.Count > 0)
                {

                    DataRow Fila = oRegistrosLN.TraerDatos().Rows[0];
                    txtCodigo.Text = Fila["Codigo"].ToString();
                    txtNombre.Text = Fila["Nombre"].ToString();
                    txtDireccion.Text = Fila["Direccion"].ToString();
                    txtNoRUC.Text = Fila["NoRUC"].ToString();
                    txtSitioWeb.Text = Fila["SitioWeb"].ToString();
                    txtTelefono.Text = Fila["Telefono"].ToString();
                    txtMovil.Text = Fila["Movil"].ToString();
                    txtObservaciones.Text = Fila["Observaciones"].ToString();
                    txtCorreo.Text = Fila["Correo"].ToString();
                    txtFechaDeCumpleanos.Text = Fila["FechaDeCumpleanos"].ToString();
                    txtMessenger.Text = Fila["Messenger"].ToString();
                    txtSkype.Text = Fila["Skype"].ToString();
                    txtTwitter.Text = Fila["Twitter"].ToString();
                    txtFaceBook.Text = Fila["Facebook"].ToString();
                    cmbEstado.Text = Fila["Estado"].ToString();

                    if (Controles.IsNullOEmptyElControl(txtFechaDeCumpleanos) == false)
                    {
                        dtpkFecha.Value = Convert.ToDateTime(txtFechaDeCumpleanos.Text);
                    }

                    if (Fila["Foto"] != DBNull.Value)
                    {
                        pbxImagen.Image = Imagenes.ProcesarImagenToBitMap((object)(Fila["Foto"]));
                    }

                    CrearyYPoblarColumnasDGVContacto();
                    CrearyYPoblarColumnasDGVLaboratorio();

                    oRegistrosEN = null;
                    oRegistrosLN = null;

                }
                else
                {
                    string Mensaje;
                    Mensaje = string.Format("El registro solicitado de {0} no ha sido encontrado."
                                            + "\n\r-----Causas---- "
                                            + "\n\r1. Este registro pudo haber sido eliminado por otro usuario."
                                            + "\n\r2. El listado no está actualizado.", NombreEntidad);

                    MessageBox.Show(Mensaje, "Listado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    oRegistrosEN = null;
                    oRegistrosLN = null;

                    this.Close();
                }

            }
            else
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(oRegistrosLN.Error, "Listado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                oRegistrosEN = null;
                oRegistrosLN = null;
            }

            this.Cursor = Cursors.Default;
        }

        private void EstablecerTituloDeVentana()
        {
            this.Text = string.Format("{0} - {1}", this.NombreEntidad, this.OperacionARealizar.ToUpper());
            this.InformacionEntidadOperacion.Text = this.NombreEntidad + " - " + this.OperacionARealizar;
        }

        private void LimpiarCampos()
        {
            //txtId.Text = string.Empty;
            txtDireccion.Text = string.Empty;

        }

        private void GuardarValoresDeConfiguracion()
        {
            Properties.Settings.Default.ProveedoresVentanaDespuesDeOperacion = (chkCerrarVentana.CheckState == CheckState.Checked ? true : false);
            Properties.Settings.Default.Save();
        }

        private void LimpiarEP()
        {
            EP.Clear();
        }

        private bool LosDatosIngresadosSonCorrectos()
        {
            LimpiarEP();

            if (Controles.IsNullOEmptyElControl(txtNombre))
            {
                EP.SetError(txtDireccion, "Este campo no puede quedar vacío");
                txtDireccion.Focus();
                return false;
            }


            return true;

        }

        private ProveedorEN InformacionDelRegistro() {

            ProveedorEN oRegistroEN = new ProveedorEN();

            oRegistroEN.idProveedor = Convert.ToInt32((txtIdentificador.Text.Length > 0 ? txtIdentificador.Text : "0"));
            oRegistroEN.Nombre = txtNombre.Text.Trim();
            oRegistroEN.Codigo = txtCodigo.Text.Trim();
            oRegistroEN.Direccion = txtDireccion.Text.Trim();
            oRegistroEN.NoRUC = txtNoRUC.Text.Trim();
            oRegistroEN.SitioWeb = txtSitioWeb.Text.Trim();
            oRegistroEN.Telefono = txtTelefono.Text.Trim();
            oRegistroEN.Movil = txtMovil.Text.Trim();
            oRegistroEN.Observaciones = txtObservaciones.Text.Trim();
            oRegistroEN.Correo = txtCorreo.Text.Trim();
            oRegistroEN.FechaDeCumpleanos = txtFechaDeCumpleanos.Text.Trim();
            oRegistroEN.Messenger = txtMessenger.Text.Trim();
            oRegistroEN.Skype = txtSkype.Text.Trim();
            oRegistroEN.Twitter = txtTwitter.Text.Trim();
            oRegistroEN.Facebook = txtFaceBook.Text.Trim();
            oRegistroEN.Estado = cmbEstado.Text.Trim();

            if (pbxImagen.Image != null)
                oRegistroEN.AFoto = Imagenes.ProcesarImagenToByte((Bitmap)(pbxImagen.Image));
            else
                oRegistroEN.AFoto = null;

            //partes generales.            
            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.IdUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.IdUsuarioDeModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;

        }

        private DataTable InformacionDeLosContactosAsociadosAlProveedor()
        {
            DataTable ODatos = null;
            try
            {

                ProveedorContactoEN oRegistroEN = new ProveedorContactoEN();
                ProveedorContactoLN oRegistroLN = new ProveedorContactoLN();

                oRegistroEN.oProveedorEN.idProveedor = ValorLlavePrimariaEntidad;

                if(oRegistroLN.ListadoPorIdentificadorDelProveedor(oRegistroEN, Program.oDatosDeConexion))
                {

                    ODatos = oRegistroLN.TraerDatos();
                    return ODatos; 

                }
                else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Información de los contactos asociados al proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return ODatos;
            }
            
        }

        private void CrearyYPoblarColumnasDGVContacto()
        {
            try
            {
                       
                CrearColumnasDGVContactos();

                DataTable DTOrden = InformacionDeLosContactosAsociadosAlProveedor();

                if (DTOrden != null)
                {

                    if (DTOrden.Rows.Count > 0)
                    {
                        int i = 1;
                        Boolean valor = false;
                        if (OperacionARealizar == "Eliminar") { valor = true; } else { valor = false; }

                        int idProveedorContacto = 0;
                        int idContacto = 0;

                        foreach (DataRow row in DTOrden.Rows)
                        {

                            if (OperacionARealizar.ToUpper() == "NUEVO A PARTIR DE REGISTRO SELECCIONADO".ToUpper())
                            {
                                idProveedorContacto = 0;
                                idContacto = 0;
                            }
                            else
                            {
                                idContacto = Convert.ToInt32(row["idContacto"]);
                                idProveedorContacto = Convert.ToInt32(row["idProveedorContacto"]);
                            }
                            
                            dgvListarContacto.Rows.Add(
                                valor,
                                idProveedorContacto,
                                idContacto,
                                row["Codigo"],
                                row["Nombre"],
                                row["Cedula"],
                                row["Direccion"],
                                row["Telefono"],
                                row["Movil"],
                                row["Sexo"],
                                row["Observaciones"],
                                row["Correo"],
                                row["FechaDeCumpleanos"],
                                row["Messenger"],
                                row["Skype"],
                                row["Twitter"],
                                row["Facebook"],
                                row["Estado"],
                                valor
                                );

                            i++;

                        }
                        
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crear y Poblar Columnas, para los contactos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void OcultarColumnasEnElDGVParaContacto(String ColumnasDelDGV)
        {
            if (dgvListarContacto.Columns.Count > 0)
            {
                String[] ArrayColumnasDGV = ColumnasDelDGV.Split(',');
                foreach (String c in ArrayColumnasDGV)
                {

                    foreach (DataGridViewColumn c1 in dgvListarContacto.Columns)
                    {
                        if (c1.Name.Trim().ToUpper() == c.Trim().ToUpper())
                        {
                            c1.Visible = false;
                        }
                    }

                }
            }
        }
        
        private void FormatearDGVParaElContacto()
        {
            try
            {
                this.dgvListarContacto.AllowUserToResizeRows = false;
                this.dgvListarContacto.AllowUserToAddRows = false;
                this.dgvListarContacto.AllowUserToDeleteRows = false;
                this.dgvListarContacto.DefaultCellStyle.BackColor = Color.White;

                this.dgvListarContacto.MultiSelect = false;
                this.dgvListarContacto.RowHeadersVisible = true;

                this.dgvListarContacto.DefaultCellStyle.Font = new Font("Segoe UI", 8);
                this.dgvListarContacto.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8);
                this.dgvListarContacto.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
                this.dgvListarContacto.BackgroundColor = System.Drawing.SystemColors.Window;
                this.dgvListarContacto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

                string OcultarColumnas = "idProveedorContacto,Cedula,idContacto,Sexo,Observaciones,FechaDeCumpleanos,Estado,Actualizar";
                OcultarColumnasEnElDGVParaContacto(OcultarColumnas);

                FormatearColumnasDelDGVParContacto();

                if (OperacionARealizar == "Eliminar")
                {
                    dgvListarContacto.DefaultCellStyle.Font = new Font(Font.Name, Font.Size, FontStyle.Strikeout);
                    dgvListarContacto.DefaultCellStyle.ForeColor = Color.Red;
                    dgvListarContacto.DefaultCellStyle.SelectionForeColor = Color.Red;
                }
                else
                {
                    dgvListarContacto.DefaultCellStyle.Font = new Font(Font.Name, Font.Size, FontStyle.Regular);
                    dgvListarContacto.DefaultCellStyle.ForeColor = Color.Black;
                    dgvListarContacto.DefaultCellStyle.SelectionForeColor = Color.Black;
                }

                if (OperacionARealizar.ToUpper() == "ELIMINAR".ToUpper() || OperacionARealizar.ToUpper() == "CONSULTAR".ToUpper())
                {
                    dgvListarContacto.Columns["Eliminar"].ReadOnly = true;
                }

                this.dgvListarContacto.RowHeadersWidth = 25;

                this.dgvListarContacto.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvListarContacto.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                this.dgvListarContacto.StandardTab = true;
                this.dgvListarContacto.ReadOnly = false;
                this.dgvListarContacto.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FormatoDGV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void FormatearColumnasDelDGVParContacto()
        {
            if (dgvListarContacto.Columns.Count > 0)
            {

                foreach (DataGridViewColumn c1 in dgvListarContacto.Columns)
                {
                    if (c1.Visible == true)
                    {
                        if (c1.Name.Trim().ToUpper() != "Seleccionar".ToUpper())
                        {
                            FormatoDGV oFormato = new FormatoDGV(c1.Name.Trim());
                            if (oFormato.ValorEncontrado == false)
                            {
                                oFormato = new FormatoDGV(c1.Name.Trim(), "Contacto");
                            }

                            if (oFormato != null)
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

        private void CrearColumnasDGVContactos()
        {
            try
            {
                
                string columnas = @"idProveedorContacto, idContacto, Codigo, Nombre, Cedula, 
                Direccion, Telefono, Movil, Sexo,Observaciones, Correo, FechaDeCumpleanos, 
                Messenger, Skype, Twitter, Facebook, Estado";

                string[] arrayColumnas = columnas.Split(',');

                dgvListarContacto.Columns.Clear();
                dgvListarContacto.ColumnCount = arrayColumnas.Length;

                DataGridViewCheckBoxColumn col1 = new DataGridViewCheckBoxColumn();
                dgvListarContacto.Columns.Insert(0, col1);
                dgvListarContacto.Columns[0].Name = "Eliminar";

                int j = 1;
                foreach (string item in arrayColumnas)
                {
                    dgvListarContacto.Columns[j].Name = item.Trim();
                    j++;
                }

                DataGridViewCheckBoxColumn col2 = new DataGridViewCheckBoxColumn();
                dgvListarContacto.Columns.Insert(j, col2);
                dgvListarContacto.Columns[j].Name = "Actualizar";

                FormatearDGVParaElContacto();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar la lista. \n" + ex.Message, "PoblarColumnasdgvLista", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void tsbCerrarVentan_Click(object sender, EventArgs e)
        {
            GuardarValoresDeConfiguracion();
            this.Close();
        }

        private void tsbLimpiarCampos_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void chkCerrarVentana_CheckedChanged(object sender, EventArgs e)
        {
            this.CerrarVentana = (chkCerrarVentana.CheckState == CheckState.Checked ? true : false);
        }

        private void frmProveedoresOperacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            GuardarValoresDeConfiguracion();
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (LosDatosIngresadosSonCorrectos())
                {

                    ProveedorEN oRegistroEN = InformacionDelRegistro();
                    ProveedorLN oRegistroLN = new ProveedorLN();

                    if (oRegistroLN.ValidarRegistroDuplicado(oRegistroEN, Program.oDatosDeConexion, "AGREGAR"))
                    {

                        MessageBox.Show(oRegistroLN.Error, "Guardar información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                    EntidadEN oEntidadEN = informacionDeLaEntidad();
                    EntidadLN oEntidadLN = new EntidadLN();

                    if (oEntidadLN.Agregar(oEntidadEN, Program.oDatosDeConexion))
                    {
                        oRegistroEN.idProveedor = oEntidadEN.idEntidad;

                        if (oRegistroLN.Agregar(oRegistroEN, Program.oDatosDeConexion))
                        {
                            txtIdentificador.Text = oRegistroEN.idProveedor.ToString();
                            ValorLlavePrimariaEntidad = oRegistroEN.idProveedor;
                            txtCodigo.Text = oRegistroEN.Codigo;
                            
                            EvaluarErrorParaMensajeAPantalla(oRegistroLN.Error, "Guardar");

                            oRegistroEN = null;
                            oRegistroLN = null;

                            this.Cursor = Cursors.Default;

                            if (InsertarActyalizarYEliminarContacto() && InsertarActyalizarYEliminarLaboratorio())
                            {

                                if (CerrarVentana == true)
                                {
                                    this.Close();
                                }
                                else
                                {
                                    OperacionARealizar = "Modificar";
                                    ObtenerValoresDeConfiguracion();
                                    LlamarMetodoSegunOperacion();
                                    EstablecerTituloDeVentana();
                                    DeshabilitarControlesSegunOperacionesARealizar();
                                }

                            }else
                            {
                                OperacionARealizar = "Modificar";
                                ObtenerValoresDeConfiguracion();
                                LlamarMetodoSegunOperacion();
                                EstablecerTituloDeVentana();
                                DeshabilitarControlesSegunOperacionesARealizar();
                            }

                        } else
                        {
                            throw new ArgumentException(oRegistroLN.Error);
                        }

                    } else
                    {
                        string mensaje = string.Format("Se ha encontrado el siguiente error: {0} {1} {0}", Environment.NewLine, oEntidadLN.Error);
                        throw new ArgumentException(mensaje);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Guardar la información del registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                this.Cursor = Cursors.Default;
            }
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Información de la entidad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return oRegistroEN;
            }
        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (LosDatosIngresadosSonCorrectos())
                {
                    if (txtIdentificador.Text.Length == 0 || txtIdentificador.Text == "0")
                    {
                        MessageBox.Show("No se puede continuar. Ha ocurrido un error y el registro no ha sido cargado correctamente.", OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }


                    if (MessageBox.Show("¿Está seguro que desea aplicar los cambios al registro seleccionado?", "Actualizar la Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    ProveedorEN oRegistroEN = InformacionDelRegistro();
                    ProveedorLN oRegistroLN = new ProveedorLN();

                    if (oRegistroLN.ValidarSiElRegistroEstaVinculado(oRegistroEN, Program.oDatosDeConexion, "ACTUALIZAR"))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (oRegistroLN.ValidarRegistroDuplicado(oRegistroEN, Program.oDatosDeConexion, "ACTUALIZAR"))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(oRegistroLN.Error, "Actualizar la información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                    if (oRegistroLN.Actualizar(oRegistroEN, Program.oDatosDeConexion))
                    {

                        txtIdentificador.Text = oRegistroEN.idProveedor.ToString();
                        ValorLlavePrimariaEntidad = oRegistroEN.idProveedor;

                        if (InsertarActyalizarYEliminarContacto() && InsertarActyalizarYEliminarLaboratorio())
                        {

                            EvaluarErrorParaMensajeAPantalla(oRegistroLN.Error, "Actualizar");

                            oRegistroEN = null;
                            oRegistroLN = null;

                            this.Cursor = Cursors.Default;

                            if (CerrarVentana == true)
                            {
                                this.Close();
                            }

                        }

                    }
                    else
                    {
                        throw new ArgumentException(oRegistroLN.Error);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Actualizar la información del registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (LosDatosIngresadosSonCorrectos())
                {
                    if (txtIdentificador.Text.Length == 0 || txtIdentificador.Text == "0")
                    {
                        MessageBox.Show("No se puede continuar. Ha ocurrido un error y el registro no ha sido cargado correctamente.", OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }


                    if (MessageBox.Show("¿Está seguro que desea eliminar el registro?", "Eliminar la Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    ProveedorEN oRegistroEN = InformacionDelRegistro();
                    ProveedorLN oRegistroLN = new ProveedorLN();

                    if (oRegistroLN.ValidarSiElRegistroEstaVinculado(oRegistroEN, Program.oDatosDeConexion, "ELIMINAR"))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (InsertarActyalizarYEliminarContacto() && InsertarActyalizarYEliminarLaboratorio())
                    {

                        if (oRegistroLN.Eliminar(oRegistroEN, Program.oDatosDeConexion))
                        {

                            txtIdentificador.Text = oRegistroEN.idProveedor.ToString();
                            ValorLlavePrimariaEntidad = oRegistroEN.idProveedor;

                            EvaluarErrorParaMensajeAPantalla(oRegistroLN.Error, "Eliminar");

                            oRegistroEN = null;
                            oRegistroLN = null;

                            this.Cursor = Cursors.Default;

                            if (CerrarVentana == true)
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eliminar la información del registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void tsbRecarRegistro_Click(object sender, EventArgs e)
        {
            LlenarCamposDesdeBaseDatosSegunID();
        }

        private void dtpkFecha_ValueChanged(object sender, EventArgs e)
        {
            txtFechaDeCumpleanos.Text = dtpkFecha.Value.ToString("dd-MM-yyyy");
            txtFechaDeCumpleanos.Visible = true;
            dtpkFecha.Visible = false;
            this.Focus();
        }

        private void txtFechaDeCumpleanos_Enter(object sender, EventArgs e)
        {
            txtFechaDeCumpleanos.Visible = false;
            dtpkFecha.Visible = true;
            dtpkFecha.Location = txtFechaDeCumpleanos.Location;
            dtpkFecha.Focus();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            txtFechaDeCumpleanos.Clear();
        }

        private void tsbBuscarLogotipo1_Click(object sender, EventArgs e)
        {
            try
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
                            long Tamano = ObtenerTamanoImagen(ImagenLogo, 'k');

                            if (Tamano > 250)
                            {
                                throw new ArgumentException("El tamaño de la imagen no puede exceder los 250 kilobytes");
                            }

                            this.pbxImagen.Image = (Image)ImagenLogo;

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ha ocurrido un error al intentar abrir el archivo: " + ex.Message, "cmdBuscar_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Buscar foto del Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public long ObtenerTamanoImagen(System.Drawing.Image vImage, char vMedida)
        {
            long vTamano = 0;
            long vTamanoBytes;
            if (vImage != null)
            {
                using (System.IO.MemoryStream vMemoryStream = new System.IO.MemoryStream())
                {
                    vImage.Save(vMemoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    vTamanoBytes = vMemoryStream.Length;
                    switch (vMedida)
                    {
                        case 'b':
                            vTamano = vTamanoBytes;
                            break;
                        case 'k':
                            vTamano = vTamanoBytes / 1204;
                            break;
                        case 'm':
                            vTamano = vTamanoBytes / 1024 / 1024;
                            break;
                    }
                }
            }
            return vTamano;
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }
                
        private void tsbAgregarContacto_Click(object sender, EventArgs e)
        {
            try
            {

                frmContactoOperacion ofrmRegistro = new frmContactoOperacion();
                ofrmRegistro.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                ofrmRegistro.OperacionARealizar = "Local";
                ofrmRegistro.NombreEntidad = "Agregar información del contacto";
                ofrmRegistro.ShowDialog();

                if(ofrmRegistro.VariableLocal == true)
                {
                    ContactoEN oRegistroEN = ofrmRegistro.oContacto;

                    dgvListarContacto.Rows.Add(false,0, 0, oRegistroEN.Codigo, oRegistroEN.Nombre, oRegistroEN.Cedula, oRegistroEN.Direccion,
                        oRegistroEN.Telefono, oRegistroEN.Movil, oRegistroEN.Sexo, oRegistroEN.Observaciones,
                        oRegistroEN.Correo, oRegistroEN.FechaDeCumpleanos, oRegistroEN.Messenger, oRegistroEN.Skype,
                        oRegistroEN.Twitter, oRegistroEN.Facebook, oRegistroEN.Estado, true);
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "agregar registro del contacto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tsbSeleccionarcontacto_Click(object sender, EventArgs e)
        {
            try
            {

                frmContacto oFrmRegistro = new frmContacto();
                oFrmRegistro.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                oFrmRegistro.VariosRegistros = true;
                oFrmRegistro.ActivarFiltros = true;
                oFrmRegistro.TituloVentana = "Seleccionar Contacto";
                
                oFrmRegistro.AplicarFiltroDeWhereExterno = true;
                oFrmRegistro.WhereExterno = WhereContacto();
                oFrmRegistro.ShowDialog();

                ContactoEN[] oRegistroEN = new ContactoEN[0];
                oRegistroEN = oFrmRegistro.oContacto;
                
                if(oRegistroEN.Length > 0)
                {
                    foreach(ContactoEN oRegistro in oRegistroEN)
                    {
                        dgvListarContacto.Rows.Add(false, 0, oRegistro.idContacto, oRegistro.Codigo, oRegistro.Nombre, oRegistro.Cedula, oRegistro.Direccion,
                        oRegistro.Telefono, oRegistro.Movil, oRegistro.Sexo, oRegistro.Observaciones,
                        oRegistro.Correo, oRegistro.FechaDeCumpleanos, oRegistro.Messenger, oRegistro.Skype,
                        oRegistro.Twitter, oRegistro.Facebook, oRegistro.Estado, true);
                    }
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Seleccionar contacto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string WhereContacto()
        {
            string Where = "";
            try
            {
                if(dgvListarContacto.Rows.Count > 0)
                {
                    String IdContacto = "";
                    foreach(DataGridViewRow Fila in dgvListarContacto.Rows)
                    {
                        int idContacto;
                        int.TryParse(Fila.Cells["idContacto"].Value.ToString(), out idContacto);

                        if (IdContacto.Trim().Length == 0)
                        {
                            if (idContacto > 0)
                            {
                                IdContacto = Fila.Cells["idContacto"].Value.ToString();
                            }
                        }else
                        {
                            if (idContacto > 0)
                            {
                                IdContacto = string.Format("{0}, {1}", IdContacto, Fila.Cells["idContacto"].Value.ToString());
                            }
                        }
                    }

                    Where = string.Format(" and idContacto Not in ({0}) ", IdContacto);
                }

                return Where;

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Where dinamico para contacto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return Where;
                
            }
        }

        private string WhereLaboratorio()
        {
            string Where = "";
            try
            {
                if (dgvListarLaboratorios.Rows.Count > 0)
                {
                    String IdLaboratorio = "";
                    foreach (DataGridViewRow Fila in dgvListarLaboratorios.Rows)
                    {
                        int idLaboratorio;
                        int.TryParse(Fila.Cells["idLaboratorio"].Value.ToString(), out idLaboratorio);

                        if (IdLaboratorio.Trim().Length == 0)
                        {
                            if (idLaboratorio > 0)
                            {
                                IdLaboratorio = Fila.Cells["idLaboratorio"].Value.ToString();
                            }
                        }
                        else
                        {
                            if (idLaboratorio > 0)
                            {
                                IdLaboratorio = string.Format("{0}, {1}", IdLaboratorio, Fila.Cells["idLaboratorio"].Value.ToString());
                            }
                        }
                    }

                    Where = string.Format(" and idLaboratorio Not in ({0}) ", IdLaboratorio);
                }

                return Where;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Where dinamico para el laboratorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return Where;

            }
        }

        #region "Trabajando con DataGridContacto"

        private bool EvaluarDataGridView(DataGridView dgv)
        {

            if (OperacionARealizar.Trim().ToUpper() == "MODIFICAR".ToUpper())
            {
                if (dgv.Rows.Count > 0)
                {

                    List<DataGridViewRow> rows = (from item in dgv.Rows.Cast<DataGridViewRow>()
                                                  let Actualizar = Convert.ToBoolean(item.Cells["Actualizar"].Value ?? false)
                                                  let Eliminar = Convert.ToBoolean(item.Cells["Eliminar"].Value ?? false)
                                                  where Actualizar.Equals(true) || Eliminar.Equals(true)
                                                  select item).ToList<DataGridViewRow>();

                    if (rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
                else
                {

                    return false;
                }

            }
            else
            {
                return true;
            }
        }

        private string DescripcionDetallada(DataGridView dgv)
        {
            string Mensaje = "";

            if (dgv.Rows.Count > 0)
            {
                
                List<DataGridViewRow> rows = (from item in dgv.Rows.Cast<DataGridViewRow>()
                                              let Actualizar = Convert.ToBoolean(item.Cells["Actualizar"].Value ?? false)
                                              let idProveedorContacto = Convert.ToInt32(item.Cells["idProveedorContacto"].Value)
                                              where Actualizar.Equals(true) && idProveedorContacto == 0
                                              select item).ToList<DataGridViewRow>();
                if (rows.Count > 0)
                {
                    Mensaje += string.Format(" Se va a agregar: {1} Registros {0}", Environment.NewLine, rows.Count);
                }

                List<DataGridViewRow> rows1 = (from item in dgv.Rows.Cast<DataGridViewRow>()
                                               let Actualizar = Convert.ToBoolean(item.Cells["Actualizar"].Value ?? false)
                                               let idProveedorContacto = Convert.ToInt32(item.Cells["idProveedorContacto"].Value)
                                               where Actualizar.Equals(true) && idProveedorContacto > 0
                                               select item).ToList<DataGridViewRow>();
                if (rows1.Count > 0)
                {
                    Mensaje += string.Format(" Se va a actualizar: {1} Registros {0}", Environment.NewLine, rows1.Count);
                }

                List<DataGridViewRow> rows2 = (from item in dgv.Rows.Cast<DataGridViewRow>()
                                               let Eliminar = Convert.ToBoolean(item.Cells["Eliminar"].Value ?? false)
                                               where Eliminar.Equals(true)
                                               select item).ToList<DataGridViewRow>();

                if (rows2.Count > 0)
                {
                    Mensaje += string.Format(" Se va a Eliminar: {1} Registros {0}", Environment.NewLine, rows2.Count);
                }

            }
            else
            {
                Mensaje = "";
            }

            if(string.IsNullOrEmpty(Mensaje) == false || Mensaje.Trim().Length > 0)
            {
                Mensaje = string.Format("Información del Contacto: {0}", Mensaje);
            }

            return Mensaje;

        }

        private bool InsertarActyalizarYEliminarContacto()
        {
            try               
            {
                this.Cursor = Cursors.WaitCursor;

                if (EvaluarDataGridView(dgvListarContacto) == false)
                {
                    MessageBox.Show("No se encontró registros a procesar", "Evaluación de registros en la lista", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return true;
                }
                else { MessageBox.Show(DescripcionDetallada(dgvListarContacto), "Registros a procesar", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                int RowsContacto = dgvListarContacto.Rows.Count;

                if(RowsContacto > 0)
                {

                    MostrarBarraDeProgreso();
                    InicializarBarraDeProgreso(RowsContacto, 0);
                    int indice = 0;
                    int IndiceProgreso = 0;
                    int TotalDeFilasMarcadasParaEliminar = TotalDeFilasMarcadas(dgvListarContacto, "Eliminar");
                    //Aqui Volvemos dinamica El codigo poniendo el valor de la llave primaria 
                    string NombreLavePrimariaDetalle = "idProveedorContacto";

                    ProveedorEN oProveedorEN = InformacionDelRegistro();

                    while (indice <= dgvListarContacto.Rows.Count - 1)
                    {
                        this.Cursor = Cursors.WaitCursor;

                        
                        IncrementarBarraDeProgreso(IndiceProgreso + 1);
                        DataGridViewRow Fila = dgvListarContacto.Rows[indice];

                        int ValorDelaLLavePrimaria;

                        int.TryParse(Fila.Cells[NombreLavePrimariaDetalle].Value.ToString(), out ValorDelaLLavePrimaria);
                        Boolean Actualizar = Convert.ToBoolean(Fila.Cells["Actualizar"].Value);
                        Boolean Eliminar = Convert.ToBoolean(Fila.Cells["Eliminar"].Value);

                        if (ValorDelaLLavePrimaria == 0 && Actualizar == false)
                        {
                            if(Eliminar == true)
                            {
                                Fila.DefaultCellStyle.Font = new Font(Font.Name, Font.Size, FontStyle.Regular);
                                Fila.DefaultCellStyle.ForeColor = Color.Black;
                                Fila.DefaultCellStyle.SelectionForeColor = Color.Black;
                                Fila.Cells["Eliminar"].Value = false;
                            }

                            indice++;
                            IndiceProgreso++;
                            continue;

                        }


                        if (LosDatosIngresadosEnGrillaSonCorrectos(Fila) == false)
                        {
                            if (indice < dgvListarContacto.Rows.Count - 1)
                            {
                                if (MessageBox.Show("¿Desea continuar con los restantes registros a procesar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                                {
                                    OcultarBarraDeProgreso();
                                    return false;
                                }
                                else
                                {
                                    indice++;
                                    IndiceProgreso++;
                                    continue;
                                }
                            }
                            else
                            {
                                OcultarBarraDeProgreso();
                                return false;
                            }
                        }

                        ProveedorContactoEN oRegistroEN = InformacionDelProveedorContacto(Fila);
                        ProveedorContactoLN oRegistroLN = new ProveedorContactoLN();

                        oRegistroEN.oProveedorEN = oProveedorEN;

                        //DETERMINAMOS LA OPERACION A REALIZAR
                        string Operacion = "";
                        int idContato;
                        int.TryParse(Fila.Cells["idContacto"].Value.ToString(), out idContato);

                        //El orden es importante porque si un usuario agrego una nueva persona pero lo marco para eliminar, no hacemos nada, solo lo quitamos de la lista.
                        if (ValorDelaLLavePrimaria == 0 && Eliminar == true) { Operacion = "ELIMINAR FILA EN GRILLA"; }
                        //VALIDAREMOS QUE LA LLAVE PRIMARIA Y EL CONTACTO SEAN CEROS PARA UN NUEVO CONTACTO
                        else if (ValorDelaLLavePrimaria == 0 && idContato == 0) { Operacion = "AGREGAR CONTACTO"; }
                        //VALIDAREMOS QUE LA LLAVE PRIMARIA SEA CERO Y EL CONTARO SEA MAYOR A CERO PARA UN NUEVO VINCULO ENTRE PROVEEDOR Y CONTACTO
                        else if (ValorDelaLLavePrimaria == 0 && idContato > 0) { Operacion = "AGREGAR"; }
                        //VALIDAREMOS PARA PODER ELIMINAR EL REGISTRO....
                        else if (ValorDelaLLavePrimaria > 0 && Eliminar == true) { Operacion = "ELIMINAR"; }
                        //VALIDAREMOS PARA PODER ACTUALIZAR EL REGISTRO
                        else if (ValorDelaLLavePrimaria > 0 && Actualizar == true) { Operacion = "ACTUALIZAR"; }
                        //NO EXISTE NINGUNA OPERACION
                        else if (ValorDelaLLavePrimaria >= 0 && Actualizar == false && Eliminar == false) { Operacion = "NINGUNA"; }
                        
                        //Validaciones 
                        if (Operacion == "ELIMINAR FILA EN GRILLA")
                        {
                            dgvListarContacto.Rows.Remove(Fila);
                            if (dgvListarContacto.RowCount <= 0) { indice++; IndiceProgreso++; }
                            continue;
                        }

                        if (Operacion == "NINGUNA")
                        {
                            indice++;
                            IndiceProgreso++;
                            continue;
                        }

                        if (Operacion == "AGREGAR")
                        {
                            if (oRegistroLN.ValidarRegistroDuplicado(oRegistroEN, Program.oDatosDeConexion, Operacion))
                            {
                                OcultarBarraDeProgreso();
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return false;
                            }
                        }

                        if (Operacion == "ACTUALIZAR")
                        {
                            if (oRegistroLN.ValidarRegistroDuplicado(oRegistroEN, Program.oDatosDeConexion, Operacion))
                            {
                                OcultarBarraDeProgreso();
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return false;
                            }
                        }

                        if (Operacion == "ELIMINAR")
                        {
                            //if (oRegistrosLN.ExisteEmpleadoVinculadoAAuxiliaresDePlanillaGeneralDetalle(oRegistrosEN, Program.oDatosConexionesEN, "ELIMINAR"))
                            //{
                            //    this.Cursor = Cursors.Default;
                            //    DialogResult Respuesta = MessageBox.Show(oRegistrosLN.Error + "\n\n¿Desea continuar con el proceso de eliminación para los empleados restantes?", this.OperacionARealizar, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            //    if (Respuesta == DialogResult.No)
                            //    {
                            //        OcultarBarraDeProgreso();
                            //        return false;
                            //    }
                            //    else
                            //    {
                            //        indice++;
                            //        indice_progreso++;
                            //        continue;
                            //    }
                            //}
                        }

                        if (Operacion == "AGREGAR CONTACTO")
                        {
                            ContactoLN oContactoLN = new ContactoLN();
                            if (oContactoLN.ValidarRegistroDuplicado(oRegistroEN.oContactoEN, Program.oDatosDeConexion, "AGREGAR"))
                            {
                                OcultarBarraDeProgreso();
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return false;
                            }

                            string Cedula = Fila.Cells["Cedula"].Value.ToString();
                            if(string.IsNullOrEmpty(Cedula) == false || Cedula.Trim().Length > 0)
                            {
                                if (oContactoLN.ValidarRegistroDuplicadoParaCedula(oRegistroEN.oContactoEN, Program.oDatosDeConexion, "AGREGAR"))
                                {
                                    OcultarBarraDeProgreso();
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return false;
                                }
                            }

                            oContactoLN = null;
                        }


                        //OPERACIONES
                        if (Operacion == "AGREGAR")
                        {
                            if (oRegistroLN.Agregar(oRegistroEN, Program.oDatosDeConexion))
                            {
                                Fila.Cells[NombreLavePrimariaDetalle].Value = oRegistroEN.idProveedorContacto;                                
                                Fila.Cells["Actualizar"].Value = false;
                                oRegistroEN = null;
                                oRegistroLN = null;
                                indice++;
                                IndiceProgreso++;
                                continue;
                            }
                            else
                            {
                                OcultarBarraDeProgreso();
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(oRegistroLN.Error, OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                oRegistroEN = null;
                                oRegistroLN = null;
                                return false;
                            }
                        }

                        if (Operacion == "ACTUALIZAR")
                        {
                            
                            if (oRegistroLN.Actualizar(oRegistroEN, Program.oDatosDeConexion))
                            {
                                
                                dgvListarContacto.Rows[Fila.Index].Cells["Actualizar"].Value = false;
                                oRegistroEN = null;
                                oRegistroLN = null;
                                indice++;
                                IndiceProgreso++;
                                continue;

                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(oRegistroLN.Error, OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                OcultarBarraDeProgreso();
                                oRegistroEN = null;
                                oRegistroLN = null;
                                return false;
                            }
                        }

                        if (Operacion == "ELIMINAR")
                        {
                            
                            if (oRegistroLN.Eliminar(oRegistroEN, Program.oDatosDeConexion))
                            {
                                dgvListarContacto.Rows.Remove(Fila);
                                oRegistroEN = null;
                                oRegistroLN = null;                                
                                if (dgvListarContacto.RowCount <= 0) { indice++; }
                                IndiceProgreso++;
                                continue;
                                
                            }
                            else
                            {
                                OcultarBarraDeProgreso();
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(oRegistroLN.Error, OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                oRegistroEN = null;
                                oRegistroLN = null;
                                return false;
                            }
                        }

                        //AGREGAR UN NUEVO CONTACTO... ANTE DE SER VINCULADO
                        if (Operacion == "AGREGAR CONTACTO")
                        {
                            /*Primero debemos agregar la entidad superior del registro*/

                            EntidadEN oEntidadEN = informacionDeLaEntidadSuperiorDelContacto();
                            EntidadLN oEntidadLN = new EntidadLN();

                            if(oEntidadLN.Agregar(oEntidadEN , Program.oDatosDeConexion))
                            {

                                ContactoLN oContactoLN = new ContactoLN();
                                oRegistroEN.oContactoEN.idContacto = oEntidadEN.idEntidad;
                                if (oContactoLN.Agregar(oRegistroEN.oContactoEN, Program.oDatosDeConexion))
                                {

                                    if(oRegistroLN.Agregar(oRegistroEN, Program.oDatosDeConexion))
                                    {

                                        Fila.Cells[NombreLavePrimariaDetalle].Value = oRegistroEN.idProveedorContacto;
                                        Fila.Cells["idContacto"].Value = oRegistroEN.oContactoEN.idContacto.ToString();
                                        Fila.Cells["Codigo"].Value = oRegistroEN.oContactoEN.Codigo;
                                        Fila.Cells["Actualizar"].Value = false;
                                        oContactoLN = null;
                                        oEntidadEN = null;
                                        oEntidadLN = null;
                                        oRegistroEN = null;
                                        oRegistroLN = null;
                                        indice++;
                                        IndiceProgreso++;
                                        continue;

                                    }
                                    else
                                    {
                                        OcultarBarraDeProgreso();
                                        this.Cursor = Cursors.Default;

                                        oContactoLN.Eliminar(oRegistroEN.oContactoEN, Program.oDatosDeConexion);
                                        oEntidadLN.Eliminar(oEntidadEN, Program.oDatosDeConexion);
                                        MessageBox.Show(oContactoLN.Error, OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        oEntidadEN = null;
                                        oEntidadLN = null;
                                        oContactoLN = null;
                                        oRegistroEN = null;
                                        oRegistroLN = null;
                                        return false;
                                    }

                                }
                                else
                                {

                                    OcultarBarraDeProgreso();
                                    this.Cursor = Cursors.Default;

                                    oEntidadLN.Eliminar(oEntidadEN, Program.oDatosDeConexion);
                                    MessageBox.Show(oContactoLN.Error, OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    oEntidadEN = null;
                                    oEntidadLN = null;
                                    oContactoLN = null;
                                    oRegistroEN = null;
                                    oRegistroLN = null;
                                    return false;

                                }

                            }
                            else
                            {
                                OcultarBarraDeProgreso();
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(oEntidadLN.Error, OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                oEntidadLN = null;
                                oRegistroEN = null;
                                oRegistroLN = null;
                                return false;
                            }                            
                          
                        }

                        this.Cursor = Cursors.Default;
                    }

                    OcultarBarraDeProgreso();
                    return true;

                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Información del Contacto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private EntidadEN informacionDeLaEntidadSuperiorDelContacto()
        {
            EntidadEN oRegistroEN = new EntidadEN();

            try
            {
                oRegistroEN.oTipoDeEntidadEN.Nombre = "Contacto";
                oRegistroEN.oTipoDeEntidadEN.NombreInterno = "contacto";
                oRegistroEN.oLoginEN = Program.oLoginEN;
                oRegistroEN.IdUsuarioDeCreacion = Program.oLoginEN.idUsuario;
                oRegistroEN.IdUsuarioDeModificacion = Program.oLoginEN.idUsuario;
                oRegistroEN.FechaDeCreacion = System.DateTime.Now;
                oRegistroEN.FechaDeModificacion = System.DateTime.Now;

                return oRegistroEN;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Información de la entidad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return oRegistroEN;
            }
        }
        
        private ContactoEN InformacionDelContacto(DataGridViewRow Fila)
        {

            ContactoEN oRegistroEN = new ContactoEN();
            int idContacto;
            int.TryParse(Fila.Cells["idContacto"].Value.ToString(), out idContacto);
            oRegistroEN.idContacto = idContacto;
            oRegistroEN.Codigo = Fila.Cells["Codigo"].Value.ToString().Trim();            
            oRegistroEN.Nombre = Fila.Cells["Nombre"].Value.ToString().Trim();
            oRegistroEN.Direccion = Fila.Cells["Direccion"].Value.ToString().Trim();
            oRegistroEN.Telefono = Fila.Cells["Telefono"].Value.ToString().Trim();
            oRegistroEN.Movil = Fila.Cells["Movil"].Value.ToString().Trim();
            oRegistroEN.Observaciones = Fila.Cells["Observaciones"].Value.ToString().Trim();
            oRegistroEN.Correo = Fila.Cells["Correo"].Value.ToString().Trim();
            oRegistroEN.Estado = Fila.Cells["Estado"].Value.ToString().Trim(); ;
            oRegistroEN.FechaDeCumpleanos = Fila.Cells["FechaDeCumpleanos"].Value.ToString().Trim();
            oRegistroEN.Messenger = Fila.Cells["Messenger"].Value.ToString().Trim();
            oRegistroEN.Twitter = Fila.Cells["Twitter"].Value.ToString();
            oRegistroEN.Facebook = Fila.Cells["Facebook"].Value.ToString();
            oRegistroEN.Skype = Fila.Cells["Skype"].Value.ToString() ;
            oRegistroEN.Sexo = Fila.Cells["Sexo"].Value.ToString();
            oRegistroEN.Cedula = Fila.Cells["Cedula"].Value.ToString();
            oRegistroEN.Foto = null;
            oRegistroEN.oLoginEN = Program.oLoginEN;           
            oRegistroEN.IdUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.IdUsuarioDeModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;

        }

        private ProveedorContactoEN InformacionDelProveedorContacto(DataGridViewRow Fila)
        {
            ProveedorContactoEN oRegistroEN = new ProveedorContactoEN();
            int idProveedorContacto;
            int.TryParse(Fila.Cells["idProveedorContacto"].Value.ToString(), out idProveedorContacto);

            oRegistroEN.idProveedorContacto = idProveedorContacto;
            oRegistroEN.oContactoEN = InformacionDelContacto(Fila);

            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.IdUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.IdUsuarioDeModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;
        }

        private bool LosDatosIngresadosEnGrillaSonCorrectos(DataGridViewRow Fila)
        {
            try
            {
                if (Convert.ToBoolean(Fila.Cells["Eliminar"].Value) == false)
                {
                    object ValorAEvaluar = null;
                    DataGridViewCell CeldaAEvaluar = null;
                    string NombreCampoId, NombreCampoNombre;

                    //evaluar si agrego una marca                    
                    NombreCampoNombre = "Nombre";                    
                    
                    ValorAEvaluar = Fila.Cells[NombreCampoNombre].Value;
                    if (ValorAEvaluar == null || string.IsNullOrEmpty(ValorAEvaluar.ToString()))
                    {
                        Fila.Selected = true;
                        dgvListarContacto.CurrentCell = CeldaAEvaluar;
                        dgvListarContacto.CurrentCell.ErrorText = "Es Necesario agregar un Nombre";
                        MessageBox.Show("Informacion del contacto \n\n Es necesario ingresar un Nombre", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    
                    
                }
                return true;
            }
            catch (Exception ex)
            {
                Fila.Selected = true;
                dgvListarContacto.CurrentCell = Fila.Cells["idProveedorContacto"];
                MessageBox.Show("Error al validar datos del Contacto: " + Fila.Cells["Nombre"].Value.ToString() + "\n" + ex.Message, "Buscar Contacto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        private int TotalDeFilasMarcadas(DataGridView odgvGrid,String Columna)
        {
            try
            {
                int numero = 0;
                foreach (DataGridViewRow Fila in odgvGrid.Rows)
                {
                    if (Convert.ToBoolean(Fila.Cells[Columna].Value) == true)
                    {
                        numero++;
                    }
                }
                return (numero);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al contabilizar las columnas (" + Columna + ") marcadas. \n" + ex.Message, "TotalDeFilasMarcadas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (0);
            }
        }

        private void InicializarBarraDeProgreso(int Maximo, int Minimo)
        {
            Barradeprogreso.Minimum = Minimo;
            Barradeprogreso.Maximum = Maximo;
        }

        private void MostrarBarraDeProgreso()
        {
            statusStrip1.Visible = true;
            Barradeprogreso.Visible = true;
            lbaBarradeprogreso.Visible = true;
        }

        private void OcultarBarraDeProgreso()
        {
            statusStrip1.Visible = false;
            Barradeprogreso.Visible = false;
            lbaBarradeprogreso.Visible = false;
        }

        private void IncrementarBarraDeProgreso(int incremento)
        {
            Barradeprogreso.Value = incremento;
            lbaBarradeprogreso.Text = Barradeprogreso.Value.ToString() + " de " + Barradeprogreso.Maximum;
            Application.DoEvents();
        }

        private void dgvListarContacto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvListarContacto.CurrentCell != null && dgvListarContacto.Columns[dgvListarContacto.CurrentCell.ColumnIndex].Name == "Eliminar")
            {
                if (Convert.ToBoolean(dgvListarContacto.Rows[dgvListarContacto.CurrentCell.RowIndex].Cells["Eliminar"].Value) == true)
                {
                    dgvListarContacto.Rows[dgvListarContacto.CurrentCell.RowIndex].DefaultCellStyle.Font = new Font(Font.Name, Font.Size, FontStyle.Strikeout);
                    dgvListarContacto.Rows[dgvListarContacto.CurrentCell.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                    dgvListarContacto.Rows[dgvListarContacto.CurrentCell.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Red;
                }
                else
                {
                    dgvListarContacto.Rows[dgvListarContacto.CurrentCell.RowIndex].DefaultCellStyle.Font = new Font(Font.Name, Font.Size, FontStyle.Regular);
                    dgvListarContacto.Rows[dgvListarContacto.CurrentCell.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    dgvListarContacto.Rows[dgvListarContacto.CurrentCell.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
                }

            }
        }

        private void dgvListarContacto_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int idProveedorContacto;
                int.TryParse(dgvListarContacto.Rows[e.RowIndex].Cells["idProveedorContacto"].Value.ToString(), out idProveedorContacto);

                if (dgvListarContacto.Rows[e.RowIndex].Cells["idProveedorContacto"].Value == null)
                    return;



                if (idProveedorContacto > 0 && dgvListarContacto.Columns[e.ColumnIndex].Name != "Eliminar")
                {
                    dgvListarContacto.Rows[e.RowIndex].Cells["Actualizar"].Value = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ingresar dato en la celda 'dgvLista_CellEndEdit'. \n" + ex.Message, "Listas de contactos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgvListarContacto_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvListarContacto.IsCurrentCellDirty)
            {
                dgvListarContacto.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvListarContacto_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right && dgvListarContacto.CurrentCell != null)
                {
                    //Este código permite seleccionar una fila del DatagridView al presionar click derecho
                    DataGridView.HitTestInfo Hitest = dgvListarContacto.HitTest(e.X, e.Y);

                    if (Hitest.Type == DataGridViewHitTestType.Cell)
                    {
                        dgvListarContacto.CurrentCell = dgvListarContacto.Rows[Hitest.RowIndex].Cells[Hitest.ColumnIndex];
                        dgvListarContacto.Rows[dgvListarContacto.CurrentCell.RowIndex].Selected = true;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mouse down del Listado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvListarContacto_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvListarContacto.RowCount > 0 && dgvListarContacto.SelectedRows.Count > 0)
                {
                    if (Convert.ToBoolean(dgvListarContacto.Rows[dgvListarContacto.SelectedRows[0].Index].Cells["Eliminar"].Value) == true)
                    {
                        dgvListarContacto.Rows[dgvListarContacto.SelectedRows[0].Index].DefaultCellStyle.SelectionForeColor = Color.Red;
                    }
                    else
                    {
                        dgvListarContacto.Rows[dgvListarContacto.SelectedRows[0].Index].DefaultCellStyle.SelectionForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Formato de fila", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {

                frmLaboratorioOperacion ofrmRegistro = new frmLaboratorioOperacion();
                ofrmRegistro.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                ofrmRegistro.OperacionARealizar = "Local";
                ofrmRegistro.NombreEntidad = "Agregar información del Laboratorio";
                ofrmRegistro.ShowDialog();

                if (ofrmRegistro.VariableLocal == true)
                {
                    LaboratorioEN oRegistroEN = ofrmRegistro.oLaboratorio;
                
                    dgvListarLaboratorios.Rows.Add(false, 0, 0, oRegistroEN.Codigo, oRegistroEN.Nombre, 
                        oRegistroEN.NoRUC, oRegistroEN.Direccion,
                        oRegistroEN.Telefono, oRegistroEN.Movil, oRegistroEN.Observaciones,
                        oRegistroEN.Correo,oRegistroEN.SitioWeb, oRegistroEN.FechaDeCumpleanos, oRegistroEN.Messenger, oRegistroEN.Skype,
                        oRegistroEN.Twitter, oRegistroEN.Facebook, oRegistroEN.Estado, true);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "agregar registro del laboratorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region "Trabajando con laboratorios"
        
        private void CrearColumnasDGVLaboratorios()
        {
            try
            {
                
                string columnas = @"idProveedorLaboratorio, idLaboratorio, Codigo, Nombre, 
                NoRUC,Direccion, Telefono, Movil, Observaciones, Correo, SitioWeb,
                FechaDeCumpleanos, Messenger, Skype, Twitter, Facebook, Estado";

                string[] arrayColumnas = columnas.Split(',');

                dgvListarLaboratorios.Columns.Clear();
                dgvListarLaboratorios.ColumnCount = arrayColumnas.Length;

                DataGridViewCheckBoxColumn col1 = new DataGridViewCheckBoxColumn();
                dgvListarLaboratorios.Columns.Insert(0, col1);
                dgvListarLaboratorios.Columns[0].Name = "Eliminar";

                int j = 1;
                foreach (string item in arrayColumnas)
                {
                    dgvListarLaboratorios.Columns[j].Name = item.Trim();
                    j++;
                }

                DataGridViewCheckBoxColumn col2 = new DataGridViewCheckBoxColumn();
                dgvListarLaboratorios.Columns.Insert(j, col2);
                dgvListarLaboratorios.Columns[j].Name = "Actualizar";

                FormatearDGVParaElLaboratorio();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar la lista. \n" + ex.Message, "PoblarColumnasdgvListarLaboratorio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private DataTable InformacionDeLosContactosAsociadosAlLaboratorio()
        {
            DataTable ODatos = null;
            try
            {

                ProveedorLaboratorioEN oRegistroEN = new ProveedorLaboratorioEN();
                ProveedorLaboratorioLN oRegistroLN = new ProveedorLaboratorioLN();

                oRegistroEN.oProveedorEN.idProveedor = ValorLlavePrimariaEntidad;

                if (oRegistroLN.ListadoPorIdentificadorDelProveedor(oRegistroEN, Program.oDatosDeConexion))
                {

                    ODatos = oRegistroLN.TraerDatos();
                    return ODatos;

                }
                else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Información de los contactos asociados al proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return ODatos;
            }

        }

        private void CrearyYPoblarColumnasDGVLaboratorio()
        {
            try
            {

                CrearColumnasDGVLaboratorios();

                DataTable DTOrden = InformacionDeLosContactosAsociadosAlLaboratorio();

                if (DTOrden != null)
                {

                    if (DTOrden.Rows.Count > 0)
                    {
                        int i = 1;
                        Boolean valor = false;
                        if (OperacionARealizar == "Eliminar") { valor = true; } else { valor = false; }

                        int idProveedorLaboratorio = 0;
                        int idLaboratorio = 0;

                        foreach (DataRow row in DTOrden.Rows)
                        {

                            if (OperacionARealizar.ToUpper() == "NUEVO A PARTIR DE REGISTRO SELECCIONADO".ToUpper())
                            {
                                idProveedorLaboratorio = 0;
                                idLaboratorio = Convert.ToInt32(row["idLaboratorio"]);
                            }
                            else
                            {
                                idLaboratorio = Convert.ToInt32(row["idLaboratorio"]);
                                idProveedorLaboratorio = Convert.ToInt32(row["idProveedorLaboratorio"]);
                            }
                            
                            dgvListarLaboratorios.Rows.Add(
                                valor,
                                idProveedorLaboratorio,
                                idLaboratorio,
                                row["Codigo"],
                                row["Nombre"],
                                row["NoRUC"],
                                row["Direccion"],
                                row["Telefono"],
                                row["Movil"],
                                row["Observaciones"],
                                row["Correo"],
                                row["SitioWeb"],
                                row["FechaDeCumpleanos"],
                                row["Messenger"],
                                row["Skype"],
                                row["Twitter"],
                                row["Facebook"],
                                row["Estado"],
                                valor
                                );

                            i++;

                        }

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crear y Poblar Columnas, para los laboratorios", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OcultarColumnasEnElDGVParaLaboratorio(String ColumnasDelDGV)
        {
            if (dgvListarLaboratorios.Columns.Count > 0)
            {
                String[] ArrayColumnasDGV = ColumnasDelDGV.Split(',');
                foreach (String c in ArrayColumnasDGV)
                {

                    foreach (DataGridViewColumn c1 in dgvListarLaboratorios.Columns)
                    {
                        if (c1.Name.Trim().ToUpper() == c.Trim().ToUpper())
                        {
                            c1.Visible = false;
                        }
                    }

                }
            }
        }

        private void FormatearDGVParaElLaboratorio()
        {
            try
            {
                this.dgvListarLaboratorios.AllowUserToResizeRows = false;
                this.dgvListarLaboratorios.AllowUserToAddRows = false;
                this.dgvListarLaboratorios.AllowUserToDeleteRows = false;
                this.dgvListarLaboratorios.DefaultCellStyle.BackColor = Color.White;

                this.dgvListarLaboratorios.MultiSelect = false;
                this.dgvListarLaboratorios.RowHeadersVisible = true;

                this.dgvListarLaboratorios.DefaultCellStyle.Font = new Font("Segoe UI", 8);
                this.dgvListarLaboratorios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8);
                this.dgvListarLaboratorios.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
                this.dgvListarLaboratorios.BackgroundColor = System.Drawing.SystemColors.Window;
                this.dgvListarLaboratorios.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

                string OcultarColumnas = "idProveedorLaboratorio,idLaboratorio,Observaciones,FechaDeCumpleanos,Estado,Actualizar";
                OcultarColumnasEnElDGVParaLaboratorio(OcultarColumnas);

                FormatearColumnasDelDGVParaLaboratorio();

                if (OperacionARealizar == "Eliminar")
                {
                    dgvListarLaboratorios.DefaultCellStyle.Font = new Font(Font.Name, Font.Size, FontStyle.Strikeout);
                    dgvListarLaboratorios.DefaultCellStyle.ForeColor = Color.Red;
                    dgvListarLaboratorios.DefaultCellStyle.SelectionForeColor = Color.Red;
                }
                else
                {
                    dgvListarLaboratorios.DefaultCellStyle.Font = new Font(Font.Name, Font.Size, FontStyle.Regular);
                    dgvListarLaboratorios.DefaultCellStyle.ForeColor = Color.Black;
                    dgvListarLaboratorios.DefaultCellStyle.SelectionForeColor = Color.Black;
                }

                if (OperacionARealizar.ToUpper() == "ELIMINAR".ToUpper() || OperacionARealizar.ToUpper() == "CONSULTAR".ToUpper())
                {
                    dgvListarLaboratorios.Columns["Eliminar"].ReadOnly = true;
                }

                this.dgvListarLaboratorios.RowHeadersWidth = 25;

                this.dgvListarLaboratorios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvListarLaboratorios.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                this.dgvListarLaboratorios.StandardTab = true;
                this.dgvListarLaboratorios.ReadOnly = false;
                this.dgvListarLaboratorios.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FormatoDGV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearColumnasDelDGVParaLaboratorio()
        {
            if (dgvListarLaboratorios.Columns.Count > 0)
            {

                foreach (DataGridViewColumn c1 in dgvListarLaboratorios.Columns)
                {
                    if (c1.Visible == true)
                    {
                        if (c1.Name.Trim().ToUpper() != "Seleccionar".ToUpper())
                        {
                            FormatoDGV oFormato = new FormatoDGV(c1.Name.Trim());
                            if (oFormato.ValorEncontrado == false)
                            {
                                oFormato = new FormatoDGV(c1.Name.Trim(), "Laboratorio");
                            }

                            if (oFormato != null)
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
        
        private string DescripcionDetalladaDelLaboratorio(DataGridView dgv)
        {
            string Mensaje = "";

            if (dgv.Rows.Count > 0)
            {

                List<DataGridViewRow> rows = (from item in dgv.Rows.Cast<DataGridViewRow>()
                                              let Actualizar = Convert.ToBoolean(item.Cells["Actualizar"].Value ?? false)
                                              let idProveedorLaboratorio = Convert.ToInt32(item.Cells["idProveedorLaboratorio"].Value)
                                              where Actualizar.Equals(true) && idProveedorLaboratorio == 0
                                              select item).ToList<DataGridViewRow>();
                if (rows.Count > 0)
                {
                    Mensaje += string.Format(" Se va a agregar: {1} Registros {0}", Environment.NewLine, rows.Count);
                }

                List<DataGridViewRow> rows1 = (from item in dgv.Rows.Cast<DataGridViewRow>()
                                               let Actualizar = Convert.ToBoolean(item.Cells["Actualizar"].Value ?? false)
                                               let idProveedorLaboratorio = Convert.ToInt32(item.Cells["idProveedorLaboratorio"].Value)
                                               where Actualizar.Equals(true) && idProveedorLaboratorio > 0
                                               select item).ToList<DataGridViewRow>();
                if (rows1.Count > 0)
                {
                    Mensaje += string.Format(" Se va a actualizar: {1} Registros {0}", Environment.NewLine, rows1.Count);
                }

                List<DataGridViewRow> rows2 = (from item in dgv.Rows.Cast<DataGridViewRow>()
                                               let Eliminar = Convert.ToBoolean(item.Cells["Eliminar"].Value ?? false)
                                               where Eliminar.Equals(true)
                                               select item).ToList<DataGridViewRow>();

                if (rows2.Count > 0)
                {
                    Mensaje += string.Format(" Se va a Eliminar: {1} Registros {0}", Environment.NewLine, rows2.Count);
                }

            }
            else
            {
                Mensaje = "";
            }

            if (string.IsNullOrEmpty(Mensaje) == false || Mensaje.Trim().Length > 0)
            {
                Mensaje = string.Format("Información del Laboratorio: {0}", Mensaje);
            }

            return Mensaje;

        }

        private bool InsertarActyalizarYEliminarLaboratorio()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (EvaluarDataGridView(dgvListarLaboratorios) == false)
                {
                    MessageBox.Show("No se encontró registros a procesar", "Evaluación de registros en la lista", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return true;
                }
                else { MessageBox.Show(DescripcionDetalladaDelLaboratorio(dgvListarLaboratorios), "Registros a procesar", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                int RowsContacto = dgvListarLaboratorios.Rows.Count;

                if (RowsContacto > 0)
                {

                    MostrarBarraDeProgreso();
                    InicializarBarraDeProgreso(RowsContacto, 0);
                    int indice = 0;
                    int IndiceProgreso = 0;
                    int TotalDeFilasMarcadasParaEliminar = TotalDeFilasMarcadas(dgvListarLaboratorios, "Eliminar");
                    //Aqui Volvemos dinamica El codigo poniendo el valor de la llave primaria 
                    string NombreLavePrimariaDetalle = "idProveedorLaboratorio";

                    ProveedorEN oProveedorEN = InformacionDelRegistro();

                    while (indice <= dgvListarLaboratorios.Rows.Count - 1)
                    {
                        this.Cursor = Cursors.WaitCursor;


                        IncrementarBarraDeProgreso(IndiceProgreso + 1);
                        DataGridViewRow Fila = dgvListarLaboratorios.Rows[indice];

                        int ValorDelaLLavePrimaria;

                        int.TryParse(Fila.Cells[NombreLavePrimariaDetalle].Value.ToString(), out ValorDelaLLavePrimaria);
                        Boolean Actualizar = Convert.ToBoolean(Fila.Cells["Actualizar"].Value);
                        Boolean Eliminar = Convert.ToBoolean(Fila.Cells["Eliminar"].Value);

                        if (ValorDelaLLavePrimaria == 0 && Actualizar == false)
                        {
                            if (Eliminar == true)
                            {
                                Fila.DefaultCellStyle.Font = new Font(Font.Name, Font.Size, FontStyle.Regular);
                                Fila.DefaultCellStyle.ForeColor = Color.Black;
                                Fila.DefaultCellStyle.SelectionForeColor = Color.Black;
                                Fila.Cells["Eliminar"].Value = false;
                            }

                            indice++;
                            IndiceProgreso++;
                            continue;

                        }


                        if (LosDatosIngresadosEnGrillaSonCorrectosDelLaboratorio(Fila) == false)
                        {
                            if (indice < dgvListarLaboratorios.Rows.Count - 1)
                            {
                                if (MessageBox.Show("¿Desea continuar con los restantes registros a procesar?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                                {
                                    OcultarBarraDeProgreso();
                                    return false;
                                }
                                else
                                {
                                    indice++;
                                    IndiceProgreso++;
                                    continue;
                                }
                            }
                            else
                            {
                                OcultarBarraDeProgreso();
                                return false;
                            }
                        }

                        ProveedorLaboratorioEN oRegistroEN = InformacionDelProveedorLaboratorio(Fila);
                        ProveedorLaboratorioLN oRegistroLN = new ProveedorLaboratorioLN();

                        oRegistroEN.oProveedorEN = oProveedorEN;

                        //DETERMINAMOS LA OPERACION A REALIZAR
                        string Operacion = "";
                        int idLaboratorio;
                        int.TryParse(Fila.Cells["idLaboratorio"].Value.ToString(), out idLaboratorio);

                        //El orden es importante porque si un usuario agrego una nueva persona pero lo marco para eliminar, no hacemos nada, solo lo quitamos de la lista.
                        if (ValorDelaLLavePrimaria == 0 && Eliminar == true) { Operacion = "ELIMINAR FILA EN GRILLA"; }
                        //VALIDAREMOS QUE LA LLAVE PRIMARIA Y EL CONTACTO SEAN CEROS PARA UN NUEVO CONTACTO
                        else if (ValorDelaLLavePrimaria == 0 && idLaboratorio == 0) { Operacion = "AGREGAR CONTACTO"; }
                        //VALIDAREMOS QUE LA LLAVE PRIMARIA SEA CERO Y EL CONTARO SEA MAYOR A CERO PARA UN NUEVO VINCULO ENTRE PROVEEDOR Y CONTACTO
                        else if (ValorDelaLLavePrimaria == 0 && idLaboratorio > 0) { Operacion = "AGREGAR"; }
                        //VALIDAREMOS PARA PODER ELIMINAR EL REGISTRO....
                        else if (ValorDelaLLavePrimaria > 0 && Eliminar == true) { Operacion = "ELIMINAR"; }
                        //VALIDAREMOS PARA PODER ACTUALIZAR EL REGISTRO
                        else if (ValorDelaLLavePrimaria > 0 && Actualizar == true) { Operacion = "ACTUALIZAR"; }
                        //NO EXISTE NINGUNA OPERACION
                        else if (ValorDelaLLavePrimaria >= 0 && Actualizar == false && Eliminar == false) { Operacion = "NINGUNA"; }

                        //Validaciones 
                        if (Operacion == "ELIMINAR FILA EN GRILLA")
                        {
                            dgvListarLaboratorios.Rows.Remove(Fila);
                            if (dgvListarLaboratorios.RowCount <= 0) { indice++; IndiceProgreso++; }
                            continue;
                        }

                        if (Operacion == "NINGUNA")
                        {
                            indice++;
                            IndiceProgreso++;
                            continue;
                        }

                        if (Operacion == "AGREGAR")
                        {
                            if (oRegistroLN.ValidarRegistroDuplicado(oRegistroEN, Program.oDatosDeConexion, Operacion))
                            {
                                OcultarBarraDeProgreso();
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return false;
                            }
                        }

                        if (Operacion == "ACTUALIZAR")
                        {
                            if (oRegistroLN.ValidarRegistroDuplicado(oRegistroEN, Program.oDatosDeConexion, Operacion))
                            {
                                OcultarBarraDeProgreso();
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return false;
                            }
                        }

                        if (Operacion == "ELIMINAR")
                        {
                            //if (oRegistrosLN.ExisteEmpleadoVinculadoAAuxiliaresDePlanillaGeneralDetalle(oRegistrosEN, Program.oDatosConexionesEN, "ELIMINAR"))
                            //{
                            //    this.Cursor = Cursors.Default;
                            //    DialogResult Respuesta = MessageBox.Show(oRegistrosLN.Error + "\n\n¿Desea continuar con el proceso de eliminación para los empleados restantes?", this.OperacionARealizar, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            //    if (Respuesta == DialogResult.No)
                            //    {
                            //        OcultarBarraDeProgreso();
                            //        return false;
                            //    }
                            //    else
                            //    {
                            //        indice++;
                            //        indice_progreso++;
                            //        continue;
                            //    }
                            //}
                        }

                        if (Operacion == "AGREGAR CONTACTO")
                        {
                            LaboratorioLN LaboratorioLN = new LaboratorioLN();
                            if (LaboratorioLN.ValidarRegistroDuplicado(oRegistroEN.oLaboratorioEN, Program.oDatosDeConexion, "AGREGAR"))
                            {
                                OcultarBarraDeProgreso();
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return false;
                            }

                            string Cedula = Fila.Cells["Cedula"].Value.ToString();
                            if (string.IsNullOrEmpty(Cedula) == false || Cedula.Trim().Length > 0)
                            {
                                if (LaboratorioLN.ValidarRegistroDuplicadoNoRUC(oRegistroEN.oLaboratorioEN, Program.oDatosDeConexion, "AGREGAR"))
                                {
                                    OcultarBarraDeProgreso();
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return false;
                                }
                            }

                            LaboratorioLN = null;
                        }
                        
                        //OPERACIONES
                        if (Operacion == "AGREGAR")
                        {
                            if (oRegistroLN.Agregar(oRegistroEN, Program.oDatosDeConexion))
                            {
                                Fila.Cells[NombreLavePrimariaDetalle].Value = oRegistroEN.idProveedorLaboratorio;
                                Fila.Cells["Actualizar"].Value = false;
                                oRegistroEN = null;
                                oRegistroLN = null;
                                indice++;
                                IndiceProgreso++;
                                continue;
                            }
                            else
                            {
                                OcultarBarraDeProgreso();
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(oRegistroLN.Error, OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                oRegistroEN = null;
                                oRegistroLN = null;
                                return false;
                            }
                        }

                        if (Operacion == "ACTUALIZAR")
                        {

                            if (oRegistroLN.Actualizar(oRegistroEN, Program.oDatosDeConexion))
                            {

                                dgvListarLaboratorios.Rows[Fila.Index].Cells["Actualizar"].Value = false;
                                oRegistroEN = null;
                                oRegistroLN = null;
                                indice++;
                                IndiceProgreso++;
                                continue;

                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(oRegistroLN.Error, OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                OcultarBarraDeProgreso();
                                oRegistroEN = null;
                                oRegistroLN = null;
                                return false;
                            }
                        }

                        if (Operacion == "ELIMINAR")
                        {

                            if (oRegistroLN.Eliminar(oRegistroEN, Program.oDatosDeConexion))
                            {
                                dgvListarLaboratorios.Rows.Remove(Fila);
                                oRegistroEN = null;
                                oRegistroLN = null;
                                if (dgvListarLaboratorios.RowCount <= 0) { indice++; }
                                IndiceProgreso++;
                                continue;

                            }
                            else
                            {
                                OcultarBarraDeProgreso();
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(oRegistroLN.Error, OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                oRegistroEN = null;
                                oRegistroLN = null;
                                return false;
                            }
                        }

                        //AGREGAR UN NUEVO CONTACTO... ANTE DE SER VINCULADO
                        if (Operacion == "AGREGAR CONTACTO")
                        {
                            /*Primero debemos agregar la entidad superior del registro*/

                            EntidadEN oEntidadEN = informacionDeLaEntidadSuperiorDelLaboratorio();
                            EntidadLN oEntidadLN = new EntidadLN();

                            if (oEntidadLN.Agregar(oEntidadEN, Program.oDatosDeConexion))
                            {

                                LaboratorioLN oLaboratorioLN = new LaboratorioLN();
                                oRegistroEN.oLaboratorioEN.idLaboratorio = oEntidadEN.idEntidad;
                                if (oLaboratorioLN.Agregar(oRegistroEN.oLaboratorioEN, Program.oDatosDeConexion))
                                {

                                    if (oRegistroLN.Agregar(oRegistroEN, Program.oDatosDeConexion))
                                    {

                                        Fila.Cells[NombreLavePrimariaDetalle].Value = oRegistroEN.idProveedorLaboratorio;
                                        Fila.Cells["idLaboratorio"].Value = oRegistroEN.oLaboratorioEN.idLaboratorio.ToString();
                                        Fila.Cells["Codigo"].Value = oRegistroEN.oLaboratorioEN.Codigo;
                                        Fila.Cells["Actualizar"].Value = false;
                                        oLaboratorioLN = null;
                                        oEntidadEN = null;
                                        oEntidadLN = null;
                                        oRegistroEN = null;
                                        oRegistroLN = null;
                                        indice++;
                                        IndiceProgreso++;
                                        continue;

                                    }
                                    else
                                    {
                                        OcultarBarraDeProgreso();
                                        this.Cursor = Cursors.Default;

                                        oLaboratorioLN.Eliminar(oRegistroEN.oLaboratorioEN, Program.oDatosDeConexion);
                                        oEntidadLN.Eliminar(oEntidadEN, Program.oDatosDeConexion);
                                        MessageBox.Show(oLaboratorioLN.Error, OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        oEntidadEN = null;
                                        oEntidadLN = null;
                                        oLaboratorioLN = null;
                                        oRegistroEN = null;
                                        oRegistroLN = null;
                                        return false;
                                    }

                                }
                                else
                                {

                                    OcultarBarraDeProgreso();
                                    this.Cursor = Cursors.Default;

                                    oEntidadLN.Eliminar(oEntidadEN, Program.oDatosDeConexion);
                                    MessageBox.Show(oLaboratorioLN.Error, OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    oEntidadEN = null;
                                    oEntidadLN = null;
                                    oLaboratorioLN = null;
                                    oRegistroEN = null;
                                    oRegistroLN = null;
                                    return false;

                                }

                            }
                            else
                            {
                                OcultarBarraDeProgreso();
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(oEntidadLN.Error, OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                oEntidadLN = null;
                                oRegistroEN = null;
                                oRegistroLN = null;
                                return false;
                            }

                        }

                        this.Cursor = Cursors.Default;
                    }

                    OcultarBarraDeProgreso();
                    return true;

                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Información del laboratorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private EntidadEN informacionDeLaEntidadSuperiorDelLaboratorio()
        {
            EntidadEN oRegistroEN = new EntidadEN();

            try
            {
                oRegistroEN.oTipoDeEntidadEN.Nombre = "Laboratorio";
                oRegistroEN.oTipoDeEntidadEN.NombreInterno = "laboratorio";
                oRegistroEN.oLoginEN = Program.oLoginEN;
                oRegistroEN.IdUsuarioDeCreacion = Program.oLoginEN.idUsuario;
                oRegistroEN.IdUsuarioDeModificacion = Program.oLoginEN.idUsuario;
                oRegistroEN.FechaDeCreacion = System.DateTime.Now;
                oRegistroEN.FechaDeModificacion = System.DateTime.Now;

                return oRegistroEN;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Información de la entidad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return oRegistroEN;
            }
        }

        private LaboratorioEN InformacionDelLaboratorio(DataGridViewRow Fila)
        {

            LaboratorioEN oRegistroEN = new LaboratorioEN();
            
            int idLaboratorio;
            int.TryParse(Fila.Cells["idLaboratorio"].Value.ToString(), out idLaboratorio);
            oRegistroEN.idLaboratorio = idLaboratorio;
            oRegistroEN.Codigo = Fila.Cells["Codigo"].Value.ToString().Trim();
            oRegistroEN.Nombre = Fila.Cells["Nombre"].Value.ToString().Trim();
            oRegistroEN.Direccion = Fila.Cells["Direccion"].Value.ToString().Trim();
            oRegistroEN.Telefono = Fila.Cells["Telefono"].Value.ToString().Trim();
            oRegistroEN.Movil = Fila.Cells["Movil"].Value.ToString().Trim();
            oRegistroEN.Observaciones = Fila.Cells["Observaciones"].Value.ToString().Trim();
            oRegistroEN.Correo = Fila.Cells["Correo"].Value.ToString().Trim();
            oRegistroEN.Estado = Fila.Cells["Estado"].Value.ToString().Trim(); ;
            oRegistroEN.FechaDeCumpleanos = Fila.Cells["FechaDeCumpleanos"].Value.ToString().Trim();
            oRegistroEN.Messenger = Fila.Cells["Messenger"].Value.ToString().Trim();
            oRegistroEN.Twitter = Fila.Cells["Twitter"].Value.ToString();
            oRegistroEN.Facebook = Fila.Cells["Facebook"].Value.ToString();
            oRegistroEN.Skype = Fila.Cells["Skype"].Value.ToString();
            oRegistroEN.SitioWeb = Fila.Cells["SitioWeb"].Value.ToString();
            oRegistroEN.NoRUC = Fila.Cells["NoRUC"].Value.ToString();
            oRegistroEN.Foto = null;
            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.IdUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.IdUsuarioDeModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;

        }

        private ProveedorLaboratorioEN InformacionDelProveedorLaboratorio(DataGridViewRow Fila)
        {
            ProveedorLaboratorioEN oRegistroEN = new ProveedorLaboratorioEN();
            int idProveedorLaboratorio;
            int.TryParse(Fila.Cells["idProveedorLaboratorio"].Value.ToString(), out idProveedorLaboratorio);

            oRegistroEN.idProveedorLaboratorio = idProveedorLaboratorio;
            oRegistroEN.oLaboratorioEN = InformacionDelLaboratorio(Fila);

            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.IdUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.IdUsuarioDeModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;
        }

        private bool LosDatosIngresadosEnGrillaSonCorrectosDelLaboratorio(DataGridViewRow Fila)
        {
            try
            {
                if (Convert.ToBoolean(Fila.Cells["Eliminar"].Value) == false)
                {
                    object ValorAEvaluar = null;
                    DataGridViewCell CeldaAEvaluar = null;
                    string NombreCampoId, NombreCampoNombre;

                    //evaluar si agrego una marca                    
                    NombreCampoNombre = "Nombre";

                    ValorAEvaluar = Fila.Cells[NombreCampoNombre].Value;
                    if (ValorAEvaluar == null || string.IsNullOrEmpty(ValorAEvaluar.ToString()))
                    {
                        Fila.Selected = true;
                        dgvListarContacto.CurrentCell = CeldaAEvaluar;
                        dgvListarContacto.CurrentCell.ErrorText = "Es Necesario agregar un Nombre";
                        MessageBox.Show("Informacion del laboratorio \n\n Es necesario ingresar un Nombre", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }


                }
                return true;
            }
            catch (Exception ex)
            {
                Fila.Selected = true;
                dgvListarContacto.CurrentCell = Fila.Cells["idProveedorLaboratorio"];
                MessageBox.Show("Error al validar datos del Contacto: " + Fila.Cells["Nombre"].Value.ToString() + "\n" + ex.Message, "Buscar laboratorio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {

                frmLaboratorio oFrmRegistro = new frmLaboratorio();
                oFrmRegistro.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                oFrmRegistro.VariosRegistros = true;
                oFrmRegistro.ActivarFiltros = true;
                oFrmRegistro.TituloVentana = "Seleccionar Laboratorio";

                oFrmRegistro.AplicarFiltroDeWhereExterno = true;
                oFrmRegistro.WhereExterno = WhereLaboratorio();
                oFrmRegistro.ShowDialog();

                LaboratorioEN[] oRegistroEN = new LaboratorioEN[0];
                oRegistroEN = oFrmRegistro.oLaboratorio;

                if (oRegistroEN.Length > 0)
                {
                    foreach (LaboratorioEN oRegistro in oRegistroEN)
                    {
                        dgvListarLaboratorios.Rows.Add(false, 0, oRegistro.idLaboratorio, oRegistro.Codigo, oRegistro.Nombre, oRegistro.NoRUC, oRegistro.Direccion,
                        oRegistro.Telefono, oRegistro.Movil, oRegistro.Observaciones,
                        oRegistro.Correo, oRegistro.SitioWeb, oRegistro.FechaDeCumpleanos, oRegistro.Messenger, oRegistro.Skype,
                        oRegistro.Twitter, oRegistro.Facebook, oRegistro.Estado, true);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Seleccionar contacto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvListarLaboratorios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvListarLaboratorios.CurrentCell != null && dgvListarLaboratorios.Columns[dgvListarLaboratorios.CurrentCell.ColumnIndex].Name == "Eliminar")
            {
                if (Convert.ToBoolean(dgvListarLaboratorios.Rows[dgvListarLaboratorios.CurrentCell.RowIndex].Cells["Eliminar"].Value) == true)
                {
                    dgvListarLaboratorios.Rows[dgvListarLaboratorios.CurrentCell.RowIndex].DefaultCellStyle.Font = new Font(Font.Name, Font.Size, FontStyle.Strikeout);
                    dgvListarLaboratorios.Rows[dgvListarLaboratorios.CurrentCell.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                    dgvListarLaboratorios.Rows[dgvListarLaboratorios.CurrentCell.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Red;
                }
                else
                {
                    dgvListarLaboratorios.Rows[dgvListarLaboratorios.CurrentCell.RowIndex].DefaultCellStyle.Font = new Font(Font.Name, Font.Size, FontStyle.Regular);
                    dgvListarLaboratorios.Rows[dgvListarLaboratorios.CurrentCell.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    dgvListarLaboratorios.Rows[dgvListarLaboratorios.CurrentCell.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
                }

            }
        }

        private void dgvListarLaboratorios_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int idProveedorLaboratorio;
                int.TryParse(dgvListarLaboratorios.Rows[e.RowIndex].Cells["idProveedorLaboratorio"].Value.ToString(), out idProveedorLaboratorio);

                if (dgvListarLaboratorios.Rows[e.RowIndex].Cells["idProveedorLaboratorio"].Value == null)
                    return;
                
                if (idProveedorLaboratorio > 0 && dgvListarLaboratorios.Columns[e.ColumnIndex].Name != "Eliminar")
                {
                    dgvListarLaboratorios.Rows[e.RowIndex].Cells["Actualizar"].Value = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ingresar dato en la celda 'dgvLista_CellEndEdit'. \n" + ex.Message, "Listas de contactos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvListarLaboratorios_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvListarLaboratorios.IsCurrentCellDirty)
            {
                dgvListarLaboratorios.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvListarLaboratorios_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right && dgvListarLaboratorios.CurrentCell != null)
                {
                    //Este código permite seleccionar una fila del DatagridView al presionar click derecho
                    DataGridView.HitTestInfo Hitest = dgvListarLaboratorios.HitTest(e.X, e.Y);

                    if (Hitest.Type == DataGridViewHitTestType.Cell)
                    {
                        dgvListarLaboratorios.CurrentCell = dgvListarLaboratorios.Rows[Hitest.RowIndex].Cells[Hitest.ColumnIndex];
                        dgvListarLaboratorios.Rows[dgvListarLaboratorios.CurrentCell.RowIndex].Selected = true;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mouse down del Listado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvListarLaboratorios_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvListarLaboratorios.RowCount > 0 && dgvListarLaboratorios.SelectedRows.Count > 0)
                {
                    if (Convert.ToBoolean(dgvListarLaboratorios.Rows[dgvListarLaboratorios.SelectedRows[0].Index].Cells["Eliminar"].Value) == true)
                    {
                        dgvListarLaboratorios.Rows[dgvListarLaboratorios.SelectedRows[0].Index].DefaultCellStyle.SelectionForeColor = Color.Red;
                    }
                    else
                    {
                        dgvListarLaboratorios.Rows[dgvListarLaboratorios.SelectedRows[0].Index].DefaultCellStyle.SelectionForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Formato de fila", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
