// <copyright file="IEmailRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Utility;

namespace Cobro_Matricula_EPN.Repository.IRepository
{
    public interface IEmailRepository
    {
        void SendEmail(Message mesaage);
    }
}
