using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ProductoPromocionEN
    {
        public int idProductoPromocion { set; get; }
        public decimal PrecioDelProducto { set; get; }
        public DateTime FechaDeInicio { set; get; }
        public DateTime FechaDeFinalizacion { set; get; }
        public string Estado { set; get; }  
        public String Descripcion { set; get; }     
        
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
