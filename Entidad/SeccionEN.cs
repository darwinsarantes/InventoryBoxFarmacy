﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class SeccionEN
    {
        public int idSeccion { set; get; }
        public string Codigo { set; get; }
        public string Nombre { set; get; }
        public string Descripcion { set; get; }
        public string CodigoDeAlmacenaje { set; get; }
        
        public int idUsuarioDeCreacion { set; get; }
        public DateTime FechaDeCreacion { set; get; }
        public int idUsuarioModificacion { set; get; }
        public DateTime FechaDeModificacion { set; get; }
        
        public LoginEN oLoginEN = new LoginEN();
        public LocacionEN oLocacionEN = new LocacionEN();

        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public String SubTituloDelReporte { set; get; }

    }
}
