using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PracticaProgramada1.Domain;

namespace PracticaProgramada1.Infrastructure.Repository;

/// <summary>
/// Interfaz que define el contrato para las operaciones CRUD de Clientes.
/// </summary>
public interface IClientesRepository
{
    /// <summary>
    /// Obtiene todos los clientes de forma asincrónica.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Lista de clientes.</returns>
    Task<List<Clientes>> GetAllClientesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene un cliente por su identificador.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Cliente si existe, objeto vacío si no existe.</returns>
    Task<Clientes> GetClienteByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Crea un nuevo cliente.
    /// </summary>
    /// <param name="cliente">Cliente a crear.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si se creó correctamente, False si falló.</returns>
    Task<bool> CreateClienteAsync(Clientes cliente, CancellationToken cancellationToken = default);

    /// <summary>
    /// Actualiza un cliente existente.
    /// </summary>
    /// <param name="cliente">Cliente con datos actualizados.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si se actualizó correctamente, False si falló o no existe.</returns>
    Task<bool> UpdateClienteAsync(Clientes cliente, CancellationToken cancellationToken = default);

    /// <summary>
    /// Elimina un cliente por su identificador.
    /// </summary>
    /// <param name="id">Identificador del cliente a eliminar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si se eliminó correctamente, False si falló o no existe.</returns>
    Task<bool> DeleteClienteAsync(int id, CancellationToken cancellationToken = default);
}
