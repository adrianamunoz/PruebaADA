//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class UsuarioPrueba
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UsuarioPrueba()
        {
            this.UsuarioProducto = new HashSet<UsuarioProducto>();
        }
    
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public Nullable<int> Telefono { get; set; }
        public string Usuario { get; set; }
        public string Identificacion { get; set; }
        public string contraseña { get; set; }
        public Nullable<short> idTipoUsuario { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioProducto> UsuarioProducto { get; set; }
    }
}