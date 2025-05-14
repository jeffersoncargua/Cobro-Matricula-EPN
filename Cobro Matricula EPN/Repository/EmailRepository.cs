using Cobro_Matricula_EPN.Repository.IRepository;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Utility;

namespace Cobro_Matricula_EPN.Repository
{
    public class EmailRepository : IEmailRepository
    {
        //Se debe agregar las configuraciones para enviar el email
        private readonly EmailConfiguration _emailConfiguration;
        public EmailRepository(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Notificacion Universidad XYZ",_emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(TextFormat.Html) { Text = message.Content };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                //Aqui se realiza el proceso de validacion de los datos de emailconfiguration para el envio de mensajes a traves de Gmail como mensajeria
                client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port,true); //Se obtiene el servidor y el puerto
                client.AuthenticationMechanisms.Remove("XOAUTH2"); //Se autentica el usuario con el metodo XAUTH2
                client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password); //Se autentica el usuario y la contraseña

                client.Send(mailMessage); //Se envia el mensaje al correo
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
