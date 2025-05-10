import { useRef, useState } from 'react';
import {Link} from 'react-router-dom'

export const Login = ({setEnableForm}) => {

    const [enablePass, setEnablePass] = useState(false);
    const userRef = useRef();
    const passReff = useRef();

    const handleLogin = () => {

    }


  return (
    <form className='border border-slate-600 p-2 rounded-lg w-80 text-sm ' onSubmit={() => handleLogin() } >                    
        <label htmlFor="email" className="text-start block mb-2 font-medium text-gray-900 dark:text-white">Ingresa tu Correo</label>
        <div className="relative mb-6">
            <div className="absolute inset-y-0 start-0 flex items-center ps-3.5 pointer-events-none">
                <svg className="w-4 h-4 text-gray-500 dark:text-gray-400" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 16">
                    <path d="m10.036 8.278 9.258-7.79A1.979 1.979 0 0 0 18 0H2A1.987 1.987 0 0 0 .641.541l9.395 7.737Z"/>
                    <path d="M11.241 9.817c-.36.275-.801.425-1.255.427-.428 0-.845-.138-1.187-.395L0 2.6V14a2 2 0 0 0 2 2h16a2 2 0 0 0 2-2V2.5l-8.759 7.317Z"/>
                </svg>
            </div>
            <input type="text" id="email" className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full ps-10 p-2.5  dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="example@example.com" ref={userRef} />
        </div>
        <label htmlFor="password" className="text-start block mb-2 font-medium text-gray-900 dark:text-white">Ingresar tu contraseña</label>
        <div className="flex">
            <span className="inline-flex items-center px-3 text-gray-900 bg-gray-200 border rounded-e-0 border-gray-300 border-e-0 rounded-s-md dark:bg-gray-600 dark:text-gray-400 dark:border-gray-600">
                <svg className="w-4 h-4 text-gray-500 dark:text-gray-400" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 20">
                    <path d="M0 8a4 4 0 0 1 7.465-2H14a.5.5 0 0 1 .354.146l1.5 1.5a.5.5 0 0 1 0 .708l-1.5 1.5a.5.5 0 0 1-.708 0L13 9.207l-.646.647a.5.5 0 0 1-.708 0L11 9.207l-.646.647a.5.5 0 0 1-.708 0L9 9.207l-.646.647A.5.5 0 0 1 8 10h-.535A4 4 0 0 1 0 8m4-3a3 3 0 1 0 2.712 4.285A.5.5 0 0 1 7.163 9h.63l.853-.854a.5.5 0 0 1 .708 0l.646.647.646-.647a.5.5 0 0 1 .708 0l.646.647.646-.647a.5.5 0 0 1 .708 0l.646.647.793-.793-1-1h-6.63a.5.5 0 0 1-.451-.285A3 3 0 0 0 4 5"/>
                    <path d="M4 8a1 1 0 1 1-2 0 1 1 0 0 1 2 0"/>
                </svg>
            </span>
            <div className='relative w-full'>
                <input type={!enablePass ? 'password':'text'} id="password" className="rounded-none rounded-e-lg bg-gray-50 border text-gray-900 focus:ring-blue-500 focus:border-blue-500 block flex-1 min-w-0 w-full border-gray-300 p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Mjug&/%113" ref={passReff} />
                <button type='button' className='absolute inset-y-0 end-0 flex items-center pe-2.5 ' onClick={()=>setEnablePass(!enablePass)}>
                    {!enablePass ? 
                    (<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-eye-fill hover:text-gray-500" viewBox="0 0 16 16">
                        <path d="M10.5 8a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0"/>
                        <path d="M0 8s3-5.5 8-5.5S16 8 16 8s-3 5.5-8 5.5S0 8 0 8m8 3.5a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7"/>
                    </svg>)
                    :(<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-eye-slash-fill hover:text-gray-500" viewBox="0 0 16 16">
                        <path d="m10.79 12.912-1.614-1.615a3.5 3.5 0 0 1-4.474-4.474l-2.06-2.06C.938 6.278 0 8 0 8s3 5.5 8 5.5a7 7 0 0 0 2.79-.588M5.21 3.088A7 7 0 0 1 8 2.5c5 0 8 5.5 8 5.5s-.939 1.721-2.641 3.238l-2.062-2.062a3.5 3.5 0 0 0-4.474-4.474z"/>
                        <path d="M5.525 7.646a2.5 2.5 0 0 0 2.829 2.829zm4.95.708-2.829-2.83a2.5 2.5 0 0 1 2.829 2.829zm3.171 6-12-12 .708-.708 12 12z"/>
                    </svg>)}
                </button>
            </div>
        </div>
        <div className='mt-2 flex flex-col'>
            <button type='submit' className='px-2.5 py-2.5 text-center bg-cyan-500 hover:bg-cyan-600 hover:text-white rounded-lg'>Iniciar Sesión</button>
            {/* <button type='button' onClick={() => setEnableForm(false) } className='px-2.5 py-2.5 text-center bg-blue-600 hover:bg-blue-700 hover:text-white rounded-lg'>Cancelar</button> */}
            <Link to={'forgetPassword'} className='text-indigo-900 font-semibold hover:text-blue-950 hover:underline px-2.5 py-2.5 '>Olvide mi contraseña</Link>
        </div>
        
    </form>
  )
}
