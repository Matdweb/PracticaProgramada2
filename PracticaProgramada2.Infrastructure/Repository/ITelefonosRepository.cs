using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PracticaProgramada1.Domain;

namespace PracticaProgramada1.Infrastructure.Repository;

/// <summary>
/// Interfaz que define el contrato para las operaciones CRUD de Telefonos.
/// </summary>
public interface ITelefonosRepository
{
    /// <summary>
    /// Obtiene todos los teléfonos de forma asincrónica.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Lista de teléfonos.</returns>
    Task<List<Telefonos>> GetAllTelefonosAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene un teléfono por su identificador.
    /// </summary>
    /// <param name="id">Identificador del teléfono.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Teléfono si existe, objeto vacío si no existe.</returns>
    Task<Telefonos> GetTelefonoByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todos los teléfonos de un cliente específico.
    /// </summary>
    /// <param name="clienteId">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Lista de teléfonos del cliente.</returns>
    Task<List<Telefonos>> GetTelefonosByClienteIdAsync(int clienteId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Crea un nuevo teléfono.
    /// </summary>
    /// <param name="telefono">Teléfono a crear.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si se creó correctamente, False si falló.</returns>
    Task<bool> CreateTelefonoAsync(Telefonos telefono, CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza un teléfono existente.
    /// </summary>
    /// <param name="telefono">Teléfono con datos actualizados.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si se actualizó correctamente, False si falló o no existe.</returns>
    Task<bool> UpdateTelefonoAsync(Telefonos telefono, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina un teléfono por su identificador.
    /// </summary>
    /// <param name="id">Identificador del teléfono a eliminar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si se eliminó correctamente, False si falló o no existe.</returns>
    Task<bool> DeleteTelefonoAsync(int id, CancellationToken cancellationToken = default);
}
