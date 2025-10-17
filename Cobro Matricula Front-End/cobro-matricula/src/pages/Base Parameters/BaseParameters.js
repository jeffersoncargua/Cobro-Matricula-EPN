import { useRef, useState } from "react";
import { useParams } from "react-router-dom";
import {
	GetBaseParameters,
	UpdateParameters,
} from "../../apiServices/BaseParametersServices";
import { LoadingSquid } from "../../components";
import { SwalFailed, SwalSuccess } from "../../sweetAlerts/SweetAlerts";
import {
	ButtonEdit,
	ModalEditParameters,
	TableParameters,
	TextPressure,
} from "./components";

export const BaseParameters = () => {
	const params = useParams();
	const [title, setTitle] = useState(params.title || "Parámetros Base");
	const [loading, setLoading] = useState(false);
	const titleRef = useRef("");
	const [baseParameters, setBaseParameters] = useState(null);
	const [enableModalParameters, setEnableModalParameters] = useState(false);

	const handleFetchData = async () => {
		setLoading(true);
		var response = await GetBaseParameters(+titleRef.current.value);

		console.log(response);

		if (response.statusCode === 401) {
			SwalFailed("Aviso", response.message, "No lo vuelva a hacer!!!");
		} else if (response.statusCode === 404) {
			SwalFailed("Error", response.message, "Por favor intente más tarde");
		} else if (response.statusCode === 500) {
			SwalFailed("Error", response.message, "Por favor intente más tarde");
		}

		if (response.isSuccess) {
			setBaseParameters(response.result);
		} else {
			setBaseParameters(null);
		}

		setLoading(false);
	};

	const handleChangeParameters = async () => {
		switch (titleRef.current.value) {
			case "1":
				setTitle("Ingeniería");
				handleFetchData();
				break;

			case "2":
				setTitle("Tecnología");
				handleFetchData();
				break;
			default:
				setTitle("Parámetros Base");
				setBaseParameters(null);
				break;
		}
	};

	const handleEditParameters = async (updateParameters) => {
		setLoading(true);
		setEnableModalParameters(false);
		console.log(updateParameters);

		const response = await UpdateParameters(
			updateParameters.id,
			updateParameters,
		);

		console.log(response);

		if (response.statusCode === 401) {
			SwalFailed("Aviso", response.message, "No lo vuelva a hacer!!!");
		} else if (response.statusCode === 404) {
			SwalFailed("Error", response.message, "Por favor intente más tarde");
		} else if (response.statusCode === 400) {
			SwalFailed("Error", response.message, "Por favor intente más tarde");
		}

		if (response.isSuccess) {
			const result = await SwalSuccess(
				"Éxito",
				response.message,
				"Parámetros actualizados correctamente",
			);
			if (result.isConfirmed) {
				setBaseParameters(response.result);
			}
		} else {
			setBaseParameters(null);
			setTitle("Parámetros Base");
		}

		setEnableModalParameters(false);
		setLoading(false);
	};

	return (
		<div className="container mx-auto">
			<div className="w-full min-h-screen flex items-center justify-center">
				<div className=" w-[90%] sm:w-[50%] h-full flex flex-col gap-y-3 justify-center items-center m-10">
					{/* <h1 className="my-1.5 text-center font-bold text-white text-5xl">Parámetros Base Ingeniería</h1> */}
					<TextPressure
						text={title}
						flex={true}
						alpha={false}
						stroke={false}
						width={true}
						weight={true}
						italic={true}
						textColor="#ffffff"
						strokeColor="#ff0000"
						minFontSize={26}
					/>

					<div className="w-full flex flex-wrap items-center justify-between gap-x-3 p-8 mb-3 md:mb-0 bg-white/90 rounded-lg">
						<select
							name=""
							id=""
							className="p-2.5 rounded-lg text-sm text-center grow bg-transparent/10"
							onChange={() => handleChangeParameters()}
							ref={titleRef}
						>
							<option value="" className="italic bg-transparent/40">
								--- Seleccione la formación académica ---
							</option>
							<option value="1"> Ingeniería </option>
							<option value="2"> Tecnología </option>
						</select>

						{baseParameters !== null && (
							<ButtonEdit setEnableModalParameters={setEnableModalParameters} />
						)}
					</div>

					{loading && <LoadingSquid />}

					{baseParameters !== null && (
						<TableParameters baseParameters={baseParameters} />
					)}

					{enableModalParameters && (
						<ModalEditParameters
							enableModalParameters={enableModalParameters}
							setEnableModalParameters={setEnableModalParameters}
							handleEditParameters={handleEditParameters}
							baseParameters={baseParameters}
						/>
					)}
				</div>
			</div>
		</div>
	);
};
