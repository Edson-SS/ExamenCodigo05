using ExamenCodigo.Models;
using ExamenCodigo.Request;
using ExamenCodigo.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamenCodigo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpPost]
        public ResponseBase InsertProducto(ProductoRequest _producto)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                using (var context = new DemoContext())
                {
                    Producto producto = new Producto
                    {
                        Nombre = _producto.Nombre,
                        Precio = _producto.Precio,
                        CategoriaID = _producto.CategoriaID,
                    };
                    context.Productos.Add(producto);
                    context.SaveChanges();
                }
                response.Mensaje = "Registro exitoso";
                response.CodigoError = 0;
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.ToString();
                response.CodigoError = 400;
            }
            return response;
        }

        [HttpGet]
        public List<ProductoResponse> GetProducto()
        {
            using (var context = new DemoContext())
            {
                var producto = context.Productos.ToList();
                var responseProducto = producto.Select(p => new ProductoResponse
                {
                    ProductoID = p.ProductoID,
                    Nombre= p.Nombre,
                    Precio= p.Precio,
                }).ToList();
                return responseProducto;
            }
        }

        [HttpGet("{id}")]
        public ProductoResponse GetProductoID(int id)
        {
            using (var context = new DemoContext())
            {
                var producto = context.Productos.Find(id);
                var categoria = context.Categorias.Find(producto.CategoriaID);
                ProductoResponse productoResponse = new ProductoResponse
                {
                    ProductoID = producto.ProductoID,
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    NombreCategoria = categoria.Nombre,
                };
                return productoResponse;
            }
        }
    }
}
