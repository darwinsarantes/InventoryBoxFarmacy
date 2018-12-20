namespace InventoryBoxFarmacy.Formularios
{
    partial class frmLocalizacionDelProducto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbAlmacenaje = new System.Windows.Forms.GroupBox();
            this.txtCodigoDeAlmacenaje = new System.Windows.Forms.TextBox();
            this.chkCodigoDeAlmacenaje = new System.Windows.Forms.CheckBox();
            this.gbAlmacen = new System.Windows.Forms.GroupBox();
            this.txtCodigoDelAlmacen = new System.Windows.Forms.TextBox();
            this.cmbAlmacen = new System.Windows.Forms.ComboBox();
            this.chkIdentificador = new System.Windows.Forms.CheckBox();
            this.txtIdentificador = new System.Windows.Forms.TextBox();
            this.chkCodigoDelAmacen = new System.Windows.Forms.CheckBox();
            this.chkAlmacen = new System.Windows.Forms.CheckBox();
            this.gbBodega = new System.Windows.Forms.GroupBox();
            this.txtCodigoDeLaBodega = new System.Windows.Forms.TextBox();
            this.cmbBodega = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.chkCodigoDeLaBodega = new System.Windows.Forms.CheckBox();
            this.chkBodega = new System.Windows.Forms.CheckBox();
            this.gbLocacion = new System.Windows.Forms.GroupBox();
            this.txtCodigoLocacion = new System.Windows.Forms.TextBox();
            this.cmbLocacion = new System.Windows.Forms.ComboBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.chkCodigoLocacion = new System.Windows.Forms.CheckBox();
            this.chkLocacion = new System.Windows.Forms.CheckBox();
            this.gbSeccion = new System.Windows.Forms.GroupBox();
            this.txtCodigoDeSeccion = new System.Windows.Forms.TextBox();
            this.cmbSeccion = new System.Windows.Forms.ComboBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.chkCodigoSeccion = new System.Windows.Forms.CheckBox();
            this.chkSeccion = new System.Windows.Forms.CheckBox();
            this.gbContenedor = new System.Windows.Forms.GroupBox();
            this.txtCodigoDelContenedor = new System.Windows.Forms.TextBox();
            this.cmbContenedor = new System.Windows.Forms.ComboBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.chkCodigoDelContenedor = new System.Windows.Forms.CheckBox();
            this.chkNombreDelContenedor = new System.Windows.Forms.CheckBox();
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsbNoRegistros = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbFiltrar = new System.Windows.Forms.ToolStripButton();
            this.tsbFiltroAutomatico = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMarcarTodos = new System.Windows.Forms.ToolStripButton();
            this.tsbSeleccionarTodos = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbAlmacenaje.SuspendLayout();
            this.gbAlmacen.SuspendLayout();
            this.gbBodega.SuspendLayout();
            this.gbLocacion.SuspendLayout();
            this.gbSeccion.SuspendLayout();
            this.gbContenedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvLista);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel2.Controls.Add(this.tsMenu);
            this.splitContainer1.Size = new System.Drawing.Size(886, 533);
            this.splitContainer1.SplitterDistance = 127;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(862, 104);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información para la ubicación o locación del producto de manera física.";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.gbAlmacenaje);
            this.flowLayoutPanel1.Controls.Add(this.gbAlmacen);
            this.flowLayoutPanel1.Controls.Add(this.gbBodega);
            this.flowLayoutPanel1.Controls.Add(this.gbLocacion);
            this.flowLayoutPanel1.Controls.Add(this.gbSeccion);
            this.flowLayoutPanel1.Controls.Add(this.gbContenedor);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(856, 85);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // gbAlmacenaje
            // 
            this.gbAlmacenaje.Controls.Add(this.txtCodigoDeAlmacenaje);
            this.gbAlmacenaje.Controls.Add(this.chkCodigoDeAlmacenaje);
            this.gbAlmacenaje.Location = new System.Drawing.Point(3, 3);
            this.gbAlmacenaje.Name = "gbAlmacenaje";
            this.gbAlmacenaje.Size = new System.Drawing.Size(412, 78);
            this.gbAlmacenaje.TabIndex = 0;
            this.gbAlmacenaje.TabStop = false;
            this.gbAlmacenaje.Text = "Buscar por codigo de Almacenaje:";
            // 
            // txtCodigoDeAlmacenaje
            // 
            this.txtCodigoDeAlmacenaje.Location = new System.Drawing.Point(154, 32);
            this.txtCodigoDeAlmacenaje.Name = "txtCodigoDeAlmacenaje";
            this.txtCodigoDeAlmacenaje.Size = new System.Drawing.Size(238, 20);
            this.txtCodigoDeAlmacenaje.TabIndex = 1;
            this.txtCodigoDeAlmacenaje.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigoDeAlmacenaje_KeyUp);
            // 
            // chkCodigoDeAlmacenaje
            // 
            this.chkCodigoDeAlmacenaje.Location = new System.Drawing.Point(9, 32);
            this.chkCodigoDeAlmacenaje.Name = "chkCodigoDeAlmacenaje";
            this.chkCodigoDeAlmacenaje.Size = new System.Drawing.Size(139, 20);
            this.chkCodigoDeAlmacenaje.TabIndex = 0;
            this.chkCodigoDeAlmacenaje.Text = "Código de almacenaje:";
            this.chkCodigoDeAlmacenaje.UseVisualStyleBackColor = true;
            this.chkCodigoDeAlmacenaje.Click += new System.EventHandler(this.chkCodigoDeAlmacenaje_Click);
            // 
            // gbAlmacen
            // 
            this.gbAlmacen.Controls.Add(this.txtCodigoDelAlmacen);
            this.gbAlmacen.Controls.Add(this.cmbAlmacen);
            this.gbAlmacen.Controls.Add(this.chkIdentificador);
            this.gbAlmacen.Controls.Add(this.txtIdentificador);
            this.gbAlmacen.Controls.Add(this.chkCodigoDelAmacen);
            this.gbAlmacen.Controls.Add(this.chkAlmacen);
            this.gbAlmacen.Location = new System.Drawing.Point(421, 3);
            this.gbAlmacen.Name = "gbAlmacen";
            this.gbAlmacen.Size = new System.Drawing.Size(412, 78);
            this.gbAlmacen.TabIndex = 0;
            this.gbAlmacen.TabStop = false;
            this.gbAlmacen.Text = "Almacen:";
            // 
            // txtCodigoDelAlmacen
            // 
            this.txtCodigoDelAlmacen.Location = new System.Drawing.Point(140, 19);
            this.txtCodigoDelAlmacen.Name = "txtCodigoDelAlmacen";
            this.txtCodigoDelAlmacen.Size = new System.Drawing.Size(252, 20);
            this.txtCodigoDelAlmacen.TabIndex = 1;
            this.txtCodigoDelAlmacen.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigoDelAlmacen_KeyUp);
            // 
            // cmbAlmacen
            // 
            this.cmbAlmacen.FormattingEnabled = true;
            this.cmbAlmacen.Location = new System.Drawing.Point(140, 45);
            this.cmbAlmacen.Name = "cmbAlmacen";
            this.cmbAlmacen.Size = new System.Drawing.Size(252, 21);
            this.cmbAlmacen.TabIndex = 2;
            this.cmbAlmacen.SelectionChangeCommitted += new System.EventHandler(this.cmbAlmacen_SelectionChangeCommitted);
            // 
            // chkIdentificador
            // 
            this.chkIdentificador.Location = new System.Drawing.Point(208, 16);
            this.chkIdentificador.Name = "chkIdentificador";
            this.chkIdentificador.Size = new System.Drawing.Size(39, 24);
            this.chkIdentificador.TabIndex = 0;
            this.chkIdentificador.Text = "Identificador:";
            this.chkIdentificador.UseVisualStyleBackColor = true;
            this.chkIdentificador.Visible = false;
            // 
            // txtIdentificador
            // 
            this.txtIdentificador.Location = new System.Drawing.Point(230, 19);
            this.txtIdentificador.Name = "txtIdentificador";
            this.txtIdentificador.Size = new System.Drawing.Size(40, 20);
            this.txtIdentificador.TabIndex = 1;
            this.txtIdentificador.Visible = false;
            this.txtIdentificador.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtIdentificador_KeyUp);
            // 
            // chkCodigoDelAmacen
            // 
            this.chkCodigoDelAmacen.Location = new System.Drawing.Point(9, 19);
            this.chkCodigoDelAmacen.Name = "chkCodigoDelAmacen";
            this.chkCodigoDelAmacen.Size = new System.Drawing.Size(139, 20);
            this.chkCodigoDelAmacen.TabIndex = 0;
            this.chkCodigoDelAmacen.Text = "Código del Álmacen:";
            this.chkCodigoDelAmacen.UseVisualStyleBackColor = true;
            this.chkCodigoDelAmacen.Click += new System.EventHandler(this.chkCodigoDelAmacen_Click);
            // 
            // chkAlmacen
            // 
            this.chkAlmacen.Location = new System.Drawing.Point(9, 46);
            this.chkAlmacen.Name = "chkAlmacen";
            this.chkAlmacen.Size = new System.Drawing.Size(139, 18);
            this.chkAlmacen.TabIndex = 0;
            this.chkAlmacen.Text = "Nombre del Álmacen:";
            this.chkAlmacen.UseVisualStyleBackColor = true;
            this.chkAlmacen.Click += new System.EventHandler(this.chkAlmacen_Click);
            // 
            // gbBodega
            // 
            this.gbBodega.Controls.Add(this.txtCodigoDeLaBodega);
            this.gbBodega.Controls.Add(this.cmbBodega);
            this.gbBodega.Controls.Add(this.checkBox1);
            this.gbBodega.Controls.Add(this.textBox2);
            this.gbBodega.Controls.Add(this.chkCodigoDeLaBodega);
            this.gbBodega.Controls.Add(this.chkBodega);
            this.gbBodega.Location = new System.Drawing.Point(3, 87);
            this.gbBodega.Name = "gbBodega";
            this.gbBodega.Size = new System.Drawing.Size(412, 78);
            this.gbBodega.TabIndex = 0;
            this.gbBodega.TabStop = false;
            this.gbBodega.Text = "Bodega:";
            // 
            // txtCodigoDeLaBodega
            // 
            this.txtCodigoDeLaBodega.Location = new System.Drawing.Point(154, 19);
            this.txtCodigoDeLaBodega.Name = "txtCodigoDeLaBodega";
            this.txtCodigoDeLaBodega.Size = new System.Drawing.Size(238, 20);
            this.txtCodigoDeLaBodega.TabIndex = 1;
            this.txtCodigoDeLaBodega.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CodigoDeLaBodega_KeyUp);
            // 
            // cmbBodega
            // 
            this.cmbBodega.FormattingEnabled = true;
            this.cmbBodega.Location = new System.Drawing.Point(154, 45);
            this.cmbBodega.Name = "cmbBodega";
            this.cmbBodega.Size = new System.Drawing.Size(238, 21);
            this.cmbBodega.TabIndex = 2;
            this.cmbBodega.SelectionChangeCommitted += new System.EventHandler(this.cmbBodega_SelectionChangeCommitted);
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(208, 16);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(39, 24);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Identificador:";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(230, 19);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(40, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.Visible = false;
            this.textBox2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtIdentificador_KeyUp);
            // 
            // chkCodigoDeLaBodega
            // 
            this.chkCodigoDeLaBodega.Location = new System.Drawing.Point(9, 19);
            this.chkCodigoDeLaBodega.Name = "chkCodigoDeLaBodega";
            this.chkCodigoDeLaBodega.Size = new System.Drawing.Size(139, 20);
            this.chkCodigoDeLaBodega.TabIndex = 0;
            this.chkCodigoDeLaBodega.Text = "Código de la Bodega";
            this.chkCodigoDeLaBodega.UseVisualStyleBackColor = true;
            this.chkCodigoDeLaBodega.Click += new System.EventHandler(this.chkCodigoDeLaBodega_Click);
            // 
            // chkBodega
            // 
            this.chkBodega.Location = new System.Drawing.Point(9, 46);
            this.chkBodega.Name = "chkBodega";
            this.chkBodega.Size = new System.Drawing.Size(139, 18);
            this.chkBodega.TabIndex = 0;
            this.chkBodega.Text = "Nombre de la Bodega:";
            this.chkBodega.UseVisualStyleBackColor = true;
            this.chkBodega.Click += new System.EventHandler(this.chkBodega_Click);
            // 
            // gbLocacion
            // 
            this.gbLocacion.Controls.Add(this.txtCodigoLocacion);
            this.gbLocacion.Controls.Add(this.cmbLocacion);
            this.gbLocacion.Controls.Add(this.checkBox3);
            this.gbLocacion.Controls.Add(this.textBox5);
            this.gbLocacion.Controls.Add(this.chkCodigoLocacion);
            this.gbLocacion.Controls.Add(this.chkLocacion);
            this.gbLocacion.Location = new System.Drawing.Point(421, 87);
            this.gbLocacion.Name = "gbLocacion";
            this.gbLocacion.Size = new System.Drawing.Size(412, 78);
            this.gbLocacion.TabIndex = 0;
            this.gbLocacion.TabStop = false;
            this.gbLocacion.Text = "Estante/Bodega/Vitrina/Caja";
            // 
            // txtCodigoLocacion
            // 
            this.txtCodigoLocacion.Location = new System.Drawing.Point(154, 19);
            this.txtCodigoLocacion.Name = "txtCodigoLocacion";
            this.txtCodigoLocacion.Size = new System.Drawing.Size(238, 20);
            this.txtCodigoLocacion.TabIndex = 1;
            this.txtCodigoLocacion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigoLocacion_KeyUp);
            // 
            // cmbLocacion
            // 
            this.cmbLocacion.FormattingEnabled = true;
            this.cmbLocacion.Location = new System.Drawing.Point(154, 45);
            this.cmbLocacion.Name = "cmbLocacion";
            this.cmbLocacion.Size = new System.Drawing.Size(238, 21);
            this.cmbLocacion.TabIndex = 2;
            this.cmbLocacion.SelectionChangeCommitted += new System.EventHandler(this.cmbLocacion_SelectionChangeCommitted);
            // 
            // checkBox3
            // 
            this.checkBox3.Location = new System.Drawing.Point(208, 16);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(39, 24);
            this.checkBox3.TabIndex = 0;
            this.checkBox3.Text = "Identificador:";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.Visible = false;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(230, 19);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(40, 20);
            this.textBox5.TabIndex = 1;
            this.textBox5.Visible = false;
            this.textBox5.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtIdentificador_KeyUp);
            // 
            // chkCodigoLocacion
            // 
            this.chkCodigoLocacion.Location = new System.Drawing.Point(9, 19);
            this.chkCodigoLocacion.Name = "chkCodigoLocacion";
            this.chkCodigoLocacion.Size = new System.Drawing.Size(139, 20);
            this.chkCodigoLocacion.TabIndex = 0;
            this.chkCodigoLocacion.Text = "Código:";
            this.chkCodigoLocacion.UseVisualStyleBackColor = true;
            this.chkCodigoLocacion.Click += new System.EventHandler(this.chkCodigoLocacion_Click);
            // 
            // chkLocacion
            // 
            this.chkLocacion.Location = new System.Drawing.Point(9, 46);
            this.chkLocacion.Name = "chkLocacion";
            this.chkLocacion.Size = new System.Drawing.Size(139, 18);
            this.chkLocacion.TabIndex = 0;
            this.chkLocacion.Text = "Nombre:";
            this.chkLocacion.UseVisualStyleBackColor = true;
            this.chkLocacion.Click += new System.EventHandler(this.chkLocacion_Click);
            // 
            // gbSeccion
            // 
            this.gbSeccion.Controls.Add(this.txtCodigoDeSeccion);
            this.gbSeccion.Controls.Add(this.cmbSeccion);
            this.gbSeccion.Controls.Add(this.checkBox2);
            this.gbSeccion.Controls.Add(this.textBox3);
            this.gbSeccion.Controls.Add(this.chkCodigoSeccion);
            this.gbSeccion.Controls.Add(this.chkSeccion);
            this.gbSeccion.Location = new System.Drawing.Point(3, 171);
            this.gbSeccion.Name = "gbSeccion";
            this.gbSeccion.Size = new System.Drawing.Size(412, 78);
            this.gbSeccion.TabIndex = 0;
            this.gbSeccion.TabStop = false;
            this.gbSeccion.Text = "Seccion:";
            // 
            // txtCodigoDeSeccion
            // 
            this.txtCodigoDeSeccion.Location = new System.Drawing.Point(154, 19);
            this.txtCodigoDeSeccion.Name = "txtCodigoDeSeccion";
            this.txtCodigoDeSeccion.Size = new System.Drawing.Size(238, 20);
            this.txtCodigoDeSeccion.TabIndex = 1;
            this.txtCodigoDeSeccion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigoDeSeccion_KeyUp);
            // 
            // cmbSeccion
            // 
            this.cmbSeccion.FormattingEnabled = true;
            this.cmbSeccion.Location = new System.Drawing.Point(154, 45);
            this.cmbSeccion.Name = "cmbSeccion";
            this.cmbSeccion.Size = new System.Drawing.Size(238, 21);
            this.cmbSeccion.TabIndex = 2;
            this.cmbSeccion.SelectionChangeCommitted += new System.EventHandler(this.cmbSeccion_SelectionChangeCommitted);
            // 
            // checkBox2
            // 
            this.checkBox2.Location = new System.Drawing.Point(208, 16);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(39, 24);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "Identificador:";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(230, 19);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(40, 20);
            this.textBox3.TabIndex = 1;
            this.textBox3.Visible = false;
            this.textBox3.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtIdentificador_KeyUp);
            // 
            // chkCodigoSeccion
            // 
            this.chkCodigoSeccion.Location = new System.Drawing.Point(9, 19);
            this.chkCodigoSeccion.Name = "chkCodigoSeccion";
            this.chkCodigoSeccion.Size = new System.Drawing.Size(139, 20);
            this.chkCodigoSeccion.TabIndex = 0;
            this.chkCodigoSeccion.Text = "Código de la Sección:";
            this.chkCodigoSeccion.UseVisualStyleBackColor = true;
            this.chkCodigoSeccion.Click += new System.EventHandler(this.chkCodigoSeccion_Click);
            // 
            // chkSeccion
            // 
            this.chkSeccion.Location = new System.Drawing.Point(9, 46);
            this.chkSeccion.Name = "chkSeccion";
            this.chkSeccion.Size = new System.Drawing.Size(139, 18);
            this.chkSeccion.TabIndex = 0;
            this.chkSeccion.Text = "Nombre de la Seccion:";
            this.chkSeccion.UseVisualStyleBackColor = true;
            this.chkSeccion.Click += new System.EventHandler(this.chkSeccion_Click);
            // 
            // gbContenedor
            // 
            this.gbContenedor.Controls.Add(this.txtCodigoDelContenedor);
            this.gbContenedor.Controls.Add(this.cmbContenedor);
            this.gbContenedor.Controls.Add(this.checkBox5);
            this.gbContenedor.Controls.Add(this.textBox4);
            this.gbContenedor.Controls.Add(this.chkCodigoDelContenedor);
            this.gbContenedor.Controls.Add(this.chkNombreDelContenedor);
            this.gbContenedor.Location = new System.Drawing.Point(421, 171);
            this.gbContenedor.Name = "gbContenedor";
            this.gbContenedor.Size = new System.Drawing.Size(412, 78);
            this.gbContenedor.TabIndex = 0;
            this.gbContenedor.TabStop = false;
            this.gbContenedor.Text = "Contenedor:";
            // 
            // txtCodigoDelContenedor
            // 
            this.txtCodigoDelContenedor.Location = new System.Drawing.Point(154, 19);
            this.txtCodigoDelContenedor.Name = "txtCodigoDelContenedor";
            this.txtCodigoDelContenedor.Size = new System.Drawing.Size(238, 20);
            this.txtCodigoDelContenedor.TabIndex = 1;
            this.txtCodigoDelContenedor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigoDelContenedor_KeyUp);
            // 
            // cmbContenedor
            // 
            this.cmbContenedor.FormattingEnabled = true;
            this.cmbContenedor.Location = new System.Drawing.Point(154, 45);
            this.cmbContenedor.Name = "cmbContenedor";
            this.cmbContenedor.Size = new System.Drawing.Size(238, 21);
            this.cmbContenedor.TabIndex = 2;
            this.cmbContenedor.SelectionChangeCommitted += new System.EventHandler(this.cmbContenedor_SelectionChangeCommitted);
            // 
            // checkBox5
            // 
            this.checkBox5.Location = new System.Drawing.Point(208, 16);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(39, 24);
            this.checkBox5.TabIndex = 0;
            this.checkBox5.Text = "Identificador:";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.Visible = false;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(230, 19);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(40, 20);
            this.textBox4.TabIndex = 1;
            this.textBox4.Visible = false;
            this.textBox4.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtIdentificador_KeyUp);
            // 
            // chkCodigoDelContenedor
            // 
            this.chkCodigoDelContenedor.Location = new System.Drawing.Point(9, 19);
            this.chkCodigoDelContenedor.Name = "chkCodigoDelContenedor";
            this.chkCodigoDelContenedor.Size = new System.Drawing.Size(139, 20);
            this.chkCodigoDelContenedor.TabIndex = 0;
            this.chkCodigoDelContenedor.Text = "Código del Contenedor:";
            this.chkCodigoDelContenedor.UseVisualStyleBackColor = true;
            this.chkCodigoDelContenedor.Click += new System.EventHandler(this.chkCodigoDelContenedor_Click);
            // 
            // chkNombreDelContenedor
            // 
            this.chkNombreDelContenedor.Location = new System.Drawing.Point(9, 46);
            this.chkNombreDelContenedor.Name = "chkNombreDelContenedor";
            this.chkNombreDelContenedor.Size = new System.Drawing.Size(149, 18);
            this.chkNombreDelContenedor.TabIndex = 0;
            this.chkNombreDelContenedor.Text = "Nombre del Contenedor:";
            this.chkNombreDelContenedor.UseVisualStyleBackColor = true;
            this.chkNombreDelContenedor.Click += new System.EventHandler(this.chkNombreDelContenedor_Click);
            // 
            // dgvLista
            // 
            this.dgvLista.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvLista.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLista.Location = new System.Drawing.Point(0, 31);
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.Size = new System.Drawing.Size(886, 349);
            this.dgvLista.TabIndex = 2;
            this.dgvLista.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLista_CellContentClick);
            this.dgvLista.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dgvLista_CellContextMenuStripNeeded);
            this.dgvLista.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvLista_CurrentCellDirtyStateChanged);
            this.dgvLista.DoubleClick += new System.EventHandler(this.dgvLista_DoubleClick);
            this.dgvLista.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvLista_MouseDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNoRegistros});
            this.statusStrip1.Location = new System.Drawing.Point(0, 380);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(886, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsbNoRegistros
            // 
            this.tsbNoRegistros.Name = "tsbNoRegistros";
            this.tsbNoRegistros.Size = new System.Drawing.Size(102, 17);
            this.tsbNoRegistros.Text = "No. de registros: 0";
            // 
            // tsMenu
            // 
            this.tsMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFiltrar,
            this.tsbFiltroAutomatico,
            this.toolStripSeparator2,
            this.tsbMarcarTodos,
            this.tsbSeleccionarTodos});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(886, 31);
            this.tsMenu.TabIndex = 0;
            this.tsMenu.Text = "Filtrar";
            // 
            // tsbFiltrar
            // 
            this.tsbFiltrar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFiltrar.Image = global::InventoryBoxFarmacy.Properties.Resources.filtrar24x24;
            this.tsbFiltrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFiltrar.Name = "tsbFiltrar";
            this.tsbFiltrar.Size = new System.Drawing.Size(28, 28);
            this.tsbFiltrar.Text = "Filtrar";
            this.tsbFiltrar.ToolTipText = "Filtrar (F5)";
            this.tsbFiltrar.Click += new System.EventHandler(this.tsbFiltrar_Click);
            // 
            // tsbFiltroAutomatico
            // 
            this.tsbFiltroAutomatico.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbFiltroAutomatico.Image = global::InventoryBoxFarmacy.Properties.Resources.checked16x16;
            this.tsbFiltroAutomatico.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFiltroAutomatico.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFiltroAutomatico.Name = "tsbFiltroAutomatico";
            this.tsbFiltroAutomatico.Size = new System.Drawing.Size(120, 28);
            this.tsbFiltroAutomatico.Text = "Filtro Automatico";
            this.tsbFiltroAutomatico.Click += new System.EventHandler(this.tsbFiltroAutomatico_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbMarcarTodos
            // 
            this.tsbMarcarTodos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMarcarTodos.Image = global::InventoryBoxFarmacy.Properties.Resources.checked16x16;
            this.tsbMarcarTodos.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbMarcarTodos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMarcarTodos.Name = "tsbMarcarTodos";
            this.tsbMarcarTodos.Size = new System.Drawing.Size(23, 28);
            this.tsbMarcarTodos.Text = "Marcar Todo";
            this.tsbMarcarTodos.Click += new System.EventHandler(this.tsbMarcarTodos_Click);
            // 
            // tsbSeleccionarTodos
            // 
            this.tsbSeleccionarTodos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSeleccionarTodos.Image = global::InventoryBoxFarmacy.Properties.Resources.checked16x16;
            this.tsbSeleccionarTodos.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSeleccionarTodos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSeleccionarTodos.Name = "tsbSeleccionarTodos";
            this.tsbSeleccionarTodos.Size = new System.Drawing.Size(23, 28);
            this.tsbSeleccionarTodos.Text = "Seleccionar";
            this.tsbSeleccionarTodos.Click += new System.EventHandler(this.tsbSeleccionarTodos_Click);
            // 
            // frmLocalizacionDelProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 533);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmLocalizacionDelProducto";
            this.Text = "Localización del producto en bodega.";
            this.Shown += new System.EventHandler(this.frmLocalizacionDelProducto_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmLocalizacionDelProducto_KeyUp);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.gbAlmacenaje.ResumeLayout(false);
            this.gbAlmacenaje.PerformLayout();
            this.gbAlmacen.ResumeLayout(false);
            this.gbAlmacen.PerformLayout();
            this.gbBodega.ResumeLayout(false);
            this.gbBodega.PerformLayout();
            this.gbLocacion.ResumeLayout(false);
            this.gbLocacion.PerformLayout();
            this.gbSeccion.ResumeLayout(false);
            this.gbSeccion.PerformLayout();
            this.gbContenedor.ResumeLayout(false);
            this.gbContenedor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsbNoRegistros;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton tsbFiltrar;
        private System.Windows.Forms.ToolStripButton tsbFiltroAutomatico;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAlmacen;
        private System.Windows.Forms.TextBox txtIdentificador;
        private System.Windows.Forms.CheckBox chkIdentificador;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbMarcarTodos;
        private System.Windows.Forms.ToolStripButton tsbSeleccionarTodos;
        private System.Windows.Forms.TextBox txtCodigoDelAlmacen;
        private System.Windows.Forms.CheckBox chkCodigoDelAmacen;
        private System.Windows.Forms.ComboBox cmbAlmacen;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox gbAlmacen;
        private System.Windows.Forms.GroupBox gbBodega;
        private System.Windows.Forms.TextBox txtCodigoDeLaBodega;
        private System.Windows.Forms.ComboBox cmbBodega;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox chkCodigoDeLaBodega;
        private System.Windows.Forms.CheckBox chkBodega;
        private System.Windows.Forms.GroupBox gbLocacion;
        private System.Windows.Forms.TextBox txtCodigoLocacion;
        private System.Windows.Forms.ComboBox cmbLocacion;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.CheckBox chkCodigoLocacion;
        private System.Windows.Forms.CheckBox chkLocacion;
        private System.Windows.Forms.GroupBox gbSeccion;
        private System.Windows.Forms.TextBox txtCodigoDeSeccion;
        private System.Windows.Forms.ComboBox cmbSeccion;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.CheckBox chkCodigoSeccion;
        private System.Windows.Forms.CheckBox chkSeccion;
        private System.Windows.Forms.GroupBox gbContenedor;
        private System.Windows.Forms.TextBox txtCodigoDelContenedor;
        private System.Windows.Forms.ComboBox cmbContenedor;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.CheckBox chkCodigoDelContenedor;
        private System.Windows.Forms.CheckBox chkNombreDelContenedor;
        private System.Windows.Forms.GroupBox gbAlmacenaje;
        private System.Windows.Forms.TextBox txtCodigoDeAlmacenaje;
        private System.Windows.Forms.CheckBox chkCodigoDeAlmacenaje;
    }
}