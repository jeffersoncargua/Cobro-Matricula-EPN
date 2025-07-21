const message = {
    req:{
        formAcademy: "Debe elegir su formacion academica.",
        regimen: "Debe elegir su regimen academico.",
        quintil : "Debe elegir el quintil al que pertenece",
        gratuidad: "Debe elegir si tiene o no Gratuidad",
        primera: "Los Creditos u Horas en primera matricula son requeridos.",
        segunda: "Los Creditos u Horas en segunda matricula son requeridos.",
        tercera: "Los Creditos u Horas en tercera matricula son requeridos."
    },

    primera: "Los creditos u horas en primera matricula deben ser un numero entero",
    segunda: "Los creditos u horas en segunda matricula deben ser un numero entero",
    tercera: "Los creditos u horas en tercera matricula deben ser un numero entero"
}

const patterns = {
    entero : /^[0-9]+$/
}

export {message, patterns};