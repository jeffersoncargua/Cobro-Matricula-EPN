import { ButtonLoading } from "../../../components";
import { useCallback, useState } from "react";
import { ForgetPass } from "../../../apiServices/UserServices";
import { ErrorMessageValidator } from "../../../components";

//import { useNavigate } from 'react-router-dom';
import { SwalSuccess, SwalFailed } from "../../../sweetAlerts/SweetAlerts";
import { useForm } from "react-hook-form";
import { message, patterns } from "../../../utility/ValidationUser";

export const ModalRecover = ({ enableModalRecover, setEnableModalRecover }) => {
	const {
		register,
		handleSubmit,
		formState: { errors },
	} = useForm();

	const [showButtonLoading, setShowButtonLoading] = useState(false);
	//const navigate = useNavigate();	


	const HandleSubmitRecover = useCallback(async(forgetResquest) => {
		setShowButtonLoading(true);

		console.log(forgetResquest);

		var response = await ForgetPass(forgetResquest.email);

		if (response.isSuccess) {
			//SwalSuccess para cuando la respuesta es positiva desde el api
			const result = await SwalSuccess("Solicitud Enviada", response.message);
			if (result.isConfirmed) {
				setEnableModalRecover(false);
			}
		} else {
			//SwalFailed para cuando la respuesta es positiva desde el api
			const result = await SwalFailed(
				"Oops...",
				response.message,
				"Solicita ayuda del administrador o crea una nueva cuenta",
			);
			if (result.isConfirmed) {
				setEnableModalRecover(false);
			}
		}

		setShowButtonLoading(false);
	},[setEnableModalRecover]);

	// const HandleSubmitRecover = async (forgetResquest) => {
	// 	setShowButtonLoading(true);

	// 	console.log(forgetResquest);

	// 	var response = await ForgetPass(forgetResquest.email);

	// 	if (response.isSuccess) {
	// 		//SwalSuccess para cuando la respuesta es positiva desde el api
	// 		const result = await SwalSuccess("Solicitud Enviada", response.message);
	// 		if (result.isConfirmed) {
	// 			setEnableModalRecover(false);
	// 		}
	// 	} else {
	// 		//SwalFailed para cuando la respuesta es positiva desde el api
	// 		const result = await SwalFailed(
	// 			"Oops...",
	// 			response.message,
	// 			"Solicita ayuda del administrador o crea una nueva cuenta",
	// 		);
	// 		if (result.isConfirmed) {
	// 			setEnableModalRecover(false);
	// 		}
	// 	}

	// 	setShowButtonLoading(false);
	// };

	return (
		<div className="">
			{/* <!-- Main modal --> */}
			<div
				id="authentication-modal"
				tabindex="-1"
				aria-hidden="true"
				className={`${!enableModalRecover && "hidden"} overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 flex justify-center items-center w-full md:inset-0 h-[calc(100%)] max-h-full bg-gray-50/50`}
			>
				<div className="relative p-4 w-full max-w-lg max-h-full">
					{/* <!-- Modal content --> */}
					<div className="relative bg-white rounded-lg shadow-sm dark:bg-gray-700">
						{/* <!-- Modal header --> */}
						<div className="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600 border-gray-200">
							<h3 className="text-xl font-semibold text-gray-900 dark:text-white">
								Recuperar contraseña
							</h3>
							<button
								type="button"
								onClick={() => setEnableModalRecover(false)}
								className="end-2.5 text-gray-400 bg-transparent hover:bg-gray-200 hover:text-gray-900 rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center dark:hover:bg-gray-600 dark:hover:text-white"
								data-modal-hide="authentication-modal"
							>
								<svg
									className="w-3 h-3"
									aria-hidden="true"
									xmlns="http://www.w3.org/2000/svg"
									fill="none"
									viewBox="0 0 14 14"
								>
									<path
										stroke="currentColor"
										strokeLinecap="round"
										strokeLinejoin="round"
										strokeWidth="2"
										d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"
									/>
								</svg>
								<span className="sr-only">Close modal</span>
							</button>
						</div>
						{/* <!-- Modal body --> */}
						<div className="p-4 md:p-5">
							<form
								className="group text-slate-900 p-4 border border-slate-100 rounded-lg "
								onSubmit={handleSubmit(HandleSubmitRecover)}
							>
								<div className="relative mb-1">
									<div className="absolute inset-y-0 start-0 flex items-center ps-3.5 pointer-events-none">
										<svg
											className="w-4 h-4 text-gray-500 dark:text-gray-400"
											aria-hidden="true"
											xmlns="http://www.w3.org/2000/svg"
											fill="currentColor"
											viewBox="0 0 20 16"
										>
											<path d="m10.036 8.278 9.258-7.79A1.979 1.979 0 0 0 18 0H2A1.987 1.987 0 0 0 .641.541l9.395 7.737Z" />
											<path d="M11.241 9.817c-.36.275-.801.425-1.255.427-.428 0-.845-.138-1.187-.395L0 2.6V14a2 2 0 0 0 2 2h16a2 2 0 0 0 2-2V2.5l-8.759 7.317Z" />
										</svg>
									</div>
									<input
										type="text"
										id="email"
										{...register("email", {
											required: message.req.email,
											pattern: {
												value: patterns.email,
												message: message.email,
											},
										})}
										className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full ps-10 p-2.5  dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
										placeholder="example@example.com"
									/>
								</div>
								{errors.email && (
									<ErrorMessageValidator message={errors.email.message} />
								)}

								{showButtonLoading
									? <ButtonLoading />
									: <button
											type="submit"
											className={`text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 cursor-pointer font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800`}
										>
											Enviar
										</button>}
							</form>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};
