using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PracticaProgramada1.Domain;
using PracticaProgramada1.Infrastructure.Data;

namespace PracticaProgramada1.Infrastructure.Repository;

/// <summary>
/// Implementación del repositorio para la entidad Telefonos.
/// Proporciona métodos para realizar operaciones CRUD.
/// </summary>
public class TelefonosRepository : ITelefonosRepository
{
    /// <summary>
    /// Contexto de la base de datos inyectado por dependencia.
    /// </summary>
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Inicializa una nueva instancia de TelefonosRepository.
    /// </summary>
    /// <param name="context">Contexto de la base de datos.</param>
    public TelefonosRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todos los teléfonos de forma asincrónica.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Lista de todos los teléfonos con sus clientes relacionados.</returns>
    public async Task<List<Telefonos>> GetAllTelefonosAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Telefonos
            .AsNoTracking()
            .Include(t => t.FkclienteNavigation)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Obtiene un teléfono por su identificador.
    /// </summary>
    /// <param name="id">Identificador del teléfono.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Teléfono si existe, objeto vacío si no.</returns>
    public async Task<Telefonos> GetTelefonoByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var telefono = await _context.Telefonos
            .Include(t => t.FkclienteNavigation)
            .FirstOrDefaultAsync(t => t.TelefonoId == id, cancellationToken);

        if (telefono == null)
        {
            return new Telefonos(); // Retorna un objeto vacío para evitar null reference exception
        }

        return telefono;
    }

    /// <summary>
    /// Obtiene todos los teléfonos de un cliente específico.
    /// </summary>
    /// <param name="clienteId">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Lista de teléfonos del cliente.</returns>
    public async Task<List<Telefonos>> GetTelefonosByClienteIdAsync(int clienteId, CancellationToken cancellationToken = default)
    {
        return await _context.Telefonos
            .AsNoTracking()
            .Where(t => t.ClienteId == clienteId)
            .Include(t => t.FkclienteNavigation)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Crea un nuevo teléfono.
    /// </summary>
    /// <param name="telefono">Teléfono a crear.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si se creó correctamente, False si falló.</returns>
    public async Task<bool> CreateTelefonoAsync(Telefonos telefono, CancellationToken cancellationToken = default)
    {
        _context.Telefonos.Add(telefono);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    /// <summary>
    /// Actualiza un teléfono existente.
    /// </summary>
    /// <param name="telefono">Teléfono con datos actualizados.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si se actualizó correctamente, False si falló o no existe.</returns>
    public async Task<bool> UpdateTelefonoAsync(Telefonos telefono, CancellationToken cancellationToken = default)
    {
        var telefonoExistente = await _context.Telefonos
            .FirstOrDefaultAsync(t => t.TelefonoId == telefono.TelefonoId, cancellationToken);

        if (telefonoExistente is null)
        {
            return false;
        }

        telefonoExistente.ClienteId = telefono.ClienteId;
        telefonoExistente.Numero = telefono.Numero;
        telefonoExistente.Tipo = telefono.Tipo;

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    /// <summary>
    /// Elimina un teléfono por su identificador.
    /// </summary>
    /// <param name="id">Identificador del teléfono a eliminar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si se eliminó correctamente, False si falló o no existe.</returns>
    public async Task<bool> DeleteTelefonoAsync(int id, CancellationToken cancellationToken = default)
    {
        var telefono = await _context.Telefonos.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        if (telefono == null)
        {
            return false;
        }

        _context.Telefonos.Remove(telefono);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
