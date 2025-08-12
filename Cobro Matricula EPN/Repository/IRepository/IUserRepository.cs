// <copyright file="IUserRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Entity.DTO.User;
using Entity.Entities;
using System.Linq.Expressions;

namespace Cobro_Matricula_EPN.Repository.IRepository
{
    /// <summary>
    /// En la interfaz IUserRepository se realiza las gestiones como register, login, forgetPassword, etc. Necesarias para la gestion de usuarios dentro del sistema.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Esta funcion permite realizar el login de usuario una vez que se haya verificado su cuenta a traves del metodo ConfirEmail.
        /// </summary>
        /// <param name="loginRequestDto">Es un conjunto de parametros que el usuario debe enviar para realizar el login. </param>
        /// <returns>Retorna un token cuando el usuario se ha logeado correctamente, caso contrario se envia un token vacio y un mensaje de error.</returns>
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        //Task<bool> IsUnique(string email);
        //Task<bool> IsConfirmEmail(string email);

        /// <summary>
        /// Esta funcion permite al usuario registrarse a la plataforma para poder realizar la gestion de los parametros base para los calculos que se necesitan conocer
        /// por parte de los estudiantes de la universidad XYZ.
        /// </summary>
        /// <param name="registrationRequestDto">Es un conjunto de parametros que el usuario debe enviar para proceder con el registro del mismo en la plataforma.</param>
        /// <returns>Retorna una respuesta afirmativa en caso de que se haya realizado el registro con exito, caso contrario retorna una respuesta negativa.</returns>
        Task<RegisterResponseDto> Register(RegistrationRequestDto registrationRequestDto);

        /// <summary>
        /// Esta funcion permite al usuario principal realizar la gestion de eliminar usuarios asistentes de la plataforma 
        /// para la gestion de los parametros base.
        /// </summary>
        /// <param name="email">Este parametro permite buscar el registro para eliminarlo de la base de datos.</param>
        /// <returns>Retorna una respuesta afirmativa en caso de eliminar el registro, caso contrario retorna una respuesta negativa.</returns>
        Task<bool> RemoveUserAsync(string email);

        /// <summary>
        /// Esta funcion permite verificar el token que se envia al usuario para confirmar su cuenta.
        /// </summary>
        /// <param name="email">Es el correo del usuario que se registro en la plataforma y con el cual se va a verificar con el token.</param>
        /// <param name="token">Es un string generado con Identity GenerateToken en el Register que permite verificar la cuenta de un usuario.</param>
        /// <returns> Retorna true si existe el usuario y se confirma el token de validacion, caso contrario retorna false, considerando si se envian valores nulos o que no existan en los registros.</returns>
        Task<bool> ConfirmEmailAsync(string email, string token);

        /// <summary>
        /// Esta metodo permite al usuario recuperar su cuenta en caso de que se haya olvidado su contraseña, verificando que se encuentre registrado para enviar un token de validacion
        /// para proceder con el cambio de contraseña en caso de ser valido el registro.
        /// </summary>
        /// <param name="email">Es el parametro que permite virificar si el usuario esta registrado.</param>
        /// <returns>Retorna una respuesta con el token de validacion, un mensaje de confirmacion y si el proceso fue satisfactorio en caso de que se haya realizado correctamente el proceso, caso contrario retorna una respuesta negativa.</returns>
        Task<ForgetResponseDto> ForgetPasswordAsyn(string email);

        /// <summary>
        /// Esta funcion permite realizar la transaccion para resetear la contraseña de un usuario valido que se encuentre registrado.
        /// </summary>
        /// <param name="resetPasswordRequestDto">Es un conjunto de parametros que le permite al usuario poder realizar la solicitud para el cambio de contraseña.</param>
        /// <returns>Retorna una respuesta afirmativa si el token en valido y el requerimiento se hace con exito, caso contrario se envia una respuesta negativa.</returns>
        Task<ResetPasswordResponseDto> ResetPasswordAsync(ResetPasswordRequestDto resetPasswordRequestDto);

        /// <summary>
        /// Esta funcion permite actualizar los datos de los usuarios.
        /// </summary>
        /// <param name="updateUserDto">Es un conjunto de parametros que se utilizan para actualizar a los usuarios.</param>
        /// <param name="email">Es un parametros para verificar si existe el registro del usuario.</param>
        /// <returns>Retorna una respuesta afirmativa si la transaccion fue exitosa, caso contrario se envia una respuesta negativa a la peticion.</returns>
        Task<UpdateUserResponseDto> UpdateUserAsync(UpdateUserDto updateUserDto, string email);

        //Task<bool> UserExist(string email);

        /// <summary>
        /// Este metodo permite obtener una lista de los usuarios registrados.
        /// </summary>
        /// <param name="filter">Es un parametros que permite filtrar los usuarios.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<UserDto>> GetUsers(Expression<Func<ApplicationUser, bool>>? filter = null);
    }
}
