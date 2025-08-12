// <copyright file="CalculatorRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Cobro_Matricula_EPN.Context;
using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.Calculator;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Cobro_Matricula_EPN.Repository
{
    public class CalculatorRepository : ICalculatorRepository
    {
        private readonly ApplicationDbContext _db;

        //private readonly IMapper _mapper;
        //private readonly IBaseParameterRepository _baseParameterRepository;
        public CalculatorRepository(ApplicationDbContext db)
        {
            _db = db;

            //_baseParameterRepository = baseParameterRepository;
            //_mapper = mapper;
            //_baseParameterRepository = baseParameterRepository;
        }

        public async Task<CalculatorResponseDto> Calculator(CalculatorRequestDto calculatorRequestDto)
        {
            CalculatorDto calculator = new();
            CalculatorResponseDto responseDto = new();

            try
            {
                //Verificar la existencia de los parametros base

                if (calculatorRequestDto.Regimen == "creditos")
                {
                    calculatorRequestDto.Primera *= 16;
                    calculatorRequestDto.Segunda *= 16;
                    calculatorRequestDto.Tercera *= 16;
                }

                //var baseParameters = await _baseParameterRepository.GetAsync(u => u.Id == calculatorRequestDto.FormacionAcademica);
                var baseParameters = await _db.BaseParameters.FirstOrDefaultAsync(u => u.Id == calculatorRequestDto.FormacionAcademica);
                if (baseParameters != null && (calculatorRequestDto.Primera + calculatorRequestDto.Segunda + calculatorRequestDto.Tercera) % 16 == 0)
                {
                    var costoHoraSocioeconomico = baseParameters.CostoHoraPeriodo * calculatorRequestDto.Quintil * 0.1f;

                    //Calcular los valores del valor de matricula, recargos de primera, segunda y tercera matricula
                    if (calculatorRequestDto.Primera == 0 && calculatorRequestDto.Segunda == 0 && calculatorRequestDto.Tercera == 0)
                    {
                        calculator.ValorMatricula = 0f;
                        calculator.ValorArancel = 0f;
                        calculator.RecargoSegunda = 0f;
                        calculator.RecargoTercera = 0f;
                        calculator.RecargoMatriculaExtraordinaria = 0f;
                        calculator.ValorTotal = 0f;
                        calculator.Gratuidad = "Ninguna";

                        responseDto.Calculator = calculator;
                        responseDto.Success = false;
                        responseDto.Message = "No se ha registrado ninguna asignatura, por lo tanto no se calculará ningún valor.";
                        responseDto.StatusCode = HttpStatusCode.BadRequest;
                        return responseDto;
                    }

                    if (calculatorRequestDto.Gratuidad == false)
                    {
                        calculator.ValorMatricula = (float)Math.Round(baseParameters.ValorMatriculaMin * calculatorRequestDto.Quintil, 2);
                        calculator.ValorArancel = (float)Math.Round((calculatorRequestDto.Primera + calculatorRequestDto.Segunda + calculatorRequestDto.Tercera) * costoHoraSocioeconomico, 2);
                        calculator.RecargoSegunda = (float)Math.Round(calculatorRequestDto.Segunda * costoHoraSocioeconomico * baseParameters.PorcentajeRecargoSegunda, 2);
                        calculator.RecargoTercera = (float)Math.Round(calculatorRequestDto.Tercera * costoHoraSocioeconomico * baseParameters.PorcentajeRecargoTercera, 2);
                        calculator.RecargoMatriculaExtraordinaria = (float)Math.Round(calculator.ValorMatricula * baseParameters.PorcentajeMatriculaExtraordinario, 2);
                        calculator.ValorTotal = (float)Math.Round(calculator.ValorMatricula + calculator.ValorArancel + calculator.RecargoSegunda + calculator.RecargoTercera + calculator.Bancario, 2);
                        calculator.Gratuidad = "Sin Gratuidad";

                        //responseDto.Calculator = calculator;
                        //responseDto.Success = true;
                        //responseDto.Message = "El calculo se realizo correctamente";
                        //return responseDto; 
                    }
                    else
                    {
                        if ((calculatorRequestDto.Segunda + calculatorRequestDto.Tercera) > 0 && (calculatorRequestDto.Primera + calculatorRequestDto.Segunda + calculatorRequestDto.Tercera) <= baseParameters.CreditoPerdidaTemporal * 16)
                        {
                            calculator.ValorMatricula = (float)Math.Round(baseParameters.ValorMatriculaMin * calculatorRequestDto.Quintil, 2);
                            calculator.ValorArancel = (float)Math.Round((calculatorRequestDto.Primera + calculatorRequestDto.Segunda + calculatorRequestDto.Tercera) * costoHoraSocioeconomico, 2);
                            calculator.RecargoSegunda = (float)Math.Round(calculatorRequestDto.Segunda * costoHoraSocioeconomico * baseParameters.PorcentajeRecargoSegunda, 2);
                            calculator.RecargoTercera = (float)Math.Round(calculatorRequestDto.Tercera * costoHoraSocioeconomico * baseParameters.PorcentajeRecargoTercera, 2);
                            calculator.RecargoMatriculaExtraordinaria = (float)Math.Round(calculator.ValorMatricula * baseParameters.PorcentajeMatriculaExtraordinario, 2);
                            calculator.ValorTotal = (float)Math.Round(calculator.ValorMatricula + calculator.ValorArancel + calculator.RecargoSegunda + calculator.RecargoTercera + calculator.Bancario, 2);
                            calculator.Gratuidad = "Perdida Temporal + Parcial";
                        }
                        else if ((calculatorRequestDto.Segunda + calculatorRequestDto.Tercera) > 0)
                        {
                            calculator.ValorMatricula = (float)Math.Round(baseParameters.ValorMatriculaMin * calculatorRequestDto.Quintil, 2);
                            calculator.ValorArancel = (float)Math.Round((calculatorRequestDto.Segunda + calculatorRequestDto.Tercera) * costoHoraSocioeconomico, 2);
                            calculator.RecargoSegunda = (float)Math.Round(calculatorRequestDto.Segunda * costoHoraSocioeconomico * baseParameters.PorcentajeRecargoSegunda, 2);
                            calculator.RecargoTercera = (float)Math.Round(calculatorRequestDto.Tercera * costoHoraSocioeconomico * baseParameters.PorcentajeRecargoTercera, 2);
                            calculator.RecargoMatriculaExtraordinaria = (float)Math.Round(calculator.ValorMatricula * baseParameters.PorcentajeMatriculaExtraordinario, 2);
                            calculator.ValorTotal = (float)Math.Round(calculator.ValorMatricula + calculator.ValorArancel + calculator.RecargoSegunda + calculator.RecargoTercera + calculator.Bancario, 2);
                            calculator.Gratuidad = "Perdida Parcial";

                            //responseDto.Calculator = calculator;
                            //responseDto.Success = true;
                            //responseDto.Message = "El calculo se realizo correctamente";

                            //return responseDto;
                        }
                        else if ((calculatorRequestDto.Primera + calculatorRequestDto.Segunda + calculatorRequestDto.Tercera) <= baseParameters.CreditoPerdidaTemporal * 16)
                        {
                            calculator.ValorMatricula = (float)Math.Round(baseParameters.ValorMatriculaMin * calculatorRequestDto.Quintil, 2);
                            calculator.ValorArancel = (float)Math.Round((calculatorRequestDto.Primera + calculatorRequestDto.Segunda + calculatorRequestDto.Tercera) * costoHoraSocioeconomico, 2);
                            calculator.RecargoSegunda = (float)Math.Round(calculatorRequestDto.Segunda * costoHoraSocioeconomico * baseParameters.PorcentajeRecargoSegunda, 2);
                            calculator.RecargoTercera = (float)Math.Round(calculatorRequestDto.Tercera * costoHoraSocioeconomico * baseParameters.PorcentajeRecargoTercera, 2);
                            calculator.RecargoMatriculaExtraordinaria = (float)Math.Round(calculator.ValorMatricula * baseParameters.PorcentajeMatriculaExtraordinario, 2);
                            calculator.ValorTotal = (float)Math.Round(calculator.ValorMatricula + calculator.ValorArancel + calculator.RecargoSegunda + calculator.RecargoTercera + calculator.Bancario, 2);
                            calculator.Gratuidad = "Perdida Temporal";

                            ////responseDto.Calculator = calculator;
                            ////responseDto.Success = true;
                            //responseDto.Message = "El calculo se realizo correctamente";

                            //return responseDto;
                        }
                        else
                        {
                            calculator.ValorMatricula = 0f;
                            calculator.ValorArancel = 0f;
                            calculator.RecargoSegunda = 0f;
                            calculator.RecargoTercera = 0f;
                            calculator.RecargoMatriculaExtraordinaria = (float)Math.Round((baseParameters.ValorMatriculaMin * calculatorRequestDto.Quintil) * baseParameters.PorcentajeMatriculaExtraordinario, 2);
                            calculator.ValorTotal = 0f;
                            calculator.Gratuidad = "Con Gratuidad";
                        }
                    }
                }
                else
                {
                    responseDto.Calculator = null;
                    responseDto.Success = false;
                    responseDto.Message = baseParameters != null ?
                        "Si elegiste regimen horas, debes recordar que debes ingresar en multiplos de 16. Por ejemplo: 0, 16, 32, 48,.... etc." +
                        "\n Caso contrario no se podrá realizar correctamente el calculo "
                        : "No existen los parametros para realizar los calculos";
                    responseDto.StatusCode = HttpStatusCode.BadRequest;

                    return responseDto;
                }

                responseDto.Calculator = calculator;
                responseDto.Success = true;
                responseDto.Message = "El calculo se realizo correctamente";
                responseDto.StatusCode = HttpStatusCode.OK;

                return responseDto;
            }
            catch (Exception)
            {
                responseDto.Calculator = null;
                responseDto.Message = "Ha courrido un error en el servidor. Por favor, contactate con el administrador";
                responseDto.Success = false;
                responseDto.StatusCode = HttpStatusCode.InternalServerError;
                return responseDto;
            }
        }
    }
}
