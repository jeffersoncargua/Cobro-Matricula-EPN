import { useEffect, useState } from "react"

export const UserCard = ({Photo, User, Action,handleOption,enableForm}) => {

    const [styleColor,setStyleColor] = useState('');
  
    useEffect(()=>{
        if (User === 'Administrador') {
            setStyleColor('px-3 py-2 bg-green-600 mb-4 rounded-lg hover:bg-green-700 hover:text-white');
        }else{
            setStyleColor('px-3 py-2 bg-blue-600 mb-4 rounded-lg hover:bg-blue-700 hover:text-white');
        }
    },[User])
    
    return (
    <section className="flex flex-col items-center mx-auto gap-y-2.5 text-sm">
        <div className="rounded-full h-20 w-20 mt-4 shadow-lg shadow-slate-50">
            <img src={Photo} alt="Aqui va la foto del admin" className='h-20 w-20 rounded-full' />
        </div>
        
        <span className="underline font-semibold text-center">{User}</span>
        {enableForm && User==='Administrador' ?(
            <button onClick={() => handleOption('Cancelar')} className={`${styleColor} ring-2 ring-black`}>Cancelar</button>
        ):(
            <button onClick={() => handleOption(Action)} className={`${styleColor} ring-2 ring-black`}>{Action}</button>
        )}
        
    </section>
  )
}
