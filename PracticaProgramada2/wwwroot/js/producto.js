(() => {

    const Producto = {
        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
        },

        inicializarTabla() {
            this.tabla = new DataTable('#tblProductos', {
                ajax: {
                    url: '/Producto/ObtenerProductos',
                    dataSrc: '',
                    type: 'GET'
                },
                columns: [
                    { data: 'productoId' },
                    { data: 'nombre', render: DataTable.render.text() },
                    { data: 'descripcion', render: DataTable.render.text() },
                    { data: 'categoria.nombre', defaultContent: '', render: DataTable.render.text() },
                    {
                        data: 'precio',
                        render: function (precio, tipo) {
                            return tipo === 'display' ? Producto.formatearPrecio(precio) : precio;
                        }
                    },
                    { data: 'stock' },
                    {
                        data: 'fechaRegistro',
                        render: function (fecha) {
                            return Producto.formatearFecha(fecha);
                        }
                    },
                    {
                        data: null,
                        orderable: false,
                        searchable: false,
                        render: function () {
                            return `
                                <div class="d-flex gap-2">
                                    <button type="button" class="btn btn-sm btn-outline-primary btn-editar-producto" title="Editar">
                                        <i class="bi bi-pencil-square"></i>
                                    </button>
                                    <button type="button" class="btn btn-sm btn-outline-danger btn-eliminar-producto" title="Eliminar">
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
            $('#btnGuardarProducto').on('click', () => Producto.guardarProducto());
            $('#btnActualizarProducto').on('click', () => Producto.actualizarProducto());

            // Se recargan las categorías cada vez que se abre el modal de creación
            $('#modalCrearProducto').on('show.bs.modal', () => {
                Producto.cargarCategorias('#crearProductoCategoria', '');
            });

            $('#modalCrearProducto').on('hidden.bs.modal', () => {
                $('#formCrearProducto')[0].reset();
            });

            $('#modalEditarProducto').on('hidden.bs.modal', () => {
                $('#formEditarProducto')[0].reset();
            });

            $('#tblProductos tbody').on('click', '.btn-editar-producto', function () {
                const fila = Producto.tabla.row($(this).closest('tr')).data();
                Producto.abrirModalEditar(fila.productoId);
            });

            $('#tblProductos tbody').on('click', '.btn-eliminar-producto', function () {
                const fila = Producto.tabla.row($(this).closest('tr')).data();
                Producto.eliminarProducto(fila);
            });
        },

        cargarCategorias(selector, categoriaSeleccionada) {
            return $.getJSON('/Categoria/ObtenerCategorias').then(function (categorias) {
                const combo = $(selector);
                combo.empty().append($('<option>').val('').text('Seleccione una categoría'));

                categorias.forEach(function (categoria) {
                    combo.append($('<option>').val(categoria.categoriaId).text(categoria.nombre));
                });

                combo.val(categoriaSeleccionada);
            });
        },

        guardarProducto() {
            const form = $('#formCrearProducto');

            if (!form[0].reportValidity()) {
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {
                    Producto.procesarRespuesta(respuesta, '#modalCrearProducto');
                },
                error: function (xhr) {
                    Producto.mostrarError(xhr);
                }
            });
        },

        abrirModalEditar(id) {
            $.getJSON('/Producto/ObtenerProductoPorId', { id: id })
                .done(function (producto) {
                    Producto.cargarCategorias('#editProductoCategoria', producto.categoriaId)
                        .done(function () {
                            $('#editProductoId').val(producto.productoId);
                            $('#editProductoNombre').val(producto.nombre);
                            $('#editProductoDescripcion').val(producto.descripcion);
                            $('#editProductoPrecio').val(producto.precio);
                            $('#editProductoStock').val(producto.stock);
                            $('#modalEditarProducto').modal('show');
                        })
                        .fail(function (xhr) {
                            Producto.mostrarError(xhr);
                        });
                })
                .fail(function (xhr) {
                    Producto.mostrarError(xhr);
                });
        },

        actualizarProducto() {
            const form = $('#formEditarProducto');

            if (!form[0].reportValidity()) {
                return;
            }

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (respuesta) {
                    Producto.procesarRespuesta(respuesta, '#modalEditarProducto');
                },
                error: function (xhr) {
                    Producto.mostrarError(xhr);
                }
            });
        },

        eliminarProducto(fila) {
            Swal.fire({
                title: '¿Eliminar producto?',
                text: `Se eliminará el producto "${fila.nombre}". Esta acción no se puede deshacer.`,
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
                    url: '/Producto/EliminarProducto',
                    type: 'POST',
                    data: { id: fila.productoId },
                    success: function (respuesta) {
                        Producto.procesarRespuesta(respuesta, null);
                    },
                    error: function (xhr) {
                        Producto.mostrarError(xhr);
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

                Producto.tabla.ajax.reload(null, false);
            }
            else {
                Swal.fire({
                    icon: "error",
                    title: "Oops...",
                    text: respuesta.mensaje
                });
            }
        },

        formatearPrecio(precio) {
            return Number(precio).toLocaleString('es-CR', {
                style: 'currency',
                currency: 'CRC'
            });
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
                mensaje = "No se encontró el producto.";
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

    $(document).ready(() => Producto.init());

})();