using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidad;
using InventoryBoxFarmacy.Formularios;

using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace InventoryBoxFarmacy
{
    static class Program
    {
        public static bool Iniciar = false;
        public static string NombreSistema = " ";//"Sistema Contable";
        public static string NombreEmpresa = "Sistema de control de inventario";
        public static string EntidadIngSistemas = "Servicios TRIGERS";
        public static string VersionSistema = Version();
        public static string NombreVersionSistema = NombreSistema + VersionSistema;
        public static DatosDeConexionEN oDatosDeConexion = null;
        public static LoginEN oLoginEN = null;
        public static string Errores;
        public static ConfiguracionEN oConfiguracionEN = new ConfiguracionEN();

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmSplash ofrmSplash = new frmSplash();
            ofrmSplash.ShowDialog();

            frmLogin oFrmLogin = new frmLogin();
            oFrmLogin.ShowDialog();

            if (Iniciar == true){Application.Run(new Principal());}
        }

        public static string Version()
        {
            string sversion = "";
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                sversion = string.Format("Versión: {0}.{1}.{2}.{3}", System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Major,
                    System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Minor,
                    System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Build,
                    System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Revision);
            }
            else
            {
                sversion = string.Format(" Versión: {0}", Application.ProductVersion.ToString());
            }

            return sversion;

        }

        public static bool Exportar_Logo_Para_Excel(PictureBox pbxImagen)
        {
            try
            {
                if (!Directory.Exists(Application.StartupPath + "/Logos"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "/Logos");
                }

                string Ruta;
                Ruta = Application.StartupPath + "/Logos/Logo_Empresa.png";

                Image image = pbxImagen.Image;
                image.Save(Ruta, System.Drawing.Imaging.ImageFormat.Png);
                return true;
            }
            catch (Exception ex)
            {
                Errores = ex.Message;
                return false;
            }
        }
    }
}
