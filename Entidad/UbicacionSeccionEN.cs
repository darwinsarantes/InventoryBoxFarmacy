﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class UbicacionSeccionEN
    {     //idUbicacionSeccion, idUbicacion, idSeccion
        public int idUbicacionSeccion { set; get; }
       
        public int IdUsuarioDeCreacion { set; get; }
        public DateTime FechaDeCreacion { set; get; }
        public int IdUsuarioDeModificacion { set; get; }
        public DateTime FechaDeModificacion { set; get; }

        public LoginEN oLoginEN = new LoginEN();
        public UbicacionEN oUbicacionEN = new UbicacionEN();
        public SeccionEN oSeccionEN = new SeccionEN();

        public string Where { set; get; }
        public string OrderBy { set; get; }
        public string TituloDelReporte { set; get; }
        public string SubTituloDelReporte { set; get; }

    }
}
