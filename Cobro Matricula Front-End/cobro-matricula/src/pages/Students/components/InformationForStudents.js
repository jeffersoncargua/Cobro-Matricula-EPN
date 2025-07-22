

export const InformationForStudents = () => {
  return (
    <div className="container w-full mt-4 bg-emerald-950/80 p-4 rounded-lg">
        <h1 className="text-white underline underline-offset-4 font-bold">Información Importante</h1>
        <div className="grid grid-cols-1 md:grid-cols-2 mt-6 gap-y-2 md:gap-y-0">
            <section className="w-[90%] mx-auto text-white text-sm border rounded-lg p-4 md:hover:-translate-y-2 md:hover:-translate-x-4 md:hover:scale-[1.08] md:hover:shadow-lg md:hover:shadow-white md:transition md:delay-150 md:duration-300 md:ease-in-out">
                <h3 className="underline underline-offset-4 flex flex-row justify-center items-center text-center mb-4">
                    <span>
                        <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" className="bi bi-exclamation-circle-fill text-blue-500 h-5 w-5 me-2.5" viewBox="0 0 16 16">
                            <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0M8 4a.905.905 0 0 0-.9.995l.35 3.507a.552.552 0 0 0 1.1 0l.35-3.507A.905.905 0 0 0 8 4m.002 6a1 1 0 1 0 0 2 1 1 0 0 0 0-2"/>
                        </svg>
                    </span>
                    Para el Estudiante:
                </h3>
                <p className="text-justify">Si no recuerdas el quintil socioeconómico al que perteneces, sigue las siguientes instrucciones:</p>
                <ul className="text-justify list-disc list-inside ">
                    <li>Ingresa al SAEW con el siguiente enlace <a href='https://saew.epn.edu.ec/' target="_blank" rel="noopener noreferrer" className={`text-pink-500 hover:underline`} >https://saew.epn.edu.ec/</a>.</li>
                    <li>Accede al sistema con tus credenciales</li>
                    <li>Dirigete al módulo de Información Estdiantil.</li>
                    <li>En la pestaña Matriculación selecciona Información de Pagos.</li>
                    <li>En Informacion de Costo de Matricula selecciona tu carrera y observaras la informacion del Quintil al que perteneces. </li>
                </ul>
            </section>
            <section className="w-[90%] mx-auto text-sm text-white border rounded-lg p-4 md:hover:-translate-y-2 md:hover:translate-x-4 md:hover:scale-[1.08] md:hover:shadow-lg md:hover:shadow-white md:transition md:delay-150 md:duration-300 md:ease-in-out">
                <h3 className="underline underline-offset-4 flex flex-row justify-center items-center text-center mb-4">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-info-circle-fill text-green-500 w-5 h-5 me-2.5" viewBox="0 0 16 16">
                      <path d="M8 16A8 8 0 1 0 8 0a8 8 0 0 0 0 16m.93-9.412-1 4.705c-.07.34.029.533.304.533.194 0 .487-.07.686-.246l-.088.416c-.287.346-.92.598-1.465.598-.703 0-1.002-.422-.808-1.319l.738-3.468c.064-.293.006-.399-.287-.47l-.451-.081.082-.381 2.29-.287zM8 5.5a1 1 0 1 1 0-2 1 1 0 0 1 0 2"/>
                    </svg>    
                    Considerar lo siguiente:
                </h3>
                <ul className="text-justify list-disc list-inside">
                    <li>Si reporbaste asignaturas en el periodo academico previo, tu sitiacion de gratuidad es de perdida de Gratuidad Parcial.</li>
                    <li>En el caso de las carreras del nuevo regimen, si no te inscribiste en el 60% de los creditos del nivel referencial (15 créditos), tu situacion de gratuidad es de Perdida Temporal de Gratuidad.</li>
                    <li>Si acumulaste mas del 30% de creditos u horas de asignaturas fallidas, respecto a los creditos u horas totales de tu carrera, tu situacion de gratuidad es de Perdida Definitiva de Gratuidad.</li>
                    <li>Si deseas conocer el % de Reprobacion de la Carrera, visita el <a className="hover:underline text-pink-500" href="http://saew.epn.edu.ec/" target="_blank" rel="noopener noreferrer">SAEW </a>y revisa tu Curriculum Academico</li>
                    <li>Para efectos de calculo, cosidera que 1 credito equivale a 16 horas.</li>
                </ul>
            </section>
        </div>
    </div>
  )
}
