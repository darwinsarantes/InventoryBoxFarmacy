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

                if(oRegistroLN.ListadoParaCombos(oRegistroEN, Program.oDatosDeConexion))
                {
                    cmbPresentacion.DataSource = oRegistroLN.TraerDatos();
                    cmbPresentacion.DisplayMember = "Nombre";
                    cmbPresentacion.ValueMember = "idProductoPresentacion";

                    cmbPresentacion.SelectedIndex = -1;
                    
                }else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }
                
            }catch(Exception ex)
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
            CrearColnmasEnElListview();
            AgregarImagenPorDefecto();
            AgregarImagenDelproductoPorDefecto();

            CrearColumnasDGVRegistros();
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
            txtPrecioPromocional.Text = "0.00";
            txtPrecioXUnidad.Text = "";
            txtTablaDeReferencia.Text = string.Empty;
            txtTablaDeReferenciaPL.Text = string.Empty;
            txtExistencias.Text = string.Empty;
            txtIdentificador.Text = string.Empty;
            txtIdPrecio.Text = string.Empty;
            txtidProductoConfiguracion.Text = string.Empty;
            txtidProductoPromocion.Text = string.Empty;
            txtIVA.Text = string.Empty;
            txtProveedorLaboratorio.Text = string.Empty;
            txtUnidadesXPresentacion.Text = "0.00";
            cmbCategoria.SelectedIndex = -1;
            cmbEstadoDeLaPromocion.SelectedIndex = -1;
            cmbPresentacion.SelectedIndex = -1;
            cmbUnidadDeMedida.SelectedIndex = -1;
            chkAplicarComision.CheckState = CheckState.Unchecked;
            chkAplicarIVA.CheckState = CheckState.Unchecked;
            chkAplicarPromocion.CheckState = CheckState.Unchecked;
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
            lvImagenes.Items.Clear();            
            dgvListar.Rows.Clear();

        }

        #region "Trabajando con el Listview"

        private void CrearColnmasEnElListview()
        {
            try
            {
                ColumnHeader c = new ColumnHeader();
                c.Name = "idProductoImagenes";
                c.TextAlign = HorizontalAlignment.Left;
                c.Width = 0;
                //idProductoImagenes,PorDefecto, Nombre, extension, Ruta, Size, Foto                
                lvImagenes.Columns.Add("PorDefecto", 80, HorizontalAlignment.Left).Text = "Por Defecto";
                lvImagenes.Columns.Add("Nombre", 250, HorizontalAlignment.Left).Text = "Nombre de la Imagen";
                lvImagenes.Columns.Add("Extension", 80, HorizontalAlignment.Left).Text = "Extensión";
                lvImagenes.Columns.Add(c);
                lvImagenes.CheckBoxes = true;
                lvImagenes.View = View.SmallIcon;
                lvImagenes.FullRowSelect = true;
                lvImagenes.LabelEdit = false;
                lvImagenes.AllowColumnReorder = true;
                lvImagenes.GridLines = true;
                lvImagenes.MultiSelect = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crear colunmas en el listview", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AgregarImagenPorDefecto()
        {
            try
            {
                if(ListaDeImagenes == null)
                {
                    ListaDeImagenes = new ImageList();
                    ListaDeImagenes.ImageSize = new Size(250, 250);
                    ListaDeImagenes.Images.Add(InventoryBoxFarmacy.Properties.Resources.iconfinder_product_49608);
                }else
                {
                    ListaDeImagenes.Images.Add(InventoryBoxFarmacy.Properties.Resources.iconfinder_product_49608);
                }


            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Agregar imagenen por defecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AgregarImagenDelproductoPorDefecto()
        {
            try
            {
                lvImagenes.LargeImageList = ListaDeImagenes;
                lvImagenes.SmallImageList = ListaDeImagenes;

                ListViewItem item = new ListViewItem(" ");                
                item.SubItems.Add("Foto por Defecto");
                item.SubItems.Add(InventoryBoxFarmacy.Properties.Resources.iconfinder_product_49608.GetType().ToString());
                item.SubItems.Add("0");
                item.ImageIndex = 0;
                lvImagenes.Items.Add(item);
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Agregar imagen del producto por defecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        
        private void tsbAgregarImagen_Click(object sender, EventArgs e)
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
                        //Pendiente de agregar...
                        if (ListaDeImagenes == null)
                        {
                            ListaDeImagenes = new ImageList();
                            ListaDeImagenes.ImageSize = new Size(250, 250);
                            ListaDeImagenes.Images.Add(resizeImage(ImagenLogo, 250, 250));

                            lvImagenes.LargeImageList = ListaDeImagenes;
                            lvImagenes.SmallImageList = ListaDeImagenes;

                        }
                        else
                        {
                            ListaDeImagenes.Images.Add(ImagenLogo);
                        }

                        ListViewItem item = new ListViewItem(" ");
                        item.SubItems.Add(Abrir.FileName);
                        item.SubItems.Add(System.IO.Path.GetExtension(Abrir.FileName));
                        item.SubItems.Add("0");
                        item.ImageIndex = ListaDeImagenes.Images.Count - 1;
                        lvImagenes.Items.Add(item);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al intentar abrir el archivo: " + ex.Message, "cmdBuscar_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public Image resizeImage(Image img, int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage((Image)b);

            g.DrawImage(img, 0, 0, width, height);
            g.Dispose();

            return (Image)b;
        }

        private void lvImagenes_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvImagenes.SelectedItems.Count > 0)
                {
                    ListViewItem Item = lvImagenes.SelectedItems[0];

                    if (Item.Checked == true)
                    {

                        foreach (ListViewItem Fila in lvImagenes.Items)
                        {
                            if (Fila.Index != Item.Index)
                            {
                                Fila.Checked = false;
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Evento clic del listview", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tsbEliminarEliminar_Click(object sender, EventArgs e)
        {
            if (lvImagenes.SelectedItems.Count > 0)
            {
                ListViewItem item = lvImagenes.SelectedItems[0];

                if (MessageBox.Show("Desea quitar la imagen seleccionada de la lista", "Quitar imagen de la Lista", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    int idProductoImagenes = Convert.ToInt32(item.SubItems[3].Text);
                    if (idProductoImagenes == 0)
                    {
                        item.Remove();
                    }
                    else
                    {
                        ProductoImagenesEN oRegistroEN = new ProductoImagenesEN();
                        ProductoImagenesLN oRegistroLN = new ProductoImagenesLN();

                        oRegistroEN.idProductoImagenes = idProductoImagenes;
                        oRegistroEN.Nombre = item.SubItems[1].Text;
                        oRegistroEN.PorDefecto = item.Checked == true ? 1 : 0;
                        oRegistroEN.Ruta = item.SubItems[1].Text;
                        oRegistroEN.extension = "bitmap";
                        oRegistroEN.AFoto = Imagenes.ProcesarImagenToByte((Bitmap)(ListaDeImagenes.Images[item.ImageIndex]));

                        oRegistroEN.oLoginEN = Program.oLoginEN;
                        oRegistroEN.idUsuarioDeCreacion = Program.oLoginEN.idUsuario;
                        oRegistroEN.idUsuarioModificacion = Program.oLoginEN.idUsuario;
                        oRegistroEN.FechaDeCreacion = System.DateTime.Now;
                        oRegistroEN.FechaDeModificacion = System.DateTime.Now;

                        if (oRegistroLN.Eliminar(oRegistroEN, Program.oDatosDeConexion))
                        {
                            item.Remove();
                        }
                    }
                }

            }
        }

        #endregion

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

            if(Minimo <= 0)
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

            if(Maximo < Minimo)
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

            if(Unidad <= 0)
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

                if(comisionporcentual == 0)
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

            if(Controles.IsNullOEmptyElControl(chkAplicarPromocion) == false)
            {

                decimal PrecioDePromocion;
                decimal.TryParse(txtPrecioPromocional.Text, out PrecioDePromocion);
                if(PrecioDePromocion == 0)
                {
                    EP.SetError(txtPrecioPromocional, "Este valor no puede ser cero o menor que cero");
                    txtPrecioPromocional.Focus();
                    return false;
                }


                DateTime FechaActual = System.DateTime.Now;
                
                if( dtpkDesdePromocion.Value < FechaActual)
                {
                    EP.SetError(dtpkDesdePromocion, "La fecha Ingresada de inicio de la promoción no puede ser menor que la fecha actual del SO");
                    dtpkDesdePromocion.Focus();
                    return false;
                }

                if(dtpkHastaPromocional.Value < dtpkDesdePromocion.Value)
                {
                    EP.SetError(dtpkHastaPromocional, string.Format( "La fecha Ingresada de finalización de la promoción no puede ser {0} menor que la fecha de inicio de la promocion", Environment.NewLine));
                    dtpkHastaPromocional.Focus();
                    return false;
                }

                if (Controles.IsNullOEmptyElControl(txtDescripcionDeLaPromocion))
                {
                    EP.SetError(txtDescripcionDeLaPromocion,"Agrege una descripcíon de la promoción que se le va aplicar al producto");
                    txtDescripcionDeLaPromocion.Focus();
                    return false;
                }

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
            oRegistroEN.idPLEntidad = Convert.ToInt32(txtIdProveedorLaboratorio.Text);
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
                    ProductoCompletoEN oProductoCompletoEN = new ProductoCompletoEN();
                    oProductoCompletoEN.oProductoEN = InformacionDelRegistro();
                    oProductoCompletoEN.oPrecioEN = InformacionDelPrecioDelProducto();
                    oProductoCompletoEN.oPromocionEN = InformacionDeLaPromocionDelProducto();
                    oProductoCompletoEN.oConfiguracionEN = InformacionDeLaConfiguracionDelProducto();

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

        #region "Trabajando con el datagridview"

        private void CrearColumnasDGVRegistros()
        {
            try
            {

                string columnas = @"idProductoSustitutos,idSustituto, Codigo, CodigoDeBarra, Nombre, NombreGenerico";

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
                MessageBox.Show("Error al llenar la lista. \n" + ex.Message, "Poblar columnas dgvListar con los productos sustitutos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OcultarColumnasEnELDGV(String ColumnasDelDGV)
        {
            if (dgvListar.Columns.Count > 0)
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

                string OcultarColumnas = "idProductoSustitutos,idSustituto,Actualizar";
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
                                oFormato = new FormatoDGV(c1.Name.Trim(), "ProductoSustitutos");
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
                                              let idProductoSustitutos = Convert.ToInt32(item.Cells["idProductoSustitutos"].Value)
                                              where Actualizar.Equals(true) && idProductoSustitutos == 0
                                              select item).ToList<DataGridViewRow>();
                if (rows.Count > 0)
                {
                    Mensaje += string.Format(" Se va a agregar: {1} Registros {0}", Environment.NewLine, rows.Count);
                }

                List<DataGridViewRow> rows1 = (from item in dgv.Rows.Cast<DataGridViewRow>()
                                               let Actualizar = Convert.ToBoolean(item.Cells["Actualizar"].Value ?? false)
                                               let idProductoSustitutos = Convert.ToInt32(item.Cells["idProductoSustitutos"].Value)
                                               where Actualizar.Equals(true) && idProductoSustitutos > 0
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
                int idProductoSustitutos;
                int.TryParse(dgvListar.Rows[e.RowIndex].Cells["idProductoSustitutos"].Value.ToString(), out idProductoSustitutos);

                if (dgvListar.Rows[e.RowIndex].Cells["idProductoSustitutos"].Value == null)
                    return;

                if (idProductoSustitutos > 0 && dgvListar.Columns[e.ColumnIndex].Name != "Eliminar")
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
        
        private DataTable InformacionDeLosContendoresPorSeccion()
        {
            DataTable ODatos = null;
            try
            {

                ProductoSustitutosEN oRegistroEN = new ProductoSustitutosEN();
                ProductoSustitutosLN oRegistroLN = new ProductoSustitutosLN();

                oRegistroEN.oProductoEN.idProducto = ValorLlavePrimariaEntidad;
                oRegistroEN.OrderBy = " Order by ps.Nombre asc ";

                if (oRegistroLN.ListadoDeProductosXIdProducto(oRegistroEN, Program.oDatosDeConexion))
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
                MessageBox.Show(ex.Message, "Información de los productos asociados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return ODatos;
            }

        }

        private void CrearyYPoblarColumnasDGVLaboratorio()
        {
            try
            {

                CrearColumnasDGVRegistros();

                DataTable DTOrden = InformacionDeLosContendoresPorSeccion();

                if (DTOrden != null)
                {

                    if (DTOrden.Rows.Count > 0)
                    {
                        int i = 1;
                        Boolean valor = false;
                        if (OperacionARealizar == "Eliminar") { valor = true; } else { valor = false; }

                        int idProductoSustitutos = 0;
                        int idSustituto = 0;

                        foreach (DataRow row in DTOrden.Rows)
                        {

                            if (OperacionARealizar.ToUpper() == "NUEVO A PARTIR DE REGISTRO SELECCIONADO".ToUpper())
                            {
                                idProductoSustitutos = 0;
                                idSustituto = Convert.ToInt32(row["idSustituto"]);
                            }
                            else
                            {
                                idSustituto = Convert.ToInt32(row["idSustituto"]);
                                idProductoSustitutos = Convert.ToInt32(row["idProductoSustitutos"]);
                            }

                            dgvListar.Rows.Add(
                                valor,
                                idProductoSustitutos,
                                idSustituto,
                                row["Codigo"],
                                row["CodigoDeBarra"],
                                row["Producto"],
                                row["NombreGenerico"],
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
                dgvListar.CurrentCell = Fila.Cells["idProductoSustitutos"];
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
                    string NombreLavePrimariaDetalle = "idProductoSustitutos";

                    ProductoEN oProductoEN = InformacionDelRegistro();

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

                        ProductoSustitutosEN oRegistroEN = InformacionDelProductoSustituto(Fila);
                        ProductoSustitutosLN oRegistroLN = new ProductoSustitutosLN();

                        oRegistroEN.oProductoEN = oProductoEN;

                        //DETERMINAMOS LA OPERACION A REALIZAR
                        string Operacion = "";
                        int idSustituto;
                        int.TryParse(Fila.Cells["idSustituto"].Value.ToString(), out idSustituto);

                        //El orden es importante porque si un usuario agrego una nueva persona pero lo marco para eliminar, no hacemos nada, solo lo quitamos de la lista.
                        if (ValorDelaLLavePrimaria == 0 && Eliminar == true) { Operacion = "ELIMINAR FILA EN GRILLA"; }                        
                        //VALIDAREMOS QUE LA LLAVE PRIMARIA SEA CERO Y EL CONTARO SEA MAYOR A CERO PARA UN NUEVO VINCULO ENTRE PROVEEDOR Y CONTACTO
                        else if (ValorDelaLLavePrimaria == 0 && idSustituto > 0) { Operacion = "AGREGAR"; }
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
                        
                        //OPERACIONES
                        if (Operacion == "AGREGAR")
                        {
                            if (oRegistroLN.Agregar(oRegistroEN, Program.oDatosDeConexion))
                            {
                                Fila.Cells[NombreLavePrimariaDetalle].Value = oRegistroEN.idProductoSustitutos;
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
        
        private ProductoSustitutosEN InformacionDelProductoSustituto(DataGridViewRow Fila)
        {
            ProductoSustitutosEN oRegistroEN = new ProductoSustitutosEN();

            int idProducto;
            int.TryParse(Fila.Cells[""].Value.ToString(), out idProducto);

            oRegistroEN.oSutitutoEN.idProducto = idProducto;
            oRegistroEN.oSutitutoEN.Codigo = Fila.Cells["Codigo"].Value.ToString();
            oRegistroEN.oSutitutoEN.CodigoDeBarra = Fila.Cells["CodigoDeBarra"].Value.ToString();
            oRegistroEN.oSutitutoEN.Nombre = Fila.Cells["Nombre"].Value.ToString();
            oRegistroEN.oSutitutoEN.NombreGenerico = Fila.Cells["NombreGenerico"].Value.ToString();

            int idProductoSustitutos;
            int.TryParse(Fila.Cells["idProductoSustitutos"].Value.ToString(), out idProductoSustitutos);
            oRegistroEN.idProductoSustitutos = idProductoSustitutos;
            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.idUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.idUsuarioModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;
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

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                frmProducto oFrmRegistro = new frmProducto();
                oFrmRegistro.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                oFrmRegistro.VariosRegistros = true;
                oFrmRegistro.ActivarFiltros = true;
                oFrmRegistro.TituloVentana = string.Format( "Seleccionar los productos asociados al '{0}'", txtNombre.Text.Trim());

                oFrmRegistro.AplicarFiltroDeWhereExterno = true;
                oFrmRegistro.WhereExterno = WhereDinamicoDelSustito();

                oFrmRegistro.ShowDialog();

                ProductoEN[] oRegistroEN = new ProductoEN[0];
                oRegistroEN = oFrmRegistro.oProducto;

                if (oRegistroEN.Length > 0)
                {
                    foreach (ProductoEN oRegistro in oRegistroEN)
                    {
                        dgvListar.Rows.Add(false, 0, oRegistro.idProducto, oRegistro.Codigo, oRegistro.CodigoDeBarra, oRegistro.Nombre, oRegistro.NombreGenerico, true);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Buscar registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private string WhereDinamicoDelSustito()
        {
            string Where = "";
            try
            {
                if (dgvListar.Rows.Count > 0)
                {
                    String IdProductoSustituto = "";
                    foreach (DataGridViewRow Fila in dgvListar.Rows)
                    {
                        int idSustituto;
                        int.TryParse(Fila.Cells["idSustituto"].Value.ToString(), out idSustituto);

                        if (IdProductoSustituto.Trim().Length == 0)
                        {
                            if (idSustituto > 0)
                            {
                                IdProductoSustituto = idSustituto.ToString();
                            }
                        }
                        else
                        {
                            if (idSustituto > 0)
                            {
                                IdProductoSustituto = string.Format("{0}, {1}", IdProductoSustituto, idSustituto.ToString());
                            }
                        }
                    }

                    if (IdProductoSustituto.Trim().Length > 0)
                    {
                        Where = string.Format(" and  p.idProducto Not in ({0}) ", IdProductoSustituto);
                    }
                    else { Where = ""; }
                }

                return Where;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Where dinamico buscar productos sustitutos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return Where;

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

                oFrmRegistro.AplicarFiltroDeWhereExterno = true;
                oFrmRegistro.WhereExterno = WhereDinamicoDelSustito();

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

                oFrmRegistro.AplicarFiltroDeWhereExterno = true;
                oFrmRegistro.WhereExterno = WhereDinamicoDelSustito();

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
    }
}
