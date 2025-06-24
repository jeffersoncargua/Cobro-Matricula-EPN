import { useParams } from "react-router-dom"
import { TableParameters, TextPressure } from "./components"



export const BaseParameters = () => {

  const params = useParams();


  return (
    <div className="container mx-auto">
        <div className='w-full min-h-screen flex items-center justify-center' >
            <div className=" w-[90%] sm:w-[50%] h-full flex flex-col gap-y-3 justify-center items-center m-10">
                {/* <h1 className="my-1.5 text-center font-bold text-white text-5xl">Parámetros Base Ingeniería</h1> */}
                <TextPressure 
                    text={params.type}
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

                <TableParameters />
                
                
            </div>
        </div>
    </div>
  )
}
