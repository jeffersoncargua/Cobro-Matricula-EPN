

export const FormCalculator = () => {

    const handleCleanForm = () => {

    }

  return (
    <form className="w-[90%] md:w-full mx-auto bg-emerald-950/50 p-2">
        <div className="w-full grid grid-cols-1 md:grid-cols-2 gap-y-4 md:gap-y-0 ">
            <section className="p-4">
                <div className="flex flex-col gap-y-2 text-start">
                    <label htmlFor="formacionAcademica" className="underline underline-offset-4 font-bold text-white ">Formación Académica:</label>
                    <select name="formacionAcademica" id="formacionAcademica" className="p-2 rounded-lg text-center text-sm italic">
                        <option >--- Seleccione su Formación Académica ---</option>
                        <option value={1}>Ingenieria</option>
                        <option value={2}>Tecnologia</option>
                    </select>
                </div>
                <div className="flex flex-col gap-y-2 text-start mt-8">
                    <label htmlFor="regimen" className="underline underline-offset-4 font-bold text-white ">Regimén:</label>
                    <select name="regimen" id="regimen" className="p-2 rounded-lg text-center text-sm italic">
                        <option >--- Seleccione su Régimen ---</option>
                        <option value={1}>Horas</option>
                        <option value={2}>Créditos</option>
                    </select>
                </div>
                <div className="flex flex-col gap-y-2 text-start mt-8">
                    <label htmlFor="quintil" className="underline underline-offset-4 font-bold text-white ">Quintil:</label>
                    <select name="quintil" id="quintil" className="p-2 rounded-lg text-center text-sm italic">
                        <option >--- Seleccione su Quintil ---</option>
                        <option value={1}>1</option>
                        <option value={2}>2</option>
                        <option value={3}>3</option>
                        <option value={4}>4</option>
                        <option value={5}>5</option>
                    </select>
                </div>
            </section>
            <section className="p-4 flex flex-col gap-y-8">
                <div className="w-full flex text-start items-center">
                    <label htmlFor="perdidaGratuidad" className="underline underline-offset-4 font-bold text-white ">Perdida de Gratuidad: </label>
                    <ul className="items-center w-full text-sm font-medium text-gray-900 bg-white border border-gray-200 rounded-lg sm:flex ">
                        <li className="w-full border-b border-gray-200 sm:border-b-0 sm:border-r dark:border-gray-600">
                            <div className="flex items-center ps-3">
                                <input id="gratuidadYes" type="radio" value="" name="list-radio" className="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 focus:ring-blue-500 " />
                                <label htmlFor="gratuidadYes" className="w-full py-3 ms-2 text-sm font-medium text-gray-900 dark:text-gray-300">Si</label>
                            </div>
                        </li>
                        <li className="w-full border-b border-gray-200 sm:border-b-0 sm:border-r dark:border-gray-600">
                            <div className="flex items-center ps-3">
                                <input id="gratuidadNo" type="radio" value="" name="list-radio" className="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 focus:ring-blue-500 " />
                                <label htmlFor="gratuidadNo" className="w-full py-3 ms-2 text-sm font-medium text-gray-900 dark:text-gray-300">No</label>
                            </div>
                        </li>
                    </ul>
                </div>
                <div className="flex flex-col gap-y-4 text-start">
                    <label className="undeline underline-offset-4 font-bold text-white">Asignaturas en créditos/horas a cursar en: </label>
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-y-2 md:gap-y-0">
                        <label htmlFor="primeraMatricula" className="text-white underline underline-offset-4 font-bold">Primera Matrícula: </label>
                        <input id="primeraMatricula" name="primeraMatricula" type='number' defaultValue={0} className="ms-3 p-2 focus:outline-none focus:border-sky-500 focus:ring focus:ring-sky-500 rounded-lg text-end" placeholder="0"  />
                    </div>
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-y-2 md:gap-y-0">
                        <label htmlFor="segundaMatricula" className="text-white underline underline-offset-4 font-bold">Segunda Matrícula: </label>
                        <input id="segundaMatricula" name="segundaMatricula" type='number' defaultValue={0} className="ms-3 p-2 focus:outline-none focus:border-sky-500 focus:ring focus:ring-sky-500 rounded-lg text-end" placeholder="0"  />
                    </div>
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-y-2 md:gap-y-0">
                        <label htmlFor="terceraMatricula" className="text-white underline underline-offset-4 font-bold">Tercera Matrícula: </label>
                        <input id="terceraMatricula" name="terceraMatricula" type='number' defaultValue={0} className="ms-3 p-2 focus:outline-none focus:border-sky-500 focus:ring focus:ring-sky-500 rounded-lg text-end" placeholder="0"  />
                    </div>
                </div>
                
            </section>
        </div>
        <div className="w-[90%] mx-auto flex flex-row justify-between space-x-10 mt-2">
            <button onClick={() => handleCleanForm()} className="w-full bg-green-700 text-white p-4 rounded-lg transition delay-150 duration-300 ease-in-out hover:bg-blue-500 hover:text-black hover:scale-[1.05]">Nueva Consulta</button>
            <button type="submit" className="w-full bg-blue-700 text-white p-4 rounded-lg transition delay-150 duration-300 ease-in-out hover:bg-green-500 hover:text-black hover:scale-[1.05]">Calcular</button>
        </div>
    </form>
  )
}
