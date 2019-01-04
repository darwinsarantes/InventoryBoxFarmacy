using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Funciones;
using Entidad;
using Logica;

namespace InventoryBoxFarmacy.Formularios
{
    public partial class frmProductoPromociones : Form
    {
        public frmProductoPromociones()
        {
            InitializeComponent();
        }

        public int ValorLlavePrimariaEntidad { set; get; }
        public string TituloDeLaVentana { set; get; }
        public string InformacionDeLaOperacion { set; get; }
        private bool CerrarVentana = true;

        public string CodigoProducto { set; get; }
        public string NombreDelProducto { set; get; }
        public string CodigoDeBarraDelProducto { set; get; }

        #region "Funciones del programador"

        private void EstablecerTituloDeVentana()
        {
            this.Text = TituloDeLaVentana;
            this.InformacionEntidadOperacion.Text = InformacionDeLaOperacion;
        }
        
        private void CrearColumnasDGVRegistros()
        {
            try
            {

                string columnas = @"idProductoPromocion, idProducto, FechaDeInicio, FechaDeFinalizacion, PrecioDelProducto, Descripcion, Estado";

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

                FormatearDGVProductoSustitutos();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar la lista. \n" + ex.Message, "Poblar columnas dgvListar con los productos sustitutos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearDGVProductoSustitutos()
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

                string OcultarColumnas = "idProductoPromocion, idProducto,Actualizar";
                OcultarColumnasEnELDGV(OcultarColumnas);

                FormatearColumnasDGVListar();

                dgvListar.DefaultCellStyle.Font = new Font(Font.Name, Font.Size, FontStyle.Regular);
                dgvListar.DefaultCellStyle.ForeColor = Color.Black;
                dgvListar.DefaultCellStyle.SelectionForeColor = Color.Black;

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
                            FormatoDGV oFormato = new FormatoDGV(c1.Name.Trim(), "ProductosPromocion");
                            if (oFormato.ValorEncontrado == false)
                            {
                                oFormato = new FormatoDGV(c1.Name.Trim());
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

        private DataTable InformacionDeLosContendoresPorSeccion()
        {
            DataTable ODatos = null;
            try
            {

                ProductoPromocionEN oRegistroEN = new ProductoPromocionEN();
                ProductoPromocionLN oRegistroLN = new ProductoPromocionLN();

                oRegistroEN.oProductoEN.idProducto = ValorLlavePrimariaEntidad;
                oRegistroEN.OrderBy = " Order By FechaDeFinalizacion desc ";

                if (oRegistroLN.ListadoPromocionesXProducto(oRegistroEN, Program.oDatosDeConexion))
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

                        int idProductoPromocion = 0;
                        int idProducto = 0;

                        foreach (DataRow row in DTOrden.Rows)
                        {

                            idProducto = Convert.ToInt32(row["idProducto"]);
                            idProductoPromocion = Convert.ToInt32(row["idProductoPromocion"]);

                            dgvListar.Rows.Add(
                                valor,
                                idProductoPromocion,
                                idProducto,
                                Convert.ToDateTime(row["FechaDeInicio"]).ToLongDateString(),
                                Convert.ToDateTime( row["FechaDeFinalizacion"]).ToLongDateString(),
                                string.Format("{0:###,###,##0.00}",Convert.ToDecimal(row["PrecioDelProducto"])),
                                row["Descripcion"],
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
                MessageBox.Show(ex.Message, "Crear y Poblar Columnas, de los diferentes productos sustitutos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private Boolean LosDatosIngresadosSonCorrectos()
        {

            decimal PrecioDePromocion;
            decimal.TryParse(txtPrecioPromocional.Text, out PrecioDePromocion);
            if (PrecioDePromocion == 0)
            {
                EP.SetError(txtPrecioPromocional, "Este valor no puede ser cero o menor que cero");
                txtPrecioPromocional.Focus();
                return false;
            }


            DateTime FechaActual = System.DateTime.Now;

            if (dtpkDesdePromocion.Value < FechaActual)
            {
                EP.SetError(dtpkDesdePromocion, "La fecha Ingresada de inicio de la promoción no puede ser menor que la fecha actual del SO");
                dtpkDesdePromocion.Focus();
                return false;
            }

            if (dtpkHastaPromocional.Value < dtpkDesdePromocion.Value)
            {
                EP.SetError(dtpkHastaPromocional, string.Format("La fecha Ingresada de finalización de la promoción no puede ser {0} menor que la fecha de inicio de la promocion", Environment.NewLine));
                dtpkHastaPromocional.Focus();
                return false;
            }

            if (Controles.IsNullOEmptyElControl(txtDescripcionDeLaPromocion))
            {
                EP.SetError(txtDescripcionDeLaPromocion, "Agrege una descripcíon de la promoción que se le va aplicar al producto");
                txtDescripcionDeLaPromocion.Focus();
                return false;
            }

            return true;
        }

        private ProductoPromocionEN InformacionDeLaPromocionDelProducto()
        {
            ProductoPromocionEN oRegistroEN = new ProductoPromocionEN();

            int idProductoPromocion;
            int.TryParse(txtidProductoPromocion.Text, out idProductoPromocion);

            oRegistroEN.idProductoPromocion = idProductoPromocion;
            oRegistroEN.oProductoEN.idProducto = ValorLlavePrimariaEntidad;
            oRegistroEN.oProductoEN.Nombre = NombreDelProducto;
            oRegistroEN.oProductoEN.Codigo = CodigoProducto;
            oRegistroEN.oProductoEN.CodigoDeBarra = CodigoDeBarraDelProducto;

            decimal PrecioDelProducto;
            decimal.TryParse(txtPrecioPromocional.Text, out PrecioDelProducto);
            oRegistroEN.PrecioDelProducto = PrecioDelProducto;
            oRegistroEN.FechaDeInicio = dtpkDesdePromocion.Value;
            oRegistroEN.FechaDeFinalizacion = dtpkHastaPromocional.Value;
            oRegistroEN.Estado = cmbEstado.Text.Trim();
            oRegistroEN.Descripcion = txtDescripcionDeLaPromocion.Text.Trim();

            //partes generales.            
            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.idUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.idUsuarioModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;

        }

        private void LimpiarControles()
        {
            txtDescripcionDeLaPromocion.Clear();
            txtPrecioPromocional.Text = "0.00";
            dtpkDesdePromocion.Value = System.DateTime.Now;
            dtpkHastaPromocional.Value = System.DateTime.Now;
            cmbEstado.SelectedIndex = -1;
        }

        private void Guardar()
        {

            try
            {

                if (LosDatosIngresadosSonCorrectos())
                {

                    ProductoPromocionEN oRegistroEN = InformacionDeLaPromocionDelProducto();
                    ProductoPromocionLN oRegistroLN = new ProductoPromocionLN();

                    if(oRegistroLN.ValidarFechaDelRegistro(oRegistroEN, Program.oDatosDeConexion, "AGREGAR"))
                    {
                        MessageBox.Show(oRegistroLN.Error, "Guardar información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if(oRegistroLN.Agregar(oRegistroEN, Program.oDatosDeConexion))
                    {
                                                
                        if(chkCerrarVentana.Checked == true)
                        {
                            this.Close();
                        }else
                        {
                            CrearyYPoblarColumnasDGVLaboratorio();
                            LimpiarControles();
                        }

                    }else
                    {
                        throw new ArgumentException(oRegistroLN.Error);
                    }

                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Guardar el registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void Actualizar()
        {

            try
            {

                if (LosDatosIngresadosSonCorrectos())
                {

                    ProductoPromocionEN oRegistroEN = InformacionDeLaPromocionDelProducto();
                    ProductoPromocionLN oRegistroLN = new ProductoPromocionLN();

                    if (oRegistroLN.ValidarFechaDelRegistro(oRegistroEN, Program.oDatosDeConexion, "ACTUALIZAR"))
                    {
                        MessageBox.Show(oRegistroLN.Error, "Actualizar información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (oRegistroLN.Actualizar(oRegistroEN, Program.oDatosDeConexion))
                    {
                        
                        if (chkCerrarVentana.Checked == true)
                        {
                            this.Close();
                        }
                        else
                        {
                            CrearyYPoblarColumnasDGVLaboratorio();
                            LimpiarControles();
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
                MessageBox.Show(ex.Message, "Guardar el registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        #endregion

        private void frmProductoPromociones_Shown(object sender, EventArgs e)
        {
            EstablecerTituloDeVentana();
            CrearyYPoblarColumnasDGVLaboratorio();
            LimpiarControles();
            chkCerrarVentana.Checked = CerrarVentana;
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            int idProductoPromocion;
            int.TryParse(txtidProductoPromocion.Text, out idProductoPromocion);

            if(idProductoPromocion == 0)
            {
                Guardar();
            }else
            {
                Actualizar();
            }

        }
    }
}
