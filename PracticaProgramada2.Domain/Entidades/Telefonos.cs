using System;
using System.Collections.Generic;

namespace PracticaProgramada1.Domain;

public partial class Telefonos
{

    public int TelefonoId { get; set; }

    public int ClienteId { get; set; }

    public string Numero { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string FechaRegistro { get; set; } = null!;

    public virtual Clientes FkclienteNavigation { get; set; } = null!;

    public bool ValidarReglaNegocioTelefonoValido()
    {
        return !string.IsNullOrWhiteSpace(Numero);
    }

    public bool ValidarReglasDeNegocio()
    {
        return ValidarReglaNegocioTelefonoValido();
    }
}
