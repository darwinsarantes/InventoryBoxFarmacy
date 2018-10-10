using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ProductoImagenesEN
    {//idProductoImagenes, idProducto, Nombre, extension, Ruta, Size, Foto
        public int idProductoImagenes { set; get; }
        public string Nombre { set; get; }
        public string extension { set; get; }
        public string Ruta { set; get; }
        public decimal Size { set; get; }       
        /// <summary>
        /// Variable tipo objeto para el objeto de la imagen de la empresa
        /// </summary>
        public Object Foto { set; get; }
        /// <summary>
        /// Arreglo de datos para la imagen de la empresa
        /// </summary>
        public byte[] AFoto { set; get; }


        public int idUsuarioDeCreacion { set; get; }
        public DateTime FechaDeCreacion { set; get; }
        public int idUsuarioModificacion { set; get; }
        public DateTime FechaDeModificacion { set; get; }
        
        public LoginEN oLoginEN = new LoginEN();
        public ProductoEN oProductoEN = new ProductoEN();      

        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public String SubTituloDelReporte { set; get; }

    }
}
