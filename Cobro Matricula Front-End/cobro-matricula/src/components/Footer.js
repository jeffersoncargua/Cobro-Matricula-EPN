import { Link } from "react-router-dom";

export const Footer = () => {
	return (
		<footer className="bg-gradient-to-r from-indigo-500 via-purple-500 to-indigo-500 shadow-sm dark:bg-gray-800">
			<div className="w-full p-4 flex justify-center">
				<span className="text-sm text-slate-900 dark:text-gray-400">
					© 2025
					<Link href="https://flowbite.com/" className="hover:underline">
						{" "}
						UniversidadXYZ™{" "}
					</Link>
					. Derechos reservados.
				</span>
			</div>
		</footer>
	);
};
