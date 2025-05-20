import {useRef} from 'react';
import { UpdateUser } from '../../../../apiServices/UserServices';
import { SwalFailed, SwalUpdated } from '../../../../sweetAlerts/SweetAlerts';

export const ModalUpdatedUser = ({enableModal,setEnableModal,user}) => {

    // const [enablePass, setEnablePass] = useState(false);
    // const [enableConfirmPass, setEnableConfirmPass] = useState(false);

    const nameRef = useRef();
    const lastNameRef = useRef();
    const cityRef = useRef();
    const phoneRef = useRef()
    const emailRef = useRef();
    //const passRef = useRef();
    //const confirmPassRef = useRef();

    const handleSubmit = async(e) => {
        e.preventDefault();
        var userUpdated = {
            name: nameRef.current.value,
            lastName : lastNameRef.current.value,
            city: cityRef.current.value,
            phone: phoneRef.current.value,
            email: emailRef.current.value
        }

        var response = UpdateUser(userUpdated);

        if(response.isSuccess){
            const result = await SwalUpdated("Exito!!",'Tu información ha sido actualizada correctamente',"https://i.gifer.com/SWYA.gif");
            if (result.isConfirmed) {
                setEnableModal(false);
            }
            
        }else{
            const result = await SwalFailed('Oops',["No se pudo actualizar la información del usuario"],'Por favor, inténtalo más tarde');
            if(result.isConfirmed){
                setEnableModal(false);
            }
        }

    }

  return (
    <div className=''>
        {/* <!-- Main modal --> */}
        <div id="authentication-modal" tabindex="-1" aria-hidden="true" className={`${!enableModal && 'hidden'} overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 flex justify-center items-center w-full md:inset-0 h-[calc(100%)] max-h-full bg-gray-50/50`}>
            <div className="relative p-4 w-full max-w-lg max-h-full">
                {/* <!-- Modal content --> */}
                <div className="relative bg-white rounded-lg shadow-sm dark:bg-gray-700">
                    {/* <!-- Modal header --> */}
                    <div className="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600 border-gray-200">
                        <h3 className="text-xl font-semibold text-gray-900 dark:text-white">
                            Actualizar Usuario
                        </h3>
                        <button type="button" onClick={() => setEnableModal(false)} className="end-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white" data-modal-hide="authentication-modal">
                            <svg className="w-3 h-3" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 14 14">
                                <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"/>
                            </svg>
                            <span className="sr-only">Close modal</span>
                        </button>
                    </div>
                    {/* <!-- Modal body --> */}
                    <div className="p-4 md:p-5">
                        <form className="group text-slate-900 p-4 border border-slate-100 rounded-lg " onSubmit={handleSubmit}>
                            <div className="grid gap-6 mb-6 md:grid-cols-2  ">
                                <div>
                                    <label htmlFor="first_name" className="block mb-2 text-sm font-medium  dark:text-white">Nombre</label>
                                    <input type="text" id="first_name" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Antonio" defaultValue={user.name} required ref={nameRef} />
                                </div>
                                <div>
                                    <label htmlFor="last_name" className="block mb-2 text-sm font-medium  dark:text-white">Apellido</label>
                                    <input type="text" id="last_name" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Sanchez" defaultValue={user.lastName} required ref={lastNameRef} />
                                </div>
                                <div>
                                    <label htmlFor="city" className="block mb-2 text-sm font-medium  dark:text-white">Ciudad</label>
                                    <input type="text" id="city" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Quito" required defaultValue={user.city} ref={cityRef} />
                                </div>  
                                <div>
                                    <label htmlFor="phone" className="block mb-2 text-sm font-medium  dark:text-white">Telefono: +593</label>
                                    <input type="tel" id="phone" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="0987654321" pattern="[0-9]{10}" defaultValue={user.phone} required ref={phoneRef} />
                                </div>
                            </div>
                            <div className="mb-6">
                                <label htmlFor="email" className="block mb-2 text-sm font-medium  dark:text-white">Correo Electrónico</label>
                                <input disabled type="email" id="email" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="example@example.com" value={user.email} required ref={emailRef} />
                            </div> 
                            {/* <div className="mb-6">
                                <label htmlFor="password" className="block mb-2 text-sm font-medium  dark:text-white">Contraseña</label>
                                <div className='w-full relative'>
                                    <input type={enablePass ? 'text':"password"} id="password" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="•••••••••" required ref={passRef} />
                                    <button type='button' onClick={() => setEnablePass(!enablePass)} className='absolute inset-y-0 end-0 pe-2.5 flex items-center text-black hover:text-cyan-600' >
                                        {!enablePass ? 
                                        (
                                            <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-eye-slash-fill w-5 h-5" viewBox="0 0 16 16">
                                                <path d="m10.79 12.912-1.614-1.615a3.5 3.5 0 0 1-4.474-4.474l-2.06-2.06C.938 6.278 0 8 0 8s3 5.5 8 5.5a7 7 0 0 0 2.79-.588M5.21 3.088A7 7 0 0 1 8 2.5c5 0 8 5.5 8 5.5s-.939 1.721-2.641 3.238l-2.062-2.062a3.5 3.5 0 0 0-4.474-4.474z"/>
                                                <path d="M5.525 7.646a2.5 2.5 0 0 0 2.829 2.829zm4.95.708-2.829-2.83a2.5 2.5 0 0 1 2.829 2.829zm3.171 6-12-12 .708-.708 12 12z"/>
                                            </svg>
                                        )
                                        :
                                        (
                                            <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-eye-fill w-5 h-5" viewBox="0 0 16 16">
                                                <path d="M10.5 8a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0"/>
                                                <path d="M0 8s3-5.5 8-5.5S16 8 16 8s-3 5.5-8 5.5S0 8 0 8m8 3.5a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7"/>
                                            </svg>
                                        )}
                                    </button>
                                </div>
                            </div> 
                            <div className="mb-6">
                                <label htmlFor="confirm_password" className="block mb-2 text-sm font-medium  dark:text-white">Confirmar Contraseña</label>
                                <div className='w-full relative'>
                                    <input type={enableConfirmPass ? 'text':'password'} id="confirm_password" className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="•••••••••" required ref={confirmPassRef} />
                                    <button type='button' onClick={() => setEnableConfirmPass(!enableConfirmPass)} className='absolute inset-y-0 end-0 pe-2.5 text-black hover:text-cyan-600' >
                                        {!enableConfirmPass ? 
                                        (
                                            <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-eye-slash-fill w-5 h-5" viewBox="0 0 16 16">
                                                <path d="m10.79 12.912-1.614-1.615a3.5 3.5 0 0 1-4.474-4.474l-2.06-2.06C.938 6.278 0 8 0 8s3 5.5 8 5.5a7 7 0 0 0 2.79-.588M5.21 3.088A7 7 0 0 1 8 2.5c5 0 8 5.5 8 5.5s-.939 1.721-2.641 3.238l-2.062-2.062a3.5 3.5 0 0 0-4.474-4.474z"/>
                                                <path d="M5.525 7.646a2.5 2.5 0 0 0 2.829 2.829zm4.95.708-2.829-2.83a2.5 2.5 0 0 1 2.829 2.829zm3.171 6-12-12 .708-.708 12 12z"/>
                                            </svg>
                                        )
                                        :
                                        (
                                            <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-eye-fill w-5 h-5" viewBox="0 0 16 16">
                                                <path d="M10.5 8a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0"/>
                                                <path d="M0 8s3-5.5 8-5.5S16 8 16 8s-3 5.5-8 5.5S0 8 0 8m8 3.5a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7"/>
                                            </svg>
                                        )}
                                    </button>
                                </div>
                                
                            </div>*/}
                            <button type="submit" className={`text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 cursor-pointer font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800`}>Editar</button>
                        </form>
                    </div>
                </div>
            </div>
        </div> 
    </div>
  )
}
