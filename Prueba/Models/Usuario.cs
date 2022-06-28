using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Prueba.Models;

namespace Prueba.Models
{
    [MetadataType(typeof(Usuario))]
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Nombre { get; set; }
        [StringLength(50), Required]
        public string Direccion { get; set; }
        [Range(0, 9999)]
        public int Telefono { get; set; }
        [StringLength(50), Required]
        public string UsuarioP { get; set; }
        [StringLength(50), Required]
        public string Identificacion { get; set; }
        [StringLength(50), Required]
        public string Contrasena { get; set; }
        [Range(0, 9999)]
        public int idTipoUsuario { get; set; }
        [StringLength(50), Required]
        public string ConfirmarContrasena { get; set; }

    }
}