namespace PracticaProgramada2.Application.Dtos
{
    public class CategoriaDto
    {
        public int CategoriaId { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }
    }
}
