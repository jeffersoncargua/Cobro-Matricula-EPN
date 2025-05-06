import { Link } from "react-router-dom"

export const Footer = () => {
  return (
    <footer className="bg-white rounded-lg shadow-sm m-4 dark:bg-gray-800">
        <div className="w-full mx-auto max-w-screen-xl p-4 flex justify-center">
        <span className="text-sm text-gray-500 dark:text-gray-400">© 2025
            <Link href="https://flowbite.com/" className="hover:underline"> UniversidadXYZ™ </Link>
            . Derechos reservados.
        </span>
        </div>
    </footer>

  )
}
