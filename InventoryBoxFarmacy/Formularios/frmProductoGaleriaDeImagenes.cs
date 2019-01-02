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
using Funciones;

namespace InventoryBoxFarmacy.Formularios
{
    public partial class frmProductoGaleriaDeImagenes : Form
    {
        public int ValorLlavePrimariaEntidad { set; get; }
        private ImageList ListaDeImagenes = null;
        public string TituloDeLaVentana { set; get; }
        public string InformacionDeLaOperacion { set; get; }

        public frmProductoGaleriaDeImagenes()
        {
            InitializeComponent();
        }

        #region "Funciones definidas por el programador"

        private void EstablecerTituloDeVentana()
        {
            this.Text = TituloDeLaVentana;
            this.InformacionEntidadOperacion.Text = InformacionDeLaOperacion;
        }

        private void ConfigurarImagenList()
        {
            try
            {

                ListaDeImagenes = new ImageList();
                ListaDeImagenes.ImageSize = new Size(250, 250);
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Configuracion del control de Listado de imagenes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CrearColumnasListView()
        {
            try
            {
                ColumnHeader c = new ColumnHeader();
                c.Name = "idProductoImagenes";
                c.TextAlign = HorizontalAlignment.Left;
                c.Width = 0;
                //idProductoImagenes,PorDefecto, Nombre, extension, Ruta, Size, Foto                
                lvImagenes.Columns.Add("Eliminar", 50, HorizontalAlignment.Left).Text = "Eliminar";                
                lvImagenes.Columns.Add(c);
                lvImagenes.CheckBoxes = true;
                lvImagenes.View = View.SmallIcon;
                lvImagenes.FullRowSelect = true;
                lvImagenes.LabelEdit = false;
                lvImagenes.AllowColumnReorder = true;
                lvImagenes.GridLines = true;
                lvImagenes.MultiSelect = false;

                lvImagenes.LargeImageList = ListaDeImagenes;
                lvImagenes.SmallImageList = ListaDeImagenes;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Crear colunmas en el listview", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public Image resizeImage(Image img, int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(img, 0, 0, width, height);
            g.Dispose();

            return (Image)b;
        }

        private static Image ThumbNailImage(int newSize, Image originalImage)
        {
            if (originalImage.Width <= newSize)
                newSize = originalImage.Width;

            var newHeight = originalImage.Height * newSize / originalImage.Width;

            if (newHeight > newSize)
            {
                // Resize with height instead 
                newSize = originalImage.Width * newSize / originalImage.Height;
                newHeight = newSize;
            }

            return originalImage.GetThumbnailImage(newSize, newHeight, null, IntPtr.Zero);
        }

        private void CargarImagenDesdeLaBD()
        {
            try
            {

                ProductoImagenesEN oRegistroEN = new ProductoImagenesEN();
                ProductoImagenesLN oRegistroLN = new ProductoImagenesLN();

                oRegistroEN.oProductoEN.idProducto = ValorLlavePrimariaEntidad;
                oRegistroEN.Where = string.Format(" and idProducto = {0}", ValorLlavePrimariaEntidad);

                ListaDeImagenes.Images.Clear();
                lvImagenes.Items.Clear();

                if (oRegistroLN.Listado(oRegistroEN, Program.oDatosDeConexion))
                {

                    if (oRegistroLN.TraerDatos().Rows.Count > 0)
                    {
                        
                        foreach (DataRow Fila in oRegistroLN.TraerDatos().Rows)
                        {
                            if (Fila["Foto"] != DBNull.Value)
                            {
                                
                                Image ImagenDB = Imagenes.ProcesarImagenToBitMap((object)(Fila["Foto"]));
                                ListaDeImagenes.Images.Add(ImagenDB);

                                ListViewItem item = new ListViewItem("");
                                item.SubItems.Add(Fila["idProductoImagenes"].ToString());
                                item.ImageIndex = ListaDeImagenes.Images.Count - 1;
                                lvImagenes.Items.Add(item);

                            }
                        }

                    }
                  
                }
                else
                {
                    throw new ArgumentException(oRegistroLN.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cargar imagenes desde la base de datos.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool InsertarEliminarActualizar()
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;

                if (EvaluarListView(lvImagenes))
                {
                    string DescOperacion = DescripcionDetallaDeLaOperacion(lvImagenes);

                    if (DescOperacion.Trim().Length > 0)
                    {
                        MessageBox.Show(DescOperacion, "Insertar, eliminar o actualizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        int Indice = 0;
                        while (Indice <= lvImagenes.Items.Count -1 )
                        {
                            ListViewItem Fila = lvImagenes.Items[Indice];

                            bool Eliminar = Fila.Checked;
                            int idProductoImagenes;
                            int.TryParse(Fila.SubItems[1].Text, out idProductoImagenes);

                            string OperacionAREalizar = "";

                            if (Eliminar == false && idProductoImagenes == 0)
                                OperacionAREalizar = "NUEVO";
                            else if (Eliminar == false && idProductoImagenes > 0)
                                OperacionAREalizar = "ACTUALIZAR";
                            else if (Eliminar == true && idProductoImagenes > 0)
                                OperacionAREalizar = "ELIMINAR";
                            else if (Eliminar == true && idProductoImagenes == 0)
                                OperacionAREalizar = "REMOVER";

                            ProductoImagenesEN oRegistroEN = new ProductoImagenesEN();
                            oRegistroEN.idProductoImagenes = idProductoImagenes;
                            oRegistroEN.oProductoEN.idProducto = ValorLlavePrimariaEntidad;
                            
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            ListaDeImagenes.Images[Fila.ImageIndex].Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            oRegistroEN.AFoto = ms.GetBuffer();                            
                            
                            ProductoImagenesLN oRegistroLN = new ProductoImagenesLN();

                            switch (OperacionAREalizar)
                            {
                                case "REMOVER":
                                    lvImagenes.Items[Indice].Remove();
                                    if (lvImagenes.Items.Count <= 0) { Indice++; }
                                    continue;

                                case "NUEVO":
                                    
                                    if(oRegistroLN.Agregar(oRegistroEN, Program.oDatosDeConexion))
                                    {
                                        Fila.SubItems[1].Text = oRegistroEN.idProductoImagenes.ToString();
                                        oRegistroEN = null;
                                        oRegistroLN = null;
                                        Indice++;
                                        continue;
                                    }
                                    else
                                    {
                                        this.Cursor = Cursors.Default;
                                        throw new ArgumentException(oRegistroLN.Error);                                        
                                    }

                                case "ACTUALIZAR":

                                    if (oRegistroLN.Actualizar(oRegistroEN, Program.oDatosDeConexion))
                                    {                                        
                                        oRegistroEN = null;
                                        oRegistroLN = null;
                                        Indice++;
                                        continue;
                                    }
                                    else
                                    {
                                        this.Cursor = Cursors.Default;
                                        throw new ArgumentException(oRegistroLN.Error);
                                    }

                                case "ELIMINAR":

                                    if (oRegistroLN.Eliminar(oRegistroEN, Program.oDatosDeConexion))
                                    {
                                        lvImagenes.Items[Indice].Remove();
                                        if (lvImagenes.Items.Count <= 0) { Indice++; }
                                        oRegistroEN = null;
                                        oRegistroLN = null;                                        
                                        continue;
                                    }
                                    else
                                    {
                                        this.Cursor = Cursors.Default;
                                        throw new ArgumentException(oRegistroLN.Error);
                                    }

                            }
                            

                        }

                        this.Cursor = Cursors.Default;

                    }
                    
                   

                }
                
                return true;
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Insertar, eliminar o actualizar registro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private bool EvaluarListView(ListView lv)
        {
            if(lv.Items.Count > 0)
            {
                IEnumerable<ListViewItem> lv1 = lv.Items.Cast<ListViewItem>();
                var rows = from x in lv1
                           where x.Checked == true || Convert.ToInt32( x.SubItems[1].Text) == 0
                           select x;

                if(rows.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        private string DescripcionDetallaDeLaOperacion(ListView lv)
        {
            string Mensaje = "";

            if (lv.Items.Count > 0)
            {
                IEnumerable<ListViewItem> lv1 = lv.Items.Cast<ListViewItem>();

                var rows = from x in lv1
                           where x.Checked == false && Convert.ToInt32(x.SubItems[1].Text) == 0
                           select x;

                if (rows.Count() > 0)
                {
                    Mensaje += string.Format("Se van agregar ({0}) imagenes a la galeria", rows.Count().ToString());
                }

                var rows1 = from x in lv1
                           where x.Checked == true && Convert.ToInt32(x.SubItems[1].Text) > 0
                           select x;

                if (rows1.Count() > 0)
                {
                    Mensaje += string.Format("Se van eliminar ({0}) imagenes a la galeria", rows1.Count().ToString());
                }
                
                return Mensaje;

            }
            else
            {
                return Mensaje;
            }
        }

        #endregion

        private void frmProductoGaleriaDeImagenes_Shown(object sender, EventArgs e)
        {
            EstablecerTituloDeVentana();
            ConfigurarImagenList();
            CrearColumnasListView();
            CargarImagenDesdeLaBD();
        }

        private void tsbAgregarImagen_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog Abrir = new OpenFileDialog();
                Image ImagenLogo;

                //Abrir.InitialDirectory = "c:\\";
                Abrir.Filter = "Archivos de imágenes (*.jpg)|*.jpg|Archivos de imágenes (*.png)|*.png|Archivos de imágenes (*.gif)|*.gif";
                Abrir.FilterIndex = 1;
                Abrir.RestoreDirectory = true;
                Abrir.Title = "Buscar archivos de imágenes compatibles";

                if (Abrir.ShowDialog() == DialogResult.OK)
                {
                    if(Abrir.OpenFile() != null)
                    {
                        ImagenLogo = new Bitmap(Abrir.FileName);
                        Image Nimagen = ThumbNailImage(250, ImagenLogo);
                        
                        //ListaDeImagenes.Images.Add(ImagenLogo);
                        ListaDeImagenes.Images.Add(Nimagen);
                        ListViewItem item = new ListViewItem(" ");                       
                        item.SubItems.Add("0");
                        item.ImageIndex = ListaDeImagenes.Images.Count - 1;
                        lvImagenes.Items.Add(item);

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Agregando imagenes a la galeria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tsbEliminarEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                if (lvImagenes.SelectedItems.Count > 0)
                {
                    ListViewItem item = lvImagenes.SelectedItems[0];

                    if (MessageBox.Show("Desea quitar la imagen seleccionada de la lista", "Quitar imagen de la Lista", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        int idProductoImagenes = Convert.ToInt32(item.SubItems[1].Text);
                        if (idProductoImagenes == 0)
                        {
                            item.Remove();
                        }
                        else
                        {
                            ProductoImagenesEN oRegistroEN = new ProductoImagenesEN();
                            ProductoImagenesLN oRegistroLN = new ProductoImagenesLN();

                            oRegistroEN.idProductoImagenes = idProductoImagenes;
                            oRegistroEN.oProductoEN.idProducto = ValorLlavePrimariaEntidad;

                            oRegistroEN.oLoginEN = Program.oLoginEN;
                            oRegistroEN.idUsuarioDeCreacion = Program.oLoginEN.idUsuario;
                            oRegistroEN.idUsuarioModificacion = Program.oLoginEN.idUsuario;
                            oRegistroEN.FechaDeCreacion = System.DateTime.Now;
                            oRegistroEN.FechaDeModificacion = System.DateTime.Now;

                            if (oRegistroLN.Eliminar(oRegistroEN, Program.oDatosDeConexion))
                            {
                                item.Remove();
                            }

                        }
                    }

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Eliminar Imagen de la Galeria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tsbGuardar_Click(object sender, EventArgs e)
        {
            if (InsertarEliminarActualizar())
            {
                MessageBox.Show("Imagenes guardadas correctamente", "Guardar imagenes en la galeria");
                this.Close();
            }
        }

        private void tsbCerrarVentan_Click(object sender, EventArgs e)
        {
            if (EvaluarListView(lvImagenes))
            {
                if( MessageBox.Show("Hay registros pendiente de guardar, desea cerrar la ventana", "Cerrar la Ventana de galeria", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Close();
                }
            }else
            {
                this.Close();
            }
        }

        private void tsbRecarRegistro_Click(object sender, EventArgs e)
        {
                        
            if (EvaluarListView(lvImagenes))
            {
                if (MessageBox.Show("Se perderan los registros al realizar la operacion, desea continuar!", "Cerrar la Ventana de galeria", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CargarImagenDesdeLaBD();
                }
            }
            else
            {
                CargarImagenDesdeLaBD();
            }
        }
    }
}
