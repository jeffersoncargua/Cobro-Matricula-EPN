import { useCallback, useState } from "react";
import { UpdateUser } from "../../../../apiServices/UserServices";
import { SwalFailed, SwalUpdated } from "../../../../sweetAlerts/SweetAlerts";
import { message, patterns } from "../../../../utility/ValidationUser";
import { ButtonLoading } from "../../../../components";
import { useForm } from "react-hook-form";
import { ErrorMessageValidator } from "../../../../components";

export const ModalUpdatedUser = ({ enableModal, setEnableModal, user,fetchData}) => {
	// const [enablePass, setEnablePass] = useState(false);
	// const [enableConfirmPass, setEnableConfirmPass] = useState(false);

	const [showButtonLoading, setShowButtonLoading] = useState(false);
	const {
		register,
		handleSubmit,
		formState: { errors },
	} = useForm();	

	const HandleSubmit = useCallback(async(userUpdated) => {
		setShowButtonLoading(true);

		var response = await UpdateUser(userUpdated);

		if (response.isSuccess) {
			const result = await SwalUpdated(
				"Exito!!",
				response.message,
				"https://i.gifer.com/SWYA.gif",
			);
			if (result.isConfirmed) {
				setEnableModal(false);
			}
		} else {
			const result = await SwalFailed(
				"Oops",
				response.message,
				"Por favor, inténtalo más tarde",
			);
			if (result.isConfirmed) {
				setEnableModal(false);
			}
		}
		setShowButtonLoading(false);
		fetchData();
	},[setEnableModal,fetchData]);


	// const HandleSubmit = async (userUpdated) => {
	// 	setShowButtonLoading(true);

	// 	var response = await UpdateUser(userUpdated);

	// 	if (response.isSuccess) {
	// 		const result = await SwalUpdated(
	// 			"Exito!!",
	// 			response.message,
	// 			"https://i.gifer.com/SWYA.gif",
	// 		);
	// 		if (result.isConfirmed) {
	// 			setEnableModal(false);
	// 		}
	// 	} else {
	// 		const result = await SwalFailed(
	// 			"Oops",
	// 			response.message,
	// 			"Por favor, inténtalo más tarde",
	// 		);
	// 		if (result.isConfirmed) {
	// 			setEnableModal(false);
	// 		}
	// 	}

	// 	setShowButtonLoading(false);
	// };

	return (
		<div className="">
			{/* <!-- Main modal --> */}
			<div
				id="authentication-modal"
				tabIndex="-1"
				aria-hidden="true"
				className={`${!enableModal && "hidden"} overflow-y-auto overflow-x-hidden fixed top-0 right-0 left-0 z-50 flex justify-center items-center w-full md:inset-0 h-[calc(100%)] max-h-full bg-gray-50/50`}
			>
				<div className="relative p-4 w-full max-w-lg max-h-full">
					{/* <!-- Modal content --> */}
					<div className="relative bg-white rounded-lg shadow-sm dark:bg-gray-700">
						{/* <!-- Modal header --> */}
						<div className="flex items-center justify-between p-4 md:p-5 border-b rounded-t dark:border-gray-600 border-gray-200">
							<h3 className="text-xl font-semibold text-gray-900 dark:text-white">
								Actualizar Usuario
							</h3>
							<button
								type="button"
								onClick={() => setEnableModal(false)}
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
								onSubmit={handleSubmit(HandleSubmit)}
							>
								<div className="grid gap-6 mb-6 md:grid-cols-2  ">
									<div>
										<label
											htmlFor="name"
											className="block mb-2 text-sm font-medium  dark:text-white"
										>
											Nombre
										</label>
										<input
											type="text"
											id="name"
											{...register("name", {
												required: message.req.name,
												pattern: {
													value: patterns.letters,
													message: message.name,
												},
											})}
											className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
											placeholder="Antonio"
											defaultValue={user.name}
										/>
										{errors.name && (
											<ErrorMessageValidator message={errors.name.message} />
										)}
									</div>
									<div>
										<label
											htmlFor="lastName"
											className="block mb-2 text-sm font-medium dark:text-white"
										>
											Apellido
										</label>
										<input
											type="text"
											id="lastName"
											{...register("lastName", {
												required: message.req.lastName,
												pattern: {
													value: patterns.letters,
													message: message.lastName,
												},
											})}
											className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
											placeholder="Sanchez"
											defaultValue={user.lastName}
										/>
										{errors.lastName && (
											<ErrorMessageValidator
												message={errors.lastName.message}
											/>
										)}
									</div>
									<div>
										<label
											htmlFor="city"
											className="block mb-2 text-sm font-medium  dark:text-white"
										>
											Ciudad
										</label>
										<input
											type="text"
											id="city"
											{...register("city", {
											required: message.req.city
										})}
											className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
											placeholder="Quito"
											defaultValue={user.city}
										/>
										{errors.city && (
											<ErrorMessageValidator message={errors.city.message} />
										)}
									</div>
									<div>
										<label
											htmlFor="phone"
											className="block mb-2 text-sm font-medium  dark:text-white"
										>
											Telefono: +593
										</label>
										<input
											type="tel"
											id="phone"
											{...register("phone", {
											required: message.req.phone,
											pattern: {
												value: patterns.numbers,
												message: message.phone,
											},
										})}
											className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
											placeholder="0987654321"
											pattern="[0-9]{10}"
											defaultValue={user.phone}
										/>
										{errors.phone && (
											<ErrorMessageValidator message={errors.phone.message} />
										)}
									</div>
								</div>
								<div className="mb-6">
									<label
										htmlFor="email"
										className="block mb-2 text-sm font-medium  dark:text-white"
									>
										Correo Electrónico
									</label>
									<input
										disabled
										type="email"
										id="email"
										{...register("email", {
											required: message.req.email,
											pattern: {
												value: patterns.email,
												message: message.email,
											},
										})}
										className="bg-gray-50 border border-gray-300  text-slate-800 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
										placeholder="example@example.com"
										value={user.email}
									/>
									{errors.email && (
										<ErrorMessageValidator message={errors.email.message} />
									)}
								</div>
								{showButtonLoading
									? <ButtonLoading />
									: <button
											type="submit"
											className={`text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 cursor-pointer font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800`}
										>
											Editar
										</button>}
							</form>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};
