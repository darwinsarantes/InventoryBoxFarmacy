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
    public partial class frmLocalizacionDelProducto : Form
    {
        public frmLocalizacionDelProducto()
        {
            InitializeComponent();
        }

        private string NOMBRE_ENTIDAD_PRIVILEGIO = "Bodega";        
        private string NOMBRE_LLAVE_PRIMARIA = "idBodega";
        private int ValorLlavePrimariaEntidad;
        private int IndiceSeleccionado;
        public bool AplicarFiltroDeWhereExterno { set; get; }
        public string WhereExterno { set; get; }

        #region "Funciones del programador"

        public bool ActivarFiltros { set; get; }
        public bool VariosRegistros { set; get; }
        public string TituloVentana { set; get; }
        public LocalizacionDelProductoEN[] oLocalizacionDelProductoEN = new LocalizacionDelProductoEN[0];

        public string Columnas { set; get; }

        public DataTable DTRegistros;

        public bool Activar_btn_imprimir { set; get; }
        /// <summary>
        /// Activa o Desactiva la opcion de ingresar un registro de la barra de menus.
        /// </summary>
        public bool Activar_btn_Nuevo { set; get; }
        /// <summary>
        /// Activa o Desactiva la opción de desplegar el menu contextual de la ventana.
        /// </summary>
        public bool Activar_MenuContextual { set; get; }
        /// <summary>
        /// Activa o Desactiva la opción de ingresar un nuevo registro dentro del menu contextual.
        /// </summary>
        public bool Activar_MenuContextual_Nuevo { set; get; }
        /// <summary>
        /// Activa o Desactiva la opción de ingresar un nuevo registro a partir de uno ya existente, dentro del menu contextual
        /// </summary>
        public bool Activar_MenuContextual_NuevoApartirDe { set; get; }
        /// <summary>
        /// Activa o Desactiva la opción de Modifcar un registro, dentro del menu contextual
        /// </summary>
        public bool Activar_MenuContextual_Modificar { set; get; }
        /// <summary>
        /// Activa o Desactiva la opcioón de Eliminar un registro, dentro del menu contextual.
        /// </summary>
        public bool Activar_MenuContextual_Eliminar { set; get; }
        /// <summary>
        /// Activa o Desactiva la opción la de Consultar, dentro del menu contextual.
        /// </summary>
        public bool Activar_MenuContextual_Consultar { set; get; }

        public bool Activar_Exportacion { set; get; }

        private void ActivarFiltrosDelaBusqueda()
        {
            if (ActivarFiltros == false)
            {

                tsbFiltrar.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbMarcarTodos.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbSeleccionarTodos.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

                tsbMarcarTodos.Visible = false;
                tsbSeleccionarTodos.Visible = false;

                VariosRegistros = false;

                Activar_MenuContextual = true;

                Activar_MenuContextual_Consultar = true;
                Activar_MenuContextual_Nuevo = true;
                Activar_MenuContextual_NuevoApartirDe = true;
                Activar_MenuContextual_Eliminar = true;
                Activar_MenuContextual_Modificar = true;


            }
            else
            {
                if (ActivarFiltros == true)
                {

                    tsbFiltrar.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    tsbMarcarTodos.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    tsbSeleccionarTodos.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

                    tsbSeleccionarTodos.Visible = true;

                    if (VariosRegistros == false)
                    {
                        tsbMarcarTodos.Visible = false;
                    }
                    else
                    {
                        tsbMarcarTodos.Visible = true;
                    }

                    this.Text = TituloVentana;

                    AgregrarColumnasAlDTRegistros();

                }
            }
        }

        private void AgregrarColumnasAlDTRegistros()
        {
            if (Columnas == null) return;

            if (DTRegistros == null)
            {

                string[] arrayColumnas = Columnas.Split(',');

                DTRegistros = new DataTable();

                foreach (string item in arrayColumnas)
                {
                    DataColumn c = DTRegistros.Columns.Add();
                    c.ColumnName = item.Trim();

                }
            }
        }

        private void DesmarcarFilas(int FilaMarcada)
        {
            try
            {

                foreach (DataGridViewRow Fila in dgvLista.Rows)
                {
                    if (Fila.Index != FilaMarcada && Convert.ToBoolean(Fila.Cells["Seleccionar"].Value) == true)
                    {
                        Fila.Cells["Seleccionar"].Value = false;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al desmarcar filas. \n" + ex.Message, "FormatoDGV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgregarRegistrosAlDTUsuario()
        {
            if (Columnas == null) return;

            if (oLocalizacionDelProductoEN.Length > 0)
            {
                //DataTable DTClass = ConvertirClassADT();
                DataTable DTClass = TraerInformacionDDGV();

                foreach (DataRow Fila in DTClass.Rows)
                {
                    bool Existe = false;

                    if (DTRegistros.Rows.Count > 0)
                    {
                        foreach (DataRow Item in DTRegistros.Rows)
                        {
                            int IdRegistro;
                            int.TryParse(Item[NOMBRE_LLAVE_PRIMARIA].ToString(), out IdRegistro);

                            if (IdRegistro == Convert.ToInt32(Fila[NOMBRE_LLAVE_PRIMARIA]))
                            {
                                Existe = true;
                            }
                        }
                    }
                    else
                    {
                        Existe = false;
                    }

                    if (Existe == false)
                    {
                        DataRow row = DTRegistros.Rows.Add();

                        String[] ArrayColumnas = Columnas.Split(',');

                        foreach (string c in ArrayColumnas)
                        {
                            row[c.Trim()] = Fila[c.Trim()];
                        }

                        Application.DoEvents();

                    }

                }
            }
        }

        private DataTable ConvertirClassADT()
        {
            DataTable DTClass = new DataTable();

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(oLocalizacionDelProductoEN.GetType());
            System.IO.StringWriter sw = new System.IO.StringWriter();
            serializer.Serialize(sw, oLocalizacionDelProductoEN);

            DataSet ds = new DataSet(NOMBRE_ENTIDAD_PRIVILEGIO);
            System.IO.StringReader reader = new System.IO.StringReader(sw.ToString());
            ds.ReadXml(reader);

            DTClass = ds.Tables[0];
            return DTClass;

        }

        private DataTable TraerInformacionDDGV()
        {
            DataTable DT = (DataTable)dgvLista.DataSource;
            DataTable DTCopy = new DataTable();

            if (dgvLista.Rows.Count > 0)
            {
                DTCopy = DT.AsEnumerable().Where(r => r.Field<bool>("Seleccionar") == true).CopyToDataTable();
            }

            return DTCopy;
        }

        private DataTable AgregarColumnaSeleccionar(DataTable Datos)
        {
            DataColumn Seleccionar = new DataColumn("Seleccionar", Type.GetType("System.Boolean"));
            Seleccionar.Caption = " ";
            Seleccionar.DefaultValue = false;

            Datos.Columns.Add(Seleccionar);
            Seleccionar.SetOrdinal(0);

            return Datos;
        }

        private string WhereDinamico() {

            string Where = "";

            if (dgvLista.Rows.Count <= 0)
            {
                return "";
            }

            string TablaDeReferencia = dgvLista.Rows[0].Cells["TablaDeReferencia"].Value.ToString().Trim() ;            
            switch (TablaDeReferencia)
            {
                case "Contenedor":

                    if(Controles.IsNullOEmptyElControl(chkCodigoDeAlmacenaje) == false && Controles.IsNullOEmptyElControl(txtCodigoDeAlmacenaje) == false)
                    {
                        Where += string.Format(" and concat(a.Codigo,'-', b.Codigo, '-', l.Codigo, '-', s.Codigo, '-',c.Codigo) like '%{0}%'", txtCodigoDeAlmacenaje.Text);
                    }

                    Where += WhereDinamicoContenedor();
                    Where += WhereDinamicoSeccion();
                    Where += WhereDinamicoLocacion();
                    Where += WhereDinamicoBodega();
                    Where += WhereDinamicoAlmacen();

                    break;

                case "Seccion":

                    if (Controles.IsNullOEmptyElControl(chkCodigoDeAlmacenaje) == false && Controles.IsNullOEmptyElControl(txtCodigoDeAlmacenaje) == false)
                    {
                        Where += string.Format(" and concat(a.Codigo,'-', b.Codigo, '-', l.Codigo, '-', s.Codigo) like '%{0}%'", txtCodigoDeAlmacenaje.Text);
                    }

                    Where += WhereDinamicoSeccion();
                    Where += WhereDinamicoLocacion();
                    Where += WhereDinamicoBodega();
                    Where += WhereDinamicoAlmacen();

                    break;

                case "Locacion":

                    if (Controles.IsNullOEmptyElControl(chkCodigoDeAlmacenaje)== false && Controles.IsNullOEmptyElControl(txtCodigoDeAlmacenaje) == false)
                    {
                        Where += string.Format(" and concat(a.Codigo,'-', b.Codigo, '-', l.Codigo) like '%{0}%'", txtCodigoDeAlmacenaje.Text);
                    }

                    Where += WhereDinamicoLocacion();
                    Where += WhereDinamicoBodega();
                    Where += WhereDinamicoAlmacen();

                    break;

                case "Bodega":

                    if (Controles.IsNullOEmptyElControl(chkCodigoDeAlmacenaje) == false && Controles.IsNullOEmptyElControl(txtCodigoDeAlmacenaje) == false)
                    {
                        Where += string.Format(" and concat(a.Codigo,'-', b.Codigo) like '%{0}%'", txtCodigoDeAlmacenaje.Text);
                    }

                    Where += WhereDinamicoBodega();
                    Where += WhereDinamicoAlmacen();

                    break;

                case "Almacen":

                    if (Controles.IsNullOEmptyElControl(chkCodigoDeAlmacenaje) == false && Controles.IsNullOEmptyElControl(txtCodigoDeAlmacenaje) == false)
                    {
                        Where += string.Format(" and a.Codigo like '%{0}%'", txtCodigoDeAlmacenaje.Text);
                    }

                    Where += WhereDinamicoAlmacen();

                    break;

            }

            if (AplicarFiltroDeWhereExterno == true)
            {
                Where += WhereExterno;
            }

            return Where;

        }

        private string WhereDinamicoContenedor()
        {
            string Where = "";

            if (Controles.IsNullOEmptyElControl(chkCodigoDelContenedor) == false && Controles.IsNullOEmptyElControl(txtCodigoDelContenedor) == false)
            {
                Where += string.Format(" and c.Codigo like '%{0}%' ", txtCodigoDelContenedor.Text.Trim());
            }

            if (Controles.IsNullOEmptyElControl(chkNombreDelContenedor) == false && Controles.IsNullOEmptyElControl(cmbContenedor) == false)
            {
                Where += string.Format(" and c.idContenedor = {0} ", cmbContenedor.SelectedValue);
            }

            return Where;
        }

        private string WhereDinamicoSeccion()
        {
            string Where = "";

            if (Controles.IsNullOEmptyElControl(chkCodigoSeccion) == false && Controles.IsNullOEmptyElControl(txtCodigoDeSeccion) == false)
            {
                Where += string.Format(" and s.Codigo like '%{0}%' ", txtCodigoDeSeccion.Text.Trim());
            }

            if (Controles.IsNullOEmptyElControl(chkSeccion) == false && Controles.IsNullOEmptyElControl(cmbSeccion) == false)
            {
                Where += string.Format(" and s.idSeccion = {0} ", cmbSeccion.SelectedValue);
            }

            return Where;
        }

        private string WhereDinamicoLocacion()
        {
            string Where = "";

            if (Controles.IsNullOEmptyElControl(chkCodigoLocacion) == false && Controles.IsNullOEmptyElControl(txtCodigoLocacion) == false)
            {
                Where += string.Format(" and l.Codigo like '%{0}%' ", txtCodigoLocacion.Text.Trim());
            }

            if (Controles.IsNullOEmptyElControl(chkLocacion) == false && Controles.IsNullOEmptyElControl(cmbLocacion) == false)
            {
                Where += string.Format(" and l.idLocacion = {0} ", cmbLocacion.SelectedValue);
            }

            return Where;
        }

        private string WhereDinamicoBodega()
        {
            string Where = "";

            if (Controles.IsNullOEmptyElControl(chkCodigoDeLaBodega) == false && Controles.IsNullOEmptyElControl(txtCodigoDeLaBodega) == false)
            {
                Where += string.Format(" and b.Codigo like '%{0}%' ", txtCodigoDeLaBodega.Text.Trim());
            }

            if (Controles.IsNullOEmptyElControl(chkBodega) == false && Controles.IsNullOEmptyElControl(cmbBodega) == false)
            {
                Where += string.Format(" and b.idBodega = {0} ", cmbBodega.SelectedValue);
            }

            return Where;
        }

        private string WhereDinamicoAlmacen()
        {
            string Where = "";

            if (Controles.IsNullOEmptyElControl(chkCodigoDelAmacen) == false && Controles.IsNullOEmptyElControl(txtCodigoDelAlmacen) == false)
            {
                Where += string.Format(" and a.Codigo like '%{0}%' ", txtCodigoDelAlmacen.Text.Trim());
            }

            if (Controles.IsNullOEmptyElControl(chkAlmacen) == false && Controles.IsNullOEmptyElControl(cmbAlmacen) == false)
            {
                Where += string.Format(" and a.idAlmacen = {0} ", cmbAlmacen.SelectedValue);
            }

            return Where;
        }

        private string TituloDinamico()
        {

            string Titulo = "";

            if (Controles.IsNullOEmptyElControl(chkIdentificador) == false && Controles.IsNullOEmptyElControl(txtIdentificador) == false)
            {
                Titulo += string.Format(" Identificador: '{0}', ", txtIdentificador.Text.Trim());
            }

            if (Controles.IsNullOEmptyElControl(chkCodigoDelAmacen) == false && Controles.IsNullOEmptyElControl(txtCodigoDelAlmacen) == false)
            {
                Titulo += string.Format(" Código del Almacen: '{0}', ", txtCodigoDelAlmacen.Text.Trim());
            }

            if (Controles.IsNullOEmptyElControl(chkAlmacen) == false && Controles.IsNullOEmptyElControl(cmbAlmacen) == false)
            {
                Titulo += string.Format(" Álmacen: '{0}', ", cmbAlmacen.Text.Trim());
            }
                       
            if (Titulo.Length > 0)
            {
                Titulo = Titulo.Substring(0, Titulo.Length - 2);
            }

            return Titulo;
                        

        }

        private void LLenarListado() {

            try
            {

                this.Cursor = Cursors.WaitCursor;

                BodegaEN oRegistrosEN = new BodegaEN();
                BodegaLN oRegistrosLN = new BodegaLN();

                oRegistrosEN.Where = WhereDinamico();
                
                if (oRegistrosLN.ListadoDeAlmacenajeDelProducto(oRegistrosEN, Program.oDatosDeConexion)) {

                    dgvLista.Columns.Clear();
                    System.Diagnostics.Debug.Print(oRegistrosLN.TraerDatos().Rows.Count.ToString());

                    if (ActivarFiltros == true)
                    {
                        dgvLista.DataSource = AgregarColumnaSeleccionar(oRegistrosLN.TraerDatos());
                    }
                    else {
                        dgvLista.DataSource = oRegistrosLN.TraerDatos();
                    }

                    FormatearDGV();                   
                    this.dgvLista.ClearSelection();

                    tsbNoRegistros.Text = "No. Registros: " + oRegistrosLN.TotalRegistros().ToString();
                    

                }
                else
                {
                    throw new ArgumentException(oRegistrosLN.Error);
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Llenar listado de registro en la lista", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }                

        }

        private void FormatearDGV()
        {
            try
            {
                this.dgvLista.AllowUserToResizeRows = false;
                this.dgvLista.AllowUserToAddRows = false;
                this.dgvLista.AllowUserToDeleteRows = false;
                this.dgvLista.DefaultCellStyle.BackColor = Color.White;

                if (VariosRegistros == true)
                {
                    this.dgvLista.MultiSelect = VariosRegistros;
                    this.dgvLista.RowHeadersVisible = false;
                }
                else
                {
                    this.dgvLista.MultiSelect = false;
                    this.dgvLista.RowHeadersVisible = true;
                }

                this.dgvLista.DefaultCellStyle.Font = new Font("Segoe UI", 8);
                this.dgvLista.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8);
                this.dgvLista.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
                this.dgvLista.BackgroundColor = System.Drawing.SystemColors.Window;
                this.dgvLista.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

                string OcultarColumnas = "idContenedor,idSeccion,idLocacion,idBodega,idAlmacen,CodigoDelAlmacen,NombreDelAlmacen,CodigoDeBodega,NombreDeLaBodega,CodigoDeLocacion,NombreDeLaLocacion,CodigoDeLaSeccion,NombreDeLaSeccion,CodigoDelContenedor,NombreDelContenedor,TablaDeReferencia";
                OcultarColumnasEnElDGV(OcultarColumnas);

                FormatearColumnasDelDGV();

                this.dgvLista.RowHeadersWidth = 25;

                this.dgvLista.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dgvLista.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                this.dgvLista.StandardTab = true;
                this.dgvLista.ReadOnly = false;
                this.dgvLista.CellBorderStyle = DataGridViewCellBorderStyle.Raised;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "FormatoDGV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OcultarColumnasEnElDGV(String ColumnasDelDGV)
        {
            if (dgvLista.Columns.Count > 0)
            {
                String[] ArrayColumnasDGV = ColumnasDelDGV.Split(',');
                foreach (String c in ArrayColumnasDGV)
                {

                    foreach (DataGridViewColumn c1 in dgvLista.Columns)
                    {
                        if (c1.Name.Trim().ToUpper() == c.Trim().ToUpper())
                        {
                            c1.Visible = false;
                        }
                    }

                }
            }
        }

        private void FormatearColumnasDelDGV()
        {
            if (dgvLista.Columns.Count > 0)
            {

                foreach (DataGridViewColumn c1 in dgvLista.Columns)
                {
                    if (c1.Visible == true)
                    {
                        if (c1.Name.Trim().ToUpper() != "Seleccionar".ToUpper())
                        {
                            FormatoDGV oFormato = new FormatoDGV(c1.Name.Trim(), "AlmacenajeDelProducto");
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
                
        private void AsignarLlavePrimaria()
        {
            this.ValorLlavePrimariaEntidad = Convert.ToInt32(this.dgvLista.Rows[this.IndiceSeleccionado].Cells[this.NOMBRE_LLAVE_PRIMARIA].Value);
        }

        private void LlenarComboAlmacen()
        {
            try
            {

                AlmacenEN oRegistroEN = new AlmacenEN();
                AlmacenLN oRegistroLN = new AlmacenLN();

                if(oRegistroLN.ListadoParaCombos(oRegistroEN, Program.oDatosDeConexion))
                {

                    cmbAlmacen.DataSource = oRegistroLN.TraerDatos();
                    cmbAlmacen.DisplayMember = "Almacen";
                    cmbAlmacen.ValueMember = "idAlmacen";

                    if(oRegistroLN.TraerDatos().Rows.Count == 1)
                    {
                        cmbAlmacen.SelectedIndex = 0;
                        chkAlmacen.CheckState = CheckState.Checked;
                    }
                    else
                    {
                        cmbAlmacen.SelectedIndex = -1;
                    }

                }else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Combo de almacenamiento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LlenarComboBodega()
        {
            try
            {

                BodegaEN oRegistroEN = new BodegaEN();

                oRegistroEN.Where = "";

                if(cmbAlmacen.SelectedIndex != -1)
                {
                    oRegistroEN.Where = string.Format(" and a.idAlmacen = {0}", cmbAlmacen.SelectedValue);
                }
                
                BodegaLN oRegistroLN = new BodegaLN();

                if (oRegistroLN.ListadoParaCombos(oRegistroEN, Program.oDatosDeConexion))
                {

                    cmbBodega.DataSource = oRegistroLN.TraerDatos();
                    cmbBodega.DisplayMember = "Bodega";
                    cmbBodega.ValueMember = "idBodega";

                    if (oRegistroLN.TraerDatos().Rows.Count == 1)
                    {
                        cmbBodega.SelectedIndex = 0;
                        chkBodega.CheckState = CheckState.Checked;
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
                MessageBox.Show(ex.Message, "Combo de la bodega", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LLenarComboLocacion()
        {
            try
            {

                LocacionEN oRegistroEN = new LocacionEN();

                oRegistroEN.Where = "";

                if (cmbBodega.SelectedIndex != -1)
                {
                    oRegistroEN.Where = string.Format(" and idBodega = {0}", cmbBodega.SelectedValue);
                }

                LocacionLN oRegistroLN = new LocacionLN();

                if (oRegistroLN.ListadoParaCombos(oRegistroEN, Program.oDatosDeConexion))
                {

                    cmbLocacion.DataSource = oRegistroLN.TraerDatos();
                    cmbLocacion.DisplayMember = "Locacion";
                    cmbLocacion.ValueMember = "idLocacion";

                    if (oRegistroLN.TraerDatos().Rows.Count == 1)
                    {
                        cmbLocacion.SelectedIndex = 0;
                        chkLocacion.CheckState = CheckState.Checked;
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
                MessageBox.Show(ex.Message, "Combo de la locacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LlenarComboSeccion()
        {
            try
            {

                SeccionEN oRegistroEN = new SeccionEN();

                oRegistroEN.Where = "";

                if (cmbLocacion.SelectedIndex != -1)
                {
                    oRegistroEN.Where = string.Format(" and s.idLocacion = {0}", cmbLocacion.SelectedValue);
                }

                SeccionLN oRegistroLN = new SeccionLN();

                if (oRegistroLN.ListadoParaCombos(oRegistroEN, Program.oDatosDeConexion))
                {

                    cmbSeccion.DataSource = oRegistroLN.TraerDatos();
                    cmbSeccion.DisplayMember = "Seccion";
                    cmbSeccion.ValueMember = "idSeccion";

                    if (oRegistroLN.TraerDatos().Rows.Count == 1)
                    {
                        cmbSeccion.SelectedIndex = 0;
                        chkSeccion.CheckState = CheckState.Checked;
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
                MessageBox.Show(ex.Message, "Combo de la bodega", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LlenarComboContenedor()
        {
            try
            {

                ContenedorEN oRegistroEN = new ContenedorEN();

                oRegistroEN.Where = "";

                if (cmbSeccion.SelectedIndex != -1)
                {
                    oRegistroEN.Where = string.Format(" and idSeccion = {0}", cmbSeccion.SelectedValue);
                }

                ContenedorLN oRegistroLN = new ContenedorLN();

                if (oRegistroLN.ListadoParaCombos(oRegistroEN, Program.oDatosDeConexion))
                {

                    cmbContenedor.DataSource = oRegistroLN.TraerDatos();
                    cmbContenedor.DisplayMember = "Contenedor";
                    cmbContenedor.ValueMember = "idContenedor";

                    if (oRegistroLN.TraerDatos().Rows.Count == 1)
                    {
                        cmbContenedor.SelectedIndex = 0;
                        chkNombreDelContenedor.CheckState = CheckState.Checked;
                    }
                    else
                    {
                        cmbContenedor.SelectedIndex = -1;
                    }

                }
                else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Combo de la bodega", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region "Eventos del formulario"

        private void frmLocalizacionDelProducto_Shown(object sender, EventArgs e)
        {
            LlenarComboAlmacen();
            LlenarComboBodega();
            LLenarComboLocacion();
            LlenarComboSeccion();
            LlenarComboContenedor();
            ActivarFiltrosDelaBusqueda();
            tsbFiltroAutomatico_Click(null, null);
            LLenarListado();
            ActivarControlesSegunNivelAlmacenamiento();

        }

        private void ActivarControlesSegunNivelAlmacenamiento()
        {
            try
            {
                if(dgvLista.Rows.Count > 0)
                {

                    DataGridViewRow Fila = dgvLista.Rows[0];
                    String TablaDeReferencia = Fila.Cells["TablaDeReferencia"].Value.ToString().Trim();

                    gbAlmacenaje.Visible = true;


                    switch (TablaDeReferencia)
                    {
                        case "Contenedor":
                            gbAlmacen.Visible = true;
                            gbBodega.Visible = true;
                            gbLocacion.Visible = true;
                            gbSeccion.Visible = true;
                            gbContenedor.Visible = true;
                            break;

                        case "Seccion":
                            gbAlmacen.Visible = true;
                            gbBodega.Visible = true;
                            gbLocacion.Visible = true;
                            gbSeccion.Visible = true;
                            gbContenedor.Visible = false;
                            break;

                        case "Locacion":
                            gbAlmacen.Visible = true;
                            gbBodega.Visible = true;
                            gbLocacion.Visible = true;
                            gbSeccion.Visible = false;
                            gbContenedor.Visible = false;
                            break;

                        case "Bodega":
                            gbAlmacen.Visible = true;
                            gbBodega.Visible = true;
                            gbLocacion.Visible = false;
                            gbSeccion.Visible = false;
                            gbContenedor.Visible = false;
                            break;

                        case "Almacen":
                            gbAlmacen.Visible = true;
                            gbBodega.Visible = false;
                            gbLocacion.Visible = false;
                            gbSeccion.Visible = false;
                            gbContenedor.Visible = false;
                            break;

                    }


                }
                else
                {
                    gbAlmacenaje.Enabled = false;
                    gbAlmacen.Visible = false;
                    gbBodega.Visible = false;
                    gbLocacion.Visible = false;
                    gbContenedor.Visible = false;
                    gbSeccion.Visible = true;
                }
                

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Activar controles segun nivel de Almacenamiento", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbFiltrar_Click(object sender, EventArgs e)
        {
            LLenarListado();
        }

        private void tsbSeleccionarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                tsbSeleccionarTodos.Checked = !tsbMarcarTodos.Checked;

                if (tsbSeleccionarTodos.Checked == true)
                {
                    tsbSeleccionarTodos.Image = Properties.Resources.unchecked16x16;
                }
                else
                {
                    tsbSeleccionarTodos.Image = Properties.Resources.checked16x16;
                }

                int a = 0;
                this.Cursor = Cursors.WaitCursor;

                if (dgvLista.Rows.Count > 0)
                {
                    foreach (DataGridViewRow Fila in dgvLista.Rows)
                    {
                        if (Convert.ToBoolean(Fila.Cells["Seleccionar"].Value) == true)
                        {
                            a++;
                            Array.Resize(ref oLocalizacionDelProductoEN, a);                            
                            LocalizacionDelProductoEN oRegistroEN = new LocalizacionDelProductoEN();

                            oRegistroEN.Codigo = Fila.Cells["CodigoDeAlmacenaje"].Value.ToString();
                            oRegistroEN.TablaDeReferencia = Fila.Cells["TablaDeReferencia"].Value.ToString().Trim();

                            switch (oRegistroEN.TablaDeReferencia)
                            {
                                case "Contenedor":
                                    oRegistroEN.idLocalizacionDelProducto = Convert.ToInt32(Fila.Cells["idContenedor"].Value.ToString());
                                    break;

                                case "Seccion":
                                    oRegistroEN.idLocalizacionDelProducto = Convert.ToInt32(Fila.Cells["idSeccion"].Value.ToString());
                                    break;

                                case "Locacion":
                                    oRegistroEN.idLocalizacionDelProducto = Convert.ToInt32(Fila.Cells["idLocacion"].Value.ToString());
                                    break;

                                case "Bodega":
                                    oRegistroEN.idLocalizacionDelProducto = Convert.ToInt32(Fila.Cells["idBodega"].Value.ToString());
                                    break;
                                    
                                case "Almacen":
                                    oRegistroEN.idLocalizacionDelProducto = Convert.ToInt32(Fila.Cells["idAlmacen"].Value.ToString());
                                    break;

                            }

                            foreach (DataGridViewColumn Columna in dgvLista.Columns)
                            {
                                
                                switch (Columna.Name.Trim())
                                {
                                    case "idContenedor":
                                        oRegistroEN.oContenedorEN.idContenedor = Convert.ToInt32(Fila.Cells[Columna.Name.Trim()].Value.ToString());
                                        break;

                                    case "idSeccion":
                                        oRegistroEN.oContenedorEN.oSeccionEN.idSeccion = Convert.ToInt32(Fila.Cells[Columna.Name.Trim()].Value.ToString());
                                        break;

                                    case "idLocacion":
                                        oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.idLocacion = Convert.ToInt32(Fila.Cells[Columna.Name.Trim()].Value.ToString());
                                        break;

                                    case "idBodega":
                                        oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.oBodegaEN.idBodega = Convert.ToInt32(Fila.Cells[Columna.Name.Trim()].Value.ToString());
                                        break;

                                    case "idAlmacen":
                                        oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.oBodegaEN.oAlmacenEN.idAlmacen = Convert.ToInt32(Fila.Cells[Columna.Name.Trim()].Value.ToString());
                                        break;

                                    case "CodigoDelAlmacen":
                                        oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.oBodegaEN.oAlmacenEN.Codigo = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                        break;

                                    case "NombreDelAlmacen":
                                        oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.oBodegaEN.oAlmacenEN.Nombre = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                        break;

                                    case "CodigoDeBodega":
                                        oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.oBodegaEN.Codigo = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                        break;

                                    case "NombreDeLaBodega":
                                        oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.oBodegaEN.Nombre = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                        break;

                                    case "CodigoDeLocacion":
                                        oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.Codigo = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                        break;

                                    case "NombreDeLaLocacion":
                                        oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.Nombre = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                        break;

                                    case "CodigoDeLaSeccion":
                                        oRegistroEN.oContenedorEN.oSeccionEN.Codigo = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                        break;

                                    case "NombreDeLaSeccion":
                                        oRegistroEN.oContenedorEN.oSeccionEN.Nombre = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                        break;

                                    case "CodigoDelContenedor":
                                        oRegistroEN.oContenedorEN.Codigo = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                        break;

                                    case "NombreDelContenedor":
                                        oRegistroEN.oContenedorEN.Nombre = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                        break;


                                }

                            }

                            oLocalizacionDelProductoEN[a - 1] = oRegistroEN;

                        }
                    }
                }
                                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Seleccionar registros", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                //AgregarRegistrosAlDTUsuario();
                this.Cursor = Cursors.Default;
                this.Close();
            }
        }

        private void dgvLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ActivarFiltros == true)
            {
                if (dgvLista.Columns[dgvLista.CurrentCell.ColumnIndex].Name == "Seleccionar" && VariosRegistros == false)
                {
                    if (Convert.ToBoolean(dgvLista.CurrentCell.Value) == true)
                    {
                        DesmarcarFilas(dgvLista.CurrentCell.RowIndex);
                    }
                }
            }
        }

        private void dgvLista_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            this.IndiceSeleccionado = e.RowIndex;
        }

        private void dgvLista_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvLista.IsCurrentCellDirty)
            {
                dgvLista.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvLista_DoubleClick(object sender, EventArgs e)
        {
            if (ActivarFiltros == true)
            {
                int a = 0;
                this.Cursor = Cursors.WaitCursor;

                dgvLista.CurrentRow.Cells["Seleccionar"].Value = true;

                foreach (DataGridViewRow Fila in dgvLista.Rows)
                {
                    if (Convert.ToBoolean(Fila.Cells["Seleccionar"].Value) == true)
                    {
                        a++;
                        Array.Resize(ref oLocalizacionDelProductoEN, a);

                        LocalizacionDelProductoEN oRegistroEN = new LocalizacionDelProductoEN();

                        oRegistroEN.Codigo = Fila.Cells["CodigoDeAlmacenaje"].Value.ToString();
                        oRegistroEN.TablaDeReferencia = Fila.Cells["TablaDeReferencia"].Value.ToString().Trim();

                        switch (oRegistroEN.TablaDeReferencia)
                        {
                            case "Contenedor":
                                oRegistroEN.idLocalizacionDelProducto = Convert.ToInt32(Fila.Cells["idContenedor"].Value.ToString());
                                break;

                            case "Seccion":
                                oRegistroEN.idLocalizacionDelProducto = Convert.ToInt32(Fila.Cells["idSeccion"].Value.ToString());
                                break;

                            case "Locacion":
                                oRegistroEN.idLocalizacionDelProducto = Convert.ToInt32(Fila.Cells["idLocacion"].Value.ToString());
                                break;

                            case "Bodega":
                                oRegistroEN.idLocalizacionDelProducto = Convert.ToInt32(Fila.Cells["idBodega"].Value.ToString());
                                break;

                            case "Almacen":
                                oRegistroEN.idLocalizacionDelProducto = Convert.ToInt32(Fila.Cells["idAlmacen"].Value.ToString());
                                break;

                        }

                        foreach (DataGridViewColumn Columna in dgvLista.Columns)
                        {
                            
                            switch (Columna.Name.Trim())
                            {
                                case "idContenedor":
                                    oRegistroEN.oContenedorEN.idContenedor = Convert.ToInt32(Fila.Cells[Columna.Name.Trim()].Value.ToString());
                                    break;

                                case "idSeccion":
                                    oRegistroEN.oContenedorEN.oSeccionEN.idSeccion = Convert.ToInt32(Fila.Cells[Columna.Name.Trim()].Value.ToString());
                                    break;

                                case "idLocacion":
                                    oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.idLocacion = Convert.ToInt32(Fila.Cells[Columna.Name.Trim()].Value.ToString());
                                    break;

                                case "idBodega":
                                    oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.oBodegaEN.idBodega = Convert.ToInt32(Fila.Cells[Columna.Name.Trim()].Value.ToString());
                                    break;

                                case "idAlmacen":
                                    oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.oBodegaEN.oAlmacenEN.idAlmacen = Convert.ToInt32(Fila.Cells[Columna.Name.Trim()].Value.ToString());
                                    break;

                                case "CodigoDelAlmacen":
                                    oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.oBodegaEN.oAlmacenEN.Codigo = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                    break;

                                case "NombreDelAlmacen":
                                    oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.oBodegaEN.oAlmacenEN.Nombre = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                    break;

                                case "CodigoDeBodega":
                                    oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.oBodegaEN.Codigo = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                    break;

                                case "NombreDeLaBodega":
                                    oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.oBodegaEN.Nombre = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                    break;

                                case "CodigoDeLocacion":
                                    oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.Codigo = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                    break;

                                case "NombreDeLaLocacion":
                                    oRegistroEN.oContenedorEN.oSeccionEN.oLocacionEN.Nombre = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                    break;

                                case "CodigoDeLaSeccion":
                                    oRegistroEN.oContenedorEN.oSeccionEN.Codigo = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                    break;

                                case "NombreDeLaSeccion":
                                    oRegistroEN.oContenedorEN.oSeccionEN.Nombre = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                    break;

                                case "CodigoDelContenedor":
                                    oRegistroEN.oContenedorEN.Codigo = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                    break;

                                case "NombreDelContenedor":
                                    oRegistroEN.oContenedorEN.Nombre = Fila.Cells[Columna.Name.Trim()].Value.ToString();
                                    break;


                            }

                        }

                        oLocalizacionDelProductoEN[a - 1] = oRegistroEN;

                    }
                }

                this.Cursor = Cursors.Default;
                this.Close();
            }
        }

        private void dgvLista_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo Hitest = dgvLista.HitTest(e.X, e.Y);

                if (Hitest.Type == DataGridViewHitTestType.Cell)
                {
                    dgvLista.CurrentCell = dgvLista.Rows[Hitest.RowIndex].Cells[Hitest.ColumnIndex];
                }

            }
        }

        private void tsbMarcarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                tsbMarcarTodos.Checked = !tsbMarcarTodos.Checked;

                if (tsbMarcarTodos.Checked == true)
                {
                    tsbMarcarTodos.Image = Properties.Resources.unchecked16x16;
                }
                else
                {
                    tsbMarcarTodos.Image = Properties.Resources.checked16x16;
                }

                foreach (DataGridViewRow Fila in dgvLista.Rows)
                {
                    Fila.Cells["Seleccionar"].Value = true;
                    //Si se llamo a la interfaz para seleccionar un solo registro, despues de marcar el primero, llamamos al que desmarca y terminamos
                    if (VariosRegistros == false)
                    {
                        DesmarcarFilas(Fila.Index);
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al marcar filas. \n" + ex.Message, "Marcar todas las filas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                this.Cursor = Cursors.Default;
            }
        }

        private void frmLocalizacionDelProducto_KeyUp(object sender, KeyEventArgs e)
        {
           
            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.F5))
            {
                tsbFiltrar_Click(null, null);
            }            

        }
        
        private void mcsMenu_Opened(object sender, EventArgs e)
        {

        }

        private void txtIdentificador_KeyUp(object sender, KeyEventArgs e)
        {
            if (Controles.IsNullOEmptyElControl(txtIdentificador))
            {
                chkIdentificador.CheckState = CheckState.Unchecked;
            }
            else { chkIdentificador.CheckState = CheckState.Checked; }

            if (chkIdentificador.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {                
                LLenarListado();
            }
        }
        
        private void tsbFiltroAutomatico_Click(object sender, EventArgs e)
        {
            tsbFiltroAutomatico.Checked = !tsbFiltroAutomatico.Checked;            

            if (tsbFiltroAutomatico.Checked == true)
            {
                tsbFiltroAutomatico.Image = Properties.Resources.unchecked16x16;
            }
            else {
                tsbFiltroAutomatico.Image = Properties.Resources.checked16x16;
            }
            
        }

        private void txtCodigoDeAlmacenaje_KeyUp(object sender, KeyEventArgs e)
        {
            if (Controles.IsNullOEmptyElControl(txtCodigoDeAlmacenaje))
            {
                chkCodigoDeAlmacenaje.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkCodigoDeAlmacenaje.CheckState = CheckState.Checked;
            }

            if (chkCodigoDeAlmacenaje.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {
                LLenarListado();
            }

        }

        private void CodigoDeLaBodega_KeyUp(object sender, KeyEventArgs e)
        {
            if (Controles.IsNullOEmptyElControl(txtCodigoDeLaBodega))
            {
                chkCodigoDeLaBodega.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkCodigoDeLaBodega.CheckState = CheckState.Checked;
            }

            if (chkCodigoDeLaBodega.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {
                LLenarListado();
            }
        }

        private void txtCodigoDeSeccion_KeyUp(object sender, KeyEventArgs e)
        {
            if (Controles.IsNullOEmptyElControl(txtCodigoDeSeccion))
            {
                chkCodigoSeccion.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkCodigoSeccion.CheckState = CheckState.Checked;
            }

            if (chkCodigoSeccion.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {
                LLenarListado();
            }
        }

        private void txtCodigoDelAlmacen_KeyUp(object sender, KeyEventArgs e)
        {
            if (Controles.IsNullOEmptyElControl(txtCodigoDelAlmacen))
            {
                chkCodigoDelAmacen.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkCodigoDelAmacen.CheckState = CheckState.Checked;
            }

            if (chkCodigoDelAmacen.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {
                LLenarListado();
            }
        }

        private void txtCodigoLocacion_KeyUp(object sender, KeyEventArgs e)
        {
            if (Controles.IsNullOEmptyElControl(txtCodigoLocacion))
            {
                chkCodigoLocacion.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkCodigoLocacion.CheckState = CheckState.Checked;
            }

            if (chkCodigoLocacion.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {
                LLenarListado();
            }
        }

        private void txtCodigoDelContenedor_KeyUp(object sender, KeyEventArgs e)
        {
            if (Controles.IsNullOEmptyElControl(txtCodigoDelContenedor))
            {
                chkCodigoDelContenedor.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkCodigoDelContenedor.CheckState = CheckState.Checked;
            }

            if (chkCodigoDelContenedor.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {
                LLenarListado();
            }
        }

        private void cmbAlmacen_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbAlmacen.SelectedIndex == -1)
            {
                chkAlmacen.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkAlmacen.CheckState = CheckState.Checked;
            }

            if (chkAlmacen.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {
                LLenarListado();
            }

            LlenarComboBodega();

        }

        private void cmbBodega_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbBodega.SelectedIndex == -1)
            {
                chkBodega.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkBodega.CheckState = CheckState.Checked;
            }

            if (chkBodega.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {
                LLenarListado();
            }

            LLenarComboLocacion();
        }

        private void cmbLocacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbLocacion.SelectedIndex == -1)
            {
                chkLocacion.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkLocacion.CheckState = CheckState.Checked;
            }

            if (chkLocacion.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {
                LLenarListado();
            }

            LlenarComboSeccion();
        }

        private void cmbSeccion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbSeccion.SelectedIndex == -1)
            {
                chkSeccion.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkSeccion.CheckState = CheckState.Checked;
            }

            if (chkSeccion.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {
                LLenarListado();
            }

            LlenarComboContenedor();
        }

        private void cmbContenedor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbContenedor.SelectedIndex == -1)
            {
                chkNombreDelContenedor.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkNombreDelContenedor.CheckState = CheckState.Checked;
            }

            if (chkNombreDelContenedor.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {
                LLenarListado();
            }
        }
        
        private void chkCodigoDeAlmacenaje_Click(object sender, EventArgs e)
        {
            LLenarListado();
        }

        private void chkCodigoDelAmacen_Click(object sender, EventArgs e)
        {
            LLenarListado();
        }

        private void chkAlmacen_Click(object sender, EventArgs e)
        {
            LLenarListado();
        }

        private void chkCodigoDeLaBodega_Click(object sender, EventArgs e)
        {
            LLenarListado();
        }

        private void chkBodega_Click(object sender, EventArgs e)
        {
            LLenarListado();
        }

        private void chkCodigoLocacion_Click(object sender, EventArgs e)
        {
            LLenarListado();
        }

        private void chkLocacion_Click(object sender, EventArgs e)
        {
            LLenarListado();
        }

        private void chkCodigoSeccion_Click(object sender, EventArgs e)
        {
            LLenarListado();
        }

        private void chkSeccion_Click(object sender, EventArgs e)
        {
            LLenarListado();
        }

        private void chkCodigoDelContenedor_Click(object sender, EventArgs e)
        {
            LLenarListado();
        }

        private void chkNombreDelContenedor_Click(object sender, EventArgs e)
        {
            LLenarListado();
        }

        #endregion

    }
}
