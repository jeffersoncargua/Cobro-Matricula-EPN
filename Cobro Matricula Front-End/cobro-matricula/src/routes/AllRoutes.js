import { Route, Routes } from "react-router-dom";
import { BaseParameters, Home, Managment, Students } from "../pages";
import {
	UserConfirmation,
	UserManagment,
	UserRecover,
	UserRegistration,
} from "../pages/Managment";

export const AllRoutes = () => {
	return (
		<Routes>
			<Route path="/" element={<Home />} />
			<Route path="manage" element={<Managment />}>
				<Route path="registration" element={<UserRegistration />} />
				<Route path="users" element={<UserManagment />} />
				<Route path="reset" element={<UserRecover />} />
				<Route path="confirmation" element={<UserConfirmation />} />
			</Route>
			<Route path="parameters" element={<BaseParameters />} />
			<Route path="calculator" element={<Students />} />
		</Routes>
	);
};
