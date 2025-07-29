import { useState } from "react"
import { DrawCircleText, FormCalculator, InformationForStudents, Pay } from "./components";
import { CalculatorPay } from "../../apiServices/CalculatorServices";
import { SwalSuccess, SwalFailed } from "../../sweetAlerts/SweetAlerts";
import { LoadingSquid } from "../../components";


export const Students = () => {

  const [showPay, setShowPay] = useState(false);
  const [payment, setPayment] = useState({});
  const [loading, setLoading] = useState(false);

  const HandleCalculator = async(calculatorRequest) => {
    //Se setea el valor de gratuidad a un valor booleano, es necesario para realizar
    //la solicitud de forma correcta
    calculatorRequest.gratuidad = calculatorRequest.gratuidad === 'true' ? true: false;
    
    //Se habilita el loading mientras se realiza la transaccion para obtener los valore a pagar 
    setLoading(true);

    //Se realiza la peticion en el api CalculatorPay
    var response = await CalculatorPay(calculatorRequest);

    if(response.isSuccess){
      //Se debe configurar el sweet alert de exito
      SwalSuccess("Exito",response.message);

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

    setLoading(false);
  }

  return (
    <div className="container mx-auto">
        <div className="w-full min-h-screen flex flex-col gap-y-4 justify-center items-center">
          <DrawCircleText />
          <FormCalculator HandleCalculator={HandleCalculator}  />
          <InformationForStudents />
          {(showPay && payment !== null)  && <Pay payment={payment} />}
          {loading && <LoadingSquid />}
        </div> 
    </div>
  )
}
