using ExamenCodigo.Models;
using System.Globalization;

namespace ExamenCodigo.Response
{
    public class ProductoResponse
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string NombreCategoria { get; set; }
    }
}
