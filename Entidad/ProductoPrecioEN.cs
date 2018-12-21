using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ProductoPrecioEN
    {
        public int idProductoPrecio { set; get; }
        public decimal Costo { set; get; }
        public decimal PorcentajeDelPrecio1 { set; get; }
        public decimal PorcentajeDelPrecio2 { set; get; }
        public decimal PorcentajeDelPrecio3 { set; get; }
        public decimal PorcentajeDelPrecio4 { set; get; }
        public decimal PorcentajeDelPrecio5 { set; get; }
        public decimal Precio1 { set; get; }
        public decimal Precio2 { set; get; }
        public decimal Precio3 { set; get; }
        public decimal Precio4 { set; get; }
        public decimal Precio5 { set; get; }
        public decimal AplicarElIva { set; get; }
        public decimal ValorDelIvaEnProcentaje { set; get; }
        public decimal ValorDelIva { set; get; }
        public string Estado { set; get; }
        public decimal PrecioXUnidad { set; get; }
        public decimal UnidadesXPrecentacion { set; get; }

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
