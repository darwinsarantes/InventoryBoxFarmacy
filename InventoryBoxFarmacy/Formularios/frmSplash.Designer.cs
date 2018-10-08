namespace InventoryBoxFarmacy.Formularios
{
    partial class frmSplash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplash));
            this.lbaTiempoRestante = new System.Windows.Forms.Label();
            this.lbaVersion = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbRespaldo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbaTiempoRestante
            // 
            this.lbaTiempoRestante.AutoSize = true;
            this.lbaTiempoRestante.BackColor = System.Drawing.Color.Transparent;
            this.lbaTiempoRestante.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbaTiempoRestante.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lbaTiempoRestante.Location = new System.Drawing.Point(587, 333);
            this.lbaTiempoRestante.Name = "lbaTiempoRestante";
            this.lbaTiempoRestante.Size = new System.Drawing.Size(104, 13);
            this.lbaTiempoRestante.TabIndex = 1;
            this.lbaTiempoRestante.Text = "Enter para omitir (10)";
            this.lbaTiempoRestante.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbaTiempoRestante_MouseMove);
            // 
            // lbaVersion
            // 
            this.lbaVersion.AutoSize = true;
            this.lbaVersion.BackColor = System.Drawing.Color.Transparent;
            this.lbaVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbaVersion.ForeColor = System.Drawing.SystemColors.Window;
            this.lbaVersion.Location = new System.Drawing.Point(537, 154);
            this.lbaVersion.Name = "lbaVersion";
            this.lbaVersion.Size = new System.Drawing.Size(22, 13);
            this.lbaVersion.TabIndex = 2;
            this.lbaVersion.Text = "1.0";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbRespaldo
            // 
            this.lbRespaldo.AutoSize = true;
            this.lbRespaldo.BackColor = System.Drawing.Color.Transparent;
            this.lbRespaldo.Location = new System.Drawing.Point(130, 333);
            this.lbRespaldo.Name = "lbRespaldo";
            this.lbRespaldo.Size = new System.Drawing.Size(0, 13);
            this.lbRespaldo.TabIndex = 3;
            // 
            // frmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::InventoryBoxFarmacy.Properties.Resources.Splash;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(703, 404);
            this.Controls.Add(this.lbRespaldo);
            this.Controls.Add(this.lbaVersion);
            this.Controls.Add(this.lbaTiempoRestante);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSplash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSplash";
            this.Load += new System.EventHandler(this.frmSplash_Load);
            this.Shown += new System.EventHandler(this.frmSplash_Shown);
            this.DoubleClick += new System.EventHandler(this.frmSplash_DoubleClick);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmSplash_KeyUp);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmSplash_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbaTiempoRestante;
        private System.Windows.Forms.Label lbaVersion;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbRespaldo;
    }
}