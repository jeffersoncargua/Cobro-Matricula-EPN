import { Routes, Route } from "react-router-dom";
import { Home,Managment } from "../pages";
import { UserManagment,UserRegistration } from "../pages/Managment";

export const AllRoutes = () => {
  return (
    <Routes>
        <Route path="/" element={<Home/>} />
        <Route path="manage" element={<Managment/>} >
          <Route path="registration" element={<UserRegistration/>} />
          <Route path="users" element={<UserManagment/>} />
        </Route>
    </Routes>
  )
}
