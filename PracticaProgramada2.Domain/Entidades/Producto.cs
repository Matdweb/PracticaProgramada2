namespace PracticaProgramada2.Domain;

public partial class Producto
{
    public int ProductoId { get; set; }

    public int CategoriaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual Categoria Categoria { get; set; } = null!;

    public bool ValidarReglaNegocioCategoriaValida()
    {
        if (CategoriaId > 0)
        {
            return true;
        }

        return false;
    }

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

    public bool ValidarReglaNegocioPrecioValido()
    {
        if (Precio >= 0)
        {
            return true;
        }

        return false;
    }

    public bool ValidarReglaNegocioStockValido()
    {
        if (Stock >= 0)
        {
            return true;
        }

        return false;
    }

    public bool ValidarReglasDeNegocio()
    {
        return ValidarReglaNegocioCategoriaValida()
            && ValidarReglaNegocioNombreValido()
            && ValidarReglaNegocioDescripcionValida()
            && ValidarReglaNegocioPrecioValido()
            && ValidarReglaNegocioStockValido();
    }
}
