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
        private readonly IUserRepository _userRepo;
        protected APIResponse _response;
        public UserController(IRepository repo, IUserRepository userRepo)
        {
            _repo = repo;
            this._response = new();
            _userRepo = userRepo;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    _response.IsSuccess = false;
                //    _response.Message.Add(ModelState.ToString());
                //    _response.Result = null;
                //    _response.StatusCode = HttpStatusCode.BadRequest;
                //    return BadRequest(_response);
                //}

                if(await _userRepo.IsConfirmEmail(loginRequestDto.Email))
                {
                    var result = await _userRepo.Login(loginRequestDto);
                    if (result.User == null && string.IsNullOrEmpty(result.Token))
                    {
                        _response.IsSuccess = false;
                        _response.Message.AddRange(["Ha ocurrido un error en el servidor"]);
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

                _response.IsSuccess = false;
                _response.Message.AddRange(["El usuario no ha verificado su cuenta. Verifique su correo electronico"]);
                _response.Result = null;
                _response.StatusCode = HttpStatusCode.BadRequest;

                return BadRequest(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = true;
                _response.Message.Add("El usuario ha sido eliminado de la base de datos!!!");
                _response.Result = null;
                return NotFound(_response);
            }
            
        }


        [HttpPost("Registration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Registration([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    Console.WriteLine(ModelState);
            //    _response.StatusCode = HttpStatusCode.InternalServerError;
            //    _response.IsSuccess = false;
            //    _response.Message.AddRange([ModelState.ToString()]);
            //    _response.Result = null;
            //    return BadRequest(_response);
            //}

            var isUnique = await _userRepo.IsUnique(registrationRequestDto.Email);
            if (!isUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Message.Add("El usuario ya existe!!!");
                _response.Result = null;
                return BadRequest(_response);
            }

            var result = await _userRepo.Register(registrationRequestDto);
            if (!result)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Message.Add(ModelState.ToString());
                _response.Result = null;
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.Created;
            _response.IsSuccess = true;
            _response.Message.Add("Registro exitoso!!!");
            _response.Result = null;
            return Ok(_response);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<APIResponse>> DeleteUser(string email)
        {
            var result = await _userRepo.RemoveUserAsync(email);
            if (!result)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.Message.Add("El usuario no existe en la base de datos!!!");
                _response.Result = null;
                return NotFound(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Message.Add("El usuario ha sido eliminado de la base de datos!!!");
            _response.Result = null;
            return Ok(_response);
        }

        [HttpPost("ForgetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> ForgetPassword([FromBody] string email)
        {
            var result = await _userRepo.ForgetPasswordAsyn(email);
            if (result)
            {
                _response.IsSuccess = true;
                _response.Message.Add("Para cambiar tu contraseña revisa tu correo y sigue los pasos!!");
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = null;
                return Ok(_response);

            }

            _response.IsSuccess = false;
            _response.Message.Add("El usuario no esta registrado!!!");
            _response.StatusCode = HttpStatusCode.NotFound;
            _response.Result = null;
            return NotFound(_response);
        }

        [HttpGet("ConfirmEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> ConfirmEmail(string token, string email)
        {
            var result = await _userRepo.ConfirmEmailAsync(token, email);
            if (result)
            {
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = null;
                _response.Message.Add("La cuenta ha sido verificada!!");
                return Ok(_response);
            }

            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Result = null;
            _response.Message.Add("No existe una cuenta registrada!!");
            return BadRequest(_response);
        }

        [HttpPost("ResetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequestDto)
        {
            var result = await _userRepo.ResetPasswordAsync(resetPasswordRequestDto);
            if (result)
            {
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message.Add("Su contraseña ha sido actualizada");
                _response.Result = null;
                return Ok(_response);
            }

            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Message.Add("No es posible actualizar su contraseña. Por favor comuniquese con el administrador!!");
            _response.Result = null;
            return BadRequest(_response);
        }

        [HttpPut("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateUSer([FromBody] UserDto userDto)
        {
            var result = await _userRepo.UpdateUserAsync(userDto);
            if (result != null)
            {
                _response.Result = result;
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message.Add("Su información ha sido actualizada");
                return Ok(_response);
            }

            _response.Result = null;
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Message.Add("Ha ocurrido un error en el servidor. No se pudo actualizar su informacion!!");
            return BadRequest(_response);
        }

    }
 
}
