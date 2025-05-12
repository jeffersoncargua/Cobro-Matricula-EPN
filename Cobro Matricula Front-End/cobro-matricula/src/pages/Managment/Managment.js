import { Outlet } from "react-router-dom"

export const Managment = () => {
  return (
    <div className="w-[95%] h-full flex items-center mx-auto">
        <Outlet />
    </div>
  )
}
