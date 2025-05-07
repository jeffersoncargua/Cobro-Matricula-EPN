import { useState, useEffect} from "react";
import { UserTable,ModalUpdatedUser } from "./components";

export const UserManagment = () => {

  const [enableModal, setEnableModal] = useState(false);


  const handleUpdatedUser = () => {
    setEnableModal(true);
  }
  
  const handleDeletedUser = () => {
    
  }


  return (
    <div className="w-[95%] mx-auto ">
      <UserTable handleUpdatedUser={handleUpdatedUser} handleDeletedUser={handleDeletedUser} />
      {enableModal && (
        <ModalUpdatedUser enableModal={enableModal} setEnableModal={setEnableModal} />
      )}
    </div>
  )
}
