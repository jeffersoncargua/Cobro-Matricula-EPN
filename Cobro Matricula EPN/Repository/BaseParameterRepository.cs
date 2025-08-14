// <copyright file="BaseParameterRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using Cobro_Matricula_EPN.Context;
using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.BaseParameter;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cobro_Matricula_EPN.Repository
{
    public class BaseParameterRepository : Repository<BaseParameter>, IBaseParameterRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public BaseParameterRepository(ApplicationDbContext db, IMapper mapper)
            : base(db)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <summary>
        /// Esta funcion permite actualizar la informacion de los parametros base del sistema para el calculo del cobro de matricula.
        /// </summary>
        /// <param name="id">Es el parametros para identificar la informacion de los parametros base.</param>
        /// <param name="updatedBaseParameter">Es un conjunto de parametros para realizar la actualizacion de los valores de los parametros base.</param>
        /// <returns>Retorna una respuesta afirmativa si se actualizaron correctamente los parametros, caso contrario se envia una respuesta negativa a la solicitud. </returns>
        public async Task<UpdateBaseParametersResponseDto> UpdateAsync(int id, UpdatedBaseParameterRequestDto updatedBaseParameter)
        {
            try
            {
                if (id != 0 && updatedBaseParameter != null)
                {
                    if (await _db.BaseParameters.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id) != null && id == updatedBaseParameter.Id)
                    {
                        BaseParameter newBaseParameter = new()
                        {
                            Id = id,
                            FormacionAcademica = updatedBaseParameter.FormacionAcademica,
                            CostoOptimo = updatedBaseParameter.CostoOptimo,
                            CostoOptimoPeriodo = updatedBaseParameter.CostoOptimo * updatedBaseParameter.PorcentajeCostoOptimoAnual,
                            ValorMin = updatedBaseParameter.CostoOptimo * updatedBaseParameter.PorcentajeCostoOptimoAnual * updatedBaseParameter.PorcentajeValorMin,
                            ValorArancelMin = (updatedBaseParameter.CostoOptimo * updatedBaseParameter.PorcentajeCostoOptimoAnual * updatedBaseParameter.PorcentajeValorMin) / 1.1f,
                            ValorMatriculaMin = ((updatedBaseParameter.CostoOptimo * updatedBaseParameter.PorcentajeCostoOptimoAnual * updatedBaseParameter.PorcentajeValorMin) / 1.1f) * 0.1f,
                            ValorMax = (updatedBaseParameter.CostoOptimo * updatedBaseParameter.PorcentajeCostoOptimoAnual) * updatedBaseParameter.PorcentajeValorMax,
                            ValorArancelMax = ((updatedBaseParameter.CostoOptimo * updatedBaseParameter.PorcentajeCostoOptimoAnual) * updatedBaseParameter.PorcentajeValorMax) / 1.1f,
                            ValorMatriculaMax = (((updatedBaseParameter.CostoOptimo * updatedBaseParameter.PorcentajeCostoOptimoAnual) * updatedBaseParameter.PorcentajeValorMax) / 1.1f) * 0.1f,
                            HoraPeriodoAcademico = updatedBaseParameter.HoraPeriodoAcademico,
                            HoraPromedioPeriodoAcademico = (float)Math.Floor(updatedBaseParameter.HoraPeriodoAcademico * updatedBaseParameter.PorcentajePromedioAcademico),
                            CreditoPeriodoAcademico = updatedBaseParameter.CreditoPeriodoAcademico,
                            CreditoPerdidaTemporal = (float)Math.Floor(updatedBaseParameter.CreditoPeriodoAcademico * updatedBaseParameter.PorcentajePerdidaTemporal),
                            CostoHoraPeriodo = (updatedBaseParameter.CostoOptimo * updatedBaseParameter.PorcentajeCostoOptimoAnual) / ((float)Math.Floor(updatedBaseParameter.HoraPeriodoAcademico * updatedBaseParameter.PorcentajePromedioAcademico) * 1.1f),
                            PorcentajeCostoOptimoAnual = updatedBaseParameter.PorcentajeCostoOptimoAnual,
                            PorcentajeValorMin = updatedBaseParameter.PorcentajeValorMin,
                            PorcentajeValorMax = updatedBaseParameter.PorcentajeValorMax,
                            PorcentajeValorArancel = updatedBaseParameter.PorcentajeValorArancel,
                            PorcentajePromedioAcademico = updatedBaseParameter.PorcentajePromedioAcademico,
                            PorcentajePerdidaTemporal = updatedBaseParameter.PorcentajePerdidaTemporal,
                            PorcentajeMatriculaExtraordinario = updatedBaseParameter.PorcentajeMatriculaExtraordinario,
                            PorcentajeMatriculaEspecial = updatedBaseParameter.PorcentajeMatriculaEspecial,
                            PorcentajeRecargoSegunda = updatedBaseParameter.PorcentajeRecargoSegunda,
                            PorcentajeRecargoTercera = updatedBaseParameter.PorcentajeRecargoTercera,
                        };

                        _db.BaseParameters.Update(newBaseParameter);
                        await _db.SaveChangesAsync();

                        return new UpdateBaseParametersResponseDto()
                        {
                            Success = true,
                            Message = "Los Parametros han sido actualizados correctamente.",
                            Result = _mapper.Map<BaseParameterDto>(newBaseParameter),
                        };
                    }
                    else
                    {
                        return new UpdateBaseParametersResponseDto()
                        {
                            Success = false,
                            Message = "No se encontró la información de los parametros a actualizar.",
                            Result = null,
                        };
                    }
                }
                else
                {
                    return new UpdateBaseParametersResponseDto()
                    {
                        Success = false,
                        Message = "Ha ocurrido un error al buscar la información de los parametros a actualizar.",
                        Result = null,
                    };
                }
            }
            catch (Exception)
            {
                return new UpdateBaseParametersResponseDto()
                {
                    Success = false,
                    Message = "Ha ocurrido un error al actualizar la información de los parametros.",
                    Result = null,
                };
            }
        }
    }
}
