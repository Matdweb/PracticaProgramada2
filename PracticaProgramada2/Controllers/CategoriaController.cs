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

        [HttpGet]
        public async Task<IActionResult> ObtenerCategoriaPorId(int id, CancellationToken cancellationToken)
        {
            var categoria = await _categoriaServicio.ObtenerCategoriaPorIdAsync(id, cancellationToken);

            if (categoria is null)
            {
                return NotFound();
            }

            return Json(categoria);
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

        [HttpPost]
        public async Task<IActionResult> ActualizarCategoria(CategoriaDto categoria, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var respuesta = await _categoriaServicio.ActualizarCategoriaAsync(categoria, cancellationToken);

            return Json(respuesta);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarCategoria(int id, CancellationToken cancellationToken)
        {
            var respuesta = await _categoriaServicio.EliminarCategoriaAsync(id, cancellationToken);

            return Json(respuesta);
        }
    }
}