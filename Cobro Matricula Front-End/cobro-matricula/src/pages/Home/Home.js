import { UserCard, Login } from './components';

import Admin from '../../assets/administrador.png';
import Student from '../../assets/estudiante.png';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export const Home = () => {

    const [enableForm,setEnableForm] = useState(false);
    const navigate = useNavigate();
    
    const handleOption = (selected) => {
        
        switch(selected){
            case 'Ingresar' : setEnableForm(!enableForm);
                break;
            case 'Calcular': navigate('/calculadora');
                break;
            default: 
                break;
        }
    }

    return (
    <div className="w-full mt-10">
        <div className="flex flex-col items-center gap-y-3 mx-auto max-w-md bg-gradient-to-r from-cyan-300 from-10% via-green-300 via-30% to-indigo-300 to-40% border border-slate-200 rounded-md ">
            <UserCard Photo={Admin} User={'Administrador'} Action={'Ingresar'} handleOption={handleOption} />
            {enableForm && (
                <Login />
                
            )}
            <UserCard Photo={Student} User={'Estudiante'} Action={'Calcular'} handleOption={handleOption}/>
        </div>
    </div>
  )
}
