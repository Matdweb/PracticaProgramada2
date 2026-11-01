using Microsoft.AspNetCore.Mvc;
using PracticaProgramada2.Application.Dtos;
using PracticaProgramada2.Application.Services;

namespace PracticaProgramada2.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoServicio _productoServicio;

        public ProductoController(IProductoServicio productoServicio)
        {
            _productoServicio = productoServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductos(CancellationToken cancellationToken)
        {
            var productos = await _productoServicio.ObtenerProductosAsync(cancellationToken);
            return Json(productos);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductoPorId(int id, CancellationToken cancellationToken)
        {
            var producto = await _productoServicio.ObtenerProductoPorIdAsync(id, cancellationToken);

            if (producto is null)
            {
                return NotFound();
            }

            return Json(producto);
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto(ProductoDto producto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var respuesta = await _productoServicio.CrearProductoAsync(producto, cancellationToken);

            return Json(respuesta);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarProducto(ProductoDto producto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var respuesta = await _productoServicio.ActualizarProductoAsync(producto, cancellationToken);

            return Json(respuesta);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarProducto(int id, CancellationToken cancellationToken)
        {
            var respuesta = await _productoServicio.EliminarProductoAsync(id, cancellationToken);

            return Json(respuesta);
        }
    }
}