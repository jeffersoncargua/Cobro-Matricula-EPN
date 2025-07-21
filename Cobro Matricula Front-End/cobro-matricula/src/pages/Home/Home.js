import { UserCard, Login, ModalRecover } from './components';

import Admin from '../../assets/administrador.png';
import Student from '../../assets/estudiante.png';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { logout } from '../../redux/userSlice';
import { LoadingSquid } from '../../components';

export const Home = () => {

    const [enableForm,setEnableForm] = useState(false);
    const [enableModalRecover, setEnableModalRecover] = useState(false);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();
    const dispatch = useDispatch();

    var user = useSelector((state) => state.userState.user);
    
    const handleOption = (selected) => {
        
        switch(selected){
            case 'Ingresar' : setEnableForm(!enableForm);
                break;
            case 'Calcular': navigate('/calculator');
                break;
            case 'Cancelar': setEnableForm(!enableForm);
                break;
            case 'Cerrar Sesión' : dispatch(logout());
                break;
            default: 
                break;
        }
    }

    return (
    <div className="container mx-auto">
        <div className='w-full min-h-screen flex items-center justify-center' > 
            <div className=" w-[80%] sm:w-[80%] md:w-1/2 lg:w-1/3 h-full flex flex-col gap-y-3 justify-center items-center brightness-105 bg-gradient-to-r from-cyan-300/70 from-20% via-green-300 via-30% to-indigo-500/70 to-80% border border-slate-200 rounded-md ">
                <UserCard Photo={Admin} User={'Administrador'} Action={ user === null ? 'Ingresar' : 'Cerrar Sesión'} handleOption={handleOption} enableForm={enableForm} />
                {enableForm && (
                    <Login setEnableForm={setEnableForm} setEnableModalRecover={setEnableModalRecover} setLoading={setLoading} />    
                )}
                
                <UserCard Photo={Student} User={'Estudiante'} Action={'Calcular'} handleOption={handleOption} enableForm={enableForm} />
            </div>
        </div>
        
        {enableModalRecover && (
            <ModalRecover enableModalRecover={enableModalRecover} setEnableModalRecover={setEnableModalRecover} />
        )}
        {loading && (<LoadingSquid />)}
        
    </div>
  )
}
