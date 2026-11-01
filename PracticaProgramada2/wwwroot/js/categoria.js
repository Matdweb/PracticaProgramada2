(() => {

    const Categoria = {
        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
        },

        inicializarTabla() {
            this.tabla = new DataTable('#tblCategorias', {
                ajax: {
                    url: '/Categoria/ObtenerCategorias',
                    dataSrc: '',
                    type: 'GET'
                },
                columns: [
                    { data: 'categoriaId' },
                    { data: 'nombre', render: DataTable.render.text() },
                    { data: 'descripcion', render: DataTable.render.text() },
                    {
                        data: 'fechaRegistro',
                        render: function (fecha) {
                            return Categoria.formatearFecha(fecha);
                        }
                    },
                    {
                        data: null,
                        orderable: false,
                        searchable: false,
                        render: function () {
                            return `
                                <div class="d-flex gap-2">
                                    <button type="button" class="btn btn-sm btn-outline-primary btn-editar-categoria" title="Editar">
                                        <i class="bi bi-pencil-square"></i>
                                    </button>
                                    <button type="button" class="btn btn-sm btn-outline-danger btn-eliminar-categoria" title="Eliminar">
                                        <i class="bi bi-trash"></i>
                                    </button>
                                </div>`;
                        }
                    }
                ],
                language: {
                    url: 'https://cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
                }
            });
        },

        registrarEventos() {
            $('#btnGuardarCategoria').on('click', () => Categoria.guardarCategoria());
            $('#btnActualizarCategoria').on('click', () => Categoria.actualizarCategoria());

            $('#modalCrearCategoria').on('hidden.bs.modal', () => {
                $('#formCrearCategoria')[0].reset();
            });

            $('#modalEditarCategoria').on('hidden.bs.modal', () => {
                $('#formEditarCategoria')[0].reset();
            });

            $('#tblCategorias tbody').on('click', '.btn-editar-categoria', function () {
                const fila = Categoria.tabla.row($(this).closest('tr')).data();
                Categoria.abrirModalEditar(fila.categoriaId);
            });

            $('#tblCategorias tbody').on('click', '.btn-eliminar-categoria', function () {
                const fila = Categoria.tabla.row($(this).closest('tr')).data();
                Categoria.eliminarCategoria(fila);
            });
        },

        guardarCategoria() {
            const form = $('#formCrearCategoria');

            if (!form[0].reportValidity()) {
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {
                    Categoria.procesarRespuesta(respuesta, '#modalCrearCategoria');
                },
                error: function (xhr) {
                    Categoria.mostrarError(xhr);
                }
            });
        },

        abrirModalEditar(id) {
            $.getJSON('/Categoria/ObtenerCategoriaPorId', { id: id })
                .done(function (categoria) {
                    $('#editCategoriaId').val(categoria.categoriaId);
                    $('#editCategoriaNombre').val(categoria.nombre);
                    $('#editCategoriaDescripcion').val(categoria.descripcion);
                    $('#modalEditarCategoria').modal('show');
                })
                .fail(function (xhr) {
                    Categoria.mostrarError(xhr);
                });
        },

        actualizarCategoria() {
            const form = $('#formEditarCategoria');

            if (!form[0].reportValidity()) {
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {
                    Categoria.procesarRespuesta(respuesta, '#modalEditarCategoria');
                },
                error: function (xhr) {
                    Categoria.mostrarError(xhr);
                }
            });
        },

        eliminarCategoria(fila) {
            Swal.fire({
                title: '¿Eliminar categoría?',
                text: `Se eliminará la categoría "${fila.nombre}". Esta acción no se puede deshacer.`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#dc3545',
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar'
            }).then((resultado) => {
                if (!resultado.isConfirmed) {
                    return;
                }

                $.ajax({
                    url: '/Categoria/EliminarCategoria',
                    type: 'POST',
                    data: { id: fila.categoriaId },
                    success: function (respuesta) {
                        Categoria.procesarRespuesta(respuesta, null);
                    },
                    error: function (xhr) {
                        Categoria.mostrarError(xhr);
                    }
                });
            });
        },

        procesarRespuesta(respuesta, modal) {
            if (respuesta.esCorrecto) {
                Swal.fire({
                    title: "¡Listo!",
                    text: respuesta.mensaje,
                    icon: "success"
                });

                if (modal) {
                    $(modal).modal('hide');
                }

                Categoria.tabla.ajax.reload(null, false);
            }
            else {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: respuesta.mensaje
                });
            }
        },

        formatearFecha(fecha) {
            if (!fecha) {
                return '';
            }

            return new Date(fecha).toLocaleDateString('es-CR');
        },

        mostrarError(xhr) {
            let mensaje = "Ocurrió un error inesperado. Intenta de nuevo.";

            if (xhr.status === 400 && xhr.responseJSON) {
                const errores = Object.values(xhr.responseJSON.errors ?? xhr.responseJSON).flat();
                if (errores.length > 0) {
                    mensaje = errores.join('\n');
                }
            }
            else if (xhr.status === 404) {
                mensaje = "No se encontró la categoría.";
            }
            else if (xhr.responseJSON && xhr.responseJSON.description) {
                mensaje = xhr.responseJSON.description;
            }

            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: mensaje
            });
        }
    };

    $(document).ready(() => Categoria.init());

})();