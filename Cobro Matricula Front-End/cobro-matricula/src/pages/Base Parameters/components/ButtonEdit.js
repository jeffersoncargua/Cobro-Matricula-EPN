//import {useEffect} from 'react'
import { useEffect, useState } from "react";
import { GetToken } from "../../../security/TokenSecurity";

export const ButtonEdit = ({ setEnableModalParameters }) => {
	const [enableButtonEdit, setEnableButtonEdit] = useState(false);

	useEffect(() => {
		var token = GetToken();
		if (token !== null) {
			setEnableButtonEdit(true);
		}
	}, []);

	return (
		// <!-- From Uiverse.io by Javierrocadev -->
		<button
			type="button"
			onClick={() => setEnableModalParameters(true)}
			disabled={!enableButtonEdit}
			class="overflow-hidden w-16 p-2 h-10 bg-purple-500 text-white border-none rounded-md text-xs md:text-sm cursor-pointer relative z-10 group"
		>
			<span className="font-semibold">Editar</span>
			<span class="absolute w-36 h-32 -top-8 -left-2 bg-white rotate-12 transform scale-x-0 group-hover:scale-x-100 transition-transform group-hover:duration-500 duration-1000 origin-left"></span>
			<span class="absolute w-36 h-32 -top-8 -left-2 bg-blue-400 rotate-12 transform scale-x-0 group-hover:scale-x-100 transition-transform group-hover:duration-700 duration-700 origin-left"></span>
			<span class="absolute w-36 h-32 -top-8 -left-2 bg-blue-600 rotate-12 transform scale-x-0 group-hover:scale-x-50 transition-transform group-hover:duration-1000 duration-500 origin-left"></span>
			<span class="group-hover:opacity-100 group-hover:duration-1000 duration-100 opacity-0 absolute top-2.5 left-6 z-10">
				<svg
					xmlns="http://www.w3.org/2000/svg"
					fill="currentColor"
					class="bi bi-pencil-square w-5 h-5"
					viewBox="0 0 16 16"
				>
					<path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
					<path
						fill-rule="evenodd"
						d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z"
					/>
				</svg>
			</span>
		</button>
	);
};
