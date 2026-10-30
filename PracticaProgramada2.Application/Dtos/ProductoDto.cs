namespace PracticaProgramada2.Application.Dtos
{
    public class ProductoDto
    {
        public int ProductoId { get; set; }

        public int CategoriaId { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public DateTime FechaRegistro { get; set; }

        public CategoriaDto? Categoria { get; set; }
    }
}
