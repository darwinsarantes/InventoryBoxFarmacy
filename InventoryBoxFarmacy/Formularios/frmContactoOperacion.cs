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
    public partial class frmContactoOperacion : Form
    {
        public frmContactoOperacion()
        {
            InitializeComponent();
        }

        public string OperacionARealizar { set; get; }
        public string NOMBRE_ENTIDAD_PRIVILEGIO { set; get; }
        public string NombreEntidad { set; get; }
        public int ValorLlavePrimariaEntidad { set; get; }
        public bool VariableLocal { set; get; }

        public ContactoEN oContacto = new ContactoEN();

        private bool CerrarVentana = false;

        private void frmAContactoOperacion_Shown(object sender, EventArgs e)
        {

            VariableLocal = false;
            LLenarComboProveedor();
            ObtenerValoresDeConfiguracion();            
            EstablecerTituloDeVentana();
            DeshabilitarControlesSegunOperacionesARealizar();
            LlamarMetodoSegunOperacion();
            configuracionDeVentana();

        }
        
        #region "Funciones"

        private void configuracionDeVentana()
        {
            try
            {

                tsbGuardar.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbActualzar.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbEliminar.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbRefrescar.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbRegistroLocal.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbNuevo.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbCerrarVentan.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Configuración de ventana", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeshabilitarControlesSegunOperacionesARealizar()
        {
            switch (this.OperacionARealizar.ToUpper())
            {
                case "NUEVO":
                    tsbGuardar.Visible = true;                    
                    tsbRegistroLocal.Visible = false;
                    tsbNuevo.Enabled = true;
                    
                    tsbActualzar.Visible = false;
                    tsbEliminar.Visible = false;
                    tsbRefrescar.Visible = false;
                    tsbSeparador.Visible = false;
                    tsbImprimir.Visible = false;
                    cmbProveedor.Visible = false;
                    lbEtiquetaProveedor.Visible = false;
                    lbListaDeProveedores.Visible = false;

                    txtIdentificador.ReadOnly = true;

                    LimpiarCampos();

                    break;

                case "MODIFICAR":
                    tsbGuardar.Visible = false;
                    tsbRegistroLocal.Visible = false;
                    tsbNuevo.Visible = false;
                    tsbActualzar.Visible = true;
                    tsbEliminar.Visible = false;
                    tsbRefrescar.Visible = true;
                    tsbSeparador.Visible = true;
                    tsbImprimir.Visible = true;

                    txtIdentificador.ReadOnly = true;
                    cmbProveedor.Visible = false;

                    break;

                case "ELIMINAR":
                    tsbGuardar.Visible = false;
                    tsbRegistroLocal.Visible = false;
                    tsbNuevo.Visible = false;
                    tsbActualzar.Visible = false;
                    tsbEliminar.Visible = true;
                    tsbRefrescar.Visible = true;
                    tsbImprimir.Visible = true;
                    tsbSeparador.Visible = true;

                    chkCerrarVentana.CheckState = CheckState.Checked;
                    chkCerrarVentana.Enabled = false;
                    txtIdentificador.ReadOnly = true;

                    txtDireccion.ReadOnly = true;
                    cmbProveedor.Visible = false;

                    break;

                case "CONSULTAR":

                    tsbGuardar.Visible = false;
                    tsbRegistroLocal.Visible = false;
                    tsbNuevo.Visible = false;
                    tsbActualzar.Visible = false;
                    tsbEliminar.Visible = false;
                    tsbRefrescar.Visible = true;
                    tsbSeparador.Visible = false;
                    tsbImprimir.Visible = true;

                    txtIdentificador.ReadOnly = true;

                    chkCerrarVentana.CheckState = CheckState.Checked;
                    chkCerrarVentana.Enabled = false;
                    txtDireccion.ReadOnly = true;
                    cmbProveedor.Visible = false;

                    break;

                case "LOCAL":

                    tsbGuardar.Visible = false;
                    tsbRegistroLocal.Visible = true;
                    tsbNuevo.Visible = false;
                    tsbActualzar.Visible = false;
                    tsbEliminar.Visible = false;
                    tsbSeparador.Visible = false;
                    tsbImprimir.Visible = false;
                    tsbRefrescar.Visible = false;

                    txtIdentificador.ReadOnly = true;

                    chkCerrarVentana.CheckState = CheckState.Checked;
                    chkCerrarVentana.Enabled = false;
                    
                    lbEtiquetaProveedor.Visible = false;
                    cmbProveedor.Visible = false;
                    lbListaDeProveedores.Visible = false;

                    LimpiarCampos();

                    break;

                default:
                    MessageBox.Show("La operación solicitada no está disponible.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

            }
        }

        private void ObtenerValoresDeConfiguracion()
        {
            chkCerrarVentana.CheckState = (Properties.Settings.Default.ContactoVentanaDespuesDeOperacion == true ? CheckState.Checked : CheckState.Unchecked);
            this.CerrarVentana = (Properties.Settings.Default.ContactoVentanaDespuesDeOperacion == true ? true : false);
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

                case "LOCAL":
                    RegistroLocal();
                    break;

                default:
                    MessageBox.Show("La operación solicitada no está disponible.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

            }
        }

        private void LlenarCamposDesdeBaseDatosSegunID()
        {
            this.Cursor = Cursors.WaitCursor;

            ContactoEN oRegistrosEN = new ContactoEN();
            ContactoLN oRegistrosLN = new ContactoLN();

            oRegistrosEN.idContacto = ValorLlavePrimariaEntidad;

            if (oRegistrosLN.ListadoPorIdentificador(oRegistrosEN, Program.oDatosDeConexion))
            {
                if (oRegistrosLN.TraerDatos().Rows.Count > 0)
                {

                    DataRow Fila = oRegistrosLN.TraerDatos().Rows[0];
                    txtCodigo.Text = Fila["Codigo"].ToString();
                    txtNombre.Text = Fila["Nombre"].ToString();
                    txtDireccion.Text = Fila["Direccion"].ToString();                                        
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
                    txtCedula.Text = Fila["Cedula"].ToString();

                    if (Controles.IsNullOEmptyElControl(txtFechaDeCumpleanos) == false)
                    {
                        dtpkFecha.Value = Convert.ToDateTime(txtFechaDeCumpleanos.Text);
                    }

                    if (Fila["Foto"] != DBNull.Value)
                    {
                        pbxImagen.Image = Imagenes.ProcesarImagenToBitMap((object)(Fila["Foto"]));
                    }

                    //LLenarCampoDeBaseDeDatosSegundContacto();
                    LLenarListBoxDeBaseDeDatosSegundContacto();

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

        private void LLenarCampoDeBaseDeDatosSegundContacto()
        {
            try
            {
                ProveedorContactoEN oRegistroEN = new ProveedorContactoEN();
                ProveedorContactoLN oRegistroLN = new ProveedorContactoLN();

                oRegistroEN.oContactoEN.idContacto = ValorLlavePrimariaEntidad;

                if(oRegistroLN.ListadoPorIdentificadorDelContacto(oRegistroEN, Program.oDatosDeConexion))
                {
                    if (oRegistroLN.TraerDatos().Rows.Count > 0)
                    {
                        cmbProveedor.SelectedValue = oRegistroEN.oProveedorEN.idProveedor;
                        txtIdProveedorContacto.Text = oRegistroEN.idProveedorContacto.ToString();
                    }

                }else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informacion del proveedor asociado al contacto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void LLenarListBoxDeBaseDeDatosSegundContacto()
        {
            try
            {
                ProveedorContactoEN oRegistroEN = new ProveedorContactoEN();
                ProveedorContactoLN oRegistroLN = new ProveedorContactoLN();

                oRegistroEN.oContactoEN.idContacto = ValorLlavePrimariaEntidad;

                if (oRegistroLN.ListadoPorIdentificadorDelContactoInformacion(oRegistroEN, Program.oDatosDeConexion))
                {
                    if (oRegistroLN.TraerDatos().Rows.Count > 0)
                    {
                        foreach(DataRow Fila in oRegistroLN.TraerDatos().Rows)
                        {
                            lbListaDeProveedores.Items.Add(Fila["Proveedor"].ToString());
                        }
                    }

                }
                else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informacion del proveedor asociado al contacto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void Nuevo()
        {
            GenerarCodigoDelContacto();
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

        private void RegistroLocal()
        {
           // GenerarCodigoDelContacto();
        }

        private void NuevoAPartirDeRegistroSeleccionado()
        {
            LlenarCamposDesdeBaseDatosSegunID();
        }

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
                
        private void GenerarCodigoDelContacto()
        {
            try
            {

                ContactoEN oRegistroEN = new ContactoEN();
                ContactoLN oRegistroLN = new ContactoLN();

                if(oRegistroLN.GenerarCodigoDelContacto(oRegistroEN, Program.oDatosDeConexion))
                {
                    txtCodigo.Text = oRegistroEN.Codigo;
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Generar codigo Automatico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void LimpiarCampos()
        {

            this.tabPage1.Controls.OfType<TextBox>().ToList().ForEach(o => o.Text = string.Empty);
            this.tabPage1.Controls.OfType<ComboBox>().ToList().ForEach(o => o.SelectedIndex = -1);
            this.tabPage1.Controls.OfType<DateTimePicker>().ToList().ForEach(o => o.Value = System.DateTime.Now);
            this.tabPage1.Controls.OfType<PictureBox>().ToList().ForEach(o => o.Image = null);

            this.tabPage2.Controls.OfType<TextBox>().ToList().ForEach(o => o.Text = string.Empty);
            this.tabPage2.Controls.OfType<ComboBox>().ToList().ForEach(o => o.SelectedIndex = -1);
            this.tabPage2.Controls.OfType<DateTimePicker>().ToList().ForEach(o => o.Value = System.DateTime.Now);
            this.tabPage2.Controls.OfType<PictureBox>().ToList().ForEach(o => o.Image = null);

            this.tabPage3.Controls.OfType<TextBox>().ToList().ForEach(o => o.Text = string.Empty);
            this.tabPage3.Controls.OfType<ComboBox>().ToList().ForEach(o => o.SelectedIndex = -1);
            this.tabPage3.Controls.OfType<DateTimePicker>().ToList().ForEach(o => o.Value = System.DateTime.Now);
            this.tabPage3.Controls.OfType<PictureBox>().ToList().ForEach(o => o.Image = null);

            GenerarCodigoDelContacto();

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

        private void LLenarComboProveedor()
        {
            try
            {

                ProveedorEN oRegistroEN = new ProveedorEN();
                ProveedorLN oRegistroLN = new ProveedorLN();

                oRegistroEN.Where = "";
                oRegistroEN.OrderBy = "";

                if (oRegistroLN.ListadoParaCombos(oRegistroEN, Program.oDatosDeConexion))
                {

                    if (oRegistroLN.TraerDatos().Rows.Count > 0)
                    {
                        
                        cmbProveedor.DataSource = oRegistroLN.TraerDatos();
                        cmbProveedor.DisplayMember = "NombreDelProveedor";
                        cmbProveedor.ValueMember = "idProveedor";

                        if (oRegistroLN.TraerDatos().Rows.Count > 0)
                        {
                            cmbProveedor.SelectedIndex = -1;
                        }

                        AutoCompleteStringCollection oDatos = new AutoCompleteStringCollection();
                        foreach(DataRow Fila in oRegistroLN.TraerDatos().Rows)
                        {
                            oDatos.Add(Fila["NombreDelProveedor"].ToString().Trim());
                        }

                        cmbProveedor.AutoCompleteCustomSource = oDatos;
                        cmbProveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmbProveedor.AutoCompleteSource = AutoCompleteSource.CustomSource;


                    }

                }else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Llenar combo proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private ContactoEN InformacionDelRegistro() {

            ContactoEN oRegistroEN = new ContactoEN();

            oRegistroEN.idContacto = Convert.ToInt32((txtIdentificador.Text.Length > 0 ? txtIdentificador.Text : "0"));
            oRegistroEN.Nombre = txtNombre.Text.Trim();
            oRegistroEN.Codigo = txtCodigo.Text.Trim();
            oRegistroEN.Direccion = txtDireccion.Text.Trim();
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
            oRegistroEN.Cedula = txtCedula.Text.Trim();

            if (pbxImagen.Image != null)
                oRegistroEN.AFoto = Imagenes.ProcesarImagenToByte((Bitmap)(pbxImagen.Image));
            else
                oRegistroEN.AFoto = null;

            oRegistroEN.Sexo = cmbSexo.Text.Trim();

            //partes generales.            
            oRegistroEN.oLoginEN = Program.oLoginEN;
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

        #endregion

        private void tsbCerrarVentan_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        private void GuardarValoresDeConfiguracion()
        {
            Properties.Settings.Default.ContactoVentanaDespuesDeOperacion = (chkCerrarVentana.CheckState == CheckState.Checked ? true : false);
            Properties.Settings.Default.Save();
        }

        private void tsbLimpiarCampos_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void EstablecerTituloDeVentana()
        {
            this.Text = string.Format("{0} - {1}", this.NombreEntidad, this.OperacionARealizar.ToUpper());
            this.InformacionEntidadOperacion.Text = this.NombreEntidad + " - " + this.OperacionARealizar;
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (LosDatosIngresadosSonCorrectos())
                {

                    ContactoEN oRegistroEN = InformacionDelRegistro();
                    ContactoLN oRegistroLN = new ContactoLN();

                    if (oRegistroLN.ValidarRegistroDuplicado(oRegistroEN, Program.oDatosDeConexion, "AGREGAR"))
                    {

                        MessageBox.Show(oRegistroLN.Error, "Guardar información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                    if(Controles.IsNullOEmptyElControl(txtCedula) == false)
                    {
                        if(oRegistroLN.ValidarRegistroDuplicadoParaCedula(oRegistroEN, Program.oDatosDeConexion, "AGREGAR"))
                        {
                            MessageBox.Show(oRegistroLN.Error, "Guardar información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    EntidadEN oEntidadEN = informacionDeLaEntidad();
                    EntidadLN oEntidadLN = new EntidadLN();

                    if (oEntidadLN.Agregar(oEntidadEN, Program.oDatosDeConexion))
                    {
                        oRegistroEN.idContacto = oEntidadEN.idEntidad;

                        if (oRegistroLN.Agregar(oRegistroEN, Program.oDatosDeConexion))
                        {
                            txtIdentificador.Text = oRegistroEN.idContacto.ToString();
                            ValorLlavePrimariaEntidad = oRegistroEN.idContacto;
                            txtCodigo.Text = oRegistroEN.Codigo;

                            GuardarActualizarVinculo();

                            EvaluarErrorParaMensajeAPantalla(oRegistroLN.Error, "Guardar");

                            if (CerrarVentana == true)
                            {
                                this.Cursor = Cursors.Default;
                                this.Close();
                            }else
                            {
                                OperacionARealizar = "Modificar";
                                ObtenerValoresDeConfiguracion();
                                DeshabilitarControlesSegunOperacionesARealizar();                                
                                EstablecerTituloDeVentana();
                                LlamarMetodoSegunOperacion();

                            }

                        }
                        else
                        {
                            oEntidadLN.Eliminar(oEntidadEN, Program.oDatosDeConexion);
                            string mensaje = string.Format("Se ha encontrado el siguiente error al Guardar la iformación del Contacto: {0} {1} {0} Desea continuar ingresando la información del Contacto", Environment.NewLine, oRegistroLN.Error);                            
                            throw new ArgumentException(mensaje);                            
                        }

                    }
                    else
                    {
                        string mensaje = string.Format("Se ha encontrado el siguiente error: {0} {1} {0} Desea continuar ingresando la información", Environment.NewLine, oEntidadLN.Error);
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

        private void GuardarActualizarVinculo()
        {
            if (cmbProveedor.SelectedIndex > -1 )
            {
                if (Controles.IsNullOEmptyElControl(txtIdProveedorContacto))
                {
                    GuardarVinculacionConElProveedor();
                }else if(Controles.IsNullOEmptyElControl(txtIdProveedorContacto) == false)
                {
                    ActualizarVinculacionConElProveedor();
                }
            }
          
        }

        private void GuardarVinculacionConElProveedor()
        {

            try
            {
                ProveedorContactoEN oRegistroEN = new ProveedorContactoEN();
                ProveedorContactoLN oRegistroLN = new ProveedorContactoLN();

                oRegistroEN.oContactoEN = InformacionDelRegistro();
                oRegistroEN.oProveedorEN.idProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
                oRegistroEN.oProveedorEN.Nombre = cmbProveedor.Text.Trim();
                oRegistroEN.oLoginEN = Program.oLoginEN;
                oRegistroEN.IdUsuarioDeCreacion = Program.oLoginEN.idUsuario;
                oRegistroEN.IdUsuarioDeModificacion = Program.oLoginEN.idUsuario;
                oRegistroEN.FechaDeCreacion = System.DateTime.Now;
                oRegistroEN.FechaDeModificacion = System.DateTime.Now;
                
                if(oRegistroLN.Agregar(oRegistroEN, Program.oDatosDeConexion))
                {
                    txtIdProveedorContacto.Text = oRegistroEN.idProveedorContacto.ToString();
                }else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Información del Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void ActualizarVinculacionConElProveedor()
        {

            try
            {
                ProveedorContactoEN oRegistroEN = new ProveedorContactoEN();
                ProveedorContactoLN oRegistroLN = new ProveedorContactoLN();

                oRegistroEN.oContactoEN = InformacionDelRegistro();
                oRegistroEN.idProveedorContacto = Convert.ToInt32(txtIdProveedorContacto.Text);
                oRegistroEN.oProveedorEN.idProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
                oRegistroEN.oProveedorEN.Nombre = cmbProveedor.Text.Trim();
                oRegistroEN.oLoginEN = Program.oLoginEN;
                oRegistroEN.IdUsuarioDeCreacion = Program.oLoginEN.idUsuario;
                oRegistroEN.IdUsuarioDeModificacion = Program.oLoginEN.idUsuario;
                oRegistroEN.FechaDeCreacion = System.DateTime.Now;
                oRegistroEN.FechaDeModificacion = System.DateTime.Now;

                if(oRegistroLN.ValidarRegistroDuplicado(oRegistroEN, Program.oDatosDeConexion, "ACTUALIZAR"))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (oRegistroLN.Actualizar(oRegistroEN, Program.oDatosDeConexion))
                {
                    txtIdProveedorContacto.Text = oRegistroEN.idProveedorContacto.ToString();
                }
                else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Información del Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void EliminarRegistroDelaVinculacion()
        {

            try
            {
                ProveedorContactoEN oRegistroEN = new ProveedorContactoEN();
                ProveedorContactoLN oRegistroLN = new ProveedorContactoLN();

                oRegistroEN.oContactoEN = InformacionDelRegistro();
                oRegistroEN.idProveedorContacto = Convert.ToInt32(txtIdProveedorContacto.Text);
                oRegistroEN.oProveedorEN.idProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
                oRegistroEN.oProveedorEN.Nombre = cmbProveedor.Text.Trim();
                oRegistroEN.oLoginEN = Program.oLoginEN;
                oRegistroEN.IdUsuarioDeCreacion = Program.oLoginEN.idUsuario;
                oRegistroEN.IdUsuarioDeModificacion = Program.oLoginEN.idUsuario;
                oRegistroEN.FechaDeCreacion = System.DateTime.Now;
                oRegistroEN.FechaDeModificacion = System.DateTime.Now;
                
                if (oRegistroLN.Eliminar(oRegistroEN, Program.oDatosDeConexion) == false)
                {                   
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Información del Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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
            catch(Exception ex)
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
                using ( System.IO.MemoryStream vMemoryStream = new System.IO.MemoryStream())
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

        private void tsbRegistroLocal_Click(object sender, EventArgs e)
        {
            try
            {
                oContacto = InformacionDelRegistro();
                VariableLocal = true;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Registro local", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tsbActualzar_Click(object sender, EventArgs e)
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

                    ContactoEN oRegistroEN = InformacionDelRegistro();
                    ContactoLN oRegistroLN = new ContactoLN();

                    /*if (oRegistroLN.ValidarSiElRegistroEstaVinculado(oRegistroEN, Program.oDatosDeConexion, "ACTUALIZAR"))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }*/

                    if (oRegistroLN.ValidarRegistroDuplicado(oRegistroEN, Program.oDatosDeConexion, "ACTUALIZAR"))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(oRegistroLN.Error, "Actualizar la información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (Controles.IsNullOEmptyElControl(txtCedula) == false)
                    {
                        if (oRegistroLN.ValidarRegistroDuplicadoParaCedula(oRegistroEN, Program.oDatosDeConexion, "ACTUALIZAR"))
                        {
                            MessageBox.Show(oRegistroLN.Error, "Guardar información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (oRegistroLN.Actualizar(oRegistroEN, Program.oDatosDeConexion))
                    {

                        GuardarActualizarVinculo();

                        EvaluarErrorParaMensajeAPantalla(oRegistroLN.Error, "Actualizar");

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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Actualizar información del registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    ContactoEN oRegistroEN = InformacionDelRegistro();
                    ContactoLN oRegistroLN = new ContactoLN();

                    if (Controles.IsNullOEmptyElControl(txtIdProveedorContacto)== false)
                    {
                        EliminarRegistroDelaVinculacion();
                    }
                    
                    if (oRegistroLN.Eliminar(oRegistroEN, Program.oDatosDeConexion))
                    {
                        
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eliminar la información del registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            LimpiarEP();
        }

        private void frmContactoOperacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            GuardarValoresDeConfiguracion();
        }

    }
}
