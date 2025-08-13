// <copyright file="BaseParameterController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.BaseParameter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility;

namespace Cobro_Matricula_EPN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseParameterController : ControllerBase
    {
        private readonly IBaseParameterRepository _baseParameterRepository;
        protected APIResponse _response;
        private readonly IMapper _mapper;

        public BaseParameterController(IBaseParameterRepository baseParameterRepository, IMapper mapper)
        {
            _baseParameterRepository = baseParameterRepository;
            _mapper = mapper;
            this._response = new();
        }

        /// <summary>
        /// Esta Api permite obtener la informacion de los parametros base del sistema.
        /// </summary>
        /// <param name="id">Es el identificador de los parametros base.</param>
        /// <returns>Retorna un statusCode de 200 y la informacion de los parametros base, caso contrario retorna un statusCode de 400.</returns>
        [HttpGet("GetParameters/{id:int}", Name = "GetParameters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin,Assistant")]
        public async Task<ActionResult<APIResponse>> GetParameters(int id)
        {
            var parameters = await _baseParameterRepository.GetAsync(u => u.Id == id);
            if (parameters != null)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = _mapper.Map<BaseParameterDto>(parameters);
                _response.Message.Add("Se ha encontrado la informacion de los parametros correctamente!!");
                return Ok(_response);
            }

            _response.StatusCode = HttpStatusCode.NotFound;
            _response.IsSuccess = false;
            _response.Message.Add("No se ha encontrado la información de los parametros.");
            _response.Result = null;
            return NotFound(_response);
        }

        /// <summary>
        /// Esta Api permite realizar la actualizacion de los datos de los parametros base del sistema.
        /// </summary>
        /// <param name="id">Es el identificador de los parametros base.</param>
        /// <param name="updatedBaseParameter">Es un conjunto de parametros necesarios para realizar la gestion de los parametros base.</param>
        /// <returns>Retorna un statusCode de 200 si se realizo la actualizacion correctamente, caso contrario se retorna un statusCode de 400.</returns>
        [HttpPut("UpdateParameters/{id:int}", Name = "UpdateParameters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<APIResponse>> UpdateParameters(int id, [FromBody] UpdatedBaseParameterRequestDto updatedBaseParameter)
        {
            var result = await _baseParameterRepository.UpdateAsync(id, updatedBaseParameter);

            if (result.Success)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message.Add(result.Message);
                _response.Result = result.Result;
                return Ok(_response);
            }

            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.Message.Add(result.Message);
            _response.Result = null;
            return BadRequest(_response);
        }
    }
}
