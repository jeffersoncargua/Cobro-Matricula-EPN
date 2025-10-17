import { useEffect } from "react";

export const TableParameters = ({ baseParameters }) => {
	//Este useEffect permite que la segunda columna de la tabla tenga el texto alineado a la derecha
	//Esto se hace para que los valores monetarios y porcentajes se vean mejor en la tabla
	useEffect(() => {
		var valorRef = document.querySelectorAll("tr td:nth-child(2)");
		valorRef.forEach((valor) => {
			valor.classList.add("text-right");
		});
	}, []);

	return (
		<div className="relative overflow-x-auto border shadow-md sm:rounded-lg mb-10">
			<table className="w-full text-xs md:text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400">
				<thead className="text-xs text-white uppercase bg-transparent">
					<tr>
						<th scope="col" className="px-6 py-3">
							Parámetro
						</th>
						<th scope="col" className="px-6 py-3">
							Valor Referencial
						</th>
					</tr>
				</thead>
				<tbody className="bg-transparent/50 text-white">
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Costo Óptimo</td>
						<td className="px-6 py-4">
							$ {baseParameters.costoOptimo.toFixed(2) || "00.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Costo Óptimo Periodo</td>
						<td className="px-6 py-4">
							$ {baseParameters.costoOptimoPeriodo.toFixed(2) || "00.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Valor Mínimo</td>
						<td className="px-6 py-4">
							$ {baseParameters.valorMin.toFixed(2) || "00.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Valor Matrícula Mínimo</td>
						<td className="px-6 py-4">
							$ {baseParameters.valorMatriculaMin.toFixed(2) || "00.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Valor Arancel Mínimo</td>
						<td className="px-6 py-4">
							$ {baseParameters.valorArancelMin.toFixed(2) || "00.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Valor Máximo</td>
						<td className="px-6 py-4">
							$ {baseParameters.valorMax.toFixed(2) || "00.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Valor Matrícula Máximo</td>
						<td className="px-6 py-4">
							$ {baseParameters.valorMatriculaMax.toFixed(2) || "00.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Valor Arancel Máximo</td>
						<td className="px-6 py-4">
							$ {baseParameters.valorArancelMax.toFixed(2) || "00.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Hora Periodo Academico</td>
						<td className="px-6 py-4">
							{baseParameters.horaPeriodoAcademico || "0"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Hora Promedio Periodo Academico</td>
						<td className="px-6 py-4">
							{Math.ceil(baseParameters.horaPromedioPeriodoAcademico) || "0"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Creditos Periodo Academico</td>
						<td className="px-6 py-4">
							{baseParameters.creditoPeriodoAcademico || "0"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Creditos Perdida Temporal</td>
						<td className="px-6 py-4">
							{baseParameters.creditoPerdidaTemporal || "0"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Costo Hora Periodo</td>
						<td className="px-6 py-4">
							$ {baseParameters.costoHoraPeriodo.toFixed(2) || "00.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Porcentaje Costo Optimo Anual</td>
						<td className="px-6 py-4">
							% {baseParameters.porcentajeCostoOptimoAnual * 100 || "%0.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Porcentaje Valor Mínimo</td>
						<td className="px-6 py-4">
							% {baseParameters.porcentajeValorMin * 100 || "%0.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Porcentaje Valor Máximo</td>
						<td className="px-6 py-4">
							% {baseParameters.porcentajeValorMax * 100 || "%0.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Porcentaje Valor Arancel</td>
						<td className="px-6 py-4">
							% {baseParameters.porcentajeValorArancel * 100 || "%0.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Porcentaje Promedio Academico</td>
						<td className="px-6 py-4">
							% {baseParameters.porcentajePromedioAcademico * 100 || "%0.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Procentaje Perdida Temporal</td>
						<td className="px-6 py-4">
							% {baseParameters.porcentajePerdidaTemporal * 100 || "%0.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Procentaje Matricula Extraordinario</td>
						<td className="px-6 py-4">
							%{" "}
							{baseParameters.porcentajeMatriculaExtraordinario * 100 ||
								"%0.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Procentaje Matricula Especial</td>
						<td className="px-6 py-4">
							% {baseParameters.porcentajeMatriculaEspecial * 100 || "%0.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Procentaje Recargo Segunda</td>
						<td className="px-6 py-4">
							% {baseParameters.porcentajeRecargoSegunda * 100 || "%0.00"}
						</td>
					</tr>
					<tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
						<td className="px-6 py-4">Procentaje Recargo Tercera</td>
						<td className="px-6 py-4">
							% {baseParameters.porcentajeRecargoTercera * 100 || "%0.00"}
						</td>
					</tr>
				</tbody>
			</table>
		</div>
	);
};
