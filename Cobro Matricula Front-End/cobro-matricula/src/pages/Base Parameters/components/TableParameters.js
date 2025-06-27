import { useEffect } from "react";

export const TableParameters = () => {


    useEffect(() => {
        var valorRef = document.querySelectorAll('tr td:nth-child(2)');
        valorRef.forEach((valor) => {
            valor.classList.add('text-right');
        });
    }, [])

  return (
    <div className="relative overflow-x-auto border shadow-md sm:rounded-lg mb-10">
        <table className="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400">
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
                    <td className="px-6 py-4">
                        Costo Óptimo
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Costo Óptimo Periodo
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Valor Mínimo
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Valor Matrícula Mínimo
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Valor Arancel Mínimo
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Valor Máximo
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Valor Matrícula Máximo
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Valor Arancel Máximo
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Hora Periodo Academico
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Hora Promedio Periodo Academico
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Hora Promedio Periodo Academico
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Creditos Periodo Academico
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Creditos Periodo Temporal
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Porcentaje Costo Optimo Anual
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Porcentaje Valor Mínimo
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Porcentaje Valor Máximo
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Porcentaje Valor Arancel
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Porcentaje Promedio Academico
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Procentaje Perdida Temporal
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Procentaje Matricula Extraordinario
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Procentaje Matricula Especial
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Procentaje Recargo Segunda
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
                <tr className="border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-transparent/80">
                    <td className="px-6 py-4">
                        Procentaje Recargo Tercera
                    </td>
                    <td className="px-6 py-4">
                        $2999
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
  )
}
