import { useFetch } from "../hooks/useFetch";
//import { useSelector } from "react-redux";
import { getToken } from "../redux/userSlice";
import { useEffect } from "react";
import { useDispatch } from "react-redux";

//const user = useSelector(state => state.userState);
const dispath = useDispatch();
var token;

useEffect (() => {
    token = dispath(getToken());
}, [dispath]);

export async function GetBaseParameters(id) {
    
    const verbose = 'GET';
    const route = `api/BaseParameters/GetBaseParameters/${id}`;
    var response = await useFetch({verbose:verbose,route:route, authToken: token});

    return response;
}

export async function UpdateParameters(id, parameters) {
    const verbose = 'PUT';
    const route = `api/BaseParameters/UpdateParameters/${id}`;
    var response = await useFetch({verbose:verbose,route:route,objectRequest:parameters, authToken: token});

    return response;
}