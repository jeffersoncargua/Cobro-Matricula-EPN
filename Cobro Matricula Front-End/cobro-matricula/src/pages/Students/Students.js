import { useCallback, useEffect, useState } from "react";
import { CalculatorPay } from "../../apiServices/CalculatorServices";
import { LoadingSquid } from "../../components";
import { SwalFailed, SwalSuccess } from "../../sweetAlerts/SweetAlerts";
import {
	DrawCircleText,
	FormCalculator,
	InformationForStudents,
	Pay,
} from "./components";

export const Students = () => {
	const [showPay, setShowPay] = useState(false);
	const [payment, setPayment] = useState({});
	const [loading, setLoading] = useState(false);
	const [elementFocus, setElementFocus] = useState(null);

	const HandleCalculator = useCallback(
		async (calculatorRequest) => {
			//la solicitud de forma correcta
			//calculatorRequest.gratuidad = calculatorRequest.gratuidad === "true" ? true : false;
			calculatorRequest.gratuidad = calculatorRequest.gratuidad === "true";

			//Se habilita el loading mientras se realiza la transaccion para obtener los valore a pagar
			setLoading(true);

			//Se setea en null el elemento a enfocar
			setElementFocus(null);

			//Se realiza la peticion en el api CalculatorPay
			var response = await CalculatorPay(calculatorRequest);

			if (response.isSuccess) {
				//Se debe configurar el sweet alert de exito
				const result = await SwalSuccess("Exito", response.message);
				//Se obtiene del DOM el elemento payment-section
				const paymentSection = document.getElementById("payment-section");
				//Se setea el valor de showPay a true en caso de exito
				setShowPay(true);
				//Se setea payment para obtener los valores calculados del pago
				setPayment(response.result);
				if (result.isConfirmed) {
					setElementFocus(paymentSection);
				}
			} else {
				//Se debe configurar el sweet alert de error
				SwalFailed("Error", response.message);

				//Se setea payment a un objeto vacio en caso de error
				setPayment(null);

				//Se setea el valor de showPay a false en caso de error
				setShowPay(false);
			}

			setLoading(false);
		},
		[setElementFocus],
	);

	// const HandleCalculator = async(calculatorRequest) => {
	// 	//la solicitud de forma correcta
	// 	//calculatorRequest.gratuidad = calculatorRequest.gratuidad === "true" ? true : false;
	// 	calculatorRequest.gratuidad = calculatorRequest.gratuidad === "true";

	// 	//Se habilita el loading mientras se realiza la transaccion para obtener los valore a pagar
	// 	setLoading(true);

	// 	//Se setea en null el elemento a enfocar
	// 	setElementFocus(null);

	// 	//Se realiza la peticion en el api CalculatorPay
	// 	var response = await CalculatorPay(calculatorRequest);

	// 	if (response.isSuccess) {
	// 		//Se debe configurar el sweet alert de exito
	// 		const result = await SwalSuccess("Exito", response.message);
	// 		//Se obtiene del DOM el elemento payment-section
	// 		const paymentSection = document.getElementById("payment-section");
	// 		//Se setea el valor de showPay a true en caso de exito
	// 		setShowPay(true);
	// 		//Se setea payment para obtener los valores calculados del pago
	// 		setPayment(response.result);
	// 		if (result.isConfirmed) {
	// 			setElementFocus(paymentSection);
	// 		}
	// 	} else {
	// 		//Se debe configurar el sweet alert de error
	// 		SwalFailed("Error", response.message);

	// 		//Se setea payment a un objeto vacio en caso de error
	// 		setPayment(null);

	// 		//Se setea el valor de showPay a false en caso de error
	// 		setShowPay(false);
	// 	}

	// 	setLoading(false);
	// };

	useEffect(() => {
		//Se emplea el setTimeout para darle un poco de tiempo para la transicion del foco para
		//mostrar los calculos correspondintes en el componente Pay
		setTimeout(() => {
			if (elementFocus !== null) {
				//elementFocus.scrollIntoView({top: elementFocus.getBoundingClientRect().top, left: elementFocus.getBoundingClientRect().left , behavior: 'smooth'});
				elementFocus.scrollIntoView({ behavior: "smooth" });
			}
		}, 1000);
	}, [elementFocus]);

	return (
		<div className="container mx-auto">
			<div className="w-full min-h-screen flex flex-col gap-y-4 justify-center items-center snap-y snap-mandatory">
				<DrawCircleText />
				<FormCalculator
					HandleCalculator={HandleCalculator}
					setShowPay={setShowPay}
					setElementFocus={setElementFocus}
				/>
				<InformationForStudents />
				<Pay payment={payment} showPay={showPay} />
				{loading && <LoadingSquid />}
			</div>
		</div>
	);
};
