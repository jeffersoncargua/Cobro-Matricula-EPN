import {NavLink, Link} from 'react-router-dom';
import Logo from '../assets/logo.png';
import { useState } from 'react';

export const Header = () => {

  const [enableDrop, setEnableDrop] = useState(false);
  const [enableMenu, setEnableMenu] = useState(false)

  return (
    
<div className="bg-white border-gray-200 dark:bg-gray-900">

  <div className=" flex items-center justify-center mx-auto p-4 bg-slate-200">
    <Link to='/' className="flex flex-wrap gap-y-2 items-center justify-center space-x-3 rtl:space-x-reverse  ">
        <img src={Logo} className="h-28 border border-slate-400" alt="Flowbite Logo" />
        <span className="font-dancing self-center text-3xl sm:text-5xl font-semibold whitespace-nowrap dark:text-white">Unversidad XYZ</span>
    </Link>
  </div>

  {/* Esto es para el navbar de la gestion */}
  <nav className="bg-white border-gray-200 dark:bg-gray-900 dark:border-gray-700">
    <div className="max-w-screen-xl flex flex-wrap items-center justify-end mx-auto p-4">
      <button data-collapse-toggle="navbar-multi-level" type="button" onClick={() => setEnableMenu(!enableMenu)} className="inline-flex items-center p-2 w-10 h-10 justify-center text-sm text-gray-500 rounded-lg md:hidden hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-gray-200 dark:text-gray-400 dark:hover:bg-gray-700 dark:focus:ring-gray-600" aria-controls="navbar-multi-level" aria-expanded="false">
          <span className="sr-only">Open main menu</span>
          <svg className="w-5 h-5" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 17 14">
              <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M1 1h15M1 7h15M1 13h15"/>
          </svg>
      </button>
      <div className={`${!enableMenu && 'hidden'}  w-full md:block md:w-auto`} id="navbar-multi-level">
        <ul className="flex flex-col font-medium p-4 md:p-0 mt-4 border border-gray-100 rounded-lg bg-gray-50 md:space-x-8 rtl:space-x-reverse md:flex-row md:mt-0 md:border-0 md:bg-white dark:bg-gray-800 md:dark:bg-gray-900 dark:border-gray-700">
          <li>
            <NavLink to={'/'} className="block py-2 px-3 text-white bg-blue-700 rounded-sm md:bg-transparent md:text-blue-700 md:p-0 md:dark:text-blue-500 dark:bg-blue-600 md:dark:bg-transparent" aria-current="page" end>Inicio</NavLink>
          </li>
          <li>
              <button id="dropdownNavbarNavLink" data-dropdown-toggle="dropdownNavbar" onMouseOver={() => setEnableDrop(true)} onMouseLeave={() => setEnableDrop(false)} className="flex items-center justify-between w-full py-2 px-3 text-gray-900 hover:bg-gray-100 md:hover:bg-transparent md:border-0 md:hover:text-blue-700 md:p-0 md:w-auto dark:text-white md:dark:hover:text-blue-500 dark:focus:text-white dark:hover:bg-gray-700 md:dark:hover:bg-transparent">Gestion de Usuarios 
                <svg className="w-2.5 h-2.5 ms-2.5" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 6">
                  <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="m1 1 4 4 4-4"/>
                </svg>
              </button>
              {/* <!-- Dropdown menu --> */}
              <div id="dropdownNavbar" onMouseOver={() => setEnableDrop(true)} onMouseLeave={() => setEnableDrop(false)} className={`z-10 md:absolute ${!enableDrop && 'hidden'} font-normal bg-white divide-y divide-gray-100 rounded-lg md:border md:border-slate-500 shadow-sm md:w-44 dark:bg-gray-700 dark:divide-gray-600`}  >
                  <ul className="py-2 text-sm text-gray-700 dark:text-gray-200" aria-labelledby="dropdownLargeButton">
                    <li>
                      <NavLink to='/manage/registration' className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">Crear Usuarios</NavLink>
                    </li>
                    <li>
                      <NavLink to='/manage/users' className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">Editar/Eliminar Usuarios</NavLink>
                    </li>
                  </ul>
                  {/* <div className="py-1">
                    <NavLink to='/' className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Sign out</NavLink>
                  </div> */}
              </div>
          </li>
          <li>
            <NavLink to='/' className="block py-2 px-3 text-gray-900 rounded-sm hover:bg-gray-100 md:hover:bg-transparent md:border-0 md:hover:text-blue-700 md:p-0 dark:text-white md:dark:hover:text-blue-500 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent">Parametros Base</NavLink>
          </li>
          <li>
            <NavLink to='/' className="block py-2 px-3 text-gray-900 rounded-sm hover:bg-gray-100 md:hover:bg-transparent md:border-0 md:hover:text-blue-700 md:p-0 dark:text-white md:dark:hover:text-blue-500 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent">Cerrar Sesión</NavLink>
          </li>
        </ul>
      </div>
    </div>
  </nav>

  
</div>

  )
}
