using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ProductoEN
    {

        public int idProducto { set; get; }
        public string Codigo { set; get; }
        public string CodigoDeBarra { set; get; }
        public string Nombre { set; get; }
        public string NombreGenerico { set; get; }
        public string NombreComun { set; get; }
        public string Descripcion { set; get; }
        public string Observaciones { set; get; }
        public decimal Existencias { set; get; }
        public decimal Minimo { set; get; }
        public decimal Maximo { set; get; }
        public string Estado { set; get; }
        public int ProductoControlado { set; get; }

        public int idUsuarioDeCreacion { set; get; }
        public DateTime FechaDeCreacion { set; get; }
        public int idUsuarioModificacion { set; get; }
        public DateTime FechaDeModificacion { set; get; }

        /// <summary>
        /// Variable tipo objeto para el objeto de la imagen del producto
        /// </summary>
        public Object oFoto { set; get; }
        /// <summary>
        /// Arreglo de datos para la imagen del producto
        /// </summary>
        public byte[] AFoto { set; get; }

        public LoginEN oLoginEN = new LoginEN();
        public ProductoUnidadDeMedidaEN oUnidadDeMedida = new ProductoUnidadDeMedidaEN();
        public ProductoPresentacionEN oPresentacion = new ProductoPresentacionEN();
        public CategoriaEN oCategoria = new CategoriaEN();

        public decimal idAlmacenEntidad { set; get; }
        public decimal idPLEntidad { set; get; }
        public string TablaDeReferenciaDeAlmacenaje { set; get; }
        public string TablaDeRefereciaDeProveedorOLaboratorio { set; get; }

        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public String SubTituloDelReporte { set; get; }

    }

}
