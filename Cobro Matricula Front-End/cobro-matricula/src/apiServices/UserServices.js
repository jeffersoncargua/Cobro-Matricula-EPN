import { useFetch } from "../hooks/useFetch";

export async function LoginUser(loginRequest) {
	const verbose = "POST";
	const route = "api/User/Login";

	var response = await useFetch({
		verbose: verbose,
		route: route,
		objectRequest: loginRequest,
	});

	return response.json();
}

export async function RegisterUser(registerRequest) {
	const verbose = "POST";
	const route = "api/User/Registration";

	var response = await useFetch({
		verbose: verbose,
		route: route,
		objectRequest: registerRequest,
	});

	return response.json();
}

export async function ForgetPass(forgetRequest) {
	const verbose = "POST";
	const route = "api/User/ForgetPassword";

	var response = await useFetch({
		verbose: verbose,
		route: route,
		objectRequest: forgetRequest,
	});

	return response.json();
}

export async function ResetPass(resetRequest) {
	const verbose = "POST";
	const route = "api/User/ResetPassword";

	var response = await useFetch({
		verbose: verbose,
		route: route,
		objectRequest: resetRequest,
	});

	return response.json();
}

export async function ConfirmationUser(confirmRequest) {
	const verbose = "GET";
	const route = `api/User/ConfirmEmail`;
	const query = `?token=${confirmRequest.get("token")}&email=${confirmRequest.get("email")}`;

	var response = await useFetch({
		verbose: verbose,
		route: route,
		query: query,
	});

	return response.json();
}

export async function GetUsers(search = '') {
	const verbose = "GET";
	const route = `api/User/GetUsers`;
	const query = `?query=${search}`

	var response = await useFetch({ 
		verbose: verbose, 
		route: route, 
		query: query
	});

	return response.json();
}

export async function DeleteUser(email) {
	const verbose = "DELETE";
	const route = "api/User/DeleteUser";
	const query = `?email=${email}`;

	var response = await useFetch({
		verbose: verbose,
		route: route,
		query: query,
	});

	return response.json();
}

export async function UpdateUser(user) {
	const verbose = "PUT";
	const route = "api/User/UpdateUser";
	const query = `?email=${user.email}`;

	var response = await useFetch({
		verbose: verbose,
		route: route,
		objectRequest: user,
		query: query,
	});

	return response.json();
}
