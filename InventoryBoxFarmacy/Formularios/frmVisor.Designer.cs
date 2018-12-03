namespace InventoryBoxFarmacy.Formularios
{
    partial class frmVisor
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
            this.crvVista = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvVista
            // 
            this.crvVista.ActiveViewIndex = -1;
            this.crvVista.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvVista.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvVista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvVista.Location = new System.Drawing.Point(0, 0);
            this.crvVista.Name = "crvVista";
            this.crvVista.Size = new System.Drawing.Size(284, 261);
            this.crvVista.TabIndex = 0;
            // 
            // frmVisor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.crvVista);
            this.Name = "frmVisor";
            this.Text = "frmVisor";
            this.Load += new System.EventHandler(this.frmVista_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvVista;
    }
}