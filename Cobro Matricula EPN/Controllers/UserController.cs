using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.User;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility;

namespace Cobro_Matricula_EPN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRepository _repo;
        protected APIResponse _response;
        public UserController(IRepository repo)
        {
            _repo = repo;
            this._response = new();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                var result = await _repo.Login(loginRequestDto);
                if(result.User == null && string.IsNullOrEmpty(result.Token))
                {
                    _response.IsSuccess = false;
                    _response.Message.AddRange([result.Message]) ;
                    _response.Result = null;
                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }

                _response.IsSuccess = true;
                _response.Message.AddRange(["Login Exitoso!!"]);
                _response.Result = result;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message.AddRange([ex.Message.ToString()]);
                _response.Result = null;
                _response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return _response;
        }
    }
}
