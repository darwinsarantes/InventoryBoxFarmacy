namespace InventoryBoxFarmacy.Formularios
{
    partial class frmProductoUnidadDeMedida
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAbreviatura = new System.Windows.Forms.TextBox();
            this.chkAbreviatura = new System.Windows.Forms.CheckBox();
            this.txtProductoUnidadDeMedida = new System.Windows.Forms.TextBox();
            this.chkProductoUnidadDeMedida = new System.Windows.Forms.CheckBox();
            this.txtIdentificador = new System.Windows.Forms.TextBox();
            this.chkIdentificador = new System.Windows.Forms.CheckBox();
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.mcsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.visualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsbNoRegistros = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbFiltrar = new System.Windows.Forms.ToolStripButton();
            this.tsbFiltroAutomatico = new System.Windows.Forms.ToolStripButton();
            this.tsbImprimir = new System.Windows.Forms.ToolStripButton();
            this.tsbNuevoRegistro = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMarcarTodos = new System.Windows.Forms.ToolStripButton();
            this.tsbSeleccionarTodos = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.mcsMenu.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(697, 533);
            this.splitContainer1.SplitterDistance = 142;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtAbreviatura);
            this.groupBox1.Controls.Add(this.chkAbreviatura);
            this.groupBox1.Controls.Add(this.txtProductoUnidadDeMedida);
            this.groupBox1.Controls.Add(this.chkProductoUnidadDeMedida);
            this.groupBox1.Controls.Add(this.txtIdentificador);
            this.groupBox1.Controls.Add(this.chkIdentificador);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(673, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtrado de información del tipo de ubicación.";
            // 
            // txtAbreviatura
            // 
            this.txtAbreviatura.Location = new System.Drawing.Point(197, 87);
            this.txtAbreviatura.Name = "txtAbreviatura";
            this.txtAbreviatura.Size = new System.Drawing.Size(390, 20);
            this.txtAbreviatura.TabIndex = 3;
            this.txtAbreviatura.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAbreviatura_KeyUp);
            // 
            // chkAbreviatura
            // 
            this.chkAbreviatura.Location = new System.Drawing.Point(21, 85);
            this.chkAbreviatura.Name = "chkAbreviatura";
            this.chkAbreviatura.Size = new System.Drawing.Size(158, 24);
            this.chkAbreviatura.TabIndex = 2;
            this.chkAbreviatura.Text = "Abreviatura:";
            this.chkAbreviatura.UseVisualStyleBackColor = true;
            // 
            // txtProductoUnidadDeMedida
            // 
            this.txtProductoUnidadDeMedida.Location = new System.Drawing.Point(197, 61);
            this.txtProductoUnidadDeMedida.Name = "txtProductoUnidadDeMedida";
            this.txtProductoUnidadDeMedida.Size = new System.Drawing.Size(390, 20);
            this.txtProductoUnidadDeMedida.TabIndex = 1;
            this.txtProductoUnidadDeMedida.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDesProductoUnidadDeMedida_KeyUp);
            // 
            // chkProductoUnidadDeMedida
            // 
            this.chkProductoUnidadDeMedida.Location = new System.Drawing.Point(21, 59);
            this.chkProductoUnidadDeMedida.Name = "chkProductoUnidadDeMedida";
            this.chkProductoUnidadDeMedida.Size = new System.Drawing.Size(197, 24);
            this.chkProductoUnidadDeMedida.TabIndex = 0;
            this.chkProductoUnidadDeMedida.Text = "Unidad de Medida del Producto:";
            this.chkProductoUnidadDeMedida.UseVisualStyleBackColor = true;
            // 
            // txtIdentificador
            // 
            this.txtIdentificador.Location = new System.Drawing.Point(197, 35);
            this.txtIdentificador.Name = "txtIdentificador";
            this.txtIdentificador.Size = new System.Drawing.Size(390, 20);
            this.txtIdentificador.TabIndex = 1;
            this.txtIdentificador.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtIdentificador_KeyUp);
            // 
            // chkIdentificador
            // 
            this.chkIdentificador.Location = new System.Drawing.Point(21, 33);
            this.chkIdentificador.Name = "chkIdentificador";
            this.chkIdentificador.Size = new System.Drawing.Size(111, 24);
            this.chkIdentificador.TabIndex = 0;
            this.chkIdentificador.Text = "Identificador:";
            this.chkIdentificador.UseVisualStyleBackColor = true;
            // 
            // dgvLista
            // 
            this.dgvLista.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvLista.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLista.ContextMenuStrip = this.mcsMenu;
            this.dgvLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLista.Location = new System.Drawing.Point(0, 31);
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.Size = new System.Drawing.Size(697, 334);
            this.dgvLista.TabIndex = 2;
            this.dgvLista.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLista_CellContentClick);
            this.dgvLista.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dgvLista_CellContextMenuStripNeeded);
            this.dgvLista.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvLista_CurrentCellDirtyStateChanged);
            this.dgvLista.DoubleClick += new System.EventHandler(this.dgvLista_DoubleClick);
            this.dgvLista.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvLista_MouseDown);
            // 
            // mcsMenu
            // 
            this.mcsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.actualizarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.toolStripSeparator1,
            this.visualizarToolStripMenuItem,
            this.imprimirToolStripMenuItem});
            this.mcsMenu.Name = "mcsMenu";
            this.mcsMenu.Size = new System.Drawing.Size(127, 120);
            this.mcsMenu.Opened += new System.EventHandler(this.mcsMenu_Opened);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = global::InventoryBoxFarmacy.Properties.Resources.New16x16;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Image = global::InventoryBoxFarmacy.Properties.Resources.Edit16x16;
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.actualizarToolStripMenuItem.Text = "Actualizar";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::InventoryBoxFarmacy.Properties.Resources.Eliminar16x16;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(123, 6);
            // 
            // visualizarToolStripMenuItem
            // 
            this.visualizarToolStripMenuItem.Image = global::InventoryBoxFarmacy.Properties.Resources.view16x16;
            this.visualizarToolStripMenuItem.Name = "visualizarToolStripMenuItem";
            this.visualizarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.visualizarToolStripMenuItem.Text = "Visualizar";
            this.visualizarToolStripMenuItem.Click += new System.EventHandler(this.visualizarToolStripMenuItem_Click);
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.Image = global::InventoryBoxFarmacy.Properties.Resources.printer16x16;
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNoRegistros});
            this.statusStrip1.Location = new System.Drawing.Point(0, 365);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(697, 22);
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
            this.tsbImprimir,
            this.tsbNuevoRegistro,
            this.toolStripSeparator2,
            this.tsbMarcarTodos,
            this.tsbSeleccionarTodos});
            this.tsMenu.Location = new System.Drawing.Point(0, 0);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(697, 31);
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
            // tsbImprimir
            // 
            this.tsbImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbImprimir.Image = global::InventoryBoxFarmacy.Properties.Resources.printer24x24;
            this.tsbImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImprimir.Name = "tsbImprimir";
            this.tsbImprimir.Size = new System.Drawing.Size(28, 28);
            this.tsbImprimir.Text = "Imprimir";
            this.tsbImprimir.Visible = false;
            // 
            // tsbNuevoRegistro
            // 
            this.tsbNuevoRegistro.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNuevoRegistro.Image = global::InventoryBoxFarmacy.Properties.Resources.new24x24;
            this.tsbNuevoRegistro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNuevoRegistro.Name = "tsbNuevoRegistro";
            this.tsbNuevoRegistro.Size = new System.Drawing.Size(28, 28);
            this.tsbNuevoRegistro.Text = "Nuevo Registro";
            this.tsbNuevoRegistro.ToolTipText = "Nuevo Registro (F2)";
            this.tsbNuevoRegistro.Click += new System.EventHandler(this.tsbNuevoRegistro_Click);
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
            this.tsbMarcarTodos.Text = "Marcar todo";
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
            // frmProductoUnidadDeMedida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 533);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmProductoUnidadDeMedida";
            this.Text = "Unidad de Medida";
            this.Shown += new System.EventHandler(this.frmProductoUnidadDeMedida_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmProductoUnidadDeMedida_KeyUp);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.mcsMenu.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox txtProductoUnidadDeMedida;
        private System.Windows.Forms.CheckBox chkProductoUnidadDeMedida;
        private System.Windows.Forms.TextBox txtIdentificador;
        private System.Windows.Forms.CheckBox chkIdentificador;
        private System.Windows.Forms.ToolStripButton tsbImprimir;
        private System.Windows.Forms.ToolStripButton tsbNuevoRegistro;
        private System.Windows.Forms.ContextMenuStrip mcsMenu;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actualizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem visualizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbMarcarTodos;
        private System.Windows.Forms.ToolStripButton tsbSeleccionarTodos;
        private System.Windows.Forms.TextBox txtAbreviatura;
        private System.Windows.Forms.CheckBox chkAbreviatura;
    }
}