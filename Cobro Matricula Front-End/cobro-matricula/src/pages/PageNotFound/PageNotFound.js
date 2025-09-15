import { useNavigate } from "react-router-dom"

export const PageNotFound = () => {

    const navigate = useNavigate();

  return (
    <div className="w-full min-h-screen flex flex-col justify-center items-center space-y-32">
        <h1 className="font-extrabold text-3xl md:text-5xl text-center">Page Not Found</h1>
        <button type="button" onClick={() => navigate("/")}  className="px-2.5 py-2 w-[80%] rounded-lg border border-gray-950 hover:bg-green-500 bg-green-700/30">Regresar al Inicio</button>
    </div>
  )
}
