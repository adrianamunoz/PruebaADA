using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prueba.Models
{
    public class UsuarioCompra
    {
        public int IdUsuarioProducto { get; set; }
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCompra { get; set; }
    }
}