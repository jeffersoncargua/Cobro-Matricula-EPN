import { useEffect, useState, useCallback } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { Loading } from "./components/Loading";
import "./style/UserConfirmation.css";
import { ConfirmationUser } from "../../../apiServices/UserServices";
import { SwalFailed, SwalSuccess } from "../../../sweetAlerts/SweetAlerts";

export const UserConfirmation = () => {
	const [showInfo, setShowInfo] = useState(false);
	const [params] = useSearchParams();

	const navigate = useNavigate(); //Descomentar cuando se vaya a configurar el enlace con las API de autenticacio

	const ConfirmCount = useCallback(async() => {
		var response = await ConfirmationUser(params);

			if (response.isSuccess) {
				const result = await SwalSuccess("Cuenta Verificada", response.message);
				if (result.isConfirmed) {
					navigate("/");
				}
			} else {
				const result = await SwalFailed(
					"Oops...",
					response.message,
					"Solicita ayuda del administrador o crea una nueva cuenta",
				);
				if (result.isConfirmed) {
					navigate("/");
				}
			}
		setShowInfo(false);
	  },[navigate, params]
	)
	

	useEffect(() => {
		setShowInfo(true);

		// const ConfirmCount = async () => {
		// 	var response = await ConfirmationUser(params);

		// 	if (response.isSuccess) {
		// 		const result = await SwalSuccess("Cuenta Verificada", response.message);
		// 		if (result.isConfirmed) {
		// 			navigate("/");
		// 		}
		// 	} else {
		// 		const result = await SwalFailed(
		// 			"Oops...",
		// 			response.message,
		// 			"Solicita ayuda del administrador o crea una nueva cuenta",
		// 		);
		// 		if (result.isConfirmed) {
		// 			navigate("/");
		// 		}
		// 	}

		// 	setShowInfo(false);
		// };

		setTimeout(() => {
			ConfirmCount();
		}, 5000);
	//}, [navigate, params]);
	}, [ConfirmCount]);

	return (
		<div className="w-full min-h-screen flex items-center justify-center">
			{showInfo && <Loading />}
		</div>
	);
};
