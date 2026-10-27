using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PracticaProgramada1.Application.Dtos;
using PracticaProgramada1.Domain;
using PracticaProgramada1.Infrastructure.Repository;

namespace PracticaProgramada1.Application.Services;

/// <summary>
/// Servicio que implementa la lógica de negocio para Telefonos.
/// </summary>
public class TelefonosServicio : ITelefonosServicio
{
    /// <summary>
    /// Repositorio de teléfonos inyectado por dependencia (inversión de dependencias).
    /// </summary>
    private readonly ITelefonosRepository _telefonosRepository;

    /// <summary>
    /// Mapper de AutoMapper para convertir entre entidades y DTOs.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Inicializa una nueva instancia de TelefonosServicio.
    /// </summary>
    /// <param name="telefonosRepository">Repositorio de teléfonos.</param>
    /// <param name="mapper">Mapper de AutoMapper.</param>
    public TelefonosServicio(ITelefonosRepository telefonosRepository, IMapper mapper)
    {
        _telefonosRepository = telefonosRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene todos los teléfonos.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Lista de DTOs de teléfonos.</returns>
    public async Task<List<TelefonosDto>> ObtenerTelefonosAsync(CancellationToken cancellationToken = default)
    {
        var telefonos = await _telefonosRepository.GetAllTelefonosAsync(cancellationToken);
        var telefonosDto = _mapper.Map<List<TelefonosDto>>(telefonos);

        return telefonosDto;
    }

    /// <summary>
    /// Obtiene un teléfono por su identificador.
    /// </summary>
    /// <param name="id">Identificador del teléfono.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>DTO del teléfono si existe, null si no.</returns>
    public async Task<TelefonosDto?> ObtenerTelefonoPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var telefono = await _telefonosRepository.GetTelefonoByIdAsync(id, cancellationToken);

        var telefonoDto = _mapper.Map<TelefonosDto>(telefono);

        return telefonoDto;
    }

    /// <summary>
    /// Obtiene todos los teléfonos de un cliente específico.
    /// </summary>
    /// <param name="clienteId">Identificador del cliente.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Lista de DTOs de teléfonos del cliente.</returns>
    public async Task<List<TelefonosDto>> ObtenerTelefonosPorClienteIdAsync(int clienteId, CancellationToken cancellationToken = default)
    {
        var telefonos = await _telefonosRepository.GetTelefonosByClienteIdAsync(clienteId, cancellationToken);
        var telefonosDto = _mapper.Map<List<TelefonosDto>>(telefonos);

        return telefonosDto;
    }

    /// <summary>
    /// Crea un nuevo teléfono.
    /// </summary>
    /// <param name="telefono">DTO del teléfono a crear.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con el resultado de la operación.</returns>
    public async Task<Respuesta<TelefonosDto>> CrearTelefonoAsync(TelefonosDto telefono, CancellationToken cancellationToken = default)
    {
        var respuesta = new Respuesta<TelefonosDto>();

        var telefonoEntity = _mapper.Map<Telefonos>(telefono);

        // Validar la regla de negocio del teléfono
        if (!telefonoEntity.ValidarReglasDeNegocio())
        {
            respuesta.EsCorrecto = false;
            respuesta.Mensaje = "El teléfono no cumple con las reglas de negocio requeridas.";
            respuesta.Codigo = 400;
            return respuesta;
        }

        // Validar el proceso de creación del teléfono
        if (!await _telefonosRepository.CreateTelefonoAsync(telefonoEntity, cancellationToken))
        {
            respuesta.EsCorrecto = false;
            respuesta.Mensaje = "No se pudo crear el teléfono.";
            respuesta.Codigo = 500;
            return respuesta;
        }

        respuesta.Dato = _mapper.Map<TelefonosDto>(telefonoEntity);
        respuesta.Mensaje = "Teléfono creado exitosamente.";
        respuesta.Codigo = 201;

        return respuesta;
    }

    /// <summary>
    /// Actualiza un teléfono existente.
    /// </summary>
    /// <param name="telefono">DTO del teléfono con datos actualizados.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con el resultado de la operación.</returns>
    public async Task<Respuesta<TelefonosDto>> ActualizarTelefonoAsync(TelefonosDto telefono, CancellationToken cancellationToken = default)
    {
        var respuesta = new Respuesta<TelefonosDto>();

        var telefonoEntity = _mapper.Map<Telefonos>(telefono);

        // Validar la regla de negocio del teléfono
        if (!telefonoEntity.ValidarReglasDeNegocio())
        {
            respuesta.EsCorrecto = false;
            respuesta.Mensaje = "El teléfono no cumple con las reglas de negocio requeridas.";
            respuesta.Codigo = 400;
            return respuesta;
        }

        // Validar el proceso de actualización
        if (!await _telefonosRepository.UpdateTelefonoAsync(telefonoEntity, cancellationToken))
        {
            respuesta.EsCorrecto = false;
            respuesta.Mensaje = "No se pudo actualizar el teléfono.";
            respuesta.Codigo = 500;
            return respuesta;
        }

        respuesta.Dato = _mapper.Map<TelefonosDto>(telefonoEntity);
        respuesta.Mensaje = "Teléfono actualizado exitosamente.";

        return respuesta;
    }

    /// <summary>
    /// Elimina un teléfono por su identificador.
    /// </summary>
    /// <param name="id">Identificador del teléfono a eliminar.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Respuesta con el resultado de la operación.</returns>
    public async Task<Respuesta<TelefonosDto>> EliminarTelefonoAsync(int id, CancellationToken cancellationToken = default)
    {
        var respuesta = new Respuesta<TelefonosDto>();

        if (!await _telefonosRepository.DeleteTelefonoAsync(id, cancellationToken))
        {
            respuesta.EsCorrecto = false;
            respuesta.Mensaje = "No se pudo eliminar el teléfono.";
            respuesta.Codigo = 500;
            return respuesta;
        }

        respuesta.Mensaje = "Teléfono eliminado exitosamente.";

        return respuesta;
    }
}
