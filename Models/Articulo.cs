using System;
using System.Collections.Generic;

#nullable disable

namespace WinFormsApp3.Models
{
    public partial class Articulo
    {
        public long ArticuloId { get; set; }
        public string Nombre { get; set; }
        public byte[] Precio { get; set; }
        public long? ProveedorId { get; set; }
    }
}
