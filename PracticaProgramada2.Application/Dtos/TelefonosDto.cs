using System;
using System.Collections.Generic;

namespace PracticaProgramada1.Application.Dtos;

/// <summary>
/// DTO para la entidad Telefonos. Se utiliza para transferencia de datos entre capas.
/// </summary>
public class TelefonosDto
{
    /// <summary>
    /// Identificador único del teléfono.
    /// </summary>
    public int TelefonoId { get; set; }

    /// <summary>
    /// Identificador del cliente al que pertenece el teléfono.
    /// </summary>
    public int ClienteId { get; set; }

    /// <summary>
    /// Número de teléfono.
    /// </summary>
    public string Numero { get; set; } = null!;

    /// <summary>
    /// Tipo de teléfono (Celular, Casa, Oficina, etc.).
    /// </summary>
    public string Tipo { get; set; } = null!;

    /// <summary>
    /// Fecha de registro del teléfono.
    /// </summary>
    public DateTime? FechaRegistro { get; set; }
}
