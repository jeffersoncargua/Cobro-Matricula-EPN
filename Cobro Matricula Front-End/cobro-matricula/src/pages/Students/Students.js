import { useState } from "react"
import { DrawCircleText, FormCalculator, InformationForStudents, Pay } from "./components"

export const Students = () => {

  const [showPay, setShowPay] = useState(true);

  const HandleCalculator = (calculatorRequest) => {
    console.log(calculatorRequest);
    setShowPay(true);
  }

  return (
    <div className="container mx-auto">
        <div className="w-full min-h-screen flex flex-col gap-y-4 justify-center items-center">
          <DrawCircleText />
          <FormCalculator HandleCalculator={HandleCalculator}/>
          <InformationForStudents />
          {showPay && <Pay />}
        </div> 
    </div>
  )
}
