namespace InventoryBoxFarmacy.Formularios
{
    partial class frmProductoPromociones
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
            this.InformacionEntidadOperacion = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtidProductoPromocion = new System.Windows.Forms.TextBox();
            this.txtDescripcionDeLaPromocion = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.txtPrecioPromocional = new System.Windows.Forms.TextBox();
            this.dtpkHastaPromocional = new System.Windows.Forms.DateTimePicker();
            this.label40 = new System.Windows.Forms.Label();
            this.dtpkDesdePromocion = new System.Windows.Forms.DateTimePicker();
            this.label39 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbGuardar = new System.Windows.Forms.ToolStripButton();
            this.tsbCerrarVentan = new System.Windows.Forms.ToolStripButton();
            this.tsbRecarRegistro = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.gbGaleriaDeImagenes = new System.Windows.Forms.GroupBox();
            this.dgvListar = new System.Windows.Forms.DataGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.chkCerrarVentana = new System.Windows.Forms.CheckBox();
            this.EP = new System.Windows.Forms.ErrorProvider(this.components);
            this.tsbNuevo = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.gbGaleriaDeImagenes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).BeginInit();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).BeginInit();
            this.SuspendLayout();
            // 
            // InformacionEntidadOperacion
            // 
            this.InformacionEntidadOperacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InformacionEntidadOperacion.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InformacionEntidadOperacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.InformacionEntidadOperacion.Location = new System.Drawing.Point(19, 30);
            this.InformacionEntidadOperacion.Name = "InformacionEntidadOperacion";
            this.InformacionEntidadOperacion.Size = new System.Drawing.Size(609, 41);
            this.InformacionEntidadOperacion.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmbEstado);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txtidProductoPromocion);
            this.groupBox1.Controls.Add(this.txtDescripcionDeLaPromocion);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label38);
            this.groupBox1.Controls.Add(this.txtPrecioPromocional);
            this.groupBox1.Controls.Add(this.dtpkHastaPromocional);
            this.groupBox1.Controls.Add(this.label40);
            this.groupBox1.Controls.Add(this.dtpkDesdePromocion);
            this.groupBox1.Controls.Add(this.label39);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.gbGaleriaDeImagenes);
            this.groupBox1.Location = new System.Drawing.Point(12, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(619, 441);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // cmbEstado
            // 
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "ACTIVA",
            "INACTIVA"});
            this.cmbEstado.Location = new System.Drawing.Point(79, 152);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(223, 21);
            this.cmbEstado.TabIndex = 36;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(22, 133);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(152, 16);
            this.label19.TabIndex = 35;
            this.label19.Text = "Estado de la Promoción";
            // 
            // txtidProductoPromocion
            // 
            this.txtidProductoPromocion.Location = new System.Drawing.Point(50, 106);
            this.txtidProductoPromocion.Name = "txtidProductoPromocion";
            this.txtidProductoPromocion.Size = new System.Drawing.Size(23, 20);
            this.txtidProductoPromocion.TabIndex = 34;
            this.txtidProductoPromocion.Visible = false;
            // 
            // txtDescripcionDeLaPromocion
            // 
            this.txtDescripcionDeLaPromocion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcionDeLaPromocion.Location = new System.Drawing.Point(336, 36);
            this.txtDescripcionDeLaPromocion.Multiline = true;
            this.txtDescripcionDeLaPromocion.Name = "txtDescripcionDeLaPromocion";
            this.txtDescripcionDeLaPromocion.Size = new System.Drawing.Size(257, 137);
            this.txtDescripcionDeLaPromocion.TabIndex = 33;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(336, 17);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(183, 16);
            this.label17.TabIndex = 32;
            this.label17.Text = "Descripción de la promoción:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(22, 85);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(213, 16);
            this.label38.TabIndex = 31;
            this.label38.Text = "Precio del producto en promoción:";
            // 
            // txtPrecioPromocional
            // 
            this.txtPrecioPromocional.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioPromocional.Location = new System.Drawing.Point(79, 105);
            this.txtPrecioPromocional.Name = "txtPrecioPromocional";
            this.txtPrecioPromocional.Size = new System.Drawing.Size(223, 22);
            this.txtPrecioPromocional.TabIndex = 30;
            this.txtPrecioPromocional.Text = "0.00";
            this.txtPrecioPromocional.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtpkHastaPromocional
            // 
            this.dtpkHastaPromocional.CustomFormat = "dd - MMM - yyyy hh:mm:ss tt";
            this.dtpkHastaPromocional.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpkHastaPromocional.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkHastaPromocional.Location = new System.Drawing.Point(79, 49);
            this.dtpkHastaPromocional.Name = "dtpkHastaPromocional";
            this.dtpkHastaPromocional.Size = new System.Drawing.Size(223, 22);
            this.dtpkHastaPromocional.TabIndex = 29;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(22, 55);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(44, 16);
            this.label40.TabIndex = 28;
            this.label40.Text = "Hasta";
            // 
            // dtpkDesdePromocion
            // 
            this.dtpkDesdePromocion.CustomFormat = "dd - MMM - yyyy hh:mm:ss tt";
            this.dtpkDesdePromocion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpkDesdePromocion.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDesdePromocion.Location = new System.Drawing.Point(79, 17);
            this.dtpkDesdePromocion.Name = "dtpkDesdePromocion";
            this.dtpkDesdePromocion.Size = new System.Drawing.Size(223, 22);
            this.dtpkDesdePromocion.TabIndex = 27;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(22, 22);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(52, 16);
            this.label39.TabIndex = 26;
            this.label39.Text = "Desde:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbGuardar,
            this.tsbCerrarVentan,
            this.tsbRecarRegistro,
            this.toolStripSeparator1,
            this.tsbNuevo});
            this.toolStrip1.Location = new System.Drawing.Point(3, 367);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(613, 71);
            this.toolStrip1.TabIndex = 1;
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
            // gbGaleriaDeImagenes
            // 
            this.gbGaleriaDeImagenes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGaleriaDeImagenes.Controls.Add(this.dgvListar);
            this.gbGaleriaDeImagenes.Controls.Add(this.toolStrip2);
            this.gbGaleriaDeImagenes.Location = new System.Drawing.Point(6, 191);
            this.gbGaleriaDeImagenes.Name = "gbGaleriaDeImagenes";
            this.gbGaleriaDeImagenes.Size = new System.Drawing.Size(613, 173);
            this.gbGaleriaDeImagenes.TabIndex = 0;
            this.gbGaleriaDeImagenes.TabStop = false;
            this.gbGaleriaDeImagenes.Text = "Promociones del Producto";
            // 
            // dgvListar
            // 
            this.dgvListar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListar.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvListar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListar.Location = new System.Drawing.Point(6, 50);
            this.dgvListar.Name = "dgvListar";
            this.dgvListar.Size = new System.Drawing.Size(601, 117);
            this.dgvListar.TabIndex = 2;
            this.dgvListar.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListar_CellClick);
            this.dgvListar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListar_CellContentClick);
            this.dgvListar.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListar_CellEndEdit);
            this.dgvListar.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvListar_CurrentCellDirtyStateChanged);
            this.dgvListar.SelectionChanged += new System.EventHandler(this.dgvListar_SelectionChanged);
            this.dgvListar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvListar_MouseDown);
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4});
            this.toolStrip2.Location = new System.Drawing.Point(3, 16);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(607, 31);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = global::InventoryBoxFarmacy.Properties.Resources.Eliminar24x24;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(78, 28);
            this.toolStripButton4.Text = "Eliminar";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // chkCerrarVentana
            // 
            this.chkCerrarVentana.AutoSize = true;
            this.chkCerrarVentana.Location = new System.Drawing.Point(387, 10);
            this.chkCerrarVentana.Name = "chkCerrarVentana";
            this.chkCerrarVentana.Size = new System.Drawing.Size(191, 17);
            this.chkCerrarVentana.TabIndex = 37;
            this.chkCerrarVentana.Text = "Cerrar la ventana automaticamente";
            this.chkCerrarVentana.UseVisualStyleBackColor = true;
            // 
            // EP
            // 
            this.EP.ContainerControl = this;
            // 
            // tsbNuevo
            // 
            this.tsbNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNuevo.Image = global::InventoryBoxFarmacy.Properties.Resources.if_Plus_1891033__1_;
            this.tsbNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNuevo.Name = "tsbNuevo";
            this.tsbNuevo.Size = new System.Drawing.Size(52, 68);
            this.tsbNuevo.Text = "Nuevo";
            this.tsbNuevo.Click += new System.EventHandler(this.tsbNuevo_Click);
            // 
            // frmProductoPromociones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 529);
            this.Controls.Add(this.chkCerrarVentana);
            this.Controls.Add(this.InformacionEntidadOperacion);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmProductoPromociones";
            this.Text = "frmProductoPromociones";
            this.Shown += new System.EventHandler(this.frmProductoPromociones_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbGaleriaDeImagenes.ResumeLayout(false);
            this.gbGaleriaDeImagenes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label InformacionEntidadOperacion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbGuardar;
        private System.Windows.Forms.ToolStripButton tsbCerrarVentan;
        private System.Windows.Forms.ToolStripButton tsbRecarRegistro;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox gbGaleriaDeImagenes;
        private System.Windows.Forms.DataGridView dgvListar;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpkHastaPromocional;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.DateTimePicker dtpkDesdePromocion;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox txtPrecioPromocional;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtDescripcionDeLaPromocion;
        private System.Windows.Forms.TextBox txtidProductoPromocion;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.CheckBox chkCerrarVentana;
        private System.Windows.Forms.ErrorProvider EP;
        private System.Windows.Forms.ToolStripButton tsbNuevo;
    }
}