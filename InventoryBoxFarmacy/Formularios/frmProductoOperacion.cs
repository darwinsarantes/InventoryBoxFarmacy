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
    public partial class frmProductoOperacion : Form
    {
        public frmProductoOperacion()
        {
            InitializeComponent();
        }

        public string OperacionARealizar { set; get; }
        public string NOMBRE_ENTIDAD_PRIVILEGIO { set; get; }
        public string NombreEntidad { set; get; }
        public int ValorLlavePrimariaEntidad { set; get; }

        private bool CerrarVentana = false;

        private void frmProductoOperacion_Shown(object sender, EventArgs e)
        {
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
            chkCerrarVentana.CheckState = (Properties.Settings.Default.ProductoVentanaDespuesDeOperacion == true ? CheckState.Checked : CheckState.Unchecked);
            this.CerrarVentana = (Properties.Settings.Default.ProductoVentanaDespuesDeOperacion == true ? true : false);
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

                    txtIdentificador.ReadOnly = true;
                    txtCodigoDeBarra.Text = string.Empty;

                    break;

                case "MODIFICAR":
                    tsbGuardar.Visible = false;
                    tsbLimpiarCampos.Visible = true;
                    tsbActualizar.Visible = true;
                    tsbEliminar.Visible = false;
                    tsbRecarRegistro.Visible = true;

                    txtIdentificador.ReadOnly = true;

                    break;

                case "ELIMINAR":
                    tsbGuardar.Visible = false;
                    tsbLimpiarCampos.Visible = false;
                    tsbActualizar.Visible = false;
                    tsbEliminar.Enabled = true;
                    tsbRecarRegistro.Visible = true;

                    chkCerrarVentana.CheckState = CheckState.Checked;
                    chkCerrarVentana.Enabled = false;
                    txtIdentificador.ReadOnly = true;

                    txtCodigoDeBarra.ReadOnly = true;

                    break;

                case "CONSULTAR":
                    tsbGuardar.Visible = false;
                    tsbLimpiarCampos.Visible = false;
                    tsbActualizar.Visible = false;
                    tsbEliminar.Visible = false;
                    tsbRecarRegistro.Visible = true;
                    txtIdentificador.ReadOnly = true;

                    chkCerrarVentana.CheckState = CheckState.Checked;
                    chkCerrarVentana.Enabled = false;
                    txtCodigoDeBarra.ReadOnly = true;


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

        private void LlenarCamposDesdeBaseDatosSegunID()
        {
            this.Cursor = Cursors.WaitCursor;

            ProductoEN oRegistrosEN = new ProductoEN();
            ProductoLN oRegistrosLN = new ProductoLN();

            oRegistrosEN.idProducto = ValorLlavePrimariaEntidad;

            if (oRegistrosLN.ListadoPorIdentificador(oRegistrosEN, Program.oDatosDeConexion))
            {
                if (oRegistrosLN.TraerDatos().Rows.Count > 0)
                {
                    //idProducto, DesProducto, Debitos, Creditos
                    DataRow Fila = oRegistrosLN.TraerDatos().Rows[0];
                    txtCodigoDeBarra.Text = Fila["Producto"].ToString();

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
            txtCodigoDeBarra.Text = string.Empty;

        }

        private void GuardarValoresDeConfiguracion()
        {
            Properties.Settings.Default.ProductoVentanaDespuesDeOperacion = (chkCerrarVentana.CheckState == CheckState.Checked ? true : false);
            Properties.Settings.Default.Save();
        }

        private void LimpiarEP()
        {
            EP.Clear();
        }

        private bool LosDatosIngresadosSonCorrectos()
        {
            LimpiarEP();

            if (Controles.IsNullOEmptyElControl(txtCodigoDeBarra))
            {
                EP.SetError(txtCodigoDeBarra, "Este campo no puede quedar vacío");
                txtCodigoDeBarra.Focus();
                return false;
            }


            return true;

        }

        private ProductoEN InformacionDelRegistro() {

            ProductoEN oRegistroEN = new ProductoEN();

            oRegistroEN.idProducto = Convert.ToInt32((txtIdentificador.Text.Length > 0 ? txtIdentificador.Text : "0"));
            oRegistroEN.Nombre = txtCodigoDeBarra.Text.Trim();
            oRegistroEN.NombreComun = txtNombreComun.Text.Trim();
            oRegistroEN.Codigo = txtCodigo.Text.Trim();
            oRegistroEN.CodigoDeBarra = txtCodigoDeBarra.Text.Trim();
            oRegistroEN.Descripcion = txtDescripcion.Text.Trim();
            oRegistroEN.NombreGenerico = txtNombreGenerico.Text.Trim();
            decimal Minimo;
            decimal.TryParse(txtMinimo.Text, out Minimo);
            oRegistroEN.Minimo = Minimo;
            decimal Maximo;
            decimal.TryParse(txtMaximo.Text, out Maximo);
            oRegistroEN.Maximo = Maximo;
            decimal Existencia;
            decimal.TryParse(txtExistencias.Text, out Existencia);
            oRegistroEN.Existencias = Existencia;
            oRegistroEN.oCategoria.idCategoria = Convert.ToInt32(cmbCategoria.SelectedValue);
            oRegistroEN.oCategoria.Nombre = cmbCategoria.Text.Trim();
            oRegistroEN.oPresentacion.idProductoPresentacion = Convert.ToInt32(cmbPresentacion.SelectedValue);
            oRegistroEN.oPresentacion.Nombre = cmbPresentacion.Text.Trim();
            oRegistroEN.oUnidadDeMedida.idProductoUnidadDeMedida = Convert.ToInt32(cmbUnidadDeMedida.SelectedValue);
            oRegistroEN.oUnidadDeMedida.Nombre = cmbUnidadDeMedida.Text.Trim();

            oRegistroEN.idAlmacenEntidad = Convert.ToInt32(txtAlmacenIdentidad.Text);
            oRegistroEN.idPLEntidad = Convert.ToInt32(txtPLEntidad.Text);
            oRegistroEN.Observaciones = txtObservacion.Text.Trim();

            //partes generales.            
            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.idUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.idUsuarioModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;


            return oRegistroEN;

        }

        private ProductoPrecioEN InformacionDelPrecioDelProducto()
        {
            ProductoPrecioEN oRegistroEN = new ProductoPrecioEN();

            oRegistroEN.idProductoPrecio = Convert.ToInt32(txtIdPrecio.Text);
            decimal Costo;
            decimal.TryParse(txtCosto.Text, out Costo);
            decimal Porcentaje1;
            decimal.TryParse(txtPorcentaje1.Text, out Porcentaje1);
            decimal Porcentaje2;
            decimal.TryParse(txtPorcentaje2.Text, out Porcentaje2);
            decimal Porcentaje3;
            decimal.TryParse(txtPorcentaje3.Text, out Porcentaje3);
            decimal Porcentaje4;
            decimal.TryParse(txtPorcentaje4.Text, out Porcentaje4);
            decimal Porcentaje5;
            decimal.TryParse(txtPorcentaje5.Text, out Porcentaje5);
            decimal Precio1;
            decimal.TryParse(txtPrecio1.Text, out Precio1);
            decimal Precio2;
            decimal.TryParse(txtPrecio2.Text, out Precio2);
            decimal Precio3;
            decimal.TryParse(txtPrecio3.Text, out Precio3);
            decimal Precio4;
            decimal.TryParse(txtPrecio4.Text, out Precio4);
            decimal Precio5;
            decimal.TryParse(txtPrecio5.Text, out Precio5);

            oRegistroEN.AplicarElIva = 0;
            if (chkAplicarIVA.CheckState == CheckState.Checked)
            {
                oRegistroEN.AplicarElIva = 1;
            }

            decimal ValorDelIVA;
            decimal.TryParse(txtIVA.Text, out ValorDelIVA);
            oRegistroEN.ValorDelIvaEnProcentaje = ValorDelIVA;
            oRegistroEN.Estado = "ACTIVO";
            oRegistroEN.oProductoEN = InformacionDelRegistro();

            //partes generales.            
            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.idUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.idUsuarioModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;

        }
        
        private ProductoConfiguracionEN InformacionDeLaConfiguracionDelProducto()
        {

            ProductoConfiguracionEN oRegistroEN = new ProductoConfiguracionEN();
            oRegistroEN.idProductoConfiguracion = Convert.ToInt32(txtidProductoConfiguracion.Text);
            oRegistroEN.oProductoEN = InformacionDelRegistro();
            oRegistroEN.AplicarComisiones = chkAplicarComision.CheckState == CheckState.Checked ? 1 : 0;
            oRegistroEN.MostrarContenidoDeObservacionesENFactura = chkMostrarObservacionesDelProducto.CheckState == CheckState.Checked ? 1: 0;
            oRegistroEN.MontoFijoPorVenta = rbMontoFijoPorVenta.Checked == true ? 1 : 0;
            oRegistroEN.PorcentajeDeLaGanacia = rbPorcentajeDeLaGanancia.Checked == true ? 1 : 0;
            oRegistroEN.PorcentajeDeLaVenta = rbPorcentajeDeLaVenta.Checked == true ? 1 : 0;
            oRegistroEN.PreguntarFechaDeVencimientoAlFacturar = chkPreguntarPorLaFechaDeVencimientoAlFacturar.CheckState == CheckState.Checked ? 1 : 0;
            oRegistroEN.PreguntarNumeroDeSerieAlFacturar = chkPreguntarPorElNumeroDeSerieALFacturar.CheckState == CheckState.Checked ? 1: 0;
            oRegistroEN.PreguntarPorResetaAlFacturar = chkProductoControlado.CheckState == CheckState.Checked ? 1 : 0;
            oRegistroEN.NoUsarComisionesParaEsteProducto = rbNoUsarComisionesParaEsteProducto.Checked == true ? 1 : 0;
            oRegistroEN.MostrarImagenAlFacturar = chkMostarImagenAlFacturar.CheckState == CheckState.Checked ? 1: 0;
            oRegistroEN.UsarComisionesDefinidasEnElregistroDelVendedor = rbUsarLaComisionDefinidaEnElRegistroDelVendedor.Checked == true ? 1 : 0;
            decimal Comision;
            decimal.TryParse(txtComisionPorcentual.Text, out Comision);
            oRegistroEN.Comision = Comision;

            decimal ComisionMaxima;
            decimal.TryParse(txtComisionMaxima.Text, out ComisionMaxima);
            oRegistroEN.ComisionMaxima = ComisionMaxima;
            oRegistroEN.ActivarPromocion = chkAplicarPromocion.CheckState == CheckState.Checked ? 1 : 0;
            
            //partes generales.            
            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.idUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.idUsuarioModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;

        }

        private ProductoPromocionEN InformacionDeLaPromocionDelProducto()
        {
            ProductoPromocionEN oRegistroEN = new ProductoPromocionEN();

            oRegistroEN.idProductoPromocion = Convert.ToInt32(txtidProductoPromocion.Text);
            oRegistroEN.oProductoEN = InformacionDelRegistro();
            decimal PrecioDelProducto;
            decimal.TryParse(txtPrecioPromocional.Text, out PrecioDelProducto);
            oRegistroEN.PrecioDelProducto = PrecioDelProducto;
            oRegistroEN.FechaDeInicio = dtpkDesdePromocion.Value;
            oRegistroEN.FechaDeFinalizacion = dtpkHastaPromocional.Value;
            oRegistroEN.Estado = cmbEstadoDeLaPromocion.Text.Trim();
            oRegistroEN.Descripcion = txtDescripcionDeLaPromocion.Text.Trim();

            //partes generales.            
            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.idUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.idUsuarioModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;

        }

        #endregion

        #region "Eventos del Formulario"
        
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

        private void frmProductoOperacion_FormClosing(object sender, FormClosingEventArgs e)
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

                    ProductoEN oRegistroEN = InformacionDelRegistro();
                    ProductoLN oRegistroLN = new ProductoLN();

                    if (oRegistroLN.ValidarRegistroDuplicado(oRegistroEN, Program.oDatosDeConexion, "AGREGAR"))
                    {

                        MessageBox.Show(oRegistroLN.Error, "Guardar información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                    if (oRegistroLN.Agregar(oRegistroEN, Program.oDatosDeConexion))
                    {

                        txtIdentificador.Text = oRegistroEN.idProducto.ToString();
                        ValorLlavePrimariaEntidad = oRegistroEN.idProducto;

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

                    ProductoEN oRegistroEN = InformacionDelRegistro();
                    ProductoLN oRegistroLN = new ProductoLN();

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

                        txtIdentificador.Text = oRegistroEN.idProducto.ToString();
                        ValorLlavePrimariaEntidad = oRegistroEN.idProducto;

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

                    ProductoEN oRegistroEN = InformacionDelRegistro();
                    ProductoLN oRegistroLN = new ProductoLN();

                    if (oRegistroLN.ValidarSiElRegistroEstaVinculado(oRegistroEN, Program.oDatosDeConexion, "ELIMINAR"))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (oRegistroLN.Eliminar(oRegistroEN, Program.oDatosDeConexion))
                    {

                        txtIdentificador.Text = oRegistroEN.idProducto.ToString();
                        ValorLlavePrimariaEntidad = oRegistroEN.idProducto;

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

        private void tsbRecarRegistro_Click(object sender, EventArgs e)
        {
            LlenarCamposDesdeBaseDatosSegunID();
        }

        private void txtPrecioPromocional_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtMinimo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtExistencias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtMaximo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtIVA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtPorcentaje1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtPorcentaje2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtPorcentaje3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtPorcentaje4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtPorcentaje5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtPrecio1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtPrecio2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtPrecio3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtPrecio4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtPrecio5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtComisionPorcentual_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        private void txtComisionMaxima_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((TextBox)sender).Text.Contains('.'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == '.' || e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
            }
        }

        #endregion

        private void txtCodigoDeBarra_TextChanged(object sender, EventArgs e)
        {
            pbxCodigoGenerado.Image = CodigoDeBarras.GenerarCodigoDeBarra_345x50(txtCodigoDeBarra.Text.Trim());
        }

    }
}
