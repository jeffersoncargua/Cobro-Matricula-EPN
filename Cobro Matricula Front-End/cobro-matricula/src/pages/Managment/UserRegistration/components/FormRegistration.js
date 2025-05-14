import { useRef, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import Swal from 'sweetalert2';

export const FormRegistration = () => {

    //var user; // sirve para enviar los datos del formulario a la API

    const [enableRegistrationButton, setEnableRegistrationButton] = useState(false);
    const [enablePass, setEnablePass] = useState(false);
    const [enableConfirmPass, setEnableConfirmPass] = useState(false);

    
    const nameRef = useRef();
    const lastNameRef = useRef();
    const cityRef = useRef();
    const phoneRef = useRef()
    const emailRef = useRef();
    const passRef = useRef();
    const confirmPassRef = useRef();
    const navigate = useNavigate();

    const handleSubmit = (e) => {
        e.preventDefault();
        Swal.fire({
        title: "Registro Exitoso!!",
        text: "Revisa tu correo para confirmar tu registro!!",
        icon: "success",
        draggable: true,
        confirmButtonText: "Listo",
        customClass: "text-sm"
        }).then(result => {
            if(result.isConfirmed){
                navigate('/');
            }
        });
    }


  return (
    <form className="w-full md:w-1/2 text-slate-900 p-4 m-4 border border-slate-100 rounded-lg bg-gradient-to-r from-indigo-500/75 from-10% via-sky-500 via-30% to-emerald-500/75 to-90%" onSubmit={handleSubmit}>
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
            <div className="w-full relative">
                <input type={enablePass ? 'text':"password" } id="password" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="•••••••••" required ref={passRef} />
                <button type="button" onClick={() =>setEnablePass(!enablePass)} className="absolute inset-y-0 end-0 flex items-center pe-2.5 text-black hover:text-cyan-600">
                    {enablePass ? 
                    (
                        <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" className="bi bi-eye-fill w-5 h-5" viewBox="0 0 16 16">
                            <path d="M10.5 8a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0"/>
                            <path d="M0 8s3-5.5 8-5.5S16 8 16 8s-3 5.5-8 5.5S0 8 0 8m8 3.5a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7"/>
                        </svg>
                    )
                    :
                    (
                        <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" className="bi bi-eye-slash-fill w-5 h-5" viewBox="0 0 16 16">
                            <path d="m10.79 12.912-1.614-1.615a3.5 3.5 0 0 1-4.474-4.474l-2.06-2.06C.938 6.278 0 8 0 8s3 5.5 8 5.5a7 7 0 0 0 2.79-.588M5.21 3.088A7 7 0 0 1 8 2.5c5 0 8 5.5 8 5.5s-.939 1.721-2.641 3.238l-2.062-2.062a3.5 3.5 0 0 0-4.474-4.474z"/>
                            <path d="M5.525 7.646a2.5 2.5 0 0 0 2.829 2.829zm4.95.708-2.829-2.83a2.5 2.5 0 0 1 2.829 2.829zm3.171 6-12-12 .708-.708 12 12z"/>
                        </svg>
                    )}
                </button>
            </div>
            
        </div> 
        <div className="mb-6 ">
            <label htmlFor="confirm_password" className="block mb-2 text-sm font-medium  dark:text-white">Confirmar Contraseña</label>
            <div className="w-full relative ">
                <input type={enableConfirmPass ? 'text':"password"} id="confirm_password" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="•••••••••" required ref={confirmPassRef} />
                <button type="button" onClick={() => setEnableConfirmPass(!enableConfirmPass)} className="absolute inset-y-0 end-0 flex items-center pe-2.5 text-black hover:text-cyan-600" >
                {enableConfirmPass ? 
                (
                    <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" className="bi bi-eye-fill w-5 h-5" viewBox="0 0 16 16">
                        <path d="M10.5 8a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0"/>
                        <path d="M0 8s3-5.5 8-5.5S16 8 16 8s-3 5.5-8 5.5S0 8 0 8m8 3.5a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7"/>
                    </svg>
                )
                :(
                    <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" className="bi bi-eye-slash-fill w-5 h-5" viewBox="0 0 16 16">
                        <path d="m10.79 12.912-1.614-1.615a3.5 3.5 0 0 1-4.474-4.474l-2.06-2.06C.938 6.278 0 8 0 8s3 5.5 8 5.5a7 7 0 0 0 2.79-.588M5.21 3.088A7 7 0 0 1 8 2.5c5 0 8 5.5 8 5.5s-.939 1.721-2.641 3.238l-2.062-2.062a3.5 3.5 0 0 0-4.474-4.474z"/>
                        <path d="M5.525 7.646a2.5 2.5 0 0 0 2.829 2.829zm4.95.708-2.829-2.83a2.5 2.5 0 0 1 2.829 2.829zm3.171 6-12-12 .708-.708 12 12z"/>
                    </svg>
                )}
                </button>
            </div>
            
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
