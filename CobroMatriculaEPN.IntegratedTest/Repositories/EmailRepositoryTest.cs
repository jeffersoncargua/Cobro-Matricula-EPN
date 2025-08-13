using Cobro_Matricula_EPN.Repository;
using Microsoft.Extensions.Configuration;
using Utility;
using Xunit.Sdk;

namespace CobroMatriculaEPN.IntegratedTest.Repositories
{
    public class EmailRepositoryTest
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailRepositoryTest()
        {
            _emailConfiguration = new EmailConfiguration()
            {
                From = "jeffersoncargua@gmail.com",
                Port = 465,
                SmtpServer = "smtp.gmail.com",
                UserName = "jeffersoncargua@gmail.com",
                Password = "vwid cozg ugqv dvog"
            };
        }


        [Theory]
        [InlineData("jeffersoncargua@gmail.com", "Es una prueba", "Soy el cuerpo de la prueba")]
        public void SendEmail_ValidMessage(string to, string subject, string body)
        {
            //Arrange
            var message = new Message([to], subject, body);
            var repository = new EmailRepository(_emailConfiguration);


            //Act
            //Record.Exception permite almacenar la Excepcion que se haya lanzado, caso contrario es null
            var action = Record.Exception(() => repository.SendEmail(message));

            //Assert
            Assert.Null(action);
        }

        /// <summary>
        /// Esta prueba permite comprobar si al enviar un mensaje sin un destinatiario, se lanzará una excepcion
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        [Theory]
        [InlineData(null, "Es una prueba", "Soy el cuerpo de la prueba")]
        [InlineData(null, null, "Soy el cuerpo de la prueba")]
        public void SendEmail_InvalidToParameter_ThrowException(string to, string subject, string body)
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<Exception>(() => new Message([to], subject, body));
        }
    }
}
