using AutoMapper;
using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.BaseParameter;
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
        public BaseParameterController(IBaseParameterRepository baseParameterRepository,IMapper mapper)
        {
            _baseParameterRepository = baseParameterRepository;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet("GetParameters/{id:int}", Name ="GetParameters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        [HttpPut("UpdateParameters/{id:int}", Name = "UpdateParameters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateParameters(int id, [FromBody] UpdatedBaseParameterRequestDto updatedBaseParameter)
        {
            
            var result = await _baseParameterRepository.UpdateAsync(id, updatedBaseParameter);
            if (result.Success)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Message.Add(result.Message);
                return Ok(_response);
            }
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.Message.Add(result.Message);
            return BadRequest(_response);
        }
    }
}
