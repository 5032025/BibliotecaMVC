// Función global para confirmar la creación o modificación de registros
function confirmarGuardar(event, formId, mensaje = "¿Deseas guardar los cambios?") {
    event.preventDefault();
    const form = document.getElementById(formId);

    Swal.fire({
        title: '¿Estás seguro?',
        text: mensaje,
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, guardar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            // Usamos HTMLFormElement.prototype.submit.call(form) por si algún input se llama 'submit' o 'id'
            HTMLFormElement.prototype.submit.call(form);
        }
    });
}

// Función global para confirmar la eliminación de registros
function confirmarAccionForm(event, formElement, titulo = "¿Estás seguro?", texto = "¡Esta acción no se puede revertir!") {
    event.preventDefault();

    Swal.fire({
        title: titulo,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, continuar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            HTMLFormElement.prototype.submit.call(formElement);
        }
    });
}