using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PracticaProgramada1.Application.Dtos;

namespace PracticaProgramada1.Application.Services;

/// <summary>
/// Interfaz que define el contrato para las operaciones de negocio en Clientes.
/// </summary>
public interface IClientesServicio
{
    /// <summary>
    /// Obtiene todos los clientes.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Lista de DTOs de clientes.</returns>
    Task<List<ClientesDto>> ObtenerClientesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene un cliente por su identificador.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>DTO del cliente si existe, null si no.</returns>
    Task<ClientesDto?> ObtenerClientePorIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Crea un nuevo cliente.
    /// </summary>
    /// <param name="cliente">DTO del cliente a crear.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con el resultado de la operación.</returns>
    Task<Respuesta<ClientesDto>> CrearClienteAsync(ClientesDto cliente, CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza un cliente existente.
    /// </summary>
    /// <param name="cliente">DTO del cliente con datos actualizados.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con el resultado de la operación.</returns>
    Task<Respuesta<ClientesDto>> ActualizarClienteAsync(ClientesDto cliente, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina un cliente por su identificador.
    /// </summary>
    /// <param name="id">Identificador del cliente a eliminar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con el resultado de la operación.</returns>
    Task<Respuesta<ClientesDto>> EliminarClienteAsync(int id, CancellationToken cancellationToken = default);
}
