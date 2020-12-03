$("#formulario").submit(function () {
    if ($("#cantMayores").val() <= 0){
        alert("debe haber por lo menos un pasajero mayor de edad.");
        return false;
    }
});