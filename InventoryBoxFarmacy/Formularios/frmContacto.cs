﻿using System;
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
    public partial class frmContacto : Form
    {
        public frmContacto()
        {
            InitializeComponent();
        }

        private string NOMBRE_ENTIDAD_PRIVILEGIO = "Contacto";
        private string NOMBRE_ENTIDAD = "Administrar los contactos del sistema";
        private string NOMBRE_LLAVE_PRIMARIA = "idContacto";
        private int ValorLlavePrimariaEntidad;
        private int IndiceSeleccionado;

        #region "Funciones del programador"

        public bool ActivarFiltros { set; get; }
        public bool VariosRegistros { set; get; }
        public string TituloVentana { set; get; }
        public ContactoEN[] oContacto = new ContactoEN[0];

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
        /// <summary>
        /// Nos permite agregar un filtro de fuente externa a la ventana
        /// </summary>
        public bool AplicarFiltroDeWhereExterno { set; get; }

        public string WhereExterno { set; get; }

        private void ActivarFiltrosDelaBusqueda()
        {
            if (ActivarFiltros == false)
            {
                
                tsbFiltrar.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbNuevoRegistro.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                tsbImprimir.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
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
                    tsbNuevoRegistro.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    tsbImprimir.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
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

                    if (tsbNuevoRegistro.Enabled == true)
                    {
                        tsbNuevoRegistro.Visible = Activar_btn_Nuevo;
                    }
                    if (tsbImprimir.Visible == true)
                        tsbImprimir.Visible = Activar_btn_imprimir;

                    mcsMenu.Enabled = Activar_MenuContextual;
                    AgregrarColumnasAlDTRegistros();

                }
            }
        }

        private void AgregarConfigurarcionAcombo()
        {
            try
            {

                AutoCompleteStringCollection oSDatos = new AutoCompleteStringCollection();                
                oSDatos.Add("Teléfono");
                oSDatos.Add("Movil");
                oSDatos.Add("Observaciones");
                oSDatos.Add("Correo Electronico");
                oSDatos.Add("Fecha Festiva");
                oSDatos.Add("Messenger");
                oSDatos.Add("Skype");
                oSDatos.Add("Twitter");
                oSDatos.Add("Facebook");
                oSDatos.Add("Sexo");

                cmbFiltros.Items.Add("Teléfono");
                cmbFiltros.Items.Add("Movil");
                cmbFiltros.Items.Add("Observaciones");
                cmbFiltros.Items.Add("Correo Electronico");
                cmbFiltros.Items.Add("Fecha Festiva");
                cmbFiltros.Items.Add("Messenger");
                cmbFiltros.Items.Add("Skype");
                cmbFiltros.Items.Add("Twitter");
                cmbFiltros.Items.Add("Facebook");
                cmbFiltros.Items.Add("Sexo");

                cmbFiltros.AutoCompleteCustomSource = oSDatos;
                cmbFiltros.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbFiltros.AutoCompleteSource = AutoCompleteSource.CustomSource;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Agregar filtros al combo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            if (oContacto.Length > 0)
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

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(oContacto.GetType());
            System.IO.StringWriter sw = new System.IO.StringWriter();
            serializer.Serialize(sw, oContacto);

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

            if (Controles.IsNullOEmptyElControl(chkCodigo) == false && Controles.IsNullOEmptyElControl(txtCodigo) == false)
            {
                Where += string.Format(" and Codigo like '%{0}%' ", txtCodigo.Text.Trim());
            }

            if (Controles.IsNullOEmptyElControl(chkProveedor) == false && Controles.IsNullOEmptyElControl(txtContacto) == false)
            {
                Where += string.Format(" and Nombre like '%{0}%' ", txtContacto.Text.Trim());
            }

            if (Controles.IsNullOEmptyElControl(chkDireccion) == false && Controles.IsNullOEmptyElControl(txtDireccion) == false)
            {
                Where += string.Format(" and Direccion like '%{0}%' ", txtDireccion.Text.Trim());
            }

            if(AplicarFiltroDeWhereExterno == true)
            {
                Where += WhereExterno;
            }

            Where += MasWhereDinamico();

            return Where;

        }

        private string MasWhereDinamico()
        {
            try
            {

                string Where = "";

                string Columna = cmbFiltros.Text.Trim().ToUpper();
                switch (Columna)
                {
                   
                    case "SITIO WEB":
                        if (Controles.IsNullOEmptyElControl(txtBusqueda) == false)
                        {
                            Where = string.Format(" and SitioWeb like '%{0}%' ", txtBusqueda.Text);
                        }
                        break;

                    case "TELÉFONO":
                        if (Controles.IsNullOEmptyElControl(txtBusqueda) == false)
                        {
                            Where = string.Format(" and Telefono like '%{0}%' ", txtBusqueda.Text);
                        }
                        break;

                    case "MOVIL":
                        if (Controles.IsNullOEmptyElControl(txtBusqueda) == false)
                        {
                            Where = string.Format(" and Movil like '%{0}%' ", txtBusqueda.Text);
                        }
                        break;

                    case "OBSERVACIONES":
                        if (Controles.IsNullOEmptyElControl(txtBusqueda) == false)
                        {
                            Where = string.Format(" and Observaciones like '%{0}%' ", txtBusqueda.Text);
                        }
                        break;

                    case "CORREO ELECTRONICO":
                        if (Controles.IsNullOEmptyElControl(txtBusqueda) == false)
                        {
                            Where = string.Format(" and Correo like '%{0}%' ", txtBusqueda.Text);
                        }
                        break;

                    case "FECHA FESTIVA":
                        if (Controles.IsNullOEmptyElControl(txtBusqueda) == false)
                        {
                            Where = string.Format(" and FechaDeCumpleanos like '%{0}%' ", txtBusqueda.Text);
                        }
                        break;

                    case "MESSENGER":
                        if (Controles.IsNullOEmptyElControl(txtBusqueda) == false)
                        {
                            Where = string.Format(" and Messenger like '%{0}%' ", txtBusqueda.Text);
                        }
                        break;

                    case "SKYPE":
                        if (Controles.IsNullOEmptyElControl(txtBusqueda) == false)
                        {
                            Where = string.Format(" and Skype like '%{0}%' ", txtBusqueda.Text);
                        }
                        break;

                    case "TWITTER":
                        if (Controles.IsNullOEmptyElControl(txtBusqueda) == false)
                        {
                            Where = string.Format(" and Twitter like '%{0}%' ", txtBusqueda.Text);
                        }
                        break;

                    case "FACEBOOK":
                        if (Controles.IsNullOEmptyElControl(txtBusqueda) == false)
                        {
                            Where = string.Format(" and Facebook like '%{0}%' ", txtBusqueda.Text);
                        }
                        break;

                    case "SEXO":
                        if (Controles.IsNullOEmptyElControl(txtBusqueda) == false)
                        {
                            Where = string.Format(" and Sexo like '%{0}%' ", txtBusqueda.Text);
                        }
                        break;


                }

                return Where;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Where Dinamico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "";
            }
        }

        private string TituloDinamico()
        {

            string Titulo = "";

            if (Controles.IsNullOEmptyElControl(chkCodigo) == false && Controles.IsNullOEmptyElControl(txtCodigo) == false)
            {
                Titulo += string.Format(" Código: '{0}', ", txtCodigo.Text.Trim());
            }

            if (Controles.IsNullOEmptyElControl(chkProveedor) == false && Controles.IsNullOEmptyElControl(txtContacto) == false)
            {
                Titulo += string.Format(" Contacto: '{0}', ", txtContacto.Text.Trim());
            }

            if (Controles.IsNullOEmptyElControl(chkDireccion) == false && Controles.IsNullOEmptyElControl(txtDireccion) == false)
            {
                Titulo += string.Format(" Dirección: '{0}', ", txtDireccion.Text.Trim());
            }

            Titulo += MasTituloDinamico();

            if (Titulo.Length > 0)
            {
                Titulo = Titulo.Substring(0, Titulo.Length - 2);
            }

            return Titulo;
                        

        }

        private string MasTituloDinamico()
        {
            try
            {
                string Titulo = "";

                if (Controles.IsNullOEmptyElControl(cmbFiltros) == false)
                {
                    if (Controles.IsNullOEmptyElControl(txtBusqueda) == false)
                    {
                        Titulo = string.Format(" {0}: {1}, ", cmbFiltros.Text.Trim(), txtBusqueda.Text.Trim());
                    }

                }

                return Titulo;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mas Titulo Dinamicos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "";
            }
        }

        private void LLenarListado() {

            try
            {

                this.Cursor = Cursors.WaitCursor;

                ContactoEN oRegistrosEN = new ContactoEN();
                ContactoLN oRegistrosLN = new ContactoLN();

                oRegistrosEN.Where = WhereDinamico();

                if (oRegistrosLN.Listado(oRegistrosEN, Program.oDatosDeConexion)) {

                    dgvLista.Columns.Clear();                    

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

                string OcultarColumnas = "idContacto,idUsuarioDeCreacion, FechaDeCreacion, idUsuarioModificacion, FechaDeModificacion,Foto";
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

        private void CargarPrivilegiosDelUsuario(){

            try
            {
                this.Cursor = Cursors.WaitCursor;

                ModuloInterfazUsuariosEN oRegistroEN = new ModuloInterfazUsuariosEN();
                ModuloInterfazUsuariosLN oRegistroLN = new ModuloInterfazUsuariosLN();

                oRegistroEN.oUsuarioEN.idUsuario = Program.oLoginEN.idUsuario;
                oRegistroEN.oPrivilegioEN.oModuloInterfazEN.oInterfazEN.Nombre = NOMBRE_ENTIDAD_PRIVILEGIO;

                if (oRegistroLN.ListadoPrivilegiosDelUsuariosPorIntefaz(oRegistroEN, Program.oDatosDeConexion))
                {

                    foreach (ToolStripItem item in mcsMenu.Items)
                    {
                        if (item.Tag != null)
                        {
                            if (item.GetType() == typeof(ToolStripMenuItem))
                            {
                                item.Enabled = oRegistroLN.VerificarSiTengoAcceso(item.Tag.ToString());
                            }
                        }
                    }

                    tsbImprimir.Enabled = oRegistroLN.VerificarSiTengoAcceso("Imprimir");
                    tsbNuevoRegistro.Enabled = oRegistroLN.VerificarSiTengoAcceso("Nuevo");

                }
                else
                {

                    mcsMenu.Enabled = false;
                    tsbImprimir.Enabled = false;
                    tsbNuevoRegistro.Enabled = false;
                    throw new ArgumentException(oRegistroLN.Error);

                }

                oRegistroEN = null;
                oRegistroLN = null;

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Privilegios de Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void MostrarFormularioParaOperacion(string OperacionesARealizar)
        {

            frmContactoOperacion ofrmContactoOperacion = new frmContactoOperacion();
            ofrmContactoOperacion.OperacionARealizar = OperacionesARealizar;
            ofrmContactoOperacion.NOMBRE_ENTIDAD_PRIVILEGIO = NOMBRE_ENTIDAD_PRIVILEGIO;
            ofrmContactoOperacion.NombreEntidad = NOMBRE_ENTIDAD;
            ofrmContactoOperacion.ValorLlavePrimariaEntidad = this.ValorLlavePrimariaEntidad;
            ofrmContactoOperacion.MdiParent = this.ParentForm;
            ofrmContactoOperacion.Show();

        }

        private void AsignarLlavePrimaria()
        {
            this.ValorLlavePrimariaEntidad = Convert.ToInt32(this.dgvLista.Rows[this.IndiceSeleccionado].Cells[this.NOMBRE_LLAVE_PRIMARIA].Value);
        }

        #endregion

        private void frmContacto_Shown(object sender, EventArgs e)
        {
            AgregarConfigurarcionAcombo();
            dgvLista.ContextMenuStrip = mcsMenu;
            CargarPrivilegiosDelUsuario();

            ActivarFiltrosDelaBusqueda();
            tsbFiltroAutomatico_Click(null, null);
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
                            Array.Resize(ref oContacto, a);
                            
                            oContacto[a - 1] = new ContactoEN();
                            oContacto[a - 1].idContacto = Convert.ToInt32(Fila.Cells["idContacto"].Value);
                            oContacto[a - 1].Codigo = Fila.Cells["Codigo"].Value.ToString();
                            oContacto[a - 1].Nombre = Fila.Cells["Nombre"].Value.ToString();
                            oContacto[a - 1].Direccion = Fila.Cells["Direccion"].Value.ToString();
                            oContacto[a - 1].Telefono = Fila.Cells["Telefono"].Value.ToString();
                            oContacto[a - 1].Movil = Fila.Cells["Movil"].Value.ToString();
                            oContacto[a - 1].Observaciones = Fila.Cells["Observaciones"].Value.ToString();
                            oContacto[a - 1].Correo = Fila.Cells["Correo"].Value.ToString();
                            oContacto[a - 1].FechaDeCumpleanos = Fila.Cells["FechaDeCumpleanos"].Value.ToString();
                            oContacto[a - 1].Messenger = Fila.Cells["Messenger"].Value.ToString();
                            oContacto[a - 1].Skype = Fila.Cells["Skype"].Value.ToString();
                            oContacto[a - 1].Twitter = Fila.Cells["Twitter"].Value.ToString();
                            oContacto[a - 1].Facebook = Fila.Cells["Facebook"].Value.ToString();
                            oContacto[a - 1].Estado = Fila.Cells["Estado"].Value.ToString();
                            oContacto[a - 1].Sexo = Fila.Cells["Sexo"].Value.ToString();
                            oContacto[a - 1].Cedula = Fila.Cells["Cedula"].Value.ToString();

                        }
                    }
                }
                                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Seleccionar registros", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                AgregarRegistrosAlDTUsuario();
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
                        Array.Resize(ref oContacto, a);

                        oContacto[a - 1] = new ContactoEN();
                        oContacto[a - 1].idContacto = Convert.ToInt32(Fila.Cells["idContacto"].Value);
                        oContacto[a - 1].Codigo = Fila.Cells["Codigo"].Value.ToString();
                        oContacto[a - 1].Nombre = Fila.Cells["Nombre"].Value.ToString();
                        oContacto[a - 1].Direccion = Fila.Cells["Direccion"].Value.ToString();
                        oContacto[a - 1].Telefono = Fila.Cells["Telefono"].Value.ToString();
                        oContacto[a - 1].Movil = Fila.Cells["Movil"].Value.ToString();
                        oContacto[a - 1].Observaciones = Fila.Cells["Observaciones"].Value.ToString();
                        oContacto[a - 1].Correo = Fila.Cells["Correo"].Value.ToString();
                        oContacto[a - 1].FechaDeCumpleanos = Fila.Cells["FechaDeCumpleanos"].Value.ToString();
                        oContacto[a - 1].Messenger = Fila.Cells["Messenger"].Value.ToString();
                        oContacto[a - 1].Skype = Fila.Cells["Skype"].Value.ToString();
                        oContacto[a - 1].Twitter = Fila.Cells["Twitter"].Value.ToString();
                        oContacto[a - 1].Facebook = Fila.Cells["Facebook"].Value.ToString();
                        oContacto[a - 1].Estado = Fila.Cells["Estado"].Value.ToString();
                        oContacto[a - 1].Sexo = Fila.Cells["Sexo"].Value.ToString();
                        oContacto[a - 1].Cedula = Fila.Cells["Cedula"].Value.ToString();

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

        private void frmContacto_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.F2) && nuevoToolStripMenuItem.Enabled == true)
            {
                nuevoToolStripMenuItem_Click(null, null);
            }

            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.F3) && actualizarToolStripMenuItem.Enabled == true)
            {
                if (dgvLista.SelectedRows.Count > 0)
                {
                    IndiceSeleccionado = dgvLista.CurrentRow.Index;
                    actualizarToolStripMenuItem_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Se debe de seleccionar un registro de la lista", "Actualizar Registro ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.F4) && eliminarToolStripMenuItem.Enabled == true)
            {
                if (dgvLista.SelectedRows.Count > 0)
                {
                    IndiceSeleccionado = dgvLista.CurrentRow.Index;
                    eliminarToolStripMenuItem_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Se debe de seleccionar un registro de la lista", "Eliminar Registro ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.F5))
            {
                tsbFiltrar_Click(null, null);
            }

            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.F6) && visualizarToolStripMenuItem.Enabled == true && dgvLista.SelectedRows.Count > 0)
            {
                if (dgvLista.SelectedRows.Count > 0)
                {
                    IndiceSeleccionado = dgvLista.CurrentRow.Index;
                    visualizarToolStripMenuItem_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Se debe de seleccionar un registro de la lista", "Consultar Registro ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
                        

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarFormularioParaOperacion("Nuevo");
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsignarLlavePrimaria();
            MostrarFormularioParaOperacion("Modificar");
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsignarLlavePrimaria();
            MostrarFormularioParaOperacion("Eliminar");
        }

        private void visualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AsignarLlavePrimaria();
            MostrarFormularioParaOperacion("Consultar");
        }

        private void mcsMenu_Opened(object sender, EventArgs e)
        {
            if (dgvLista.DataSource == null || dgvLista.Rows.Count <= 0 || dgvLista.SelectedRows.Count <= 0)
            {
                eliminarToolStripMenuItem.Enabled = false;
                actualizarToolStripMenuItem.Enabled = false;
                visualizarToolStripMenuItem.Enabled = false;
                imprimirToolStripMenuItem.Enabled = false;                
            }
            else
            {
                CargarPrivilegiosDelUsuario();

            }
        }
        
        private void tsbNuevoRegistro_Click(object sender, EventArgs e)
        {
            MostrarFormularioParaOperacion("Nuevo");
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

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void txtBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            if (Controles.IsNullOEmptyElControl(txtBusqueda))
            {
                chkOtro.CheckState = CheckState.Unchecked;
            }
            else { chkOtro.CheckState = CheckState.Checked; }

            if (chkOtro.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {
                LLenarListado();
            }
        }

        private void txtDireccion_KeyUp(object sender, KeyEventArgs e)
        {
            if (Controles.IsNullOEmptyElControl(txtDireccion))
            {
                chkDireccion.CheckState = CheckState.Unchecked;
            }
            else { chkDireccion.CheckState = CheckState.Checked; }

            if (chkDireccion.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {
                LLenarListado();
            }
        }

        private void txtContador_KeyUp(object sender, KeyEventArgs e)
        {
            if (Controles.IsNullOEmptyElControl(txtContacto))
            {
                chkProveedor.CheckState = CheckState.Unchecked;
            }
            else { chkProveedor.CheckState = CheckState.Checked; }

            if (chkProveedor.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {
                LLenarListado();
            }
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            if (Controles.IsNullOEmptyElControl(txtCodigo))
            {
                chkCodigo.CheckState = CheckState.Unchecked;
            }
            else { chkCodigo.CheckState = CheckState.Checked; }

            if (chkCodigo.CheckState == CheckState.Checked && tsbFiltroAutomatico.CheckState == CheckState.Checked)
            {
                LLenarListado();
            }
        }
    }
}
