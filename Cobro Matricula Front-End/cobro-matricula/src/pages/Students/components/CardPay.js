

export const CardPay = ({title, gratuidad, payment ,recargoExtraordinaria = null}) => {
  return (
    <section className={`w-[70%] mx-auto text-white text-sm text-start ${recargoExtraordinaria !== null && ('md:mt-0 mt-4')} `}>
        <h2 className="underline underline-offset-4 mb-4">Matricula {title}:</h2>
        <span className="">Condicion de Gratuidad: <b className="font-bold italic text-red-600" >{gratuidad}</b></span>
        <div className='bg-slate-50/90 mt-2 rounded-r-md hover:scale-[1.05] transition delay-150 duration-300 ease-in-out relative hover:shadow-lg hover:shadow-white/50'>
            <div className="border-l-[10px] border-indigo-500 mix-blend-multiply p-2 pb-4">
                <table className="w-[90%] mx-auto text-slate-950 text-center ">
                    <thead className="">
                        <tr className="">
                            <th scope="col" className="p-2">Detalle</th>
                            <th scope="col" className="p-2">Valor</th>
                        </tr>
                    </thead>
                    <tbody className="border-b-2 border-slate-600">
                        <tr>
                            <td>Valor Matrícula</td>
                            <td> $ {payment.valorMatricula}</td>
                        </tr>
                        <tr>
                            <td>Valor Arancel</td>
                            <td> $ {payment.valorArancel}</td>
                        </tr>
                        {recargoExtraordinaria !=null && (
                            <tr>
                                <td>Recargo Matricula Extraordinaria/Especial</td>
                                <td> $ {recargoExtraordinaria}</td>
                            </tr>
                        )}
                        
                        <tr>
                            <td>Recargo Segunda Matricula</td>
                            <td> $ {payment.recargoSegunda}</td>
                        </tr>
                        <tr>
                            <td>Recargo Tercera Matricula</td>
                            <td> $ {payment.recargoTercera}</td>
                        </tr>
                        <tr>
                            <td>Bancario</td>
                            <td> $ {payment.bancario}</td>
                        </tr>
                    </tbody>
                    <tfoot className="">
                        <tr className="h-1"></tr>
                        <tr className="">
                            <td className="underline underline-offset-4 p-0.5">Total a Pagar:</td>
                            <td className='text-white hover:scale-[1.1] bg-gradient-to-r from-red-700 from-20% via-orange-500 via-25% to-yellow-400 to-80% p-0.5' >
                                <div className="w-full bg-white text-black hover:text-white hover:bg-gradient-to-r hover:from-red-700 hover:from-19% hover:via-orange-500 hover:via-24% hover:to-yellow-400 hover:to-79% "> $ {recargoExtraordinaria ===null ? payment.valorTotal : payment.valorTotal+recargoExtraordinaria} </div>
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <div className="w-full absolute bottom-0 left-0 border-b-8 border-red-500 rounded-br-md mix-blend-multiply"></div>
        </div>
    </section>
  )
}
