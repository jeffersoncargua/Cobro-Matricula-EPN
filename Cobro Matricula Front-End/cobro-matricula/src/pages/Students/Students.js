import { DrawCircleText, FormCalculator, InformationForStudents } from "./components"

export const Students = () => {
  return (
    <div className="container mx-auto">
        <div className="w-full min-h-screen flex flex-col gap-y-4 justify-center items-center">
          <DrawCircleText />
          <FormCalculator />
          <InformationForStudents />
        </div> 
    </div>
  )
}
