import {useFetch} from '../hooks/useFetch';

const  CalculatorPay = async(calculatorRequest) => {
    const verbose = "POST";
    const route = "api/Calculator/CalculatorPay";

    var response = await useFetch({verbose:verbose, route:route,objectRequest:calculatorRequest});

    console.log(response.json());

    return response.json();
}

export {CalculatorPay};