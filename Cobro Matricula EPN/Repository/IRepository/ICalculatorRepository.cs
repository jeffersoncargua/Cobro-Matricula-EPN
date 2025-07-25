﻿using Entity.DTO.Calculator;

namespace Cobro_Matricula_EPN.Repository.IRepository
{
    public interface ICalculatorRepository
    {
        Task<CalculatorResponseDto> Calculator(CalculatorRequestDto calculatorRequestDto);
    }
}
