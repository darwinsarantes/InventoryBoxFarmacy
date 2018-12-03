using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidad;
using Logica;
using InventoryBoxFarmacy.Reportes;

namespace InventoryBoxFarmacy.Formularios
{
    public partial class frmVisor : Form
    {
        public frmVisor()
        {
            InitializeComponent();
        }


        private CrystalDecisions.CrystalReports.Engine.ReportDocument RPT;
        public string NombreReporte { get; set; }
        public string TituloDelReporte { set; get; }
        public string SubTituloFiltros { set; get; }
        public object Entidad { set; get; }
        public int AplicarBorder { set; get; }

        public DataTable oTablas { set; get; }

        DataSet DS;
        private void LlenarParametros(string[,] Parametros)
        {

            for (int i = 0; i < Parametros.GetLength(0); i++) //Recorremos la dimención de la primera dimensión
                for (int j = 0; j < Parametros.GetLength(1);) //Recorremos la dimensión de la segunda dimensión
                {
                    //Asignamos los parámetros a la variable RPT, que contiene una instancia del reporte a mostrar
                    RPT.SetParameterValue(Parametros[i, j], Parametros[i, j + 1]);
                    break;
                }

        }

        private DataSet AgregarTablaADataSet(DataTable DT, string Tabla)
        {
            if (DS == null)
            {
                DS = new DataSet("DataSet");
            }

            DS.Tables.Add(DT);
            DS.Tables[DS.Tables.Count - 1].TableName = Tabla;
            return DS;
        }

        private void AgregarTablaEmpresaADataSet()
        {
            EmpresaEN oRegistroEN = new EmpresaEN();
            EmpresaLN oRegistroLN = new EmpresaLN();
            DataSet DS = new DataSet();

            oRegistroEN.IdEmpresa = 1;
            oRegistroLN.Listado(oRegistroEN, Program.oDatosDeConexion);

            AgregarTablaADataSet(oRegistroLN.TraerDatos(), "ListadoEmpresa");
        }

        private void frmVista_Load(object sender, EventArgs e)
        {

            switch (this.NombreReporte)
            {

                case "Proveedores - Listado":
                    ImprimirListadoDeProveedores();
                    break;
                    
                default:
                    MessageBox.Show("No existe código asociado al reporte solicitado: " + this.NombreReporte, "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }

        }

        private void ImprimirListadoDeProveedores()
        {
            try
            {

                ProveedorEN oRegistroEN = new ProveedorEN();
                ProveedorLN oRegistroLN = new ProveedorLN();

                oRegistroEN = (ProveedorEN)this.Entidad;

                if (oRegistroLN.ListadoParaReportes(oRegistroEN, Program.oDatosDeConexion))
                {
                    RPT = new rptListadoDeProveedores();
                    AgregarTablaEmpresaADataSet();
                    RPT.SetDataSource(AgregarTablaADataSet(oRegistroLN.TraerDatos(), "ListadoProveedores"));
                    LlenarParametros(new string[,] { { "NombreDelSistema", Program.NombreVersionSistema }, { "TituloDelReporte", oRegistroEN.TituloDelReporte }, { "SubTituloDeReporte", oRegistroEN.SubTituloDelReporte }, { "AplicarBorde", this.AplicarBorder.ToString() } });
                    this.Text = "Listado de Reportes";
                    crvVista.ReportSource = RPT;

                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(oRegistroLN.Error, "Listado de Reportes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                oRegistroLN = null;
                this.Entidad = null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        
    }
}
