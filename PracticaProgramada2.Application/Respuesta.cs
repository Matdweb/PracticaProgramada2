using System;
using System.Collections.Generic;

namespace PracticaProgramada1.Application;

/// <summary>
/// Clase genérica que encapsula la respuesta de una operación en la aplicación.
/// Contiene el resultado de la operación, mensajes de estado y código de respuesta.
/// </summary>
/// <typeparam name="T">Tipo de dato que contiene la respuesta.</typeparam>
public class Respuesta<T>
{
    /// <summary>
    /// Indica si la operación fue exitosa.
    /// </summary>
    public bool EsCorrecto { get; set; } = true;

    /// <summary>
    /// Mensaje descriptivo de la operación (éxito o error).
    /// </summary>
    public string Mensaje { get; set; } = string.Empty;

    /// <summary>
    /// Código de estado HTTP o código personalizado de la operación.
    /// </summary>
    public int Codigo { get; set; } = 200;

    /// <summary>
    /// Dato retornado por la operación.
    /// </summary>
    public T Dato { get; set; }

    /// <summary>
    /// Lista de errores adicionales (si aplica).
    /// </summary>
    public List<string> Errores { get; set; } = new List<string>();
}
