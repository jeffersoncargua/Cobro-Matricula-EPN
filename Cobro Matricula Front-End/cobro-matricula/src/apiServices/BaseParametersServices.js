import { useFetch } from "../hooks/useFetch";
//import { useSelector } from "react-redux";
//import { getToken } from "../redux/userSlice";
import { GetToken } from "../security/TokenSecurity";
//import { useEffect } from "react";
//import { useDispatch } from "react-redux";





export async function GetBaseParameters(id) {
    // const dispath = useDispatch();
    //var token = dispath(getToken());
    var token = GetToken();

    const verbose = 'GET';
    const route = `api/BaseParameter/GetParameters/${id}`;
    var response = await useFetch({verbose:verbose,route:route, authToken: token});
    
    console.log(response.status);

    if (response.status === 401) {
        return { isSuccess: false, message: ['Acceso Denagado. El usuario tendrá problemas'], result: null, statusCode: 401 };
    }

    return response.json();    
}

// export async function UpdateParameters(id, parameters) {
//     const verbose = 'PUT';
//     const route = `api/BaseParameters/UpdateParameters/${id}`;
//     var response = await useFetch({verbose:verbose,route:route,objectRequest:parameters, authToken: token});

//     return response;
// }