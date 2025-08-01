import {useFetch} from '../hooks/useFetch';

export async function CalculatorPay (calculatorRequest)  {
    const verbose = "POST";
    const route = "api/Calculator/CalculatorPay";

    var response = await useFetch({verbose:verbose, route:route, objectRequest:calculatorRequest});

    return response.json();
}

