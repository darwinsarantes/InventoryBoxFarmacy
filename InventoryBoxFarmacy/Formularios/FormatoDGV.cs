using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryBoxFarmacy.Formularios
{
    class FormatoDGV
    {
        public string Columna { set; get; }
        public string Descripcion { set; get; }
        public int Tamano { set; get; }
        public bool SoloLectura { set; get; }
        public DataGridViewContentAlignment Alineacion { set; get; }
        public DataGridViewContentAlignment AlineacionDelEncabezado { set; get; }
        
        public bool ValorEncontrado { set; get; }

        public FormatoDGV(string NombreDelaColumna)
        {

            this.Columna = NombreDelaColumna.Trim();
            this.ValorEncontrado = true;
            
            switch (Columna)
            {
                case "Eliminar":
                    this.Descripcion = "Eliminar";
                    this.Tamano = 50;
                    this.Alineacion = DataGridViewContentAlignment.MiddleCenter;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = false;
                    break;

                case "idTipoDeEntidad":
                    this.Descripcion = "ID";
                    this.Tamano = 50;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "TipoDeEntidad":
                    this.Descripcion = "Tipo de Entidad";
                    this.Tamano = 200;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "Seccion":
                    this.Descripcion = "Sección";
                    this.Tamano = 200;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "Ubicacion":
                    this.Descripcion = "Ubicación";
                    this.Tamano = 200;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "TipoDeUbicacion":
                    this.Descripcion = "Tipo de Ubicación";
                    this.Tamano = 160;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "idModuloInterfazUsuario":
                    this.Descripcion = "ID";
                    this.Tamano = 50;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "Modulo":
                    this.Descripcion = "Modulo";
                    this.Tamano = 160;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "Interfaz":
                    this.Descripcion = "Interfaz";
                    this.Tamano = 160;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "NombreAMostrar":
                    this.Descripcion = "Nombre Interno";
                    this.Tamano = 200;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "Privilegio":
                    this.Descripcion = "Permiso";
                    this.Tamano = 200;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "Acceso":
                    this.Descripcion = "Acceso";
                    this.Tamano = 50;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "idPrivilegio":
                    this.Descripcion = "ID";
                    this.Tamano = 50;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "idModuloInterfazRol":
                    this.Descripcion = "ID";
                    this.Tamano = 50;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "UsuarioDecreacion":
                    this.Descripcion = "Creado por";
                    this.Tamano = 130;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "UsuarioDeCreacion":
                    this.Descripcion = "Creado por";
                    this.Tamano = 130;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "UsuarioDeModificacion":
                    this.Descripcion = "Modificado por";
                    this.Tamano = 130;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "FechaDeCreacion":
                    this.Descripcion = "Crado el";
                    this.Tamano = 130;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;                    
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;
                
                case "FechaDeModificacion":
                    this.Descripcion = "Modificado el";
                    this.Tamano = 130;
                    this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                case "Seleccionar":
                    this.Descripcion = " ";
                    this.Tamano = 50;
                    this.Alineacion = DataGridViewContentAlignment.MiddleCenter;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = false;
                    break;

                case "Marcar":
                    this.Descripcion = "";
                    this.Tamano = 50;
                    this.Alineacion = DataGridViewContentAlignment.MiddleCenter;
                    this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                    this.SoloLectura = true;
                    break;

                default: this.ValorEncontrado = false; break;

            }

        }

        public FormatoDGV(string NombreDelaColumna, string Tabla)
        {

            this.Columna = NombreDelaColumna;
            
            switch (Tabla)
            {
                case "ProductoUnidadDeMedida":

                    this.ValorEncontrado = true;

                    switch (Columna)
                    {
                        case "Abreviatura":
                            this.Descripcion = "Abreviatura";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Descripcion":
                            this.Descripcion = "Descripción";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Nombre":
                            this.Descripcion = "Unidad de Medida del producto";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "idProductoUnidadDeMedida":
                            this.Descripcion = "ID";
                            this.Tamano = 50;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleCenter;
                            this.SoloLectura = true;
                            break;

                        default: this.ValorEncontrado = false; break;
                    }

                    break;

                case "ProductoPresentacion":

                    this.ValorEncontrado = true;

                    switch (Columna)
                    {
                        case "Abreviatura":
                            this.Descripcion = "Abreviatura";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Descripcion":
                            this.Descripcion = "Descripción";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Nombre":
                            this.Descripcion = "Presentación del producto";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "idProductoPresentacion":
                            this.Descripcion = "ID";
                            this.Tamano = 50;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleCenter;
                            this.SoloLectura = true;
                            break;

                        default: this.ValorEncontrado = false; break;
                    }

                    break;

                case "Seccion":

                    this.ValorEncontrado = true;

                    switch (Columna)
                    {
                        
                        case "Descripcion":
                            this.Descripcion = "Descripción";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Nombre":
                            this.Descripcion = "Sección";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "idSeccion":
                            this.Descripcion = "ID";
                            this.Tamano = 50;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleCenter;
                            this.SoloLectura = true;
                            break;

                        default: this.ValorEncontrado = false; break;
                    }

                    break;

                case "Almacen":

                    this.ValorEncontrado = true;

                    switch (Columna)
                    {
                        case "PorDefecto":
                            this.Descripcion = "Por Defecto";
                            this.Tamano = 80;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleCenter;
                            this.SoloLectura = true;
                            break;

                        case "Descripcion":
                            this.Descripcion = "Descripción";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Nombre":
                            this.Descripcion = "Almacen";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "idAlmacen":
                            this.Descripcion = "ID";
                            this.Tamano = 50;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleCenter;
                            this.SoloLectura = true;
                            break;

                        default: this.ValorEncontrado = false; break;
                    }

                    break;

                case "Categoria":

                    this.ValorEncontrado = true;

                    switch (Columna)
                    {
                        case "Descripcion":
                            this.Descripcion = "Descripción";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Nombre":
                            this.Descripcion = "Categoria";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "idCategoria":
                            this.Descripcion = "ID";
                            this.Tamano = 50;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleCenter;
                            this.SoloLectura = true;
                            break;

                        default: this.ValorEncontrado = false; break;
                    }

                    break;

                case "Ubicacion":

                    this.ValorEncontrado = true;

                    switch (Columna)
                    {
                        case "TipoDeUbicacion":
                            this.Descripcion = "Tipo de Ubicación";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Descripcion":
                            this.Descripcion = "Descripción";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Nombre":
                            this.Descripcion = "Ubicación";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "idUbicacion":
                            this.Descripcion = "ID";
                            this.Tamano = 50;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleCenter;
                            this.SoloLectura = true;
                            break;

                        default: this.ValorEncontrado = false; break;
                    }

                    break;

                case "TipoDeUbicacion":

                    this.ValorEncontrado = true;

                    switch (Columna)
                    {
                        case "Descripcion":
                            this.Descripcion = "Descripción";
                            this.Tamano = 260;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Nombre":
                            this.Descripcion = "Tipo de Ubicación";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "idTipoDeUbicacion":
                            this.Descripcion = "ID";
                            this.Tamano = 50;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleCenter;
                            this.SoloLectura = true;
                            break;

                        default: this.ValorEncontrado = false; break;
                    }

                    break;

                case "Contacto":

                    this.ValorEncontrado = true;

                    switch (Columna)
                    {

                        case "Cedula":
                            this.Descripcion = "Cédula";
                            this.Tamano = 130;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Sexo":
                            this.Descripcion = "Sexo";
                            this.Tamano = 80;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Estado":
                            this.Descripcion = "Estado";
                            this.Tamano = 80;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Facebook":
                            this.Descripcion = "Facebook";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Twitter":
                            this.Descripcion = "Twitter";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Skype":
                            this.Descripcion = "Skype";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Messenger":
                            this.Descripcion = "Messenger";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "FechaDeCumpleanos":
                            this.Descripcion = "Fecha Festiva";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Correo":
                            this.Descripcion = "Correo Electrónico";
                            this.Tamano = 250;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Descripcion":
                            this.Descripcion = "Observaciones";
                            this.Tamano = 250;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Observaciones":
                            this.Descripcion = "Observaciones";
                            this.Tamano = 250;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Movil":
                            this.Descripcion = "Movil";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Telefono":
                            this.Descripcion = "Teléfono";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;
                            
                        case "Direccion":
                            this.Descripcion = "Dirección";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Contacto":
                            this.Descripcion = "Contacto";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Nombre":
                            this.Descripcion = "Contacto";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Codigo":
                            this.Descripcion = "Código";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "idContacto":
                            this.Descripcion = "ID";
                            this.Tamano = 50;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleCenter;
                            this.SoloLectura = true;
                            break;

                        default: this.ValorEncontrado = false; break;
                    }

                    break;

                case "Proveedor":

                    this.ValorEncontrado = true;
                    
                    switch (Columna)
                    {
                        
                        case "Estado":
                            this.Descripcion = "Estado";
                            this.Tamano = 80;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Facebook":
                            this.Descripcion = "Facebook";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;
                            
                        case "Twitter":
                            this.Descripcion = "Twitter";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Skype":
                            this.Descripcion = "Skype";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Messenger":
                            this.Descripcion = "Messenger";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "FechaDeCumpleanos":
                            this.Descripcion = "Fecha Festiva";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Correo":
                            this.Descripcion = "Correo Electrónico";
                            this.Tamano = 250;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Descripcion":
                            this.Descripcion = "Observaciones";
                            this.Tamano = 250;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Observaciones":
                            this.Descripcion = "Observaciones";
                            this.Tamano = 250;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Movil":
                            this.Descripcion = "Movil";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Telefono":
                            this.Descripcion = "Teléfono";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "SitioWeb":
                            this.Descripcion = "Sitio Web";
                            this.Tamano = 160;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "NoRUC":
                            this.Descripcion = "No RUC";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Direccion":
                            this.Descripcion = "Dirección";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Proveedor":
                            this.Descripcion = "Proveedor";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Nombre":
                            this.Descripcion = "Proveedor";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Codigo":
                            this.Descripcion = "Código";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "idProveedor":
                            this.Descripcion = "ID";
                            this.Tamano = 50;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleCenter;
                            this.SoloLectura = true;
                            break;
                            
                        default: this.ValorEncontrado = false; break;
                    }

                    break;

                case "Laboratorio":

                    this.ValorEncontrado = true;

                    switch (Columna)
                    {

                        case "Estado":
                            this.Descripcion = "Estado";
                            this.Tamano = 80;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Facebook":
                            this.Descripcion = "Facebook";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Twitter":
                            this.Descripcion = "Twitter";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Skype":
                            this.Descripcion = "Skype";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Messenger":
                            this.Descripcion = "Messenger";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "FechaDeCumpleanos":
                            this.Descripcion = "Fecha Festiva";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Correo":
                            this.Descripcion = "Correo Electrónico";
                            this.Tamano = 250;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Descripcion":
                            this.Descripcion = "Observaciones";
                            this.Tamano = 250;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Observaciones":
                            this.Descripcion = "Observaciones";
                            this.Tamano = 250;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Movil":
                            this.Descripcion = "Movil";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Telefono":
                            this.Descripcion = "Teléfono";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "SitioWeb":
                            this.Descripcion = "Sitio Web";
                            this.Tamano = 160;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "NoRUC":
                            this.Descripcion = "No RUC";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Direccion":
                            this.Descripcion = "Dirección";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Laboratorio":
                            this.Descripcion = "Laboratorio";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Nombre":
                            this.Descripcion = "Laboratorio";
                            this.Tamano = 200;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "Codigo":
                            this.Descripcion = "Código";
                            this.Tamano = 100;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;
                            break;

                        case "idLaboratorio":
                            this.Descripcion = "ID";
                            this.Tamano = 50;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleCenter;
                            this.SoloLectura = true;
                            break;

                        default: this.ValorEncontrado = false; break;
                    }

                    break;

                case "Rol":

                    this.ValorEncontrado = true;

                    switch (Columna)
                    {
                        case "idRol":
                            this.Descripcion = "ID";
                            this.Tamano = 50;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleCenter;
                            this.SoloLectura = true;

                            break;

                        case "Nombre":
                            this.Descripcion = "Tipo de cuenta";
                            this.Tamano = 350;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;

                            break;

                        case "Descripcion":
                            this.Descripcion = "Descripción de la cuenta";
                            this.Tamano = 300;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;

                            break;

                        default: this.ValorEncontrado = false; break;
                    }

                    break;
                    
                case "Usuario":

                    this.ValorEncontrado = true;

                    switch (Columna)
                    {
                        case "Estado":
                            this.Descripcion = "Estado";
                            this.Tamano = 160;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;

                            break;

                        case "TipoDeCuenta":
                            this.Descripcion = "Tipo de cuenta";
                            this.Tamano = 160;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;

                            break;

                        case "Email":
                            this.Descripcion = "Correo electronico";
                            this.Tamano = 160;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;

                            break;

                        case "Login":
                            this.Descripcion = "Nombre de sesión";
                            this.Tamano = 300;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;

                            break;

                        case "Usuario":
                            this.Descripcion = "Nombre del Usuario";
                            this.Tamano = 300;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;

                            break;

                        case "idUsuario":
                            this.Descripcion = "ID";
                            this.Tamano = 50;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;

                            break;

                        default: this.ValorEncontrado = false; break;
                    }

                    break;

                case "TasaDeCambio":

                    this.ValorEncontrado = true;

                    switch (Columna)
                    {
                        case "Cambio":
                            this.Descripcion = "Cambio";
                            this.Tamano = 130;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleRight;
                            this.SoloLectura = true;

                            break;

                        case "FechaDelCambio":
                            this.Descripcion = "Fecha del Cambio Oficial";
                            this.Tamano = 130;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;

                            break;
                            
                        case "idTasaDeCambio":
                            this.Descripcion = "ID";
                            this.Tamano = 50;
                            this.AlineacionDelEncabezado = DataGridViewContentAlignment.MiddleCenter;
                            this.Alineacion = DataGridViewContentAlignment.MiddleLeft;
                            this.SoloLectura = true;

                            break;

                        default: this.ValorEncontrado = false; break;
                    }

                    break;

                default: this.ValorEncontrado = false; break;

            }

        }
    }
}
