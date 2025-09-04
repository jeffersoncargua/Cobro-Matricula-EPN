// <copyright file="UserController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.User;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utility;

namespace Cobro_Matricula_EPN.Controllers
{
    /// <summary>
    /// Este controlador permite realizar la gestion de los usuarios que se registren en el sistema.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        //private readonly IRepository _repo;
        private readonly IUserRepository _userRepo;
        protected APIResponse _response;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// El contructor UserController permite realizar la DI para acceder a los servicios e interfaces del proyecto.
        /// </summary>
        /// <param name="userRepo">Es la interfaz que permite realizar las operaciones necesarias para la gestion de los usuarios.</param>
        public UserController(IUserRepository userRepo)
        {
            //_repo = repo;
            this._response = new();
            _userRepo = userRepo;
        }

        /// <summary>
        /// Esta Api permite obtener un listado de los usuarios registrados en el sistema.
        /// </summary>
        /// <param name="query">Es un parametro que permite filtrar a los usuarios segun se los requerimientos de busqueda.</param>
        /// <returns>Retorna un statusCOde de 200 y la lista de los usuarios registrados con/sin filtros de busqueda, Caso contrario retorna un statusCode de 400.</returns>
        [HttpGet("GetUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAll([FromQuery] string query = null)
        {
            var users = await _userRepo.GetUsers(query != null ? u => u.Name.Contains(query) : null);

            _response.IsSuccess = true;
            _response.Message.Add("Se envio la lista de usuarios");
            _response.Result = users;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        /// <summary>
        /// Este Api permite realizar el login de usuario, una vez que se haya verificado su cuenta.
        /// </summary>
        /// <param name="loginRequestDto">Es un conjunto de parametros para realizar el login de usuario.</param>
        /// <returns>Retorna un statusCode de 200 y un token en caso de que el login haya sido exitoso, caso contrario se envia un statusCode de 400.</returns>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result = await _userRepo.Login(loginRequestDto);
            if (result.User == null && string.IsNullOrEmpty(result.Token))
            {
                _response.IsSuccess = false;
                _response.Message.Add(result.Message);
                _response.Result = null;
                _response.StatusCode = HttpStatusCode.BadRequest;

                return BadRequest(_response);
            }

            _response.IsSuccess = true;
            _response.Message.Add(result.Message);
            _response.Result = result;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        /// <summary>
        /// Este Api permite realizar el registro de los usuarios en el sistema. 
        /// </summary>
        /// <param name="registrationRequestDto">Es un conjunto de parametros que se requieren para hacer el registro de los usuarios.</param>
        /// <returns>Retorna un statusCode de 200 y se envia un correo en caso de que el registro sea exitoso, caso contrario se envia un statusCode de 400.</returns>
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

            var result = await _userRepo.Register(registrationRequestDto);
            if (!result.Success)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = result.Success;
                _response.Message = result.MessageResponse;
                _response.Result = null;
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.Created;
            _response.IsSuccess = result.Success;
            _response.Message = result.MessageResponse;
            _response.Result = result.Token;
            return Ok(_response);
        }

        /// <summary>
        /// Esta Api permite realizar la eliminacion de un usuario registrado en el sistema.
        /// </summary>
        /// <param name="email">Es un parametro para verificar el registro del usuario.</param>
        /// <returns>Retorna un statusCode de 200 cuando la eliminacion haya sido exitosa, caso contrario se envia un statusCode de 400.</returns>
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

        /// <summary>
        /// Esta Api permite realizar la gestion para cambiar la contraseña de usuario, cuando este se olvida y se encuentra registrado.
        /// </summary>
        /// <param name="email">Es un parametro para verificar que exista el registro del usuario.</param>
        /// <returns>Retorna un statusCode de 200 y el token para realizar el proceso de cambiar la contraseña, caso contrario se envia un statusCode de 400.</returns>
        [HttpPost("ForgetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> ForgetPassword([FromBody] string email)
        {
            var result = await _userRepo.ForgetPasswordAsyn(email);
            if (result.Success)
            {
                _response.IsSuccess = true;
                _response.Message.Add(result.Message);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = result.Token;
                return Ok(_response);
            }

            _response.IsSuccess = false;
            _response.Message.Add(result.Message);
            _response.StatusCode = HttpStatusCode.NotFound;
            _response.Result = null;
            return BadRequest(_response);
        }

        /// <summary>
        /// Esta Api permite realizar la transaccion para confirmar el token de validacion de usuario.
        /// </summary>
        /// <param name="token">Es un token necesario para autenticar la informacion del usuario.</param>
        /// <param name="email">Es un parametro para verificar el registro del usuario.</param>
        /// <returns>Retorna un statusCode de 200 en caso de que la validacion haya sido exitosa, caso contrario se retorna un statusaCode de 400 si la peticion a fallado.</returns>
        [HttpGet("ConfirmEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> ConfirmEmail(string token, string email)
        {
            var result = await _userRepo.ConfirmEmailAsync(email, token);
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

        /// <summary>
        /// Este Api permite cambiar la contraseña de un usuario registrado en el sistema pero que ha olvidado su contraseña.
        /// </summary>
        /// <param name="resetPasswordRequestDto">Es un conjunto de parametros necesarios para poder realizar el cambio de contraseña.</param>
        /// <returns>Retorna un statusCode de 200 en caso de que la peticion haya sido tratada con exito, caso contrario se retorna un statusCode de 400.</returns>
        [HttpPost("ResetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequestDto)
        {
            var result = await _userRepo.ResetPasswordAsync(resetPasswordRequestDto);
            if (result.Success)
            {
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message.Add(result.Message);
                _response.Result = null;
                return Ok(_response);
            }

            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Message.Add(result.Message);
            _response.Result = null;
            return BadRequest(_response);
        }

        /// <summary>
        /// Esta Api permite realizar la actualizacion de los datos de los usuarios.
        /// </summary>
        /// <param name="email">Es un parametro para verificar el registro del usuario.</param>
        /// <param name="updateUserDto">Es un conjunto de parametros necesarios para realizar la actualizacion de los datos del usuario.</param>
        /// <returns>Retorna un statusCode de 200 en caso de que se haya realizado correctamente la solicitud, caso contrario se retorna un statusCode de 400.</returns>
        [HttpPut("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateUser(string email, [FromBody] UpdateUserDto updateUserDto)
        {
            var result = await _userRepo.UpdateUserAsync(updateUserDto, email);
            if (result.Success)
            {
                _response.Result = result.User;
                _response.IsSuccess = result.Success;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Message.Add(result.Message);
                return Ok(_response);
            }

            _response.Result = null;
            _response.IsSuccess = result.Success;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Message.Add(result.Message);
            return BadRequest(_response);
        }
    }
}