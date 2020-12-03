$("#formulario").submit(function () {
    var apellido = $("#apellido").val()
    var nombre = $("#nombre").val()
    var cedula = $("#cedula").val()
    var p = $("#password").val();

    if (cedula.toString().length < 7 || cedula.toString().length > 9) {
        alert("El numero de cedula debe tener entre 7 y 9 digitos")
    }

    if (apellido.length < 2) {
        alert("El apellido debe tener un largo de por lo menos 2 caracteres")
    }

    if (nombre.length < 2) {
        alert("El nombre debe tener un largo de por lo menos 2 caracteres")
    }

    if (p.length < 6) {
        alert("La contraseña debe tener mas de 6 caracteres")
        return false;
    }
    if (p.search(/[a-z]/) == -1) {
        alert("La contraseña debe tener al menos una letra minúscula")
        return false;
    }
    if (p.search(/[A-Z]/) == -1) {
        var show = p.search(/[A-Z]/);
        console.log(show);
        alert("La contraseña debe tener al menos una letra mayúscula")
        return false;
    }
    if (p.search(/[0-9]/) == -1) {
        alert("La contraseña debe tener al menos un número")
        return false;
    }
})