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
                    }
                ],
                language: {
                    url: 'https://cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
                }
            });
        },

        registrarEventos() {
            $('#btnGuardarCategoria').on('click', () => Categoria.guardarCategoria());

            $('#modalCrearCategoria').on('hidden.bs.modal', () => {
                $('#formCrearCategoria')[0].reset();
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
                    if (respuesta.esCorrecto) {
                        Swal.fire({
                            title: "¡Listo!",
                            text: respuesta.mensaje,
                            icon: "success"
                        });
                        $('#modalCrearCategoria').modal('hide');
                        Categoria.tabla.ajax.reload();
                    }
                    else {
                        Swal.fire({
                            icon: "error",
                            title: "Oops...",
                            text: respuesta.mensaje
                        });
                    }
                },
                error: function (xhr) {
                    Categoria.mostrarError(xhr);
                }
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

            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: mensaje
            });
        }
    };

    $(document).ready(() => Categoria.init());

})();
