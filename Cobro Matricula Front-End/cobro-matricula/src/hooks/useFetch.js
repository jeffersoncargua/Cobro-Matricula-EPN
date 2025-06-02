
export const useFetch = async({verbose,route,objectRequest = null,query = ''}) => {
  
    //verbose - Es el verbo que se va a utilizar para realizar la peticion al API : GET,POST,PUT y DELETE
    //route - Esta puede incluir los queries que se emplearan en las solicitudes

    const apiUrl = process.env.REACT_APP_API_URL;

    var response;

    switch (verbose) {
        case 'GET': 
                    var apiResponse = await fetch(`${apiUrl}/${route+query}`,{
                        method: verbose,
                        headers:{
                            'Content-Type': 'application/json',
                            'Accept' : 'application/json'
                        }             
                    });
                    response = apiResponse.json();

            break;
    
        default: 
                    var apiResponse2 = await fetch(`${apiUrl}/${route+query}`,{
                        method: verbose,
                        headers:{
                            'Content-Type': 'application/json',
                            'Accept' : 'application/json',
                            'Access-Control-Allow-Origin' : `${apiUrl}`
                        },
                        body : JSON.stringify(objectRequest)
                    });
                    response= apiResponse2.json();
            break;
    }

    return response;
    //.catch(() => window.location.href = "http://localhost:3000/manage/registration");

}
