using Prueba.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prueba.Models
{
    public class ListProducto
    {
        public int IdProducto { get; set; }
        [Required]
        public string NombreProducto { get; set; }
        [Required]
        public int CantidadDisponible { get; set; }
        [Required]
        public string Descripcion { get; set; }

        public int CantidadComprar { get; set; }
    }
}