﻿using Microsoft.Extensions.Hosting;

namespace ExamenCodigo.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }

        public ICollection<Producto> productos { get; } = new List<Producto>();

    }
}
