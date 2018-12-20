using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Funciones;
namespace InventoryBoxFarmacy.Formularios
{
    public partial class frmGenerarSeccionOContenedores : Form
    {
        public frmGenerarSeccionOContenedores()
        {
            InitializeComponent();
        }

        public int ValorInicial { set; get; }
        public int ValorFinal { set; get; }
        public string TituloDeLaVentana { set; get; }
        public string TituloDelGroupBox { set; get; }
        public bool AplicarAutomatico { set; get; }

        private bool ValidarValores()
        {
            if (Controles.IsNullOEmptyElControl(txtInicio))
            {
                errorProvider1.SetError(txtInicio, "No puede haber valor vacio");
                txtInicio.Focus();
                return false;
            }

            if (Controles.IsNullOEmptyElControl(txtFinal))
            {
                errorProvider1.SetError(txtInicio, "No puede haber valor vacio");
                txtFinal.Focus();
                return false;
            }

            int Valor1;
            int.TryParse(txtInicio.Text, out Valor1);

            int Valor2;
            int.TryParse(txtFinal.Text, out Valor2);

            if(Valor1 > Valor2)
            {
                errorProvider1.SetError(txtInicio, "No puede ser mayor que valor final");
                txtInicio.Focus();
                return false;
            }

            if (Valor2 < Valor1)
            {
                errorProvider1.SetError(txtFinal, "No puede ser menor que valor inicial");
                txtFinal.Focus();
                return false;
            }

            return true;
        }

        private void btAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarValores())
            {
                int Valor1;
                int.TryParse(txtInicio.Text, out Valor1);
                int Valor2;
                int.TryParse(txtFinal.Text, out Valor2);

                ValorInicial = Valor1;
                ValorFinal = Valor2;
                this.AplicarAutomatico = true;
                this.Close();
            }
        }

        private void txtInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar)) //Al pulsar teclas como Borrar y eso.
            {
                e.Handled = false; //Se acepta (todo OK)
            }
            else //Para todo lo demas
            {
                e.Handled = true; //No se acepta (si pulsas cualquier otra cosa pues no se envia)
            }
        }

        private void txtFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar)) //Al pulsar teclas como Borrar y eso.
            {
                e.Handled = false; //Se acepta (todo OK)
            }
            else //Para todo lo demas
            {
                e.Handled = true; //No se acepta (si pulsas cualquier otra cosa pues no se envia)
            }
        }

        private void frmGenerarSeccionOContenedores_Shown(object sender, EventArgs e)
        {
            this.Text = TituloDeLaVentana;
            groupBox1.Text = TituloDelGroupBox;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.AplicarAutomatico = false;
            this.Close();
        }
    }
}
