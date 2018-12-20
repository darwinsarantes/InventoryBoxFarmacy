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
    public partial class frmSeccionOperacion : Form
    {
        public frmSeccionOperacion()
        {
            InitializeComponent();
        }

        public string OperacionARealizar { set; get; }
        public string NOMBRE_ENTIDAD_PRIVILEGIO { set; get; }
        public string NombreEntidad { set; get; }
        public int ValorLlavePrimariaEntidad { set; get; }
        public bool VariableLocal { set; get; }
        public SeccionEN oSeccion = null;

        private bool CerrarVentana = false;

        private void frmSeccionOperacion_Shown(object sender, EventArgs e)
        {
            LLenarComboDelAlmacen();
            LLenarComboBodega();
            LLenarComboLocacion();
            ObtenerValoresDeConfiguracion();
            LlamarMetodoSegunOperacion();
            EstablecerTituloDeVentana();
            DeshabilitarControlesSegunOperacionesARealizar();

            tsbGuardar.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsbEliminar.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsbActualizar.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsbLimpiarCampos.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsbRecarRegistro.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsbCerrarVentan.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

        }

        #region "Funciones"
        
        private void LLenarComboDelAlmacen()
        {
            try
            {
                AlmacenEN oRegistroEN = new AlmacenEN();
                AlmacenLN oRegistroLN = new AlmacenLN();

                if (oRegistroLN.ListadoParaCombos(oRegistroEN, Program.oDatosDeConexion))
                {

                    cmbAlmacen.DataSource = oRegistroLN.TraerDatos();
                    cmbAlmacen.DisplayMember = "Almacen";
                    cmbAlmacen.ValueMember = "idAlmacen";
                    cmbAlmacen.DropDownWidth = 268;

                    if (oRegistroLN.TraerDatos().Rows.Count == 1)
                    {
                        cmbAlmacen.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbAlmacen.SelectedIndex = -1;
                    }

                }
                else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informacion del Almacen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LLenarComboBodega()
        {
            try
            {
                BodegaEN oRegistroEN = new BodegaEN();

                if (cmbAlmacen.SelectedIndex > -1)
                {
                    oRegistroEN.Where = string.Format(" and a.idAlmacen = {0} ", cmbAlmacen.SelectedValue);
                }
                else
                {
                    oRegistroEN.Where = "";
                }

                oRegistroEN.OrderBy = " Order by a.Nombre asc ";

                BodegaLN oRegistroLN = new BodegaLN();

                if (oRegistroLN.ListadoParaCombos(oRegistroEN, Program.oDatosDeConexion))
                {

                    cmbBodega.DataSource = oRegistroLN.TraerDatos();
                    cmbBodega.DisplayMember = "Bodega";
                    cmbBodega.ValueMember = "idBodega";
                    cmbBodega.DropDownWidth = 268;

                    if (oRegistroLN.TraerDatos().Rows.Count == 1)
                    {
                        cmbBodega.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbBodega.SelectedIndex = -1;
                    }

                }
                else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informacion del Almacen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LLenarComboLocacion()
        {
            try
            {
                LocacionEN oRegistroEN = new LocacionEN();

                if (cmbBodega.SelectedIndex > -1)
                {
                    oRegistroEN.Where = string.Format(" and idBodega = {0} ", cmbBodega.SelectedValue);
                }
                else
                {
                    oRegistroEN.Where = "";
                }

                oRegistroEN.OrderBy = " Order by Nombre asc ";

                LocacionLN oRegistroLN = new LocacionLN();

                if (oRegistroLN.ListadoParaCombos(oRegistroEN, Program.oDatosDeConexion))
                {

                    cmbLocacion.DataSource = oRegistroLN.TraerDatos();
                    cmbLocacion.DisplayMember = "Locacion";
                    cmbLocacion.ValueMember = "idLocacion";
                    cmbLocacion.DropDownWidth = 268;

                    if (oRegistroLN.TraerDatos().Rows.Count == 1)
                    {
                        cmbLocacion.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbLocacion.SelectedIndex = -1;
                    }

                }
                else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informacion del Almacen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            chkCerrarVentana.CheckState = (Properties.Settings.Default.SeccionVentanaDespuesDeOperacion == true ? CheckState.Checked : CheckState.Unchecked);
            this.CerrarVentana = (Properties.Settings.Default.SeccionVentanaDespuesDeOperacion == true ? true : false);
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

        private void DeshabilitarControlesSegunOperacionesARealizar()
        {
            switch (this.OperacionARealizar.ToUpper())
            {
                case "NUEVO":
                    tsbGuardar.Visible = true;
                    tsbRecarRegistro.Visible = false;
                    tsbRegistroLocal.Visible = false;
                    tsbLimpiarCampos.Visible = true;
                    tsbActualizar.Visible = false;
                    tsbEliminar.Visible = false;                    

                    txtIdentificador.ReadOnly = true;
                    txtDescripcion.Text = string.Empty;                                    
                   
                    break;

                case "MODIFICAR":
                    tsbGuardar.Visible = false;
                    tsbRecarRegistro.Visible = false;
                    tsbLimpiarCampos.Visible = true;
                    tsbActualizar.Visible = true;
                    tsbEliminar.Visible = false;
                    tsbRegistroLocal.Visible = false;

                    txtIdentificador.ReadOnly = true;
                                        
                    break;

                case "ELIMINAR":
                    tsbGuardar.Visible = false;
                    tsbRecarRegistro.Visible = false;
                    tsbLimpiarCampos.Visible = false;
                    tsbActualizar.Visible = false;
                    tsbEliminar.Enabled = true;
                    tsbRegistroLocal.Visible = false;

                    chkCerrarVentana.CheckState = CheckState.Checked;
                    chkCerrarVentana.Enabled = false;
                    txtIdentificador.ReadOnly = true;
                    cmbAlmacen.Enabled = false;
                    cmbBodega.Enabled = false;
                    cmbLocacion.Enabled = false;

                    txtDescripcion.ReadOnly = true;
                    
                    break;

                case "CONSULTAR":
                    tsbGuardar.Visible = false;
                    tsbRecarRegistro.Visible = false;
                    tsbLimpiarCampos.Visible = false;
                    tsbActualizar.Visible = false;
                    tsbEliminar.Visible = false;
                    tsbRegistroLocal.Visible = false;
                    txtIdentificador.ReadOnly = true;
                    
                    chkCerrarVentana.CheckState = CheckState.Checked;
                    chkCerrarVentana.Enabled = false;
                    txtCodigo.ReadOnly = true;
                    txtSeccion.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    cmbAlmacen.Enabled = false;
                    cmbBodega.Enabled = false;
                    cmbLocacion.Enabled = false;

                    break;

                case "LOCAL":

                    tsbGuardar.Visible = false;
                    tsbRegistroLocal.Visible = true;
                    tsbLimpiarCampos.Visible = false;
                    tsbActualizar.Visible = false;
                    tsbEliminar.Visible = false;
                    tsbRecarRegistro.Visible = false;

                    txtIdentificador.ReadOnly = true;
                    txtCodigo.Text = string.Empty;
                    txtSeccion.Text = string.Empty;
                    txtDescripcion.Text = string.Empty;

                    tabControl1.TabPages.Remove(tabControl1.TabPages[tabControl1.TabPages.Count - 1]);
                    splitContainer1.Panel1Collapsed = true;
                    gbGeneral.Visible = false;

                    this.Width = 514;
                    this.Height = 517;

                    break;

                default:
                    MessageBox.Show("La operación solicitada no está disponible.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

            }
        }

        private void Nuevo()
        {
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

        private void RegistroLocal()
        {

        }

        private void LlenarCamposDesdeBaseDatosSegunID()
        {
            this.Cursor = Cursors.WaitCursor;

            SeccionEN oRegistrosEN = new SeccionEN();
            SeccionLN oRegistrosLN = new SeccionLN();

            oRegistrosEN.idSeccion = ValorLlavePrimariaEntidad;

            if (oRegistrosLN.ListadoPorIdentificador(oRegistrosEN, Program.oDatosDeConexion))
            {
                if (oRegistrosLN.TraerDatos().Rows.Count > 0)
                {
                    //idSeccion, DesSeccion, Debitos, Creditos
                    DataRow Fila = oRegistrosLN.TraerDatos().Rows[0];
                    txtDescripcion.Text = Fila["Descripcion"].ToString();
                    txtCodigo.Text = Fila["Codigo"].ToString();
                    txtSeccion.Text = Fila["Nombre"].ToString();
                    cmbAlmacen.SelectedValue = Convert.ToInt32(Fila["idAlmacen"]);
                    cmbBodega.SelectedValue = Convert.ToInt32(Fila["idBodega"]);
                    cmbLocacion.SelectedValue = Convert.ToInt32(Fila["idLocacion"]);

                    CrearyPoblarColumnasDGVContenedor();

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
            txtDescripcion.Text = string.Empty;            
           
        }

        private void GuardarValoresDeConfiguracion()
        {
            Properties.Settings.Default.SeccionVentanaDespuesDeOperacion = (chkCerrarVentana.CheckState == CheckState.Checked ? true : false);
            Properties.Settings.Default.Save();
        }

        private void LimpiarEP()
        {
            EP.Clear();
        }

        private bool LosDatosIngresadosSonCorrectos()
        {
            LimpiarEP();

            if (Controles.IsNullOEmptyElControl(txtCodigo))
            {
                EP.SetError(txtCodigo, "Este campo no puede quedar vacío");
                txtCodigo.Focus();
                return false;
            }

            if (Controles.IsNullOEmptyElControl(txtSeccion))
            {
                EP.SetError(txtSeccion, "Este campo no puede quedar vacío");
                txtSeccion.Focus();
                return false;
            }


            return true;

        }

        private SeccionEN InformacionDelRegistro() {

            SeccionEN oRegistroEN = new SeccionEN();

            oRegistroEN.idSeccion = Convert.ToInt32((txtIdentificador.Text.Length > 0 ? txtIdentificador.Text : "0"));
            oRegistroEN.Codigo = txtCodigo.Text.Trim();
            oRegistroEN.Nombre = txtSeccion.Text.Trim();
            oRegistroEN.Descripcion = txtDescripcion.Text.Trim();
            oRegistroEN.oLocacionEN.idLocacion = Convert.ToInt32(cmbLocacion.SelectedValue);
            oRegistroEN.oLocacionEN.Nombre = cmbLocacion.Text;

            //partes generales.            
            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.idUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.idUsuarioModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;

        }

        private SeccionEN InformacionDelRegistroLocal()
        {

            SeccionEN oRegistroEN = new SeccionEN();

            oRegistroEN.idSeccion = Convert.ToInt32((txtIdentificador.Text.Length > 0 ? txtIdentificador.Text : "0"));
            oRegistroEN.Codigo = txtCodigo.Text.Trim();
            oRegistroEN.Nombre = txtSeccion.Text.Trim();
            oRegistroEN.Descripcion = txtDescripcion.Text.Trim();
            
            //partes generales.            
            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.idUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.idUsuarioModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;

        }

        private void GuardarRegistro()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (LosDatosIngresadosSonCorrectos())
                {

                    SeccionEN oRegistroEN = InformacionDelRegistro();
                    SeccionLN oRegistroLN = new SeccionLN();

                    if (oRegistroLN.ValidarRegistroDuplicado(oRegistroEN, Program.oDatosDeConexion, "AGREGAR"))
                    {

                        MessageBox.Show(oRegistroLN.Error, "Guardar información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                    if (oRegistroLN.ValidarCodigo(oRegistroEN, Program.oDatosDeConexion, "AGREGAR"))
                    {

                        MessageBox.Show(oRegistroLN.Error, "Guardar información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                    if (oRegistroLN.Agregar(oRegistroEN, Program.oDatosDeConexion))
                    {

                        txtIdentificador.Text = oRegistroEN.idSeccion.ToString();
                        ValorLlavePrimariaEntidad = oRegistroEN.idSeccion;

                        EvaluarErrorParaMensajeAPantalla(oRegistroLN.Error, "Guardar");

                        oRegistroEN = null;
                        oRegistroLN = null;

                        this.Cursor = Cursors.Default;

                        if (InsertarActualizarOEliminarContenedor())
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

                        }
                        else
                        {
                            OperacionARealizar = "Modificar";
                            ObtenerValoresDeConfiguracion();
                            EstablecerTituloDeVentana();
                            DeshabilitarControlesSegunOperacionesARealizar();
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
                MessageBox.Show(ex.Message, "Guardar la información del registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void EliminarRegistro()
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

                    SeccionEN oRegistroEN = InformacionDelRegistro();
                    SeccionLN oRegistroLN = new SeccionLN();

                    if (oRegistroLN.VerificarSiLaEntidadEstaAsociadaAProducto(oRegistroEN, Program.oDatosDeConexion, "ELIMINAR"))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (oRegistroLN.ValidarSiElRegistroEstaVinculado(oRegistroEN, Program.oDatosDeConexion, "ELIMINAR"))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (InsertarActualizarOEliminarContenedor())
                    {

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

        private void ActualizarRegistro()
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

                    SeccionEN oRegistroEN = InformacionDelRegistro();
                    SeccionLN oRegistroLN = new SeccionLN();

                    if (oRegistroLN.VerificarSiLaEntidadEstaAsociadaAProducto(oRegistroEN, Program.oDatosDeConexion, "ACTUALIZAR"))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (oRegistroLN.ValidarSiElRegistroEstaVinculadoParaActualizacion(oRegistroEN, Program.oDatosDeConexion, "ACTUALIZAR"))
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

                    if (oRegistroLN.ValidarCodigo(oRegistroEN, Program.oDatosDeConexion, "ACTUALIZAR"))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(oRegistroLN.Error, "Actualizar la información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                    if (oRegistroLN.Actualizar(oRegistroEN, Program.oDatosDeConexion))
                    {

                        txtIdentificador.Text = oRegistroEN.idSeccion.ToString();
                        ValorLlavePrimariaEntidad = oRegistroEN.idSeccion;

                        EvaluarErrorParaMensajeAPantalla(oRegistroLN.Error, "Actualizar");

                        oRegistroEN = null;
                        oRegistroLN = null;

                        this.Cursor = Cursors.Default;

                        if (InsertarActualizarOEliminarContenedor())
                        {

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
        
        #endregion

        #region "Eventos de Fomrularios"

        private void tsbCerrarVentan_Click(object sender, EventArgs e)
        {
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

        private void frmSeccionOperacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            GuardarValoresDeConfiguracion();
        }
        
        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            GuardarRegistro();
        }
                
        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            ActualizarRegistro();
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void tsbRecarRegistro_Click(object sender, EventArgs e)
        {
            LlenarCamposDesdeBaseDatosSegunID();
        }

        private string WhereDinamicoDelContenedor()
        {
            string Where = "";
            try
            {
                if (dgvListar.Rows.Count > 0)
                {
                    String IdContenedor = "";
                    foreach (DataGridViewRow Fila in dgvListar.Rows)
                    {
                        int idContenedor;
                        int.TryParse(Fila.Cells["idContenedor"].Value.ToString(), out idContenedor);

                        if (IdContenedor.Trim().Length == 0)
                        {
                            if (idContenedor > 0)
                            {
                                IdContenedor = idContenedor.ToString();
                            }
                        }
                        else
                        {
                            if (idContenedor > 0)
                            {
                                IdContenedor = string.Format("{0}, {1}", IdContenedor, idContenedor.ToString());
                            }
                        }
                    }

                    if (IdContenedor.Trim().Length > 0)
                    {
                        Where = string.Format(" and cr.idContenedor Not in ({0}) ", IdContenedor);
                    }else
                    {
                        Where = "";
                    }
                }

                return Where;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Where dinamico buscar contenedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return Where;

            }
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            try
            {

                frmContenedorOperacion ofrmRegistro = new frmContenedorOperacion();
                ofrmRegistro.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                ofrmRegistro.OperacionARealizar = "Local";
                ofrmRegistro.NombreEntidad = "Agregar información del contenedor";
                ofrmRegistro.ShowDialog();

                if (ofrmRegistro.VariableLocal == true)
                {
                    ContenedorEN oRegistroEN = ofrmRegistro.oContenedor;

                    dgvListar.Rows.Add(false, 0, oRegistroEN.Codigo, oRegistroEN.Nombre, oRegistroEN.Descripcion, true);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "agregar registro del contacto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tsbNuevoRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                oSeccion = InformacionDelRegistroLocal();
                VariableLocal = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registro local", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region "Trabajando con el DataGrid"

            private void CrearColumnasDGVLaboratorios()
            {
                try
                {

                    string columnas = @"idContenedor,Codigo,Nombre,Descripcion";

                    string[] arrayColumnas = columnas.Split(',');

                    dgvListar.Columns.Clear();
                    dgvListar.ColumnCount = arrayColumnas.Length;

                    DataGridViewCheckBoxColumn col1 = new DataGridViewCheckBoxColumn();
                    dgvListar.Columns.Insert(0, col1);
                    dgvListar.Columns[0].Name = "Eliminar";

                    int j = 1;
                    foreach (string item in arrayColumnas)
                    {
                        dgvListar.Columns[j].Name = item.Trim();
                        j++;
                    }

                    DataGridViewCheckBoxColumn col2 = new DataGridViewCheckBoxColumn();
                    dgvListar.Columns.Insert(j, col2);
                    dgvListar.Columns[j].Name = "Actualizar";

                    FormatearDGVContenedor();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al llenar la lista. \n" + ex.Message, "Poblar columnas dgvListar con contenedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        
            private void OcultarColumnasEnELDGV(String ColumnasDelDGV)
            {
                if (dgvListar.Columns.Count > 0 )
                {
                    if (ColumnasDelDGV.Trim().Length > 0)
                    {

                        String[] ArrayColumnasDGV = ColumnasDelDGV.Split(',');
                        foreach (String c in ArrayColumnasDGV)
                        {

                            foreach (DataGridViewColumn c1 in dgvListar.Columns)
                            {
                                if (c1.Name.Trim().ToUpper() == c.Trim().ToUpper())
                                {
                                    c1.Visible = false;
                                }
                            }

                        }
                    }
                }
            }

            private void FormatearDGVContenedor()
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

                    string OcultarColumnas = "idContenedor,Actualizar";
                    OcultarColumnasEnELDGV(OcultarColumnas);

                    FormatearColumnasDGVListar();

                    if (OperacionARealizar == "Eliminar")
                    {
                        dgvListar.DefaultCellStyle.Font = new Font(Font.Name, Font.Size, FontStyle.Strikeout);
                        dgvListar.DefaultCellStyle.ForeColor = Color.Red;
                        dgvListar.DefaultCellStyle.SelectionForeColor = Color.Red;
                    }
                    else
                    {
                        dgvListar.DefaultCellStyle.Font = new Font(Font.Name, Font.Size, FontStyle.Regular);
                        dgvListar.DefaultCellStyle.ForeColor = Color.Black;
                        dgvListar.DefaultCellStyle.SelectionForeColor = Color.Black;
                    }

                    if (OperacionARealizar.ToUpper() == "ELIMINAR".ToUpper() || OperacionARealizar.ToUpper() == "CONSULTAR".ToUpper())
                    {
                        dgvListar.Columns["Eliminar"].ReadOnly = true;
                    }

                    this.dgvListar.RowHeadersWidth = 25;

                    this.dgvListar.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dgvListar.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                    this.dgvListar.StandardTab = true;
                    this.dgvListar.ReadOnly = false;
                    this.dgvListar.CellBorderStyle = DataGridViewCellBorderStyle.Single;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "FormatoDGV", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void FormatearColumnasDGVListar()
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
                                    oFormato = new FormatoDGV(c1.Name.Trim(), "SeccionContenedor");
                                }

                                if (oFormato != null)
                                {
                                    if (oFormato.ValorEncontrado == true)
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
            }

            private string DescripcionDetallaDelContenedor(DataGridView dgv)
            {
                string Mensaje = "";

                if (dgv.Rows.Count > 0)
                {

                    List<DataGridViewRow> rows = (from item in dgv.Rows.Cast<DataGridViewRow>()
                                                  let Actualizar = Convert.ToBoolean(item.Cells["Actualizar"].Value ?? false)
                                                  let idContenedor = Convert.ToInt32(item.Cells["idContenedor"].Value)
                                                  where Actualizar.Equals(true) && idContenedor == 0
                                                  select item).ToList<DataGridViewRow>();
                    if (rows.Count > 0)
                    {
                        Mensaje += string.Format(" Se va a agregar: {1} Registros {0}", Environment.NewLine, rows.Count);
                    }

                    List<DataGridViewRow> rows1 = (from item in dgv.Rows.Cast<DataGridViewRow>()
                                                   let Actualizar = Convert.ToBoolean(item.Cells["Actualizar"].Value ?? false)
                                                   let idContenedor = Convert.ToInt32(item.Cells["idContenedor"].Value)
                                                   where Actualizar.Equals(true) && idContenedor > 0
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
                    Mensaje = string.Format("Información de los contenedores: {0}", Mensaje);
                }

                return Mensaje;

            }

            private DataTable InformacionDeLosContendoresPorSeccion()
            {
                DataTable ODatos = null;
                try
                {

                    ContenedorEN oRegistroEN = new ContenedorEN();
                    ContenedorLN oRegistroLN = new ContenedorLN();

                    oRegistroEN.Where = string.Format(" and idSeccion = {0} ", ValorLlavePrimariaEntidad);
                    oRegistroEN.OrderBy = " Order by Nombre asc ";

                    if (oRegistroLN.ListadoPorInclusionDeSecciones(oRegistroEN, Program.oDatosDeConexion))
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

            private void CrearyPoblarColumnasDGVContenedor()
            {
                try
                {

                    CrearColumnasDGVLaboratorios();

                    DataTable DTOrden = InformacionDeLosContendoresPorSeccion();

                    if (DTOrden != null)
                    {

                        if (DTOrden.Rows.Count > 0)
                        {
                            int i = 1;
                            Boolean valor = false;
                            if (OperacionARealizar == "Eliminar") { valor = true; } else { valor = false; }
                            
                            int idContenedor = 0;

                            foreach (DataRow row in DTOrden.Rows)
                            {

                                if (OperacionARealizar.ToUpper() == "NUEVO A PARTIR DE REGISTRO SELECCIONADO".ToUpper())
                                {                                    
                                    idContenedor = Convert.ToInt32(row["idContenedor"]);
                                }
                                else
                                {
                                    idContenedor = Convert.ToInt32(row["idContenedor"]);                                 
                                }

                                dgvListar.Rows.Add(
                                    valor,                                    
                                    idContenedor,
                                    row["Codigo"],
                                    row["Nombre"],
                                    row["Descripcion"],
                                    valor
                                    );

                                i++;

                            }

                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Crear y Poblar Columnas, con los diferentes contenedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        
            private void dgvListar_SelectionChanged(object sender, EventArgs e)
            {
                try
                {
                    if (dgvListar.RowCount > 0 && dgvListar.SelectedRows.Count > 0)
                    {
                        if (Convert.ToBoolean(dgvListar.Rows[dgvListar.SelectedRows[0].Index].Cells["Eliminar"].Value) == true)
                        {
                            dgvListar.Rows[dgvListar.SelectedRows[0].Index].DefaultCellStyle.SelectionForeColor = Color.Red;
                        }
                        else
                        {
                            dgvListar.Rows[dgvListar.SelectedRows[0].Index].DefaultCellStyle.SelectionForeColor = Color.Black;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Formato de fila", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void dgvListar_MouseDown(object sender, MouseEventArgs e)
            {
                try
                {
                    if (e.Button == MouseButtons.Right && dgvListar.CurrentCell != null)
                    {
                        //Este código permite seleccionar una fila del DatagridView al presionar click derecho
                        DataGridView.HitTestInfo Hitest = dgvListar.HitTest(e.X, e.Y);

                        if (Hitest.Type == DataGridViewHitTestType.Cell)
                        {
                            dgvListar.CurrentCell = dgvListar.Rows[Hitest.RowIndex].Cells[Hitest.ColumnIndex];
                            dgvListar.Rows[dgvListar.CurrentCell.RowIndex].Selected = true;
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Mouse down del Listado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void dgvListar_CurrentCellDirtyStateChanged(object sender, EventArgs e)
            {
                if (dgvListar.IsCurrentCellDirty)
                {
                    dgvListar.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }

            private void dgvListar_CellEndEdit(object sender, DataGridViewCellEventArgs e)
            {
                try
                {
                    int idContenedor;
                    int.TryParse(dgvListar.Rows[e.RowIndex].Cells["idContenedor"].Value.ToString(), out idContenedor);

                    if (dgvListar.Rows[e.RowIndex].Cells["idContenedor"].Value == null)
                        return;

                    if (idContenedor > 0 && dgvListar.Columns[e.ColumnIndex].Name != "Eliminar")
                    {
                        dgvListar.Rows[e.RowIndex].Cells["Actualizar"].Value = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al ingresar dato en la celda 'dgvLista_CellEndEdit'. \n" + ex.Message, "Listas de contactos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void dgvListar_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                if (dgvListar.CurrentCell != null && dgvListar.Columns[dgvListar.CurrentCell.ColumnIndex].Name == "Eliminar")
                {
                    if (Convert.ToBoolean(dgvListar.Rows[dgvListar.CurrentCell.RowIndex].Cells["Eliminar"].Value) == true)
                    {
                        dgvListar.Rows[dgvListar.CurrentCell.RowIndex].DefaultCellStyle.Font = new Font(Font.Name, Font.Size, FontStyle.Strikeout);
                        dgvListar.Rows[dgvListar.CurrentCell.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                        dgvListar.Rows[dgvListar.CurrentCell.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Red;
                    }
                    else
                    {
                        dgvListar.Rows[dgvListar.CurrentCell.RowIndex].DefaultCellStyle.Font = new Font(Font.Name, Font.Size, FontStyle.Regular);
                        dgvListar.Rows[dgvListar.CurrentCell.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                        dgvListar.Rows[dgvListar.CurrentCell.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
                    }

                }
            }

            private ContenedorEN InformacionDelContenedor(DataGridViewRow Fila)
            {
                ContenedorEN oRegistro = new ContenedorEN();

                oRegistro.idContenedor = Convert.ToInt32(Fila.Cells["idContenedor"].Value.ToString());
                oRegistro.Codigo = Fila.Cells["Codigo"].Value.ToString();
                oRegistro.Nombre = Fila.Cells["Nombre"].Value.ToString();
                oRegistro.Descripcion = Fila.Cells["Descripcion"].Value.ToString();
                oRegistro.oSeccionEN = InformacionDelRegistro();
                oRegistro.oLoginEN = Program.oLoginEN;
                oRegistro.idUsuarioDeCreacion = Program.oLoginEN.idUsuario;
                oRegistro.idUsuarioModificacion = Program.oLoginEN.idUsuario;
                oRegistro.FechaDeCreacion = System.DateTime.Now;
                oRegistro.FechaDeModificacion = System.DateTime.Now;

                return oRegistro;
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
                            dgvListar.CurrentCell = CeldaAEvaluar;
                            dgvListar.CurrentCell.ErrorText = "Es Necesario agregar un Nombre";
                            MessageBox.Show("Informacion de Contenedor \n\n Es necesario ingresar un Nombre", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }


                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Fila.Selected = true;
                    dgvListar.CurrentCell = Fila.Cells["idContenedor"];
                    MessageBox.Show("Error al validar datos del Contacto: " + Fila.Cells["Nombre"].Value.ToString() + "\n" + ex.Message, "Buscar Contenedor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
        
            private bool InsertarActualizarOEliminarContenedor()
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    if (EvaluarDataGridView(dgvListar) == false)
                    {
                        MessageBox.Show("No se encontró registros a procesar", "Evaluación de registros en la lista", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return true;
                    }
                    else { MessageBox.Show(DescripcionDetallaDelContenedor(dgvListar), "Registros a procesar", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                    int RowsContacto = dgvListar.Rows.Count;

                    if (RowsContacto > 0)
                    {

                        MostrarBarraDeProgreso();
                        InicializarBarraDeProgreso(RowsContacto, 0);
                        int indice = 0;
                        int IndiceProgreso = 0;
                        int TotalDeFilasMarcadasParaEliminar = TotalDeFilasMarcadas(dgvListar, "Eliminar");
                        //Aqui Volvemos dinamica El codigo poniendo el valor de la llave primaria 
                        string NombreLavePrimariaDetalle = "idContenedor";
                    
                        while (indice <= dgvListar.Rows.Count - 1)
                        {
                            this.Cursor = Cursors.WaitCursor;


                            IncrementarBarraDeProgreso(IndiceProgreso + 1);
                            DataGridViewRow Fila = dgvListar.Rows[indice];

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


                            if (LosDatosIngresadosEnGrillaSonCorrectos(Fila) == false)
                            {
                                if (indice < dgvListar.Rows.Count - 1)
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

                            ContenedorEN oRegistroEN = InformacionDelContenedor(Fila);
                            ContenedorLN oRegistroLN = new ContenedorLN();


                            //DETERMINAMOS LA OPERACION A REALIZAR
                            string Operacion = "";
                            
                            //El orden es importante porque si un usuario agrego una nueva persona pero lo marco para eliminar, no hacemos nada, solo lo quitamos de la lista.
                            if (ValorDelaLLavePrimaria == 0 && Eliminar == true) { Operacion = "ELIMINAR FILA EN GRILLA"; }
                            //VALIDAREMOS QUE LA LLAVE PRIMARIA SEA CERO Y EL CONTARO SEA MAYOR A CERO PARA UN NUEVO VINCULO ENTRE PROVEEDOR Y CONTACTO
                            else if (ValorDelaLLavePrimaria == 0 ) { Operacion = "AGREGAR"; }
                            //VALIDAREMOS PARA PODER ELIMINAR EL REGISTRO....
                            else if (ValorDelaLLavePrimaria > 0 && Eliminar == true) { Operacion = "ELIMINAR"; }
                            //VALIDAREMOS PARA PODER ACTUALIZAR EL REGISTRO
                            else if (ValorDelaLLavePrimaria > 0 && Actualizar == true) { Operacion = "ACTUALIZAR"; }
                            //NO EXISTE NINGUNA OPERACION
                            else if (ValorDelaLLavePrimaria >= 0 && Actualizar == false && Eliminar == false) { Operacion = "NINGUNA"; }

                            //Validaciones 
                            if (Operacion == "ELIMINAR FILA EN GRILLA")
                            {
                                dgvListar.Rows.Remove(Fila);
                                if (dgvListar.RowCount <= 0) { indice++; IndiceProgreso++; }
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

                            if (oRegistroLN.VerificarSiLaEntidadEstaAsociadaAProducto(oRegistroEN, Program.oDatosDeConexion, "ACTUALIZAR"))
                            {
                                this.Cursor = Cursors.Default;
                                DialogResult Respuesta = MessageBox.Show(oRegistroLN.Error + "\n\n¿Desea continuar con el proceso de actualización para los empleados restantes?", this.OperacionARealizar, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                if (Respuesta == DialogResult.No)
                                {
                                    OcultarBarraDeProgreso();
                                    return false;
                                }
                                
                            }

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

                            if (oRegistroLN.VerificarSiLaEntidadEstaAsociadaAProducto(oRegistroEN, Program.oDatosDeConexion, "ELIMINAR"))
                            {
                                this.Cursor = Cursors.Default;
                                DialogResult Respuesta = MessageBox.Show(oRegistroLN.Error + "\n\n¿Desea continuar con el proceso de eliminación para los empleados restantes?", this.OperacionARealizar, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                if (Respuesta == DialogResult.No)
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

                            if (oRegistroLN.ValidarSiElRegistroEstaVinculado(oRegistroEN, Program.oDatosDeConexion, "ELIMINAR"))
                                {
                                    this.Cursor = Cursors.Default;
                                    DialogResult Respuesta = MessageBox.Show(oRegistroLN.Error + "\n\n¿Desea continuar con el proceso de eliminación para los empleados restantes?", this.OperacionARealizar, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                    if (Respuesta == DialogResult.No)
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
                        }
                                                       
                            //OPERACIONES
                            if (Operacion == "AGREGAR")
                            {
                                if (oRegistroLN.Agregar(oRegistroEN, Program.oDatosDeConexion))
                                {
                                    Fila.Cells[NombreLavePrimariaDetalle].Value = oRegistroEN.idContenedor;
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

                                    dgvListar.Rows[Fila.Index].Cells["Actualizar"].Value = false;
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
                                    dgvListar.Rows.Remove(Fila);
                                    oRegistroEN = null;
                                    oRegistroLN = null;
                                    if (dgvListar.RowCount <= 0) { indice++; }
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
                    MessageBox.Show(ex.Message, "Información del Contenedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }

            private int TotalDeFilasMarcadas(DataGridView odgvGrid, String Columna)
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

            #region "Barra de Progreso"

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



        #endregion

        #endregion

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            txtSeccion.Text = txtCodigo.Text;
        }

        private void tsbEditarlFilas_Click(object sender, EventArgs e)
        {

            tsbEditarlFilas.Checked = !tsbEditarlFilas.Checked;
            bool activarEdicion = true;
            this.dgvListar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (tsbEditarlFilas.Checked == true)
            {
                this.dgvListar.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                activarEdicion = false;
            }

            if (dgvListar.Rows.Count > 0)
            {

                foreach (DataGridViewRow Fila in dgvListar.Rows)
                {
                    Fila.Cells["Codigo"].ReadOnly = activarEdicion;
                    Fila.Cells["Nombre"].ReadOnly = activarEdicion;
                    Fila.Cells["Descripcion"].ReadOnly = activarEdicion;
                }
            }
        }

        private void tsbGeneradorAutomaticoParaContenedores_Click(object sender, EventArgs e)
        {
            try
            {

                frmGenerarSeccionOContenedores oFrmAuto = new frmGenerarSeccionOContenedores();
                oFrmAuto.StartPosition = FormStartPosition.CenterScreen;
                oFrmAuto.TituloDeLaVentana = "Generar Contenedores";
                oFrmAuto.TituloDelGroupBox = "Generar Contenedores automaticos";
                oFrmAuto.ShowDialog();

                if (oFrmAuto.AplicarAutomatico)
                {
                    int idSeccion = Convert.ToInt32(txtIdentificador.Text);

                    for (int i = oFrmAuto.ValorInicial; i <= oFrmAuto.ValorFinal; i++)
                    {
                        string valor = "";
                        if (i <= 9) { valor = string.Format("0{0}", i.ToString()); } else { valor = i.ToString(); }                        
                        dgvListar.Rows.Add(false, 0, valor, valor, "", true);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Generar Contenedores de manera automatica", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
