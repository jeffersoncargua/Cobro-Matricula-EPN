const message = {
    req: {
        costoOptimo: "El costo optimo anual es requerido",
        horaPeriodoAcademico: "La hora del periodo academico es requerida",
        creditoPeriodoAcademico: "El credito del periodo academico es requerido",
        porcentajeMatriculaEspecial: "El porcentaje arancel especial es requerido",
        porcentajeRecargoSegunda: "El porcentaje de recargo segunda es requerido",
        porcentajeRecargoTercera: "El porcentaje de recargo tercera es requerido",
        porcentajeCostoOptimoAnual: "El porcentaje de costo optimo anual es requerido",
        porcentajeValorArancel: "El porcentaje del valor de arancel es requerido",
        porcentajeMatriculaExtraordinaria: "El porcentaje de matricula extraordinaria es requerido",
        porcentajePerdidaTemporal : "El porcentaje de perdida temporal es requerido",
        porcentajePromedioAcademico: "El porcentaje de promedio academico es requerido",
        porcentajeValorMin : "El porcentaje del valor minimo es requerido",
        porcentajeValorMax : "El porcentaje del valor maximo es requerido", 
    },
    costoOptimo: "El costo optimo anual debe ser un numero decimal entre 0.00 y 10000.00",
    horaPeriodoAcademico: "La hora del periodo academico debe ser un numero entero entre 0 y 1000",
    creditoPeriodoAcademico: "El credito del periodo academico debe ser un numero entero entre 0 y 100",
    porcentajeMatriculaEspecial: "El porcentaje arancel especial debe ser un numero decimal entre 0 y 1",
    porcentajeRecargoSegunda: "El porcentaje de recargo segunda debe ser un numero decimal entre 0 y 1",
    porcentajeRecargoTercera: "El porcentaje de recargo tercera debe ser un numero  decimal entre 0 y 1",
    porcentajeCostoOptimoAnual: "El porcentaje de costo optimo anual debe ser un numero decimal entre 0 y 1",
    porcentajeValorArancel: "El porcentaje del valor de arancel debe ser un numero decimal entre 0 y 1",
    porcentajeMatriculaExtraordinaria: "El porcentaje de matricula extraordinaria debe ser un numero decimal entre 0 y 1",
    porcentajePerdidaTemporal: "El porcentaje de perdida temporal debe ser un numero decimal entre 0 y 1",
    porcentajePromedioAcademico: "El porcentaje de promedio academico debe ser un numero decimal entre 0 y 1",
    porcentajeValorMin: "El porcentaje del valor minimo debe ser un numero decimal entre 0 y 1",
    porcentajeValorMax: "El porcentaje del valor maximo debe ser un numero decimal entre 0 y 1",
};

const patterns = {
    entero1000 : /^(0|[1-9]\d{0,2}|1000)$/,
    entero100 : /^([0-9]|[1-9][0-9]|100)$/,
    decimal10000 : /^(10000(\.0{1,2})?|[0-9]{1,3}(\.[0-9]{1,2})?)$/,
    decimal : /^0(\.\d+)?$|^1(\.0+)?$/,
}

export { message, patterns };