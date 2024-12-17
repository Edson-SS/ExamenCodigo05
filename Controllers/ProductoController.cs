using ExamenCodigo.Models;
using ExamenCodigo.Request;
using ExamenCodigo.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                var responseProducto = context.Productos.Include(x=>x.categoria).Select(p => new ProductoResponse
                {
                    ProductoID = p.ProductoID,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    NombreCategoria = p.categoria.Nombre,
                }).ToList();
                return responseProducto;
            }
        }

        [HttpGet("{id}")]
        public ProductoResponse GetProductoID(int id)
        {
            using (var context = new DemoContext())
            {
                var producto = context.Productos
                    .Include(x => x.categoria)
                    .Where(y=> y.ProductoID==id)
                    .Select(p => new ProductoResponse
                    {
                        ProductoID = p.ProductoID,
                        Nombre = p.Nombre,
                        Precio = p.Precio,
                        NombreCategoria = p.categoria.Nombre,
                    }).FirstOrDefault();
                return producto;
            }
        }
    }
}
