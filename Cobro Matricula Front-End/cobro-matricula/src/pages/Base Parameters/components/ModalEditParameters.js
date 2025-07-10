import { useForm } from "react-hook-form";
import { ErrorMessageValidator } from "../../../components";
import { message, patterns } from "../../../utility/ValidationParameters";

export const ModalEditParameters = ({enableModalParameters, setEnableModalParameters, handleEditParameters,baseParameters}) => {

    const {register, handleSubmit, formState:{errors}} = useForm({
        defaultValues: {
            id: baseParameters.id,
            formacionAcademica: baseParameters.formacionAcademica,
        }

    });

  return (
    <div id="authentication-modal" tabindex="-1" aria-hidden="true" className={`${!enableModalParameters && 'hidden'} overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 flex justify-center items-center w-full md:inset-0 h-[calc(100%)] max-h-full bg-gray-50/50`}>
        <div className="relative p-4 w-full max-w-lg max-h-full">
            {/* <!-- Modal content --> */}
            <div className="relative bg-white rounded-lg shadow-sm dark:bg-gray-700">
                {/* <!-- Modal header --> */}
                <div className="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600 border-gray-200">
                    <h3 className="text-xl font-semibold text-gray-900 dark:text-white">
                        Editar Parámetros Base
                    </h3>
                    <button type="button" onClick={() => setEnableModalParameters(false)} className="end-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white" data-modal-hide="authentication-modal">
                        <svg className="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
                            <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"/>
                        </svg>
                        <span className="sr-only">Close modal</span>
                    </button>
                </div>
                {/* <!-- Modal body --> */}
                <div className="p-4 md:p-5">
                    <form className="group text-slate-900 p-4 border border-slate-100 rounded-lg " onSubmit={handleSubmit(handleEditParameters)}>
                        <section className="mt-2">
                            <label htmlFor="costoOptimo" className="relative">
                                <input  type='number' step={0.01} id="costoOptimo" {...register('costoOptimo',{required: message.req.costoOptimo, pattern:{value:patterns.decimal10000, message: message.costoOptimo}, valueAsNumber:true})} defaultValue={baseParameters.costoOptimo} className="peer mt-0.5 p-2 w-full rounded border-gray-300 shadow-sm sm:text-sm" />
                                <span className="absolute inset-y-0 start-3 -translate-y-5 bg-white px-1 text-sm font-medium text-blue-600 transition-transform peer-placeholder-shown:translate-y-0 peer-focus:-translate-y-5">
                                    Costo Optimo Anual
                                </span>
                            </label>
                            {errors.costoOptimo && <ErrorMessageValidator message={errors.costoOptimo.message} />}
                        </section>
                        <section className="mt-6">
                            <label htmlFor="horaPeriodoAcademico" className="relative">
                                <input  type='number' step={1} id="horaPeriodoAcademico" {...register('horaPeriodoAcademico', {required: message.req.horaPeriodoAcademico, pattern:{value:patterns.entero1000, message: message.horaPeriodoAcademico}, valueAsNumber:true})} defaultValue={baseParameters.horaPeriodoAcademico} className="peer mt-0.5 p-2 w-full rounded border-gray-300 shadow-sm sm:text-sm" />
                                <span className="absolute inset-y-0 start-3 -translate-y-5 bg-white px-1 text-sm font-medium text-blue-600 transition-transform peer-placeholder-shown:translate-y-0 peer-focus:-translate-y-5">
                                    Hora Periodo Académico
                                </span>
                            </label>
                            {errors.horaPeriodoAcademico && <ErrorMessageValidator message={errors.horaPeriodoAcademico.message} />}
                        </section>
                        <section className="mt-6">
                            <label htmlFor="creditoPeriodoAcademico" className="relative">
                                <input  type='number' step={1} id="creditoPeriodoAcademico" {...register('creditoPeriodoAcademico',{required:message.req.creditoPeriodoAcademico, pattern:{value:patterns.entero100, message: message.creditoPeriodoAcademico}, valueAsNumber:true})} defaultValue={baseParameters.creditoPeriodoAcademico} className="peer mt-0.5 p-2 w-full rounded border-gray-300 shadow-sm sm:text-sm" />
                                <span className="absolute inset-y-0 start-3 -translate-y-5 bg-white px-1 text-sm font-medium text-blue-600 transition-transform peer-placeholder-shown:translate-y-0 peer-focus:-translate-y-5">
                                    Credito Periodo Academico
                                </span>
                            </label>
                            {errors.creditoPeriodoAcademico && <ErrorMessageValidator message={errors.creditoPeriodoAcademico.message} />}
                        </section>
                        <section className="mt-6">
                            <label htmlFor="porcentajeCostoOptimoAnual" className="relative">
                                <input  type='number' max={1.00} min={0.00} step={0.01} id="porcentajeCostoOptimoAnual" {...register('porcentajeCostoOptimoAnual', {required: message.req.porcentajeCostoOptimoAnual, pattern:{value:patterns.decimal, message:message.porcentajeCostoOptimoAnual}, valueAsNumber:true})} defaultValue={baseParameters.porcentajeCostoOptimoAnual} className="peer mt-0.5 p-2 w-full rounded border-gray-300 shadow-sm sm:text-sm" />
                                <span className="absolute inset-y-0 start-3 -translate-y-5 bg-white px-1 text-sm font-medium text-blue-600 transition-transform peer-placeholder-shown:translate-y-0 peer-focus:-translate-y-5">
                                    Porcentaje Costo Optimo Anual
                                </span>
                            </label>
                            {errors.porcentajeCostoOptimoAnual && <ErrorMessageValidator message={errors.porcentajeCostoOptimoAnual.message} />}
                        </section>
                        <section className="mt-6">
                            <label htmlFor="porcentajeValorMin" className="relative">
                                <input  type='number' max={1.00} min={0.00} step={0.01} id="porcentajeValorMin" {...register('porcentajeValorMin',{required: message.req.porcentajeValorMin, pattern:{value:patterns.decimal, message:message.porcentajeValorMin}, valueAsNumber:true})} defaultValue={baseParameters.porcentajeValorMin} className="peer mt-0.5 p-2 w-full rounded border-gray-300 shadow-sm sm:text-sm" />
                                <span className="absolute inset-y-0 start-3 -translate-y-5 bg-white px-1 text-sm font-medium text-blue-600 transition-transform peer-placeholder-shown:translate-y-0 peer-focus:-translate-y-5">
                                    Porcentaje Valor Minimo
                                </span>
                            </label>
                            {errors.porcentajeValorMin && <ErrorMessageValidator message={errors.porcentajeValorMin.message} />}
                        </section>
                        <section className="mt-6">
                            <label htmlFor="porcentajeValorMax" className="relative">
                                <input  type='number' max={1.00} min={0.00} step={0.01} id="porcentajeValorMax" {...register('porcentajeValorMax', {required:message.req.porcentajeValorMax, pattern:{value:patterns.decimal, message:message.porcentajeValorMax}, valueAsNumber:true})} defaultValue={baseParameters.porcentajeValorMax} className="peer mt-0.5 p-2 w-full rounded border-gray-300 shadow-sm sm:text-sm" />
                                <span className="absolute inset-y-0 start-3 -translate-y-5 bg-white px-1 text-sm font-medium text-blue-600 transition-transform peer-placeholder-shown:translate-y-0 peer-focus:-translate-y-5">
                                    Porcentaje Valor Maximo
                                </span>
                            </label>
                            {errors.porcentajeValorMax && <ErrorMessageValidator message={errors.porcentajeValorMax.message} />}
                        </section>
                        <section className="mt-6">
                            <label htmlFor="porcentajeValorArancel" className="relative">
                                <input  type='number' max={1.00} min={0.00} step={0.01} id="porcentajeValorArancel" {...register('porcentajeValorArancel',{required:message.req.porcentajeValorArancel, pattern:{value:patterns.decimal, message:message.porcentajeValorArancel}, valueAsNumber:true})} defaultValue={baseParameters.porcentajeValorArancel} className="peer mt-0.5 p-2 w-full rounded border-gray-300 shadow-sm sm:text-sm" />
                                <span className="absolute inset-y-0 start-3 -translate-y-5 bg-white px-1 text-sm font-medium text-blue-600 transition-transform peer-placeholder-shown:translate-y-0 peer-focus:-translate-y-5">
                                    Porcentaje Valor Arancel
                                </span>
                            </label>
                            {errors.porcentajeValorArancel && <ErrorMessageValidator message={errors.porcentajeValorArancel.message} />}
                        </section>
                        <section className="mt-6">
                            <label htmlFor="porcentajePromedioAcademico" className="relative">
                                <input  type='number' max={1.00} min={0.00} step={0.01} id="porcentajePromedioAcademico" {...register('porcentajePromedioAcademico', {required: message.req.porcentajePromedioAcademico, pattern:{value:patterns.decimal, message: message.porcentajePromedioAcademico}, valueAsNumber:true})} defaultValue={baseParameters.porcentajePromedioAcademico} className="peer mt-0.5 p-2 w-full rounded border-gray-300 shadow-sm sm:text-sm" />
                                <span className="absolute inset-y-0 start-3 -translate-y-5 bg-white px-1 text-sm font-medium text-blue-600 transition-transform peer-placeholder-shown:translate-y-0 peer-focus:-translate-y-5">
                                    Porcentaje Promedio Academico
                                </span>
                            </label>
                            {errors.porcentajePromedioAcademico && <ErrorMessageValidator message={errors.porcentajePromedioAcademico.message} />}
                        </section>
                        <section className="mt-6">
                            <label htmlFor="porcentajePerdidaTemporal" className="relative">
                                <input  type='number' max={1.00} min={0.00} step={0.01} id="porcentajePerdidaTemporal" {...register('porcentajePerdidaTemporal',{required:message.req.porcentajePerdidaTemporal, pattern:{value:patterns.decimal, message:message.porcentajePerdidaTemporal}, valueAsNumber:true})} defaultValue={baseParameters.porcentajePerdidaTemporal} className="peer mt-0.5 p-2 w-full rounded border-gray-300 shadow-sm sm:text-sm" />
                                <span className="absolute inset-y-0 start-3 -translate-y-5 bg-white px-1 text-sm font-medium text-blue-600 transition-transform peer-placeholder-shown:translate-y-0 peer-focus:-translate-y-5">
                                    Porcentaje Perdida Temporal
                                </span>
                            </label>
                            {errors.porcentajePerdidaTemporal && <ErrorMessageValidator message={errors.porcentajePerdidaTemporal.message} />}
                        </section>
                        <section className="mt-6">
                            <label htmlFor="porcentajeMatriculaExtraordinario" className="relative">
                                <input  type='number' max={1.00} min={0.00} step={0.01} id="porcentajeMatriculaExtraordinario" {...register('porcentajeMatriculaExtraordinario',{required:message.req.porcentajeMatriculaExtraordinaria, pattern:{value:patterns.decimal, message: message.porcentajeMatriculaExtraordinaria}, valueAsNumber:true})} defaultValue={baseParameters.porcentajeMatriculaExtraordinario} className="peer mt-0.5 p-2 w-full rounded border-gray-300 shadow-sm sm:text-sm" />
                                <span className="absolute inset-y-0 start-3 -translate-y-5 bg-white px-1 text-sm font-medium text-blue-600 transition-transform peer-placeholder-shown:translate-y-0 peer-focus:-translate-y-5">
                                    Porcentaje Matricula Extraordinario
                                </span>
                            </label>
                            {errors.porcentajeMatriculaExtraordinario && <ErrorMessageValidator message={errors.porcentajeMatriculaExtraordinario.message} />}
                        </section>
                        <section className="mt-6">
                            <label htmlFor="porcentajeMatriculaEspecial" className="relative">
                                <input  type='number' max={1.00} min={0.00} step={0.01} id="porcentajeMatriculaEspecial" {...register('porcentajeMatriculaEspecial', {required: message.req.porcentajeMatriculaEspecial, pattern:{value:patterns.decimal, message: message.porcentajeMatriculaEspecial}, valueAsNumber:true})} defaultValue={baseParameters.porcentajeMatriculaEspecial} className="peer mt-0.5 p-2 w-full rounded border-gray-300 shadow-sm sm:text-sm" />
                                <span className="absolute inset-y-0 start-3 -translate-y-5 bg-white px-1 text-sm font-medium text-blue-600 transition-transform peer-placeholder-shown:translate-y-0 peer-focus:-translate-y-5">
                                    Porcentaje Matricula Especial
                                </span>
                            </label>
                            {errors.porcentajeMatriculaEspecial && <ErrorMessageValidator message={errors.porcentajeMatriculaEspecial.message} />}
                        </section>
                        <section className="mt-6">
                            <label htmlFor="porcentajeRecargoSegunda" className="relative">
                                <input  type='number' max={1.00} min={0.00} step={0.01} id="porcentajeRecargoSegunda" {...register('porcentajeRecargoSegunda', { required: message.req.porcentajeRecargoSegunda, pattern:{value:patterns.decimal, message: message.porcentajeRecargoSegunda}, valueAsNumber:true})} defaultValue={baseParameters.porcentajeRecargoSegunda} className="peer mt-0.5 p-2 w-full rounded border-gray-300 shadow-sm sm:text-sm" />
                                <span className="absolute inset-y-0 start-3 -translate-y-5 bg-white px-1 text-sm font-medium text-blue-600 transition-transform peer-placeholder-shown:translate-y-0 peer-focus:-translate-y-5">
                                    Porcentaje Recargo Segunda
                                </span>
                            </label>
                            {errors.porcentajeRecargoSegunda && <ErrorMessageValidator message={errors.porcentajeRecargoSegunda.message} />}
                        </section>
                        <section className="mt-6">
                            <label htmlFor="porcentajeRecargoTercera" className="relative">
                                <input  type='number' max={1.00} min={0.00} step={0.01} id="porcentajeRecargoTercera" {...register('porcentajeRecargoTercera',{required: message.req.porcentajeRecargoTercera, pattern:{value: patterns.decimal, message: message.porcentajeRecargoTercera}, valueAsNumber:true})} defaultValue={baseParameters.porcentajeRecargoTercera} className="peer mt-0.5 p-2 w-full rounded border-gray-300 shadow-sm sm:text-sm" />
                                <span className="absolute inset-y-0 start-3 -translate-y-5 bg-white px-1 text-sm font-medium text-blue-600 transition-transform peer-placeholder-shown:translate-y-0 peer-focus:-translate-y-5">
                                    Porcentaje Recargo Tercera
                                </span>
                            </label>
                            {errors.porcentajeRecargoTercera && <ErrorMessageValidator message={errors.porcentajeRecargoTercera.message} />}
                        </section>
                        <button type="submit" className="mt-4 px-2.5 py-2 bg-blue-900 w-full text-white hover:bg-blue-700 hover:font-bold">Editar</button>                       
                    </form>
                </div>
            </div>
        </div>
    </div> 
  )
}
