import {NavLink, Link} from 'react-router-dom';
import Logo from '../assets/logo.png';
import { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { getUser, logout } from '../redux/userSlice';


export const Header = () => {

  const [enableDrop, setEnableDrop] = useState(false);
  //const [enableDropParameters, setEnableDropParameters] = useState(false);
  const [enableMenu, setEnableMenu] = useState(false);
  const dispatch = useDispatch();

  const user = useSelector(state => state.userState);

  useEffect(() => {
    dispatch(getUser())
  }, [dispatch])

  const handleLogout = () => {
    dispatch(logout());
    setEnableDrop(false);
    setEnableMenu(false);
  }
  

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
      <div className="max-w-screen-xl flex flex-wrap items-center justify-start mx-auto p-4">
        <button data-collapse-toggle="navbar-multi-level" type="button" onClick={() => setEnableMenu(!enableMenu)} className="inline-flex items-center p-2 w-10 h-10 justify-center text-sm text-gray-500 rounded-lg md:hidden hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-gray-200 dark:text-gray-400 dark:hover:bg-gray-700 dark:focus:ring-gray-600" aria-controls="navbar-multi-level" aria-expanded="false">
            <span className="sr-only">Open main menu</span>
            <svg className="w-5 h-5" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 17 14">
                <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M1 1h15M1 7h15M1 13h15"/>
            </svg>
        </button>
        <div className={`${!enableMenu && 'hidden'}  w-full md:block md:w-auto`} id="navbar-multi-level">
          <ul className="flex flex-col font-medium p-4 md:p-0 mt-4 border border-gray-100 rounded-lg bg-gray-50 md:space-x-8 rtl:space-x-reverse md:flex-row md:mt-0 md:border-0 md:bg-white">
            <li className="flex hover:text-blue-700">
              <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" className="bi bi-house-door w-5 h-5 ms-1.5 me-1.5" viewBox="0 0 16 16">
                <path d="M8.354 1.146a.5.5 0 0 0-.708 0l-6 6A.5.5 0 0 0 1.5 7.5v7a.5.5 0 0 0 .5.5h4.5a.5.5 0 0 0 .5-.5v-4h2v4a.5.5 0 0 0 .5.5H14a.5.5 0 0 0 .5-.5v-7a.5.5 0 0 0-.146-.354L13 5.793V2.5a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1.293zM2.5 14V7.707l5.5-5.5 5.5 5.5V14H10v-4a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5v4z"/>
              </svg>
              <NavLink to={'/'} onClick={()=>{setEnableMenu(false)}} className="block py-2 px-3 rounded-sm md:bg-transparent md:p-0" aria-current="page" >Inicio</NavLink>
            </li>
            {(user.rol === process.env.REACT_APP_ROLEMANAGE || user.rol === process.env.REACT_APP_ROLECOLLAB) && 
            (
              <>
                <li className='hover:text-blue-700' >
                  <button id="dropdownNavbarNavLink" data-dropdown-toggle="dropdownNavbar" onClick={() => setEnableDrop(true)} className="flex items-center justify-between w-full py-2 px-3 text-gray-900 hover:bg-gray-100 md:hover:underline md:hover:underline-offset-8 md:hover:decoration-4 md:hover:bg-transparent md:border-0 md:hover:text-blue-700 md:p-0 md:w-auto">
                    <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-people w-5 h-5 ms-1.5 me-1.5" viewBox="0 0 16 16">
                      <path d="M15 14s1 0 1-1-1-4-5-4-5 3-5 4 1 1 1 1zm-7.978-1L7 12.996c.001-.264.167-1.03.76-1.72C8.312 10.629 9.282 10 11 10c1.717 0 2.687.63 3.24 1.276.593.69.758 1.457.76 1.72l-.008.002-.014.002zM11 7a2 2 0 1 0 0-4 2 2 0 0 0 0 4m3-2a3 3 0 1 1-6 0 3 3 0 0 1 6 0M6.936 9.28a6 6 0 0 0-1.23-.247A7 7 0 0 0 5 9c-4 0-5 3-5 4q0 1 1 1h4.216A2.24 2.24 0 0 1 5 13c0-1.01.377-2.042 1.09-2.904.243-.294.526-.569.846-.816M4.92 10A5.5 5.5 0 0 0 4 13H1c0-.26.164-1.03.76-1.724.545-.636 1.492-1.256 3.16-1.275ZM1.5 5.5a3 3 0 1 1 6 0 3 3 0 0 1-6 0m3-2a2 2 0 1 0 0 4 2 2 0 0 0 0-4"/>
                    </svg>
                    Gestion de Usuarios 
                    <svg className="w-2.5 h-2.5 ms-2.5" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 6">
                      <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="m1 1 4 4 4-4"/>
                    </svg>
                  </button>
                  {/* <!-- Dropdown menu --> */}
                  <div id="dropdownNavbar" onMouseLeave={() => setEnableDrop(false)} className={`z-10 md:absolute ${!enableDrop && 'hidden'} md:bg-slate-50/95 md:rounded-b-lg font-normal bg-white divide-y divide-gray-100 md:mt-2.5 md:border-b md:border-x md:border-slate-500 shadow-sm md:w-52`}  >
                      <ul className="py-2 text-sm text-gray-700 dark:text-gray-200" aria-labelledby="dropdownLargeButton">
                        <li className='md:hover:text-blue-700 md:hover:bg-gray-300'>
                          <NavLink to='/manage/registration' onClick={()=>{setEnableDrop(false);setEnableMenu(false)}} className="block px-4 py-2  ">Crear Usuarios</NavLink>
                        </li>
                        <li className='md:hover:text-blue-700 md:hover:bg-gray-300'>
                          <NavLink to='/manage/users' onClick={()=>{setEnableDrop(false);setEnableMenu(false)}} className="block px-4 py-2 ">Editar/Eliminar Usuarios</NavLink>
                        </li>
                      </ul>
                      {/* <div className="py-1">
                        <NavLink to='/' className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Sign out</NavLink>
                      </div> */}
                  </div>
                </li>
                <li className='flex hover:text-blue-700'>
                  {/* <button id="dropdownNavbarNavLink" data-dropdown-toggle="dropdownNavbar" onMouseOver={() => setEnableDropParameters(true)} onMouseLeave={() => setEnableDropParameters(false)} className="flex items-center justify-between w-full py-2 px-3 text-gray-900 hover:bg-gray-100 md:hover:bg-transparent md:border-0 md:hover:text-blue-700 md:p-0 md:w-auto dark:text-white md:dark:hover:text-blue-500 dark:focus:text-white dark:hover:bg-gray-700 md:dark:hover:bg-transparent">Parametros Base 
                    <svg className="w-2.5 h-2.5 ms-2.5" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 6">
                      <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="m1 1 4 4 4-4"/>
                    </svg>
                  </button>
                  <div id="dropdownNavbar" onMouseOver={() => setEnableDropParameters(true)} onMouseLeave={() => setEnableDropParameters(false)} className={`z-10 md:absolute ${!enableDropParameters && 'hidden'} font-normal bg-white divide-y divide-gray-100 rounded-lg md:border md:border-slate-500 shadow-sm md:w-44 dark:bg-gray-700 dark:divide-gray-600`}  >
                      <ul className="py-2 text-sm text-gray-700 dark:text-gray-200" aria-labelledby="dropdownLargeButton">
                        <li>
                          <NavLink to='parameters/ingenieria' onClick={()=>{setEnableDrop(false);setEnableMenu(false)}} className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">Ingeniería</NavLink>
                        </li>
                        <li>
                          <NavLink to='parameters/tecnologia' onClick={()=>{setEnableDrop(false);setEnableMenu(false)}} className="block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white">Tecnología</NavLink>
                        </li>
                      </ul>
                  </div> */}
                  <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" class="bi bi-calculator w-5 h-5 ms-1.5 me-1.5" viewBox="0 0 16 16">
                    <path d="M12 1a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H4a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1zM4 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2z"/>
                    <path d="M4 2.5a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 .5.5v2a.5.5 0 0 1-.5.5h-7a.5.5 0 0 1-.5-.5zm0 4a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5zm0 3a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5zm0 3a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5zm3-6a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5zm0 3a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5zm0 3a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5zm3-6a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5zm0 3a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5v4a.5.5 0 0 1-.5.5h-1a.5.5 0 0 1-.5-.5z"/>
                  </svg>
                  <NavLink to='parameters' onClick={()=>{setEnableDrop(false);setEnableMenu(false)}} className="block py-2 px-3 text-gray-900 rounded-sm hover:bg-gray-100 md:hover:bg-transparent md:border-0 md:hover:text-blue-700 md:p-0">Parametros Base</NavLink>
                </li>
              </>
              
            )
            }
            {(user.user !== null) && 
              (
              <li className='hover:text-blue-700'>
                <button onClick={() => handleLogout()} className="block py-2 px-3 text-gray-900 rounded-sm hover:bg-gray-100 md:hover:bg-transparent md:border-0 md:hover:text-blue-700 md:p-0 ">Cerrar Sesión</button>
              </li>
            )
            }
            
          </ul>
        </div>
      </div>
    </nav>
  </div>

  )
}
