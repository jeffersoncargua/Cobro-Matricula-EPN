import { useEffect, useState, useCallback, useRef } from "react";
import { UserTable, ModalUpdatedUser, SearchInput } from "./components";
import { DeleteUser, GetUsers } from "../../../apiServices/UserServices";
import {
	SwalCancel,
	SwalConfirmed,
	SwalDeleted,
	SwalFailed,
} from "../../../sweetAlerts/SweetAlerts";
import { LoadingSquid } from "../../../components";

export const UserManagment = () => {
	const [enableModal, setEnableModal] = useState(false);
	const [user, setUser] = useState({});
	const [users, setUsers] = useState([]);
	const [loading, setLoading] = useState(false);
	const searchRef = useRef('');

	const handleUpdatedUser = (user) => {
		setUser(user);
		setEnableModal(true);
	};
	
	const fetchData = useCallback(async() => {
		var response = await GetUsers(searchRef.current.value);
		if (response.isSuccess) {
			setUsers(response.result);
		} else {
			setUsers([]);
		}
	}, [setUsers])

	useEffect(() => {
		// const fetchData = async () => {
		// 	var response = await GetUsers();
		// 	if (response.isSuccess) {
		// 		setUsers(response.result);
		// 	} else {
		// 		setUsers([]);
		// 	}
		// };

		fetchData();

	}, [fetchData]);

	const handleDeletedUser = async (email) => {
		const responseSwal = await SwalDeleted();
		if (responseSwal.isConfirmed) {
			setLoading(true);
			const apiResponse = await DeleteUser(email);
			if (apiResponse.isSuccess) {
				SwalConfirmed();
			} else {
				SwalFailed(
					"Oopss",
					apiResponse.message,
					"Solicita ayuda con el administrador",
				);
			}
		} else {
			SwalCancel();
		}
		setLoading(false);
		fetchData();
	};

	return (
		<section className="w-[95%] min-h-screen mx-auto">
			<SearchInput searchRef={searchRef} fetchData={fetchData}  />
			<div className="flex flex-col justify-center">
				<UserTable
					handleUpdatedUser={handleUpdatedUser}
					handleDeletedUser={handleDeletedUser}
					users={users}
				/>
			</div>
			{enableModal && (
				<ModalUpdatedUser
					enableModal={enableModal}
					setEnableModal={setEnableModal}
					user={user}
					fetchData={fetchData}
				/>
			)}

			{loading && <LoadingSquid />}
		</section>
	);
};
