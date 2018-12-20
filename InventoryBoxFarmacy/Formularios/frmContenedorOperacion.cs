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
    public partial class frmContenedorOperacion : Form
    {
        public frmContenedorOperacion()
        {
            InitializeComponent();
        }

        public string OperacionARealizar { set; get; }
        public string NOMBRE_ENTIDAD_PRIVILEGIO { set; get; }
        public string NombreEntidad { set; get; }
        public int ValorLlavePrimariaEntidad { set; get; }
        public bool VariableLocal { set; get; }
        public ContenedorEN oContenedor = null;

        private bool CerrarVentana = false;

        private void frmContenedorOperacion_Shown(object sender, EventArgs e)
        {
            LLenarComboDelAlmacen();
            LLenarComboBodega();
            LLenarComboLocacion();
            LLenarComboSeccion();

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
                MessageBox.Show(ex.Message, "Informacion de la bodega", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show(ex.Message, "Informacion del estante/vitrina/caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LLenarComboSeccion()
        {
            try
            {
                SeccionEN oRegistroEN = new SeccionEN();

                if (cmbLocacion.SelectedIndex > -1)
                {
                    oRegistroEN.Where = string.Format(" and s.idLocacion = {0} ", cmbLocacion.SelectedValue);
                }
                else
                {
                    oRegistroEN.Where = "";
                }

                oRegistroEN.OrderBy = " Order by s.Nombre asc ";

                SeccionLN oRegistroLN = new SeccionLN();

                if (oRegistroLN.ListadoParaCombos(oRegistroEN, Program.oDatosDeConexion))
                {

                    cmbSeccion.DataSource = oRegistroLN.TraerDatos();
                    cmbSeccion.DisplayMember = "Seccion";
                    cmbSeccion.ValueMember = "idSeccion";
                    cmbSeccion.DropDownWidth = 268;

                    if (oRegistroLN.TraerDatos().Rows.Count == 1)
                    {
                        cmbSeccion.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbSeccion.SelectedIndex = -1;
                    }

                }
                else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informacion de la sección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            chkCerrarVentana.CheckState = (Properties.Settings.Default.ContenedorVentanaDespuesDeOperacion == true ? CheckState.Checked : CheckState.Unchecked);
            this.CerrarVentana = (Properties.Settings.Default.ContenedorVentanaDespuesDeOperacion == true ? true : false);
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
                    tsbRegistroLocal.Visible = false;
                    tsbLimpiarCampos.Visible = true;
                    tsbActualizar.Visible = false;
                    tsbEliminar.Visible = false;
                    tsbRecarRegistro.Visible = false;

                    txtIdentificador.ReadOnly = true;
                    txtDescripcion.Text = string.Empty;
                    txtCodigo.Text = string.Empty;
                    txtContenedor.Text = string.Empty;                             
                   
                    break;

                case "MODIFICAR":

                    tsbGuardar.Visible = false;
                    tsbRegistroLocal.Visible = false;
                    tsbLimpiarCampos.Visible = true;
                    tsbActualizar.Visible = true;
                    tsbEliminar.Visible = false;
                    tsbRecarRegistro.Visible = true;

                    txtIdentificador.ReadOnly = true;                    
                                        
                    break;

                case "ELIMINAR":
                    tsbGuardar.Visible = false;
                    tsbRegistroLocal.Visible = false;
                    tsbLimpiarCampos.Visible = false;
                    tsbActualizar.Visible = false;
                    tsbEliminar.Enabled = true;
                    tsbRecarRegistro.Visible = true;

                    chkCerrarVentana.CheckState = CheckState.Checked;
                    chkCerrarVentana.Enabled = false;
                    txtIdentificador.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    txtCodigo.ReadOnly = true;
                    txtContenedor.ReadOnly = true;

                    gbGeneral.Enabled = false;

                    break;

                case "CONSULTAR":
                    tsbGuardar.Visible = false;
                    tsbRegistroLocal.Visible = false;
                    tsbLimpiarCampos.Visible = false;
                    tsbActualizar.Visible = false;
                    tsbEliminar.Visible = false;
                    tsbRecarRegistro.Visible = true;
                    txtIdentificador.ReadOnly = true;
                    
                    chkCerrarVentana.CheckState = CheckState.Checked;
                    chkCerrarVentana.Enabled = false;
                    txtDescripcion.ReadOnly = true;
                    txtCodigo.ReadOnly = true;
                    txtContenedor.ReadOnly = true;
                    gbGeneral.Enabled = false;


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
                    txtContenedor.Text = string.Empty;
                    txtDescripcion.Text = string.Empty;

                    splitContainer1.Panel1Collapsed = true;
                    this.Width = 513;
                    this.Height = 462;

                    break;

                default:
                    MessageBox.Show("La operación solicitada no está disponible.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

            }
        }

        private void Nuevo()
        {

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

            ContenedorEN oRegistrosEN = new ContenedorEN();
            ContenedorLN oRegistrosLN = new ContenedorLN();

            oRegistrosEN.idContenedor = ValorLlavePrimariaEntidad;

            if (oRegistrosLN.ListadoPorIdentificador(oRegistrosEN, Program.oDatosDeConexion))
            {
                if (oRegistrosLN.TraerDatos().Rows.Count > 0)
                {
                    //idContenedor, DesContenedor, Debitos, Creditos
                    DataRow Fila = oRegistrosLN.TraerDatos().Rows[0];
                    txtDescripcion.Text = Fila["Descripcion"].ToString();
                    txtCodigo.Text = Fila["Codigo"].ToString();
                    txtContenedor.Text = Fila["Nombre"].ToString();
                    cmbAlmacen.SelectedValue = Convert.ToInt32(Fila["idAlmacen"]);
                    cmbBodega.SelectedValue = Convert.ToInt32(Fila["idBodega"]);
                    cmbLocacion.SelectedValue = Convert.ToInt32(Fila["idLocacion"]);
                    cmbSeccion.SelectedValue = Convert.ToInt32(Fila["idSeccion"]);

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
            txtCodigo.Text = string.Empty;
            txtContenedor.Text = string.Empty;
            txtDescripcion.Text = string.Empty;      
           
        }

        private void GuardarValoresDeConfiguracion()
        {
            Properties.Settings.Default.ContenedorVentanaDespuesDeOperacion = (chkCerrarVentana.CheckState == CheckState.Checked ? true : false);
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

            if (Controles.IsNullOEmptyElControl(txtContenedor))
            {
                EP.SetError(txtContenedor, "Este campo no puede quedar vacío");
                txtContenedor.Focus();
                return false;
            }

            if (Controles.IsNullOEmptyElControl(cmbSeccion))
            {
                EP.SetError(cmbSeccion, "Se debe seleccionar una sección");
                cmbSeccion.Focus();
                return false;
            }


            return true;

        }

        private ContenedorEN InformacionDelRegistro() {

            ContenedorEN oRegistroEN = new ContenedorEN();

            oRegistroEN.idContenedor = Convert.ToInt32((txtIdentificador.Text.Length > 0 ? txtIdentificador.Text : "0"));
            oRegistroEN.Codigo = txtCodigo.Text.Trim();
            oRegistroEN.Nombre = txtContenedor.Text.Trim();
            oRegistroEN.Descripcion = txtDescripcion.Text.Trim();
            oRegistroEN.oSeccionEN.idSeccion = Convert.ToInt32(cmbSeccion.SelectedValue);
            oRegistroEN.oSeccionEN.Nombre = cmbSeccion.Text;

            //partes generales.            
            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.idUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.idUsuarioModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            return oRegistroEN;

        }

        private ContenedorEN InformacionDelRegistroLocal()
        {

            ContenedorEN oRegistroEN = new ContenedorEN();

            oRegistroEN.idContenedor = Convert.ToInt32((txtIdentificador.Text.Length > 0 ? txtIdentificador.Text : "0"));
            oRegistroEN.Codigo = txtCodigo.Text.Trim();
            oRegistroEN.Nombre = txtContenedor.Text.Trim();
            oRegistroEN.Descripcion = txtDescripcion.Text.Trim();            
            
            return oRegistroEN;

        }

        private EntidadEN InformacionDeLaEntidad()
        {
            EntidadEN oRegistroEN = new EntidadEN();

            oRegistroEN.oTipoDeEntidadEN.NombreInterno = "Contenedor";
            oRegistroEN.oTipoDeEntidadEN.Nombre = "Contenedor";
            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.IdUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.IdUsuarioDeModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;
        }

        #endregion

        #region "Eventos del formulario"

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

        private void frmContenedorOperacion_FormClosing(object sender, FormClosingEventArgs e)
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

                    ContenedorEN oRegistroEN = InformacionDelRegistro();
                    ContenedorLN oRegistroLN = new ContenedorLN();

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
                        txtIdentificador.Text = oRegistroEN.idContenedor.ToString();
                        ValorLlavePrimariaEntidad = oRegistroEN.idContenedor;

                        EvaluarErrorParaMensajeAPantalla(oRegistroLN.Error, "Guardar");

                        oRegistroEN = null;
                        oRegistroLN = null;

                        this.Cursor = Cursors.Default;

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

                    ContenedorEN oRegistroEN = InformacionDelRegistro();
                    ContenedorLN oRegistroLN = new ContenedorLN();

                    if (oRegistroLN.VerificarSiLaEntidadEstaAsociadaAProducto(oRegistroEN, Program.oDatosDeConexion, "ACTUALIZAR"))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

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

                        txtIdentificador.Text = oRegistroEN.idContenedor.ToString();
                        ValorLlavePrimariaEntidad = oRegistroEN.idContenedor;

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

                    ContenedorEN oRegistroEN = InformacionDelRegistro();
                    ContenedorLN oRegistroLN = new ContenedorLN();

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

                    if (oRegistroLN.Eliminar(oRegistroEN, Program.oDatosDeConexion))
                    {

                        EvaluarErrorParaMensajeAPantalla(oRegistroLN.Error, "Eliminar");

                        oRegistroEN = null;
                        oRegistroLN = null;

                        this.Cursor = Cursors.Default;

                        this.Close();


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

        private void tsbRecarRegistro_Click(object sender, EventArgs e)
        {
            LlenarCamposDesdeBaseDatosSegunID();
        }

        private void tsbRegistroLocal_Click(object sender, EventArgs e)
        {
            try
            {
                oContenedor = InformacionDelRegistroLocal();
                VariableLocal = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registro local", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            if(Controles.IsNullOEmptyElControl(txtContenedor))
                txtContenedor.Text = txtCodigo.Text;
        }
    }
}
