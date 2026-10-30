namespace PracticaProgramada2.Domain;

public partial class Categoria
{
    public int CategoriaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();

    public bool ValidarReglaNegocioNombreValido()
    {
        if (!string.IsNullOrWhiteSpace(Nombre))
        {
            return true;
        }

        return false;
    }

    public bool ValidarReglaNegocioDescripcionValida()
    {
        if (!string.IsNullOrWhiteSpace(Descripcion))
        {
            return true;
        }

        return false;
    }

    public bool ValidarReglasDeNegocio()
    {
        return ValidarReglaNegocioNombreValido()
            && ValidarReglaNegocioDescripcionValida();
    }
}
