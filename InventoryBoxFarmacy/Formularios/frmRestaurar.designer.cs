namespace InventoryBoxFarmacy.Formularios
{
    partial class frmRestaurar
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdRecargarRegistrosRespaldo = new System.Windows.Forms.ToolStripButton();
            this.dgvListaArchivosRespaldo = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRutaRespaldos = new System.Windows.Forms.TextBox();
            this.tsBarraHarramientas = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdRespaldarBaseDatos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblInfoProgreso = new System.Windows.Forms.ToolStripLabel();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaArchivosRespaldo)).BeginInit();
            this.tsBarraHarramientas.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.dgvListaArchivosRespaldo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtRutaRespaldos);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(826, 449);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdRecargarRegistrosRespaldo});
            this.toolStrip1.Location = new System.Drawing.Point(6, 81);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(203, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdRecargarRegistrosRespaldo
            // 
            this.cmdRecargarRegistrosRespaldo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdRecargarRegistrosRespaldo.Name = "cmdRecargarRegistrosRespaldo";
            this.cmdRecargarRegistrosRespaldo.Size = new System.Drawing.Size(169, 22);
            this.cmdRecargarRegistrosRespaldo.Text = "Recargar registros de respaldo";
            this.cmdRecargarRegistrosRespaldo.ToolTipText = "Recargar registros de respaldo";
            this.cmdRecargarRegistrosRespaldo.Click += new System.EventHandler(this.cmdRecargarRegistrosRespaldo_Click);
            // 
            // dgvListaArchivosRespaldo
            // 
            this.dgvListaArchivosRespaldo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListaArchivosRespaldo.BackgroundColor = System.Drawing.Color.White;
            this.dgvListaArchivosRespaldo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaArchivosRespaldo.Location = new System.Drawing.Point(6, 109);
            this.dgvListaArchivosRespaldo.Name = "dgvListaArchivosRespaldo";
            this.dgvListaArchivosRespaldo.Size = new System.Drawing.Size(812, 331);
            this.dgvListaArchivosRespaldo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(329, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(277, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "La disponibilidad de la ruta de respaldo depende del acceso a la red.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(311, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Los archivos de respaldo se obtienen de la siguiente ruta de red:";
            // 
            // txtRutaRespaldos
            // 
            this.txtRutaRespaldos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRutaRespaldos.Location = new System.Drawing.Point(6, 38);
            this.txtRutaRespaldos.Name = "txtRutaRespaldos";
            this.txtRutaRespaldos.ReadOnly = true;
            this.txtRutaRespaldos.Size = new System.Drawing.Size(812, 20);
            this.txtRutaRespaldos.TabIndex = 0;
            // 
            // tsBarraHarramientas
            // 
            this.tsBarraHarramientas.BackColor = System.Drawing.Color.Transparent;
            this.tsBarraHarramientas.Dock = System.Windows.Forms.DockStyle.None;
            this.tsBarraHarramientas.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsBarraHarramientas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.cmdRespaldarBaseDatos,
            this.toolStripSeparator2,
            this.lblInfoProgreso});
            this.tsBarraHarramientas.Location = new System.Drawing.Point(10, 464);
            this.tsBarraHarramientas.Name = "tsBarraHarramientas";
            this.tsBarraHarramientas.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsBarraHarramientas.Size = new System.Drawing.Size(309, 25);
            this.tsBarraHarramientas.TabIndex = 2;
            this.tsBarraHarramientas.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdRespaldarBaseDatos
            // 
            this.cmdRespaldarBaseDatos.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdRespaldarBaseDatos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdRespaldarBaseDatos.Name = "cmdRespaldarBaseDatos";
            this.cmdRespaldarBaseDatos.Size = new System.Drawing.Size(206, 22);
            this.cmdRespaldarBaseDatos.Tag = "Restaurar";
            this.cmdRespaldarBaseDatos.Text = "Restaurar base de datos seleccionada";
            this.cmdRespaldarBaseDatos.Click += new System.EventHandler(this.cmdRespaldarBaseDatos_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // lblInfoProgreso
            // 
            this.lblInfoProgreso.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblInfoProgreso.Name = "lblInfoProgreso";
            this.lblInfoProgreso.Size = new System.Drawing.Size(88, 22);
            this.lblInfoProgreso.Text = "lblInfoProgreso";
            // 
            // frmRestaurar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 499);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tsBarraHarramientas);
            this.Name = "frmRestaurar";
            this.Text = "Restaurar base de datos";
            this.Shown += new System.EventHandler(this.frmRestaurar_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaArchivosRespaldo)).EndInit();
            this.tsBarraHarramientas.ResumeLayout(false);
            this.tsBarraHarramientas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdRecargarRegistrosRespaldo;
        private System.Windows.Forms.DataGridView dgvListaArchivosRespaldo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRutaRespaldos;
        private System.Windows.Forms.ToolStrip tsBarraHarramientas;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cmdRespaldarBaseDatos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblInfoProgreso;
    }
}