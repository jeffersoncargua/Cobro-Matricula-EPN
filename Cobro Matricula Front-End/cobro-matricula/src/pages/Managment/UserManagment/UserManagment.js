import { useEffect, useState} from "react";
import { UserTable,ModalUpdatedUser } from "./components";
import { DeleteUser, GetUsers } from "../../../apiServices/UserServices";
import { SwalCancel, SwalConfirmed, SwalDeleted, SwalFailed } from "../../../sweetAlerts/SweetAlerts";


export const UserManagment = () => {

  const [enableModal, setEnableModal] = useState(false);
  const [user, setUser] = useState({});
  const [users, setUsers] = useState([]);


  const handleUpdatedUser = (user) => {
    setUser(user);
    setEnableModal(true);
  }

  useEffect(()=>{

    const fetchData = async() => {
      var response = await GetUsers();
      if(response.isSuccess){
        setUsers(response.result);
      }else{
        setUsers([]);
      }
    }

    fetchData();
    
  },[setUsers, setUser])
  
  const handleDeletedUser = async(email) => {
    let responseSwal = await SwalDeleted();
    if(responseSwal.isConfirmed){
      var apiResponse = DeleteUser(email);
      if(apiResponse.isSuccess){
        SwalConfirmed();
      }else{
        SwalFailed('Oopss',apiResponse.message,'Solicita ayuda con el administrador');
      }
    }else{
      SwalCancel();
    }

  }


  return (
    <div className="flex items-center justify-center w-[95%] min-h-screen mx-auto ">
      <UserTable handleUpdatedUser={handleUpdatedUser} handleDeletedUser={handleDeletedUser} users={users} />
      {enableModal && (
        <ModalUpdatedUser enableModal={enableModal} setEnableModal={setEnableModal} user={user} />
      )}
    </div>
  )
}
