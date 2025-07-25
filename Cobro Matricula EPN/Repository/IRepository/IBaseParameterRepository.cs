﻿using Entity.DTO.BaseParameter;
using Entity.Entities;

namespace Cobro_Matricula_EPN.Repository.IRepository
{
    public interface IBaseParameterRepository : IRepository<BaseParameter>
    {
        Task<UpdateBaseParametersResponseDto> UpdateAsync( int id ,UpdatedBaseParameterRequestDto updatedBaseParameter);

    }
}
