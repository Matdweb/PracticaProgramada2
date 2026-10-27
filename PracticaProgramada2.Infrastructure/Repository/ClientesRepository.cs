using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PracticaProgramada1.Domain;
using PracticaProgramada1.Infrastructure.Data;

namespace PracticaProgramada1.Infrastructure.Repository;

/// <summary>
/// Implementación del repositorio para la entidad Clientes.
/// Proporciona métodos para realizar operaciones CRUD.
/// </summary>
public class ClientesRepository : IClientesRepository
{
    /// <summary>
    /// Contexto de la base de datos inyectado por dependencia.
    /// </summary>
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Inicializa una nueva instancia de ClientesRepository.
    /// </summary>
    /// <param name="context">Contexto de la base de datos.</param>
    public ClientesRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todos los clientes de forma asincrónica.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Lista de todos los clientes con sus teléfonos relacionados.</returns>
    public async Task<List<Clientes>> GetAllClientesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Clientes
            .AsNoTracking()
            .Include(c => c.Telefonos)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Obtiene un cliente por su identificador.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Cliente si existe, objeto vacío si no.</returns>
    public async Task<Clientes> GetClienteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var cliente = await _context.Clientes
            .Include(c => c.Telefonos)
            .FirstOrDefaultAsync(c => c.ClienteId == id, cancellationToken);

        if (cliente == null)
        {
            return new Clientes(); // Retorna un objeto vacío para evitar null reference exception
        }

        return cliente;
    }

    /// <summary>
    /// Crea un nuevo cliente.
    /// </summary>
    /// <param name="cliente">Cliente a crear.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si se creó correctamente, False si falló.</returns>
    public async Task<bool> CreateClienteAsync(Clientes cliente, CancellationToken cancellationToken = default)
    {
        _context.Clientes.Add(cliente);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    /// <summary>
    /// Actualiza un cliente existente.
    /// </summary>
    /// <param name="cliente">Cliente con datos actualizados.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si se actualizó correctamente, False si falló o no existe.</returns>
    public async Task<bool> UpdateClienteAsync(Clientes cliente, CancellationToken cancellationToken = default)
    {
        var clienteExistente = await _context.Clientes
            .FirstOrDefaultAsync(c => c.ClienteId == cliente.ClienteId, cancellationToken);

        if (clienteExistente is null)
        {
            return false;
        }

        clienteExistente.Cedula = cliente.Cedula;
        clienteExistente.Nombre = cliente.Nombre;
        clienteExistente.PrimerApellido = cliente.PrimerApellido;
        clienteExistente.SegundoApellido = cliente.SegundoApellido;
        clienteExistente.Correo = cliente.Correo;
        clienteExistente.Direccion = cliente.Direccion;

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    /// <summary>
    /// Elimina un cliente por su identificador.
    /// </summary>
    /// <param name="id">Identificador del cliente a eliminar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>True si se eliminó correctamente, False si falló o no existe.</returns>
    public async Task<bool> DeleteClienteAsync(int id, CancellationToken cancellationToken = default)
    {
        var cliente = await _context.Clientes.FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        if (cliente == null)
        {
            return false;
        }

        _context.Clientes.Remove(cliente);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
