import { Routes, Route } from "react-router-dom";
import { Home,Managment } from "../pages";
import { UserRegistration,UserManagment, UserRecover } from "../pages/Managment";

export const AllRoutes = () => {
  return (
    <Routes>
        <Route path="/" element={<Home/>} />
        <Route path="manage" element={<Managment/>} >
          <Route path="registration" element={<UserRegistration/>} />
          <Route path="users" element={<UserManagment/>} />
           <Route path="forget" element={<UserRecover/>} />
        </Route>
    </Routes>
  )
}
