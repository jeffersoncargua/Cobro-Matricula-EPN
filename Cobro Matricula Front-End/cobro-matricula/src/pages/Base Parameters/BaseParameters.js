import { useParams } from "react-router-dom"
import { TableParameters, TextPressure , ButtonEdit} from "./components"
import { useCallback,useRef,useState } from "react";
import { GetBaseParameters } from "../../apiServices/BaseParametersServices";
import { SwalFailed } from "../../sweetAlerts/SweetAlerts";
import { LoadingSquid } from '../../components';



export const BaseParameters = () => {

  const params = useParams();
  const [title, setTitle] = useState(params.title || 'Parámetros Base');
  const [loading, setLoading] = useState(false);
  const titleRef = useRef('');
  const [baseParameters, setBaseParameters] = useState(null);

  const handleSetTitle = useCallback(async() => { 

    if (titleRef.current.value === '1') {
      setTitle('Ingeniería');
      
    }else if (titleRef.current.value === '2') {
      setTitle('Tecnología');
    }else {
      setTitle('Parámetros Base');
    }

    setLoading(true);
    var response = await GetBaseParameters(+titleRef.current.value);

    console.log(response);
    
    if (response.statusCode === 401) {
        SwalFailed("Aviso",response.message,"No lo vuelva a hacer!!!");
    }else if(response.statusCode === 404){
        SwalFailed("Error",response.message,"Por favor intente más tarde");
    }
    if (response.isSuccess) {
        setBaseParameters(response.result);
    }else{
        setBaseParameters(null);
    }
    
    setLoading(false);

   },[setBaseParameters])

   




  return (
    <div className="container mx-auto">
        <div className='w-full min-h-screen flex items-center justify-center' >
            <div className=" w-[90%] sm:w-[50%] h-full flex flex-col gap-y-3 justify-center items-center m-10">
                {/* <h1 className="my-1.5 text-center font-bold text-white text-5xl">Parámetros Base Ingeniería</h1> */}
                <TextPressure 
                    text={title}
                    flex={true}
                    alpha={false}
                    stroke={false}
                    width={true}
                    weight={true}
                    italic={true}
                    textColor="#ffffff"
                    strokeColor="#ff0000"
                    minFontSize={26}
                />

                <div className="w-full flex items-center justify-between gap-x-3 p-8 bg-white/90 rounded-lg">
                    <select name="" id="" className="p-2.5 rounded-lg text-sm text-center bg-transparent/10" onChange={() => handleSetTitle()} ref={titleRef} >
                      <option value="" className="italic bg-transparent/40">--- Seleccione la formación académica ---</option>
                      <option value="1"> Ingeniería </option>
                      <option value="2"> Tecnología </option>
                    </select>

                    <ButtonEdit />

                </div>

                {loading && (<LoadingSquid />)}

                {baseParameters !== null && (<TableParameters baseParameters={baseParameters} />)}  
                
            </div>
        </div>
    </div>
  )
}
