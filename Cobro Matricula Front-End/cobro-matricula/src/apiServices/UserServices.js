import { useFetch } from "../hooks/useFetch";

export async function LoginUser (loginRequest) {

    const verbose = 'POST';
    const route = 'User/Login';
    
    var response = await useFetch({verbose:verbose,route:route,objectRequest:loginRequest});

    return response;
}

export async function RegisterUser(registerRequest) {
    const verbose = 'POST';
    const route = 'User/Registration';

    var response = await useFetch({verbose:verbose,route:route,objectRequest:registerRequest});

    return response;
}

export async function ForgetPass(forgetRequest) {
    const verbose = 'POST';
    const route = 'User/ForgetPassword';

    var response = await useFetch({verbose:verbose,route:route,objectRequest:forgetRequest});

    return response;
}

export async function ResetPass(resetRequest) {
    const verbose = 'POST';
    const route = 'User/ResetPassword';

    var response = await useFetch({verbose:verbose,route:route,objectRequest:resetRequest});

    return response;
}

export async function ConfirmationUser(confirmRequest) {
    const verbose = 'GET';
    const route = `User/ConfirmEmail`;
    const query = `?token=${confirmRequest.token}&email=${confirmRequest.email}`;

    var response = await useFetch({verbose:verbose,route:route,query:query});

    return response;
}

export async function GetUsers() {
    const verbose = 'GET';
    const route = 'User/GetUsers';
    
    var response = await useFetch({verbose:verbose,route:route});
    
    return response;
}

export async function DeleteUser(email){
    const verbose = 'DELETE';    
    const route = "User/DeleteUser";
    const query = `?email=${email}`;

    var response = await useFetch({verbose:verbose,route:route,query:query});

    return response;
}

export async function UpdateUser(user) {
    const verbose = 'PUT';
    const route = 'User/UpdateUser'
    const query = `?email=${user.email}`;

    var response = await useFetch({verbose:verbose,route:route,objectRequest:user,query:query});

    return response;
}




