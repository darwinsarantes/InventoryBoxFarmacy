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
        private ImageList ListaDeImagenes = null;
        private int idLoteProducto;

        private bool CerrarVentana = false;

        private void frmProductoOperacion_Shown(object sender, EventArgs e)
        {
            LlenarComboPresentacion();
            LlenarComboUnidadDeMedida();
            LlenarComboCategoria();
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

        private void LlenarComboPresentacion()
        {
            try
            {

                ProductoPresentacionEN oRegistroEN = new ProductoPresentacionEN();
                ProductoPresentacionLN oRegistroLN = new ProductoPresentacionLN();

                oRegistroEN.Where = "";
                oRegistroEN.OrderBy = " Order by pps.Nombre asc ";

                if (oRegistroLN.ListadoParaCombos(oRegistroEN, Program.oDatosDeConexion))
                {
                    cmbPresentacion.DataSource = oRegistroLN.TraerDatos();
                    cmbPresentacion.DisplayMember = "Nombre";
                    cmbPresentacion.ValueMember = "idProductoPresentacion";

                    cmbPresentacion.SelectedIndex = -1;

                } else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Presentación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LlenarComboUnidadDeMedida()
        {
            try
            {

                ProductoUnidadDeMedidaEN oRegistroEN = new ProductoUnidadDeMedidaEN();
                ProductoUnidadDeMedidaLN oRegistroLN = new ProductoUnidadDeMedidaLN();

                oRegistroEN.Where = "";
                oRegistroEN.OrderBy = " Order by pum.Nombre asc ";

                if (oRegistroLN.ListadoParaCombos(oRegistroEN, Program.oDatosDeConexion))
                {
                    cmbUnidadDeMedida.DataSource = oRegistroLN.TraerDatos();
                    cmbUnidadDeMedida.DisplayMember = "Nombre";
                    cmbUnidadDeMedida.ValueMember = "idProductoUnidadDeMedida";

                    cmbUnidadDeMedida.SelectedIndex = -1;

                }
                else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unidad de Medida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LlenarComboCategoria()
        {
            try
            {

                CategoriaEN oRegistroEN = new CategoriaEN();
                CategoriaLN oRegistroLN = new CategoriaLN();

                oRegistroEN.Where = "";
                oRegistroEN.OrderBy = " Order by ub.Nombre asc ";

                if (oRegistroLN.ListadoParaCombos(oRegistroEN, Program.oDatosDeConexion))
                {
                    cmbCategoria.DataSource = oRegistroLN.TraerDatos();
                    cmbCategoria.DisplayMember = "Nombre";
                    cmbCategoria.ValueMember = "idCategoria";

                    cmbCategoria.SelectedIndex = -1;

                }
                else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Categoria", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    txtCodigoDeBarra.Text = Fila["CodigoDeBarra"].ToString();
                    txtCodigo.Text = Fila["Codigo"].ToString();
                    txtNombre.Text = Fila["ProductoNombre"].ToString();
                    txtNombreGenerico.Text = Fila["NombreGenerico"].ToString();
                    txtNombreComun.Text = Fila["NombreComun"].ToString();
                    txtDescripcion.Text = Fila["Descripcion"].ToString();
                    txtObservacion.Text = Fila["Observaciones"].ToString();
                    txtExistencias.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["Existencias"]));
                    txtMinimo.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["Minimo"]));
                    txtMaximo.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["Maximo"]));
                    cmbCategoria.SelectedValue = Convert.ToInt32(Fila["idCategoria"]);
                    cmbPresentacion.SelectedValue = Convert.ToInt32(Fila["idProductoPresentacion"]);
                    cmbUnidadDeMedida.SelectedValue = Convert.ToInt32(Fila["idProductoUnidadDeMedida"]);
                    txtAlmacenIdentidad.Text = Fila["idAlmacenEntidad"].ToString();
                    txtTablaDeReferencia.Text = Fila["TablaDeReferenciaDeAlmacenaje"].ToString();
                    txtTablaDeReferenciaPL.Text = Fila["TablaDeRefereciaDeProveedorOLaboratorio"].ToString();
                    txtIdProveedorLaboratorio.Text = Fila["idPLEntidad"].ToString();
                    txtIdPrecio.Text = Fila["idProductoPrecio"].ToString();
                    txtidProductoConfiguracion.Text = Fila["idProductoConfiguracion"].ToString();
                    txtUnidadesXPresentacion.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["UnidadesXPrecentacion"]));
                    txtPrecioXUnidad.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["PrecioXUnidad"]));
                    txtCosto.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["Costo"]));
                    txtPorcentaje1.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["PorcentajeDelPrecio1"]));
                    txtPorcentaje2.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["PorcentajeDelPrecio2"]));
                    txtPorcentaje3.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["PorcentajeDelPrecio3"]));
                    txtPorcentaje4.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["PorcentajeDelPrecio4"]));
                    txtPorcentaje5.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["PorcentajeDelPrecio5"]));
                    txtPrecio1.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["Precio1"]));
                    txtPrecio2.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["Precio2"]));
                    txtPrecio3.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["Precio3"]));
                    txtPrecio4.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["Precio4"]));
                    txtPrecio5.Text = string.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["Precio5"]));
                    chkAplicarIVA.Checked = Convert.ToBoolean(Convert.ToInt32(Fila["AplicarElIva"]));
                    txtIVA.Text = String.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["ValorDelIvaEnProcentaje"]));
                    chkPreguntarPorLaFechaDeVencimientoAlFacturar.Checked = Convert.ToBoolean(Convert.ToInt32(Fila["PreguntarFechaDeVencimientoAlFacturar"]));
                    chkAplicarIVA.Checked = Convert.ToBoolean(Convert.ToInt32(Fila["AplicarElIva"]));
                    rbMontoFijoPorVenta.Checked = Convert.ToBoolean(Convert.ToInt32(Fila["MontoFijoPorVenta"]));
                    rbUsarLaComisionDefinidaEnElRegistroDelVendedor.Checked = Convert.ToBoolean(Convert.ToInt32(Fila["UsarComisionesDefinidasEnElregistroDelVendedor"]));
                    rbPorcentajeDeLaGanancia.Checked = Convert.ToBoolean(Convert.ToInt32(Fila["PorcentajeDeLaGanacia"]));
                    rbPorcentajeDeLaVenta.Checked = Convert.ToBoolean(Convert.ToInt32(Fila["PorcentajeDeLaVenta"]));
                    chkMostarImagenAlFacturar.Checked = Convert.ToBoolean(Convert.ToInt32(Fila["MostrarImagenAlFacturar"]));
                    chkPreguntarPorElNumeroDeSerieALFacturar.Checked = Convert.ToBoolean(Convert.ToInt32(Fila["PreguntarNumeroDeSerieAlFacturar"]));
                    txtComisionMaxima.Text = String.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["ComisionMaxima"]));
                    txtComisionPorcentual.Text = String.Format("{0:###,###,##0.00}", Convert.ToDecimal(Fila["Comision"]));
                    txtMarcaDelProducto.Text = Fila["MarcaDelProducto"].ToString();
                    txtNumeroDeCerie.Text = Fila["NumeroDeSerie"].ToString();
                    txtModelo.Text = Fila["ModeloDelProducto"].ToString();

                    if (Fila["Foto"] != DBNull.Value)
                    {
                        pictureBox1.Image = Imagenes.ProcesarImagenToBitMap((object)(Fila["Foto"]));
                    }
                    else
                    {
                        pictureBox1.Image = InventoryBoxFarmacy.Properties.Resources.iconfinder_product_49608;
                    }

                    string Estado = Fila["Estado"].ToString().Trim();
                    GenerarCodigoDeBarra();

                    if (Estado.ToUpper() == "ACTIVO")
                    {
                        chkProductoDescontinuado.CheckState = CheckState.Unchecked;
                    }
                    else {
                        chkProductoDescontinuado.CheckState = CheckState.Checked;
                        groupBox1.Enabled = false;
                        groupBox2.Enabled = false;
                        groupBox3.Enabled = false;
                        groupBox5.Enabled = false;
                        groupBox6.Enabled = false;
                        gbLaboratorio.Enabled = false;
                        groupBox7.Enabled = false;
                        groupBox8.Enabled = false;                        

                        txtObservacion.ReadOnly = true;
                       
                    }
                    
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
            txtCodigoDeBarra.Text = string.Empty;
            txtAlmacenaje.Text = string.Empty;
            txtAlmacenIdentidad.Text = string.Empty;
            txtCodigo.Text = string.Empty;
            txtCodigoDeBarra.Text = string.Empty;
            pbxCodigoGenerado.Image = null;
            txtComisionMaxima.Text = "0.00";
            txtComisionPorcentual.Text = "0.00";
            txtCosto.Text = "0.00";
            txtDescripcion.Text = string.Empty;
            txtObservacion.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtNombreComun.Text = string.Empty;
            txtNombreGenerico.Text = string.Empty;
            txtNumeroDeCerie.Text = string.Empty;
            txtModelo.Text = string.Empty;
            txtMarcaDelProducto.Text = string.Empty;
            txtPorcentaje1.Text = "0.00";
            txtPorcentaje2.Text = "0.00";
            txtPorcentaje3.Text = "0.00";
            txtPorcentaje4.Text = "0.00";
            txtPorcentaje5.Text = "0.00";
            txtPrecio1.Text = "0.00";
            txtPrecio2.Text = "0.00";
            txtPrecio3.Text = "0.00";
            txtPrecio4.Text = "0.00";
            txtPrecio5.Text = "0.00";            
            txtPrecioXUnidad.Text = "";
            txtTablaDeReferencia.Text = string.Empty;
            txtTablaDeReferenciaPL.Text = string.Empty;
            txtExistencias.Text = string.Empty;
            txtIdentificador.Text = string.Empty;
            txtIdPrecio.Text = string.Empty;
            txtidProductoConfiguracion.Text = string.Empty;            
            txtIVA.Text = string.Empty;
            txtProveedorLaboratorio.Text = string.Empty;
            txtUnidadesXPresentacion.Text = "0.00";
            cmbCategoria.SelectedIndex = -1;            
            cmbPresentacion.SelectedIndex = -1;
            cmbUnidadDeMedida.SelectedIndex = -1;
            chkAplicarComision.CheckState = CheckState.Unchecked;
            chkAplicarIVA.CheckState = CheckState.Unchecked;            
            chkMostarImagenAlFacturar.CheckState = CheckState.Unchecked;
            chkMostrarObservacionesDelProducto.CheckState = CheckState.Unchecked;
            chkPreguntarPorElNumeroDeSerieALFacturar.CheckState = CheckState.Unchecked;
            chkPreguntarPorLaFechaDeVencimientoAlFacturar.CheckState = CheckState.Unchecked;
            chkProductoControlado.CheckState = CheckState.Unchecked;
            chkProductoDescontinuado.CheckState = CheckState.Unchecked;
            rbMontoFijoPorVenta.Checked = false;
            rbNoUsarComisionesParaEsteProducto.Checked = false;
            rbPorcentajeDeLaGanancia.Checked = false;
            rbPorcentajeDeLaVenta.Checked = false;
            rbUsarLaComisionDefinidaEnElRegistroDelVendedor.Checked = false;          

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

            if (Controles.IsNullOEmptyElControl(txtNombre))
            {
                EP.SetError(txtNombre, "Este campo no puede quedar vacío");
                txtNombre.Focus();
                return false;
            }

            decimal Minimo;
            decimal.TryParse(txtMinimo.Text, out Minimo);

            if (Minimo <= 0)
            {
                EP.SetError(txtMinimo, "El valor ingresado no es válido");
                txtMinimo.Focus();
                return false;
            }

            decimal Maximo;
            decimal.TryParse(txtMaximo.Text, out Maximo);

            if (Maximo <= 0)
            {
                EP.SetError(txtMaximo, "El valor ingresado no es válido");
                txtMaximo.Focus();
                return false;
            }

            if (Maximo < Minimo)
            {
                EP.SetError(txtMaximo, "El valor máximo no puede ser menor que el minimo");
                txtMaximo.Focus();
                return false;
            }

            if (Maximo == Minimo)
            {
                EP.SetError(txtMaximo, "El valor máximo no puede ser igual que el minimo");
                txtMaximo.Focus();
                return false;
            }

            int Unidad;
            int.TryParse(txtUnidadesXPresentacion.Text, out Unidad);

            if (Unidad <= 0)
            {
                EP.SetError(txtUnidadesXPresentacion, "El valor ingresado no es válido");
                txtUnidadesXPresentacion.Focus();
                return false;
            }

            if (Controles.IsNullOEmptyElControl(cmbCategoria))
            {
                EP.SetError(cmbCategoria, "Se debe de agregar el producto a algunas de nuestras categorias");
                cmbCategoria.Focus();
                return false;
            }

            if (Controles.IsNullOEmptyElControl(cmbUnidadDeMedida))
            {
                EP.SetError(cmbUnidadDeMedida, "Se debe de agregar el producto a alguna unidad de medida de nuestra lista");
                cmbUnidadDeMedida.Focus();
                return false;
            }

            if (Controles.IsNullOEmptyElControl(cmbPresentacion))
            {
                EP.SetError(cmbPresentacion, "Se debe agregar un tipo de presentación para el producto");
                cmbPresentacion.Focus();
                return false;
            }

            if (Controles.IsNullOEmptyElControl(txtAlmacenIdentidad))
            {
                EP.SetError(btnBuscarAlmacen, "Devemos especificar el lugar de almacenaje");
                btnBuscarAlmacen.Focus();
                return false;
            }

            if (Controles.IsNullOEmptyElControl(txtIdProveedorLaboratorio))
            {
                EP.SetError(btnBuscarLaboratorio, "Devemos especificar el laboratorio al que pertenece el producto");
                btnBuscarLaboratorio.Focus();
                return false;
            }

            if (Controles.IsNullOEmptyElControl(chkAplicarComision) == false)
            {

                decimal comisionporcentual;
                decimal.TryParse(txtComisionPorcentual.Text, out comisionporcentual);

                if (comisionporcentual == 0)
                {
                    EP.SetError(txtComisionPorcentual, "Este valor no puede ser cero o menor que cero");
                    txtComisionPorcentual.Focus();
                    return false;
                }

                decimal ComisionMaxima;
                decimal.TryParse(txtComisionMaxima.Text, out ComisionMaxima);

                if (ComisionMaxima == 0)
                {
                    EP.SetError(txtComisionMaxima, "Este valor no puede ser cero o menor que cero");
                    txtComisionMaxima.Focus();
                    return false;
                }

            }
            
            if (dtpkFechaDeVencimiento.Checked == true)
            {
                DateTime FechaActual = new DateTime();
                FechaActual = System.DateTime.Now;
                if (dtpkFechaDeVencimiento.Value < FechaActual)
                {
                    EP.SetError(dtpkFechaDeVencimiento, "La fecha de vencimiento del item es menor que la que tiene el sistema actualmente");
                    dtpkFechaDeVencimiento.Focus();
                    return false;
                } else if (dtpkFechaDeVencimiento.Value == FechaActual)
                {
                    if (MessageBox.Show("La fecha del vencimiento del item es igual a la del Sistema \n Desea continuar guardando la información", "Validar fecha de vencimiento", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        return true;
                    }else
                    {
                        return false;
                    }

                }

            }

            return true;

        }

        private ProductoEN InformacionDelRegistro() {

            ProductoEN oRegistroEN = new ProductoEN();

            oRegistroEN.idProducto = Convert.ToInt32((txtIdentificador.Text.Length > 0 ? txtIdentificador.Text : "0"));
            oRegistroEN.Nombre = txtNombre.Text.Trim();
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
            oRegistroEN.idPLEntidad = Convert.ToInt32(txtIdProveedorLaboratorio.Text);
            oRegistroEN.TablaDeRefereciaDeProveedorOLaboratorio = txtTablaDeReferenciaPL.Text.Trim();
            oRegistroEN.TablaDeReferenciaDeAlmacenaje = txtTablaDeReferencia.Text.Trim();
            oRegistroEN.Observaciones = txtObservacion.Text.Trim();
            oRegistroEN.ProductoControlado = chkProductoControlado.CheckState == CheckState.Checked ? 1 : 0;
                        
            if(chkProductoDescontinuado.CheckState == CheckState.Checked)
            {
                oRegistroEN.Estado = "INACTIVO";
            }else
            {
                oRegistroEN.Estado = "ACTIVO";
            }

            if(pictureBox1.Image == null)
            {
                oRegistroEN.AFoto = Imagenes.ProcesarImagenToByte((Bitmap)(InventoryBoxFarmacy.Properties.Resources.iconfinder_product_49608));
            }
            else
            {
                oRegistroEN.AFoto = Imagenes.ProcesarImagenToByte((Bitmap)(pictureBox1.Image));
            }

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

            int idProductoPrecio;
            int.TryParse(txtIdPrecio.Text, out idProductoPrecio);
            oRegistroEN.idProductoPrecio = idProductoPrecio;
            decimal Costo;
            decimal.TryParse(txtCosto.Text, out Costo);
            oRegistroEN.Costo = Costo;
            decimal Porcentaje1;
            decimal.TryParse(txtPorcentaje1.Text, out Porcentaje1);
            oRegistroEN.PorcentajeDelPrecio1 = Porcentaje1;
            decimal Porcentaje2;
            decimal.TryParse(txtPorcentaje2.Text, out Porcentaje2);
            oRegistroEN.PorcentajeDelPrecio2 = Porcentaje2;
            decimal Porcentaje3;
            decimal.TryParse(txtPorcentaje3.Text, out Porcentaje3);
            oRegistroEN.PorcentajeDelPrecio3 = Porcentaje3;
            decimal Porcentaje4;
            decimal.TryParse(txtPorcentaje4.Text, out Porcentaje4);
            oRegistroEN.PorcentajeDelPrecio4 = Porcentaje4;
            decimal Porcentaje5;
            decimal.TryParse(txtPorcentaje5.Text, out Porcentaje5);
            oRegistroEN.PorcentajeDelPrecio5 = Porcentaje5;
            decimal Precio1;
            decimal.TryParse(txtPrecio1.Text, out Precio1);
            oRegistroEN.Precio1 = Precio1;
            decimal Precio2;
            decimal.TryParse(txtPrecio2.Text, out Precio2);
            oRegistroEN.Precio2 = Precio2;
            decimal Precio3;
            decimal.TryParse(txtPrecio3.Text, out Precio3);
            oRegistroEN.Precio3 = Precio3;
            decimal Precio4;
            decimal.TryParse(txtPrecio4.Text, out Precio4);
            oRegistroEN.Precio4 = Precio4;
            decimal Precio5;
            decimal.TryParse(txtPrecio5.Text, out Precio5);
            oRegistroEN.Precio5 = Precio5;

            decimal UnidadesXPrecentacion;
            decimal.TryParse(txtUnidadesXPresentacion.Text, out UnidadesXPrecentacion);
            oRegistroEN.UnidadesXPrecentacion = UnidadesXPrecentacion;

            decimal PrecioXUnidad;
            decimal.TryParse(txtPrecioXUnidad.Text, out PrecioXUnidad);
            oRegistroEN.PrecioXUnidad = PrecioXUnidad;

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
            int idProductoConfiguracion;
            int.TryParse(txtidProductoConfiguracion.Text, out idProductoConfiguracion);
            oRegistroEN.idProductoConfiguracion = idProductoConfiguracion;
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
            oRegistroEN.MarcaDelProducto = txtMarcaDelProducto.Text.Trim();
            oRegistroEN.ModeloDelProducto = txtModelo.Text.Trim();
            oRegistroEN.NumeroDeSerie = txtNumeroDeCerie.Text.Trim();
            
            //partes generales.            
            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.idUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.idUsuarioModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;

        }
                
        private ProductoLoteEN InformacionDelLote()
        {
            ProductoLoteEN oRegistroEN = new ProductoLoteEN();
            oRegistroEN.idLoteDelProducto = idLoteProducto;
            oRegistroEN.oProductoEN = InformacionDelRegistro();
            oRegistroEN.CantidadDelLote = Convert.ToDecimal(txtExistencias.Text);
            oRegistroEN.FechaDeVencimiento = dtpkFechaDeVencimiento.Value;

            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.IdUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.IdUsuarioDeModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            if (dtpkFechaDeVencimiento.Checked == true)
                oRegistroEN.AplicarCambio = true;
            else
                oRegistroEN.AplicarCambio = false;

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
                    ProductoCompletoEN oProductoCompletoEN = new ProductoCompletoEN();
                    oProductoCompletoEN.oProductoEN = InformacionDelRegistro();
                    oProductoCompletoEN.oPrecioEN = InformacionDelPrecioDelProducto();                    
                    oProductoCompletoEN.oConfiguracionEN = InformacionDeLaConfiguracionDelProducto();
                    oProductoCompletoEN.oProductoLote = InformacionDelLote();

                    ProductoLN oRegistroLN = new ProductoLN();

                    if (oRegistroLN.ValidarRegistroDuplicado(oProductoCompletoEN.oProductoEN, Program.oDatosDeConexion, "AGREGAR"))
                    {

                        MessageBox.Show(oRegistroLN.Error, "Guardar información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                    if (oRegistroLN.AgregarUtilizandoLaMismaConexion(oProductoCompletoEN, Program.oDatosDeConexion))
                    {

                        txtIdentificador.Text = oProductoCompletoEN.oProductoEN.idProducto.ToString();
                        ValorLlavePrimariaEntidad = oProductoCompletoEN.oProductoEN.idProducto;
                        txtIdPrecio.Text = oProductoCompletoEN.oPrecioEN.idProductoPrecio.ToString();
                        txtidProductoConfiguracion.Text = oProductoCompletoEN.oConfiguracionEN.idProductoConfiguracion.ToString();
                                                
                        if(oProductoCompletoEN.oProductoLote.AplicarCambio == true)
                        {
                            idLoteProducto = oProductoCompletoEN.oProductoLote.idLoteDelProducto;
                        }

                        EvaluarErrorParaMensajeAPantalla(oRegistroLN.Error, "Guardar");

                        oProductoCompletoEN = null;
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

                    ProductoCompletoEN oRegistroEN = new ProductoCompletoEN();
                    oRegistroEN.oProductoEN = InformacionDelRegistro();
                    oRegistroEN.oPrecioEN = InformacionDelPrecioDelProducto();
                    oRegistroEN.oConfiguracionEN = InformacionDeLaConfiguracionDelProducto();
                    oRegistroEN.oProductoLote = InformacionDelLote();

                    ProductoLN oRegistroLN = new ProductoLN();

                    if (oRegistroLN.ValidarSiElRegistroEstaVinculado(oRegistroEN.oProductoEN, Program.oDatosDeConexion, "ACTUALIZAR"))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(oRegistroLN.Error, this.OperacionARealizar, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (oRegistroLN.ValidarRegistroDuplicado(oRegistroEN.oProductoEN, Program.oDatosDeConexion, "ACTUALIZAR"))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show(oRegistroLN.Error, "Actualizar la información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }

                    if (oRegistroLN.Actualizar(oRegistroEN.oProductoEN, Program.oDatosDeConexion))
                    {

                        txtIdentificador.Text = oRegistroEN.oProductoEN.idProducto.ToString();
                        ValorLlavePrimariaEntidad = oRegistroEN.oProductoEN.idProducto;

                        txtIdPrecio.Text = oRegistroEN.oPrecioEN.idProductoPrecio.ToString();
                        txtidProductoConfiguracion.Text = oRegistroEN.oConfiguracionEN.idProductoConfiguracion.ToString();

                        if (oRegistroEN.oProductoLote.AplicarCambio == true)
                        {
                            idLoteProducto = oRegistroEN.oProductoLote.idLoteDelProducto;
                        }

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

        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void GenerarCodigoDeBarra()
        {
            try
            {
                if (Funciones.Numeros.IsNumeric(txtCodigoDeBarra.Text.Trim()))
                {

                    if (txtCodigoDeBarra.Text.Trim().Length > 11 && txtCodigoDeBarra.Text.Trim().Length <= 12)
                    {
                        pbxCodigoGenerado.Image = CodigoDeBarras.GenerarCodigoDeBarra_345x50(txtCodigoDeBarra.Text.Trim(), "UPC-A");
                    }
                    else if (txtCodigoDeBarra.Text.Trim().Length == 13)
                    {
                        pbxCodigoGenerado.Image = CodigoDeBarras.GenerarCodigoDeBarra_345x50(txtCodigoDeBarra.Text.Trim(), "EAN-13");
                    }
                    else
                    {
                        pbxCodigoGenerado.Image = CodigoDeBarras.GenerarCodigoDeBarra_345x50(txtCodigoDeBarra.Text.Trim(), "Code 128");
                    }

                }
                else
                {
                    pbxCodigoGenerado.Image = CodigoDeBarras.GenerarCodigoDeBarra_345x50(txtCodigoDeBarra.Text.Trim(), "Code 128");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Generar codigo de barra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion
        
        private void txtUnidadesXPresentacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar)) //Al pulsar teclas como Borrar y eso.
            {
                e.Handled = false; //Se acepta (todo OK)
            }
            else //Para todo lo demas
            {
                e.Handled = true; //No se acepta (si pulsas cualquier otra cosa pues no se envia)
            }
        }
        
        private void btnBuscarAlmacen_Click(object sender, EventArgs e)
        {
            try
            {
                frmLocalizacionDelProducto oFrmRegistro = new frmLocalizacionDelProducto();
                oFrmRegistro.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                oFrmRegistro.VariosRegistros = false;
                oFrmRegistro.ActivarFiltros = true;
                oFrmRegistro.TituloVentana = string.Format("Localización del producto");

                //oFrmRegistro.AplicarFiltroDeWhereExterno = true;
                //oFrmRegistro.WhereExterno = WhereDinamicoDelSustito();

                oFrmRegistro.ShowDialog();

                LocalizacionDelProductoEN[] oRegistroEN = new LocalizacionDelProductoEN[0];
                oRegistroEN = oFrmRegistro.oLocalizacionDelProductoEN;

                if (oRegistroEN.Length > 0)
                {
                    LocalizacionDelProductoEN oRegistro = oRegistroEN[0];
                    txtAlmacenaje.Text = oRegistro.Codigo;
                    txtTablaDeReferencia.Text = oRegistro.TablaDeReferencia;
                    txtAlmacenIdentidad.Text = oRegistro.idLocalizacionDelProducto.ToString();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Buscar registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCodigoDeBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    GenerarCodigoDeBarra();
                    TextBox oText = (TextBox)sender;
                    oText.SelectAll();
                }else
                {
                    GenerarCodigoDeBarra();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Generar codigo de barra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscarLaboratorio_Click(object sender, EventArgs e)
        {
            try
            {
                frmlProveedorLaboratorio oFrmRegistro = new frmlProveedorLaboratorio();
                oFrmRegistro.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                oFrmRegistro.VariosRegistros = false;
                oFrmRegistro.ActivarFiltros = true;
                oFrmRegistro.TituloVentana = string.Format("Localización del producto");

                //oFrmRegistro.AplicarFiltroDeWhereExterno = true;
                //oFrmRegistro.WhereExterno = WhereDinamicoDelSustito();

                oFrmRegistro.ShowDialog();

                ProveedorLaboratorioEN[] oRegistroEN = new ProveedorLaboratorioEN[0];
                oRegistroEN = oFrmRegistro.oProveedorLaboratorioEN;

                if (oRegistroEN.Length > 0)
                {
                    ProveedorLaboratorioEN oRegistro = oRegistroEN[0];
                    txtTablaDeReferenciaPL.Text = oRegistro.TablaDeReferencia;
                    txtIdProveedorLaboratorio.Text = oRegistro.idProveedorLaboratorio.ToString();
                    txtProveedorLaboratorio.Text = oRegistro.ProveedorLaboratorio;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Buscar registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtPorcentaje1_KeyUp(object sender, KeyEventArgs e)
        {
            decimal Costo, Porcentaje, valortotal= 0;
            if (Controles.IsNullOEmptyElControl(txtCosto)) Costo = 0; else decimal.TryParse(txtCosto.Text, out Costo);
            if (Controles.IsNullOEmptyElControl(txtPorcentaje1)) Porcentaje = 0; else decimal.TryParse(txtPorcentaje1.Text, out Porcentaje);
             
            if(Costo > 0 && Porcentaje > 0)
            {
                valortotal = Costo + ((Costo * Porcentaje) / 100);
            }

            txtPrecio1.Text = string.Format("{0:###,###,##0.00}", valortotal.ToString());

            CalcularPRecioUnidad();
        }

        private void txtPrecio1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                decimal Costo, Precio, Porcentaje = 0, Valor = 0;
                if (Controles.IsNullOEmptyElControl(txtCosto)) Costo = 0; else decimal.TryParse(txtCosto.Text, out Costo);
                if (Controles.IsNullOEmptyElControl(txtPrecio1)) Precio = 0; else decimal.TryParse(txtPrecio1.Text, out Precio);
                if (Controles.IsNullOEmptyElControl(txtPorcentaje1)) Porcentaje = 0; else decimal.TryParse(txtPorcentaje1.Text, out Porcentaje);

                if (Costo > 0 && Precio > 0 )
                {
                    Porcentaje = (Precio / Costo);
                    Valor = (Porcentaje - 1) * 100;
                    txtPorcentaje1.Text = string.Format("{0:###,###,##0.00}", Valor);
                }
                else if (Costo > 0 && Precio == 0 && Porcentaje > 0)
                {
                    Precio = Costo + ((Costo * Porcentaje) / 100);
                    txtPrecio1.Text = string.Format("{0:###,###,##0.00}", Precio);
                }

                CalcularPRecioUnidad();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Calculo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCosto_KeyUp(object sender, KeyEventArgs e)
        {

            decimal Costo = 0, Precio = 0, Porcentaje = 0, Valor = 0;

            if (Controles.IsNullOEmptyElControl(txtCosto)) Costo = 0; else decimal.TryParse(txtCosto.Text, out Costo);
            if (Controles.IsNullOEmptyElControl(txtPorcentaje1)) Porcentaje = 0; else decimal.TryParse(txtPorcentaje1.Text, out Porcentaje);
            if (Controles.IsNullOEmptyElControl(txtPrecio1)) Precio = 0; else decimal.TryParse(txtPrecio1.Text, out Precio);

            if (Costo > 0 && Precio > 0 )
            {
                Porcentaje = (Precio / Costo);
                Valor = (Porcentaje - 1) / 100;
                txtPorcentaje1.Text = string.Format("{0:###,###,##0.00}", Valor);
            }else if (Costo > 0 && Precio == 0 && Porcentaje > 0)
            {
                Precio = Costo + ((Costo * Porcentaje) / 100);
                txtPrecio1.Text = string.Format("{0:###,###,##0.00}", Precio);
            }else if (Costo == 0 && Precio > 0 && Porcentaje > 0)
            {
                Costo = Precio / (1 + (1 / Porcentaje));
                txtCosto.Text = string.Format("{0:###,###,##0.00}", Costo);
            }else if (Costo > 0 && Precio > 0 && Porcentaje > 0)
            {
                Precio = Costo + ((Costo * Porcentaje) / 100);
                txtPrecio1.Text = string.Format("{0:###,###,##0.00}", Precio);
            }

            CalcularPRecioUnidad();

        }

        private void txtUnidadesXPresentacion_KeyUp(object sender, KeyEventArgs e)
        {
            CalcularPRecioUnidad();
        }

        private void CalcularPRecioUnidad()
        {
            
            decimal Unidades = 0;
            decimal Precio = 0;

            decimal.TryParse(txtPrecio1.Text, out Precio);
            decimal.TryParse(txtUnidadesXPresentacion.Text, out Unidades);

            if (Unidades > 0 && Precio > 0)
            {
                decimal valor = Precio / Unidades;
                txtPrecioXUnidad.Text = string.Format("{0:###,###,##0.00}", valor);
            }else
            {
                decimal valor = 0;
                txtPrecioXUnidad.Text = string.Format("{0:###,###,##0.00}", valor);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
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
                        this.pictureBox1.Image = (Image)ImagenLogo;
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
