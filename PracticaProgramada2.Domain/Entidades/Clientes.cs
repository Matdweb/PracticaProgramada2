using System;
using System.Collections.Generic;

namespace PracticaProgramada1.Domain;

public partial class Clientes
{

    public int ClienteId { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string SegundoApellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string FechaRegistro { get; set; } = null!;

    public virtual ICollection<Telefonos> Telefonos { get; set; } = new List<Telefonos>();

    public bool ValidarReglaNegocioClienteActivo()
    {
        return true;
    }

    public bool ValidarReglasDeNegocio()
    {
        return ValidarReglaNegocioClienteActivo();
    }
}
