using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ProductoCompletoEN
    {

        public ProductoEN oProductoEN = new ProductoEN();
        public ProductoConfiguracionEN oConfiguracionEN = new ProductoConfiguracionEN();
        public ProductoPrecioEN oPrecioEN = new ProductoPrecioEN();
        public ProductoPromocionEN oPromocionEN = new ProductoPromocionEN();
        public ProductoLoteEN oProductoLote = new ProductoLoteEN();

        public LoginEN oLoginEN = new LoginEN();

        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public String SubTituloDelReporte { set; get; }

    }
}
