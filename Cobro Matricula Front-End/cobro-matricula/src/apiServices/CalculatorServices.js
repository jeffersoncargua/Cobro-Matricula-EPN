import { useFetch } from "../hooks/useFetch";

export async function CalculatorPay(calculatorRequest) {
	const verbose = "POST";
	const route = "api/Calculator/CalculatorPay";

	var response = await useFetch({
		verbose: verbose,
		route: route,
		objectRequest: calculatorRequest,
	});

	if (response.statusCode === 500) {
		return response;
	}

	return response.json();
}
