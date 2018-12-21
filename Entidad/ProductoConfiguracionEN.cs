using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class ProductoConfiguracionEN
    {
        public int idProductoConfiguracion { set; get; }
        public int ActivarPromocion { set; get; }
        public int AplicarComisiones { set; get; }
        public int MostrarContenidoDeObservacionesENFactura { set; get; }
        public int MostrarImagenAlFacturar { set; get; } 
        public int PreguntarNumeroDeSerieAlFacturar { set; get; }
        public int PreguntarFechaDeVencimientoAlFacturar { set; get; }
        public int PreguntarPorResetaAlFacturar { set; get; }
        public int NoUsarComisionesParaEsteProducto { set; get; }
        public int UsarComisionesDefinidasEnElregistroDelVendedor { set; get; }
        public int MontoFijoPorVenta { set; get; }
        public int PorcentajeDeLaVenta { set; get; }
        public int PorcentajeDeLaGanacia { set; get; }
        public decimal Comision { set; get; }
        public decimal ComisionMaxima { set; get; }
        public string MarcaDelProducto { set; get; }
        public string ModeloDelProducto { set; get; }
        public string NumeroDeSerie { set; get; }

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
