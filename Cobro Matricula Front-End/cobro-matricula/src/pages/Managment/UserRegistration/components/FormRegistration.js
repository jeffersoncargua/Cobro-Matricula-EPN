import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { RegisterUser } from "../../../../apiServices/UserServices";
import { useForm } from 'react-hook-form';
import { SwalFailed, SwalSuccess } from "../../../../sweetAlerts/SweetAlerts";
import { ButtonLoading } from "../../../../components";
import { message,patterns } from "../../../../utility/ValidationUser";
import { ErrorMessageValidator } from "../../../../components";


export const FormRegistration = () => {

    //Esta funcion permitira realizar las validaciones de los campos
    //correspondientes y verificarlos cuando se realice el submit del formulario
    const {register, handleSubmit,formState:{errors}, watch} = useForm({
        defaultValues:{
            role : 'Assistant'
        }
    });
    

    //const [enableRegistrationButton, setEnableRegistrationButton] = useState(false);
    const [enablePass, setEnablePass] = useState(false);
    const [enableConfirmPass, setEnableConfirmPass] = useState(false);
    const [showButtonLoading,setShowButtonLoading] = useState(false);
    const password = watch('password');
    
    const navigate = useNavigate();


    const HandleSubmit = async(registrationUser) => {
        //e.preventDefault();

        setShowButtonLoading(true);

        var response = await RegisterUser(registrationUser);

            console.log(response);

            if (response.isSuccess) {
                const result = await SwalSuccess("Registro Exitoso!!",response.message);
                if(result.isConfirmed){
                    navigate('/');
                }
            }else{
                //const result = await SwalFailed('Oops...',response.message);
                SwalFailed('Oops...',response.message);
                console.log(response.message);
            }

        
        setShowButtonLoading(false);
    }


  return (
    <form className=" w-full md:w-1/2 text-slate-900 p-4 m-4 border border-slate-100 rounded-lg bg-gradient-to-r from-indigo-500/75 from-10% via-sky-500 via-30% to-emerald-500/75 to-90%" onSubmit={handleSubmit(HandleSubmit)}  >
        <div className="grid gap-6 mb-6 md:grid-cols-2  ">
            <div>
                <label htmlFor="name" className="block mb-2 text-sm font-medium ">Nombre</label>
                <input type="text" name="name" {...register("name",{required: message.req.name,pattern:{value:patterns.letters,message:message.name}})} className={` bg-gray-50 border  ${errors.name ? 'text-red-500  focus:outline-red-700' : 'text-slate-800 focus:outline-blue-500'} text-sm rounded-lg block w-full p-2.5 `} placeholder="Roberta"  />
                {errors.name && ( <ErrorMessageValidator message={errors.name.message} /> )}
            </div>
            <div>
                <label htmlFor="lastName" className="block mb-2 text-sm font-medium dark:text-white">Apellido</label>
                <input type="text" name="lastName" {...register("lastName",{required: message.req.lastName,pattern:{value:patterns.letters,message:message.lastName}})} className={` bg-gray-50 border  ${errors.lastName ? 'text-red-500  focus:outline-red-700' : 'text-slate-800 focus:outline-blue-500'} text-sm rounded-lg block w-full p-2.5 `} placeholder="Mendez"  />
                {errors.lastName && ( <ErrorMessageValidator message={errors.lastName.message} /> )}
            </div>
            <div>
                <label htmlFor="city" className="block mb-2 text-sm font-medium  dark:text-white">Ciudad</label>
                <input type="text" name="city" {...register("city",{required: message.req.city})} className={` bg-gray-50 border  ${errors.city ? 'text-red-500  focus:outline-red-700' : 'text-slate-800 focus:outline-blue-500'} text-sm rounded-lg block w-full p-2.5 `} placeholder='Guayaquil'/>
                {errors.city && ( <ErrorMessageValidator message={errors.city.message} /> )}
            </div>  
            <div>
                <label htmlFor="phone" className="block mb-2 text-sm font-medium  dark:text-white">Telefono: +593</label>
                <input type="tel" name="phone" {...register("phone",{required: message.req.phone, pattern:{value:patterns.numbers,message:message.phone}})} className={` bg-gray-50 border  ${errors.phone ? 'text-red-500  focus:outline-red-700' : 'text-slate-800 focus:outline-blue-500'} text-sm rounded-lg block w-full p-2.5 `} placeholder="0987654321"  />
                {errors.phone && ( <ErrorMessageValidator message={errors.phone.message} /> )}
            </div>
        </div>
        <div className="mb-6">
            <label htmlFor="email" className="block mb-2 text-sm font-medium  dark:text-white">Correo Electrónico</label>
            <input type="email" name="email" {...register("email",{required: message.req.email, pattern:{value:patterns.email,message:message.email}})} className={` bg-gray-50 border  ${errors.email ? 'text-red-500  focus:outline-red-700' : 'text-slate-800 focus:outline-blue-500'} text-sm rounded-lg block w-full p-2.5 `} placeholder="example@example.com"  />
            {errors.email && ( <ErrorMessageValidator message={errors.email.message} /> )}            
        </div> 
        <div className="mb-6">
            <label htmlFor="password" className="block mb-2 text-sm font-medium  dark:text-white">Contraseña</label>
            <div className="w-full relative">
                <input type={enablePass ? 'text':"password" } name="password" {...register("password",{required: message.req.password,pattern:{value:patterns.password,message:message.password}})}  className={` bg-gray-50 border  ${errors.password ? 'text-red-500  focus:outline-red-700' : 'text-slate-800 focus:outline-blue-500'} text-sm rounded-lg block w-full p-2.5 `} placeholder="•••••••••"   />
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
            {errors.password && ( <ErrorMessageValidator message={errors.password.message} /> )}
            
        </div> 
        <div className="mb-6 ">
            <label htmlFor="confirmPass" className="block mb-2 text-sm font-medium  dark:text-white">Confirmar Contraseña</label>
            <div className="w-full relative ">
                <input type={enableConfirmPass ? 'text':"password"} name="confirmPass" {...register("confirmPass",{required: message.req.confirmPass, validate: (value) => value === password || message.confirmPass })} className={` bg-gray-50 border  ${errors.confirmPass ? 'text-red-500  focus:outline-red-700' : 'text-slate-800 focus:outline-blue-500'} text-sm rounded-lg block w-full p-2.5 `} placeholder="•••••••••"  />
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
            {errors.confirmPass && ( <ErrorMessageValidator message={errors.confirmPass.message} /> )}
        </div> 
        {/* <div className="flex items-start mb-6">
            <div className="flex items-center h-5">
            <input id="remember" type="checkbox" value="" onChange={() => setEnableRegistrationButton(!enableRegistrationButton)} className="w-4 h-4 border border-gray-300 rounded-sm bg-gray-50 focus:ring-3 focus:ring-blue-300 " />
            </div>
            <label htmlFor="remember" className="ms-2 text-sm font-medium  dark:text-gray-300">Aceptas los <Link to='/' className="text-pink-600 hover:underline dark:text-blue-500">términos y condiciones</Link>.</label>
        </div> */}

            

            {!showButtonLoading ? 
            (
                <button type="submit"  className={`text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 cursor-pointer font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center  `}>Registrar</button>
            )
            :
            (
                <ButtonLoading/>
            )}
        
    </form>
  )
}
