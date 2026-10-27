using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PracticaProgramada1.Application.Dtos;
using PracticaProgramada1.Domain;
using PracticaProgramada1.Infrastructure.Repository;

namespace PracticaProgramada1.Application.Services;

/// <summary>
/// Servicio que implementa la lógica de negocio para Clientes.
/// </summary>
public class ClientesServicio : IClientesServicio
{
    /// <summary>
    /// Repositorio de clientes inyectado por dependencia (inversión de dependencias).
    /// </summary>
    private readonly IClientesRepository _clientesRepository;

    /// <summary>
    /// Mapper de AutoMapper para convertir entre entidades y DTOs.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Inicializa una nueva instancia de ClientesServicio.
    /// </summary>
    /// <param name="clientesRepository">Repositorio de clientes.</param>
    /// <param name="mapper">Mapper de AutoMapper.</param>
    public ClientesServicio(IClientesRepository clientesRepository, IMapper mapper)
    {
        _clientesRepository = clientesRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene todos los clientes.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Lista de DTOs de clientes.</returns>
    public async Task<List<ClientesDto>> ObtenerClientesAsync(CancellationToken cancellationToken = default)
    {
        var clientes = await _clientesRepository.GetAllClientesAsync(cancellationToken);
        var clientesDto = _mapper.Map<List<ClientesDto>>(clientes);

        return clientesDto;
    }

    /// <summary>
    /// Obtiene un cliente por su identificador.
    /// </summary>
    /// <param name="id">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>DTO del cliente si existe, null si no.</returns>
    public async Task<ClientesDto?> ObtenerClientePorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var cliente = await _clientesRepository.GetClienteByIdAsync(id, cancellationToken);

        var clienteDto = _mapper.Map<ClientesDto>(cliente);

        return clienteDto;
    }

    /// <summary>
    /// Crea un nuevo cliente.
    /// </summary>
    /// <param name="cliente">DTO del cliente a crear.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con el resultado de la operación.</returns>
    public async Task<Respuesta<ClientesDto>> CrearClienteAsync(ClientesDto cliente, CancellationToken cancellationToken = default)
    {
        var respuesta = new Respuesta<ClientesDto>();

        var clienteEntity = _mapper.Map<Clientes>(cliente);

        // Validar la regla de negocio del cliente
        if (!clienteEntity.ValidarReglasDeNegocio())
        {
            respuesta.EsCorrecto = false;
            respuesta.Mensaje = "El cliente no cumple con las reglas de negocio requeridas.";
            respuesta.Codigo = 400;
            return respuesta;
        }

        // Validar el proceso de creación del cliente
        if (!await _clientesRepository.CreateClienteAsync(clienteEntity, cancellationToken))
        {
            respuesta.EsCorrecto = false;
            respuesta.Mensaje = "No se pudo crear el cliente.";
            respuesta.Codigo = 500;
            return respuesta;
        }

        respuesta.Dato = _mapper.Map<ClientesDto>(clienteEntity);
        respuesta.Mensaje = "Cliente creado exitosamente.";
        respuesta.Codigo = 201;

        return respuesta;
    }

    /// <summary>
    /// Actualiza un cliente existente.
    /// </summary>
    /// <param name="cliente">DTO del cliente con datos actualizados.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con el resultado de la operación.</returns>
    public async Task<Respuesta<ClientesDto>> ActualizarClienteAsync(ClientesDto cliente, CancellationToken cancellationToken = default)
    {
        var respuesta = new Respuesta<ClientesDto>();

        var clienteEntity = _mapper.Map<Clientes>(cliente);

        // Validar la regla de negocio del cliente
        if (!clienteEntity.ValidarReglasDeNegocio())
        {
            respuesta.EsCorrecto = false;
            respuesta.Mensaje = "El cliente no cumple con las reglas de negocio requeridas.";
            respuesta.Codigo = 400;
            return respuesta;
        }

        // Validar el proceso de actualización
        if (!await _clientesRepository.UpdateClienteAsync(clienteEntity, cancellationToken))
        {
            respuesta.EsCorrecto = false;
            respuesta.Mensaje = "No se pudo actualizar el cliente.";
            respuesta.Codigo = 500;
            return respuesta;
        }

        respuesta.Dato = _mapper.Map<ClientesDto>(clienteEntity);
        respuesta.Mensaje = "Cliente actualizado exitosamente.";

        return respuesta;
    }

    /// <summary>
    /// Elimina un cliente por su identificador.
    /// </summary>
    /// <param name="id">Identificador del cliente a eliminar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con el resultado de la operación.</returns>
    public async Task<Respuesta<ClientesDto>> EliminarClienteAsync(int id, CancellationToken cancellationToken = default)
    {
        var respuesta = new Respuesta<ClientesDto>();

        if (!await _clientesRepository.DeleteClienteAsync(id, cancellationToken))
        {
            respuesta.EsCorrecto = false;
            respuesta.Mensaje = "No se pudo eliminar el cliente.";
            respuesta.Codigo = 500;
            return respuesta;
        }

        respuesta.Mensaje = "Cliente eliminado exitosamente.";

        return respuesta;
    }
}
