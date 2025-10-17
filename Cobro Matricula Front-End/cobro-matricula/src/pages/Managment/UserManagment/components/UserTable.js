export const UserTable = ({ handleUpdatedUser, handleDeletedUser, users }) => {
	return (
		<div className="relative overflow-x-auto shadow-md sm:rounded-lg">
			<table className="w-full text-xs md:text-sm text-left rtl:text-right text-slate-900 dark:text-gray-400 mt-14">
				<thead className="text-xs md:text-sm uppercase bg-gray-50/40 dark:bg-gray-700 dark:text-gray-400">
					<tr>
						<th scope="col" className="px-6 py-3">
							Nombre
						</th>
						<th scope="col" className="px-6 py-3">
							Apellido
						</th>
						<th scope="col" className="px-6 py-3">
							Ciudad
						</th>
						<th scope="col" className="px-6 py-3">
							Teléfono
						</th>
						<th scope="col" className="px-6 py-3">
							Correo
						</th>
						<th scope="col" className="px-6 py-3">
							<span className="sr-only">Acciones</span>
						</th>
					</tr>
				</thead>
				<tbody>
					{users.map((user) => (
						<tr
							key={user.email}
							className="bg-white/80 border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200 hover:bg-gray-50/90 dark:hover:bg-gray-600"
						>
							<th
								scope="row"
								className="px-6 py-4 font-medium text-slate-900 whitespace-nowrap dark:text-white"
							>
								{user.name}
							</th>
							<td className="px-6 py-4">{user.lastName}</td>
							<td className="px-6 py-4">{user.city}</td>
							<td className="px-6 py-4">{user.phone}</td>
							<td className="px-6 py-4">{user.email}</td>
							<td className="px-6 py-4 text-right flex justify-center gap-x-4 ">
								<button
									type="button"
									onClick={() => handleUpdatedUser(user)}
									className="px-2.5 py-2.5 bg-blue-500 hover:bg-blue-600 font-medium text-white dark:text-blue-500 flex items-center justify-center rounded-lg "
								>
									<svg
										xmlns="http://www.w3.org/2000/svg"
										fill="currentColor"
										className="bi bi-person-fill-up me-2.5 w-5 h-5 "
										viewBox="0 0 16 16"
									>
										<path d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7m.354-5.854 1.5 1.5a.5.5 0 0 1-.708.708L13 11.707V14.5a.5.5 0 0 1-1 0v-2.793l-.646.647a.5.5 0 0 1-.708-.708l1.5-1.5a.5.5 0 0 1 .708 0M11 5a3 3 0 1 1-6 0 3 3 0 0 1 6 0" />
										<path d="M2 13c0 1 1 1 1 1h5.256A4.5 4.5 0 0 1 8 12.5a4.5 4.5 0 0 1 1.544-3.393Q8.844 9.002 8 9c-5 0-6 3-6 4" />
									</svg>
									Editar
								</button>
								<button
									type="button"
									onClick={() => handleDeletedUser(user.email)}
									className="px-2.5 py-2.5 bg-red-500 hover:bg-red-600 font-medium text-white dark:text-blue-500 flex items-center justify-center rounded-lg "
								>
									<svg
										xmlns="http://www.w3.org/2000/svg"
										fill="currentColor"
										className="bi bi-person-fill-x me-2.5 w-5 h-5"
										viewBox="0 0 16 16"
									>
										<path d="M11 5a3 3 0 1 1-6 0 3 3 0 0 1 6 0m-9 8c0 1 1 1 1 1h5.256A4.5 4.5 0 0 1 8 12.5a4.5 4.5 0 0 1 1.544-3.393Q8.844 9.002 8 9c-5 0-6 3-6 4" />
										<path d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7m-.646-4.854.646.647.646-.647a.5.5 0 0 1 .708.708l-.647.646.647.646a.5.5 0 0 1-.708.708l-.646-.647-.646.647a.5.5 0 0 1-.708-.708l.647-.646-.647-.646a.5.5 0 0 1 .708-.708" />
									</svg>
									Eliminar
								</button>
							</td>
						</tr>
					))}
				</tbody>
			</table>
		</div>
	);
};
