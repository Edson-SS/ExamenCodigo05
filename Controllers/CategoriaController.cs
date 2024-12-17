using ExamenCodigo.Models;
using ExamenCodigo.Request;
using ExamenCodigo.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ExamenCodigo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        [HttpPost]
        public ResponseBase InsertCategoria(CategoriaRequest _categoria)
        {
            ResponseBase response = new ResponseBase();
            try
            {
                using (var context = new DemoContext())
                {
                    Categoria categoria = new Categoria
                    {
                        Nombre = _categoria.Nombre,
                        Descripcion = _categoria.Descripcion,
                    };
                    context.Categorias.Add(categoria);
                    context.SaveChanges();

                    response.Mensaje = "Registro exitoso";
                    response.CodigoError = 0;
                }
            }
            catch (Exception ex)
            {
                response.Mensaje = ex.ToString();
                response.CodigoError = 400;
            }
            return response;
        }

        [HttpGet]
        public List<CategoriaResponse> GetCategoria()
        {
            using (var context = new DemoContext())
            {
                var categorias = context.Categorias.ToList();
                var responseCategoria = categorias.Select(c => new CategoriaResponse
                {
                    CategoriaID = c.CategoriaID,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion,
                }).ToList();
                return responseCategoria;
            }
        }

        [HttpGet("{id}")]
        public CategoriaResponse GetCategoriaID(int id)
        {
            using (var context = new DemoContext())
            {
                var categoria = context.Categorias.Find(id);
                CategoriaResponse categoriaResponse = new CategoriaResponse
                {
                    CategoriaID = categoria.CategoriaID,
                    Nombre = categoria.Nombre,
                    Descripcion = categoria.Descripcion,
                };
                return categoriaResponse;
            }
        }
    }
}
