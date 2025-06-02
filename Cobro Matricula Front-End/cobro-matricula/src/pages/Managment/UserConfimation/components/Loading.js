import {  useEffect, useState } from "react";

export const Loading = () => {

    const [count, setCount] = useState(1); 

    useEffect(() => {
        const interval = setInterval(() => {
            var arrayPings = document.getElementById('spans');
            if (count === 3) {
                arrayPings.children[count-1].classList.remove('bg-white');
                arrayPings.children[count-1].classList.add('bg-black');
                arrayPings.children[0].classList.add('bg-white');  
                arrayPings.children[0].classList.remove('bg-black');
                setCount(1);
            }else{
                arrayPings.children[count-1].classList.add('bg-black');
                arrayPings.children[count-1].classList.remove('bg-white');
                arrayPings.children[count].classList.add('bg-white');  
                arrayPings.children[count].classList.remove('bg-black');
                setCount(count => count+1);
            }

        },500);
        
        return () => clearInterval(interval);

    },[count])
    

  return (
    <div className="flex flex-wrap gap-y-2 text-3xl text-black animate-[pulse_1.5s_cubic-bezier(0.4,0,0.6,0.5)_infinite]">
        <p className=" me-3 sombreado">Estamos verificando su cuenta. Por favor espere </p>
        <div id="spans" className="flex items-center mt-2 ">
            <span className="shadow-[1px_2px_5px_rgba(33,97,140,0.75)] flex w-1 h-1 me-3 bg-white rounded-full"></span>
            <span className="shadow-[1px_2px_5px_rgba(33,97,140,0.75)] flex w-1 h-1 me-3 bg-black rounded-full"></span>
            <span className="shadow-[1px_2px_5px_rgba(33,97,140,0.75)] flex w-1 h-1 me-3 bg-black rounded-full"></span>
        </div>  
    </div>
  )
}
