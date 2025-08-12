// <copyright file="CalculatorController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.Calculator;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace Cobro_Matricula_EPN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorRepository _calculatorRepository;
        private readonly APIResponse _response;

        public CalculatorController(ICalculatorRepository calculatorRepository)
        {
            _calculatorRepository = calculatorRepository;
            this._response = new ();
        }

        [HttpPost("CalculatorPay")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CalculatorPay([FromBody] CalculatorRequestDto calculatorRequestDto)
        {
            var response = await _calculatorRepository.Calculator(calculatorRequestDto);
            _response.Message.Add(response.Message);
            _response.Result = response.Calculator;
            _response.IsSuccess = response.Success;
            _response.StatusCode = response.StatusCode;
            return StatusCode((int)_response.StatusCode, _response);
        }
    }
}
