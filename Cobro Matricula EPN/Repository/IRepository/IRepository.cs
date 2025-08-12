// <copyright file="IRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq.Expressions;

namespace Cobro_Matricula_EPN.Repository.IRepository
{
    public interface IRepository<T> 
        where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);

        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);
        
        Task Save();
    }
}
