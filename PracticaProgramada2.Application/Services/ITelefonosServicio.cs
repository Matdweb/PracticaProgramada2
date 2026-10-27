using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PracticaProgramada1.Application.Dtos;

namespace PracticaProgramada1.Application.Services;

/// <summary>
/// Interfaz que define el contrato para las operaciones de negocio en Telefonos.
/// </summary>
public interface ITelefonosServicio
{
    /// <summary>
    /// Obtiene todos los teléfonos.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Lista de DTOs de teléfonos.</returns>
    Task<List<TelefonosDto>> ObtenerTelefonosAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene un teléfono por su identificador.
    /// </summary>
    /// <param name="id">Identificador del teléfono.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>DTO del teléfono si existe, null si no.</returns>
    Task<TelefonosDto?> ObtenerTelefonoPorIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los teléfonos de un cliente específico.
    /// </summary>
    /// <param name="clienteId">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Lista de DTOs de teléfonos del cliente.</returns>
    Task<List<TelefonosDto>> ObtenerTelefonosPorClienteIdAsync(int clienteId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Crea un nuevo teléfono.
    /// </summary>
    /// <param name="telefono">DTO del teléfono a crear.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con el resultado de la operación.</returns>
    Task<Respuesta<TelefonosDto>> CrearTelefonoAsync(TelefonosDto telefono, CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza un teléfono existente.
    /// </summary>
    /// <param name="telefono">DTO del teléfono con datos actualizados.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con el resultado de la operación.</returns>
    Task<Respuesta<TelefonosDto>> ActualizarTelefonoAsync(TelefonosDto telefono, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina un teléfono por su identificador.
    /// </summary>
    /// <param name="id">Identificador del teléfono a eliminar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con el resultado de la operación.</returns>
    Task<Respuesta<TelefonosDto>> EliminarTelefonoAsync(int id, CancellationToken cancellationToken = default);
}
