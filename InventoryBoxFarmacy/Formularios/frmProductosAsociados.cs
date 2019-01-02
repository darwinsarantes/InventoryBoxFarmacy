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
    public partial class frmProductosAsociados : Form
    {
        public int ValorLlavePrimariaEntidad { set; get; }        
        public string TituloDeLaVentana { set; get; }
        public string InformacionDeLaOperacion { set; get; }

        public string CodigoProducto { set; get; }
        public string NombreDelProducto { set; get; }
        public string CodigoDeBarraDelProducto { set; get; }

        public frmProductosAsociados()
        {
            InitializeComponent();
        }

        #region "Funciones del producto sustituto"
        
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

                string OcultarColumnas = "idProductoSustitutos,idSustituto,Actualizar";
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

                ProductoSustitutosEN oRegistroEN = new ProductoSustitutosEN();
                ProductoSustitutosLN oRegistroLN = new ProductoSustitutosLN();

                oRegistroEN.oProductoEN.idProducto = ValorLlavePrimariaEntidad;
                oRegistroEN.OrderBy = " Order By p.Nombre asc ";

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
                        
                        int idProductoSustitutos = 0;
                        int idSustituto = 0;

                        foreach (DataRow row in DTOrden.Rows)
                        {

                            idSustituto = Convert.ToInt32(row["idSustituto"]);
                            idProductoSustitutos = Convert.ToInt32(row["idProductoSustitutos"]);
                            
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
                MessageBox.Show(ex.Message, "Crear y Poblar Columnas, de los diferentes productos sustitutos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dgv.Rows.Count > 0)
            {

                List<DataGridViewRow> rows = (from item in dgv.Rows.Cast<DataGridViewRow>()
                                              let Eliminar = Convert.ToBoolean(item.Cells["Eliminar"].Value ?? false)
                                              let idProductoSustituto = Convert.ToInt32(item.Cells["idProductoSustitutos"].Value)
                                              where Eliminar.Equals(true) || idProductoSustituto.Equals(0)
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
        
        private ProductoSustitutosEN InformacionDelProductoSustituto(DataGridViewRow Fila)
        {
            ProductoSustitutosEN oRegistroEN = new ProductoSustitutosEN();
            
            int idSustituto;
            int.TryParse(Fila.Cells["idSustituto"].Value.ToString(), out idSustituto);
        
            oRegistroEN.oSutitutoEN.idProducto = idSustituto;
            oRegistroEN.oSutitutoEN.Codigo = Fila.Cells["Codigo"].Value.ToString();
            oRegistroEN.oSutitutoEN.CodigoDeBarra = Fila.Cells["CodigoDeBarra"].Value.ToString();
            oRegistroEN.oSutitutoEN.Nombre = Fila.Cells["Nombre"].Value.ToString();
            oRegistroEN.oSutitutoEN.NombreGenerico = Fila.Cells["NombreGenerico"].Value.ToString();

            int idProductoSustitutos;
            int.TryParse(Fila.Cells["idProductoSustitutos"].Value.ToString(), out idProductoSustitutos);           
            oRegistroEN.idProductoSustitutos = idProductoSustitutos;

            oRegistroEN.oProductoEN.idProducto = ValorLlavePrimariaEntidad;
            oRegistroEN.oProductoEN.Nombre = NombreDelProducto;
            oRegistroEN.oProductoEN.Codigo = CodigoProducto;
            oRegistroEN.oProductoEN.CodigoDeBarra = CodigoDeBarraDelProducto;

            /*Informacion general*/
            oRegistroEN.oLoginEN = Program.oLoginEN;
            oRegistroEN.idUsuarioDeCreacion = Program.oLoginEN.idUsuario;
            oRegistroEN.idUsuarioModificacion = Program.oLoginEN.idUsuario;
            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

            return oRegistroEN;
        }

        private void EstablecerTituloDeVentana()
        {
            this.Text = TituloDeLaVentana;
            this.InformacionEntidadOperacion.Text = InformacionDeLaOperacion;
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
                                MessageBox.Show(oRegistroLN.Error, Operacion, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return false;
                            }
                        }

                        if (Operacion == "ACTUALIZAR")
                        {
                            if (oRegistroLN.ValidarRegistroDuplicado(oRegistroEN, Program.oDatosDeConexion, Operacion))
                            {
                                OcultarBarraDeProgreso();
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(oRegistroLN.Error, Operacion, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                                MessageBox.Show(oRegistroLN.Error, Operacion, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                                MessageBox.Show(oRegistroLN.Error, Operacion, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                                MessageBox.Show(oRegistroLN.Error, Operacion, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void cargarProducto()
        {
            try
            {
                frmProducto oFrmRegistro = new frmProducto();
                oFrmRegistro.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                oFrmRegistro.VariosRegistros = true;
                oFrmRegistro.ActivarFiltros = true;
                oFrmRegistro.TituloVentana = string.Format("Seleccionar los productos asociados al '{0}-{1}-{2}'", CodigoProducto, CodigoDeBarraDelProducto, NombreDelProducto);

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

        #endregion

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
        
        private void frmProductosAsociados_Shown(object sender, EventArgs e)
        {
            EstablecerTituloDeVentana();
            CrearyYPoblarColumnasDGVLaboratorio();
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            if (InsertarActualizarOEliminarContenedor())
            {
                this.Close();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            cargarProducto();
        }

        private void tsbCerrarVentan_Click(object sender, EventArgs e)
        {
            if (EvaluarDataGridView(dgvListar))
            {
                if (MessageBox.Show("Hay registros pendiente de guardar, desea cerrar la ventana", "Cerrar la Ventana de galeria", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void tsbRecarRegistro_Click(object sender, EventArgs e)
        {
            if (EvaluarDataGridView(dgvListar))
            {
                if (MessageBox.Show("Se perderan los registros al realizar la operacion, desea continuar!", "Cerrar la Ventana de galeria", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CrearyYPoblarColumnasDGVLaboratorio();
                }
            }
            else
            {
                CrearyYPoblarColumnasDGVLaboratorio();
            }
        }
    }
}
