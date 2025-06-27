using AutoMapper;
using Cobro_Matricula_EPN.Context;
using Cobro_Matricula_EPN.Repository.IRepository;
using Entity.DTO.BaseParameter;
using Entity.Entities;

namespace Cobro_Matricula_EPN.Repository
{
    public class BaseParameterRepository : Repository<BaseParameter> ,IBaseParameterRepository 
    {
        private readonly ApplicationDbContext _db;
        public BaseParameterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<UpdateBaseParametersResponseDto> UpdateAsync(int id, UpdatedBaseParameterRequestDto updatedBaseParameter)
        {
            if (id == updatedBaseParameter.Id && updatedBaseParameter !=null)
            {
                var updateBaseParameter = await _db.BaseParameters.FindAsync(id);
                BaseParameter newBaseParameter = new()
                {
                    Id = id,
                    FormacionAcademica = updatedBaseParameter.FormacionAcademica,
                    CostoOptimo = updatedBaseParameter.CostoOptimo,
                    CostoOptimoPeriodo = updatedBaseParameter.CostoOptimo * updatedBaseParameter.PorcentajeCostoOptimoAnual,
                    ValorMin = updatedBaseParameter.CostoOptimo * updatedBaseParameter.PorcentajeCostoOptimoAnual * updatedBaseParameter.PorcentajeValorMin,
                    ValorArancelMin = (updatedBaseParameter.CostoOptimo * updatedBaseParameter.PorcentajeCostoOptimoAnual * updatedBaseParameter.PorcentajeValorMin) / 1.1f,
                    ValorMatriculaMin = ((updatedBaseParameter.CostoOptimo * updatedBaseParameter.PorcentajeCostoOptimoAnual * updatedBaseParameter.PorcentajeValorMin) / 1.1f)*0.1f,
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
                    PorcentajeArancelEspecial = updatedBaseParameter.PorcentajeArancelEspecial,
                    PorcentajeRecargoSegunda = updatedBaseParameter.PorcentajeRecargoSegunda,
                    PorcentajeRecargoTercera = updatedBaseParameter.PorcentajeRecargoTercera
                };

                _db.BaseParameters.Update(newBaseParameter);
                await _db.SaveChangesAsync();

                return new UpdateBaseParametersResponseDto()
                {
                    Success = true,
                    Message = "Los Parametros han sido actualizados correctamente.",
                };
            }

            return new UpdateBaseParametersResponseDto()
            {
                Success = false,
                Message = "Ha ocurrido un error al actualizar la información de los parametros.",
            };
        }
    }
}
