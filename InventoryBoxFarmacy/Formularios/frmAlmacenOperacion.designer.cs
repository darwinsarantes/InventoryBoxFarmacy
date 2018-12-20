namespace InventoryBoxFarmacy.Formularios
{
    partial class frmAlmacenOperacion
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvListar = new System.Windows.Forms.DataGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.chkPorDefecto = new System.Windows.Forms.CheckBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAlmacen = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIdentificador = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbaBarradeprogreso = new System.Windows.Forms.ToolStripStatusLabel();
            this.Barradeprogreso = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbGuardar = new System.Windows.Forms.ToolStripButton();
            this.tsbActualizar = new System.Windows.Forms.ToolStripButton();
            this.tsbEliminar = new System.Windows.Forms.ToolStripButton();
            this.tsbLimpiarCampos = new System.Windows.Forms.ToolStripButton();
            this.tsbCerrarVentan = new System.Windows.Forms.ToolStripButton();
            this.tsbRecarRegistro = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbImprimir = new System.Windows.Forms.ToolStripButton();
            this.chkCerrarVentana = new System.Windows.Forms.CheckBox();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.InformacionEntidadOperacion = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(12, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1056, 408);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(591, 292);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvListar);
            this.tabPage2.Controls.Add(this.toolStrip2);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(583, 261);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Bodegas del almacen";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvListar
            // 
            this.dgvListar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListar.Location = new System.Drawing.Point(3, 32);
            this.dgvListar.Name = "dgvListar";
            this.dgvListar.Size = new System.Drawing.Size(577, 226);
            this.dgvListar.TabIndex = 1;
            this.dgvListar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListar_CellContentClick);
            this.dgvListar.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListar_CellEndEdit);
            this.dgvListar.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvListar_CurrentCellDirtyStateChanged);
            this.dgvListar.SelectionChanged += new System.EventHandler(this.dgvListar_SelectionChanged);
            this.dgvListar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvListar_MouseDown);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNuevo});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(577, 29);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbNuevo
            // 
            this.tsbNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNuevo.Image = global::InventoryBoxFarmacy.Properties.Resources.new22x22;
            this.tsbNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNuevo.Name = "tsbNuevo";
            this.tsbNuevo.Size = new System.Drawing.Size(26, 26);
            this.tsbNuevo.Text = "toolStripButton2";
            this.tsbNuevo.ToolTipText = "Nuevo Registro";
            this.tsbNuevo.Click += new System.EventHandler(this.tsbNuevo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Controls.Add(this.chkPorDefecto);
            this.groupBox1.Controls.Add(this.txtCodigo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtAlmacen);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtIdentificador);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 337);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informacion del Almacen";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDescripcion.Location = new System.Drawing.Point(8, 107);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(412, 185);
            this.txtDescripcion.TabIndex = 3;
            // 
            // chkPorDefecto
            // 
            this.chkPorDefecto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkPorDefecto.Location = new System.Drawing.Point(9, 298);
            this.chkPorDefecto.Name = "chkPorDefecto";
            this.chkPorDefecto.Size = new System.Drawing.Size(164, 22);
            this.chkPorDefecto.TabIndex = 9;
            this.chkPorDefecto.Text = "Almacen por Defecto";
            this.chkPorDefecto.UseVisualStyleBackColor = true;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(173, 22);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(247, 24);
            this.txtCodigo.TabIndex = 4;
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(8, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "Código:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtAlmacen
            // 
            this.txtAlmacen.Location = new System.Drawing.Point(173, 52);
            this.txtAlmacen.Name = "txtAlmacen";
            this.txtAlmacen.Size = new System.Drawing.Size(247, 24);
            this.txtAlmacen.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(8, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nombre del Almacen:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(8, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "Descripción:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtIdentificador
            // 
            this.txtIdentificador.Location = new System.Drawing.Point(8, 22);
            this.txtIdentificador.Name = "txtIdentificador";
            this.txtIdentificador.Size = new System.Drawing.Size(19, 24);
            this.txtIdentificador.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbaBarradeprogreso,
            this.Barradeprogreso});
            this.statusStrip1.Location = new System.Drawing.Point(0, 236);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(447, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // lbaBarradeprogreso
            // 
            this.lbaBarradeprogreso.Name = "lbaBarradeprogreso";
            this.lbaBarradeprogreso.Size = new System.Drawing.Size(38, 17);
            this.lbaBarradeprogreso.Text = "0 de 0";
            // 
            // Barradeprogreso
            // 
            this.Barradeprogreso.Name = "Barradeprogreso";
            this.Barradeprogreso.Size = new System.Drawing.Size(300, 16);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbGuardar,
            this.tsbActualizar,
            this.tsbEliminar,
            this.tsbLimpiarCampos,
            this.tsbCerrarVentan,
            this.tsbRecarRegistro,
            this.toolStripSeparator1,
            this.tsbImprimir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 337);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1056, 71);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbGuardar
            // 
            this.tsbGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbGuardar.Image = global::InventoryBoxFarmacy.Properties.Resources.if_floppy_285657;
            this.tsbGuardar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGuardar.Name = "tsbGuardar";
            this.tsbGuardar.Size = new System.Drawing.Size(68, 68);
            this.tsbGuardar.Text = "Guardar";
            this.tsbGuardar.ToolTipText = "Guardar registro";
            this.tsbGuardar.Click += new System.EventHandler(this.tsbGuardar_Click);
            // 
            // tsbActualizar
            // 
            this.tsbActualizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbActualizar.Image = global::InventoryBoxFarmacy.Properties.Resources.if_edit_173002__2_;
            this.tsbActualizar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbActualizar.Name = "tsbActualizar";
            this.tsbActualizar.Size = new System.Drawing.Size(68, 68);
            this.tsbActualizar.Text = "Actualizar";
            this.tsbActualizar.ToolTipText = "Actualizar registro";
            this.tsbActualizar.Click += new System.EventHandler(this.tsbActualizar_Click);
            // 
            // tsbEliminar
            // 
            this.tsbEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEliminar.Image = global::InventoryBoxFarmacy.Properties.Resources.if_edit_delete_118920__2_;
            this.tsbEliminar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEliminar.Name = "tsbEliminar";
            this.tsbEliminar.Size = new System.Drawing.Size(68, 68);
            this.tsbEliminar.Text = "Eliminar";
            this.tsbEliminar.ToolTipText = "Eliminar registro";
            this.tsbEliminar.Click += new System.EventHandler(this.tsbEliminar_Click);
            // 
            // tsbLimpiarCampos
            // 
            this.tsbLimpiarCampos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLimpiarCampos.Image = global::InventoryBoxFarmacy.Properties.Resources.if_Plus_1891033__1_;
            this.tsbLimpiarCampos.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbLimpiarCampos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLimpiarCampos.Name = "tsbLimpiarCampos";
            this.tsbLimpiarCampos.Size = new System.Drawing.Size(52, 68);
            this.tsbLimpiarCampos.Text = "Nuevo";
            this.tsbLimpiarCampos.ToolTipText = "Nuevo registro";
            this.tsbLimpiarCampos.Click += new System.EventHandler(this.tsbLimpiarCampos_Click);
            // 
            // tsbCerrarVentan
            // 
            this.tsbCerrarVentan.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbCerrarVentan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCerrarVentan.Image = global::InventoryBoxFarmacy.Properties.Resources.if_Log_Out_27856;
            this.tsbCerrarVentan.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCerrarVentan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCerrarVentan.Name = "tsbCerrarVentan";
            this.tsbCerrarVentan.Size = new System.Drawing.Size(52, 68);
            this.tsbCerrarVentan.Text = "Salir";
            this.tsbCerrarVentan.ToolTipText = "Cerrar la ventana y retornar";
            this.tsbCerrarVentan.Click += new System.EventHandler(this.tsbCerrarVentan_Click);
            // 
            // tsbRecarRegistro
            // 
            this.tsbRecarRegistro.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbRecarRegistro.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRecarRegistro.Image = global::InventoryBoxFarmacy.Properties.Resources.if_Synchronize_27883__1_;
            this.tsbRecarRegistro.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbRecarRegistro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRecarRegistro.Name = "tsbRecarRegistro";
            this.tsbRecarRegistro.Size = new System.Drawing.Size(52, 68);
            this.tsbRecarRegistro.Text = "Recargar ";
            this.tsbRecarRegistro.ToolTipText = "Recargar  registro";
            this.tsbRecarRegistro.Click += new System.EventHandler(this.tsbRecarRegistro_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 71);
            // 
            // tsbImprimir
            // 
            this.tsbImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImprimir.Image = global::InventoryBoxFarmacy.Properties.Resources.if_printer_39263__3_;
            this.tsbImprimir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImprimir.Name = "tsbImprimir";
            this.tsbImprimir.Size = new System.Drawing.Size(68, 68);
            this.tsbImprimir.Text = "Imprimir";
            this.tsbImprimir.Visible = false;
            // 
            // chkCerrarVentana
            // 
            this.chkCerrarVentana.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkCerrarVentana.AutoSize = true;
            this.chkCerrarVentana.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCerrarVentana.Location = new System.Drawing.Point(12, 449);
            this.chkCerrarVentana.Name = "chkCerrarVentana";
            this.chkCerrarVentana.Size = new System.Drawing.Size(275, 22);
            this.chkCerrarVentana.TabIndex = 1;
            this.chkCerrarVentana.Text = "Cerrar ventana de manera automatica";
            this.chkCerrarVentana.UseVisualStyleBackColor = true;
            this.chkCerrarVentana.CheckedChanged += new System.EventHandler(this.chkCerrarVentana_CheckedChanged);
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // InformacionEntidadOperacion
            // 
            this.InformacionEntidadOperacion.AutoSize = true;
            this.InformacionEntidadOperacion.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InformacionEntidadOperacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.InformacionEntidadOperacion.Location = new System.Drawing.Point(14, 15);
            this.InformacionEntidadOperacion.Name = "InformacionEntidadOperacion";
            this.InformacionEntidadOperacion.Size = new System.Drawing.Size(0, 16);
            this.InformacionEntidadOperacion.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(1056, 337);
            this.splitContainer1.SplitterDistance = 455;
            this.splitContainer1.TabIndex = 14;
            // 
            // frmAlmacenOperacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1080, 483);
            this.Controls.Add(this.InformacionEntidadOperacion);
            this.Controls.Add(this.chkCerrarVentana);
            this.Controls.Add(this.panel1);
            this.Name = "frmAlmacenOperacion";
            this.Text = "frmAlmacenOperacion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAlmacenOperacion_FormClosing);
            this.Shown += new System.EventHandler(this.frmAlmacenOperacion_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbGuardar;
        private System.Windows.Forms.CheckBox chkCerrarVentana;
        private System.Windows.Forms.ToolStripButton tsbActualizar;
        private System.Windows.Forms.ToolStripButton tsbEliminar;
        private System.Windows.Forms.ToolStripButton tsbLimpiarCampos;
        private System.Windows.Forms.ToolStripButton tsbRecarRegistro;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbImprimir;
        private System.Windows.Forms.ToolStripButton tsbCerrarVentan;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtIdentificador;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.Label InformacionEntidadOperacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtAlmacen;
        private System.Windows.Forms.CheckBox chkPorDefecto;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvListar;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbaBarradeprogreso;
        private System.Windows.Forms.ToolStripProgressBar Barradeprogreso;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}