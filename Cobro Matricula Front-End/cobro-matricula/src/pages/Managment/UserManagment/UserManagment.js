import { useState} from "react";
import { UserTable,ModalUpdatedUser } from "./components";
import Swal from "sweetalert2";

export const UserManagment = () => {

  const [enableModal, setEnableModal] = useState(false);


  const handleUpdatedUser = () => {
    setEnableModal(true);
  }
  
  const handleDeletedUser = () => {
    Swal.fire({
    title: "Estas Seguro?",
    text: "Se eliminará el registro de la base de datos!",
    icon: "warning",
    showCancelButton: true,
    cancelButtonText : "Cancelar",
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Si, borralo"
    }).then((result) => {
      if (result.isConfirmed) {
        Swal.fire({
          title: "Eliminado!",
          text: "Se eliminó el registro con éxito.",
          icon: "success",
          confirmButtonText: "Listo"
        });
      }
    });
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
