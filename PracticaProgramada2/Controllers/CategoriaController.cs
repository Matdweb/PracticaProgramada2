using Microsoft.AspNetCore.Mvc;
using PracticaProgramada2.Application.Dtos;
using PracticaProgramada2.Application.Services;

namespace PracticaProgramada2.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaServicio _categoriaServicio;

        public CategoriaController(ICategoriaServicio categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ObtenerCategorias(CancellationToken cancellationToken)
        {
            var categorias = await _categoriaServicio.ObtenerCategoriasAsync(cancellationToken);
            return Json(categorias);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoria(CategoriaDto categoria, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var respuesta = await _categoriaServicio.CrearCategoriaAsync(categoria, cancellationToken);

            return Json(respuesta);
        }
    }
}
