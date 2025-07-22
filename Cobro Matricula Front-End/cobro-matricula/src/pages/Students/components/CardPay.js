

export const CardPay = ({title, gratuidad, payment ,recargoExtraordinaria = null}) => {
  return (
    <section className={`w-[70%] mx-auto text-white text-sm text-start ${recargoExtraordinaria !== null && ('md:mt-0 mt-4')}`}>
        <h2 className="underline underline-offset-4 mb-4">Matricula {title}:</h2>
        <span className="">Condicion de Gratuidad: <b className="font-bold italic text-red-600" >{gratuidad}</b></span>
        <div className='bg-slate-50/90 mt-2 rounded-r-md hover:scale-[1.05] transition delay-150 duration-300 ease-in-out relative'>
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
                            <td> $ 45,56</td>
                        </tr>
                        <tr>
                            <td>Valor Arancel</td>
                            <td> $ 45,56</td>
                        </tr>
                        {recargoExtraordinaria !=null && (
                            <tr>
                                <td>Recargo Matricula Extraordinaria/Especial</td>
                                <td> $ {recargoExtraordinaria}</td>
                            </tr>
                        )}
                        
                        <tr>
                            <td>Recargo Segunda Matricula</td>
                            <td> $ 45,56</td>
                        </tr>
                        <tr>
                            <td>Recargo Tercera Matricula</td>
                            <td> $ 45,56</td>
                        </tr>
                        <tr>
                            <td>Bancario</td>
                            <td> $ 45,56</td>
                        </tr>
                    </tbody>
                    <tfoot className="">
                        <hr className="border border-transparent mt-2" />
                        <tr className="">
                            <td className="underline underline-offset-4 ">Total a Pagar:</td>
                            <td className='outline outline-offset-2 outline-2 outline-green-500 text-white bg-blue-500 rounded-full hover:scale-[1.1]' > $ 45,56</td>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <div className="w-full absolute bottom-0 left-0 border-b-8 border-red-500 rounded-br-md mix-blend-multiply"></div>
        </div>
    </section>
  )
}
