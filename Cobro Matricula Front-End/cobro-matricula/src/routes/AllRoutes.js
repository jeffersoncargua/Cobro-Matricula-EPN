import { Routes, Route } from "react-router-dom";
import { Home,Managment,BaseParameters } from "../pages";
import { UserRegistration,UserManagment, UserRecover,UserConfirmation } from "../pages/Managment";

export const AllRoutes = () => {
  return (
    <Routes>
        <Route path="/" element={<Home/>} />
        <Route path="manage" element={<Managment/>} >
          <Route path="registration" element={<UserRegistration/>} />
          <Route path="users" element={<UserManagment/>} />
          <Route path="reset" element={<UserRecover/>} />
          <Route path="confirmation" element={<UserConfirmation/>} />
        </Route>
        <Route path="parameters/:type" element={<BaseParameters/>} />
    </Routes>
  )
}
