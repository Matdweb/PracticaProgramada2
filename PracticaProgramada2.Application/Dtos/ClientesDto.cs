using System;
using System.Collections.Generic;

namespace PracticaProgramada1.Application.Dtos;

/// <summary>
/// DTO para la entidad Clientes. Se utiliza para transferencia de datos entre capas.
/// </summary>
public class ClientesDto
{
    /// <summary>
    /// Identificador único del cliente.
    /// </summary>
    public int ClienteId { get; set; }

    /// <summary>
    /// Número de cédula del cliente.
    /// </summary>
    public string Cedula { get; set; } = null!;

    /// <summary>
    /// Nombre del cliente.
    /// </summary>
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Primer apellido del cliente.
    /// </summary>
    public string PrimerApellido { get; set; } = null!;

    /// <summary>
    /// Segundo apellido del cliente.
    /// </summary>
    public string SegundoApellido { get; set; } = null!;

    /// <summary>
    /// Correo electrónico del cliente.
    /// </summary>
    public string Correo { get; set; } = null!;

    /// <summary>
    /// Dirección del cliente.
    /// </summary>
    public string Direccion { get; set; } = null!;

    /// <summary>
    /// Fecha de registro del cliente.
    /// </summary>
    public DateTime? FechaRegistro { get; set; }

    /// <summary>
    /// Lista de DTOs de teléfonos asociados al cliente.
    /// </summary>
    public List<TelefonosDto> Telefonos { get; set; } = new List<TelefonosDto>();
}
