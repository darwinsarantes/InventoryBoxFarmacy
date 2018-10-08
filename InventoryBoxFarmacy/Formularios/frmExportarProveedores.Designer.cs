namespace InventoryBoxFarmacy.Formularios
{
    partial class frmExportarProveedores
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
            this.dgvListar = new System.Windows.Forms.DataGridView();
            this.btnImportarDesdeExcel = new System.Windows.Forms.Button();
            this.btnCerrarVentana = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreDeLaHoja = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pbBarra = new System.Windows.Forms.ProgressBar();
            this.lbEtiqueta = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvListar
            // 
            this.dgvListar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListar.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvListar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListar.Location = new System.Drawing.Point(10, 45);
            this.dgvListar.Name = "dgvListar";
            this.dgvListar.Size = new System.Drawing.Size(768, 237);
            this.dgvListar.TabIndex = 0;
            this.dgvListar.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvListar_CurrentCellDirtyStateChanged);
            // 
            // btnImportarDesdeExcel
            // 
            this.btnImportarDesdeExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportarDesdeExcel.Location = new System.Drawing.Point(8, 288);
            this.btnImportarDesdeExcel.Name = "btnImportarDesdeExcel";
            this.btnImportarDesdeExcel.Size = new System.Drawing.Size(255, 31);
            this.btnImportarDesdeExcel.TabIndex = 1;
            this.btnImportarDesdeExcel.Text = "Importar Datos del proveedor desde Excel";
            this.btnImportarDesdeExcel.UseVisualStyleBackColor = true;
            this.btnImportarDesdeExcel.Click += new System.EventHandler(this.btnImportarDesdeExcel_Click);
            // 
            // btnCerrarVentana
            // 
            this.btnCerrarVentana.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrarVentana.Location = new System.Drawing.Point(607, 288);
            this.btnCerrarVentana.Name = "btnCerrarVentana";
            this.btnCerrarVentana.Size = new System.Drawing.Size(171, 31);
            this.btnCerrarVentana.TabIndex = 2;
            this.btnCerrarVentana.Text = "Cerrar ventana";
            this.btnCerrarVentana.UseVisualStyleBackColor = true;
            this.btnCerrarVentana.Click += new System.EventHandler(this.btnCerrarVentana_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nombre de la Hoja a Importar.";
            // 
            // txtNombreDeLaHoja
            // 
            this.txtNombreDeLaHoja.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombreDeLaHoja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreDeLaHoja.Location = new System.Drawing.Point(240, 16);
            this.txtNombreDeLaHoja.Name = "txtNombreDeLaHoja";
            this.txtNombreDeLaHoja.Size = new System.Drawing.Size(538, 22);
            this.txtNombreDeLaHoja.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(309, 288);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(255, 31);
            this.button1.TabIndex = 5;
            this.button1.Text = "Guardar datos del proveedor.";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pbBarra
            // 
            this.pbBarra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBarra.Location = new System.Drawing.Point(10, 327);
            this.pbBarra.Name = "pbBarra";
            this.pbBarra.Size = new System.Drawing.Size(768, 23);
            this.pbBarra.TabIndex = 6;
            this.pbBarra.Visible = false;
            // 
            // lbEtiqueta
            // 
            this.lbEtiqueta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbEtiqueta.BackColor = System.Drawing.Color.Transparent;
            this.lbEtiqueta.Location = new System.Drawing.Point(17, 327);
            this.lbEtiqueta.Name = "lbEtiqueta";
            this.lbEtiqueta.Size = new System.Drawing.Size(750, 23);
            this.lbEtiqueta.TabIndex = 7;
            this.lbEtiqueta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbEtiqueta.Visible = false;
            // 
            // frmExportarProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 360);
            this.Controls.Add(this.lbEtiqueta);
            this.Controls.Add(this.pbBarra);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtNombreDeLaHoja);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCerrarVentana);
            this.Controls.Add(this.btnImportarDesdeExcel);
            this.Controls.Add(this.dgvListar);
            this.Name = "frmExportarProveedores";
            this.Text = "Importar información de los proveedores";
            ((System.ComponentModel.ISupportInitialize)(this.dgvListar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvListar;
        private System.Windows.Forms.Button btnImportarDesdeExcel;
        private System.Windows.Forms.Button btnCerrarVentana;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreDeLaHoja;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar pbBarra;
        private System.Windows.Forms.Label lbEtiqueta;
    }
}