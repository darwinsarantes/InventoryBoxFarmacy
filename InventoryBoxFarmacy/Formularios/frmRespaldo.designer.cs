namespace InventoryBoxFarmacy.Formularios
{
    partial class frmRespaldo
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRutaRespaldos = new System.Windows.Forms.TextBox();
            this.tsBarraHarramientas = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdRespaldarBaseDatos = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1.SuspendLayout();
            this.tsBarraHarramientas.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtRutaRespaldos);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(758, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 61);
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
            this.label1.Size = new System.Drawing.Size(334, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "El respaldo (Backup) de la base de datos se hará en la siguiente ruta:";
            // 
            // txtRutaRespaldos
            // 
            this.txtRutaRespaldos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRutaRespaldos.Location = new System.Drawing.Point(6, 38);
            this.txtRutaRespaldos.Name = "txtRutaRespaldos";
            this.txtRutaRespaldos.ReadOnly = true;
            this.txtRutaRespaldos.Size = new System.Drawing.Size(744, 20);
            this.txtRutaRespaldos.TabIndex = 0;
            // 
            // tsBarraHarramientas
            // 
            this.tsBarraHarramientas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tsBarraHarramientas.BackColor = System.Drawing.Color.Transparent;
            this.tsBarraHarramientas.Dock = System.Windows.Forms.DockStyle.None;
            this.tsBarraHarramientas.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsBarraHarramientas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.cmdRespaldarBaseDatos,
            this.toolStripSeparator2});
            this.tsBarraHarramientas.Location = new System.Drawing.Point(10, 119);
            this.tsBarraHarramientas.Name = "tsBarraHarramientas";
            this.tsBarraHarramientas.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsBarraHarramientas.Size = new System.Drawing.Size(183, 25);
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
            this.cmdRespaldarBaseDatos.Size = new System.Drawing.Size(137, 22);
            this.cmdRespaldarBaseDatos.Tag = "Respaldar";
            this.cmdRespaldarBaseDatos.Text = "Respaldar base de datos";
            this.cmdRespaldarBaseDatos.Click += new System.EventHandler(this.cmdRespaldarBaseDatos_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // frmRespaldo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 148);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tsBarraHarramientas);
            this.Name = "frmRespaldo";
            this.Text = "Respaldar base de datos";
            this.Shown += new System.EventHandler(this.frmRespaldo_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tsBarraHarramientas.ResumeLayout(false);
            this.tsBarraHarramientas.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRutaRespaldos;
        private System.Windows.Forms.ToolStrip tsBarraHarramientas;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cmdRespaldarBaseDatos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}