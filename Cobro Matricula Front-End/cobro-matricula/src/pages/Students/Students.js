import { useState } from "react"
import { DrawCircleText, FormCalculator, InformationForStudents, Pay } from "./components";
import { CalculatorPay } from "../../apiServices/CalculatorServices";
import { SwalSuccess, SwalFailed } from "../../sweetAlerts/SweetAlerts";


export const Students = () => {

  const [showPay, setShowPay] = useState(true);
  const [payment, setPayment] = useState({});

  const HandleCalculator = async(calculatorRequest) => {
    
    var response = await CalculatorPay(calculatorRequest);

    if(response.isSuccess){
      //Se debe configurar el sweet alert de exito
      SwalSuccess("Exito",response.message[0]);

      //Se setea payment para obtener los valores calculados del pago
      setPayment(response.result);
      
      //Se setea el valor de showPay a true en caso de exito
      setShowPay(true);
    }else{
      //Se debe configurar el sweet alert de error
      SwalFailed("Error",response.message);

      //Se setea payment a un objeto vacio en caso de error
      setPayment(null);
      
      //Se setea el valor de showPay a false en caso de error
      setShowPay(false);
    }

    
  }

  return (
    <div className="container mx-auto">
        <div className="w-full min-h-screen flex flex-col gap-y-4 justify-center items-center">
          <DrawCircleText />
          <FormCalculator HandleCalculator={HandleCalculator}/>
          <InformationForStudents />
          {(showPay && payment !== null)  && <Pay payment={payment} />}
        </div> 
    </div>
  )
}
