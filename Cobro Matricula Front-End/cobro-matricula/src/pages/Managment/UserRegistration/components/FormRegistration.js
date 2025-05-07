import { useRef, useState } from "react";
import { Link } from "react-router-dom"

export const FormRegistration = () => {

    var user; // sirve para enviar los datos del formulario a la API

    const [enableRegistrationButton, setEnableRegistrationButton] = useState(false);
    
    const nameRef = useRef();
    const lastNameRef = useRef();
    const cityRef = useRef();
    const phoneRef = useRef()
    const emailRef = useRef();
    const passRef = useRef();
    const confirmPassRef = useRef();

    const handleSubmit = () => {

    }


  return (
    <form className="group text-slate-900 p-4 border border-slate-100 rounded-lg bg-gradient-to-r from-indigo-500/75 from-10% via-sky-500 via-30% to-emerald-500/75 to-90%" onSubmit={() => handleSubmit()}>
        <div className="grid gap-6 mb-6 md:grid-cols-2  ">
            <div>
                <label htmlFor="first_name" className="block mb-2 text-sm font-medium  dark:text-white">Nombre</label>
                <input type="text" id="first_name" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Roberta" required ref={nameRef} />
            </div>
            <div>
                <label htmlFor="last_name" className="block mb-2 text-sm font-medium  dark:text-white">Apellido</label>
                <input type="text" id="last_name" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Mendez" required ref={lastNameRef} />
            </div>
            <div>
                <label htmlFor="city" className="block mb-2 text-sm font-medium  dark:text-white">Ciudad</label>
                <input type="text" id="city" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Guayaquil" required ref={cityRef} />
            </div>  
            <div>
                <label htmlFor="phone" className="block mb-2 text-sm font-medium  dark:text-white">Telefono: +593</label>
                <input type="tel" id="phone" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="0987654321" pattern="[0-9]{10}" required ref={phoneRef} />
            </div>
        </div>
        <div className="mb-6">
            <label htmlFor="email" className="block mb-2 text-sm font-medium  dark:text-white">Correo Electrónico</label>
            <input type="email" id="email" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="example@example.com" required ref={emailRef} />
        </div> 
        <div className="mb-6">
            <label htmlFor="password" className="block mb-2 text-sm font-medium  dark:text-white">Contraseña</label>
            <input type="password" id="password" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="•••••••••" required ref={passRef} />
        </div> 
        <div className="mb-6">
            <label htmlFor="confirm_password" className="block mb-2 text-sm font-medium  dark:text-white">Confirmar Contraseña</label>
            <input type="password" id="confirm_password" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="•••••••••" required ref={confirmPassRef} />
        </div> 
        <div className="flex items-start mb-6">
            <div className="flex items-center h-5">
            <input id="remember" type="checkbox" value="" onChange={() => setEnableRegistrationButton(!enableRegistrationButton)} className="w-4 h-4 border border-gray-300 rounded-sm bg-gray-50 focus:ring-3 focus:ring-blue-300 dark:bg-gray-700 dark:border-gray-600 dark:focus:ring-blue-600 dark:ring-offset-gray-800" required />
            </div>
            <label htmlFor="remember" className="ms-2 text-sm font-medium  dark:text-gray-300">I agree with the <Link to='/' className="text-pink-600 hover:underline dark:text-blue-500">terms and conditions</Link>.</label>
        </div>
        
        <button type="submit" disabled={enableRegistrationButton ? false: true} className={`text-white ${enableRegistrationButton ? 'bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 cursor-pointer':'bg-blue-400 cursor-not-allowed'} font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800`}>Registrar</button>
    </form>
  )
}
