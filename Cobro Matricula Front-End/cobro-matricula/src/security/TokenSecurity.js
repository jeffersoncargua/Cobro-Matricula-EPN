import Cookies from 'js-cookie';

function SendToken(token) {
    const cookieToken = token;
    Cookies.set('jwtToken',cookieToken,{
        secure: true,
        sameSite: 'Strict',
        expires: 30
    })
}

function GetToken(){
    const token = Cookies.get('jwtToken');
    console.log(token);
    if (token !== null) {
        return token;
    }

    return null;
    
}

function DeleteToken(){
    
    Cookies.remove('jwtToken',{
        secure : true,
        sameSite: 'Strict'
    });
}

export {SendToken,GetToken, DeleteToken}