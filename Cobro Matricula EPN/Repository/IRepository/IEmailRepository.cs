using Utility;

namespace Cobro_Matricula_EPN.Repository.IRepository
{
    public interface IEmailRepository
    {
        void SendEmail(Message mesaage);
    }
}
