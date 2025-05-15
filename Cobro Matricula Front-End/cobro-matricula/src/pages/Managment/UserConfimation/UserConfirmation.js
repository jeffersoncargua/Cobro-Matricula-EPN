import { useCallback, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
//import Swal from "sweetalert2";  //Descomentar cuando se vaya a configurar el enlace con las API de autenticacion

import './style/UserConfirmation.css';


export const UserConfirmation = () => {

    const [showInfo, setShowInfo] = useState(false);
    // const array = new [1,2,3];
    const [count, setCount] = useState(1);
    //const navigate = useNavigate(); //Descomentar cuando se vaya a configurar el enlace con las API de autenticacion

    const handleAnimation = useCallback(() => {
        var arrayPings = document.getElementById('spans');
        setTimeout(()=>{
        if (count===3) {
            arrayPings.children[count-1].classList.remove('bg-white');
            arrayPings.children[count-1].classList.add('bg-black');
            arrayPings.children[0].classList.add('bg-white');  
            arrayPings.children[0].classList.remove('bg-black');
            setCount(1);
        }else{
            arrayPings.children[count-1].classList.remove('bg-white');
            arrayPings.children[count-1].classList.add('bg-black');
            arrayPings.children[count].classList.add('bg-white');  
            arrayPings.children[count].classList.remove('bg-black');
            setCount(count => count+1);
        }

        //console.log(count);
        },[200])

        
    },[count]);


    useEffect(()=>{
        //     setShowInfo(!showInfo);

        //Descomentar cuando se vaya a configurar el enlace con las API de autenticacion
        //En caso de ser afirmativo 
        
        // Swal.fire({
        // title: "Cuenta Verificada",
        // icon: "success",
        // draggable: true,
        // customClass:"text-sm",
        // });

        ////En caso de error este SWAL
        // Swal.fire({
        // icon: "error",
        // title: "Oops...",
        // text: "El enlace de tu cuenta de verificacion ha caducado",
        // footer: "Solicita ayuda del administrador o crea una nueva cuenta",
        // customClass:"text-sm",
        // }).then(result => {
        //     if(result.isConfirmed){
        //         navigate('/');
        //     }
        // });      
        handleAnimation();

    },[handleAnimation])

  return (
    <div className="w-full min-h-screen flex items-center justify-center">
        {!showInfo &&
        (
            <div className="flex flex-wrap gap-y-2 text-3xl text-black animate-[pulse_1.5s_cubic-bezier(0.4,0,0.6,0.5)_infinite]">
                <p className=" me-3 sombreado">Estamos verificando su cuenta. Por favor espere </p>
                <div id="spans" className="flex items-center mt-2 ">
                    <span id="1" class="shadow-[1px_2px_5px_rgba(33,97,140,0.75)] flex w-1 h-1 me-3 bg-white rounded-full"></span>
                    <span id="2" class="shadow-[1px_2px_5px_rgba(33,97,140,0.75)] flex w-1 h-1 me-3 bg-black rounded-full"></span>
                    <span id="3" class="shadow-[1px_2px_5px_rgba(33,97,140,0.75)] flex w-1 h-1 me-3 bg-black rounded-full"></span>
                </div>  
            </div>
        )}
    </div>
  )
}
